namespace Domain;

public class ChangeNameTransaction : ChangeEmployeeTransaction
{
    private readonly string _newName;

    public ChangeNameTransaction(int id, string newName)
        : base(id)
    {
        _newName = newName;
    }

    protected override void Change(Employee employee)
    {
        employee.Name = _newName;
    }
}