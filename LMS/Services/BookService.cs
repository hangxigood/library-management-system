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
    public BookService(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        InitializeDatabaseAsync();
    }

    private async Task InitializeDatabaseAsync()
    {
        await db.CreateTableAsync<Book>();
    }

    public async Task AddBook(Book newBook)
    {
        await db.InsertAsync(newBook);
    }
    public async Task<List<Book>> GetBooksAsync()
    {
        return await db.Table<Book>().ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(int bookId)
    {
        return await db.FindAsync<Book>(bookId);
    }

    public async Task RemoveBook(int bookId)
    {
        var book = await db.FindAsync<Book>(bookId);
        if (book != null)
        {
            await db.DeleteAsync(book);
        }
    }

    public async Task UpdateBook(Book updatedBook)
    {
        await db.UpdateAsync(updatedBook);
    }

}