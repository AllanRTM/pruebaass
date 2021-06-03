using ExcelDataReader;
using kielb.Models;
using SpreadsheetLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace kielb.Controllers
{
    public class KB_PresupuestoTiendaMasivoController : Controller

    {
       

        // GET: KB_PresupuestoTiendaMasivo
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(KB_Presupuesto_Far datos)
        {
            //compruebo que si se este enviando 

            if (datos.ruta == null)
            {
                @TempData["Message"] = "Debe poner un archivo excel primero.";
                return RedirectToAction("Index", new { message = "El archivo excel es obligatorio " });
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

                    var oMiExcel = new KB_Presupuesto_Far();

                    oMiExcel.Suc_id = sucur;
                    oMiExcel.Monto = montos;
                    oMiExcel.Mes = mess;
                    oMiExcel.Año = anio;
                    oMiExcel.checklist = check;

                   // String query2 = @"Select Suc_id, Mes, Año from KB_Presupuesto_Far Where Suc_id = " + oMiExcel.Suc_id + " and Mes = " + oMiExcel.Mes + " and Año = " + oMiExcel.Año + "";
                    
                    var d = db.KB_Presupuesto_Far.Where(m => m.Suc_id == oMiExcel.Suc_id && m.Mes == oMiExcel.Mes && m.Año == oMiExcel.Año);

                    if (d == null)
                    {
                        @TempData["Message"] = "Espere por favor...";

                        string query = @"INSERT INTO KB_Presupuesto_Far(Suc_id,Monto,Mes,Año,checklist) VALUES(" + oMiExcel.Suc_id + "," + oMiExcel.Monto + "," + oMiExcel.Mes + "," + oMiExcel.Año + "," + oMiExcel.checklist + ")";

                        n.insertarDatos(query);
                    }


                    iRow++;
                }

                @TempData["Message"] = "Se cargaron correctamente los archivos";

                return RedirectToAction("Index");
            }



        }
    }
}