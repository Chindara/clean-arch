using CleanArchitectureTemplate.Domain.Primitives;
using CleanArchitectureTemplate.Domain.Shared;

namespace CleanArchitectureTemplate.Domain.Entities;

public sealed class FeatureName : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }

    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
    public DateTime Modified { get; set; }
    public long ModifiedBy { get; set; }

    protected FeatureName() : base(default)
    {
        // Parameterless constructor for EF Core
    }

    private FeatureName(long companyId, string name) : base(companyId)
    {
        Name = name;
    }

    public static Result<FeatureName> Create(long companyId, string name)
    {
        if (String.IsNullOrEmpty(name))
            return Result.Failure<FeatureName>(new Error("FeatureName.Create", "Name is required"));

        var record = new FeatureName(companyId, name);
        return Result.Success<FeatureName>(record);
    }
}