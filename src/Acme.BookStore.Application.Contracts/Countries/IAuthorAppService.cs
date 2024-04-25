using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Countries;

public interface ICountryAppService :
    ICrudAppService< //Defines CRUD methods
        CountryDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateCountryDto> //Used to create/update a book
{
}