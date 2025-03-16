using Extensoes;
using Microsoft.AspNetCore.Mvc;
using ModuloUsuario.Api;
using ModuloUsuario.Infra.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(UsuarioController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Retorna os erros no padrão da ResponseModel
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var erros = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        var response = new PadraoRespostasApi<object>
        {
            Dados = erros,
            Mensagem = "Erro de validação",
            HttpStatusCode = System.Net.HttpStatusCode.BadRequest
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddModuloUsuario(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ExecutaMigrationsModuloUsuario();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
