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
            new Member { Id = 1, Name = "Alice Johnson", Email = "alice.johnson@example.com" },
            new Member { Id = 2, Name = "Bob Smith", Email = "bob.smith@example.com" },
            new Member { Id = 3, Name = "Carol White", Email = "carol.white@example.com" },
            new Member { Id = 4, Name = "David Brown", Email = "david.brown@example.com" },
            new Member { Id = 5, Name = "Eva Parker", Email = "eva.parker@example.com" },
            new Member { Id = 6, Name = "Frank Harris", Email = "frank.harris@example.com" },
            new Member { Id = 7, Name = "Grace Hall", Email = "grace.hall@example.com" },
            new Member { Id = 8, Name = "Henry Allen", Email = "henry.allen@example.com" },
            new Member { Id = 9, Name = "Ivy Wilson", Email = "ivy.wilson@example.com" },
            new Member { Id = 10, Name = "Jack Martinez", Email = "jack.martinez@example.com" },
            new Member { Id = 11, Name = "Karen Hill", Email = "karen.hill@example.com" },
            new Member { Id = 12, Name = "Leo Scott", Email = "leo.scott@example.com" },
            new Member { Id = 13, Name = "Mia Rivera", Email = "mia.rivera@example.com" },
            new Member { Id = 14, Name = "Noah Green", Email = "noah.green@example.com" },
            new Member { Id = 15, Name = "Olivia Lee", Email = "olivia.lee@example.com" },
            new Member { Id = 16, Name = "Pete Young", Email = "pete.young@example.com" },
            new Member { Id = 17, Name = "Quinn Cook", Email = "quinn.cook@example.com" },
            new Member { Id = 18, Name = "Rachel Adams", Email = "rachel.adams@example.com" },
            new Member { Id = 19, Name = "Samuel Black", Email = "samuel.black@example.com" },
            new Member { Id = 20, Name = "Tina Clark", Email = "tina.clark@example.com" },
            new Member { Id = 21, Name = "Uma Thompson", Email = "uma.thompson@example.com" },
            new Member { Id = 22, Name = "Victor Phillips", Email = "victor.phillips@example.com" },
            new Member { Id = 23, Name = "Wendy Mitchell", Email = "wendy.mitchell@example.com" },
            new Member { Id = 24, Name = "Xavier Turner", Email = "xavier.turner@example.com" },
            new Member { Id = 25, Name = "Yara Lewis", Email = "yara.lewis@example.com" },
            new Member { Id = 26, Name = "Zane Walker", Email = "zane.walker@example.com" },
            new Member { Id = 27, Name = "April Carter", Email = "april.carter@example.com" },
            new Member { Id = 28, Name = "Bruce King", Email = "bruce.king@example.com" },
            new Member { Id = 29, Name = "Cindy Wright", Email = "cindy.wright@example.com" },
            new Member { Id = 30, Name = "Derek Foster", Email = "derek.foster@example.com" }
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
