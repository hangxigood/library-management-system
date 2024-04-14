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

    public async Task AddLoan(Loan newLoan)
    {
        await db.InsertAsync(newLoan);
    }
    public async Task<List<Loan>> GetLoansAsync()
    {
        var loans = await db.Table<Loan>().ToListAsync();
        foreach (var loan in loans)
        {
            var book = await db.FindAsync<Book>(loan.BookId);
            if (book != null)
            {
                loan.BookTitle = book.Title;
            }

            var member = await db.FindAsync<Member>(loan.MemberId);
            if (member != null)
            {
                loan.MemberName = member.Name;
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