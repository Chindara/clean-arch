//TODO: Implement DeleteFeatureNameCommand

using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.FeatureNames;

public record DeleteFeatureNameCommand(long UserId) : IRequest<Result<FeatureName>>;

internal class DeleteFeatureNameCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteFeatureNameCommand, Result<FeatureName>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<FeatureName>> Handle(DeleteFeatureNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}