namespace Domain;

public class TimeCardTransaction: ITransaction
{
    private readonly DateTime _date;
    private readonly double _hours;
    private readonly int _employeeId;

    public TimeCardTransaction(DateTime date, double hours, int employeeId)
    {
        _date = date;
        _hours = hours;
        _employeeId = employeeId;
    }

    public void Execute()
    {
        Employee employee = PayrollDataBase.GetEmployee(_employeeId);

        if (employee != null)
        {
            if (employee.Classification is HourlyClassification hourlyClassification)
                hourlyClassification.AddTimeCard(new TimeCard(_date, _hours));
            else
                throw new InvalidOperationException($"Tried to add timecard to {employee.Name} non-hourly employee");
        }
        else
        {
            throw new InvalidOperationException("No such employee");
        }
    }
}