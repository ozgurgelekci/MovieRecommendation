using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieRecommendation.Application;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Identity;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Domain.Entities;
using MovieRecommendation.Infrastructure;
using MovieRecommendation.Persistence;
using MovieRecommendation.Persistence.Context;
using MovieRecommendation.Persistence.Seeds;
using MovieRecommendation.WebAPI.Services;
using Hangfire;
using Hangfire.SqlServer;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Services Dependency Inversion

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

#endregion

#region Hangfire       

builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("SQLServer")));
builder.Services.AddHangfireServer();

#endregion

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(options =>
    {
        options.DocumentTitle = "Movie Recommendation API";
        options.SpecUrl = "/swagger/v1/swagger.json";
    });
}

using (var scope = app.Services.CreateScope())
{
    #region Migrate

    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();

    #endregion

    #region User and Role Seeds

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    if (!roleManager.Roles.Any())
    {
        await DefaultRoles.SeedAsync(roleManager);
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    if (!userManager.Users.Any())
    {
        await DefaultSuperAdmin.SeedAsync(userManager);
    }

    #endregion

    #region Movie Seeds

    var movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

    if (!movieRepository.GetAsync().Result.Any())
    {
        var cacheManager = scope.ServiceProvider.GetRequiredService<ICacheManager>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        await SeedMovies.SeedAsync(movieRepository, cacheManager, mapper);
    }

    #endregion
}

#region Cors

app.UseCors(x => x
.SetIsOriginAllowed(origin => true)
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

#endregion

app.UseHangfireServer(new BackgroundJobServerOptions());

GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
