using SQLite;

public interface IBookService
{
    Task AddBook(Book newBook);
    Task RemoveBook(int bookId);
    Task UpdateBook(Book updatedBook);
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(int bookId);
}

public class BookService : IBookService
{
    private readonly SQLiteAsyncConnection db;

    // Constructor initializes the SQLite connection
    public BookService(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        InitializeDatabaseAsync(); // Initializes database tables
    }

    // Asynchronously creates the Book table if it does not exist in the database
    private async Task InitializeDatabaseAsync()
    {
        await db.CreateTableAsync<Book>();
    }

    // Adds a new book to the database, setting available copies to the total copies
    public async Task AddBook(Book newBook)
    {
        // Initializes available copies to match the total copies at the time of addition
        newBook.AvailableCopies = newBook.TotalCopies;
        await db.InsertAsync(newBook); // Inserts the new book into the database
    }

    // Retrieves all books from the database
    public async Task<List<Book>> GetBooksAsync()
    {
        return await db.Table<Book>().ToListAsync(); // Returns a list of all books
    }

    // Retrieves a specific book by its ID
    public async Task<Book> GetBookByIdAsync(int bookId)
    {
        // Uses the primary key to find a single book, if it exists
        return await db.FindAsync<Book>(bookId);
    }

    // Removes a specific book from the database
    public async Task RemoveBook(int bookId)
    {
        var book = await db.FindAsync<Book>(bookId); // First finds the book by ID
        if (book != null)
        {
            await db.DeleteAsync(book); // Deletes the book if found
        }
    }

    // Updates the details of an existing book in the database
    public async Task UpdateBook(Book updatedBook)
    {
        await db.UpdateAsync(updatedBook); // Performs an update on the book record
    }
}
