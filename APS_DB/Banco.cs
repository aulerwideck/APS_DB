using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace APS_DB
{
    class Banco
    {
        private string ip = "";
        private string user = "";
        private string senha = "";
        private string db = "";
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        public void abreConexao()
        {
            mConn = new MySqlConnection("Persist Security Info=False;server=" + ip + ";database=;uid=" + user + ";server=" + ip + ";database=;uid=" + user + ";pwd=" + senha);
        }
        
        //select ***
        public DataTable getPaises()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM Pais ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }

        //inserts

        public void run()
        {
            if (mConn.State == ConnectionState.Open)
            {
                dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM ", mConn);
                mAdapter.Fill(dt);
            }
        }
            
            
              
    }
}
