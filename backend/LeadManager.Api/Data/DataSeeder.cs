using LeadManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Api.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>().HasData(
                new Lead
                {
                    Id = 5577421,
                    Status = "invited",
                    FirstName = "Bill",
                    LastName = "",
                    CreatedAt = "January 4 @ 2:37 pm",
                    Suburb = "Yanderra 2574",
                    Category = "Painters",
                    Description = "Need to paint 2 aluminum windows and a sliding glass door",
                    Price = 62.00m,
                    Phone = "+61 6577421",
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
                    Price = 49.00m,
                    Phone = "+61 400 000 002",
                    Email = "craig@example.com"
                }
            );
        }
    }
}
