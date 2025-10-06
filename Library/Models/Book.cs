namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; // Para validar único
        public int AvailableCopies { get; set; }
        public bool Availability { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
