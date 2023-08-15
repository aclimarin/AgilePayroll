namespace Domain;

public class ChangeHoldTransaction : ChangeMethodTransaction
{

    public ChangeHoldTransaction(int id) 
        : base(id)
    {
    }

    protected override PaymentMethod Method
    {
        get { return new HoldMethod(); }
    }
}