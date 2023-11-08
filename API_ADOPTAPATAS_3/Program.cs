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
    options.AddPolicy("AllowAnyOrigin",  // dejarlo en "AllowAnyOrigin" para cualquier origen
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Reemplaza aqui los link de la pagina de adoptapatas
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Description",
    });
    o.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement{
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
        Reference = new Microsoft.OpenApi.Models.OpenApiReference{
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    },
    new string[] { }


    }
    });
    
}
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAnyOrigin");   // dejarlo en "AllowAnyOrigin" si se configura en cualquier origen

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
