namespace Domain;

public class PaydayTransaction : ITransaction
{
    private readonly DateTime _payDate;
    Dictionary<int, Paycheck> _paychecks;

    public PaydayTransaction(DateTime payDate)
    {
        _payDate = payDate;
        _paychecks = new();
    }

    public void Execute()
    {
        var empIds = PayrollDataBase.GetAllEmployeeIds();

        foreach (int empId in empIds)
        {
            Employee employee = PayrollDataBase.GetEmployee(empId);

            if (employee.IsPayDate(_payDate))
            {
                DateTime startDate = employee.GetPayPeriodStartDate(_payDate);
                Paycheck paycheck = new(startDate, _payDate);
                _paychecks[empId] = paycheck;
                employee.Payday(paycheck);
            }
        }
    }

    public Paycheck GetPaycheck(int empId)
    {
        if (!_paychecks.ContainsKey(empId))
        {
            return null;
        }
        return _paychecks[empId];
    }
}