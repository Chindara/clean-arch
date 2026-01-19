//TODO: Implement UpdateFeatureNameCommand

using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.FeatureNames;

public record UpdateFeatureNameCommand(long UserId) : IRequest<Result<FeatureName>>;

internal class UpdateFeatureNameCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateFeatureNameCommand, Result<FeatureName>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<FeatureName>> Handle(UpdateFeatureNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}