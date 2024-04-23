using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Authors;

public class CreateAuthorDto
{
    [Required]
    [StringLength(AuthorConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public Guid CountryId { get; set; }
    public string ShortBio { get; set; }
}
