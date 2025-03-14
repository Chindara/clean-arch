//TODO: Implement UpdateFeatureNameCommand

using ProjectName.Domain.Abstractions;
using ProjectName.Domain.Entities;
using ProjectName.Domain.Shared;
using MediatR;

namespace ProjectName.Application.FeatureNames;

public record DeleteFeatureNameCommand(long UserId, long Id) : IRequest<Result<FeatureName>>;

internal class DeleteFeatureNameCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteFeatureNameCommand, Result<FeatureName>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<FeatureName>> Handle(DeleteFeatureNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}