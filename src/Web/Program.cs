using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CONFIGURACION DE ENTITY FRAMEWORK PARA PERSISTENCIA DE DATOS
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
    builder.Configuration["ConnectionStrings:DBConnectionString"]));





#region Services
builder.Services.AddScoped<IClientService,ClientService>();
builder.Services.AddScoped<IModeratorService,ModeratorService>();
#endregion


#region Repositories
builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IModeratorRepository,ModeratorRepository>();
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
