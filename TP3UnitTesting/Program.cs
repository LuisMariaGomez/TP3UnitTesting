using CDatos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de EF Core (ejemplo con InMemory para pruebas)
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseInMemoryDatabase("ZombiesDB"));

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
