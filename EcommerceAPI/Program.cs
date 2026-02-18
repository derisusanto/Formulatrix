using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FluentValidation;
using System.Text;
using System.Reflection;
using Ecommerce.Data;
using Ecommerce.Model;
using Ecommerce.Services;
using Ecommerce.DTOs.User;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.Category;
using Ecommerce.Validators;
using Ecommerce.Data.Seed;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext ke DI dan gunakan SQLite sebagai database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=ecommerce.db"));


// Konfigurasi ASP.NET Identity dengan custom User dan Role berbasis GUID
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    // Aturan keamanan password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true; // wajib simbol
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Email harus unik
    options.User.RequireUniqueEmail = true;
})
// Simpan data user & role ke database menggunakan EF Core
.AddEntityFrameworkStores<AppDbContext>()
// Provider token untuk reset password, email confirmation, dll
.AddDefaultTokenProviders();


// Ambil konfigurasi JWT dari appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Ambil secret key untuk sign token
var key = Encoding.ASCII.GetBytes(
    jwtSettings["Secret"] ?? "super-secret-key-256bits-min");

// Konfigurasi Authentication menggunakan JWT Bearer
builder.Services.AddAuthentication(options =>
{
    // Default authentication & challenge scheme
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // untuk development
    options.SaveToken = true;

    // Aturan validasi token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // validasi signature
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,   // bisa diaktifkan jika pakai issuer spesifik
        ValidateAudience = false, // bisa diaktifkan jika pakai audience spesifik

        ValidateLifetime = true,  // token expired akan ditolak
        ClockSkew = TimeSpan.Zero // tidak ada toleransi waktu expire
    };
});

// Register AutoMapper untuk mapping DTO <-> Entity
builder.Services.AddAutoMapper(typeof(Program));


// Register semua validator agar bisa di-inject ke controller
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterValidator>();
builder.Services.AddScoped<IValidator<UserLoginDto>, UserLoginValidator>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();


// Register repository untuk akses database
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


// Register business logic service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Enable controller support
builder.Services.AddControllers();

// SWAGGER + JWT
// Aktifkan API Explorer (dibutuhkan oleh Swagger untuk membaca endpoint)
builder.Services.AddEndpointsApiExplorer();

// Konfigurasi Swagger (dokumentasi API otomatis)
builder.Services.AddSwaggerGen(c =>
{
    // Informasi dasar dokumentasi API (judul, versi, deskripsi)
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "API Ecommerce dengan JWT Auth & Validation"
    });

    // Tambahkan konfigurasi JWT supaya bisa input token di Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Masukkan: Bearer {token}", // format pengisian token
        Name = "Authorization", // nama header
        In = ParameterLocation.Header, // dikirim lewat header
        Type = SecuritySchemeType.ApiKey, // tipe autentikasi
        Scheme = "Bearer"
    });

    // Supaya semua endpoint bisa menggunakan JWT di Swagger
    // Jadi setelah klik "Authorize", token otomatis dikirim ke request
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                }
            },
            new List<string>()
        }
    });

    // Jika ada XML comment (///), tampilkan di Swagger
    // Ini membuat dokumentasi endpoint lebih rapi
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// Izinkan semua origin (untuk development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Membuat role default dan admin saat aplikasi pertama kali jalan
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeed.InitializeAsync(services);
}

// MIDDLEWARE PIPELINE

if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI di environment development
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
        c.RoutePrefix = string.Empty; // Swagger di root
    });
}

// Mengizinkan akses file statis (misalnya upload image)
app.UseStaticFiles();

// Aktifkan CORS policy
app.UseCors("AllowAll");

// Authentication harus sebelum Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map semua controller endpoint
app.MapControllers();

app.Run();
