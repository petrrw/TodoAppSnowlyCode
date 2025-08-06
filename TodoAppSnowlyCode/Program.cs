using Microsoft.EntityFrameworkCore;
using TodoAppSnowlyCode.AutoMapping;
using TodoAppSnowlyCode.Business.Interfaces;
using TodoAppSnowlyCode.Business.Services;
using TodoAppSnowlyCode.Business.Validations;
using TodoAppSnowlyCode.Data.DbSetup;
using TodoAppSnowlyCode.Data.Interfaces;
using TodoAppSnowlyCode.Data.Repositories;
using TodoAppSnowlyCode.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddScoped<ToDoItemValidator>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ToDoItemProfile>();
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (dbContext.Database.GetPendingMigrations().Any())
        dbContext.Database.Migrate();
}


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

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


// So IT tests can work properly..
public partial class Program { }
