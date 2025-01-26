//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

using PRG2REAL_assignment;
using System.ComponentModel;

// Feature 2 Load Files (flights)

// Create flight dictionary 
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

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

// call method
LoadFlights(flightDict); // test if method works
