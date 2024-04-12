using SQLite;
using System.Threading.Tasks;

public class AdminService : IAdminService
{
    private readonly SQLiteAsyncConnection db;

    public AdminService(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        InitializeDatabaseAsync();
    }

    private async Task InitializeDatabaseAsync()
    {
        await db.CreateTableAsync<Book>();
        await db.CreateTableAsync<Member>();
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

    public async Task AddMember(Member newMember)
    {
        await db.InsertAsync(newMember);
    }

    public async Task<List<Member>> GetMembersAsync()
    {
        return await db.Table<Member>().ToListAsync();
    }

    public async Task RemoveMember(int memberId)
    {
        var member = await db.FindAsync<Member>(memberId);
        if (member != null)
        {
            await db.DeleteAsync(member);
        }
    }

    public async Task UpdateMember(Member updatedMember)
    {
        await db.UpdateAsync(updatedMember);
    }

}

public interface IAdminService
{
    Task AddBook(Book newBook);
    Task RemoveBook(int bookId);
    Task UpdateBook(Book updatedBook);
    Task AddMember(Member newMember);
    Task RemoveMember(int memberId);
    Task UpdateMember(Member updatedMember);
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(int bookId);
    Task<List<Member>> GetMembersAsync();
}
