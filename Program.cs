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

app.MapGet("/", () => "Yes, API is Perfectly Calling");

var products = new List<Product>()
{
    new Product("Samsung Galaxy S24 Ultra",120000,101),
    new Product("Apple Iphone 16 Pro Max",160000,102),
    new Product("OnePlus 10 Pro",72000,103)
};

app.MapGet("/products", () =>
{
    return Results.Ok(products);    // 200
});



app.MapPost("/hello", () =>
{
    return Results.Created();  //  201
});

app.MapPut("/hello", () =>
{
    return Results.NoContent();  //  204
});

app.MapDelete("/hello", () =>
{
    return Results.NoContent();  //  204
});

app.Run();

public record Product(String Name, decimal Price, int id);
