using CleanArchitectureTemplate.Domain.Primitives;
using CleanArchitectureTemplate.Domain.Shared;
using System.Text.RegularExpressions;

namespace CleanArchitectureTemplate.Domain.Entities;

public sealed class User : BaseEntity, IAuditableEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Mobile { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public int Status { get; private set; }
    public int UserType { get; private set; }
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
    public DateTime Modified { get; set; }
    public long ModifiedBy { get; set; }
    public string FullName => FirstName + " " + LastName;

    private User() : base(default)
    {
        // Parameterless constructor for EF Core
    }

    private User(long companyId, string firstName, string lastName, string email, string mobile, int userType, string passwordHash) : base(companyId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Mobile = mobile;
        UserType = userType;
        PasswordHash = passwordHash;
        Status = (int)UserStatus.NewUser;
    }

    public static Result<User> Create(long companyId, string firstName, string lastName, string email, string mobile, int userType, string password)
    {
        if (String.IsNullOrEmpty(firstName))
        {
            return Result.Failure<User>(new Error("User.Create", "First name is required"));
        }
        if (String.IsNullOrEmpty(lastName))
        {
            return Result.Failure<User>(new Error("User.Create", "Last name is required"));
        }
        if (String.IsNullOrEmpty(email) || !IsValidEmail(email))
        {
            return Result.Failure<User>(new Error("User.Create", "Email must be valid email"));
        }

        string hash = BCrypt.Net.BCrypt.HashPassword(password);

        var record = new User(companyId, firstName, lastName, email, mobile, userType, hash);
        return Result.Success<User>(record);
    }

    public Result<User> Update(string firstName, string lastName, string mobile, int status, int userType)
    {
        if (!String.IsNullOrEmpty(firstName))
        {
            FirstName = firstName;
        }
        if (!String.IsNullOrEmpty(lastName))
        {
            LastName = lastName;
        }
        if (!String.IsNullOrEmpty(mobile))
        {
            Mobile = mobile;
        }
        Status = status;
        UserType = userType;
        return Result.Success<User>(this);
    }

    public Result<User> Delete()
    {
        if (Status == (int)UserStatus.Deleted)
        {
            return Result.Failure<User>(new Error("User.Delete", " User has already been deactivated"));
        }
        Status = (int)UserStatus.Deleted;
        return Result.Success<User>(this);
    }

    public Result<User> ResetPassword(string tempPassword)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
        PasswordHash = hash;
        Mobile = tempPassword;
        Status = (int)UserStatus.PasswordReset;
        return Result.Success<User>(this);
    }

    public static bool IsValidEmail(string email)
    {
        // Define a regular expression pattern for email validation
        string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        // Use Regex.IsMatch to check if the email matches the pattern
        return Regex.IsMatch(email, pattern);
    }
}