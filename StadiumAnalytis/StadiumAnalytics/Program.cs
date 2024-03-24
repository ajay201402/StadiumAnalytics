using Microsoft.EntityFrameworkCore;
using StadiumAnalytics;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<ISensorDbContext, SensorDbContext>();

builder.Services.AddDbContext<SensorDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SensorDbConnection")));

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
