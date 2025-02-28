using CleanArchitectureTemplate.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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