using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kielb.Models;
using static kielb.Models.basedeDatos;

namespace kielb.Controllers
{
    public class KB_Presupuesto_DomController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();

        
        basedeDatos bd = new basedeDatos();

        
        // GET: KB_Presupuesto_Dom
        public ActionResult Index()
        {
            return View(db.KB_Presupuesto_Dom.ToList());
        }

        // GET: KB_Presupuesto_Dom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Presupuesto_Dom kB_Presupuesto_Dom = db.KB_Presupuesto_Dom.Find(id);
            if (kB_Presupuesto_Dom == null)
            {
                return HttpNotFound();
            }
            return View(kB_Presupuesto_Dom);
        }

        // GET: KB_Presupuesto_Dom/Create
        public ActionResult Create()
        {
            nuevo();
            me();
            return View();
        }

        

        public ActionResult me()
        {
            ViewBag.Sucursales1 = new SelectList(db.TBL_MESES, "Id_num", "Mes_ID");

            return View();
        }
       
        public ActionResult nuevo()
        {
            ViewBag.Sucursales = new SelectList(db.TBL_Anios, "ID", "Anio");

            return View();
        }


        




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KB_Presupuesto_Dom datos)
        {
           
            try
            {
                string query = @"INSERT INTO KB_Presupuesto_Dom(Empleado_id,Monto,Mes,Año,checklist) VALUES(" + datos.Empleado_id + "," + datos.Monto + "," + datos.Mes + "," + datos.Año + "," + datos.checklist + ")";

                if (datos.Empleado_id == null && datos.Monto == null && datos.Mes == null && datos.Año == null && datos.checklist == null)
                {
                    return RedirectToAction("Create", new { message = "Todos los campos son obligatorios" });
                }

                else
                {
                    bd.insertarDatos(query);

                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }
       
     
        
        // GET: KB_Presupuesto_Dom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Presupuesto_Dom kB_Presupuesto_Dom = db.KB_Presupuesto_Dom.Find(id);
            if (kB_Presupuesto_Dom == null)
            {
                return HttpNotFound();
            }
            return View(kB_Presupuesto_Dom);
        }

        // POST: KB_Presupuesto_Dom/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KB_Presupuesto_Dom datos)
        {
            try
            {
                string query = @"UPDATE KB_Presupuesto_Dom SET Empleado_id =" + datos.Empleado_id+" , Monto = " + datos.Monto+", Mes = "+datos.Mes+ ", Año = " + datos.Año + ", checklist = " + datos.checklist + " WHERE Id = '" + datos.Id + "'; ";
               
                if (datos.Empleado_id == null && datos.Monto == null && datos.Mes == null && datos.Año == null && datos.checklist == null)
                {
                    return RedirectToAction("Create", new { message = "Todos los campos son obligatorios" });
                }

                else
                {
                    bd.insertarDatos(query);

                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        // GET: KB_Presupuesto_Dom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Presupuesto_Dom kB_Presupuesto_Dom = db.KB_Presupuesto_Dom.Find(id);
            if (kB_Presupuesto_Dom == null)
            {
                return HttpNotFound();
            }
            return View(kB_Presupuesto_Dom);
        }

        // POST: KB_Presupuesto_Dom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KB_Presupuesto_Dom kB_Presupuesto_Dom = db.KB_Presupuesto_Dom.Find(id);
            db.KB_Presupuesto_Dom.Remove(kB_Presupuesto_Dom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
