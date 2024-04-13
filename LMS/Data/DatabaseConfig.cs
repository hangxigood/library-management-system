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
            new Book { Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "978-1503290563", TotalCopies = 4, AvailableCopies = 4 },
            new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "978-0743273565", TotalCopies = 5, AvailableCopies = 3 },
            new Book { Title = "Moby Dick", Author = "Herman Melville", ISBN = "978-1503280786", TotalCopies = 3, AvailableCopies = 3 },
            new Book { Title = "War and Peace", Author = "Leo Tolstoy", ISBN = "978-1400079988", TotalCopies = 2, AvailableCopies = 1 },
            new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "978-0316769488", TotalCopies = 4, AvailableCopies = 4 },
            new Book { Title = "1984", Author = "George Orwell", ISBN = "978-0451524935", TotalCopies = 3, AvailableCopies = 3 },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "978-0446310789", TotalCopies = 2, AvailableCopies = 1 },
            new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", ISBN = "978-0544003415", TotalCopies = 4, AvailableCopies = 2 },
            new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", ISBN = "978-0547928227", TotalCopies = 3, AvailableCopies = 2 },
            new Book { Title = "Fahrenheit 451", Author = "Ray Bradbury", ISBN = "978-1451673319", TotalCopies = 3, AvailableCopies = 3 },
            new Book { Title = "Brave New World", Author = "Aldous Huxley", ISBN = "978-0060850524", TotalCopies = 5, AvailableCopies = 5 },
            new Book { Title = "The Road", Author = "Cormac McCarthy", ISBN = "978-0307387899", TotalCopies = 2, AvailableCopies = 1 },
            new Book { Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", ISBN = "978-0486415871", TotalCopies = 3, AvailableCopies = 2 },
            new Book { Title = "Anna Karenina", Author = "Leo Tolstoy", ISBN = "978-1593080273", TotalCopies = 4, AvailableCopies = 4 },
            new Book { Title = "The Adventures of Huckleberry Finn", Author = "Mark Twain", ISBN = "978-0486280615", TotalCopies = 3, AvailableCopies = 3 },
            new Book { Title = "Les Misérables", Author = "Victor Hugo", ISBN = "978-0451419439", TotalCopies = 2, AvailableCopies = 1 },
            new Book { Title = "Great Expectations", Author = "Charles Dickens", ISBN = "978-0141439563", TotalCopies = 3, AvailableCopies = 2 },
            new Book { Title = "Catch-22", Author = "Joseph Heller", ISBN = "978-1451626650", TotalCopies = 3, AvailableCopies = 3 },
            new Book { Title = "Wuthering Heights", Author = "Emily Bronte", ISBN = "978-1853260018", TotalCopies = 2, AvailableCopies = 2 },
            new Book { Title = "Jane Eyre", Author = "Charlotte Bronte", ISBN = "978-0142437209", TotalCopies = 5, AvailableCopies = 5 },
            new Book { Title = "Frankenstein", Author = "Mary Shelley", ISBN = "978-0486282114", TotalCopies = 3, AvailableCopies = 2 },
            new Book { Title = "The Picture of Dorian Gray", Author = "Oscar Wilde", ISBN = "978-0486278073", TotalCopies = 2, AvailableCopies = 1 },
            new Book { Title = "The Grapes of Wrath", Author = "John Steinbeck", ISBN = "978-0143039433", TotalCopies = 4, AvailableCopies = 4 }
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
