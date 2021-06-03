using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kielb.Models;

namespace kielb.Controllers
{
    public class KB_VentasAcumuladasTotalController : Controller
    {
        private SITEPLUSEntities db = new SITEPLUSEntities();
        // GET: KB_VentasAcumuladasTotal

        public ActionResult Index(int? datos)
        {
            List<VentaTotales> Faltantes = new List<VentaTotales>();


            using (var connection = new SqlConnection("Data Source=172.16.2.125 ;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                string sql = "SELECT * FROM[SITEPLUS].[dbo].[KB_Calculo_Bono] KCB inner join[SITEPLUS].[dbo].[KB_Porcentajes] KP on KCB.empleado_id = 10000600 and KP.mes = 5 and KP.año = 2021";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            VentaTotales f = new VentaTotales();
                            
                            string sucursal = reader[0].ToString();

                            string comision = reader[1].ToString();
                            string porcentajeComision = reader[17].ToString();

                            string CuadroA= reader[2].ToString();
                            string CuadroAPorcentaje = reader[18].ToString();

                            string CuadroB = reader[3].ToString();
                            string CuadroBPorcentaje = reader[19].ToString();

                            string CuadroC = reader[4].ToString();
                            string CuadroCPorcentaje = reader[20].ToString();

                            string Consumo = reader[5].ToString();
                            string ConsumoPorcentaje = reader[21].ToString();

                            string Canje = reader[6].ToString();
                            string CanjePorcentaje = reader[25].ToString();

                            string Checklist = reader[7].ToString();
                            string ChecklistPorcentaje = reader[22].ToString();



                            f.suc_id = int.Parse(sucursal);

                            if (comision != null)
                            {
                                f.comision = 0;
                                f.resultadoComision = 0;
                            }
                            else
                            {
                                f.comision = decimal.Parse(comision);
                                f.resultadoComision = decimal.Parse(comision) * decimal.Parse(porcentajeComision);
                            }
                            f.porcentajeComision = decimal.Parse(porcentajeComision);

                            f.cuadroA = decimal.Parse(CuadroA);
                            f.porcentajeA = decimal.Parse(CuadroAPorcentaje);
                            f.resultadoA = decimal.Parse(CuadroA) * decimal.Parse(CuadroAPorcentaje);

                            f.cuadroB = decimal.Parse(CuadroB);
                            f.porcentajeB = decimal.Parse(CuadroBPorcentaje);
                            f.resultadoB = decimal.Parse(CuadroB)*decimal.Parse(CuadroBPorcentaje);

                            f.cuadroC = decimal.Parse(CuadroC);
                            f.porcentajeC = decimal.Parse(CuadroCPorcentaje);
                            f.resultadoC = decimal.Parse(CuadroC)*decimal.Parse(CuadroCPorcentaje);

                            f.consumo = decimal.Parse(Consumo);
                            f.porcentajeConsumo = decimal.Parse(ConsumoPorcentaje);
                            f.resultadoConsumo = decimal.Parse(Consumo)*decimal.Parse(ConsumoPorcentaje);

                            f.canje = decimal.Parse(Canje);
                            f.porcentajeCanje = decimal.Parse(CanjePorcentaje);
                            f.resultadoCanje = decimal.Parse(Canje) * decimal.Parse(CanjePorcentaje);

                            if (comision != null)
                            {
                                f.checklist = 0;
                                f.resultadochecklist = 0;
                            }
                            else
                            {
                                f.checklist = decimal.Parse(Checklist);
                                f.resultadochecklist = decimal.Parse(Checklist) * decimal.Parse(ChecklistPorcentaje);
                            }
                            
                            f.porcentajeChecklist = decimal.Parse(ChecklistPorcentaje);
                           

                            f.empleado_id = reader[11].ToString();
                            f.nombre = reader[14].ToString();

                            Faltantes.Add(f);
                        }
                    }
                }
            }
            return View(Faltantes);
        }
            
    }
}