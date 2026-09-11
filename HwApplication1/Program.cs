var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var students = new List<Student>
{
    new Student
    {
        Id = 1,
        Name = "Ayan",
        Age = 20
    },
    new Student
    {
        Id = 2,
        Name = "Dana",
        Age = 21
    }
};

app.MapGet("/api/students", () =>
{
    return Results.Ok(students);
});

app.MapPost("/api/students", (Student student) =>
{
    student.Id = students.Count + 1;
    students.Add(student);

    return Results.Ok(student);
});

app.MapGet("/api/students/{id}", (int id) =>
{
    var student = students.FirstOrDefault(x => x.Id == id);

    if (student == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(student);
});

app.Run();

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}