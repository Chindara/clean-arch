//TODO: Implement GetFeatureNameQuery

using CleanArchitectureTemplate.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Application.FeatureNames;

public record GetFeatureNameQuery(long companyId, long Id) : IRequest<FeatureNameResponse>;

internal class GetFeatureNameQueryHandler(IApplicationDbContext context) : IRequestHandler<GetFeatureNameQuery, FeatureNameResponse>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<FeatureNameResponse> Handle(GetFeatureNameQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}