using Microsoft.EntityFrameworkCore;
using TechStore.Api.Auth;
using TechStore.Api.Middleware;
using TechStore.Api.Security;
using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Core.useCases.categorias;
using TechStore.Core.UseCases.Clientes;
using TechStore.Core.UseCases.Pedidos;
using TechStore.Core.UseCases.Produtos;
using TechStore.Infra.Context;
using TechStore.Infra.Sql.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AuthState>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddDbContext<TechStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepositorySql>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepositorySql>();
builder.Services.AddScoped<IClienteRepository, ClienteRepositorySql>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepositorySql>();
builder.Services.AddScoped<IUserRepository, UserRepositorySql>();

builder.Services.AddScoped<CategoriaUseCases>();
builder.Services.AddScoped<ProdutoUseCases>();
builder.Services.AddScoped<PedidoUseCases>();
builder.Services.AddScoped<ClienteUseCases>();
builder.Services.AddScoped<CheckoutUseCases>();

var app = builder.Build();

// migrate sempre
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TechStoreDbContext>();
    db.Database.Migrate();

    if (app.Environment.IsDevelopment())
    {
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        if (!db.Users.Any(u => u.Login == "admin" || u.Email == "admin@techstore.com"))
        {
            db.Users.Add(
                new User(
                    login: "admin",
                    email: "admin@techstore.com",
                    senhaHash: hasher.Hash("Admin@123"),
                    role: UserRole.Admin
                )
            );
            db.SaveChanges();
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.Run();
