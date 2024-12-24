using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Registry.API.Data;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Repositories.implementations;
using Registry.API.Repositories.Interfaces;
using Registry.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RegistryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddOpenApi();

builder.Services.AddScoped<ExceptionFilter>();



builder.Services.AddCors(option =>
{
    option.DefaultPolicyName = "NetRegistryCors";
    option.AddDefaultPolicy(configure =>
    {
        configure.AllowAnyHeader();
        configure.AllowAnyMethod();
        configure.AllowAnyOrigin();
    });
});

builder.Services.AddTransient(typeof(IAsyncRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IRegistryRepository, RegistryRepository>();
builder.Services.AddScoped<IRegistryService, RegistryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

app.MapControllers();
app.UseCors("NetRegistryCors");
app.MigrateDatabase<RegistryDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<RegistryDbContext>>();
}).Run();
