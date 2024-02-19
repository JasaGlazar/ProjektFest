using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFest
{
    public class Sank
    {
        public string ime { get; set; }
        public List<Oseba> natakarji { get; set; }
        public  Oseba nosac { get; set; }

        public Sank(string ime)
        { 
            this.ime = ime;
            this.natakarji= new List<Oseba>();
        }
    }
}
