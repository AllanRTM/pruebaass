using kielb.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kielb.Controllers
{
    public class KB_EmpleadosCargaMasivaController : Controller
    {
        // GET: KB_EmpleadosCargaMasiva
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View();
        }


        [HttpPost]
        public ActionResult Index(KB_Empleados datos)
        {

            //ruta del archivo

            string NombreDeArchivo = Path.GetFileName(datos.ruta.FileName);

            string path = Server.MapPath("/");
            string s = Path.Combine(path+ "Files/"+NombreDeArchivo);


            datos.ruta.SaveAs(s);

            if (datos.ruta != null)
            {
                @TempData["Message"] = "Se cargaron correctamente los archivos"+s;
                return RedirectToAction("Index", new { message = "El archivo excel es obligatorio : " + s + " " });
            }

           

            SLDocument sl = new SLDocument(path);

            using (var db = new SITEPLUSEntities())
            {
                basedeDatos n = new basedeDatos();
                //inicia en la segunda fila del archivo excel ya que la primera son nombres de las tablas
                int iRow = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    int sucur = sl.GetCellValueAsInt32(iRow, 1);
                    string codigo = sl.GetCellValueAsString(iRow, 2);
                    int edad = sl.GetCellValueAsInt32(iRow, 3);
                    int mes = sl.GetCellValueAsInt32(iRow, 4);
                    int check = sl.GetCellValueAsInt32(iRow, 5);

                    var oMiExcel = new KB_Empleados();

                    oMiExcel.Suc_id = sucur;
                    oMiExcel.Empleado_id = codigo;
                    oMiExcel.Mes = mes;
                    oMiExcel.Año = edad;
                    oMiExcel.tipo = check;

                    string query = @"INSERT INTO KB_Empleados(Suc_id,Empleado_id,Mes,Año,tipo_id) VALUES(" + oMiExcel.Suc_id + "," + oMiExcel.Empleado_id + "," + oMiExcel.Mes + "," + oMiExcel.Año + "," + oMiExcel.tipo + ")";

                    n.insertarDatos(query);

                    iRow++;
                }

                @TempData["Message"] = "Se cargaron correctamente los archivos";
               
                return RedirectToAction("Index");
            }
        }
    }
}