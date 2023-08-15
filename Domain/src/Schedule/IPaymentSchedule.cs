namespace Domain;

public interface IPaymentSchedule
{
    bool IsPayDate(DateTime paydate);
    DateTime GetPayPeriodStartDate(DateTime payDate);
}