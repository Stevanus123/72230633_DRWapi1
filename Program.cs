using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//Dependency Injection
builder.Services.AddSingleton<ICategory, CategoryNew06>();

//Dependency Injection Instructor
builder.Services.AddSingleton<IInstructor, InstructorNew06>();

//Dependency Injection Course
builder.Services.AddSingleton<ICourse, CourseNew07>();

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


// bawaan dari template
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


// coba dengan parameter (awal)
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


// category implementation
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
    categoryData.DeleteCategory(id);
});



// tak kasih tanda biar agak jauh dan beda
// instructor implementation ini tugasnya
app.MapGet("api/v1/instructors", (IInstructor instructorData)=>
{
    return instructorData.GetInstructors();

});

app.MapGet("api/v1/instructors/{id}", (IInstructor instructorData, int id)=>
{
    return instructorData.GetInstructorById(id);
});

app.MapDelete("api/v1/instructors/{id}", (IInstructor instructorData, int id)=>
{
    instructorData.DeleteInstructor(id);
});

app.MapPost("api/v1/instructors", (IInstructor instructorData, Instructor instructor)=>
{
    return instructorData.InsertInstructor(instructor);
});

app.MapPut("api/v1/instructors", (IInstructor instructorData, Instructor instructor)=>
{
    return instructorData.UpdateInstructor(instructor);
});
// ini gak dari gpt atau apa ya kak, full buatan atas otak sendiri

app.MapGet("api/v1/course", (ICourse courseData) =>
{
    return courseData.GetCourses();
});

app.MapGet("api/v1/course/{id}", (ICourse courseData, int id) => 
{
    return courseData.GetCourseById(id);
});

app.MapPost("api/v1/course", (ICourse courseData, Course course) =>
{
   return courseData.AddCourse(course); 
});

app.MapPut("api/v1/course", (ICourse courseData, Course course) =>
{
    return courseData.UpdateCourse(course);
});

app.MapDelete("api/v1/course/{id}", (ICourse courseData, int id)=>
{
    courseData.DeleteCourse(id);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
