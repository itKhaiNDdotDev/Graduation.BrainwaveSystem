using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Services.DeviceDataService;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Graduation.BrainwaveSystem.Services.DeviceServices;
using Graduation.BrainwaveSystem.Services.DeviceDataServices;
using Graduation.BrainwaveSystem.Services.Data8BandsEEGServices;
using Graduation.BrainwaveSystem.Services.DataRawEEGServices;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Policy (KhaiND - 01/05/2023)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Resolve DataContext with ConnectionString
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")).EnableSensitiveDataLogging());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Resolve Dependency Injection (KhaiND - 22/04/2023)
builder.Services.AddTransient(typeof(IBaseService<,>), typeof(BaseService<,>));
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IDeviceDataService, DeviceDataService>();
builder.Services.AddTransient<IData8BandsEEGService, Data8BandsEEGService>();
builder.Services.AddTransient<IDataRawEEGService, DataRawEEGService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aply CORS Midlewares (Author: KhaiND - 21/12/2022)
app.UseCors("myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
