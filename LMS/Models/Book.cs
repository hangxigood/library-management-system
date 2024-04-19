using SQLite;

public class Book
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearPublished { get; set; }
    public bool IsAvailable { get; set; } // True if the book is available for borrowing
}
