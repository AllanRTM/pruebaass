using kielb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kielb.Controllers
{
    public class KB_BodegasController : Controller
    {
        //List<VSP_KB_TipoFaltantes_X_Pais> nuevo = db.VSP_KB_TipoFaltantes_X_Pais.ToList();
        private SITEPLUSEntities db = new SITEPLUSEntities();
        // GET: KB_Bodegas
        public ActionResult Index(int? Sucursal_id, int? Sucursal_id2, DateTime? finicial, DateTime? ffinal)
        {
            List<VSP_KB_TipoFaltantes_X_Pais> Faltantes = new List<VSP_KB_TipoFaltantes_X_Pais>();

            if (Sucursal_id == null && Sucursal_id2 == null && finicial == null && ffinal == null) {
                
                using (var connection = new SqlConnection("Data Source=172.16.2.125 ;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    connection.Open();
                    string sql = "SELECT [Suc_Id] ,[Bodega_Id] ,[Mov_Id] ,[Mov_Fecha] ,[Mov_Fecha_Aplicado] ,[Mov_Comentario] ,[Mov_Estado] ,[Mov_Numero] FROM[SITEPLUS].[dbo].[VSP_KB_TipoFaltantes_X_Pais]";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                VSP_KB_TipoFaltantes_X_Pais f = new VSP_KB_TipoFaltantes_X_Pais();

                                f.Suc_Id = (short)reader["Suc_Id"];
                                f.Bodega_Id = (short)reader["Bodega_Id"];
                                f.Mov_Fecha = (DateTime)reader["Mov_Fecha"];
                                f.Mov_Fecha_Aplicado = (DateTime)reader["Mov_Fecha_Aplicado"];
                                f.Mov_Estado = (string)reader["Mov_Estado"];
                                f.Mov_Comentario = (string)reader["Mov_Comentario"];
                                f.Mov_Id = (int)reader["Mov_Id"];
                                f.Mov_Numero = (int)reader["Mov_Numero"];

                                Faltantes.Add(f);
                            }
                        }
                    }
                }
            }
            else
            {
                using (var connection = new SqlConnection("Data Source=172.16.2.125 ;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    connection.Open();
                    string sql = "SELECT [Suc_Id] ,[Bodega_Id] ,[Mov_Id] ,[Mov_Fecha] ,[Mov_Fecha_Aplicado] ,[Mov_Comentario] ,[Mov_Estado] ,[Mov_Numero] FROM[SITEPLUS].[dbo].[VSP_KB_TipoFaltantes_X_Pais] where Suc_Id >= "+Sucursal_id+" and Suc_Id <= "+Sucursal_id2+"";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                VSP_KB_TipoFaltantes_X_Pais f = new VSP_KB_TipoFaltantes_X_Pais();

                                f.Suc_Id = (short)reader["Suc_Id"];
                                f.Bodega_Id = (short)reader["Bodega_Id"];
                                f.Mov_Fecha = (DateTime)reader["Mov_Fecha"];
                                f.Mov_Fecha_Aplicado = (DateTime)reader["Mov_Fecha_Aplicado"];
                                f.Mov_Estado = (string)reader["Mov_Estado"];
                                f.Mov_Comentario = (string)reader["Mov_Comentario"];
                                f.Mov_Id = (int)reader["Mov_Id"];
                                f.Mov_Numero = (int)reader["Mov_Numero"];

                                Faltantes.Add(f);
                            }
                        }
                    }
                }
            }
            
           

            return View(Faltantes);
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