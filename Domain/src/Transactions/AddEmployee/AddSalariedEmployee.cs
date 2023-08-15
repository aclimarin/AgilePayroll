namespace Domain;


/// <summary>
/// A concrete command class to add salaried employee
/// </summary>

public class AddSalariedEmployee : AddEmployeeTransacion
{
    private readonly double _salary;

    public AddSalariedEmployee(int id, string name, string adress, double salary)
        : base(id, name, adress)
    {
        _salary = salary;
    }

    protected override PaymentClassification MakeClassification()
    {
        return new SalariedClassification(_salary);
    }

    protected override IPaymentSchedule MakeSchedule()
    {
        return new MonthlySchedule();
    }
}