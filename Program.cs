using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BodeAppBasico.Data;
using BodeAppBasico.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. REGISTRO DE SERVICIOS EN EL CONTENEDOR DE INYECCIÓN DE DEPENDENCIAS (IoC)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Entity Framework Core con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=bodeapp.db"));

// Registrar el servicio desacoplado (Interfaz -> Implementación)
builder.Services.AddScoped<ITiendaServicio, TiendaServicio>();

// Configurar CORS para permitir peticiones AJAX desde index.html
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 2. INICIALIZACIÓN AUTOMÁTICA DE LA BASE DE DATOS SQLITE
// Garantiza que la base de datos .db y sus tablas se creen automáticamente si no existen
using (var scope = app.Services.CreateScope())
{
    var bd = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    bd.Database.EnsureCreated();
}

// 3. CONFIGURACIÓN DEL PIPELINE DE PETICIONES HTTP (MIDDLEWARE)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirTodo");
app.UseAuthorization();
app.MapControllers();

app.Run();