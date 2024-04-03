using System;
using System.Collections.Generic;

namespace TicketingSystem
{
    enum SeatLabel { A, B, C, D }

    class Seat
    {
        public bool IsBooked { get; set; }
        public Passenger Passenger { get; set; }
        public SeatLabel Label { get; set; }

        public Seat(SeatLabel label)
        {
            Label = label;
            IsBooked = false;
            Passenger = null;
        }
    }

    class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SeatPreference Preference { get; set; }
        public Seat BookedSeat { get; set; }

        public Passenger(string firstName, string lastName, SeatPreference preference)
        {
            FirstName = firstName;
            LastName = lastName;
            Preference = preference;
            BookedSeat = null;
        }
    }

    enum SeatPreference { Window = 1, Aisle }

    class Program
    {
        static List<List<Seat>> seatingChart = new List<List<Seat>>();

        static void Main(string[] args)
        {
            InitializeSeatingChart();

            int choice = 0;
            while (choice != 3)
            {
                Console.WriteLine("Please enter 1 to book a ticket.");
                Console.WriteLine("Please enter 2 to see seating chart.");
                Console.WriteLine("Please enter 3 to exit the application.");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            BookTicket();
                            break;
                        case 2:
                            ShowSeatingChart();
                            break;
                        case 3:
                            Console.WriteLine("Exiting the application.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                }
            }
        }

        static void InitializeSeatingChart()
        {
            for (int i = 0; i < 12; i++)
            {
                List<Seat> row = new List<Seat>();
                for (int j = 0; j < 4; j++)
                {
                    row.Add(new Seat((SeatLabel)j));
                }
                seatingChart.Add(row);
            }
        }

        static void BookTicket()
        {
            Console.WriteLine("Please enter the passenger's first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter the passenger's last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter to pick the first available seat:");
            SeatPreference preference;
            if (!Enum.TryParse(Console.ReadLine(), out preference) || preference < SeatPreference.Window || preference > SeatPreference.Aisle)
            {
                preference = SeatPreference.Window; // Default preference
            }

            Passenger passenger = new Passenger(firstName, lastName, preference);

            bool seatFound = false;

            foreach (var row in seatingChart)
            {
                foreach (var seat in row)
                {
                    if (!seat.IsBooked)
                    {
                        if (preference == SeatPreference.Window && (seat.Label == SeatLabel.A || seat.Label == SeatLabel.D))
                        {
                            seat.Passenger = passenger;
                            seat.IsBooked = true;
                            passenger.BookedSeat = seat;
                            seatFound = true;
                            break;
                        }
                        else if (preference == SeatPreference.Aisle && (seat.Label == SeatLabel.B || seat.Label == SeatLabel.C))
                        {
                            seat.Passenger = passenger;
                            seat.IsBooked = true;
                            passenger.BookedSeat = seat;
                            seatFound = true;
                            break;
                        }
                        else if (preference == SeatPreference.Window && (seat.Label == SeatLabel.B || seat.Label == SeatLabel.C))
                        {
                            seat.Passenger = passenger;
                            seat.IsBooked = true;
                            passenger.BookedSeat = seat;
                            seatFound = true;
                            break;
                        }
                    }
                }
                if (seatFound)
                    break;
            }

            if (seatFound)
            {
                Console.WriteLine($"The seat located in {passenger.BookedSeat.Label} {(int)passenger.BookedSeat.Label + 1} has been booked.");
            }
            else
            {
                Console.WriteLine("Sorry, the plane is completely booked.");
            }
        }

        static void ShowSeatingChart()
        {
            foreach (var row in seatingChart)
            {
                foreach (var seat in row)
                {
                    if (seat.IsBooked)
                    {
                        Console.Write($"{seat.Passenger.FirstName[0]}{seat.Passenger.LastName[0]} ");
                    }
                    else
                    {
                        Console.Write($"{seat.Label} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

