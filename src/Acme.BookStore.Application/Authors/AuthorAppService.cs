using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Countries;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.ObjectMapping;

namespace Acme.BookStore.Authors;

[Authorize(BookStorePermissions.Authors.Default)]
public class AuthorAppService : BookStoreAppService, IAuthorAppService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IRepository<Country, Guid> _countryRepository;
    private readonly AuthorManager _authorManager;

    public AuthorAppService(
        IAuthorRepository authorRepository,
        IRepository<Country, Guid> countryRepository,
        AuthorManager authorManager)
    {
        _authorRepository = authorRepository;
        _countryRepository = countryRepository;
        _authorManager = authorManager;
    }

    public async Task<AuthorDto> GetAsync(Guid id)
    {
        var author = await _authorRepository.GetAsync(id);
        return ObjectMapper.Map<Author, AuthorDto>(author);
    }

    public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Author.Name);
        }

        //Get the IQueryable<Book> from the repository
        var queryable = await _authorRepository.GetQueryableAsync();

        //Prepare a query to join authors and countries
        var query = from author in queryable
                    join country in await _countryRepository.GetQueryableAsync() on author.CountryId equals country.Id
                    select new { author, country };

        //Paging
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);


        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of AuhtorDto objects
        var authorDtos = queryResult.Select(x =>
        {
            var authorDto = ObjectMapper.Map<Author, AuthorDto>(x.author);
            authorDto.CountryName = x.country.Name;
            return authorDto;
        }).ToList();

        var totalCount = input.Filter == null
            ? await _authorRepository.CountAsync()
            : await _authorRepository.CountAsync(
                author => author.Name.Contains(input.Filter));

        return new PagedResultDto<AuthorDto>(
            totalCount,
            authorDtos
        );
    }

    public async Task<ListResultDto<CountryLookupDto>> GetCountryLookupAsync()
    {
        var countries = await _countryRepository.GetListAsync();

        return new ListResultDto<CountryLookupDto>(
            ObjectMapper.Map<List<Country>, List<CountryLookupDto>>(countries)
        );
    }

    [Authorize(BookStorePermissions.Authors.Create)]
    public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
    {
        var author = await _authorManager.CreateAsync(
            input.Name,
            input.BirthDate,
            input.CountryId,
            input.ShortBio
        );

        await _authorRepository.InsertAsync(author);

        return ObjectMapper.Map<Author, AuthorDto>(author);
    }

    [Authorize(BookStorePermissions.Authors.Edit)]
    public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
    {
        var author = await _authorRepository.GetAsync(id);

        if (author.Name != input.Name)
        {
            await _authorManager.ChangeNameAsync(author, input.Name);
        }

        author.BirthDate = input.BirthDate;
        author.ShortBio = input.ShortBio;

        await _authorRepository.UpdateAsync(author);
    }

    [Authorize(BookStorePermissions.Authors.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _authorRepository.DeleteAsync(id);
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"author.{nameof(Author.Name)}";
        }

        if (sorting.Contains("countryName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "countryName",
                "country.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"author.{sorting}";
    }

}
