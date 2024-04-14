

using SQLite;
using System.ComponentModel.DataAnnotations;

public class Book
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Author is required.")]
    public string Author { get; set; }

    [Required(ErrorMessage = "ISBN is required.")]
    [Unique] // This ensures the ISBN is unique across all entries
    [RegularExpression(@"^97[89]-\d{1,5}\d{1,7}\d{1,7}\d{1}$", ErrorMessage = "Invalid ISBN format.")]
    public string ISBN { get; set; }
    // Added properties for Book Copy amounts

    [ValidateCopyNumbers("TotalCopies", "AvailableCopies", ErrorMessage = "Total copies cannot be less than available copies.")]
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
}