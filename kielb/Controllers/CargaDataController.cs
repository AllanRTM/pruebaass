using kielb.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kielb.Controllers
{
    public class CargaDataController : Controller
    {
        private SqlConnection con;
        private SqlDataAdapter da;
 
        private SqlCommand cmd;
        // GET: CargaData
        public ActionResult CargaPresupuestoFarmacia()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View();
        }
        [HttpPost]
        public ActionResult CargaPresupuestoFarmacia(KB_Presupuesto_Far datos)
        {
            //compruebo que si se este enviando 

            if (datos.ruta == null)
            {
                @TempData["Message"] = "Debe poner un archivo excel primero.";
                return RedirectToAction("CargaPresupuestoFarmacia", new { message = "El archivo excel es obligatorio " });
            }

            //ruta del archivo
            string NombreDeArchivo = Path.GetFileName(datos.ruta.FileName);

            string path = Server.MapPath("/");
            string s = Path.Combine(path + "Files/" + NombreDeArchivo);

            //guardo el archivo excel para utilizarlo 
            datos.ruta.SaveAs(s);

            //utilizo el archivo que se guardo en la carpeta Files 
            SLDocument sl = new SLDocument(s);

            using (var db = new SITEPLUSEntities())
            {
                basedeDatos n = new basedeDatos();

                //Comienza desde la segunda fila ya que la primera son los nombres de los datos
                int iRow = 2;
                //Compruebo que cada celda este correcta y si es asi se ingresa
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    int sucur = sl.GetCellValueAsInt32(iRow, 1);
                    int montos = sl.GetCellValueAsInt32(iRow, 2);
                    int mess = sl.GetCellValueAsInt32(iRow, 3);
                    int anio = sl.GetCellValueAsInt32(iRow, 4);
                    int check = sl.GetCellValueAsInt32(iRow, 5);
                    int supervisorId = sl.GetCellValueAsInt32(iRow, 6);

                    var oMiExcel = new KB_Presupuesto_Far();

                    oMiExcel.Suc_id = sucur;
                    oMiExcel.Monto = montos;
                    oMiExcel.Mes = mess;
                    oMiExcel.Año = anio;
                    oMiExcel.checklist = check;
                    oMiExcel.supervisor_id = supervisorId;

                    String query2 = @"Select Suc_id, Mes, Año from KB_Presupuesto_Far Where Suc_id = " + oMiExcel.Suc_id + " and Mes = " + oMiExcel.Mes + " and Año = " + oMiExcel.Año + "";

                    
                    basedeDatos mo = new basedeDatos();

                    var domoo = mo.selectDatos(query2);

                    
                    if (domoo == 0)
                    {
                        @TempData["Message"] = "Se cargaron correctamente los archivos";

                        string query = @"INSERT INTO KB_Presupuesto_Far(Suc_id,Monto,Mes,Año,checklist,supervisor_id) VALUES(" + oMiExcel.Suc_id + "," + oMiExcel.Monto + "," + oMiExcel.Mes + "," + oMiExcel.Año + "," + oMiExcel.checklist + "," + oMiExcel.supervisor_id + ")";

                        n.insertarDatos(query);
                    }
                    else
                    {
                        @TempData["Message"] = "Los archivos ya existen";
                    }


                    iRow++;
                }

               

                return RedirectToAction("CargaPresupuestoFarmacia");
            }
        }

        public ActionResult CargaEmpleados()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View();
        }

        [HttpPost]
        public ActionResult CargaEmpleados(KB_Empleados datos)
        {
            if (datos.ruta == null)
            {
                @TempData["Message"] = "Debe poner un archivo excel primero.";
                return RedirectToAction("CargaEmpleados", new { message = "El archivo excel es obligatorio " });
            }

            //ruta del archivo
            string NombreDeArchivo = Path.GetFileName(datos.ruta.FileName);

            string path = Server.MapPath("/");
            string s = Path.Combine(path + "Files/" + NombreDeArchivo);

            //guardo el archivo excel para utilizarlo 
            datos.ruta.SaveAs(s);

            //utilizo el archivo que se guardo en la carpeta Files 
            SLDocument sl = new SLDocument(s);
            using (var db = new SITEPLUSEntities())
            {
                basedeDatos n = new basedeDatos();
                //inicia en la segunda fila del archivo excel ya que la primera son nombres de las tablas
                int iRow = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    int sucur = sl.GetCellValueAsInt32(iRow, 1);
                    string codigo = sl.GetCellValueAsString(iRow, 2);
                    int mes = sl.GetCellValueAsInt32(iRow, 3);
                    int edad = sl.GetCellValueAsInt32(iRow, 4);
                    int check = sl.GetCellValueAsInt32(iRow, 5);
                    int status = sl.GetCellValueAsInt32(iRow, 6);

                    var oMiExcel = new KB_Empleados();

      //              [Suc_id]
                   //,[Empleado_id]
                   //,[Mes]
                   //,[Año]
                   //,[tipo_id]
                   //,[status]


                    oMiExcel.Suc_id = sucur;
                    oMiExcel.Empleado_id = codigo;
                    oMiExcel.Mes = mes;
                    oMiExcel.Año = edad;
                    oMiExcel.tipo = check;
                    oMiExcel.status = status;

                    string qu = "select Suc_id, Empleado_id, Mes, Año from KB_Empleados where Suc_id = "+oMiExcel.Suc_id+ " and Empleado_id =  " + oMiExcel.Empleado_id + " and Mes =  " + oMiExcel.Mes + " and Año =  " + oMiExcel.Año + " ";
                    
                    basedeDatos mo = new basedeDatos();

                    var domoo = mo.selectDatos(qu);
                   
                    if (domoo == 0 )
                    {
                        @TempData["Message"] = "Se cargaron correctamente el archivo";

                        string query = @"INSERT INTO KB_Empleados(Suc_id,Empleado_id,Mes,Año,tipo_id,status) VALUES(" + oMiExcel.Suc_id + "," + oMiExcel.Empleado_id + "," + oMiExcel.Mes + "," + oMiExcel.Año + "," + oMiExcel.tipo + "," + oMiExcel.status + ")";

                        n.insertarDatos(query);
                       
                    }
                    else
                    {
                        @TempData["Message"] = "Ya existen los archivos";
                    }

                    iRow++;
                }

               
                return RedirectToAction("CargaEmpleados");
            }
        }

        private void select()
        {
            throw new NotImplementedException();
        }

        public ActionResult CargaPresupuestoDom()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult CargaPresupuestoDom(KB_Presupuesto_Dom datos)
        {
            if (datos.ruta == null)
            {
                @TempData["Message"] = "Debe poner un archivo excel primero.";
                return RedirectToAction("CargaPresupuestoDom", new { message = "El archivo excel es obligatorio " });
            }

            //ruta del archivo
            string NombreDeArchivo = Path.GetFileName(datos.ruta.FileName);
            //ruta de donde se guarda en el servidor 
            string path = Server.MapPath("/");
            string s = Path.Combine(path + "Files/" + NombreDeArchivo);

            //guardo el archivo excel para utilizarlo 
            datos.ruta.SaveAs(s);

            //utilizo el archivo que se guardo en la carpeta Files 
            SLDocument sl = new SLDocument(s);

            using (var db = new SITEPLUSEntities())
            {
                basedeDatos n = new basedeDatos();
                //inicia en la segunda fila del archivo excel ya que la primera son nombres de las tablas
                int iRow = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    string sucur = sl.GetCellValueAsString(iRow, 1);
                    int monto = sl.GetCellValueAsInt32(iRow, 2);
                    int mes = sl.GetCellValueAsInt32(iRow, 3);
                    int anio = sl.GetCellValueAsInt32(iRow, 4);
                    int check = sl.GetCellValueAsInt32(iRow, 5);

                    var oMiExcel = new KB_Presupuesto_Dom();

                    oMiExcel.Empleado_id = sucur;
                    oMiExcel.Monto = monto;
                    oMiExcel.Mes = mes;
                    oMiExcel.Año = anio;
                    oMiExcel.checklist = check;


                    //,[Empleado_id]
                    //,[Monto]
                    //,[Mes]
                    //,[Año]
                    //,[checklist]

                    string qu = "select Empleado_id, Mes, Año from KB_Presupuesto_Dom where Empleado_id =  " + oMiExcel.Empleado_id + " and Mes =  " + oMiExcel.Mes + " and Año =  " + oMiExcel.Año + " ";

                    basedeDatos mo = new basedeDatos();

                    var domoo = mo.selectDatos(qu);

                    if (domoo == 0)
                    {
                        @TempData["Message"] = "Se cargaron correctamente los archivos";
                        string query = @"INSERT INTO KB_Presupuesto_Dom(Empleado_id,Monto,Mes,Año,checklist) VALUES(" + oMiExcel.Empleado_id + "," + oMiExcel.Monto + "," + oMiExcel.Mes + "," + oMiExcel.Año + "," + oMiExcel.checklist + ")";
                        n.insertarDatos(query);
                    }
                    else
                    {  
                        @TempData["Message"] = "Ya existen los archivos";
                    }

                   
                    iRow++;
                }

                

                return RedirectToAction("CargaPresupuestoDom");
            }
        }

        public ActionResult CargaChecklist()
        {
            return View();
        }
        //CargaPorcentajeComisiones
        public ActionResult CargaPorcentajeComisiones()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View();
        }
        [HttpPost]
        public ActionResult CargaPorcentajeComisiones(KB_Porcentajes datos)
        {
            //compruebo que si se este enviando 

            if (datos.ruta == null)
            {
                @TempData["Message"] = "Debe poner un archivo excel primero.";
                return RedirectToAction("CargaPorcentajeComisiones", new { message = "El archivo excel es obligatorio " });
            }

            //ruta del archivo
            string NombreDeArchivo = Path.GetFileName(datos.ruta.FileName);

            string path = Server.MapPath("/");
            string s = Path.Combine(path + "Files/" + NombreDeArchivo);

            //guardo el archivo excel para utilizarlo 
            datos.ruta.SaveAs(s);

            //utilizo el archivo que se guardo en la carpeta Files 
            SLDocument sl = new SLDocument(s);

            using (var db = new SITEPLUSEntities())
            {
                basedeDatos n = new basedeDatos();

                //Comienza desde la segunda fila ya que la primera son los nombres de los datos
                int iRow = 2;
                //Compruebo que cada celda este correcta y si es asi se ingresa
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    int idPorcentajes = sl.GetCellValueAsInt32(iRow, 1);
                    int cumpli = sl.GetCellValueAsInt32(iRow, 2);
                    int comi = sl.GetCellValueAsInt32(iRow, 3);
                    int cuadroA = sl.GetCellValueAsInt32(iRow, 4);
                    int cuadroB = sl.GetCellValueAsInt32(iRow, 5);
                    int cuadroC = sl.GetCellValueAsInt32(iRow, 6);
                    int consu = sl.GetCellValueAsInt32(iRow, 7);
                    int check = sl.GetCellValueAsInt32(iRow, 8);
                    int mes = sl.GetCellValueAsInt32(iRow, 9);
                    int año = sl.GetCellValueAsInt32(iRow, 10);
                    int canje = sl.GetCellValueAsInt32(iRow, 11);
                    int suc_id = sl.GetCellValueAsInt32(iRow, 12);

                    var oMiExcel = new KB_Porcentajes();

                    //datos que vienen de la base de datos
                    // [id]
                    //,[cumplimiento]
                    //,[comision]
                    //,[cuadroA]
                    //,[cuadroB]
                    //,[cuadroC]
                    //,[consumo]
                    //,[checklist]
                    //,[mes]
                    //,[año]
                    //,[canje]
                    //,[suc_id]

                    oMiExcel.id = idPorcentajes;
                    oMiExcel.cumplimiento = cumpli;
                    oMiExcel.comision = comi;
                    oMiExcel.cuadroA = cuadroA;
                    oMiExcel.cuadroB = cuadroB;
                    oMiExcel.cuadroC = cuadroC;
                    oMiExcel.consumo = consu;
                    oMiExcel.checklist = check;
                    oMiExcel.mes = mes;
                    oMiExcel.año = año;
                    oMiExcel.canje = canje;
                    oMiExcel.suc_id = suc_id;

                    //consulta para verificar si exite si existe no se inserta y pasara al siguiente campo 
                    string qu = "select id, mes, año from KB_Porcentajes where id =  " + oMiExcel.id + " and mes =  " + oMiExcel.mes + " and año =  " + oMiExcel.año + " ";

                    basedeDatos mo = new basedeDatos();

                    var domoo = mo.selectDatos(qu);

                    //si el registro en la base de datos ya existe no lo inserta y para al siguiente
                    if (domoo == 0)
                    {
                        string query = @"INSERT INTO KB_Porcentajes(cumplimiento,comision,cuadroA,cuadroB,cuadroC,consumo,checklist,mes,año,canje,suc_id) VALUES(" + oMiExcel.cumplimiento + "," + oMiExcel.comision + "," + oMiExcel.cuadroA + "," + oMiExcel.cuadroB + "," + oMiExcel.cuadroC + "," + oMiExcel.consumo + "," + oMiExcel.checklist + "," + oMiExcel.mes + "," + oMiExcel.año + "," + oMiExcel.canje + "," + oMiExcel.suc_id + ")";

                        n.insertarDatos(query);

                        @TempData["Message"] = "Se cargaron correctamente los archivos";
                    }
                    else
                    {
                        @TempData["Message"] = "Ya existen los archivos";
                    }


                    iRow++;
                }

               

                return RedirectToAction("CargaPorcentajeComisiones");
            }
        }

        public ActionResult CargaMantenimientoComisiones()
        {
            return View();
        }
    }
}