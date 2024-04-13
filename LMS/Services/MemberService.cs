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
