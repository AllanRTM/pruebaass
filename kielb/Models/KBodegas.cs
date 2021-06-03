using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kielb.Models
{
    public class KBodegas
    {
        
        public short Suc_Id { get; set; }
        public short Bodega_Id { get; set; }
        public int Mov_Id { get; set; }
        public System.DateTime Mov_Fecha { get; set; }
        public System.DateTime Mov_Fecha_Aplicado { get; set; }
        public string Mov_Comentario { get; set; }
        public string Mov_Estado { get; set; }
        public int Mov_Numero { get; set; }
        
    }
}