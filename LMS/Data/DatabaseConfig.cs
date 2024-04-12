using SQLite;
using System;
using System.IO;

public class DatabaseConfig
{
    private readonly string dbPath;

    public DatabaseConfig(string databaseFileName = "LibraryManagement.db")
    {
        // Builds the database path.
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        dbPath = Path.Combine(folderPath, databaseFileName);
        ConfigureDatabase();
    }

    private void ConfigureDatabase()
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            // Drop tables if they exist to reset the database on each build
            db.DropTable<Loan>();
            db.DropTable<Member>();
            db.DropTable<Book>();
            // Create tables based on models
            db.CreateTable<Book>();
            db.CreateTable<Member>();
            db.CreateTable<Loan>();

            // Seed the tables with initial data
            SeedDatabase(db);
        }
    }

    public string GetDatabasePath()
    {
        return dbPath;
    }

    private void SeedDatabase(SQLiteConnection db)
    {
        // Example data for Books
        var books = new List<Book>
        {
            new Book { Title = "1984", Author = "George Orwell", ISBN = "978-0451524935", TotalCopies = 3, AvailableCopies = 3 },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "978-0446310789", TotalCopies = 2, AvailableCopies = 1 }
        };
        db.InsertAll(books);

        // Example data for Members
        var members = new List<Member>
        {
            new Member { Name = "John Doe", Email = "john.doe@example.com" },
            new Member { Name = "Jane Smith", Email = "jane.smith@example.com" }
        };
        db.InsertAll(members);

        // Example data for Loans
        var loans = new List<Loan>
        {
            new Loan { BookId = 2, MemberId = 1, CheckoutDate = DateTime.Now.AddDays(-10), DueDate = DateTime.Now.AddDays(20) }
        };
        db.InsertAll(loans);
    }
}
