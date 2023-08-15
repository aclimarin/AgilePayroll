using Domain;

namespace Test;

public class DeleteEmployeeTest
{

    [Fact]
    public void TestDeleteSalariedEmployee()
    {
        int empId = 4;
        AddSalariedEmployee addTransaction =
            new(empId, "Bob", "Home", 1000.00);

        addTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        DeleteEmployeeTransaction deleteTransaction =
            new(empId);
        deleteTransaction.Execute();

        employee = PayrollDataBase.GetEmployee(empId);
        Assert.Null(employee);
    }

    [Fact]
    public void TestDeleteComissionedEmployee()
    {
        int empId = 4;
        AddComissionedEmployee addTransaction =
            new(empId, "Bob", "Home", 1000.00, 5, new DateTime(2023, 02, 05));

        addTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        DeleteEmployeeTransaction deleteTransaction =
            new(empId);
        deleteTransaction.Execute();

        employee = PayrollDataBase.GetEmployee(empId);
        Assert.Null(employee);
    }

    [Fact]
    public void TestDeleteHourlyEmployee()
    {
        int empId = 4;
        AddHourlyEmployee addTransaction =
            new(empId, "Bob", "Home", 80);

        addTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        DeleteEmployeeTransaction deleteTransaction =
            new(empId);
        deleteTransaction.Execute();

        employee = PayrollDataBase.GetEmployee(empId);
        Assert.Null(employee);
    }
}