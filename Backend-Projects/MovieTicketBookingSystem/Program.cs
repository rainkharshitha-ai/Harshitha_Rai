using MOVIETICKETBOOKINGSYSTEM.Exceptions;
using MOVIETICKETBOOKINGSYSTEM.Interfaces;
using MOVIETICKETBOOKINGSYSTEM.Models;
using MOVIETICKETBOOKINGSYSTEM.Services;

namespace MOVIETICKETBOOKINGSYSTEM;

public class Program
{
static List<Movie> movies = new();
static List<Theater> theaters = new();
static List<Show> shows = new();
static List<Customer> customers = new();

static FileService fileService = new();

public static void Main()
{
    LoadMovies();
    LoadTheaters();
    LoadShows();
    LoadCustomers();

    while (true)
    {
        Console.WriteLine("\n================================");
        Console.WriteLine("MOVIE TICKET BOOKING SYSTEM");
        Console.WriteLine("================================");
        Console.WriteLine("1. View Movies");
        Console.WriteLine("2. View Customers");
        Console.WriteLine("3. Book Ticket");
        Console.WriteLine("4. View Booking History");
        Console.WriteLine("5. Exit");

        Console.Write("Choose Option : ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)

        {
            case 1:
                ViewMovies();
                break;

            case 2:
                ViewCustomers();
                break;


            case 3:
                BookTicket();
                break;
            case 4:
                fileService.DisplayBookingHistory();
                break;

               
            case 5:
                Console.WriteLine("Application Closed");
                return;

            default:
                Console.WriteLine("Invalid Option");
                break;
        }
    }
}

static void LoadMovies()
{
    string path = Path.Combine("Data", "movies.txt");

    foreach (string line in File.ReadAllLines(path))
    {
        string[] data = line.Split('|');

        movies.Add(
            new Movie(
                Convert.ToInt32(data[0]),
                data[1],
                data[2],
                Convert.ToInt32(data[3]),
                Convert.ToDecimal(data[4])));
    }
}

static void LoadTheaters()
{
    string path = Path.Combine("Data", "theaters.txt");

    foreach (string line in File.ReadAllLines(path))
    {
        string[] data = line.Split('|');

        theaters.Add(
            new Theater(
                Convert.ToInt32(data[0]),
                data[1],
                data[2]));
    }
}

static void LoadCustomers()
{
    string path = Path.Combine("Data", "customers.txt");

    foreach (string line in File.ReadAllLines(path))
    {
        string[] data = line.Split('|');

        customers.Add(
            new Customer(
                Convert.ToInt32(data[0]),
                data[1],
                Convert.ToDecimal(data[2])));
    }
}

static void LoadShows()
{
    string path = Path.Combine("Data", "shows.txt");

    foreach (string line in File.ReadAllLines(path))
    {
        string[] data = line.Split('|');

        int movieId = Convert.ToInt32(data[1]);
        int theaterId = Convert.ToInt32(data[2]);

        Movie movie =
            movies.First(m => m.Id == movieId);

        Theater theater =
            theaters.First(t => t.Id == theaterId);

        shows.Add(
            new Show(
                Convert.ToInt32(data[0]),
                movie,
                theater,
                data[3]));
    }
}

static void ViewMovies()
{
    Console.WriteLine("\nMovies");

    foreach (var movie in movies)
    {
        Console.WriteLine(
            $"{movie.Id} | {movie.Title} | {movie.Language} | {movie.Duration} | ₹{movie.TicketPrice}");
    }
}

static void ViewCustomers()
{
    Console.WriteLine("\nCustomers");

    foreach (var customer in customers)
    {
        Console.WriteLine(
            $"{customer.Id} | {customer.Name} | ₹{customer.WalletBalance}");
    }
}

static void BookTicket()
{
    try
    {
        ViewCustomers();

        Console.Write("\nEnter Customer Id : ");
        int customerId =
            Convert.ToInt32(Console.ReadLine());

        Customer customer =
            customers.FirstOrDefault(
                c => c.Id == customerId)
            ?? throw new BookingException(
                "Invalid Customer");

        ViewMovies();

        Console.Write("\nEnter Movie Id : ");
        int movieId =
            Convert.ToInt32(Console.ReadLine());

        Movie movie =
            movies.FirstOrDefault(
                m => m.Id == movieId)
            ?? throw new BookingException(
                "Invalid Movie");

        Console.WriteLine("\nAvailable Shows");

        var movieShows =
            shows.Where(
                s => s.Movie.Id == movieId)
            .ToList();

        foreach (var show in movieShows)
        {
            Console.WriteLine(
                $"{show.Id} | {show.ShowTime}");
        }

        Console.Write("\nEnter Show Id : ");
        int showId =
            Convert.ToInt32(Console.ReadLine());

        Show selectedShow =
            movieShows.FirstOrDefault(
                s => s.Id == showId)
            ?? throw new BookingException(
                "Invalid Show");

        Console.Write(
            "\nEnter Ticket Count : ");

        int ticketCount =
            Convert.ToInt32(Console.ReadLine());

        if (ticketCount <= 0)
        {
            throw new BookingException(
                "Invalid Ticket Count");
        }

        decimal amount =
            movie.TicketPrice *
            ticketCount;

        Console.WriteLine(
            $"\nAmount : ₹{amount}");

        Console.WriteLine(
            $"Balance : ₹{customer.WalletBalance}");

        Console.WriteLine("\n1. UPI");
        Console.WriteLine("2. Card");

        int paymentChoice =
            Convert.ToInt32(Console.ReadLine());

        IPaymentService paymentService =
            paymentChoice == 1
            ? new UpiPaymentService()
            : new CardPaymentService();

        INotificationService
            notificationService =
            new EmailNotificationService();

        Booking booking =
            new Booking(
                new Random().Next(1000, 9999),
                customer,
                selectedShow,
                ticketCount,
                amount);

        BookingService bookingService =
            new BookingService(
                paymentService,
                notificationService,
                fileService);

        bookingService.BookTicket(
            booking);
    }
    catch (BookingException ex)
    {
        Console.WriteLine(
            $"Booking Error : {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"System Error : {ex.Message}");
    }
}

}
