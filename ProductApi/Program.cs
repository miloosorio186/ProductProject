using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexión a base de datos SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=productosDB.db"));

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Habilitar CORS (para permitir conexión desde tu frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()   // Permite cualquier origen (frontend)
            .AllowAnyMethod()   // Permite métodos GET, POST, PUT, DELETE
            .AllowAnyHeader()); // Permite cabeceras personalizadas
});

var app = builder.Build();

// Aplicar CORS
app.UseCors("AllowAll");

// Redirección HTTPS (opcional)
app.UseHttpsRedirection();

//  Swagger (documentación de API)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapear controladores (rutas API)
app.MapControllers();

// ▶Ejecutar aplicación
app.Run();
