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

        public string Ip
        {
            get{return ip;}
            set{ip = value;}
        }

        public string User
        {
            get{return user;}
            set{user = value;}
        }

        public string Senha
        {
            get{return senha;}
            set{senha = value;}
        }

        public string Db
        {
            get{ return db; }
            set{db = value;}
        }

        public void setIp(string ip)
        {
            this.ip = ip;
        }
        public void abreConexao()
        {
            mConn = new MySqlConnection("Persist Security Info=False;server=" + ip + ";database="+db+";uid=" + user + ";server=" + ip + ";database=" + db + ";uid=" + user + ";pwd=" + senha);
        }

        public Boolean verificaConexao()
        {
            return mConn.State == ConnectionState.Open;
        }
        //select ***
        public DataTable getCidades()
        {
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if (verificaConexao())
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
            if(verificaConexao())
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
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM veiculo ", mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        //inserts
        public void insertFrete()
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("SELECT * FROM veiculo ", mConn);
            }
        }
        public void insertFreteVeiculo()
        {

        }
        public void insertMarca()
        {

        }
        public void insertModelo()
        {

        }
        public void insertPessoa()
        {

        }
        public void insertTelefone()
        {

        }
        public void insertTipoPessoa()
        {

        }
        public void insertTipoVeiculo()
        {

        }
        public void insertVeiculo()
        {

        }

    }
}
