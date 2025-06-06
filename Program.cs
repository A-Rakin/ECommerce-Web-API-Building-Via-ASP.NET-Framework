using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the Controller
builder.Services.AddControllers();
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


app.MapControllers();
app.Run();
//CRUD 
//Create => Create a Category -->POST: /api/categories
//Read => Read a Category   --> GET: /api/categories
//Update => Update a Category  -->PUT: /api/categories
//Delete => Delete a Category  --> DELETE: /api/categories