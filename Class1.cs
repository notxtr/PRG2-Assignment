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

    public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
    {
        this.flightNumber = flightNumber;
        this.origin = origin;
        this.destination = destination;
        this.expectedTime = expectedTime;
        this.status = status;
    }

    public void CalculateFees(double fee)
    {

    }
    public override string ToString()
    {
        return "Flight Number: " + flightNumber + ", Origin: " + origin + ", Destination: " + destination + ", Expected Time: " + expectedTime + ", Status: " + status;
    }
}
