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


const string AllowALLCORS = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowALLCORS,
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                              });
    options.AddPolicy(name: "TESTCORS",
        policy =>
    {
        policy.AllowAnyOrigin().WithMethods("GET").AllowAnyHeader();

    });

    // Local development policy for pages served from VS Code Live Server
    options.AddPolicy(name: "LocalDev",
        policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5501", "http://localhost:5501").AllowAnyMethod().AllowAnyHeader();
    });

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

app.UseHttpsRedirection();

// Use LocalDev CORS during development so the API can be called from VS Code Live Server
if (app.Environment.IsDevelopment())
{
    app.UseCors("LocalDev");
}
else
{
    app.UseCors("TESTCORS");
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
