using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieRecommendation.Application.Interfaces.Identity;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Domain.Entities;
using MovieRecommendation.Domain.Models.Responses;
using MovieRecommendation.Domain.Models.Settings;
using MovieRecommendation.Persistence.Context;
using MovieRecommendation.Persistence.Repositories;
using MovieRecommendation.Persistence.Services;
using Newtonsoft.Json;
using System.Text;

namespace MovieRecommendation.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServer")));

            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieVoteRepository, MovieVoteRepository>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmins", policy => policy.RequireRole("SuperAdmin", "Admin"));
            });

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion

            #region Configure
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            #endregion

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
                .AddTokenProvider("MovieRecommendation", typeof(DataProtectorTokenProvider<ApplicationUser>));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero,
                       ValidIssuer = configuration["JWTSettings:Issuer"],
                       ValidAudience = configuration["JWTSettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                   };
                   o.Events = new JwtBearerEvents()
                   {
                       OnAuthenticationFailed = c =>
                       {
                           c.NoResult();
                           c.Response.StatusCode = 500;
                           c.Response.ContentType = "text/plain";
                           return c.Response.WriteAsync(c.Exception.ToString());
                       },
                       OnChallenge = context =>
                       {
                           context.HandleResponse();
                           context.Response.StatusCode = 401;
                           context.Response.ContentType = "application/json";
                           var result = JsonConvert.SerializeObject(new BaseResponse<string>("You are not Authorized"));
                           return context.Response.WriteAsync(result);
                       },
                       OnForbidden = context =>
                       {
                           context.Response.StatusCode = 403;
                           context.Response.ContentType = "application/json";
                           var result = JsonConvert.SerializeObject(new BaseResponse<string>("You are not authorized to access this resource"));
                           return context.Response.WriteAsync(result);
                       },
                   };
               });

            return services;
        }
    }
}
