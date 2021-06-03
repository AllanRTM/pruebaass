using kielb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace kielb.Controllers
{
    public class PresupuestoTiendaController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();
        private basedeDatos bd = new basedeDatos();
        // GET: PresupuestoTienda
        public ActionResult Index()
        {
            
            return View(db.KB_Presupuesto_Far.ToList());
        }



        //Metodos de edicion para presupuesto de bono
        //  [Id]
        //,[Suc_id]
        //,[Monto]
        //,[Mes]
        //,[Año]
        //,[checklist]
        public ActionResult Create()
        {
            
           
            mesesss();
            anioss();
            return View();
        }

        public ActionResult RegistriEditar(string mensaje, int identificador)
        {

            ViewBag.Mensaje = mensaje;

            ViewBag.Identificador = identificador;

            return View();

        }

        public ActionResult mesesss()
        {
            ViewBag.Sucursales1 = new SelectList(db.TBL_MESES, "Id_num", "Mes_ID");

            return View();
        }
        public ActionResult anioss()
        {
            ViewBag.Sucursales = new SelectList(db.TBL_Anios, "ID", "Anio");

            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KB_Presupuesto_Far datos)
        {
            
            try
            {
                string query = @"INSERT INTO KB_Presupuesto_Far(Suc_id,Monto,Mes,Año,checklist) VALUES(" + datos.Suc_id + "," + datos.Monto + "," + datos.Mes + "," + datos.Año + "," + datos.checklist + ")";

                if (datos.Suc_id == null && datos.Monto == null && datos.Mes == null && datos.Año == null && datos.checklist == null)
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


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KB_Presupuesto_Far kB_Presupuesto_Far = db.KB_Presupuesto_Far.Find(id);
            if (kB_Presupuesto_Far == null)
            {
                return HttpNotFound();
            }
            return View(kB_Presupuesto_Far);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KB_Presupuesto_Far datos)
        {
           
            try
            {
                string query = @"UPDATE KB_Presupuesto_Far SET Suc_id="+datos.Suc_id+ ",Monto=" + datos.Monto + ",Mes=" + datos.Mes + ", Año=" + datos.Año + ",checklist= " + datos.checklist + " WHERE Id='" + datos.Id + "';";

                if (datos.Suc_id == null && datos.Monto == null && datos.Mes == null && datos.Año == null && datos.checklist == null)
                {
                    return RedirectToAction("Edit", new { message = "Todos los campos son obligatorios" });
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


        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Presupuesto_Far kB_Presupuesto_Far = db.KB_Presupuesto_Far.Find(id);
            if (kB_Presupuesto_Far == null)
            {
                return HttpNotFound();
            }
            return View(kB_Presupuesto_Far);
        }


    }
}