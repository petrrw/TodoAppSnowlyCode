using Microsoft.EntityFrameworkCore;
using TodoAppSnowlyCode.AutoMapping;
using TodoAppSnowlyCode.Business.Interfaces;
using TodoAppSnowlyCode.Business.Services;
using TodoAppSnowlyCode.Business.Validations;
using TodoAppSnowlyCode.Data.DbSetup;
using TodoAppSnowlyCode.Data.Interfaces;
using TodoAppSnowlyCode.Data.Repositories;
using TodoAppSnowlyCode.Middlewares;
using TodoAppSnowlyCode.Extensions;
using FluentValidation;
using TodoAppSnowlyCode.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// This could also be implemented in an extension methods..
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddScoped<IValidator<ToDoItem>, ToDoItemValidator>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ToDoItemProfile>();
});

// Just for test purposes..
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Applies DB migrations
app.UpdateDatabase();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.

// Swagger enabled in all environments for simplicity
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseAuthorization();

app.MapControllers();

app.Run();


// So IT tests can work properly..
public partial class Program { }
