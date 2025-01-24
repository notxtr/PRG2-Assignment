using System;

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

