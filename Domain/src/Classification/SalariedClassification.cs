namespace Domain;

public class SalariedClassification : PaymentClassification
{
    public SalariedClassification(double salary)
    {
        Salary = salary;
    }
    
    public double Salary { get; set; }

    public override double CalculatePay(Paycheck paycheck)
    {
        return Salary;
    }
}