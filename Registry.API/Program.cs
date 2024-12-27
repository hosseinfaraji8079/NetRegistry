using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Registry.API.Data;
using Registry.API.Enums;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Hubs;
using Registry.API.Repositories.implementations;
using Registry.API.Repositories.Interfaces;
using Registry.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description =
            "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJMbGciMiJIUzI1NiIsMnR5cCI6IkpXVCJ9\"",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<RegistryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddOpenApi();

builder.Services.AddScoped<ExceptionFilter>();

builder.Services.AddCors(option =>
{
    // option.DefaultPolicyName = "NetRegistryCors";
    // option.AddDefaultPolicy(configure =>
    // {
    //     configure.AllowAnyHeader();
    //     configure.AllowAnyMethod();
    //     configure.AllowAnyOrigin();
    // });
    
    option.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true);
    });
}); 

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                string? accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                
                if (string.IsNullOrEmpty(accessToken))
                {
                    accessToken = context.Request.Query["access_token"];
                }

                if (accessToken != null)
                {
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = context.HttpContext.RequestServices
                            .GetRequiredService<IConfiguration>()["JWT:ValidIssuer"],
                        ValidAudience = context.HttpContext.RequestServices
                            .GetRequiredService<IConfiguration>()["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            context.HttpContext.RequestServices.GetRequiredService<IConfiguration>()["JWT:SecurityKey"]))
                    };


                    var handler = new JwtSecurityTokenHandler();
                    var principal = handler.ValidateToken(accessToken, tokenValidationParameters, out _);
                    context.HttpContext.User = principal;
                    context.Token = accessToken;
                }
                
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:ValidIssuer"),
            ValidAudience = builder.Configuration.GetValue<string>("JWT:ValidAudience"),
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:SecurityKey")))
        };
    });


builder.Services.AddTransient(typeof(IAsyncRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IRegistryRepository, RegistryRepository>();
builder.Services.AddScoped<IRegistryService, RegistryService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//
// }

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// app.UseCors("NetRegistryCors");
app.UseCors("CorsPolicy");

app.MapHub<UsersHubs>("/usersHubs");
app.UseHttpsRedirection();

app.MigrateDatabase<RegistryDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<RegistryDbContext>>();
}).Run();
