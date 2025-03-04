using CleanArchitectureTemplate.Application.DTO;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Primitives;
using CleanArchitectureTemplate.Persistence;
using MediatR;

namespace CleanArchitectureTemplate.Application.Users;

public record GetUsersQuery(long CompanyId, int Page, int Size) : IRequest<PagedResult<UsersResponse>>;

internal class GetUsersQueryHandler(IApplicationDbContext context) : IRequestHandler<GetUsersQuery, PagedResult<UsersResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PagedResult<UsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> userQuery = _context.Users;
        var userResponseQuery = userQuery
            .Where(c => c.CompanyId == request.CompanyId && c.Status != (int)UserStatus.Deleted)
            .Select(c => new UsersResponse(
                c.Id,
                c.FirstName,
                c.LastName,
                c.Email,
                c.Mobile,
                Enums.GetEnumValue<UserStatus>(c.Status),
                Enums.GetEnumValue<UserType>(c.UserType),
                c.Created,
                c.Modified
            ));
        PagedResult<UsersResponse> users = await PagedResult<UsersResponse>.CreateAsync(userResponseQuery, request.Page, request.Size);
        return users;
    }
}