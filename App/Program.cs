using Extensoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using ModuloUsuario.Api;
using ModuloUsuario.Infra.Base;
using System.Text;

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


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var secretKey = builder.Configuration["Jwt:Secret"];
    if (string.IsNullOrEmpty(secretKey))
    {
        throw new InvalidOperationException("A chave JWT não foi configurada corretamente.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

        ValidateIssuer = true,  // Habilita a validação do Issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true, // Habilita a validação da Audience
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true, // Garante que tokens expirados não sejam aceitos
        ClockSkew = TimeSpan.Zero // Remove tolerância de tempo para expiração
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
app.UseMiddleware<ExcecaoMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
