using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kielb.Models;
namespace kielb.Controllers
{
    public class KB_BonoAppController : Controller
    {
        // GET: KB_BonoApp
        public ActionResult Index(string Emp_id)
        {
            List<KB_BonoApp> Faltantes = new List<KB_BonoApp>();


            using (var connection = new SqlConnection("Data Source=172.16.2.125 ;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();

                string sql = "SELECT * FROM [SITEPLUS].[dbo].[KB_Calculo_Bono] WHERE empleado_id = '"+Emp_id+"'";

                if (Emp_id == null)
                {
                    @TempData["Message"] = "Inserte el id del empleado";
                }
                else
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                KB_BonoApp f = new KB_BonoApp();

                                string id = reader[0].ToString();
                                string comision = reader[1].ToString();
                                string CuadroA = reader[2].ToString();
                                string CuadroB = reader[3].ToString();
                                string CuadroC = reader[4].ToString();
                                string Consumo = reader[5].ToString();
                                string Canje = reader[6].ToString();
                                string Checklist = reader[7].ToString();
                                string faltanteInventario = reader[8].ToString();
                                string faltanteCaja = reader[9].ToString();
                                string fecha = reader[10].ToString();
                                string empleadoId = reader[11].ToString();
                                string puesto = reader[12].ToString();
                                string sucursalId = reader[13].ToString();
                                string nombre = reader[14].ToString();

         
                                @TempData["Message"] = "Id del empleado: " + Emp_id+"consulta"+command;
                        

                                    if (comision == "s" || CuadroA == "" || CuadroB == "" || CuadroC == "" || Consumo == "" || Canje == "" || Checklist == "s" || faltanteInventario == ""
                                   || faltanteCaja == "" || fecha == "" || empleadoId == "" || puesto == "" || sucursalId == "" || nombre == "")
                                    {
                                        f.suc_id = 0;
                                        f.empleado_id = "";
                                        f.puesto = "nada";
                                        f.nombre = "";
                                        f.bonoTotal = 0;
                                        f.faltInv = 0;
                                        f.faltCaja = 0;
                                        f.deduccion_total = 0;
                                        f.fecha = DateTime.Now;
                                        f.total = 0;
                                    }
                                    else if (comision == "" || Checklist == "")
                                    {
                                        string comision2 = "0";

                                        f.suc_id = int.Parse(sucursalId);
                                        f.empleado_id = empleadoId;
                                        f.puesto = puesto;
                                        f.nombre = nombre;
                                        f.bonoTotal = decimal.Parse(comision2) + decimal.Parse(CuadroA) + decimal.Parse(CuadroB) + decimal.Parse(CuadroC) + decimal.Parse(Consumo);
                                        f.faltInv = decimal.Parse(faltanteInventario);
                                        f.faltCaja = decimal.Parse(faltanteCaja);
                                        f.deduccion_total = decimal.Parse(faltanteCaja) + decimal.Parse(faltanteInventario);
                                        f.fecha = DateTime.Parse(fecha);
                                        f.total = decimal.Parse(CuadroA) + decimal.Parse(CuadroB) + decimal.Parse(CuadroC) + decimal.Parse(Consumo) - decimal.Parse(faltanteCaja) - decimal.Parse(faltanteInventario);

                                    }
                                    else
                                    {
                                        f.suc_id = 0;
                                        f.empleado_id = "no encontrado";
                                        f.puesto = "";
                                        f.nombre = "";
                                        f.bonoTotal = 0;
                                        f.faltInv = 0;
                                        f.faltCaja = 0;
                                        f.deduccion_total = 0;
                                        f.fecha = DateTime.Now;
                                        f.total = 0;
                                    }

                                Faltantes.Add(f);
                            }

                                
 
                            }

                        }
                    }
                }
            ViewBag.Message = TempData["Message"].ToString();

            return View(Faltantes);
        }
    }
}