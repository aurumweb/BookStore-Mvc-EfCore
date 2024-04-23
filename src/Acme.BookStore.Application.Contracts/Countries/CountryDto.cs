using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Countries;

public class CountryDto : EntityDto<Guid>
{
    public string Name { get; set; }

}
