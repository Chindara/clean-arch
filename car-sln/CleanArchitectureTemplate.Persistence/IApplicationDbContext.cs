using CleanArchitectureTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Persistence;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; set; }
    DbSet<User> Users { get; set; }
}