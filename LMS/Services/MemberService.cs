using SQLite;

public interface IMemberService
{
    Task AddMember(Member newMember);
    Task RemoveMember(int memberId);
    Task UpdateMember(Member updatedMember);
    Task<List<Member>> GetMembersAsync();
}

public class MemberService : IMemberService
{
    private readonly SQLiteAsyncConnection db;

    public MemberService(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        InitializeDatabaseAsync();
    }
    private async Task InitializeDatabaseAsync()
    {
        await db.CreateTableAsync<Member>();
    }
    public async Task AddMember(Member newMember)
    {
        await db.InsertAsync(newMember);
    }

    public async Task<List<Member>> GetMembersAsync()
    {
        // Retrieve all members
        var members = await db.Table<Member>().ToListAsync();

        // Collect member IDs for loan lookup
        var memberIds = members.Select(m => m.Id).ToList();

        // Retrieve all loans that have a MemberId matching any member ID
        var loans = await db.Table<Loan>().Where(l => memberIds.Contains(l.MemberId)).ToListAsync();

        // Create a dictionary to count loans for each member
        var loanCounts = new Dictionary<int, int>();
        foreach (var loan in loans)
        {
            if (loanCounts.ContainsKey(loan.MemberId))
            {
                loanCounts[loan.MemberId]++;
            }
            else
            {
                loanCounts[loan.MemberId] = 1;
            }
        }

        // Assign loan count to each member
        foreach (var member in members)
        {
            member.NumberOfLoans = loanCounts.ContainsKey(member.Id) ? loanCounts[member.Id] : 0;
        }

        return members;
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
