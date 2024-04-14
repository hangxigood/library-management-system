

using SQLite;
using System.ComponentModel.DataAnnotations;

public class Loan
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "BookId is required.")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "MemberId is required.")]
    public int MemberId { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable for when the book is not yet returned
}