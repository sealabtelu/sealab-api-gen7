using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SealabAPI.Base;
using SealabAPI.DataAccess;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Seeders;
using SealabAPI.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverterHelper());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var webUrl = builder.Configuration.GetValue<string>("WebUrl");
var secretKey = builder.Configuration.GetValue<string>("SecretKey");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseSnakeCaseNamingConvention();
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "I-Smile Laboratory API",
        Description = "API untuk website praktikum",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins($"https://{webUrl}", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireRole("Assistant")
    .Build();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AbstractModelValidator<BaseModel>>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();

//builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddSingleton<IMailService, MailService>();

Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t =>
        !t.IsInterface &&
        t != typeof(BaseService<>) &&
        t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<>))
    )
    .ToList().ForEach(assignedTypes =>
    {
        var serviceType = assignedTypes.GetInterfaces().First(i => !i.IsGenericType);
        builder.Services.AddScoped(serviceType, assignedTypes);
    });

builder.Services.AddScoped<SeelabsPracticumService>();
builder.Services.AddScoped<SeelabsProctorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseAuthentication();

app.UseCors();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = string.Empty;
    });
    app.UseHttpsRedirection();
    app.MapControllers();
}
else
{
    app.MapControllers().RequireAuthorization();
}

using (var scope = app.Services.CreateScope())
{
    AppDbContext appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await appContext.Database.MigrateAsync();

    await Seeder.Seed<Assistant, AssistantSeed>(appContext);
}

app.Run();
