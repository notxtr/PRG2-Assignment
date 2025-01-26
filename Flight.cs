using System;
using System.Runtime.CompilerServices;
//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================
public class Flight
{
    private string flightNumber;
    private string origin;
    private string destination;
    private DateTime expectedTime;
    private string status;

    public string FlightNumber { get { return flightNumber; } set { flightNumber = value; } }
    public string Origin { get { return origin; } set { origin = value; } }
    public string Destination { get { return destination; } set { destination = value; } }
    public DateTime ExpectedTime { get { return expectedTime; } set { expectedTime = value; } }

    public string Status { get { return status; } set { status = value; } }

    public Flight(string _flightNumber, string _origin, string _destination, DateTime _expectedTime, string _status)
    {
        FlightNumber = _flightNumber;
        Origin = _origin;
        Destination = _destination;
        ExpectedTime  = _expectedTime;
        Status = _status;
    }

    public double CalculateFees()
    {
        double fee = 300;
        if (Origin == "Singapore (SIN)") // Departing flight fee
        {
            fee += 800;
        }
        else if (Destination == "Singapore (SIN)") // Arriving flight fee
        {
            fee += 500;
        }

        return fee;
    }
    public override string ToString()
    {
        return $"{FlightNumber}\t{Origin}\t\t{Destination}\t\t{ExpectedTime}";
    }
}


