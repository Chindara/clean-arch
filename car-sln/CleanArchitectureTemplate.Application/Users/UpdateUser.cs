﻿using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.Users;

public record UpdateUserCommand(long UserId, long Id, string FirstName, string LastName, string Email, string Mobile, int UserType, int Status) : IRequest<Result<User>>;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetById(request.Id);
        if (user is null)
        {
            return Result.Failure<User>(new Error("UpdateUserCommand", "Username not found"));
        }

        Result<User> userResult = user.Update(request.FirstName, request.LastName, request.Mobile, request.Status, request.UserType);
        if (userResult.IsFailure)
        {
            return Result.Failure<User>(new Error("UpdateUserCommand", "Error occurred in update"));
        }

        _userRepository.Update(userResult.Value);
        await _unitOfWork.SaveChangesAsync(request.UserId, cancellationToken);

        return Result.Success<User>(userResult.Value);
    }
}