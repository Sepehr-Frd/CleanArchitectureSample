using Domain.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public required string Title { get; set; }

    public string? Description { get; set; }
}