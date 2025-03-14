using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using FluentValidation;
using MediatR;

namespace CleanArchitectureTemplate.Application.Users;

public record UpdateUserCommand(long UserId, long Id, string FirstName, string LastName, string Email, string Mobile, int UserType, int Status) : IRequest<Result<User>>;

internal class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

    internal sealed class UpdateUserCommandHandlerValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandHandlerValidator()
        {
            RuleFor(x => x.UserId).NotEqual(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} required.").EmailAddress().WithMessage("{PropertyName} is invalid.");
            RuleFor(x => x.Mobile).NotEmpty().WithMessage("{PropertyName} required.");
            RuleFor(x => x.UserType).NotEqual(0).WithMessage("{PropertyName} required.");
        }
    }
}