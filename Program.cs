//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

using PRG2REAL_assignment;

// Create Generic Collections: 
Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
Dictionary<string, BoardingGate> BoardingGateLookup = new Dictionary<string, BoardingGate>(); //For basic feature 7
Queue<Flight> UnassignedFlights = new Queue<Flight>(); // For advanced feature (a)


// Feature 1 Load files (airlines and boarding gates)
void InitAirlines()
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
void InitBoardingGates()
{
    using (StreamReader sr = new StreamReader("boardingGates.csv"))
    {
        string line;

        sr.ReadLine(); // Skip the first line
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string gateName = parts[0];
            bool supportsDDJB = bool.Parse(parts[1]);
            bool supportsCFFT = bool.Parse(parts[2]);
            bool supportsLWTT = bool.Parse(parts[3]);

            // Create BoardingGate object
            BoardingGate boardingGate = new BoardingGate(gateName, supportsDDJB, supportsCFFT, supportsLWTT, null);

            // Add BoardingGate object to dictionary
            boardingGates.Add(gateName, boardingGate);
        }
    }
}

// Feature 2 Load Files (flights)
// Method to load flights data, create flight objects and add to dictionary
void InitFlights()
{
    string[] csvLines = File.ReadAllLines("flights.csv");

    // Create flight objects based on data and add to dictionary
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] flightDetails = csvLines[i].Split(',');
        string flightNumber = flightDetails[0];
        string origin = flightDetails[1];
        string destination = flightDetails[2];
        DateTime expectedTime = DateTime.Parse(flightDetails[3]);
        string specialRequestCode = flightDetails[4];

        // Get airline code from flight number
        string airlineCode = flightNumber.Substring(0, 2);

        // Get airline object from dictionary
        Airline airline = airlines[airlineCode];

        // Initialize flight object
        Flight flight;

        // Create specific flight object based on status
        if (specialRequestCode == "CFFT")
        {
            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "On Time", airline);
        }
        else if (specialRequestCode == "DDJB")
        {
            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "On Time", airline);
        }
        else if (specialRequestCode == "LWTTFlight")
        {
            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "On Time", airline);
        }
        else
        {
            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "On Time", airline);
        }

        // Add the flight to the dictionary with flightNumber as the key
        flights.Add(flightNumber, flight);
        airline.AddFlight(flight);

    }
}

// Feature 3 List all flights with their basic information
void DisplayFlights()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");

        string[] csvLines = File.ReadAllLines("flights.csv");
        string[] csvLines2 = File.ReadAllLines("airlines.csv");

        // Display heading 
        string[] heading = csvLines[0].Split(',');
        Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-20} {4,-25}", heading[0], "Airline Name", heading[1], heading[2], heading[3]);

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

            Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-20} {4,-25}", flight.FlightNumber, airlineName, flight.Origin, flight.Destination, flight.ExpectedTime);
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

