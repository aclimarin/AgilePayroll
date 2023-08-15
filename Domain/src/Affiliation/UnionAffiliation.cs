namespace Domain;

public class UnionAffiliation : Affiliation
{
    private List<ServiceCharge> _charges = new List<ServiceCharge>();
    public double Dues { get; set; }
    public int MemberId { get; set; }

    public UnionAffiliation()
    {
        
    }

    public UnionAffiliation(int memberId, double dues )
    {
        MemberId = memberId;
        Dues = dues;
    }

    public void AddServiceCharge(ServiceCharge serviceCharge)
    {
        _charges.Add(serviceCharge);
    }

    public ServiceCharge GetServiceCharge(DateTime dateTime)
    {
        return _charges.Where(c => c.Date.Date == dateTime.Date).First();
    }

    public double CalculateDeductions(Paycheck paycheck)
    {
        int fridays = NumberOfFridaysInPayPeriod(
            paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);

        double totalDues = Dues * fridays;
        return totalDues;
    }

    private int NumberOfFridaysInPayPeriod(DateTime payPeriodStartDate, DateTime payPeriodEndDate)
    {
        int fridays = 0;
        for (DateTime day = payPeriodStartDate;
            day.Date <= payPeriodEndDate.Date;
            day = day.AddDays(1))
        {
            if (day.DayOfWeek == DayOfWeek.Friday)
                fridays++;
        }

        return fridays;
    }
}