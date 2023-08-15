namespace Domain;

public class WeeklySchedule : IPaymentSchedule
{
    public DateTime GetPayPeriodStartDate(DateTime payDate)
    {
        DateTime monday = payDate.AddDays(-5);;
        return monday;
    }

    public bool IsPayDate(DateTime paydate)
    {
         return IsFryday(paydate);
    }

    private bool IsFryday(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Friday;
    }
}