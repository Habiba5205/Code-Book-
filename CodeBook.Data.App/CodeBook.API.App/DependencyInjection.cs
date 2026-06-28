using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Business.App.Validator;
using CodeBook.Business.App.Methods;
using CodeBook.Business.App.Services;
using CodeBook.Data.App.IRepositories;
using CodeBook.Data.App.Repositories;
using FluentValidation;

namespace CodeBook.API.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IuserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AbstractValidator<LoginDto>, LoginValidator>();
            services.AddScoped<AbstractValidator<RegisterDto>, RegisterValidator>();

            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<ICommunityRepository, CommunityRepository>();

            services.AddScoped<IModerationService, ModerationService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            return services;
        }
    }
}
