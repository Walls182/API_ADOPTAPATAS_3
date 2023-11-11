using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Services;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BdadoptapatasContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// add services
builder.Services.AddScoped<UserService, UserService>();

builder.Services.AddScoped<FundacionService, FundacionService>();
builder.Services.AddScoped<ModeradorService, ModeradorService>();
builder.Services.AddScoped<Encrip, Encrip>();
builder.Services.AddScoped<GenericPass, GenericPass>();

// add repositories..
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<FundacionRepository, FundacionRepository>();
builder.Services.AddScoped<ModeradorRepository, ModeradorRepository>();

// Add Automapper settings
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Ad jwt settings
var bindjwt = new JwtSettingsDto();
builder.Configuration.Bind("JwtKeys",bindjwt);    
builder.Services.AddSingleton(bindjwt);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = bindjwt.ValidKey,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindjwt.Key)),
            ValidateIssuer = bindjwt.ValidateIssuer,
            ValidIssuer = bindjwt.Issuer,
            ValidateAudience = bindjwt.ValidateAudience,
            ValidAudience = bindjwt.Audience,
            RequireExpirationTime = bindjwt.RequireExpirationTime,
            ValidateLifetime = bindjwt.RequireExpirationTime,
            ClockSkew = TimeSpan.Zero,


        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired-Time", "True");
                }
                return Task.CompletedTask;
            }
        };
    }
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",  // dejarlo en "AllowAnyOrigin" para cualquier origen y en --"AllowSpecificOrigin" para el link de adoptapatas
        builder =>
        {
            builder.WithOrigins("AllowSpecificOrigin") // Reemplazo aqui los link de la pagina de adoptapatas
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter Bearer [Space] and then your valid token in the text",
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
    {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Reference = new Microsoft.OpenApi.Models.OpenApiReference
            {
                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
        }
    });
    options.SwaggerDoc("v3", new OpenApiInfo
    {
        Version = "v3",
        Title = "API_ADOPTAPATAS_3:" + builder.Configuration.GetValue<string>("AplicationInsights:Envieroment"),
        Description = "Adoptapatas_Api con autenticacion y tokenizado. Servicio por capas en la version 3 ",
        Contact = new OpenApiContact
        {
            Name = "Adoptapatas",
            Email = "wasc0144@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "UDEC",
            Url = new Uri("https://www.ucundinamarca.edu.co/")
        }
    });
});


var app = builder.Build();



app.UseCors("AllowSpecificOrigin");   // dejarlo en "AllowAnyOrigin" si se configura en cualquier origen -----"AllowSpecificOrigin" para el link de adoptapatas
app.UseSwagger();
app.UseSwaggerUI(u =>
{
    u.SwaggerEndpoint("/swagger/v3/swagger.json", "API_ADOPTAPATAS_3");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
