﻿// Ignore Spelling: Validator

using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using FluentValidation;
using MediatR;

namespace CleanArchitectureTemplate.Application.Users;
public record CreateUserCommand(long CompanyId, long UserId, string FirstName, string LastName, string Email, string Mobile, int UserType) : IRequest<Result<User>>;

internal class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User existingUser = await _userRepository.GetByEmail(request.Email);
        if (existingUser is not null)
        {
            return Result.Failure<User>(new Error("CreateUserCommand", "Username already exists"));
        }

        Random random = new Random();
        string password = random.Next(111111111, 999999999).ToString();

        Result<User> newUserResult = User.Create(request.CompanyId, request.FirstName, request.LastName, request.Email, request.Mobile, request.UserType, password);
        if (newUserResult.IsFailure)
        {
            return Result.Failure<User>(newUserResult.Error);
        }

        _userRepository.Add(newUserResult.Value);
        await _unitOfWork.SaveChangesAsync(request.UserId, cancellationToken);

        return Result.Success<User>(newUserResult.Value);
    }

    internal sealed class CreateUserCommandHandlerValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandHandlerValidator()
        {
            RuleFor(x => x.CompanyId).NotEqual(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.UserId).NotEqual(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} required.").EmailAddress().WithMessage("{PropertyName} is invalid.");
            RuleFor(x => x.Mobile).NotEmpty().WithMessage("{PropertyName} required.");
            RuleFor(x => x.UserType).NotEqual(0).WithMessage("{PropertyName} required.");
        }
    }
}