

using SQLite;

public class Loan
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable for when the book is not yet returned
}