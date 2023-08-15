namespace Domain;


/// <summary>
/// A Factory Template Method to represent Concrete Commands
/// </summary>

public abstract class AddEmployeeTransacion : ITransaction
{
    private readonly int _id;
    private readonly string _name;
    private readonly string _adress;

    public AddEmployeeTransacion(int id, string name, string adress)
    {
        _id = id;
        _name = name;
        _adress = adress;
    }

    protected abstract PaymentClassification MakeClassification();
    protected abstract IPaymentSchedule MakeSchedule();

    public void Execute()
    {
        PaymentClassification paymentClassification = MakeClassification();
        IPaymentSchedule paymentSchedule = MakeSchedule();
        PaymentMethod paymentMethod = new HoldMethod();

        Employee employee = new(_id, _name, _adress)
        {
            Classification = paymentClassification,
            Schedule = paymentSchedule,
            Method = paymentMethod
        };

        PayrollDataBase.AddEmployee(_id, employee);
    }
}