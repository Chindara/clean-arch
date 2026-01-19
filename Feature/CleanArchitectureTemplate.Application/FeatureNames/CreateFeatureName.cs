//TODO: Implement CreateFeatureNameCommand

using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.FeatureNames;

public record CreateFeatureNameCommand(long UserId) : IRequest<Result<FeatureName>>;

internal class CreateFeatureNameCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateFeatureNameCommand, Result<FeatureName>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<FeatureName>> Handle(CreateFeatureNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}