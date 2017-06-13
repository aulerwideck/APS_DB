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
        private string ip = "108.179.253.205";
        private string user = "trans548_rastrea";
        private string senha = "515cp44r4r453cp";
        private string db = "trans548_rastreador";
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
