namespace Domain;

public class DirectMethod: PaymentMethod
{
    public DirectMethod(string bank, string account)
    {
        Bank = bank;
        Account = account;
    }

    public string Bank { get; set; }
    public string Account { get; set; }
}