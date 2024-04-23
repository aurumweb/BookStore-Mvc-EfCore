using System;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Countries;

[Authorize(BookStorePermissions.Countries.Default)]
public class CountryAppService :
    CrudAppService<
        Country, //The Book entity
        CountryDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateCountryDto>, //Used to create/update a book
    ICountryAppService //implement the IBookAppService
{
    public CountryAppService(
        IRepository<Country, Guid> repository)
        : base(repository)
    {
        GetPolicyName = BookStorePermissions.Countries.Default;
        GetListPolicyName = BookStorePermissions.Countries.Default;
        CreatePolicyName = BookStorePermissions.Countries.Create;
        UpdatePolicyName = BookStorePermissions.Countries.Edit;
        DeletePolicyName = BookStorePermissions.Countries.Delete;
    }

}
