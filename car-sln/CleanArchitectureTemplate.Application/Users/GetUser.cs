﻿using CleanArchitectureTemplate.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Application.Users;

public record GetUserQuery(long CompanyId, long Id) : IRequest<UserResponse>;

internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
{
    private readonly IApplicationDbContext _context;

    public GetUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                                .Where(c => c.CompanyId == request.CompanyId && c.Id == request.Id)
                                .FirstAsync(cancellationToken);

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Mobile,
            user.Status,
            user.UserType
            );
    }
}