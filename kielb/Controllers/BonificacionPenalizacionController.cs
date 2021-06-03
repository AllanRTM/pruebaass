using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kielb.Models;
using System.Globalization;


namespace kielb.Controllers
{
    public class BonificacionPenalizacionController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();
        // GET: BonificacionPenalizacion
        public ActionResult CalculoVAC_Auditor()
        {
            return View();
        }

        public ActionResult CalculoJF_turno_Coord_1_2_Auditor()
        {
            return View();
        }

        public ActionResult CalculoSup_Farm_Auditor()
        {
            return View();
        }

        public ActionResult CalculoOp_call_center_Auditor()
        {
            return View();
        }

        public ActionResult CalculoSup_call_center_Auditor()
        {
            return View();
        }

        public ActionResult CalculoFaltantes_inventario_Auditor()
        {
            return View();
        }

        public ActionResult ConsultasAuditor()
        {
            return View();
        }

        //Para el empleado
        public ActionResult ConsultasEmpleado()
        {
            return View();
        }
        public ActionResult CalculoVAC_Empleado()
        {
            return View();
        }

        public ActionResult CalculoJF_turno_Coord_1_2_Empleado()
        {
            return View();
        }

        public ActionResult CalculoSup_Farm_Empleado()
        {
            return View();
        }

        public ActionResult CalculoOp_call_center_Empleado()
        {
            return View();
        }

        public ActionResult CalculoSup_call_center_Empleado()
        {
            return View();
        }

        public ActionResult CalculoFaltantes_inventario_Empleado()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // ******************************************** Funciones ********************************************
        
        public JsonResult GetMeses()
        {
            List<SelectListItem> Lst = (from m in db.TBL_MESES
                                        orderby m.Mes_ID
                                        select new SelectListItem
                                        { Value = m.Mes_ID.ToString(), Text = m.Nombre_mes }).ToList();
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnios()
        {
            List<SelectListItem> Lst = (from a in db.TBL_Anios
                                        orderby a.ID
                                        select new SelectListItem
                                        { Value = a.ID.ToString(), Text = a.Anio.ToString() }).Distinct().ToList();
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVendedores(int suc_id)
        {
            var Lst = db.VSP_KB_Vendedores_x_pais.SqlQuery("select distinct cn.*, s.Suc_Nombre from VSP_KB_Vendedores_x_pais as CN,  VSP_TR_SUCURSAL as s where cn.Suc_Id = s.Suc_Id and cn.Suc_Id= {0}", suc_id).ToList();
            var Lista = Lst.Select(x => new SelectListItem
            {
                Value = x.Vendedor_Id.ToString(),
                Text = x.Vendedor_Nombre_Completo
            }).ToList();
            return Json(Lista, JsonRequestBehavior.AllowGet);
        }
    }
}
