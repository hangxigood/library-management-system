using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

public class LibraryDatabase
{
    readonly SQLiteAsyncConnection _database;

    public LibraryDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Book>().Wait();
    }

    public Task<List<Book>> GetBooksAsync()
    {
        // Retrieves all books
        return _database.Table<Book>().ToListAsync();
    }

    public Task<List<Book>> GetAvailableBooksAsync()
    {
        // Retrieves all available books
        return _database.Table<Book>().Where(b => b.IsAvailable).ToListAsync();
    }

    public Task<Book> GetBookAsync(int id)
    {
        // Retrieves a single book by ID
        return _database.Table<Book>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public Task<int> SaveBookAsync(Book book)
    {
        if (book.ID != 0)
        {
            // Update an existing book
            return _database.UpdateAsync(book);
        }
        else
        {
            // Add a new book
            return _database.InsertAsync(book);
        }
    }

    public Task<int> DeleteBookAsync(Book book)
    {
        // Delete a book
        return _database.DeleteAsync(book);
    }
}
