using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TestRestAPI.Data;
using TestRestAPI.Models;
using TestRestAPI.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(option =>
{
    option.CreateMap<Student,StudentCreateDTO>().ReverseMap();
    option.CreateMap<Student,StudentUpdateDTO>().ReverseMap();
    option.CreateMap<Student, StudentDTO>().ReverseMap();
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


