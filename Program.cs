using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Menambahkan EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") ?? String.Empty));

//Sambung dengan CategoryEF
builder.Services.AddScoped<ICategory, CategoryEF>();

//Sambung dengan InstructorEF
builder.Services.AddScoped<IInstructor, InstructorEF>();

//Sambung dengan CourseEF
builder.Services.AddScoped<ICourse, CourseEF>();

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
app.MapGet("api/v1/categories", (ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
});

app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});

app.MapPost("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var newCategory = categoryData.InsertCategory(category);
    return newCategory;
});

app.MapPut("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return updatedCategory;
});

app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
});



// tak kasih tanda biar agak jauh dan beda
// instructor implementation ini tugasnya
app.MapGet("api/v1/instructors", (IInstructor instructorData) =>
{
    return instructorData.GetInstructors();

});

app.MapGet("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    return instructorData.GetInstructorById(id);
});

app.MapDelete("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
});

app.MapPost("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    return instructorData.InsertInstructor(instructor);
});

app.MapPut("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    return instructorData.UpdateInstructor(instructor);
});
// ini gak dari gpt atau apa ya kak, full buatan atas otak sendiri


// course implementation
app.MapGet("api/v1/course", (ICourse courseData) =>
{
    List<CourseDTO> courseDTOs = new List<CourseDTO>();
    var courses = courseData.GetAllCourses();
    foreach (var course in courses)
    {
        CourseDTO courseDTO = new CourseDTO
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            CourseDescription = course.CourseDescription,
            Duration = course.Duration,
            Category = new CategoryDTO
            {
                CategoryId = course.Category?.CategoryId ?? 0,
                CategoryName = course.Category?.CategoryName ?? string.Empty
            },
            Instructor = new InstructorDTO
            {
                InstructorId = course.Instructor?.InstructorId?? 0,
                InstructorName = course.Instructor?.InstructorName ?? string.Empty,
                InstructorEmail = course.Instructor?.InstructorEmail ?? string.Empty,
                InstructorPhone = course.Instructor?.InstructorPhone ?? string.Empty,
                InstructorAddress = course.Instructor?.InstructorAddress ?? string.Empty,
                InstructorCity = course.Instructor?.InstructorCity ?? string.Empty
            }
        };
        courseDTOs.Add(courseDTO);
    }
    return Results.Ok(courseDTOs);
});

app.MapGet("api/v1/course/{id}", (ICourse courseData, int id) =>
{
    CourseDTO courseDTO = new CourseDTO();
    var course = courseData.GetCoursesByIdCourse(id);
    if (course == null)
    {
        return Results.NotFound();
    }
    courseDTO.CourseId = course.CourseId;
    courseDTO.CourseName = course.CourseName;
    courseDTO.CourseDescription = course.CourseDescription;
    courseDTO.Duration = course.Duration;
    courseDTO.Category = new CategoryDTO
    {
        CategoryId = course.Category?.CategoryId?? 0,
        CategoryName = course.Category?.CategoryName ?? string.Empty
    };
    courseDTO.Instructor = new InstructorDTO
    {
        InstructorId = course.Instructor?.InstructorId?? 0,
        InstructorName = course.Instructor?.InstructorName ?? string.Empty,
        InstructorEmail = course.Instructor?.InstructorEmail ?? string.Empty,
        InstructorPhone = course.Instructor?.InstructorPhone ?? string.Empty,
        InstructorAddress = course.Instructor?.InstructorAddress ?? string.Empty,
        InstructorCity = course.Instructor?.InstructorCity ?? string.Empty
    };
    return Results.Ok(courseDTO);
});

app.MapPost("api/v1/course", (ICourse courseData, CourseAddDTO courseAddDTO) =>
{
    Course course = new Course
    {
        CourseName = courseAddDTO.CourseName,
        CourseDescription = courseAddDTO.CourseDescription,
        Duration = courseAddDTO.Duration,
        CategoryId = courseAddDTO.CategoryId,
        InstructorId = courseAddDTO.InstructorId
    };
    try
    {
        var newCourse = courseData.AddCourse(course);
        CourseDTO courseDTO = new CourseDTO
        {
            CourseId = newCourse.CourseId,
            CourseName = newCourse.CourseName,
            CourseDescription = newCourse.CourseDescription,
            Duration = newCourse.Duration,
            Category = new CategoryDTO
            {
                CategoryId = newCourse.CategoryId
            },
            Instructor = new InstructorDTO
            {
                InstructorId = newCourse.InstructorId
            }
        };
        return Results.Created($"/api/v1/course/{newCourse.CourseId}", courseDTO);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("api/v1/course", (ICourse courseData, CourseUpdateDTO courseUpdateDTO) =>
{
    Course course = new Course
    {
        CourseId = courseUpdateDTO.CourseId,
        CourseName = courseUpdateDTO.CourseName,
        CourseDescription = courseUpdateDTO.CourseDescription,
        Duration = courseUpdateDTO.Duration,
        CategoryId = courseUpdateDTO.CategoryId,
        InstructorId = courseUpdateDTO.InstructorId
    };
    try
    {
        var updatedCourse = courseData.UpdateCourse(course);
        CourseDTO courseDTO = new CourseDTO
        {
            CourseId = updatedCourse.CourseId,
            CourseName = updatedCourse.CourseName,
            CourseDescription = updatedCourse.CourseDescription,
            Duration = updatedCourse.Duration,
            Category = new CategoryDTO
            {
                CategoryId = updatedCourse.CategoryId
            },
            Instructor = new InstructorDTO
            {
                InstructorId = updatedCourse.InstructorId
            }
        };
        return Results.Ok(courseDTO);

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("api/v1/course/{id}", (ICourse courseData, int id) =>
{
    courseData.DeleteCourse(id);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
