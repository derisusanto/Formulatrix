using Serilog;
using OthelloAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // <--- wajib untuk Swagger
builder.Services.AddSwaggerGen();            // <--- Swagger
builder.Services.AddSingleton<GameController>();

// Setup Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()                     // level minimal log
    .WriteTo.Console()                        // tampil di console
    .WriteTo.File("logs/log-.txt",            // log file
                  rollingInterval: RollingInterval.Day,
                  retainedFileCountLimit: 1, // simpan 7 hari
                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();            // <--- aktifkan Swagger
    app.UseSwaggerUI();          // <--- UI Swagger
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
