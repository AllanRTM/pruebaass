using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kielb.Models
{
    public class KB_presupuesto_domicilio
    {

        //  [Id]
        //,[Empleado_id]
        //,[Monto]
        //,[Mes]
        //,[Año]
        //,[checklist]
        public string id_general { get; set; }
        public string id_empleado { get; set; }
        public string Monto_empleado { get; set; }
        public string MesEmpleado { get; set; }
        public string AñoEmpleado { get; set; }
        

    }
}