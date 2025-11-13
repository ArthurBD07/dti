namespace LeadManager.Api.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string Status { get; set; } = "invited"; // invited | accepted | declined
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        public string Suburb { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
