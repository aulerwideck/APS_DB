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
        private DataTable dt;
        public void abreConexao()
        {
            mConn = new MySqlConnection("Persist Security Info=False;server=" + ip + ";database=;uid=" + user + ";server=" + ip + ";database=;uid=" + user + ";pwd=" + senha);
        }
        
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
