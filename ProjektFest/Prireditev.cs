using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFest
{
    public class Prireditev
    {
        public string ime_prireditve { get; set; }
        public string leto_prireditve { get; set; }
        public List<Pijaca> seznam_pijace { get; set; }
        public List<Sank> sanki { get; set; }

        public Prireditev() 
        { 
            this.sanki= new List<Sank>();
            this.seznam_pijace = new List<Pijaca>();
        } 

    }
}
