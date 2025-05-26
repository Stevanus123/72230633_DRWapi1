using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Menambahkan EF Core

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty)));

//Sambung dengan CategoryEF
builder.Services.AddScoped<ICategory, CategoryEF>();

//Sambung dengan InstructorEF
builder.Services.AddScoped<IInstructor, InstructorEF>();

//Sambung dengan CourseEF
builder.Services.AddScoped<ICourse, CourseEF>();

//Sambung dengan AspUserEF
builder.Services.AddScoped<IAspUser, AspUserEF>();

// menambahkan AutoMapper
builder.Services.AddAutoMapper(typeof(mappingProfile));

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
app.MapGet("api/v1/course", (ICourse courseData, IMapper mapper) =>
{
    var courses = courseData.GetAllCourses();
    var courseDTOs = mapper.Map<List<CourseDTO>>(courses); 
    return Results.Ok(courseDTOs);
});


app.MapGet("api/v1/course/{id}", (ICourse courseData, IMapper mapper, int id) =>
{
    var course = courseData.GetCoursesByIdCourse(id);
    if (course == null)
    {
        return Results.NotFound();
    }

    var courseDTO = mapper.Map<CourseDTO>(course);
    return Results.Ok(courseDTO);
});


app.MapPost("api/v1/course", (ICourse courseData, IMapper mapper, CourseAddDTO courseAddDTO) =>
{
    try
    {
        var courseEntity = mapper.Map<Course>(courseAddDTO);
        var newCourse = courseData.AddCourse(courseEntity);
        var courseDTO = mapper.Map<CourseDTO>(newCourse);

        return Results.Created($"/api/v1/course/{newCourse.CourseId}", courseDTO);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("api/v1/course", (ICourse courseData, IMapper mapper, CourseUpdateDTO courseUpdateDTO) =>
{
    try
    {
        var courseEntity = mapper.Map<Course>(courseUpdateDTO);
        var updatedCourse = courseData.UpdateCourse(courseEntity);
        var courseDTO = mapper.Map<CourseDTO>(updatedCourse);

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



// Asp User punya tempat
app.MapGet("api/v1/users", (IAspUser aspUser) =>
{
    return aspUser.GetAllUsers();
});
app.MapGet("api/v1/users/{username}", (IAspUser aspUser, string username) =>
{
    return aspUser.GetUserByUsername(username);
});
app.MapPost("api/v1/users", (IAspUser aspUser, AspUser user) =>
{
    return aspUser.RegisterUser(user);
});
app.MapPut("api/v1/users", (IAspUser aspUser, AspUser user) =>
{
    return aspUser.UpdateUser(user);
});
app.MapDelete("api/v1/users/{username}", (IAspUser aspUser, string username) =>
{
    aspUser.DeleteUser(username);
});
app.MapGet("api/v1/users/{username}/{password}", (IAspUser aspUser, string username, string password) =>
{
    var userLogin = aspUser.Login(username, password);
    if (userLogin)
        return "Kamu berhasil login!";
    else
        return "Username/password salah!";
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
