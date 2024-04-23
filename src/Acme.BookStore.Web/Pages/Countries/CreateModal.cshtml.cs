using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Countries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Countries;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateCountryViewModel Country { get; set; }


    private readonly ICountryAppService _countryAppService;

    public CreateModalModel(
        ICountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }

    public async Task OnGetAsync()
    {
        Country = new CreateCountryViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _countryAppService.CreateAsync(
            ObjectMapper.Map<CreateCountryViewModel, CreateUpdateCountryDto>(Country)
        );
        return NoContent();
    }

    public class CreateCountryViewModel
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}
