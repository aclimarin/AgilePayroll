namespace Domain;

public class TimeCard
{
    public DateTime Date { get; set; }
    public double Hours { get; set; }

    public TimeCard(DateTime date, double hours)
    {
        Date = date;
        Hours = hours;
    }
}