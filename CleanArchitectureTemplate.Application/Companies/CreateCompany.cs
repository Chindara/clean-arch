using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Domain.Abstractions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Shared;
using MediatR;

namespace CleanArchitectureTemplate.Application.Companies;

public record CreateCompanyCommand(
    long UserId,
    string Name,
    string Address1,
    string Address2,
    string Address3,
    string PostalCode,
    string Country,
    string Telephone,
    string Email,
    string Website) : IRequest<Result<Company>>;

internal class CreateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCompanyCommand, Result<Company>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Company>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        Result<Company> companyResult = Company.Create(request.Name, request.Address1, request.Address2, request.Address3, request.PostalCode, request.Country, request.Telephone, request.Email, request.Website);

        _companyRepository.Add(companyResult.Value);
        await _unitOfWork.SaveChangesAsync(request.UserId, cancellationToken);

        return Result.Success<Company>(companyResult.Value);
    }
}