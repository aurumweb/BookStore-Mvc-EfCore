using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Authors;

public class CountryLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
