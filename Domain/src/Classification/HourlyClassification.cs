namespace Domain;

public class HourlyClassification : PaymentClassification
{
    private readonly IList<TimeCard> _timeCards = new List<TimeCard>();
    public HourlyClassification(double hourlyRate)
    {
        HourlyRate = hourlyRate;
    }

    public double HourlyRate { get; set; }

    public override double CalculatePay(Paycheck paycheck)
    {
        double totalPay = 0.0;
        foreach (TimeCard timeCard in _timeCards)
        {
            if (IsInPayPeriod(timeCard.Date, paycheck))
                totalPay += CalculatePayForTimeCard(timeCard);
        }

        return totalPay;
    }

    private double CalculatePayForTimeCard(TimeCard timeCard)
    {
        double overtimeHours = Math.Max(0.0, timeCard.Hours -8);
        double normalHours = timeCard.Hours - overtimeHours;
        return HourlyRate * normalHours +
            HourlyRate * 1.5 * overtimeHours;
    }

    // private bool IsInPayPeriod(TimeCard timeCard, DateTime payPeriod)
    // {
    //     DateTime payPeriodEndDate = payPeriod;
    //     DateTime payPeriodStartDate = payPeriod.AddDays(-5);

    //     return timeCard.Date.Date <= payPeriodEndDate.Date &&
    //         timeCard.Date >= payPeriodStartDate.Date;
    // }

    public TimeCard GetTimeCard(DateTime dateTime)
    {
        return _timeCards.Where(timeCard => timeCard.Date.Date == dateTime.Date).First();
    }

    internal void AddTimeCard(TimeCard timeCard)
    {
        _timeCards.Add(timeCard);
    }
}