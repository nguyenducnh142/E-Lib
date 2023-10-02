using Microsoft.EntityFrameworkCore;
using JwtTokenAuthentication;
using HelpService.Model;
using HelpService.Repository;
//using HelpService.Repository;

var builder = WebApplication.CreateBuilder(args);
//gmail
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IHelpRepository, HelpRepository>();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication();
//builder.Services.AddScoped<IStudentRepository, StudentRepository>();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
