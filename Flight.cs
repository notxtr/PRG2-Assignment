using System;
using System.Runtime.CompilerServices;

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

    public double CalculateFees(double fee)
    {
        fee = 300;
        if (Origin == "Singapore (SIN)")
        {
            fee += 800;
        }
        else if (Destination == "Singapore (SIN)")
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

public class NORMFlight : Flight
{
    public NORMFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }
    public void CalculateFees(double fee) { }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class CFFTFlight : Flight
{
    private double requestFee;
    public double RequestFee { get { return requestFee; } set { requestFee = value; } }
    public CFFTFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }


    public double CalculateFees(double fee)
    {
        fee = base.CalculateFees(fee);
        RequestFee = 150;
        fee += RequestFee;
        return fee;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class DDJBFlight : Flight
{
    private double requestFee;
    public double RequestFee { get { return requestFee; } set { requestFee = value; } }
    public DDJBFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }


    public double CalculateFees(double fee)
    {
        fee = base.CalculateFees(fee);
        RequestFee = 300;
        fee += RequestFee;
        return fee;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class LWTTFlight : Flight
{
    private double requestFee;
    public double RequestFee { get { return requestFee; } set { requestFee = value; } }
    public LWTTFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }


    public double CalculateFees(double fee)
    {
        fee = base.CalculateFees(fee);
        RequestFee = 500;
        fee += RequestFee;
        return fee;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}