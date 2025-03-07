//TODO: Implement UpdateFeatureNameCommand

using ProjectName.Domain.Abstractions;
using ProjectName.Domain.Entities;
using ProjectName.Domain.Shared;
using MediatR;

namespace ProjectName.Application.FeatureNames;

public record UpdateFeatureNameCommand(long UserId, long Id) : IRequest<Result<FeatureName>>;

internal class UpdateFeatureNameCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateFeatureNameCommand, Result<FeatureName>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<FeatureName>> Handle(UpdateFeatureNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}