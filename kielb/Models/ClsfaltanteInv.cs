using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kielb.Models
{
    public class ClsfaltanteInv
    {
        public int id { get; set; }
        public int suc_id { get; set; }
        public int bodega_id { get; set; }
        public DateTime fecha_apertura { get; set; }
        public DateTime fecha_aplicacion { get; set; }
        public string estado { get; set; }
        public string comentario { get; set; }
        public int id_movimiento { get; set; }
        public int num_movimiento { get; set; }
        public int aplica { get; set; }
        public int tipo_cobro { get; set; }
        public int tipo_faltante { get; set; }
        public double costo_total { get; set; }

    }
}