using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Countries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.ObjectMapping;

namespace Acme.BookStore.Web.Pages.Countries;

public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditCountryViewModel Country { get; set; }

    private readonly ICountryAppService _countryAppService;

    public EditModalModel(ICountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var countryDto = await _countryAppService.GetAsync(id);
        Country = ObjectMapper.Map<CountryDto, EditCountryViewModel>(countryDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _countryAppService.UpdateAsync(
            Country.Id,
            ObjectMapper.Map<EditCountryViewModel, CreateUpdateCountryDto>(Country)
        );

        return NoContent();
    }

    public class EditCountryViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}
