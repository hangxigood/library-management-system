using SQLite;


public interface ILoanService
{
    Task AddLoan(Loan newLoan);
    Task RemoveLoan(int loanId);
    Task UpdateLoan(Loan updatedLoan);
    Task<List<Loan>> GetLoansAsync();
    Task<Loan> GetLoanByIdAsync(int loanId);
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
        return await db.Table<Loan>().ToListAsync();
    }

    public async Task<Loan> GetLoanByIdAsync(int LoanId)
    {
        return await db.FindAsync<Loan>(LoanId);
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