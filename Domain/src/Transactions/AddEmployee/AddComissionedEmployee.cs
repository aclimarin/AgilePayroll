namespace Domain;

public class AddComissionedEmployee: AddEmployeeTransacion
{
    private readonly double _salary;
    private readonly double _commissionRate;
    private readonly DateTime _referencePaymentDate;

    public AddComissionedEmployee(int id, string name, string adress, double salary, double commissionRate, DateTime referencePaymentDate)
        : base(id, name, adress)
    {
        _salary = salary;
        _commissionRate = commissionRate;
        _referencePaymentDate = referencePaymentDate;
    }

    protected override PaymentClassification MakeClassification()
    {
        return new ComissionedClassification(_salary, _commissionRate);
    }

    protected override IPaymentSchedule MakeSchedule()
    {
        return new BiweeklySchedule(_referencePaymentDate);
    }
}