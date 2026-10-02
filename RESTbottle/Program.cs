using RESTbottle.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSingleton<IBottlesRepository> (new BottlesRepositoryList(includesTestData: true));
builder.Services.AddSingleton<ITradingCardsRepo> (new TradingCardRepo());
builder.Services.AddSingleton<ICharacterRepo>(new CharacterRepo());
//builder.Services.AddSingleton<IBottlesRepository, BottlesRepositoryList>();
//builder.Services.AddSingleton<IBottlesRepository, BottlesRepositoryDatabase>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenApi();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


var connectionString =
    builder.Configuration.GetConnectionString("SimplyConnection");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
