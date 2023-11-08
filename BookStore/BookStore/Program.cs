using BookStore.Services;
using BookStore.Services.Seeder;
using BookStore.Services.Interfaces;
using BookStore.Entities;
using BookStore.Mapper;
using NLog.Web;
using BookStore.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register controllers
builder.Services.AddDbContext<BookStoreDbContext>();
builder.Services.AddScoped<IWeatherForecast, WeatherForecastService>();
builder.Services.AddScoped<IDataSeeder<BookStore.Entities.BookStore>, BookStoreSeeder>();
builder.Services.AddScoped<IBookStoreService, BookStoreService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BookStoreMappingProfile)); // New version of autoMapper conf

builder.Logging.ClearProviders();
builder.WebHost.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API");
});

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var seeder = serviceProvider.GetRequiredService<IDataSeeder<BookStore.Entities.BookStore>>();
    seeder.Seed();
}

app.Run();
