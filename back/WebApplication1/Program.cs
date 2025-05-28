using Microsoft.OpenApi.Models;
using BookReviewAPI.Data;
using BookReviewAPI;
using Microsoft.EntityFrameworkCore;
using BookReviewAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();

// Настройка CORS - объединяем обе политики здесь
builder.Services.AddCors(options =>
{
    // Политика для Blazor фронтенда
    options.AddPolicy("AllowBlazorFrontend",
        builder => builder
            .WithOrigins("https://localhost:7294", "http://localhost:5207")
            .AllowAnyMethod()
            .AllowAnyHeader());

    // Политика "AllowAll" (если она действительно нужна)
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "BooksReviewAPI",
        Version = "v2"
    }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "BooksReviewAPI v2");
});

app.UseHttpsRedirection();
app.UseRouting();

// Используем нужную политику CORS
app.UseCors("AllowBlazorFrontend"); // Или "AllowAll", если нужна более открытая политика

app.UseAuthorization();

app.MapGet("/book/bycategory/{category}", async (string category, IBookService bookService, HttpContext context) =>
{
    var books = await bookService.GetBooksByCategoryAsync(category);
    if (books == null)
    {
        context.Response.StatusCode = 404;
        return Results.NotFound();
    }
    return Results.Json(books);
});
app.MapControllers();

app.Run();