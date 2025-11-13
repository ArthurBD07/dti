using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ✅ Habilita CORS para permitir requisições do frontend (React)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .AllowAnyOrigin()   // em produção você pode restringir
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// ✅ Usa CORS na aplicação
app.UseCors();

// "Banco de dados" em memória só para teste
var leads = new List<Lead>
{
    new Lead
    {
        Id = 5577421,
        Status = "invited",
        FirstName = "Bill",
        LastName = "Smith",
        CreatedAt = "January 4 @ 2:37 pm",
        Suburb = "Yanderra 2574",
        Category = "Painters",
        Description = "Need to paint 2 aluminium windows and a sliding glass door",
        Price = 62m,
        Phone = "+61 400 000 001",
        Email = "bill@example.com"
    },
    new Lead
    {
        Id = 5588872,
        Status = "invited",
        FirstName = "Craig",
        LastName = "Johnson",
        CreatedAt = "January 4 @ 1:15 pm",
        Suburb = "Woolooware 2230",
        Category = "Interior Painters",
        Description = "internal walls 3 colours",
        Price = 49m,
        Phone = "+61 400 000 002",
        Email = "craig@example.com"
    }
};

// Listar todos os leads invited
app.MapGet("/leads/invited", () =>
    leads.Where(l => l.Status == "invited")
);

// Listar todos os leads accepted
app.MapGet("/leads/accepted", () =>
    leads.Where(l => l.Status == "accepted")
);

// Aceitar lead
app.MapPost("/leads/{id}/accept", (int id) =>
{
    var lead = leads.SingleOrDefault(l => l.Id == id);
    if (lead is null)
    {
        return Results.NotFound();
    }

    // aplica desconto de 10% se preço > 500
    if (lead.Price > 500)
    {
        lead.Price = Math.Round(lead.Price * 0.9m, 2);
    }

    lead.Status = "accepted";

    // simula envio de e-mail: grava em arquivo de log
    var mensagem =
        $"[{DateTime.Now}] Lead {lead.Id} aceito por {lead.Price:C} - enviar e-mail para vendas@test.com";
    File.AppendAllText("emails.log", mensagem + Environment.NewLine);

    return Results.Ok(lead);
});

// Recusar lead
app.MapPost("/leads/{id}/decline", (int id) =>
{
    var lead = leads.SingleOrDefault(l => l.Id == id);
    if (lead is null)
    {
        return Results.NotFound();
    }

    lead.Status = "declined";
    return Results.Ok(lead);
});


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record Lead
{
    public int Id { get; init; }
    public string Status { get; set; } = "invited"; // invited | accepted | declined
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string CreatedAt { get; init; } = default!;
    public string Suburb { get; init; } = default!;
    public string Category { get; init; } = default!;
    public string Description { get; init; } = default!;
    public decimal Price { get; set; }
    public string Phone { get; init; } = default!;
    public string Email { get; init; } = default!;
}
