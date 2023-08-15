namespace Domain;

public class ChangeMemeberTransaction : ChangeAffiliationTransaction
{
    private readonly int _memberId;
    private readonly double _dues;

    public ChangeMemeberTransaction(int employeeId, int memeberId, double dues) 
        : base(employeeId)
    {
        _memberId = memeberId;
        _dues = dues;
    }

    protected override Affiliation Affiliation
    {
        get { return new UnionAffiliation(_memberId, _dues); }
    }

    protected override void RecordMembership(Employee employee)
    {
        PayrollDataBase.AddUnionMember(_memberId, employee);
    }
}