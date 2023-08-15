namespace Domain;

public class Employee
{
    public Employee(int empId, string name, string adress)
    {
        Id = empId;
        Name = name;
        Adress = adress;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public PaymentClassification Classification { get; set; }
    public IPaymentSchedule Schedule { get; set; }
    public PaymentMethod Method { get; set; }

    private Affiliation affiliation;
    public Affiliation Affiliation
    {
        get { return affiliation ?? new NoAffiliation(); }
        set { affiliation = value; }
    }
    

    internal bool IsPayDate(DateTime payDate)
    {
        return Schedule.IsPayDate(payDate);
    }

    internal void Payday(Paycheck paycheck)
    {
        double grossPay = Classification.CalculatePay(paycheck);
        double deductions = Affiliation.CalculateDeductions(paycheck);
        double netPay = grossPay - deductions;

        paycheck.Grosspay = grossPay;
        paycheck.Deductions = deductions;
        paycheck.NetPay = netPay;

        Method.Pay(paycheck);
    }

    internal DateTime GetPayPeriodStartDate(DateTime date)
    {
        return Schedule.GetPayPeriodStartDate(date);
    }
}