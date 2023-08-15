namespace Domain;

public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
{
    public ChangeAffiliationTransaction(int id) : base(id)
    {
    }

    protected override void Change(Employee employee)
    {
        RecordMembership(employee);
        Affiliation affiliation = Affiliation;
        employee.Affiliation = affiliation;
    }

    protected abstract Affiliation Affiliation { get; }
    protected abstract void RecordMembership(Employee employee);
}