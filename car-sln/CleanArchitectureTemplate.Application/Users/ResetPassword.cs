using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Users;

public record ResetPasswordCommand(long UserId, long CompanyId, string Email) : IRequest<Result<User>>;

internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<User>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user is null)
        {
            return Result.Failure<User>(new Error("DeleteUserCommand", "Username not found"));
        }

        Random random = new Random();
        string password = random.Next(111111111, 999999999).ToString();

        var passwordResult = user.ResetPassword(password);

        _userRepository.Update(passwordResult.Value);
        await _unitOfWork.SaveChangesAsync(request.UserId, cancellationToken);

        return Result.Success<User>(passwordResult.Value);
    }
}
