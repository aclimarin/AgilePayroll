namespace Domain;

public class DeleteEmployeeTransaction: ITransaction
{
    private readonly int _id;

    public DeleteEmployeeTransaction(int id)
    {
        _id = id;
    }

    public void Execute()
    {
        PayrollDataBase.DeleteEmployee(_id);
    }
}