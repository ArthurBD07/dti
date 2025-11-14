using System;
using System.Linq;
using LeadManager.Api.Data;
using LeadManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LeadManager.Api.Tests
{
    public class LeadDbContextTests
    {
        // Cria um DbContext em memória para usar nos testes
        private LeadDbContext CriarContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<LeadDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LeadDbContext(options);
        }

        // Helper para criar um lead "fake" preenchido
        private Lead CriarLeadFake(int id, string status = "invited")
        {
            return new Lead
            {
                Id = id,
                Status = status,
                FirstName = "Bill",
                LastName = "Smith",
                CreatedAt = "January 4 @ 2:37 pm",
                Suburb = "Yanderra 2574",
                Category = "Painters",
                Description = "Teste de lead",
                Price = 100m,
                Phone = "+61 400 000 001",
                Email = "bill@example.com"
            };
        }

        [Fact]
        public void Deve_Salvar_Lead_Com_Status_Invited()
        {
            // arrange
            using var db = CriarContextoEmMemoria();
            var lead = CriarLeadFake(1, "invited");

            // act
            db.Leads.Add(lead);
            db.SaveChanges();

            var salvo = db.Leads.Single(l => l.Id == 1);

            // assert
            Assert.Equal("invited", salvo.Status);
        }

        [Fact]
        public void Deve_Atualizar_Status_Para_Accepted()
        {
            // arrange
            using var db = CriarContextoEmMemoria();
            var lead = CriarLeadFake(2, "invited");
            db.Leads.Add(lead);
            db.SaveChanges();

            // act
            lead.Status = "accepted";
            db.SaveChanges();

            var salvo = db.Leads.Single(l => l.Id == 2);

            // assert
            Assert.Equal("accepted", salvo.Status);
        }
    }
}
