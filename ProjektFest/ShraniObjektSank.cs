using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjektFest
{
    public class ShraniObjektSank
    {
        public string imePrireditve {  get; set; }
        public string letoPrireditve {  set; get; }
        public string sank {  set; get; }
        public List<Oseba> kelnarji { get; set; }
        public Oseba nosac {  get; set; }

        public DataTable Komora { get; set; }
        public DataTable NosacDataTable { get; set; }
        public DataTable Rezultat {  get; set; }
        public double SumiranaVrednost {  get; set; }
        public double SuminaraVrednostNosac { get; set; }
        public double SuminaraVrednostRazlika { get; set; }

    }
}
