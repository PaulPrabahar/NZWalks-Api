using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Mappings;
using NZWalks.Repository.Interface;
using NZWalks.Repository.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependency Injection
builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
builder.Services.AddScoped<IRegionRepository, NsgpRegionRepository>();
builder.Services.AddScoped<IWalkRepository, NsgpWalkRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
