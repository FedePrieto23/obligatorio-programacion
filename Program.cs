using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Repository;
using Obligatorio_Programacion.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddScoped<AsignacionRepository>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<CompraRepository>();
builder.Services.AddScoped<GastoRepository>();
builder.Services.AddScoped<MaterialRepository>();
builder.Services.AddScoped<MaterialObraRepository>();
builder.Services.AddScoped<ObraRepository>();
builder.Services.AddScoped<OficioRepository>();
builder.Services.AddScoped<ProveedorRepository>();
builder.Services.AddScoped<TareaRepository>();
builder.Services.AddScoped<TipoGastoRepository>();
builder.Services.AddScoped<UsuarioRepository>();

builder.Services.AddScoped<AsignacionService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CompraService>();
builder.Services.AddScoped<GastoService>();
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<MaterialObraService>();
builder.Services.AddScoped<ObraService>();
builder.Services.AddScoped<OficioService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<TareaService>();
builder.Services.AddScoped<TipoGastoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuditoriaService>();
builder.Services.AddScoped<AlertaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
