using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailSettings"));

        services.AddScoped<IEmailSender, SmtpEmailSender>();

        return services;
    }
}