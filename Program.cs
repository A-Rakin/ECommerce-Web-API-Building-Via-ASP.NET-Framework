var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

List<Category> categories = new List<Category>();

app.MapGet("/", () => "Yes, API is Perfectly Calling");


//Read a Category   --> GET: /api/categories
app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories);    // 200
});

app.MapPost("/api/categories", () =>
{
    var newcategories = new Category
    {
        CategoryId = Guid.Parse("7211fc23-deee-4d8b-b908-d02c40d7c68d"),
        Name = "Electronics",
        Description = "All are Electronic devices",
        CreatedAt = DateTime.UtcNow
    };
    categories.Add(newcategories);
    return Results.Created("/api/categories/{newcategories.CategoryId}", newcategories);  //201
});

app.MapDelete("/api/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == Guid.Parse
    ("7211fc23-deee-4d8b-b908-d02c40d7c6"));


    if (foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();    // 200
});


app.MapPut("/api/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == Guid.Parse
    ("7211fc23-deee-4d8b-b908-d02c40d7c6"));

    if (foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }

    foundCategory.Name = "Smartphone";
    foundCategory.Description = "Smartphone are nice Category";
    return Results.NoContent();    // 200
});
app.Run();

public record Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}


//CRUD 
//Create => Create a Category -->POST: /api/categories
//Read => Read a Category   --> GET: /api/categories
//Update => Update a Category  -->PUT: /api/categories
//Delete => Delete a Category  --> DELETE: /api/categories