// Feature 4 List all boarding gates 
void ListAllBoardingGates()
{
    Console.WriteLine("================================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("================================================");

    // Display heading
    Console.WriteLine("{0,-20} {1,-15} {2,-15} {3,-15}", "Gate Name", "DDJB", "CFFT", "LWTT");

    // Display the rest of boarding gate details
    foreach (BoardingGate boardingGate in boardingGates.Values)
    {
        Console.WriteLine("{0,-20} {1,-15} {2,-15} {3,-15}",
            boardingGate.GateName, boardingGate.SupportsDDJB, boardingGate.SupportsCFFT, boardingGate.SupportsLWTT);
    }
}

// Feature 5 Assign a boarding gate to a flight
void AssignBoardingGate()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    // Get flight number from user
    Console.WriteLine("Enter Flight Number: ");
    string? flightNumber = Console.ReadLine();

    // Find flight object from dictionary
    Flight chosenFlight = flights[flightNumber];

    // Check if it has already been assigned a boarding gate
    bool alreadyAssigned = boardingGates.Values.Any(bg => bg.Flight != null);

    if (alreadyAssigned)
    {
        Console.WriteLine("This flight has already been assigned a boarding gate.");
        return;
    }

    // Get boarding gate name from user
    Console.WriteLine("Enter Boarding Gate Name: ");
    string? gateName = Console.ReadLine();

    // Check if boarding gate exists
    if (!boardingGates.ContainsKey(gateName))
    {
        Console.WriteLine("This boarding gate does not exist.");
        return;
    }
    // Find boarding gate object from dictionary
    BoardingGate chosenGate = boardingGates[gateName];

    // Check if boarding gate is already assigned
    if (chosenGate.Flight != null)
    {
        Console.WriteLine("This boarding gate has already been assigned a flight.");
        return;
    }

    // Assign flight to boarding gate
    chosenGate.Flight = chosenFlight;

    // Change flight special request code to "" instead of NORM
    string specialRequestCode;

    // Check if flight is a special request flight
    if (chosenFlight is CFFTFlight)
    {
        specialRequestCode = "CFFT";
    }
    else if (chosenFlight is DDJBFlight)
    {
        specialRequestCode = "DDJB";
    }
    else if (chosenFlight is LWTTFlight)
    {
        specialRequestCode = "LWTT";
    }
    else
    {
        specialRequestCode = "None";
    }

    // Display flight information
    Console.WriteLine($"Flight Number: {chosenFlight.FlightNumber}");
    Console.WriteLine($"Origin: {chosenFlight.Origin}");
    Console.WriteLine($"Destination: {chosenFlight.Destination}");
    Console.WriteLine($"Expected Time: {chosenFlight.ExpectedTime}");
    Console.WriteLine($"Special Request Code: {specialRequestCode}");
    Console.WriteLine($"Boarding Gate Name: {chosenGate.GateName}");
    Console.WriteLine($"Supports DDJB: {chosenGate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {chosenGate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {chosenGate.SupportsLWTT}");
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");

    // Get user input
    string? updateStatus = Console.ReadLine();

    // Check if user input is valid
    if (updateStatus == "Y" || updateStatus == "y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.WriteLine("Please select the new status of the flight:");
        string? newStatus = Console.ReadLine();
        chosenFlight.Status = newStatus;
    }
    else
    {
        Console.WriteLine($"Flight status remains {chosenFlight.Status}");
    }

    // Display updated flight information
    Console.WriteLine($"Flight {chosenFlight.FlightNumber} has been assigned to Boarding Gate {chosenGate.GateName}!");
}



// Feature 6 Create a new flight
void CreateNewFlight()
{
    Console.Write("Enter Flight Number: ");
    string? flightNumber = Console.ReadLine();

    // Get airline code from flight number
    string airlineCode = flightNumber.Substring(0, 2);

    Console.Write("Enter Origin: ");
    string? origin = Console.ReadLine();

    Console.Write("Enter Destination: ");
    string? destination = Console.ReadLine();

    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
    DateTime expectedTime = DateTime.Parse(Console.ReadLine());

    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
    string? specialRequestCode = Console.ReadLine();

    // Check flight type based on special request code
    Flight flight;

    // Create specific flight object based on request code
    if (specialRequestCode == "CFFT")
    {
        flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "On Time", airlines[airlineCode]);
    }
    else if (specialRequestCode == "DDJB")
    {
        flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "On Time", airlines[airlineCode]);
    }
    else if (specialRequestCode == "LWTT")
    {
        flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "On Time", airlines[airlineCode]);
    }
    else
    {
        flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "On Time", airlines[airlineCode]);
    }

    // Add the flight to the dictionary with flightNumber as the key
    flights.Add(flightNumber, flight);

    // Add the flight to the airline
    airlines[airlineCode].AddFlight(flight);

    // Display success message
    Console.WriteLine($"Flight {flight.FlightNumber} has been added!");

    // Ask user if they want to add another flight
    Console.WriteLine("Would you like to add another flight? (Y/N)");
    string? addAnother = Console.ReadLine();

    if (addAnother == "Y" || addAnother == "y")
    {
        CreateNewFlight();
    }
    else if (addAnother == "N" || addAnother == "n")
    {
        return;
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter Y/N.");
    }
}

