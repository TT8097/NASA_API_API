using AutoMapper;
using NASASerwerAPI.MappingProfile;
using NASASerwerAPI.Models;
using NASASerwerAPI.WebServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<Dictionary<string, SatelliteModel>>();
builder.Services.AddTransient<Dictionary<string, string>>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<List<MissionModel>>();
builder.Services.AddSingleton<SateliteService>();
builder.Services.AddSingleton<ApodService>();
builder.Services.AddSingleton<OSDRService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
