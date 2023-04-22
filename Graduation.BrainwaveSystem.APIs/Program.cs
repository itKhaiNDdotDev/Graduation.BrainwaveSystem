using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Services.Base;
using Graduation.BrainwaveSystem.Services.Device;
using Graduation.BrainwaveSystem.Services.DeviceData;

var builder = WebApplication.CreateBuilder(args);

// Resolve DataContext with ConnectionString
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Resolve Dependency Injection (KhaiND - 22/04/2023)
builder.Services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IDeviceDataService, DeviceDataService>();

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
