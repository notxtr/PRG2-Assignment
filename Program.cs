//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

using PRG2REAL_assignment;

List<Flight> FlightList = new List<Flight>();
List<BoardingGate> BoardingGateList = new List<BoardingGate>();
List<Airline> AirlineList = new List<Airline>();
void DisplayMenu()
{
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit");
}

// Method to create Airline objects
void CreateAirlineObject(List<Airline> airlineList)
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string line;

        sr.ReadLine(); // Skip the first line
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string airlinename = parts[0];
            string airlinecode = parts[1];


            airlineList.Add(new Airline(airlinename, airlinecode));
        }
    }
}

// Method to create Boarding Gate objects
void CreateBoardingGateObject(List<BoardingGate> boardingGateList)
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string line;
        sr.ReadLine(); // Skip the header

        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string boardinggate = parts[0];
            bool ddjb = Convert.ToBoolean(parts[1]);
            bool cfft = Convert.ToBoolean(parts[2]);
            bool lwtt = Convert.ToBoolean(parts[3]);

            // Assuming `FlightList` is already populated by CreateFlightObject
            foreach (var flight in FlightList)
            {
                if ((flight is DDJBFlight && ddjb) ||
                    (flight is CFFTFlight && cfft) ||
                    (flight is LWTTFlight && lwtt) ||
                    (flight is NORMFlight))
                {
                    boardingGateList.Add(new BoardingGate(boardinggate, ddjb, cfft, lwtt, flight));
                }
            }
        }
    }
}

// Method to create Flight objects
void CreateFlightObject(List<Flight> flightList)
{
    using (StreamReader str = new StreamReader("flights.csv"))
    {
        string line;
        str.ReadLine(); // Skip the header

        while ((line = str.ReadLine()) != null)
        {
            string[] flightparts = line.Split(',');
            string flightnumber = flightparts[0];
            string origin = flightparts[1];
            string destination = flightparts[2];
            DateTime estimatedtime = Convert.ToDateTime(flightparts[3]);
            string status = flightparts[4];

            // Create the appropriate flight type based on status
            Flight flight = status switch
            {
                "DDJB" => new DDJBFlight(flightnumber, origin, destination, estimatedtime, status),
                "" => new CFFTFlight(flightnumber, origin, destination, estimatedtime, status),
                "LWTT" => new LWTTFlight(flightnumber, origin, destination, estimatedtime, status),
                _ => new NORMFlight(flightnumber, origin, destination, estimatedtime, status)
            };

            flightList.Add(flight);
        }
    }
}



bool trueornot = true;
while (trueornot == true)
{
    DisplayMenu();
    CreateAirlineObject(AirlineList);
    CreateBoardingGateObject(BoardingGateList);
    CreateFlightObject(FlightList);
    Console.WriteLine("Please select your option: ");
    int option = Convert.ToInt32(Console.ReadLine());
    switch (option)
    {
        case 0:
            trueornot = false;
            Console.WriteLine("Goodbye!");  
            break;

        case 1:
            break;
        case 2:
            break;
        case 3:
            break;
        case 4:
            break;
        case 5:
            break;
        case 6:
            break;
        case 7:
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;

    }
}

