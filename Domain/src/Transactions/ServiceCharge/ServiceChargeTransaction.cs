namespace Domain;

public class ServiceChargeTransaction : ITransaction
{
    private readonly int _memberId;
    private readonly DateTime _date;
    private readonly double _charge;

    public ServiceChargeTransaction(int memberId, DateTime date, double charge)
    {
        _memberId = memberId;
        _date = date;
        _charge = charge;
    }

    public void Execute()
    {
        Employee employee = PayrollDataBase.GetUnionMember(_memberId);

        if (employee != null)
        {
            UnionAffiliation? unionAffiliation = null;
            if (employee.Affiliation is UnionAffiliation)
                unionAffiliation = employee.Affiliation as UnionAffiliation;

            if (unionAffiliation != null)
                unionAffiliation.AddServiceCharge(new ServiceCharge(_date, _charge));
            else
                throw new InvalidOperationException($"Tried to add service charge union member without a union affiliation");
        }
        else
        {
            throw new InvalidOperationException("No such union member");
        }

    }
}