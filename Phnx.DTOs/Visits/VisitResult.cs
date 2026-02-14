namespace Phnx.DTOs.Visits;

public class VisitResult(Guid clientId, DateTime visitTime, string note)
{
    public Guid ClientId => clientId;
    public DateTime VisitTime => visitTime;
    public string Note => note;
}
