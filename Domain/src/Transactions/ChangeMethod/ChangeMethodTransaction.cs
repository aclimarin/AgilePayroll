namespace Domain;

public abstract class ChangeMethodTransaction : ChangeEmployeeTransaction
{
    public ChangeMethodTransaction(int id) : base(id)
    {
    }

    protected override void Change(Employee employee)
    {
        employee.Method = Method;
    }

    protected abstract PaymentMethod Method { get; }

}