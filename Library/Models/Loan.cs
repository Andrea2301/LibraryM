namespace Library.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime ReturnDate { get; set; }

        // Relaciones
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
