using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//Dependency Injection
builder.Services.AddSingleton<ICategory, CategoryDal>();

//Dependency Injection Instructor
builder.Services.AddSingleton<IInstructor, InstructorDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/coba", (string? id) =>
{
    return $"Hello, {id}!";
});

app.MapGet("/coba/{nama}", (string nama) => $"Hello, {nama}!");

app.MapGet("/coba/luas-segitiga", (double alas, double tinggi) =>
{
    double luas = 0.5 * alas * tinggi;
    return $"Luas segitiga dengan alas={alas} dan tinggi={tinggi} adalah {luas}";
});
app.MapGet("/coba/luas-lingkaran", (double radius) =>
{
    double luas = Math.PI * Math.Pow(radius, 2);
    double keliling = Math.PI * 2 * radius;
    return $"Luas lingkaran dengan radius={radius} adalah {luas}, dengan keliling adalah {keliling}";
});

app.MapGet("api/v1/categories", (ICategory categoryData)=>
{
    var categories = categoryData.GetCategories();
    return categories;
});

app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id)=>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});

app.MapPost("api/v1/categories", (ICategory categoryData, Category category)=>
{
    var newCategory = categoryData.InsertCategory(category);
    return newCategory;
});

app.MapPut("api/v1/categories", (ICategory categoryData, Category category)=>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return updatedCategory;
});

app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id)=>
{
    var deletedCategory = categoryData.DeleteCategory(id);
    return deletedCategory;
});

app.MapGet("api/v1/instructors", (IInstructor instructorData)=>
{
    var instructors = instructorData.GetInstructors();
    return instructors;
});

app.MapGet("api/v1/instructors/{id}", (IInstructor instructorData, int id)=>
{
    var instructor = instructorData.GetInstructorById(id);
    return instructor;
});

app.MapDelete("api/v1/instructors/{id}", (IInstructor instructorData, int id)=>
{
    var deletedInstructor = instructorData.DeleteInstructor(id);
    return deletedInstructor;
});

app.MapPost("api/v1/instructors", (IInstructor instructorData, Instructor instructor)=>
{
    var newInstructor = instructorData.InsertInstructor(instructor);
    return newInstructor;
});

app.MapPut("api/v1/instructors", (IInstructor instructorData, Instructor instructor)=>
{
    var updatedInstructor = instructorData.UpdateInstructor(instructor);
    return updatedInstructor;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
