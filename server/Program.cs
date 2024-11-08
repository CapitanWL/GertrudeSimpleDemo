using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using server.Intarfaces;
using server.Models;
using server.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gertrude API",
        Version = "v1",
        Description = "Gertrude + Swagger",
        Contact = new OpenApiContact { Name = "Capitan", Email = "mail@mail.com" },
        License = new OpenApiLicense { Name = "GPL 3", Url = new Uri("https://opensource.org/license/gpl-3-0") }
    }
    );

    // Подключение XML-документации
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Default connection"));

builder.Services.AddScoped<IUserService, UserServise>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gertrude API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
