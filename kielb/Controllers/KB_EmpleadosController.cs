using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kielb.Models;

namespace kielb.Controllers
{
    public class KB_EmpleadosController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();
        //conexion y metodos que se usan esta en la carpeta model y se llama basedeDatos
        basedeDatos bd = new basedeDatos();
        // GET: KB_Empleados
        public ActionResult Index()
        {
            return View(db.KB_Empleados.ToList());
        }

        // GET: KB_Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Empleados kB_Empleados = db.KB_Empleados.Find(id);
            if (kB_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(kB_Empleados);
        }

        // GET: KB_Empleados/Create
        public ActionResult Create()
        {
            mesess();
            añoss();
            sucursales();
            trabajadores();
            return View();
        }

        public ActionResult trabajadores()
        {
            ViewBag.empleado = new SelectList(db.KB_Presupuesto_Dom, "Empleado_id", "Empleado_id");

            return View();
        }

        public ActionResult sucursales()
        {
            ViewBag.sucursal = new SelectList(db.KB_Presupuesto_Far, "Suc_id", "Suc_id");

            return View();
        }

        public ActionResult mesess()
        {
            ViewBag.mes = new SelectList(db.TBL_MESES, "Id_num", "Mes_ID");

            return View();
        }

        public ActionResult añoss()
        {
            ViewBag.anio = new SelectList(db.TBL_Anios, "ID", "Anio");

            return View();
        }


        // POST: KB_Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KB_Empleados datos)
        {
            
            //Estos son los campos que estan en la base de datos
            // [Suc_id]
            //,[Empleado_id]
            //,[Supervisor_id]
            //,[Mes]
            //,[Año]
            //,[tipo]

            
            try
            {
                string query = @"INSERT INTO KB_Empleados(Suc_id,Empleado_id,Supervisor_id,Mes,Año,tipo) VALUES(" + datos.Suc_id + "," + datos.Empleado_id + "," + datos.Mes + "," + datos.Año + "," + datos.tipo + ");";

                if (datos.Suc_id == null && datos.Empleado_id == null && datos.Mes == null && datos.Año == null && datos.tipo == null)
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

        // GET: KB_Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Empleados kB_Empleados = db.KB_Empleados.Find(id);
            if (kB_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(kB_Empleados);
        }

        // POST: KB_Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KB_Empleados datos)
        {
           
            try
            {

                string query = @"UPDATE KB_Empleados SET Suc_id = " + datos.Suc_id + ", Empleado_id = " + datos.Empleado_id + ", Mes = " + datos.Mes + ", Año = " + datos.Año + ", Tipo = " + datos.tipo + " WHERE Empleado_id = '" + datos.Id + "'; ";

                if (datos.Suc_id == null && datos.Empleado_id == null && datos.Mes == null && datos.Año == null && datos.tipo == null)
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

        // GET: KB_Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Empleados kB_Empleados = db.KB_Empleados.Find(id);
            if (kB_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(kB_Empleados);
        }

        // POST: KB_Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KB_Empleados kB_Empleados = db.KB_Empleados.Find(id);
            db.KB_Empleados.Remove(kB_Empleados);
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
