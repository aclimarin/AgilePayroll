namespace Domain;

public class AddHourlyEmployee: AddEmployeeTransacion
{
    private readonly double _hourlyRate;

    public AddHourlyEmployee(int id, string name, string adress, double hourlyRate)
        : base(id, name, adress)
    {
        _hourlyRate = hourlyRate;
    }

    protected override PaymentClassification MakeClassification()
    {
        return new HourlyClassification(_hourlyRate);
    }

    protected override IPaymentSchedule MakeSchedule()
    {
        return new WeeklySchedule();
    }
}