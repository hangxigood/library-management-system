using SQLite;

public interface IMemberService
{
    void RegisterMember(Member newMember);
    Member GetMemberDetails(int memberId);
    void UpdateMemberInfo(Member updatedMember);
}

public class MemberService : IMemberService
{
    private readonly SQLiteConnection db;

    public MemberService(string dbPath)
    {
        db = new SQLiteConnection(dbPath);
        db.CreateTable<Member>();
    }

    public void RegisterMember(Member newMember)
    {
        db.Insert(newMember);
    }

    public Member GetMemberDetails(int memberId)
    {
        return db.Get<Member>(memberId);
    }

    public void UpdateMemberInfo(Member updatedMember)
    {
        db.Update(updatedMember);
    }
}
