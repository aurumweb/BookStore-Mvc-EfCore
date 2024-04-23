using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Authors;

public class Author : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public DateTime BirthDate { get; set; }
    public string ShortBio { get; set; }
    public Guid CountryId { get; set; }


    private Author()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Author(
        Guid id,
        [NotNull] string name,
        DateTime birthDate,
        Guid countryId,
        [CanBeNull] string shortBio = null)
        : base(id)
    {
        SetName(name);
        BirthDate = birthDate;
        ShortBio = shortBio;
        CountryId = countryId;
    }

    internal Author ChangeCountry(Guid countryId)
    {
        SetCountry(countryId);
        return this;
    }
    private void SetCountry(Guid countryId)
    {
        CountryId = countryId;
    }

    internal Author ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: AuthorConsts.MaxNameLength
        );
    }
}
