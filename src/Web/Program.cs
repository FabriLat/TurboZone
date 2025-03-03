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
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.Configure<AuthenticationServiceOptions>(
  builder.Configuration.GetSection(AuthenticationServiceOptions.AuthenticationService));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
#endregion


#region Repositories
builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IModeratorRepository,ModeratorRepository>();
builder.Services.AddScoped<IVehicleRepository,VehicleRepository>();
builder.Services.AddScoped<ISysAdminRepository,SysAdminRepository>();
builder.Services.AddScoped<IUserRepository, RepositoryUser>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

#endregion



builder.Services.AddAuthorization(options => //valida que el usuario logueado sea sysadmin
{
    options.AddPolicy("SysAdminOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type.Contains("role") && c.Value == "SysAdmin")));
});


builder.Services.AddAuthorization(options => //valida que el usuario logueado sea moderador o sysadmin
{
    options.AddPolicy("ModeratorAndSysAdmin", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type.Contains("role") && (c.Value == "SysAdmin"  || c.Value == "Moderator"))));
});


builder.Services.AddAuthorization(options => //valida que el usuario logueado sea client
{
    options.AddPolicy("ClientOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type.Contains("role") && c.Value == "Client")));
});



//Habilita el cors para que se pueda usar en el front.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//para poder usar el cors
app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
