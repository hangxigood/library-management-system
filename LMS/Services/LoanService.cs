using SQLite;


public interface ILoanService
{
    Task AddLoan(Loan newLoan);
    Task RemoveLoan(int loanId);
    Task UpdateLoan(Loan updatedLoan);
    Task<List<Loan>> GetLoansAsync();
    Task<Loan> GetLoanByIdAsync(int loanId);
    Task ReturnLoan(Loan returnLoan);
}

public class LoanService : ILoanService
{
    private readonly SQLiteAsyncConnection db;
    public LoanService(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        InitializeDatabaseAsync();
    }

    private async Task InitializeDatabaseAsync()
    {
        await db.CreateTableAsync<Loan>();
    }

    public async Task AddLoan(Loan loan)
    {
        // Retrieve the book based on ISBN
        var book = await db.Table<Book>().Where(b => b.ISBN == loan.BookISBN).FirstOrDefaultAsync();
        if (book == null)
        {
            throw new InvalidOperationException("No book found with the specified ISBN.");
        }
        else if (book.AvailableCopies <= 0)
        {
            throw new InvalidOperationException("This book is not avaliable.");
        }
        else
        {
            book.AvailableCopies -= 1;
            await db.UpdateAsync(book);
        }
        // Retrieve the member based on Email
        var member = await db.Table<Member>().Where(m => m.Email == loan.MemberEmail).FirstOrDefaultAsync();
        if (member == null)
        {
            throw new InvalidOperationException("No member found with the specified email.");
        }

        // Create a new Loan object
        var newLoan = new Loan
        {
            BookId = book.Id,  // Assuming the Loan table uses BookId
            MemberId = member.Id,  // Assuming the Loan table uses MemberId
            CheckoutDate = DateTime.Now,  // Assuming LoanDate is required
            DueDate = DateTime.Now.AddDays(14)  // Example of setting a due date 2 weeks from now
        };

    // Insert the new loan into the database
        await db.InsertAsync(newLoan);
    }
    public async Task<List<Loan>> GetLoansAsync()
    {
        var loans = await db.Table<Loan>().ToListAsync();
        var bookIds = loans.Select(l => l.BookId);
        var memberIds = loans.Select(l => l.MemberId);

        var books = await db.Table<Book>().Where(b => bookIds.Contains(b.Id)).ToListAsync();
        var members = await db.Table<Member>().Where(m => memberIds.Contains(m.Id)).ToListAsync();

        var bookDictionary = books.ToDictionary(b => b.Id, b => b);
        var memberDictionary = members.ToDictionary(m => m.Id, m => m);

        foreach (var loan in loans)
        {
            if (bookDictionary.TryGetValue(loan.BookId, out var book))
            {
                loan.BookTitle = book.Title;
                loan.BookISBN = book.ISBN;
            }

            if (memberDictionary.TryGetValue(loan.MemberId, out var member))
            {
                loan.MemberName = member.Name;
                loan.MemberEmail = member.Email;
            }
        }
        return loans;
    }


    public async Task<Loan> GetLoanByIdAsync(int LoanId)
    {
        return await db.FindAsync<Loan>(LoanId);
    }

    public async Task ReturnLoan(Loan returnLoan)
    {
        // Check if the loan already has a return date
        if (returnLoan.ReturnDate != null)
        {
            throw new InvalidOperationException("This loan has already been returned.");
        }

        // Set the return date to now
        returnLoan.ReturnDate = DateTime.Now;

        // Update the loan record in the database
        await db.UpdateAsync(returnLoan);

        // Update the available copies for the book
        var book = await db.FindAsync<Book>(returnLoan.BookId);
        if (book != null)
        {
            book.AvailableCopies += 1;
            await db.UpdateAsync(book);
        }
    }

    public async Task RemoveLoan(int LoanId)
    {
        var Loan = await db.FindAsync<Loan>(LoanId);
        if (Loan != null)
        {
            await db.DeleteAsync(Loan);
        }
    }
    public async Task UpdateLoan(Loan updatedLoan)
    {
        await db.UpdateAsync(updatedLoan);
    }

}