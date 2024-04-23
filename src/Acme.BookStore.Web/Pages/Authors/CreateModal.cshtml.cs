using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Acme.BookStore.Countries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Authors;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateAuthorViewModel Author { get; set; }
    public List<SelectListItem> Countries { get; set; }

    private readonly IAuthorAppService _authorAppService;

    public CreateModalModel(IAuthorAppService authorAppService)
    {
        _authorAppService = authorAppService;
    }

    public async Task OnGetAsync()
    {
        Author = new CreateAuthorViewModel();

        var countryLookup = await _authorAppService.GetCountryLookupAsync();
        Countries = countryLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateAuthorViewModel, CreateAuthorDto>(Author);
        await _authorAppService.CreateAsync(dto);
        return NoContent();
    }

    public class CreateAuthorViewModel
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        [SelectItems(nameof(Countries))]
        [DisplayName("Country")]
        public Guid CountryId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [TextArea]
        public string ShortBio { get; set; }
    }
}
