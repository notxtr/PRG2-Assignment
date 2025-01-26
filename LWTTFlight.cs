using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================
namespace PRG2REAL_assignment
{
    class LWTTFlight : Flight
    {
        // Attribute
        private double RequestFee()
        {
            return 500; // Set default request fee to $500 for LWTT flights
        }

        // Default Constructor
        public LWTTFlight() { }

        // Parameterized Constructor
        public LWTTFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }

        // Override CalculateFees method
        public override double CalculateFees()
        {
            if (Destination == "SIN")
            {
                return 500 + 300 + RequestFee();
            }
            else
            {
                return 800 + 300 + RequestFee();
            }
        }

        // ToString method 
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
