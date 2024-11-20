using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static Infrastructure.Services.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    //Esto va a permitir usar swagger con el token.
    setupAction.AddSecurityDefinition("API-TurboZoneBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Pegar el token generado"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "API-TurboZoneBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definición
                }, new List<string>() }
    });
});




//CONFIGURACION DE ENTITY FRAMEWORK PARA PERSISTENCIA DE DATOS
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
    builder.Configuration["ConnectionStrings:DBConnectionString"]));


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationService:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationService:SecretForKey"]))
        };
    });



#region Services
builder.Services.AddScoped<IClientService,ClientService>();
builder.Services.AddScoped<IModeratorService,ModeratorService>();
builder.Services.AddScoped<IVehicleService,VehicleService>();
builder.Services.AddScoped<ISysAdminService,SysAdminService>();

//builder.Services.Configure<AuthenticationServiceOptions>(
//    builder.Configuration.GetSection(AuthenticationServiceOptions.AuthenticationService));
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
#endregion


#region Repositories
builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IModeratorRepository,ModeratorRepository>();
builder.Services.AddScoped<IVehicleRepository,VehicleRepository>();
builder.Services.AddScoped<ISysAdminRepository,SysAdminRepository>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
