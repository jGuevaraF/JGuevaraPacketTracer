using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Envio
    {
        //public List<object> Envios { get; set; }
        public ML.Paquete Paquete { get; set; }
        public ML.Repartidor Repartidor { get; set; }
        public int NumeroPaquetes { get; set; }

        public List<ML.Envio> Envios { get; set; }
    }
}
