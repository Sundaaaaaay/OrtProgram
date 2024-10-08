using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using OrtProgram.Server.Data;
using OrtProgram.Server.Interfaces.Repositories;
using OrtProgram.Server.Interfaces.Services;
using OrtProgram.Server.Repositories;
using OrtProgram.Server.Services;
using ILogger = NLog.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddSingleton<ILogger>(serviceProvider =>
    LogManager.GetCurrentClassLogger());

builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestService, TestService>();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAllOrigins",
//         policy =>
//         {
//             policy.AllowAnyOrigin() // Разрешить запросы с любого домена
//                 .AllowAnyHeader()  // Разрешить любые заголовки
//                 .AllowAnyMethod(); // Разрешить любые методы (GET, POST и т.д.)
//         });
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") // Разрешить запросы с этого домена
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();