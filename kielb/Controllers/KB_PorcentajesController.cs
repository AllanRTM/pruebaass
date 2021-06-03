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
    public class KB_PorcentajesController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();

        // GET: KB_Porcentajes
        public ActionResult Index()
        {
            return View(db.KB_Porcentajes.ToList());
        }

        // GET: KB_Porcentajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Porcentajes kB_Porcentajes = db.KB_Porcentajes.Find(id);
            if (kB_Porcentajes == null)
            {
                return HttpNotFound();
            }
            return View(kB_Porcentajes);
        }

        // GET: KB_Porcentajes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KB_Porcentajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cumplimiento,comision,cuadroA,cuadroB,cuadroC,consumo,checklist,mes,año")] KB_Porcentajes kB_Porcentajes)
        {
            if (ModelState.IsValid)
            {
                db.KB_Porcentajes.Add(kB_Porcentajes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kB_Porcentajes);
        }

        // GET: KB_Porcentajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Porcentajes kB_Porcentajes = db.KB_Porcentajes.Find(id);
            if (kB_Porcentajes == null)
            {
                return HttpNotFound();
            }
            return View(kB_Porcentajes);
        }

        // POST: KB_Porcentajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cumplimiento,comision,cuadroA,cuadroB,cuadroC,consumo,checklist,mes,año")] KB_Porcentajes kB_Porcentajes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kB_Porcentajes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kB_Porcentajes);
        }

        // GET: KB_Porcentajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_Porcentajes kB_Porcentajes = db.KB_Porcentajes.Find(id);
            if (kB_Porcentajes == null)
            {
                return HttpNotFound();
            }
            return View(kB_Porcentajes);
        }

        // POST: KB_Porcentajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KB_Porcentajes kB_Porcentajes = db.KB_Porcentajes.Find(id);
            db.KB_Porcentajes.Remove(kB_Porcentajes);
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
