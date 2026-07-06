using Microsoft.EntityFrameworkCore;
using Obligatorio_Programacion.Data;
using Obligatorio_Programacion.Entity;
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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("connectionString")));

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


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    if (!db.Oficios.Any())
    {
        db.Oficios.AddRange(
            new Oficio
            {
                NombreOficio = "AlbaÃ±il",
                descripcionOficio = "Trabajos generales de construcciÃ³n"
            },
            new Oficio
            {
                NombreOficio = "Electricista",
                descripcionOficio = "Instalaciones y reparaciones elÃ©ctricas"
            },
            new Oficio
            {
                NombreOficio = "Sanitario",
                descripcionOficio = "Trabajos de caÃ±erÃ­a, baÃ±os y sanitaria"
            },
            new Oficio
            {
                NombreOficio = "Pintor",
                descripcionOficio = "Pintura interior y exterior"
            },
            new Oficio
            {
                NombreOficio = "Carpintero",
                descripcionOficio = "Trabajos en madera y estructuras"
            }
        );

        db.SaveChanges();
    }
}

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
