public sealed class FeatureName : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }

    public DateTime CreatedDateTime { get; set; }
    public long CreatedUser { get; set; }
    public string CreatedMachine { get; set; }
    public DateTime ModifiedDateTime { get; set; }
    public long ModifiedUser { get; set; }
    public string ModifiedMachine { get; set; }

    protected FeatureName() : base(default)
    {
        // Parameterless constructor for EF Core
    }

    private FeatureName(long companyId, string name) : base(companyId)
    {
        Name = name;
    }

    public static Create(long companyId, string name)
    {
        if (String.IsNullOrEmpty(name))
            return Result.Failure<FeatureName>(new Error("FeatureName.Create", "Name is required"));

        var record = new FeatureName(companyId, name);
        return Result.Success<FeatureName>(record);
    }
}