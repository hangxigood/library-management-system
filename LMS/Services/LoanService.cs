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

    // Constructor initializes the SQLite connection
    public LoanService(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        InitializeDatabaseAsync();
    }

    // Initializes the database by creating the Loan table if it doesn't exist
    private async Task InitializeDatabaseAsync()
    {
        await db.CreateTableAsync<Loan>();
    }

    // Adds a new loan to the database after validating the book and member details
    public async Task AddLoan(Loan loan)
    {
        // Retrieve the book based on ISBN and check availability
        var book = await db.Table<Book>().Where(b => b.ISBN == loan.BookISBN).FirstOrDefaultAsync();
        if (book == null)
        {
            throw new InvalidOperationException("No book found with the specified ISBN.");
        }
        else if (book.AvailableCopies <= 0)
        {
            throw new InvalidOperationException("This book is not available.");
        }

        // Retrieve the member based on Email
        var member = await db.Table<Member>().Where(m => m.Email == loan.MemberEmail).FirstOrDefaultAsync();
        if (member == null)
        {
            throw new InvalidOperationException("No member found with the specified email.");
        }

        // Set the properties of the new Loan object
        var newLoan = new Loan
        {
            BookId = book.Id,
            MemberId = member.Id,
            CheckoutDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)  // Setting the due date 2 weeks from now
        };

        book.AvailableCopies -= 1;  // Decrement the number of available copies
        await db.UpdateAsync(book);  // Update the book record
        await db.InsertAsync(newLoan); // Insert the new loan into the database
    }

    // Retrieves all loans and their associated book and member details
    public async Task<List<Loan>> GetLoansAsync()
    {
        var loans = await db.Table<Loan>().ToListAsync();
        var bookIds = loans.Select(l => l.BookId);
        var memberIds = loans.Select(l => l.MemberId);

        var books = await db.Table<Book>().Where(b => bookIds.Contains(b.Id)).ToListAsync();
        var members = await db.Table<Member>().Where(m => memberIds.Contains(m.Id)).ToListAsync();

        var bookDictionary = books.ToDictionary(b => b.Id, b => b);
        var memberDictionary = members.ToDictionary(m => m.Id, m => m);

        // Augment each loan with book and member details
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

    // Retrieves a specific loan by its ID
    public async Task<Loan> GetLoanByIdAsync(int LoanId)
    {
        return await db.FindAsync<Loan>(LoanId);
    }

    // Returns a loan and updates the book's available copies
    public async Task ReturnLoan(Loan returnLoan)
    {
        if (returnLoan.ReturnDate != null)
        {
            throw new InvalidOperationException("This loan has already been returned.");
        }

        returnLoan.ReturnDate = DateTime.Now;  // Set the return date to now
        await db.UpdateAsync(returnLoan);  // Update the loan record

        var book = await db.FindAsync<Book>(returnLoan.BookId);
        if (book != null)
        {
            book.AvailableCopies += 1;  // Increment the available copies
            await db.UpdateAsync(book);  // Update the book record
        }
    }

    // Removes a loan from the database
    public async Task RemoveLoan(int LoanId)
    {
        var Loan = await db.FindAsync<Loan>(LoanId);
        if (Loan != null)
        {
            await db.DeleteAsync(Loan);
        }
    }

    // Updates a loan's details in the database
    public async Task UpdateLoan(Loan updatedLoan)
    {
        await db.UpdateAsync(updatedLoan);
    }
}
