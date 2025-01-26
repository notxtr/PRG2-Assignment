using System;
//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================
public class LWTTFlight : Flight
{
    private double requestFee;
    public double RequestFee { get { return requestFee; } set { requestFee = value; } }
    public LWTTFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }


    public double CalculateFees()
    {
        double fee = base.CalculateFees();
        RequestFee = 500;
        fee += RequestFee;
        return fee;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
