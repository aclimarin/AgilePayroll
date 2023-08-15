namespace Domain;

public class SalesReceiptTransaction: ITransaction
{
    private readonly DateTime _date;
    private readonly double _amount;
    private readonly int _employeeId;

    public SalesReceiptTransaction(DateTime date, int amount, int employeeId)
    {
        _date = date;
        _amount = amount;
        _employeeId = employeeId;
    }

    public void Execute()
    {
        Employee employee = PayrollDataBase.GetEmployee(_employeeId);

        if (employee != null)
        {
            if (employee.Classification is ComissionedClassification comissionedClassification)
                comissionedClassification.AddSalesReceipt(new SalesReceipt(_date, _amount));
            else
                throw new InvalidOperationException($"Tried to add sales receipt to {employee.Name} non-commisioned employee");
        }
        else
        {
            throw new InvalidOperationException("No such employee");
        }
    }
}