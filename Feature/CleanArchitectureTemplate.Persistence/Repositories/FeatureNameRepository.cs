using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Persistence.Repositories;

internal sealed class FeatureNameRepository : IFeatureNameRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FeatureNameRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<FeatureName?> GetById(long id)
    {
        var record = await _applicationDbContext.FeatureNames.Where(c => c.Id == id).FirstOrDefaultAsync();
        if (record is not null)
        {
            return record;
        }
        return null;
    }

    public void Add(FeatureName FeatureName)
    {
        _applicationDbContext.FeatureNames.Add(FeatureName);
    }

    public void Update(FeatureName FeatureName)
    {
        _applicationDbContext.FeatureNames.Update(FeatureName);
    }
}