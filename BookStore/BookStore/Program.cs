using BookStore.Services;
using BookStore.Services.Seeder;
using BookStore.Services.Interfaces;
using BookStore.Entities;
using BookStore.Mapper;
using BookStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register controllers
builder.Services.AddDbContext<BookStoreDbContext>();
builder.Services.AddScoped<IWeatherForecast, WeatherForecastService>();
builder.Services.AddScoped<IDataSeeder<BookStore.Entities.BookStore>, BookStoreSeeder>();
builder.Services.AddScoped<IDataService<BookStoreDto>, BookStoreService>();
builder.Services.AddAutoMapper(typeof(BookStoreMappingProfile)); // New version of autoMapper conf

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var seeder = serviceProvider.GetRequiredService<IDataSeeder<BookStore.Entities.BookStore>>();
    seeder.Seed();
}

app.Run();
