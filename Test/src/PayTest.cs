
using Domain;

namespace Test;

public class PayTest
{
    [Fact]
    public void TestPaySingleSalariedEmployee()
    {
        int empId = 12;
        AddSalariedEmployee addSalariedEmployee =
            new(empId, "Bob", "Home", 1000.00);

        addSalariedEmployee.Execute();

        DateTime payDate = new(2023, 11, 30);
        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        Paycheck paycheck = paydayTransaction.GetPaycheck(empId);
        Assert.NotNull(paycheck);
        Assert.Equal(1000.00, paycheck.Grosspay);
        Assert.Equal("Hold", paycheck.GetField("Disposition"));
        Assert.Equal(0.0, paycheck.Deductions);
        Assert.Equal(1000.00, paycheck.NetPay);
    }

    [Fact]
    public void TestPaySingleSalariedEmployeeWrongDate()
    {
        int empId = 12;
        AddSalariedEmployee addSalariedEmployee =
            new(empId, "Bob", "Home", 1000.00);

        addSalariedEmployee.Execute();

        DateTime payDate = new(2023, 11, 29);
        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        Paycheck paycheck = paydayTransaction.GetPaycheck(empId);
        Assert.Null(paycheck);
    }

    [Fact]
    public void TestPaySingleHourlyEmployeeNoTimeCard()
    {
        int empId = 14;
        AddHourlyEmployee addHourlyEmployeetransaction =
            new(empId, "Bob", "Home", 80);

        addHourlyEmployeetransaction.Execute();

        DateTime payDate = new(2023, 08, 4);
        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 0);
    }

    [Fact]
    public void TestPaySingleHourlyEmployeeOneTimeCard()
    {
        int empId = 14;
        AddHourlyEmployee addHourlyEmployeetransaction =
            new(empId, "Bob", "Home", 15.25);

        addHourlyEmployeetransaction.Execute();

        DateTime payDate = new(2023, 08, 4);
        TimeCardTransaction timeCardTransaction =
            new(new DateTime(2023, 08, 03), 2, empId);
        timeCardTransaction.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);


        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 30.5);

    }

    [Fact]
    public void TestPaySingleHourlyEmployeeTwoTimeCard()
    {
        int empId = 14;
        AddHourlyEmployee addHourlyEmployeetransaction =
            new(empId, "Bob", "Home", 15.25);

        addHourlyEmployeetransaction.Execute();

        DateTime payDate = new(2023, 08, 4);
        TimeCardTransaction timeCardTransaction =
            new(new DateTime(2023, 08, 03), 2, empId);
        timeCardTransaction.Execute();

        timeCardTransaction =
            new(new DateTime(2023, 08, 04), 8, empId);
        timeCardTransaction.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 152.5);
    }

    [Fact]
    public void TestPaySingleHourlyEmployeeOverTimeCard()
    {
        int empId = 14;
        AddHourlyEmployee addHourlyEmployeetransaction =
            new(empId, "Bob", "Home", 15.25);

        addHourlyEmployeetransaction.Execute();

        DateTime payDate = new(2023, 08, 4);
        TimeCardTransaction timeCardTransaction =
            new(new DateTime(2023, 08, 03), 9, empId);
        timeCardTransaction.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 144.875);
    }

    [Fact]
    public void TestPaySingleHourlyEmployeeWrongDate()
    {
        int empId = 14;
        AddHourlyEmployee addHourlyEmployeetransaction =
            new(empId, "Bob", "Home", 15.25);

        addHourlyEmployeetransaction.Execute();

        DateTime payDate = new(2023, 08, 3);
        TimeCardTransaction timeCardTransaction =
            new(new DateTime(2023, 08, 03), 9, empId);
        timeCardTransaction.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        Paycheck paycheck = paydayTransaction.GetPaycheck(empId);
        Assert.Null(paycheck);
    }

    [Fact]
    public void TestPaySingleHourlyEmployeeTimeCardsTwoPeriods()
    {
        int empId = 546;
        AddHourlyEmployee addHourlyEmployeetransaction =
            new(empId, "Bob", "Home", 15.25);

        addHourlyEmployeetransaction.Execute();

        DateTime payDate = new(2023, 08, 4);
        TimeCardTransaction timeCardTransaction =
            new(new DateTime(2023, 08, 03), 2, empId);
        timeCardTransaction.Execute();

        DateTime datePreviousPayPeriod = new(2023, 07, 27);
        timeCardTransaction =
            new(datePreviousPayPeriod, 5, empId);
        timeCardTransaction.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 30.5);
    }

    [Fact]
    public void TestPayComissionedEmployee()
    {
        int empId = 546;
        AddComissionedEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 1500, 7, new DateTime(2023, 07, 21));
        addEmployeeTransaction.Execute();

        DateTime payDate = new DateTime(2023, 08, 04);

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 1500);
    }

    [Fact]
    public void TestPayComissionedEmployeeWithOneSale()
    {
        int empId = 54546;
        AddComissionedEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 1500, 7, new DateTime(2023, 07, 21));
        addEmployeeTransaction.Execute();

        DateTime payDate = new (2023, 08, 04);
        SalesReceiptTransaction salesTransacion =
            new(payDate, 8000, empId);
        salesTransacion.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 2060);
    }

    [Fact]
    public void TestPayComissionedEmployeeWithTwoSales()
    {
         int empId = 546;
        AddComissionedEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 1500, 7, new DateTime(2023, 07, 21));
        addEmployeeTransaction.Execute();

        DateTime payDate = new (2023, 08, 04);
        SalesReceiptTransaction salesTransacion =
            new(payDate, 8000, empId);
        salesTransacion.Execute();

        salesTransacion =
            new(payDate, 1000, empId);
        salesTransacion.Execute();

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();

        ValidatePayCheck(paydayTransaction, empId, payDate, 2130);
    }

    [Fact]
    public void TestPayComissionedEmployeeWrongDate()
    {
        int empId = 546;
        AddComissionedEmployee addEmployeeTransaction =
            new(empId, "Bob", "Home", 1500, 7, new DateTime(2023, 08, 03));
        addEmployeeTransaction.Execute();

        DateTime payDate = new (2023, 08, 04);

        PaydayTransaction paydayTransaction =
            new(payDate);

        paydayTransaction.Execute();Paycheck paycheck = paydayTransaction.GetPaycheck(empId);
        Assert.Null(paycheck);
    }
    
    private void ValidatePayCheck(PaydayTransaction paydayTransaction, int empId, DateTime payDate, double pay)
    {
        Paycheck paycheck = paydayTransaction.GetPaycheck(empId);
        Assert.NotNull(paycheck);
        Assert.Equal(payDate, paycheck.PayPeriodEndDate);
        Assert.Equal(pay, paycheck.Grosspay);
        Assert.Equal("Hold", paycheck.GetField("Disposition"));
        Assert.Equal(0.0, paycheck.Deductions);
        Assert.Equal(pay, paycheck.NetPay);
    }
}