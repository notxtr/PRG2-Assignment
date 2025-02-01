//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

using PRG2REAL_assignment;

// Create Generic Collections: 
Dictionary<string,Airline> airlines = new Dictionary<string, Airline>();
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
Dictionary<string, BoardingGate> BoardingGateLookup = new Dictionary<string, BoardingGate>(); //For basic feature 7
Queue<Flight> UnassignedFlights = new Queue<Flight>(); // For advanced feature (a)

void DisplayMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
}

// Feature 1  Load Files (airlines and boarding gates)
void CreateAirlineObject(Dictionary<string, Airline> airlines)
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string line;

        sr.ReadLine(); // Skip the first line
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string airlineName = parts[0];
            string airlineCode = parts[1];
            // Create Airline object
            Airline airline = new Airline(airlineName, airlineCode);
            // Add Airline object to dictionary
            airlines.Add(airlineCode, airline);
        }
    }
}

// Method to create Boarding Gate objects and add to dictionary
void CreateBoardingGateObject(Dictionary<string, BoardingGate> BoardingGates)
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string line;
        sr.ReadLine(); // Skip the header

        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string bgName = parts[0];
            bool supportDDJB = Convert.ToBoolean(parts[1]);
            bool supportCFFT = Convert.ToBoolean(parts[2]);
            bool supportLWTT = Convert.ToBoolean(parts[3]);

            // Create BoardingGate object
            BoardingGate boardingGate = new BoardingGate(bgName, supportCFFT, supportDDJB, supportLWTT, null);

            // Add BoardingGate object to Dictionary
            boardingGates.Add(bgName, boardingGate);
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
void ListAllFlights(Dictionary<string, Airline> a, Dictionary<string, BoardingGate> boardingGateLookup)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    // List all airlines available
    foreach (var airline in a)
    {
        Console.WriteLine(airline.ToString());
    }

    // Prompt the user to enter the 2-letter airline code
    Console.Write("Enter Airline code (e.g., SQ, MH): ");
    string code = Console.ReadLine();
    bool airlineFound = false; // Flag for checking if the airline code is found

    // Retrieve the Airline object by code
    foreach (var airline in airlines)
    {
        if (airline.Code.Equals(code, StringComparison.OrdinalIgnoreCase)) // Case-insensitive comparison
        {
            airlineFound = true;
            Console.WriteLine($"=============================================\r\nList of Flights for {airline.Name}\r\n=============================================");
            Console.WriteLine($"List of Flights for {airline}.");
            Console.WriteLine($"");

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

// Create airline dictionary 
Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
// Modify flight details
void ModifyFlight(List<Flight> flightList)
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================");
    foreach (var airline in airlines)
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

// Feature 2 Load Files (flights)

// Method to load flights data, create flight objects and add to dictionary
void LoadFlights(Dictionary<string, Flight> fDict)
{
    string[] csvLines = File.ReadAllLines("flights.csv");

    // Display heading 
    string[] heading = csvLines[0].Split(',');
    Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-30} {4,-25}", heading[0], heading[1], heading[2], heading[3], heading[4]);

    // Display the rest of flight details
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] flightDetails = csvLines[i].Split(',');
        Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-30} {4,-25}", flightDetails[0], flightDetails[1], flightDetails[2], flightDetails[3], flightDetails[4]);
    }

    // Create flight objects based on data and add to dictionary
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] flightDetails = csvLines[i].Split(',');
        string flightNumber = flightDetails[0];
        string origin = flightDetails[1];
        string destination = flightDetails[2];
        DateTime expectedTime = DateTime.Parse(flightDetails[3]);
        string status = flightDetails[4];

        Flight flight;

        // Create specific flight object based on status
        if (status == "CFFT")
        {
            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, status);
        }
        else if (status == "DDJB")
        {
            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, status);
        }
        else if (status == "LWTTFlight")
        {
            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, status);
        }
        else
        {
            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, status);
        }

        // Add the flight to the dictionary with flightNumber as the key
        fDict[flightNumber] = flight;
    }
}

// Feature 3 List all flights with their basic information
void DisplayFlights()
{
    try
    {
        string[] csvLines = File.ReadAllLines("flights.csv");
        string[] csvLines2 = File.ReadAllLines("airlines.csv");
        // Display heading 
        string[] heading = csvLines[0].Split(',');
        Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-30} {4,-25}", heading[0], "Airline Name", heading[2], heading[3], heading[4]);

        // Display the rest of flight details
        foreach (Flight flight in flights.Values)
        {
            string airlineName = "";
            if (airlines.ContainsKey(flight.FlightNumber.Substring(0, 2)))
            {
                airlineName = airlines[flight.FlightNumber.Substring(0, 2)].Name;
            }
            else
            {
                airlineName = "Unknown";
            }

            Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-30} {4,-25}", flight.FlightNumber, airlineName, flight.Origin, flight.Destination, flight.ExpectedTime);
        }
    }
    catch (IndexOutOfRangeException ex)
    {
        Console.WriteLine("An error occurred: Index was outside the bounds of the array.");
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unexpected error occurred.");
        Console.WriteLine(ex.Message);
    }
}








CreateAirlineObject(airlines);
CreateBoardingGateObject(boardingGates);// This won't work unless CreateFlightObject is called first (delete this comment when you have the method)
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
                ListAllBoardingGates(boardingGates);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                ListAllFlights(airlines, BoardingGateLookup);
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




