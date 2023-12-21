using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;

using LoginToken.Models;
using LoginToken.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();








builder.Services.AddDbContext<UsuDbContext>(opt =>
{

    opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSql"));




});


builder.Services.AddScoped<IAutorizacionService,AutorizazcionServide>();



var key = builder.Configuration.GetValue<String>("JwtSetting:key");

var KeyBytes = Encoding.ASCII.GetBytes(key);


builder.Services.AddAuthentication(config =>
{


    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



}).AddJwtBearer(config =>
{

    config.RequireHttpsMetadata = false;

    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey= true ,
        IssuerSigningKey = new SymmetricSecurityKey(KeyBytes) ,
        ValidateIssuer=false,
        ValidateAudience=false,
        ValidateLifetime=true,
        ClockSkew = TimeSpan.Zero






    };





});



builder.Services.AddCors(option =>
{

    option.AddPolicy("nuevapolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });

});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("nuevapolitica");



app.UseAuthentication();











app.UseAuthorization();

app.MapControllers();

app.Run();





  

