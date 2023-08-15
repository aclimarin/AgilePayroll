using Domain;

namespace Test;

public class TimeCardAndSalesReceiptTest
{
    [Fact]
    public void TestTimeCardTransaction()
    {
        int empId = 8484;
        AddHourlyEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 80);
        addEmployeeTransaction.Execute();

        TimeCardTransaction timeCardTransaction =
            new(new DateTime(2023, 08, 03), 8, empId);
        timeCardTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.True(paymentClassification is HourlyClassification);

        HourlyClassification? hourlyClassification = paymentClassification as HourlyClassification;
        Assert.Equal(80, hourlyClassification?.HourlyRate);

        TimeCard? timeCard = hourlyClassification?.GetTimeCard(new DateTime(2023, 08, 03));
        Assert.NotNull(timeCard);
        Assert.Equal(8, timeCard.Hours);
    }

    [Fact]
    public void TestSalesReceiptTransaction()
    {
        int empId = 7;
        AddComissionedEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 1500, 7, new DateTime(2023, 02, 05));
        addEmployeeTransaction.Execute();

        SalesReceiptTransaction salesTransacion =
            new(new DateTime(2023, 08, 04), 8000, empId);
        salesTransacion.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.True(paymentClassification is ComissionedClassification);

        ComissionedClassification? comissionedClassification = paymentClassification as ComissionedClassification;
        Assert.Equal(1500, comissionedClassification?.Salary);
        Assert.Equal(7, comissionedClassification?.ComissionRate);


        SalesReceipt? salesReceipt = comissionedClassification?.GetSalesReceipt(new DateTime(2023, 08, 04));
        Assert.NotNull(salesReceipt);
        Assert.Equal(8000, salesReceipt.Amount);
    }

     [Fact]
    public void TestTwoSalesReceiptTransaction()
    {
        int empId = 7;
        AddComissionedEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 1500, 7, new DateTime(2023, 02, 05));
        addEmployeeTransaction.Execute();

        SalesReceiptTransaction salesTransacion =
            new(new DateTime(2023, 08, 04), 8000, empId);
        salesTransacion.Execute();

        salesTransacion =
            new(new DateTime(2023, 08, 03), 1000, empId);
        salesTransacion.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.True(paymentClassification is ComissionedClassification);

        ComissionedClassification? comissionedClassification = paymentClassification as ComissionedClassification;
        Assert.Equal(1500, comissionedClassification?.Salary);
        Assert.Equal(7, comissionedClassification?.ComissionRate);


        SalesReceipt? salesReceipt1 = comissionedClassification?.GetSalesReceipt(new DateTime(2023, 08, 04));
        Assert.NotNull(salesReceipt1);
        Assert.Equal(8000, salesReceipt1.Amount);
        SalesReceipt? salesReceipt2 = comissionedClassification?.GetSalesReceipt(new DateTime(2023, 08, 03));
        Assert.NotNull(salesReceipt2);
        Assert.Equal(1000, salesReceipt2.Amount);
    }
}