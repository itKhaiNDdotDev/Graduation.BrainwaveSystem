using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Services.DeviceDataService;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Graduation.BrainwaveSystem.Services.DeviceServices;
using Graduation.BrainwaveSystem.Services.DeviceDataServices;
using Graduation.BrainwaveSystem.Services.Data8BandsEEGServices;
using Graduation.BrainwaveSystem.Services.DataRawEEGServices;
using Graduation.BrainwaveSystem.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Graduation.BrainwaveSystem.APIs.Common;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = "Client",
        ValidIssuer = "Server",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Graduation.BrainwaveSystem"))
    };
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
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDeviceDataService, DeviceDataService>();
builder.Services.AddTransient<IData8BandsEEGService, Data8BandsEEGService>();
builder.Services.AddTransient<IDataRawEEGService, DataRawEEGService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();  
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationhub");

// Aply CORS Midlewares (Author: KhaiND - 21/12/2022)
app.UseCors("myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
