using ModuloUsuario.Api;
using ModuloUsuario.Infra.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UsuarioController).Assembly);
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

builder.Services.AddModuloUsuario(app.Configuration);
app.ExecutaMigrationsModuloUsuario();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
