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
    public class KB_UsuariosKielsaBonosController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();

     
        // GET: KB_UsuariosKielsaBonos
        [Authorize]
        public ActionResult Index()
        {
            return View(db.KB_UsuariosKielsaBonos.ToList());
        }

        // GET: KB_UsuariosKielsaBonos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_UsuariosKielsaBonos kB_UsuariosKielsaBonos = db.KB_UsuariosKielsaBonos.Find(id);
            if (kB_UsuariosKielsaBonos == null)
            {
                return HttpNotFound();
            }
            return View(kB_UsuariosKielsaBonos);
        }

        // GET: KB_UsuariosKielsaBonos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KB_UsuariosKielsaBonos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Empleado_id,Nombre,Sucursal_id,pass,ID_Estado,ID_Rol,email,token_recovery, Emp_id")] KB_UsuariosKielsaBonos kB_UsuariosKielsaBonos)
        {
            if (ModelState.IsValid)
            {
                db.KB_UsuariosKielsaBonos.Add(kB_UsuariosKielsaBonos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kB_UsuariosKielsaBonos);
        }

        // GET: KB_UsuariosKielsaBonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_UsuariosKielsaBonos kB_UsuariosKielsaBonos = db.KB_UsuariosKielsaBonos.Find(id);
            if (kB_UsuariosKielsaBonos == null)
            {
                return HttpNotFound();
            }
            return View(kB_UsuariosKielsaBonos);
        }

        // POST: KB_UsuariosKielsaBonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Empleado_id,Nombre,Sucursal_id,pass,ID_Estado,ID_Rol,email,token_recovery, Emp_id")] KB_UsuariosKielsaBonos kB_UsuariosKielsaBonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kB_UsuariosKielsaBonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kB_UsuariosKielsaBonos);
        }

        // GET: KB_UsuariosKielsaBonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KB_UsuariosKielsaBonos kB_UsuariosKielsaBonos = db.KB_UsuariosKielsaBonos.Find(id);
            if (kB_UsuariosKielsaBonos == null)
            {
                return HttpNotFound();
            }
            return View(kB_UsuariosKielsaBonos);
        }

        // POST: KB_UsuariosKielsaBonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KB_UsuariosKielsaBonos kB_UsuariosKielsaBonos = db.KB_UsuariosKielsaBonos.Find(id);
            db.KB_UsuariosKielsaBonos.Remove(kB_UsuariosKielsaBonos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // ******************************************** Funciones ********************************************
        ////traer info de sociedades
        public JsonResult GetEmpresas()
        {
            string user = Session["Usuario"] as string;
            var Lst1 = db.VSP_Empresas.SqlQuery("select distinct * from VSP_Empresas").ToList();

            var Lista1 = Lst1.Select(x => new SelectListItem
            {
                Value = x.Emp_Id.ToString(),
                Text = x.Emp_Nombre
            }).ToList();
            return Json(Lista1, JsonRequestBehavior.AllowGet);
        }

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

        ////traer info de estado
        public JsonResult GetEstado()
        {
            var Lst3 = db.TBL_Estado_Uuarios.SqlQuery("select distinct * from TBL_Estado_Uuarios").ToList();
            var Lista3 = Lst3.Select(x => new SelectListItem
            {
                Value = x.ID_Estado.ToString(),
                Text = x.Estado
            }).ToList();
            return Json(Lista3, JsonRequestBehavior.AllowGet);
        }

        ////traer info de roles
        public JsonResult GetRoles()
        {
            var Lst4 = db.KB_Mantenimiento_Perfiles_Y_Roles.SqlQuery("select distinct * from KB_Mantenimiento_Perfiles_Y_Roles").ToList();
            var Lista4 = Lst4.Select(x => new SelectListItem
            {
                Value = x.Id_rol.ToString(),
                Text = x.Rol
            }).ToList();
            return Json(Lista4, JsonRequestBehavior.AllowGet);
        }
    }
}

