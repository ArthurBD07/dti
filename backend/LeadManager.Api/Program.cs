using LeadManager.Api.Data;
using LeadManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);



// CORS para permitir o React acessar a API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ⚠️ Ajuste se o nome do servidor SQL for diferente.
// Pelo seu print do SSMS o servidor parece ser "PC-ARTHUR\Arthu" (ou parecido).
// Olhe no SSMS, na parte de cima da árvore, e copie EXATAMENTE o nome após o "(":
//
// Exemplo: PC-ARTHUR\SQLEXPRESS  -> use assim:
var connectionString =
    "Server=localhost;Database=LeadManagerDb;Trusted_Connection=True;TrustServerCertificate=True;";


// Registra o DbContext do EF Core usando SQL Server
builder.Services.AddDbContext<LeadDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();



app.UseHttpsRedirection();
app.UseCors();

// Garante que o banco existe e faz seed dos dados iniciais
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LeadDbContext>();

    // Cria o banco e tabela se ainda não existirem
    db.Database.EnsureCreated();

    // Seed: só insere se ainda não tiver leads
    if (!db.Leads.Any())
    {
        db.Leads.AddRange(
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
        );

        db.SaveChanges();
    }
}

// Listar todos os leads invited
app.MapGet("/leads/invited", async (LeadDbContext db) =>
    await db.Leads.Where(l => l.Status == "invited").ToListAsync()
);

// Listar todos os leads accepted
app.MapGet("/leads/accepted", async (LeadDbContext db) =>
    await db.Leads.Where(l => l.Status == "accepted").ToListAsync()
);

// Aceitar lead
app.MapPost("/leads/{id}/accept", async (int id, LeadDbContext db) =>
{
    var lead = await db.Leads.SingleOrDefaultAsync(l => l.Id == id);
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

    await db.SaveChangesAsync();

    // simula envio de e-mail: grava em arquivo de log
    var mensagem =
        $"[{DateTime.Now}] Lead {lead.Id} aceito por {lead.Price:C} - enviar e-mail para vendas@test.com";
    File.AppendAllText("emails.log", mensagem + Environment.NewLine);

    return Results.Ok(lead);
});

// Recusar lead
app.MapPost("/leads/{id}/decline", async (int id, LeadDbContext db) =>
{
    var lead = await db.Leads.SingleOrDefaultAsync(l => l.Id == id);
    if (lead is null)
    {
        return Results.NotFound();
    }

    lead.Status = "declined";
    await db.SaveChangesAsync();

    return Results.Ok(lead);
});

// Endpoint extra de WeatherForecast (opcional)
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
