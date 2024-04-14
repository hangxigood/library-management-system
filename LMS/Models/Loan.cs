

using SQLite;
using System.ComponentModel.DataAnnotations;

public class Loan
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Required(ErrorMessage = "BookId is required.")]
    public int BookId { get; set; }
    [Ignore]
    public string BookTitle { get; set; }  // Additional property for book title
    [Ignore]
    [Required(ErrorMessage = "ISBN is required.")]
    [RegularExpression(@"^97[89]-\d{1,5}\d{1,7}\d{1,7}\d{1}$", ErrorMessage = "Invalid ISBN format.")]
    public string BookISBN { get; set; }

    [Required(ErrorMessage = "MemberId is required.")]
    public int MemberId { get; set; }
    [Ignore]
    public string MemberName { get; set; }  // Additional property for member name
    [Ignore]
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email format.")]
    public string MemberEmail { get; set; }

    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable for when the book is not yet returned
}