// Feature 7 Display full flight details from an airline
void DisplayAirlineFlights()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-20}{1,-20}", "Airline Code", "Airline Name");

    // Display list of airlines
    foreach (Airline airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-20}{airline.Name,-20}");
    }

    Console.Write("Enter Airline Code: ");
    string? airlineCode = Console.ReadLine().ToUpper();
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Singapore Airlines");
    Console.WriteLine("=============================================");

    // Check if airline code exists
    if (airlineCode == "SQ" || airlineCode == "MH" || airlineCode == "JL" || airlineCode == "CX" || airlineCode == "QF" || airlineCode == "TR" || airlineCode == "EK" || airlineCode == "BA")
    {
        Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-20} {4,-25}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

        // Display the rest of flight details
        foreach (Flight flight in flights.Values)
        {
            if (flight.Airline.Code == airlineCode)
            {
                Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-20} {4,-25}",
                    flight.FlightNumber, flight.Airline.Name, flight.Origin, flight.Destination, (flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")));
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code. Please try again.");
    }
}

// Feature 8 Modify flight details 
void ModifyFlightDetails()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-20}{1,-20}", "Airline Code", "Airline Name");

    // Display list of airlines
    foreach (Airline airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-20}{airline.Name,-20}");
    }

    //  Prompt user to enter airline code
    Console.WriteLine("Enter Airline Code: ");
    string? airlineCode = Console.ReadLine().ToUpper();

    // Check if airline code exists
    if (airlineCode == "SQ" || airlineCode == "MH" || airlineCode == "JL" || airlineCode == "CX" || airlineCode == "QF" || airlineCode == "TR" || airlineCode == "EK" || airlineCode == "BA")
    {
        Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-20} {4,-25}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

        // Display the rest of flight details
        foreach (Flight flight in flights.Values)
        {
            if (flight.Airline.Code == airlineCode)
            {
                Console.WriteLine("{0,-17} {1,-21} {2,-20} {3,-20} {4,-25}",
                    flight.FlightNumber, flight.Airline.Name, flight.Origin, flight.Destination, (flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")));
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code. Please try again.");
    }
    Console.WriteLine("Choose an existing Flight to modify or delete: ");
    string? flightNumber = Console.ReadLine();

    // Check if flight exists
    if (flights.ContainsKey(flightNumber))
    {
        Flight chosenFlight = flights[flightNumber];
        Console.WriteLine("1. Modify Flight");
        Console.WriteLine("2. Delete Flight");
        Console.WriteLine("Choose an option:");

        // Get user input
        string? option = Console.ReadLine();

        // Modify Flight
        if (option == "1")
        {
            Console.WriteLine("1. Modify Basic Information");
            Console.WriteLine("2. Modify Status");
            Console.WriteLine("3. Modify Special Request Code");
            Console.WriteLine("4. Modify Boarding Gate");
            Console.WriteLine("Choose an option:");

            // Get user input
            string? modifyOption = Console.ReadLine();
            // Modify Basic Information
            if (modifyOption == "1")
            {
                Console.Write("Enter new Origin: ");
                string? newOrigin = Console.ReadLine();
                Console.Write("Enter new Destination: ");
                string? newDestination = Console.ReadLine();
                Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                DateTime newExpectedTime = DateTime.Parse(Console.ReadLine());

                // Update flight details
                chosenFlight.Origin = newOrigin;
                chosenFlight.Destination = newDestination;
                chosenFlight.ExpectedTime = newExpectedTime;
            }
            // Modify Status
            else if (modifyOption == "2")
            {
                // Display status options
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");

                Console.WriteLine("Please select the new status of the flight:");
                // Get user input
                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    chosenFlight.Status = "Delayed";
                }
                else if (choice == "2")
                {
                    chosenFlight.Status = "Boarding";
                }
                else if (choice == "3")
                {
                    chosenFlight.Status = "On Time";
                }
            }
            // Modify Special Request Code
            else if (modifyOption == "3")
            {
                Console.WriteLine("Enter new Special Request Code (CFFT/DDJB/LWTT/None): ");
                // Get user input
                string? newSpecialRequestCode = Console.ReadLine();

                if (newSpecialRequestCode == "CFFT")
                {
                    // Remove old flight object from dictionary 
                    flights.Remove(chosenFlight.FlightNumber);
                    // Create new CFFT flight object
                    chosenFlight = new CFFTFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status, chosenFlight.Airline);
                    // Add new flight object to dictionary
                    flights.Add(chosenFlight.FlightNumber, chosenFlight);
                }
                else if (newSpecialRequestCode == "DDJB")
                {
                    // Remove old flight object from dictionary 
                    flights.Remove(chosenFlight.FlightNumber);
                    // Create new DDJB flight object
                    chosenFlight = new DDJBFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status, chosenFlight.Airline);
                    // Add new flight object to dictionary
                    flights.Add(chosenFlight.FlightNumber, chosenFlight);

                }
                else if (newSpecialRequestCode == "LWTT")
                {
                    // Remove old flight object from dictionary 
                    flights.Remove(chosenFlight.FlightNumber);
                    // Create new LWTT flight object
                    chosenFlight = new LWTTFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status, chosenFlight.Airline);
                    // Add new flight object to dictionary
                    flights.Add(chosenFlight.FlightNumber, chosenFlight);
                }
                else if (newSpecialRequestCode == "None")
                {
                    // Remove old flight object from dictionary 
                    flights.Remove(chosenFlight.FlightNumber);
                    // Create new NORM flight object
                    chosenFlight = new NORMFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status, chosenFlight.Airline);
                    // Add new flight object to dictionary
                    flights.Add(chosenFlight.FlightNumber, chosenFlight);
                }
                else
                {
                    Console.WriteLine("Invalid Option. Please try again.");
                    return;
                }
            }
            // Modify Boarding Gate
            else if (modifyOption == "4")
            {
                Console.WriteLine("Enter new Boarding Gate Name: ");
                string? newGateName = Console.ReadLine();

                // Check if boarding gate exists
                if (!boardingGates.ContainsKey(newGateName))
                {
                    Console.WriteLine("This boarding gate does not exist.");
                    return;
                }
                // Find boarding gate object from dictionary
                BoardingGate newGate = boardingGates[newGateName];

                // Check if boarding gate is already assigned
                if (newGate.Flight != null)
                {
                    Console.WriteLine("This boarding gate has already been assigned a flight.");
                    return;
                }

                // Check if boarding gate supports the flight and special code 
                if (newGate is CFFTFlight && newGate.SupportsCFFT == false)
                {
                    Console.WriteLine("This boarding gate does not support CFFT flights.");
                    return;
                }
                else if (newGate is DDJBFlight && newGate.SupportsDDJB == false)
                {
                    Console.WriteLine("This boarding gate does not support DDJB flights.");
                    return;
                }
                else if (newGate is LWTTFlight && newGate.SupportsLWTT == false)
                {
                    Console.WriteLine("This boarding gate does not support LWTT flights.");
                    return;
                }
                else
                {
                    // Assign flight to boarding gate
                    newGate.Flight = chosenFlight;

                    // Display updated flight information
                    Console.WriteLine($"Flight {chosenFlight.FlightNumber} has been assigned to Boarding Gate {newGate.GateName}!");
                }
            }
            else
            {
                Console.WriteLine("Invalid Option. Please try again.");
                return;
            }

            // Display updated flight information
            Console.WriteLine("Flight updated!");
            Console.WriteLine($"Flight Number: {flightNumber}");
            Console.WriteLine($"Airline Name: {chosenFlight.Airline.Name}");
            Console.WriteLine($"Origin: {chosenFlight.Origin}");
            Console.WriteLine($"Destination: {chosenFlight.Destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {chosenFlight.ExpectedTime}");
            Console.WriteLine($"Status: {chosenFlight.Status}");

            // Check for special request code
            if (chosenFlight is CFFTFlight)
            {
                Console.WriteLine("Special Request Code: CFFT");
            }
            else if (chosenFlight is DDJBFlight)
            {
                Console.WriteLine("Special Request Code: DDJB");
            }
            else if (chosenFlight is LWTTFlight)
            {
                Console.WriteLine("Special Request Code: LWTT");
            }
            else
            {
                Console.WriteLine("Special Request Code: None");
            }

            // Check for boarding gate
            foreach (var gate in boardingGates.Values)
            {
                if (gate.Flight == chosenFlight)
                {
                    Console.WriteLine($"Boarding Gate: {gate.GateName}");
                    break;
                }
                else
                {
                    Console.WriteLine("Boarding Gate: Unassigned");
                    break;
                }
            }
        }
        // Delete Flight
        else if (option == "2")
        {
            // Prompt user for confirmation
            Console.WriteLine("Are you sure you want to delete this flight? (Y/N):");
            string? confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                // Remove flight from dictionary
                flights.Remove(flightNumber);
                Console.WriteLine("Flight deleted!");
            }
            else if (confirm == "N")
            {
                Console.WriteLine("Flight not deleted.");
            }
            else
            {
                Console.WriteLine("Invalid Option. Please try again.");
                return;
            }
        }
        // Input Validation
        else
        {
            Console.WriteLine("Invalid Option. Please try again.");
        }
    }
}


