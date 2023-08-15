namespace Domain;

public class ChangeSalariedTransaction : ChangeClassificationTransaction
{
    private readonly double _salary;

    public ChangeSalariedTransaction(int id, double salary)
        : base(id)
    {
        _salary = salary;
    }

    protected override PaymentClassification Classification
    {
        get { return new SalariedClassification(_salary); }
    }

    protected override IPaymentSchedule Schedule
    {
        get { return new MonthlySchedule(); }
    }
}