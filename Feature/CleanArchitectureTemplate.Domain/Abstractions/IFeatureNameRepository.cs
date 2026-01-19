using CleanArchitectureTemplate.Domain.Entities;

namespace CleanArchitectureTemplate.Domain.Abstractions;

public interface IFeatureNameRepository
{
    Task<FeatureName> GetById(long id);

    void Add(FeatureName FeatureName);

    void Update(FeatureName FeatureName);
}