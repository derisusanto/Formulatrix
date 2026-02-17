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
// using Ecommerce.Repositories; 
using Ecommerce.Repositories;




var builder = WebApplication.CreateBuilder(args);

// =====================
// DATABASE (SQLite)
// =====================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=ecommerce.db"));

// =====================
// IDENTITY (GUID FIX)
// =====================
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    // Aturan password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true; // Supaya ada simbol
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Aturan user
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders(); // Untuk reset password, email confirmation, dsb

// =====================
// JWT AUTHENTICATION
// =====================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? "super-secret-key-256bits-min");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // bisa diubah kalau pakai multi-tenant
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Token expire tepat waktu
    };
});

// =====================
// AUTOMAPPER
// =====================
builder.Services.AddAutoMapper(typeof(Program));


// =====================
// FLUENTVALIDATION
// =====================
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterValidator>();
builder.Services.AddScoped<IValidator<UserLoginDto>, UserLoginValidator>();
builder.Services.AddTransient<IValidator<CreateProductDto>, CreateProductDtoValidator>();

//add repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//add service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();




// =====================
// SERVICES
// =====================
builder.Services.AddScoped<IAuthService, AuthService>();

// =====================
// CONTROLLERS
// =====================
builder.Services.AddControllers();

// =====================
// SWAGGER + JWT
// =====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "API Ecommerce dengan JWT Auth & Validation"
    });

    // JWT Authorization di Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header menggunakan Bearer scheme. Masukkan 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new List<string>()
        }
    });

    // XML comment
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// =====================
// CORS
// =====================
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

// =====================
// SEED DATA (Role & Admin)
// =====================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeed.InitializeAsync(services);
}

// =====================
// MIDDLEWARE
// =====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
        c.RoutePrefix = string.Empty; // Swagger UI di root
    });
}
// akses image
app.UseStaticFiles();


app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication(); // JWT auth harus sebelum Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
