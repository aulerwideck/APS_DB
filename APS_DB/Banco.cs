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
        public DataTable getCidades()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM cidade", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getEnderecos()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM endereco", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getEstados()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM estado", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getFretes()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM frete", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getFrete_Veiculo()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM frete_veiculo ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getMarca()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM marca ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }

        public DataTable getModeelos()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM modelo ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getPaises()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM pais ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getPessoa()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM pessoa ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getTelefone()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM telefone ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getTipoPessoa()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM tipoPessoa ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getTipoVeiculo()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM tipoVeiculo ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        public DataTable getVeiculos()
        {
            if (mConn.State == ConnectionState.Open)
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM veiculo ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        //inserts



    }
}
