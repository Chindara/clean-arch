using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.Users;
public record CreateUserCommand(long CompanyId, long UserId, string FirstName, string LastName, string Email, string Mobile, int UserType) : IRequest<Result<User>>;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

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
}