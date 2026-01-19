//TODO: Implement GetFeatureNamesQuery

using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Application.DTO;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Primitives;
using MediatR;

namespace CleanArchitectureTemplate.Application.FeatureNames;

public record GetFeatureNamesQuery(long CompanyId, int Page, int Size) : IRequest<PagedResult<FeatureNamesResponse>>;

internal class GetFeatureNamesQueryHandler(IApplicationDbContext context) : IRequestHandler<GetFeatureNamesQuery, PagedResult<FeatureNamesResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PagedResult<FeatureNamesResponse>> Handle(GetFeatureNamesQuery request, CancellationToken cancellationToken)
    {
        //var results = await PagedResult<FeatureNameResponses>.CreateAsync(query, request.Page, request.Size);
        //return results;

        throw new NotImplementedException();
    }
}