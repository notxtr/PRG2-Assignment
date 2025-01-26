using System;
//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================
public class NORMFlight : Flight
{
    public NORMFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }
    public void CalculateFees() { } //Empty method since there is no additional fees for a normal flight

    public override string ToString()
    {
        return base.ToString();
    }
}
