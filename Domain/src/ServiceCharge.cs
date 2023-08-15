namespace Domain;

public class ServiceCharge
{
    private DateTime _date;
    private double _charge;

    public ServiceCharge(DateTime date, double charge)
    {
        _date = date;
        _charge = charge;
    }

    public DateTime Date { get { return _date; }}
    public double Amount { get { return _charge; } }
}