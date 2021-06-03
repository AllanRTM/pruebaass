using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kielb.Models
{
    public class KB_BonoApp
    {
        public int id { get; set; }
        public Nullable<int> suc_id { get; set; }
        public string empleado_id { get; set; }
        public string nombre { get; set; }
        public string puesto { get; set; }
        public Nullable<decimal> bonoTotal { get; set; }
        public Nullable<decimal> porcentajeA { get; set; }
        public Nullable<decimal> faltInv { get; set; }
        public Nullable<decimal> faltCaja { get; set; }
        public Nullable<decimal> deduccion_total { get; set; }
        public DateTime fecha { get; set; }
        public Nullable<decimal> total { get; set; }
    }
}