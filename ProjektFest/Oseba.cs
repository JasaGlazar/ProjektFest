using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFest
{
    public class Oseba
    {
        public string ime { get; set; }
        public string priimek { get; set; }

        public Oseba(string ime,string priimek) 
        {
            this.ime = ime; 
            this.priimek = priimek;
        }
        public Oseba() { }
    }
}
