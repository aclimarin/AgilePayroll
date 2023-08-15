namespace Domain;

public abstract class ChangeEmployeeTransaction : ITransaction
{
    private readonly int _id;

    public ChangeEmployeeTransaction(int id)
    {
        _id = id;
    }

    public void Execute()
    {
        Employee employee = PayrollDataBase.GetEmployee(_id);
        
        if (employee != null )
            Change(employee);
        else
            throw new InvalidOperationException("No such employee.");
    }

    protected abstract void Change(Employee employee);
}