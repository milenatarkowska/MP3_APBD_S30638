using MP3_APBD_S30638.Models;
namespace MP3_APBD_S30638.Data;

public class ExampleData
{
    public static List<Room> Rooms = new()
    {
        new Room(1, "Sala Konferencyjna A", "A", 1, 50, true, true),
        new Room(2, "Sala konferencyjna B", "B", 0, 100, true, true),
        new Room(3, "Labolatorium A", "A", 2, 15, false, true),
        new Room(4, "Sala Konferencyjna C", "C", 0, 150, true, true),
        new Room(5, "Labolatorium B", "B", 1, 10, false, false),
        new Room(6, "Mała Sala Wykładowa H", "H", 4, 30, true, false)
    };

    public static List<Reservation> Reservations = new()
    {
        new Reservation(
            1, 1, "Jan Kowalski", "Wprowadzenie do programowania w C#", 
            new DateOnly(2026, 5, 10), new TimeOnly(9, 0), new TimeOnly(11, 0), Status.Confirmed
        ),
        new Reservation(
            2, 1, "Marek Nowak", "Zaawansowane API", 
            new DateOnly(2026, 5, 10), new TimeOnly(12, 0), new TimeOnly(14, 0), Status.Planned
        ),
        new Reservation(
            3, 2, "Anna Sowa", "Ćwiczenia Python", 
            new DateOnly(2026, 5, 11), new TimeOnly(10, 0), new TimeOnly(15, 0), Status.Confirmed
        ),
        new Reservation(
            4, 3, "Piotr Zieliński", "Spotkanie Zarządu", 
            new DateOnly(2026, 5, 12), new TimeOnly(8, 30), new TimeOnly(9, 30), Status.Cancelled
        ),
        new Reservation(
            5, 4, "Zofia Szajko", "Konferencja administratorów Jira", 
            new DateOnly(2026, 6, 1), new TimeOnly(18, 0), new TimeOnly(20, 0), Status.Planned
        )
    };
}