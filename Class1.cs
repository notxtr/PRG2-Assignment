using System;

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
        _flightNumber = flightNumber;
        _origin = origin;
        _destination = destination;
        _expectedTime = expectedTime;
        _status = status;
    }

    public void CalculateFees(double fee)
    {

    }
    public override string ToString()
    {
        return "Flight Number: " + FlightNumber + ", Origin: " + Origin + ", Destination: " + Destination + ", Expected Time: " + ExpectedTime + ", Status: " + Status;
    }
}
