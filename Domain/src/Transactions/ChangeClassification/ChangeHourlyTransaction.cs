namespace Domain;

public class ChangeHourlyTransaction : ChangeClassificationTransaction
{

    private readonly double _hourlyRate;

    public ChangeHourlyTransaction(int id, double hourlyRate)
        : base(id)
    {
        _hourlyRate = hourlyRate;
    }

    protected override PaymentClassification Classification
    {
        get { return new HourlyClassification(_hourlyRate); }
    }

    protected override IPaymentSchedule Schedule
    {
        get { return new WeeklySchedule(); }
    }
}