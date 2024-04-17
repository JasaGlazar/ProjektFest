using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjektFest
{
    public class Pijaca
    {
        public string ime { get; set; }
        public double kolicina {  get; set; }
        public double cena { get; set; }

        public Pijaca(string ime, double cena, double kolicina)
        {
            this.ime = ime;
            this.cena = cena;
            this.kolicina = kolicina;
        }

        public Pijaca(string ime, double kolicina)
        {
            this.ime = ime;
            this.kolicina = kolicina;
        }

        public Pijaca(string ime) 
        {
            this.ime = ime;
        }

        public Pijaca() { }

        public static List<Pijaca> DataTablePijaca()
        {
            //21 razlicic
            List<Pijaca> pijacas = new List<Pijaca>();
            pijacas.Add(new Pijaca("Pepsi Cola 1,5l", 1.5));
            pijacas.Add(new Pijaca("Pepsi Cola 0,5l", 0.5));
            pijacas.Add(new Pijaca("Schweps 1.5l", 1.5));
            pijacas.Add(new Pijaca("Ora 1,5l",1.5));
            pijacas.Add(new Pijaca("Ora 0,5l",0.5));
            pijacas.Add(new Pijaca("Ledeni čaj 0,5l",0.5));
            pijacas.Add(new Pijaca("Voda z ok. 0,5l",0.5));
            pijacas.Add(new Pijaca("Voda brez ok. 0,5l", 0.5));
            pijacas.Add(new Pijaca("Juice 1l", 1));
            pijacas.Add(new Pijaca("Radenska 0,5l", 0.5));
            pijacas.Add(new Pijaca("Energijska pijača", 0.25));
            pijacas.Add(new Pijaca("Pivo Laško", 0.33));
            pijacas.Add(new Pijaca("Pivo Union", 0.33));
            pijacas.Add(new Pijaca("Pivo Uni. BREZalk.", 0.5));
            pijacas.Add(new Pijaca("Vino Belo 1l",1));
            pijacas.Add(new Pijaca("Vino Rdeče 1l",1));
            pijacas.Add(new Pijaca("Vodka 1l",1));
            pijacas.Add(new Pijaca("Borovničevec 1l",1));
            pijacas.Add(new Pijaca("Jagermeister 1l",1));
            pijacas.Add(new Pijaca("Jack Daniels 1l",1));
            pijacas.Add(new Pijaca("Gin 1l",1));

            return pijacas;

        }

    }
}
