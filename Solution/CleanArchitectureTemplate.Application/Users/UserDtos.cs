namespace CleanArchitectureTemplate.Application.Users;
public record UsersResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Mobile,
    string Status,
    string UserType,
    DateTime Created,
    DateTime Modified
);

public record UserResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Mobile,
    int Status,
    int UserType
);