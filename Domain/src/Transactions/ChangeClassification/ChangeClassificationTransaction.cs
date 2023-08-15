namespace Domain;

public abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
{
    public ChangeClassificationTransaction(int id) : base(id)
    {
    }

    protected override void Change(Employee employee)
    {
        employee.Classification = Classification;
        employee.Schedule = Schedule;
    }

    protected abstract PaymentClassification Classification { get; }
    protected abstract IPaymentSchedule Schedule { get; }
}