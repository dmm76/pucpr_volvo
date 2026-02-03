using TechStore.Api.Auth;
using TechStore.Api.Middleware;
using TechStore.Core.Interfaces;
using TechStore.Core.Services;
using TechStore.Infra.Fake.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Controller Auth
builder.Services.AddSingleton<AuthState>();
builder.Services.AddSingleton<AdminRepositoryFake>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI (Fake Infra)
builder.Services.AddSingleton<ICategoriaRepository, CategoriaRepositoryFake>();
builder.Services.AddSingleton<CategoriaService>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

// Mapeia controllers
app.MapControllers();

app.Run();