// Feature 9 Display scheduled flights in chronological order, with boarding gates assignments where applicable
void DisplayScheduledFlights()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-17} {"Airline Name",-21} {"Origin",-20} {"Destination",-20} {"Expected Departure/Arrival Time",-35} {"Status",-12} {"Boarding Gate",-10}");


    // Change dictionary to list to sort
    List<Flight> sortedFlights = flights.Values.ToList();

    // Sort the list of flights by expected time
    sortedFlights.Sort();

    // Find boarding gate for each flight
    foreach (var flight in sortedFlights)
    {
        // Set default to unassigned
        string boardingGate = "Unassigned";

        // Find assigned boarding gate
        foreach (var gate in boardingGates.Values)
        {
            if (gate.Flight == flight)
            {
                boardingGate = gate.GateName;
                break;
            }
        }

        // Format Expected Time
        string formattedTime = flight.ExpectedTime.ToString("d/M/yyyy HH:mm:ss tt");

        // Display flight information
        Console.WriteLine($"{flight.FlightNumber,-17} {airlines[flight.FlightNumber.Substring(0, 2)].Name,-21} {flight.Origin,-20} {flight.Destination,-20} {formattedTime,-35} {flight.Status,-12} {boardingGate,-10}");
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
    foreach (var airline in airlines)
    {
        foreach (var flight in airline.Value.Flights.Values)
        {
            if (!BoardingGateLookup.ContainsKey(flight.FlightNumber))
            {
                unassignedFlights.Enqueue(flight);
            }
        }
    }

    int unassignedFlightsCount = unassignedFlights.Count;
    int unassignedGatesCount = boardingGates.Count(bg => bg.Value.Flight == null);

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
            //assignedGate = boardingGates.FirstOrDefault(bg => bg.Value.Flight == null && SupportsSpecialCode(bg, specialRequestCode));
        }
        else
        {
            //assignedGate = boardingGates.FirstOrDefault(bg => bg.Value.Flight == null);
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


// Initialize Airlines, Boarding Gates and Flights
InitAirlines();
InitBoardingGates();
InitFlights();


ProcessUnassignedFlights(UnassignedFlights);
bool trueornot = true;
while (trueornot == true)
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
    Console.WriteLine("Please select an option: ");
    try
    {
        int option = Convert.ToInt32(Console.ReadLine());
        switch (option)
        {
            case 1:
                DisplayFlights();
                break;
            case 2:
                ListAllBoardingGates();
                break;
            case 3:
                AssignBoardingGate();
                break;
            case 4:
                CreateNewFlight();
                break;
            case 5:
                DisplayAirlineFlights();
                break;
            case 6:
                ModifyFlightDetails();
                break;
            case 7:
                DisplayScheduledFlights();
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




