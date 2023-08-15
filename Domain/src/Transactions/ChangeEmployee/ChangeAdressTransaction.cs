namespace Domain;

public class ChangeAdressTransaction : ChangeEmployeeTransaction
{
    private readonly string _newAdress;

    public ChangeAdressTransaction(int id, string newAdress)
        : base(id)
    {
        _newAdress = newAdress;
    }

    protected override void Change(Employee employee)
    {
        employee.Adress = _newAdress;
    }
}