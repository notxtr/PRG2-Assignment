using System;

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

