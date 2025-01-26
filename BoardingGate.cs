using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2REAL_assignment
{
    class BoardingGate
    {
        // Attributes
        private string gateName;
        private bool supportsCFFT;
        private bool supportsDDJB;
        private bool supportsLWTT;
        private Flight flight;

        // Properties 
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        // Default constructor 
        public BoardingGate() { }

        // Parameterized constructor
        public BoardingGate(string gn, bool sCFFT, bool sDDJB, bool sLWTT, Flight f)
        {
            GateName = gn;
            SupportsCFFT = sCFFT;
            SupportsDDJB = sDDJB;
            SupportsLWTT = sLWTT;
            Flight = f;
        }
        // Method to calculate fees
        public double CalculateFees(double fee)
        {
            return Flight.CalculateFees(fee);
        }

        // ToString method 
        public override string ToString()
        {
            return $"{GateName} Supports CFFT: {SupportsCFFT} Supports DDJB: {SupportsDDJB} " +
                $"Supports LWTT: {SupportsLWTT} Flight: {Flight}";
        }
    }
}
