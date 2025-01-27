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

// Display all boarding gates
void ListAllBoardingGates(List<BoardingGate> boardingGateList)
{
    foreach (var boardingGate in boardingGateList)
    {
        Console.WriteLine(boardingGate.ToString());
    }
}

// Display all flights through airline list
void ListAllFlights(List<Airline> airlineList)
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================");
    foreach (var airline in airlineList)
    {
        Console.WriteLine(airline.ToString());
    }
    Console.WriteLine("\nEnter Airline code: ");
    string code = Console.ReadLine();
    bool notfound = true; // Boolean for checking if airline code is found
    foreach (var airline in airlineList)
    {
        if (airline.Code == code)
        {
            notfound = false;
            Console.WriteLine($"=============================================\r\nList of Flights for {airline.Name}\r\n=============================================");
            Console.WriteLine("FlightNumber\tOrigin\t\tDestination\t\tExpectedTime");
            foreach (var flight in airline.Flights.Values)
            {
                Console.WriteLine(flight.ToString());
            }

            Console.WriteLine("Enter a specific flight number to view details: ");
            string flightnumber = Console.ReadLine();
            bool notfound2 = true; // Boolean for checking if flight number is found
            foreach (var specificflight in airline.Flights.Values)
            {

                if (specificflight.FlightNumber == flightnumber)
                {
                    notfound2 = false;
                    Console.WriteLine("=============================================\r\nFlight Details\r\n=============================================");
                    Console.WriteLine("FlightNumber\tOrigin\t\tDestination\t\tExpectedTime");
                    Console.WriteLine(specificflight.ToString());
                }
            }
            if (notfound2)
            {
                Console.WriteLine("Flight number not found.");
            }
        }
    } 
    if (notfound)
    {
        Console.WriteLine("Airline code not found.");
    }
}

// Modify flight details
void ModifyFlight(List<BoardingGate> boardingGateList)
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================");
    foreach (var airline in AirlineList)
    {
        Console.WriteLine(airline.ToString());
    }
    Console.WriteLine("\nEnter Airline code: ");
    string code = Console.ReadLine();
    bool notfound = true; // Boolean for checking if airline code is found
    foreach (var airline in AirlineList)
    {
        if (airline.Code == code)
        {
            notfound = false;
            Console.WriteLine($"=============================================\r\nList of Flights for {airline.Name}\r\n=============================================");
            Console.WriteLine("FlightNumber\tOrigin\t\tDestination\t\tExpectedTime");
            foreach (var flight in airline.Flights.Values)
            {
                Console.WriteLine(flight.ToString());
            }

            Console.WriteLine("Enter a specific flight number to modify or delete: ");
            string flightnumber = Console.ReadLine();
            bool notfound2 = true; // Boolean for checking if flight number is found
            foreach (var specificflight in airline.Flights.Values)
            {
                if (specificflight.FlightNumber == flightnumber)
                {
                    notfound2 = false;

                    Console.WriteLine("1. Modify Flight");
                    Console.WriteLine("2. Delete Flight");
                    Console.WriteLine("3. Modify Boarding Gate");
                    Console.WriteLine("Please select your option: ");
                    try
                    {
                        int option = Convert.ToInt32(Console.ReadLine());
                        switch (option)
                        {
                            case 1:
                                // Modify other flight details
                                Console.WriteLine("Enter new origin: ");
                                string neworigin = Console.ReadLine();
                                Console.WriteLine("Enter new destination: ");
                                string newdestination = Console.ReadLine();

                                // Validate DateTime input for new expected time
                                DateTime newexpectedtime;
                                while (true)
                                {
                                    Console.WriteLine("Enter new expected time (format: MM/dd/yyyy HH:mm): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out newexpectedtime))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid date format. Please try again.");
                                    }
                                }

                                specificflight.Origin = neworigin;
                                specificflight.Destination = newdestination;
                                specificflight.ExpectedTime = newexpectedtime;
                                Console.WriteLine("Flight details modified.");
                                Console.WriteLine("FlightNumber\tOrigin\t\tDestination\t\tExpectedTime");
                                Console.WriteLine(specificflight.ToString());
                                break;

                            case 2:
                                // Confirm flight deletion
                                Console.WriteLine("Confirm deletion of flight? (Y/N)");
                                string confirmDelete = Console.ReadLine().ToUpper();
                                if (confirmDelete == "Y")
                                {
                                    if (airline.Flights.ContainsKey(flightnumber))
                                    {
                                        airline.Flights.Remove(flightnumber);
                                        Console.WriteLine("Flight Removed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Flight number not found.");
                                    }
                                }
                                break;

                            case 3:
                                // Modify Boarding Gate details
                                BoardingGate flightBoardingGate = null;
                                foreach (var gate in boardingGateList)
                                {
                                    if (gate.Flight != null && gate.Flight.FlightNumber == flightnumber)
                                    {
                                        flightBoardingGate = gate;
                                        break;
                                    }
                                }

                                if (flightBoardingGate != null)
                                {
                                    Console.WriteLine("Current Boarding Gate details:");
                                    Console.WriteLine(flightBoardingGate.ToString());

                                    // Modify Boarding Gate details
                                    Console.WriteLine("Enter new Gate Name: ");
                                    string gateName = Console.ReadLine();

                                    // Validate boolean input for gate support types (CFFT, DDJB, LWTT)
                                    bool supportsCFFT = GetBooleanInput("Supports CFFT (true/false): ");
                                    bool supportsDDJB = GetBooleanInput("Supports DDJB (true/false): ");
                                    bool supportsLWTT = GetBooleanInput("Supports LWTT (true/false): ");

                                    // Update the Boarding Gate details
                                    flightBoardingGate.GateName = gateName;
                                    flightBoardingGate.SupportsCFFT = supportsCFFT;
                                    flightBoardingGate.SupportsDDJB = supportsDDJB;
                                    flightBoardingGate.SupportsLWTT = supportsLWTT;

                                    Console.WriteLine("Boarding Gate details updated.");
                                    Console.WriteLine(flightBoardingGate.ToString());
                                }
                                else
                                {
                                    Console.WriteLine("No Boarding Gate assigned to this flight.");
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }
            if (notfound2)
            {
                Console.WriteLine("Flight number not found.");
            }
        }
    }
    if (notfound)
    {
        Console.WriteLine("Airline code not found.");
    }
}

// Helper method for validating boolean inputs for boarding gate supports
bool GetBooleanInput(string prompt)
{
    bool result;
    while (true)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine().ToLower();
        if (input == "true" || input == "false")
        {
            result = Convert.ToBoolean(input);
            return result;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
        }
    }
}




CreateAirlineObject(AirlineList);
CreateBoardingGateObject(BoardingGateList);// This won't work unless CreateFlightObject is called first (delete when you have the method)
bool trueornot = true;
while (trueornot == true)
{
    DisplayMenu();

    Console.WriteLine("Please select your option: ");
    try
    {
        int option = Convert.ToInt32(Console.ReadLine());
        switch (option)
        {
            case 1:
                break;
            case 2:
                ListAllBoardingGates(BoardingGateList);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                ListAllFlights(AirlineList);
                break;
            case 6:
                ModifyFlight(BoardingGateList);
                break;
            case 7:
                break;
            case 0:
                trueornot = false;
                break;
            default:
                Console.WriteLine("Invalid integer. Please try again.");
                break;
        }
    }
    
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Please try again.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Invalid input. Please try again.");
    }
        
        
}




