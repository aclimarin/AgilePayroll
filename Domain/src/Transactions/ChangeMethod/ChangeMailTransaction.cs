namespace Domain;

public class ChangeMailTransaction : ChangeMethodTransaction
{
    private readonly string _addess;

    public ChangeMailTransaction(int id, string addess) 
        : base(id)
    {
        _addess = addess;
    }

    protected override PaymentMethod Method
    {
        get { return new MailMethod(_addess); }
    }
}