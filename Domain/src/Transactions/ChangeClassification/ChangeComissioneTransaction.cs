namespace Domain;

public class ChangeComissioneTransaction : ChangeClassificationTransaction
{
    private readonly double _salary;
    private readonly double _commissionRate;
    private readonly DateTime _referencePaymentDate;

    public ChangeComissioneTransaction(int id, double salary, double commissionRate, DateTime referencePaymentDate)
        : base(id)
    {
        _salary = salary;
        _commissionRate = commissionRate;
        _referencePaymentDate = referencePaymentDate;
    }

    protected override PaymentClassification Classification
    {
        get { return new ComissionedClassification(_salary, _commissionRate); }
    }

    protected override IPaymentSchedule Schedule
    {
        get { return new BiweeklySchedule(_referencePaymentDate); }
    }
}