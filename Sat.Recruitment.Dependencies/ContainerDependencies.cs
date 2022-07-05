using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Services;

namespace Sat.Recruitment.Dependencies
{
    public class ContainerDependencies
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();            
        }
    }
}
