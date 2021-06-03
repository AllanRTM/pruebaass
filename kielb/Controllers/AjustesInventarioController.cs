using kielb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kielb.Controllers
{
    public class AjustesInventarioController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();

        // GET: AjustesInventario
        //public ActionResult Index()
        //{
        //    return View();
        //}


        public ActionResult Index(string Emp_id, int? Sucursal_id, int? Sucursal_id2, DateTime? finicial, DateTime? ffinal, int? Tipo_id)
        {
            List<ClsfaltanteInv> lst = (from f in db.KB_CuadroFaltINV
                                    where f.suc_id >= Sucursal_id && f.suc_id <= Sucursal_id2
                                    && f.fecha_apertura >= finicial && f.fecha_apertura <= ffinal 
                                    && f.tipo_cobro == Tipo_id
                                    select new ClsfaltanteInv
                                    {
                                        suc_id = (int)f.suc_id,
                                        bodega_id = (int)f.bodega_id,
                                        fecha_apertura = (DateTime)f.fecha_apertura,
                                        fecha_aplicacion = (DateTime)f.fecha_aplicacion,
                                        estado = f.estado,
                                        comentario = f.comentario,
                                        id_movimiento = (int)f.id_mov,
                                        num_movimiento = (int)f.Num_Mov,
                                        aplica = (int)f.aplica,
                                        tipo_cobro = (int)f.tipo_cobro,
                                        tipo_faltante = (int)f.tipo_faltante,
                                        costo_total = (double)f.costo_total,
                                    }).ToList();
            return View(lst);
        }

        [HttpPost]
        public ActionResult filtrarLista(string Emp_id, int? Sucursal_id, int? Sucursal_id2, DateTime? finicial, DateTime? ffinal, int? Tipo_id)
        {
            if (!string.IsNullOrEmpty(Emp_id))
            {
                List<ClsfaltanteInv> lst = (from f in db.KB_CuadroFaltINV
                                            where f.suc_id >= Sucursal_id && f.suc_id <= Sucursal_id2
                                            && f.fecha_apertura >= finicial && f.fecha_apertura <= ffinal
                                            && f.tipo_cobro == Tipo_id
                                            select new ClsfaltanteInv
                                            {
                                                suc_id = (int)f.suc_id,
                                                bodega_id = (int)f.bodega_id,
                                                fecha_apertura = (DateTime)f.fecha_apertura,
                                                fecha_aplicacion = (DateTime)f.fecha_aplicacion,
                                                estado = f.estado,
                                                comentario = f.comentario,
                                                id_movimiento = (int)f.id_mov,
                                                num_movimiento = (int)f.Num_Mov,
                                                aplica = (int)f.aplica,
                                                tipo_cobro = (int)f.tipo_cobro,
                                                tipo_faltante = (int)f.tipo_faltante,
                                                costo_total = (double)f.costo_total,
                                            }).ToList();
                return View(lst);
            }
            else
            {
                List<ClsfaltanteInv> lst = (from f in db.KB_CuadroFaltINV
                                            where f.suc_id >= Sucursal_id && f.suc_id <= Sucursal_id2
                                            && f.fecha_apertura >= finicial && f.fecha_apertura <= ffinal
                                            && f.tipo_cobro == Tipo_id
                                            select new ClsfaltanteInv
                                            {
                                                suc_id = (int)f.suc_id,
                                                bodega_id = (int)f.bodega_id,
                                                fecha_apertura = (DateTime)f.fecha_apertura,
                                                fecha_aplicacion = (DateTime)f.fecha_aplicacion,
                                                estado = f.estado,
                                                comentario = f.comentario,
                                                id_movimiento = (int)f.id_mov,
                                                num_movimiento = (int)f.Num_Mov,
                                                aplica = (int)f.aplica,
                                                tipo_cobro = (int)f.tipo_cobro,
                                                tipo_faltante = (int)f.tipo_faltante,
                                                costo_total = (double)f.costo_total,
                                            }).ToList();
                return View(lst);
            }
        }



        //funciones de llenado del combobox
        ////traer info de centros
        public JsonResult GetSuc(int emp_id)
        {
            var Lst2 = db.VSP_TR_SUCURSAL.SqlQuery("select distinct cn.* from VSP_TR_SUCURSAL as CN, VSP_Empresas S where cn.Emp_id = {0}", emp_id).ToList();
            var Lista2 = Lst2.Select(x => new SelectListItem
            {
                Value = x.Suc_Id.ToString(),
                Text = x.Suc_Nombre
            }).ToList();
            return Json(Lista2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFaltante(int emp_id)
        {
            var Lst2 = db.VSP_KB_NombreFaltantes.SqlQuery("select distinct cn.* from VSP_KB_NombreFaltantes as CN, VSP_Empresas S where cn.Emp_id = {0}", emp_id).ToList();
            var Lista2 = Lst2.Select(x => new SelectListItem
            {
                Value = x.Tipo_Id.ToString(),
                Text = x.Tipo_Nombre
            }).ToList();
            return Json(Lista2, JsonRequestBehavior.AllowGet);
        }
    }
}