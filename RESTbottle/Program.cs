using RESTbottle.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


const string FrontendClients = "FrontendClients";

builder.Services.AddCors(options =>
{
    // This is a public teaching API, so browser clients served by Live Server
    // (or another front-end project) may use every CRUD endpoint.
    options.AddPolicy(FrontendClients, policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

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

// Live Server uses the local HTTP launch profile during development. Azure (and
// other production hosts) should still redirect clients to HTTPS.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(FrontendClients);

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
