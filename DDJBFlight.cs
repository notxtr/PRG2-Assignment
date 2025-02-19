﻿using PRG2REAL_assignment;
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
    class DDJBFlight : Flight
    {
        // Attribute
        private double RequestFee()
        {
            return 300; // Set default request fee to $300 for DDJB flights
        }

        // Default Constructor
        public DDJBFlight() { }

        // Parameterized Constructor
        public DDJBFlight(string fn, string o, string d, DateTime et, string s, Airline airline) : base(fn, o, d, et, s, airline) { }

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
            return base.ToString() + $"\t\t{ExpectedTime}\t\t{Status}";
        }
    }
}
