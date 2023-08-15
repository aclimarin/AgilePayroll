namespace Domain;

public class Paycheck
{    
    public Paycheck(DateTime payPeriodStart, DateTime payDate)
    {
        PayPeriodEndDate = payDate;
        PayPeriodStartDate = payPeriodStart;
    }

    public Paycheck() { }

    public DateTime PayPeriodEndDate { get; set; }
    public DateTime PayPeriodStartDate { get; set; }
    public double Grosspay { get; set; }
    public double Deductions { get; set; }
    public double NetPay { get; set; }

    public Paycheck GetPaycheck(int empId)
    {
        throw new NotImplementedException();
    }

    public string GetField(string fieldName)
    {
        return "Hold";
    }
}