using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;

namespace kielb.Models
{
    public class basedeDatos
    {
        private SqlConnection con;
        private SqlDataAdapter da;
        static DataTable dt;
        private SqlCommand cmd;

        
        
       
        public void Conectar()
        {
            con = new SqlConnection("Data Source=172.16.2.125 ;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
        }

        public void insertarDatos(String query)
        {
            con = new SqlConnection("Data Source=172.16.2.125;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            SqlCommand comando = new SqlCommand(query,con);
            comando.ExecuteNonQuery();
        }

        public int selectDatos(string query)
        {
            con = new SqlConnection("Data Source=172.16.2.125 ;database=SITEPLUS;User ID=sateliteadmin;Password=K!3l$a#2018;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
           
            con.Open();
            
            string qu = query;

            SqlCommand comando = new SqlCommand(qu, con);

            int m = Convert.ToInt32(comando.ExecuteScalar());

            return m;
        }

        public void Desconectar()
        {
            con.Close();
        }

        public void CrearComando(string consulta)
        {
            cmd = new SqlCommand(consulta, con);
        }

        public void AsignarParametro(string param, SqlDbType tipo, object val)
        {
            cmd.Parameters.Add(param, tipo).Value = val;
        }

        public int EjecutarConsulta()
        {
            return cmd.ExecuteNonQuery();
        }

        public DataTable EjecutarDataTable()
        {
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public class MobileViewModel 
{          
    public List<TBL_MESES> MobileList;
    public SelectList Vendor { get; set; }
    public string SelectedVendor {get;set;}
}
    }
}