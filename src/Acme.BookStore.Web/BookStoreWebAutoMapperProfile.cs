using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Countries;
using AutoMapper;

namespace Acme.BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel, CreateAuthorDto>();
        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel, UpdateAuthorDto>();
        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
        CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();


        CreateMap<CountryDto, CreateUpdateCountryDto>();
        CreateMap<Pages.Countries.CreateModalModel.CreateCountryViewModel, CreateUpdateCountryDto>();
        CreateMap<Pages.Countries.EditModalModel.EditCountryViewModel, CountryDto>();
        CreateMap<Pages.Countries.EditModalModel.EditCountryViewModel, CreateUpdateCountryDto>();
        CreateMap<CountryDto, Pages.Countries.EditModalModel.EditCountryViewModel>();
        
    }
}
