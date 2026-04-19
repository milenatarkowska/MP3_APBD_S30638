namespace MP3_APBD_S30638.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; }
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Status Status { get; set; }

    public Reservation(int id, int roomId, string organizerName, string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, Status status)
    {
        Id = id;
        RoomId = roomId;
        OrganizerName = organizerName;
        Topic = topic;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
    }
}