﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kielb.Models;
using System.Web.Security;


namespace kielb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string user_name, string user_pass)
        {
            if (!string.IsNullOrEmpty(user_name) && !string.IsNullOrEmpty(user_pass))
            {
                SITEPLUSEntities db = new SITEPLUSEntities();
                var usuario = db.KB_UsuariosKielsaBonos.FirstOrDefault(e => e.Nombre == user_name && e.pass == user_pass);
                //si el usuario es diferente de null
                if (usuario != null)
                {
                    //se encontró el usuario
                    FormsAuthentication.SetAuthCookie(usuario.Nombre, true);

                    //se almacena el nombre de usuario en una sesion para usar en permisos 
                    Session["Usuario"] = user_name;

                    //Recuperar el rol para permisos, Joaquín Valladares al 19/03/2021
                    var myusuario = (from usu in db.KB_UsuariosKielsaBonos
                                     where usu.Nombre == user_name
                                     select usu).ToList();
                    int? idrol = myusuario[0].ID_Rol;
                    int id = myusuario[0].Id;
                    int? cod_Suc = myusuario[0].Sucursal_id;
                    string cid = Convert.ToString(id);
                    string csuc = Convert.ToString(cod_Suc);
                    Session["Id_Usuario"] = cid;
                    Session["Sucursal"] = csuc;

                    var myrol = (from t in db.KB_UsuariosKielsaBonos
                                 where t.ID_Rol == idrol
                                 select t).ToList();

                    Session["RolId"] = Convert.ToString(myrol[0].ID_Rol);
                    RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    return RedirectToAction("Index", new { message = "Datos Incorrectos para este usuario, por favor verifique de nuevo" });
                }
            }
            else
            {
                return RedirectToAction("Index", new { message = "Todos los campos son obligatorios" });
            }
            return RedirectToAction("Dashboard", "Home");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            //SITEPLUSEntities db = new SITEPLUSEntities();
            //string user_name = Session["Usuario"] as string;

            //List<ClsUsuariosDashboard> lst = (from t in db.VSP_DatosUsuarios
            //                                  where t.nombreUsuario == user_name
            //                                  select new ClsUsuariosDashboard
            //                                  {
            //                                      id = t.id,
            //                                      nombre = t.nombreUsuario,
            //                                      email = t.email,
            //                                      pass = t.pass,
            //                                      sociedades = t.nombre_sociedad,
            //                                      id_sociedad = (int)t.id_sociedad,
            //                                      centros = t.nombre_centro,
            //                                      id_centro = (int)t.id_centro,
            //                                      estado = t.estado,
            //                                      id_estado = (int)t.id_estado,
            //                                      rol = t.rol,
            //                                      id_rol = (int)t.id_rol,
            //                                  }).ToList();
            //return View(lst);
            return View();

        }


        public ActionResult AccesoDenegado()
        {
            ViewBag.Message = "No tienes permisos para ver esta pantalla, por favor regresa a la página anterior. ";

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            HttpCookie c = new HttpCookie("Login");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}


