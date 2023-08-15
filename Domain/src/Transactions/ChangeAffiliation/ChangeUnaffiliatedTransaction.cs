namespace Domain;

public class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction
{
    public ChangeUnaffiliatedTransaction(int id) : base(id)
    {
    }

    protected override Affiliation Affiliation
    {
        get { return new NoAffiliation(); }  
    }

    protected override void RecordMembership(Employee employee)
    {
        Affiliation affiliation = employee.Affiliation;
        if (affiliation is UnionAffiliation)
        {
            UnionAffiliation? unionAffiliation = affiliation as UnionAffiliation;
            int memberId = unionAffiliation.MemberId;
            PayrollDataBase.RemoveUnionMember(memberId);
        }
    }
}