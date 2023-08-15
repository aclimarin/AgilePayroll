namespace Domain;

public abstract class PaymentClassification
{
    public abstract double CalculatePay(Paycheck paycheck);

    protected bool IsInPayPeriod(DateTime date, Paycheck paycheck)
    {
        DateTime payPeriodEndDate = paycheck.PayPeriodEndDate;
        DateTime payPeriodStartDate = paycheck.PayPeriodStartDate;

        return date.Date <= payPeriodEndDate.Date &&
            date.Date >= payPeriodStartDate.Date;
    }
}