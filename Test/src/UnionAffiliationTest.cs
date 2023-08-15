using Domain;

namespace Test;

public class UnionAffiliationTest
{
    [Fact]
    public void TestAddServiceChage()
    {
        int empId = 55;
        AddHourlyEmployee transaction =
            new(empId, "Bob", "Home", 15.60);

        transaction.Execute();

        Employee employee = PayrollDataBase.GetEmployee(empId);
        Assert.NotNull(employee);

        UnionAffiliation unionAffiliation = new UnionAffiliation();
        employee.Affiliation = unionAffiliation;

        int memberId = 86;

        PayrollDataBase.AddUnionMember(memberId, employee);

        ServiceChargeTransaction serviceChargeTransaction =
            new(memberId, new DateTime(2023, 01, 02), 12.9);

        serviceChargeTransaction.Execute();

        ServiceCharge serviceCharge = unionAffiliation.GetServiceCharge(new DateTime(2023, 01, 02));
        Assert.NotNull(serviceCharge);
        Assert.Equal(12.9, serviceCharge.Amount);
    }

    [Fact]
    public void TestSalareiedUnionMemberDues()
    {
        int empId = 1351;
        AddSalariedEmployee transaction =
            new(empId, "Bob", "Home", 1000.00);
        transaction.Execute();

        int unionMemberId = 7764;
        ChangeMemeberTransaction changeUnionMemeber =
            new(empId, unionMemberId, 9.24);
        changeUnionMemeber.Execute();

        DateTime payDate = new(2023, 06, 30);
        PaydayTransaction paydayTransaction =
            new(payDate);
        paydayTransaction.Execute();

        Paycheck paycheck = paydayTransaction.GetPaycheck(empId);
        Assert.NotNull(paycheck);
        Assert.Equal(payDate.Date, paycheck.PayPeriodEndDate.Date);
        Assert.Equal(1000.00, paycheck.Grosspay);
        Assert.Equal("Hold", paycheck.GetField("Disposition"));        
        Assert.Equal(9.24*5, paycheck.Deductions);
        Assert.Equal(1000.00 - (9.24*5), paycheck.NetPay);
    }
}