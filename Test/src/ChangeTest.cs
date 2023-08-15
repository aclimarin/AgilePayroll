using Domain;

namespace Test;

public class ChangeTest
{
    [Fact]
    public void TestChangeNameTransaction()
    {
        int empId = 1;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);

        addHourlyTransaction.Execute();

        ChangeNameTransaction changeNameTransaction =
            new(empId, "Bob");

        changeNameTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);
        Assert.Equal("Bob", employee.Name);
    }

    [Fact]
    public void TestChangeAdressTransaction()
    {
        int empId = 19846;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);

        addHourlyTransaction.Execute();

        ChangeAdressTransaction changeAdressTransaction =
            new(empId, "The beech");

        changeAdressTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);
        Assert.Equal("The beech", employee.Adress);
    }

    [Fact]
    public void TestChangeHourlyTransaction()
    {
        int empId = 15;
        AddComissionedEmployee addComissionedTransacion =
            new(empId, "Bob", "Home", 1000.00, 5, new DateTime(2023, 02, 05));

        addComissionedTransacion.Execute();

        ChangeHourlyTransaction changeHourlyTransaction =
            new(empId, 27.52);

        changeHourlyTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.NotNull(paymentClassification);
        Assert.True(paymentClassification is HourlyClassification);

        HourlyClassification? hourlyClassification = paymentClassification as HourlyClassification;
        Assert.Equal(27.52, hourlyClassification?.HourlyRate);

        IPaymentSchedule paymentSchedule = employee.Schedule;
        Assert.True(paymentSchedule is WeeklySchedule);
    }

    [Fact]
    public void TestChangeComissionedTransaction()
    {
        int empId = 1;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);
        addHourlyTransaction.Execute();

        ChangeComissioneTransaction changeComissionedTransaction =
            new(empId, 1000.00, 5, new DateTime(2023, 02, 06));

        changeComissionedTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.NotNull(paymentClassification);
        Assert.True(paymentClassification is ComissionedClassification);

        ComissionedClassification? comissionedClassification = paymentClassification as ComissionedClassification;
        Assert.Equal(1000.00, comissionedClassification?.Salary);
        Assert.Equal(5, comissionedClassification?.ComissionRate);

        IPaymentSchedule paymentSchedule = employee.Schedule;
        Assert.True(paymentSchedule is BiweeklySchedule);
    }

    [Fact]
    public void TestChangeSalariedTransaction()
    {
        int empId = 1;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);
        addHourlyTransaction.Execute();

        ChangeSalariedTransaction changeSalariedTransaction =
            new(empId, 1000.00);

        changeSalariedTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentClassification paymentClassification = employee.Classification;
        Assert.NotNull(paymentClassification);
        Assert.True(paymentClassification is SalariedClassification);

        SalariedClassification? salariedClassification = paymentClassification as SalariedClassification;
        Assert.Equal(1000.00, salariedClassification?.Salary);

        IPaymentSchedule paymentSchedule = employee.Schedule;
        Assert.True(paymentSchedule is MonthlySchedule);
    }

    [Fact]
    public void TestChangeDirectTransaction()
    {
        int empId = 1;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);
        addHourlyTransaction.Execute();

        ChangeDirectTransaction changeDirectTransaction =
            new(empId, "Bank of Brazil", "1002-5");

        changeDirectTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentMethod paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is DirectMethod);

        DirectMethod? directMethod = paymentoMethod as DirectMethod;
        Assert.Equal("Bank of Brazil", directMethod?.Bank);
        Assert.Equal("1002-5", directMethod?.Account);
    }

    [Fact]
    public void TestChangeMailTransaction()
    {
        int empId = 1;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);
        addHourlyTransaction.Execute();

        ChangeMailTransaction changeMailTransaction =
            new(empId, "payment adress");

        changeMailTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentMethod paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is MailMethod);

        MailMethod? mailMethod = paymentoMethod as MailMethod;
        Assert.Equal("payment adress", mailMethod?.Address);
    }

    [Fact]
    public void TestChangeHoldTransaction()
    {
        int empId = 1;
        AddHourlyEmployee addHourlyTransaction =
            new(empId, "Bill", "Home", 80);
        addHourlyTransaction.Execute();

        ChangeMailTransaction changeMailTransaction =
            new(empId, "payment adress");

        changeMailTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        PaymentMethod paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is MailMethod);

        ChangeHoldTransaction changeHoldTransaction =
            new(empId);

        changeHoldTransaction.Execute();

        paymentoMethod = employee.Method;
        Assert.True(paymentoMethod is HoldMethod);
    }

    [Fact]
    public void TestChangeUnionMemeber()
    {
        int empId = 8;
        AddHourlyEmployee transaction =
            new(empId, "Bob", "Home", 15.60);

        transaction.Execute();

        int memberId = 102;

        ChangeMemeberTransaction changeMemeberTransaction =
            new(empId, memberId, 88.78);

        changeMemeberTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        Affiliation affiliation = employee.Affiliation;
        Assert.NotNull(affiliation);
        Assert.True(affiliation is UnionAffiliation);

        UnionAffiliation? unionAffiliation = employee.Affiliation as UnionAffiliation;
        Assert.Equal(88.78, unionAffiliation?.Dues);

        Employee member = PayrollDataBase.GetUnionMember(memberId);
        Assert.NotNull(member);
        Assert.Equal(member, employee);
    }

    [Fact]
    public void TestChangeUnaffiliation()
    {
        int empId = 8;
        AddHourlyEmployee transaction =
            new(empId, "Bob", "Home", 15.60);

        transaction.Execute();

        int memberId = 102;

        ChangeMemeberTransaction changeMemeberTransaction =
            new(empId, memberId, 88.78);

        changeMemeberTransaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        Affiliation affiliation = employee.Affiliation;
        Assert.NotNull(affiliation);
        Assert.True(affiliation is UnionAffiliation);

        UnionAffiliation? unionAffiliation = employee.Affiliation as UnionAffiliation;
        Assert.Equal(88.78, unionAffiliation?.Dues);

        Employee member = PayrollDataBase.GetUnionMember(memberId);
        Assert.NotNull(member);
        Assert.Equal(member, employee);

        ChangeUnaffiliatedTransaction changeUnaffiliationTransaction =
            new(empId);
        changeUnaffiliationTransaction.Execute();

        member = PayrollDataBase.GetUnionMember(memberId);
        Assert.Null(member);
    }
}