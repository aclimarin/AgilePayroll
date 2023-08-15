namespace Domain;

public class ComissionedClassification : PaymentClassification
{
    private readonly IList<SalesReceipt> _salesReceipts = new List<SalesReceipt>();

    public ComissionedClassification(double salary, double commissionRate)
    {
        Salary = salary;
        ComissionRate = commissionRate;
    }

    public double Salary { get; set; }
    public double ComissionRate { get; set; }

    public override double CalculatePay(Paycheck paycheck)
    {
        double totalPay = Salary;
        foreach (var saleReceipt in _salesReceipts)
        {
            if (IsInPayPeriod(saleReceipt.Date, paycheck))
                totalPay += CalculateCommission(saleReceipt);
        }

        return totalPay;
    }

    private double CalculateCommission(SalesReceipt saleReceipt)
    {
        var commission = saleReceipt.Amount * (ComissionRate / 100);
        return commission;
    }

    public SalesReceipt GetSalesReceipt(DateTime date)
    {
        return _salesReceipts.Where(sr => sr.Date.Date == date.Date).First();
    }

    internal void AddSalesReceipt(SalesReceipt salesReceipt)
    {
        _salesReceipts.Add(salesReceipt);
    }
}