using AuthenticationDuda.Helpers;
using AuthenticationDuda.Interfaces.Repositries;
using AuthenticationDuda.Interfaces.Workers;
using AuthenticationDuda.Repositries;
using AuthenticationDuda.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Workers
builder.Services.AddSingleton<IAuthenticationWorker, AuthenticationWorker>();

//Repositories
builder.Services.AddSingleton<IUserRepository, UserRepository>();

//Helpers
builder.Services.AddSingleton<JwtManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
