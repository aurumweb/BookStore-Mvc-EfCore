using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Countries;

public class Country : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
}
