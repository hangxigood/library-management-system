

using SQLite;

public class Book
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    // Added properties for Book Copy amounts
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
}