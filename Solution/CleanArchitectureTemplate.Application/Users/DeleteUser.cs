using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.Users;

public record DeleteUserCommand(long UserId, long CompanyId, long Id) : IRequest<Result<User>>;

internal class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetById(request.Id);
        if (user is null)
        {
            return Result.Failure<User>(new Error("DeleteUserCommand", "Username not found"));
        }
        Result<User> userResult = user.Delete();
        if (userResult.IsFailure)
        {
            return Result.Failure<User>(userResult.Error);
        }

        _userRepository.Update(userResult.Value);
        await _unitOfWork.SaveChangesAsync(request.UserId, cancellationToken);

        return Result.Success<User>(userResult.Value);
    }
}