namespace Domain;

public class BiweeklySchedule : IPaymentSchedule
{
    private readonly DateTime _referenceDate;

    public BiweeklySchedule(DateTime referenceDate)
    {
        _referenceDate = referenceDate;
    }

    public DateTime GetPayPeriodStartDate(DateTime payDate)
    {
        DateTime payPeriodStartDate = payDate;
        payPeriodStartDate.AddDays(-13);
        return payPeriodStartDate;
    }

    public bool IsPayDate(DateTime paydate)
    {
        if (paydate.DayOfWeek != DayOfWeek.Friday)
            return false;

        return PastTwoWeeks(paydate);
    }

    public bool PastTwoWeeks(DateTime date)
    {
        var weeks = (date.Date - _referenceDate.Date).TotalDays / 7;
        if (weeks % 2 == 0)
            return true;

        return false;
    }
}