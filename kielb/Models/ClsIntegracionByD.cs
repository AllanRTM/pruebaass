using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kielb.Models
{
    public class ClsIntegracionByD
    {
        public int id { get; set; }
        public int suc_id { get; set; }
        public string empleado_id { get; set; }
        public DateTime fecha { get; set; }
        public string puesto { get; set; }
        public string nombre { get; set; }
        public int faltante_caja { get; set; }
        public int faltante_inventario { get; set; }
        public double deduccion_total { get; set; }
        public double pago_total { get; set; }
        public double bono_total { get; set; }
    }
}