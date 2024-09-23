using DriveApp;
using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using DriveApp.Services;
using DriveApp.Services.Interfaces;
using DriveApp.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITripServices,TripServices>();
builder.Services.AddScoped<IAccountServices,AccountServices>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IRatingServices, RatingServices>();
builder.Services.Configure<MailSetting>(config.GetSection("MailSettings"));
builder.Services.AddTransient<IMailServices,MailServices>();
builder.Services.AddIdentity<UserApplication, RoleApplication>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(
    options =>
   {
       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
   }
    ).AddJwtBearer(options=>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = config["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(config["JWT:SecretKey"]!))
        };
    });
builder.Services.AddMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
