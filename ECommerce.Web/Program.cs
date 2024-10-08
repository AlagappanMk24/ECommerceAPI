using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.Mappings;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ECommerceDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            ClockSkew = TimeSpan.Zero   //when token expires it expires immediately
        };
    }
    );

#region Swagger Authentication
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "E-Commerce",

    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter You JWT Key"

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer",

                            },Name="Bearer",
                            In=ParameterLocation.Header

                        },
                        new List<string>()
                    }

                });
});
#endregion

// stripe config
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

//Email config
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
