using SQLite;

public interface IBookService
{
    List<Book> SearchBooks(string criteria);
    bool CheckoutBook(int bookId, int memberId);
    bool ReturnBook(int bookCopyId);
}

public class BookService : IBookService
{
    private readonly SQLiteConnection db;

    public BookService(string dbPath)
    {
        db = new SQLiteConnection(dbPath);
        db.CreateTable<Book>();
        db.CreateTable<Loan>();
    }

    public List<Book> SearchBooks(string criteria)
    {
        return db.Table<Book>().Where(b => b.Title.Contains(criteria) || b.Author.Contains(criteria)).ToList();
    }

    public bool CheckoutBook(int bookId, int memberId)
    {
        var book = db.Table<Book>().FirstOrDefault(b => b.Id == bookId);
        if (book == null || book.AvailableCopies <= 0) return false;

        book.AvailableCopies--;
        db.Update(book);

        var loan = new Loan { BookId = bookId, MemberId = memberId, CheckoutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) };
        db.Insert(loan);
        return true;
    }

    public bool ReturnBook(int bookId)
    {
        var book = db.Table<Book>().FirstOrDefault(b => b.Id == bookId);
        if (book == null || book.AvailableCopies >= book.TotalCopies) return false;

        book.AvailableCopies++;
        db.Update(book);

        var loan = db.Table<Loan>().FirstOrDefault(l => l.BookId == bookId && l.ReturnDate == null);
        if (loan != null)
        {
            loan.ReturnDate = DateTime.Now;
            db.Update(loan);
        }
        return true;
    }
}
