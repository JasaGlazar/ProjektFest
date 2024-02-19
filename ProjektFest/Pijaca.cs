using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFest
{
    public class Pijaca
    {
        public string ime { get; set; }
        public double cena { get; set; }

        public Pijaca(string ime, double cena)
        {
            this.ime = ime;
            this.cena = cena;
        }

        public Pijaca() { }

    }
}
