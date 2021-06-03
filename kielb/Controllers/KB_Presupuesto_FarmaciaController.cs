using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kielb.Models;

namespace kielb.Controllers
{
    public class KB_Presupuesto_FarmaciaController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: KB_Presupuesto_Farmacia
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "Data source=MSSQLLocalDB; database=KielsaBase; integrated security= SSPI; ";
        }
        [HttpPost]
        //los datos vienen de la carpeta models y el archivo se llama KB_Presupuesto_Farmacia el cual contiene el tipo de dato 
        //y sus get y set.

        public ActionResult verify(KB_Presupuesto_Farmacia datos)
        {
            //Data Source=172.16.2.125;User ID=sateliteadmin;Password=********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Insert into KB_presupuesto_farmacia values ('" +datos.Nombre + "','" +datos.Monto + "','"+ datos.Año + "', '" + datos.Mes+ "') ";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return RedirectToAction("KB_presupuesto", new { message = "Datos Correctos" });

            }
            else
            {
                con.Close();
                return RedirectToAction("KB_presupuesto", new { message = "Datos Incorrectos para insertar, por favor verifique" });

            }

        }
    }
}