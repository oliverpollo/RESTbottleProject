using RESTbottle.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSingleton<IBottlesRepository> (new BottlesRepositoryList(includesTestData: true));
//builder.Services.AddSingleton<IBottlesRepository, BottlesRepositoryList>();
//builder.Services.AddSingleton<IBottlesRepository, BottlesRepositoryDatabase>();
builder.Services.AddOpenApi();

var app = builder.Build();

var connectionString =
    builder.Configuration.GetConnectionString("SimplyConnection");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
