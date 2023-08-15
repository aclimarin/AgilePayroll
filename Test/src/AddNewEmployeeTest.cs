using Domain;

namespace Test;

public class AddNewEmployeeTest
{
    [Fact]
    public void TestAddSalariedEmployee()
    {
        int empId = 1666;
        AddSalariedEmployee transaction =
            new(empId, "Bob", "Home", 1000.00);

        transaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.Equal("Bob", employee.Name);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.True(paymentClassification is SalariedClassification);

        SalariedClassification? salariedClassification = paymentClassification as SalariedClassification;
        Assert.Equal(1000.00, salariedClassification?.Salary);

        IPaymentSchedule paymentSchedule = employee.Schedule;
        Assert.True(paymentSchedule is MonthlySchedule);

        PaymentMethod paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is HoldMethod);
    }

    [Fact]
    public void TestAddComissionedEmployee()
    {
        int empId = 1;
        AddComissionedEmployee transaction =
            new(empId, "Bob", "Home", 1000.00, 5, new DateTime(2023, 02, 05));

        transaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.Equal("Bob", employee.Name);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.True(paymentClassification is ComissionedClassification);

        ComissionedClassification? comissionedClassification = paymentClassification as ComissionedClassification;
        Assert.Equal(1000.00, comissionedClassification?.Salary);
        Assert.Equal(5, comissionedClassification?.ComissionRate);

        IPaymentSchedule paymentSchedule = employee.Schedule;
        Assert.True(paymentSchedule is BiweeklySchedule);

        PaymentMethod paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is HoldMethod);
    }

    [Fact]
    public void TestAddHourlyEmployee()
    {
        int empId = 1;
        AddHourlyEmployee transaction =
            new(empId, "Bob", "Home", 80);

        transaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.Equal("Bob", employee.Name);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.True(paymentClassification is HourlyClassification);

        HourlyClassification? hourlyClassification = paymentClassification as HourlyClassification;
        Assert.Equal(80, hourlyClassification?.HourlyRate);

        IPaymentSchedule paymentSchedule = employee.Schedule;
        Assert.True(paymentSchedule is WeeklySchedule);

        PaymentMethod paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is HoldMethod);
    }
}