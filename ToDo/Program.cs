using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using ToDo.Context;
using ToDo.DTO.Mappings;
using ToDo.Extensions;
using ToDo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//database connection
string? Connection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(Connection))
{
    throw new InvalidOperationException("The connection string 'DefaultConnection' was not found.");
}

string mySqlConnection = Connection;
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
builder.Services.AddAutoMapper(typeof(TaskDTOMappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("tasksApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseCors("tasksApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
