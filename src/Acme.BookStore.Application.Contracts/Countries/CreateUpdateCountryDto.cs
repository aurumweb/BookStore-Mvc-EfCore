using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Countries;

public class CreateUpdateCountryDto
{
    [Required]
    [StringLength(CountryConsts.MaxNameLength)]
    public string Name { get; set; }

}
