namespace Domain;

public class MonthlySchedule : IPaymentSchedule
{
    public DateTime GetPayPeriodStartDate(DateTime payDate)
    {
        DateTime firstOfMonth = new (payDate.Year, payDate.Month, 1);
        return firstOfMonth;
    }

    public bool IsPayDate(DateTime paydate)
    {
        return IsLastDayOfMonth(paydate);
    }

    private bool IsLastDayOfMonth(DateTime date)
    {        
        var nextDay = date.AddDays(1);
        return nextDay.Day == 1;
    }
}