//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

using PRG2REAL_assignment;

void DisplayMenu()
{
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit");
}

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

void CreateBoardingGateObject(List<BoardingGate> boardingGateList, List<Flight> flightList)
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string line;

        sr.ReadLine(); // Skip the first line
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string boardinggate = parts[0];
            bool ddjb = Convert.ToBoolean(parts[1]);
            bool cfft = Convert.ToBoolean(parts[2]);
            bool lwtt = Convert.ToBoolean(parts[3]);

            using (StreamReader str = new StreamReader("flights.csv"))
            {
                if (ddjb == true)
                {

                    flightList.Add(new DDJBFlight(boardinggate, ddjb, cfft, lwtt));
                }
                else if (cfft == true)
                {
                    flightList.Add(new CFFTFlight(boardinggate, ddjb, cfft, lwtt));
                }
                else if (lwtt == true)
                {
                    flightList.Add(new CFFTFlight(boardinggate, ddjb, cfft, lwtt));
                }
                else
                {
                    flightList.Add(new NORMFlight(boardinggate, ddjb, cfft, lwtt));
                }
            }

            

            boardingGateList.Add(new BoardingGate(boardinggate, ddjb, cfft, lwtt,));

        }
    }
}
List<Flight> FlightList = new List<Flight>();
List<BoardingGate> BoardingGateList = new List<BoardingGate>();
List<Airline> AirlineList = new List<Airline>();
bool trueornot = true;
while (trueornot == true)
{
    DisplayMenu();
    CreateAirlineObject(AirlineList);
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

