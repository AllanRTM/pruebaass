using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kielb.Models
{
    public class VentaTotales
    {
        public int id { get; set; }
        public Nullable<decimal> comision { get; set; }
        public Nullable<decimal> porcentajeComision { get; set; }
        public Nullable<decimal> resultadoComision { get; set; }
        public Nullable<decimal> cuadroA { get; set; }
        public Nullable<decimal> porcentajeA { get; set; }
        public Nullable<decimal> resultadoA { get; set; }
        public Nullable<decimal> cuadroB { get; set; }
        public Nullable<decimal> porcentajeB { get; set; }
        public Nullable<decimal> resultadoB { get; set; }
        public Nullable<decimal> cuadroC { get; set; }
        public Nullable<decimal> porcentajeC { get; set; }
        public Nullable<decimal> resultadoC { get; set; }
        public Nullable<decimal> consumo { get; set; }
        public Nullable<decimal> porcentajeConsumo { get; set; }
        public Nullable<decimal> resultadoConsumo { get; set; }
        public Nullable<decimal> canje { get; set; }
        public Nullable<decimal> porcentajeCanje { get; set; }
        public Nullable<decimal> resultadoCanje { get; set; }
        public Nullable<decimal> checklist { get; set; }
        public Nullable<decimal> porcentajeChecklist { get; set; }
        public Nullable<decimal> resultadochecklist { get; set; }
        public Nullable<decimal> totalComision { get; set; }
        public Nullable<decimal> porcentajeTotalComision { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string empleado_id { get; set; }
        public string puesto { get; set; }
        public Nullable<int> suc_id { get; set; }
        public string nombre { get; set; }
    }
}