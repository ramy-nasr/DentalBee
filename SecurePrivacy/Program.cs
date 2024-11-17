using Application;
using Application.Commands;
using Infrastructure;
using Infrastructure.Settings;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);


// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Infrastructure
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddInfrastructure(configuration);

// Add Application
builder.Services.AddApplication();

// Add command handlers, services, etc.
builder.Services.AddTransient<RegisterUserCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Use CORS middleware
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
