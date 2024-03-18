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

        public Pijaca(string ime)
        {
            this.ime = ime;
        }

        public Pijaca() { }

        public static List<Pijaca> DataTablePijaca()
        {
            List<Pijaca> pijacas = new List<Pijaca>();
            pijacas.Add(new Pijaca("Pepsi Cola 1,5l"));
            pijacas.Add(new Pijaca("Pepsi Cola 0,5l"));
            pijacas.Add(new Pijaca("Schweps 1.5l"));
            pijacas.Add(new Pijaca("Ora 1,5l"));
            pijacas.Add(new Pijaca("Ora 0,5l"));
            pijacas.Add(new Pijaca("Ledeni čaj 0,5l"));
            pijacas.Add(new Pijaca("Voda z ok. 0,5l"));
            pijacas.Add(new Pijaca("Voda brez ok. 0,5l"));
            pijacas.Add(new Pijaca("Juice 1l"));
            pijacas.Add(new Pijaca("Radenska 0,5l"));
            pijacas.Add(new Pijaca("Energijska pijača"));
            pijacas.Add(new Pijaca("Pivo Laško"));
            pijacas.Add(new Pijaca("Pivo Uni. BREZalk."));
            pijacas.Add(new Pijaca("Vino Belo 1l"));
            pijacas.Add(new Pijaca("Vino Rdeče 1l"));
            pijacas.Add(new Pijaca("Vodka 1l"));
            pijacas.Add(new Pijaca("Borovničevec 1l"));
            pijacas.Add(new Pijaca("Jagermeister 1l"));
            pijacas.Add(new Pijaca("Jack Daniels 1l"));
            pijacas.Add(new Pijaca("Gin 1l"));

            return pijacas;

        }

    }
}
