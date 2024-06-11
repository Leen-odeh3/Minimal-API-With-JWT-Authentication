using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Configurations;
using MinimalAPIWithJWTAuthentication.Api.Services;
using MinimalAPIWithJWTAuthentication.Api.DBContext;
using MinimalAPIWithJWTAuthentication.Api.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IUserRepo, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.Configure<JwtAuthenticationConfig>(builder.Configuration.GetSection("JWTToken"));

var jwtConfig = builder.Configuration.GetSection("JwtAuthenticationConfig").Get<JwtAuthenticationConfig>() ?? throw new ArgumentNullException("JWTToken:Wrong Configuration");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.SecretKey));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = key
        };
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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