using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Services;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BdadoptapatasContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// add services
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<FundacionService, FundacionService>();
// add repositories..

builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<FundacionRepository, FundacionRepository>();





// Add Automapper settings
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Ad jwt settings
var bindjwt = new JwtSettingsDto();
builder.Configuration.Bind("Jwt", bindjwt);
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
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindjwt.Key)),
            ValidateIssuerSigningKey = bindjwt.ValidKey,
            ValidIssuer = bindjwt.Issuer,
            ValidateIssuer = bindjwt.ValidIssuer,
            ValidAudience = bindjwt.Audience,
            ValidateAudience = bindjwt.ValidAudience,
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
    options.AddPolicy("AllowSpecificOrigin",  // dejarlo en "AllowAnyOrigin" para cualquier origen
        builder =>
        {
            builder.WithOrigins("https://example.com") // Reemplaza aqui los link de la pagina de adoptapatas
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");   // dejarlo en "AllowAnyOrigin" si se configura en cualquier origen

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
