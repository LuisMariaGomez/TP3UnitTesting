using CDatos.Data;
using CDatos.Repositorio;
using CDatos.Repositorio.IRepositorio;
using CNegocio.Logica;
using CNegocio.Logica.ILogica;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// In the Program.cs file, add the following line to configure the HttpClient service
builder.Services.AddHttpClient();

builder.Services.AddScoped<IZombieLogica, ZombieLogica>();
builder.Services.AddScoped<IZombieRepositorio, ZombieRepositorio>();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


// Servicios API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Zombies v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
