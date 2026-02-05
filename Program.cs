using TechStore.Api.Auth;
using TechStore.Api.Middleware;
using TechStore.Api.Security;
using TechStore.Core.Interfaces;
using TechStore.Core.useCases.categorias;
using TechStore.Core.UseCases.Clientes;
using TechStore.Core.UseCases.Pedidos;
using TechStore.Core.UseCases.Produtos;
using TechStore.Infra.Fake.Repositories;
using TechStore.Infra.Fake.Seed;

var builder = WebApplication.CreateBuilder(args);

//Controller Auth
builder.Services.AddSingleton<AuthState>();

// builder.Services.AddSingleton<UserRepositoryFake>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI (Fake Infra)
builder.Services.AddSingleton<ICategoriaRepository, CategoriaRepositoryFake>();
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepositoryFake>();
builder.Services.AddSingleton<IClienteRepository, ClienteRepositoryFake>();
builder.Services.AddSingleton<IPedidoRepository, PedidoRepositoryFake>();
builder.Services.AddSingleton<IUserRepository, UserRepositoryFake>();
builder.Services.AddSingleton<CategoriaUseCases>();

builder.Services.AddSingleton<ProdutoUseCases>();

builder.Services.AddSingleton<PedidoUseCases>();

builder.Services.AddSingleton<ClienteUseCases>();

builder.Services.AddSingleton<CheckoutUseCases>();

var app = builder.Build();

// SEED (Fake)
using (var scope = app.Services.CreateScope())
{
    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    var clienteRepo = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    FakeSeedClientes.Seed(userRepo, clienteRepo, hasher);
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
