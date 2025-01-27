//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

using PRG2REAL_assignment;

List<Flight> FlightList = new List<Flight>();
List<BoardingGate> BoardingGateList = new List<BoardingGate>();
Dictionary<string, BoardingGate> BoardingGateLookup = new Dictionary<string, BoardingGate>(); //For basic feature 7
List<Airline> AirlineList = new List<Airline>();
Queue<Flight> UnassignedFlights = new Queue<Flight>(); // For advanced feature (a)
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
void ListAllFlights(List<Airline> airlineList, Dictionary<string, BoardingGate> boardingGateLookup)
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================");

    // List all airlines available
    foreach (var airline in airlineList)
    {
        Console.WriteLine(airline.ToString());
    }

    // Prompt the user to enter the 2-letter airline code
    Console.WriteLine("\nEnter Airline code (e.g., SQ, MH): ");
    string code = Console.ReadLine();
    bool airlineFound = false; // Flag for checking if the airline code is found

    // Retrieve the Airline object by code
    foreach (var airline in airlineList)
    {
        if (airline.Code.Equals(code, StringComparison.OrdinalIgnoreCase)) // Case-insensitive comparison
        {
            airlineFound = true;
            Console.WriteLine($"=============================================\r\nList of Flights for {airline.Name}\r\n=============================================");
            Console.WriteLine("FlightNumber\tOrigin\t\tDestination");

            // Display flight list for the selected airline
            foreach (var flight in airline.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber}\t\t{flight.Origin}\t\t{flight.Destination}");
            }

            // Prompt user to select a flight number
            Console.WriteLine("\nEnter a specific flight number to view details: ");
            string flightNumber = Console.ReadLine();
            bool flightFound = false; // Flag for checking if the flight number is found

            // Retrieve the specific flight from the dictionary
            if (airline.Flights.ContainsKey(flightNumber))
            {
                var flight = airline.Flights[flightNumber];
                flightFound = true;
                Console.WriteLine("=============================================\r\nFlight Details\r\n=============================================");
                Console.WriteLine("FlightNumber\tAirline\t\tOrigin\t\tDestination\t\tExpectedTime\t\tSpecial Request\tBoarding Gate");

                // Display basic flight details
                Console.WriteLine($"{flight.FlightNumber}\t{airline.Name}\t{flight.Origin}\t{flight.Destination}\t{flight.ExpectedTime}\t\t\t{(boardingGateLookup.ContainsKey(flight.FlightNumber) ? boardingGateLookup[flight.FlightNumber].GateName : "None")}");

                // If BoardingGate exists, show its details
                if (boardingGateLookup.ContainsKey(flight.FlightNumber))
                {
                    var boardingGate = boardingGateLookup[flight.FlightNumber];
                    Console.WriteLine($"Boarding Gate Details:");
                    Console.WriteLine($"Gate Name: {boardingGate.GateName}");
                    Console.WriteLine($"Supports CFFT: {boardingGate.SupportsCFFT}");
                    Console.WriteLine($"Supports DDJB: {boardingGate.SupportsDDJB}");
                    Console.WriteLine($"Supports LWTT: {boardingGate.SupportsLWTT}");
                }
            }

            // If no matching flight was found, notify the user
            if (!flightFound)
            {
                Console.WriteLine("Flight number not found.");
            }

            break; // Exit the loop once the airline is found and processed
        }
    }

    // If no matching airline code is found, notify the user
    if (!airlineFound)
    {
        Console.WriteLine("Airline code not found.");
    }
}

// Modify flight details
void ModifyFlight(List<Flight> flightList)
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

                    Console.WriteLine("1. Modify Existing Flight");
                    Console.WriteLine("2. Delete Existing Flight");
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
                                foreach (var flight in flightList)
                                {
                                    Console.WriteLine(flight.ToString());
                                }
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
                                        Console.WriteLine(specificflight.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("Flight number not found.");
                                    }
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

// Process unassigned flights in bulk
void ProcessUnassignedFlights(Queue<Flight> unassignedFlights)
{
    // Add unassigned flights to queue
    foreach (var airline in AirlineList)
    {
        foreach (var flight in airline.Flights.Values)
        {
            if (!BoardingGateLookup.ContainsKey(flight.FlightNumber))
            {
                unassignedFlights.Enqueue(flight);
            }
        }
    }

    int unassignedFlightsCount = unassignedFlights.Count;
    int unassignedGatesCount = BoardingGateList.Count(bg => bg.Flight == null);

    Console.WriteLine($"Total unassigned flights: {unassignedFlightsCount}");
    Console.WriteLine($"Total unassigned boarding gates: {unassignedGatesCount}");

    int assignedCount = 0;

    while (unassignedFlights.Count > 0)
    {
        Flight flight = unassignedFlights.Dequeue();
        BoardingGate assignedGate = null;

        string specialRequestCode = GetSpecialRequestCode(flight); // Get the request code dynamically

        if (!string.IsNullOrEmpty(specialRequestCode))
        {
            assignedGate = BoardingGateList.FirstOrDefault(bg => bg.Flight == null && SupportsSpecialCode(bg, specialRequestCode));
        }
        else
        {
            assignedGate = BoardingGateList.FirstOrDefault(bg => bg.Flight == null);
        }

        if (assignedGate != null)
        {
            assignedGate.Flight = flight;
            BoardingGateLookup[flight.FlightNumber] = assignedGate;
            assignedCount++;
            Console.WriteLine($"Assigned Flight {flight.FlightNumber} to Gate {assignedGate.GateName}");
        }
        else
        {
            Console.WriteLine($"No available boarding gate for Flight {flight.FlightNumber}");
        }
    }

    Console.WriteLine($"Total flights processed and assigned: {assignedCount}");
    Console.WriteLine($"Assignment success rate: {(unassignedFlightsCount > 0 ? (assignedCount * 100.0 / unassignedFlightsCount) : 0):F2}%");
}

// Method to get special request code dynamically
string GetSpecialRequestCode(Flight flight)
{
    if (flight is DDJBFlight) return "DDJB";
    if (flight is CFFTFlight) return "CFFT";
    if (flight is LWTTFlight) return "LWTT";
    return ""; // No special request for normal flights
}

// Method to check if a boarding gate supports a special request code
bool SupportsSpecialCode(BoardingGate bg, string specialRequestCode)
{
    return (specialRequestCode == "DDJB" && bg.SupportsDDJB) ||
           (specialRequestCode == "CFFT" && bg.SupportsCFFT) ||
           (specialRequestCode == "LWTT" && bg.SupportsLWTT);
}





CreateAirlineObject(AirlineList);
CreateBoardingGateObject(BoardingGateList);// This won't work unless CreateFlightObject is called first (delete this comment when you have the method)
ProcessUnassignedFlights(UnassignedFlights);
bool trueornot = true;
while (trueornot == true)
{
    DisplayMenu();

    Console.WriteLine("Please select an option: ");
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
                ListAllFlights(AirlineList, BoardingGateLookup);
                break;
            case 6:
                ModifyFlight(FlightList);
                break;
            case 7:
                break;
            case 0:
                trueornot = false;
                Console.WriteLine("Goodbye!");
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




