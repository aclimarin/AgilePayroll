namespace Domain;

public class SalesReceipt
{
    public SalesReceipt(DateTime date, double amount)
    {
        Date = date;
        Amount = amount;
    }

    public double Amount { get; set; }
    public DateTime Date { get; set; }
}