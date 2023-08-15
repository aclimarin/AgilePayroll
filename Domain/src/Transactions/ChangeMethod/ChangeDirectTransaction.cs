namespace Domain;

public class ChangeDirectTransaction : ChangeMethodTransaction
{
    private readonly string _bank;
    private readonly string _account;

    public ChangeDirectTransaction(int id, string bank, string account) 
        : base(id)
    {
        _bank = bank;
        _account = account;
    }

    protected override PaymentMethod Method
    {
        get { return new DirectMethod(_bank, _account); }
    }
}