using System;

public class NORMFlight : Flight
{
    public NORMFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }
    public void CalculateFees(double fee) { }

    public override string ToString()
    {
        return base.ToString();
    }
}
