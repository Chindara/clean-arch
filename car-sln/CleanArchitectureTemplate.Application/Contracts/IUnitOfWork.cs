namespace CleanArchitectureTemplate.Application.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync(long userId, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}