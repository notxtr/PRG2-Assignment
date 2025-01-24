using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2REAL_assignment
{
    class Airline
    {
        // attributes
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        // dictionary to store flights
        private Dictionary<string, Flight>;
        public Dictionary<string, Flight> flightDict { get; set; }
                                        = new Dictionary<string, Flight>();

    }
}
