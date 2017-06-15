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
            get { return ip; }
            set { ip = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public string Db
        {
            get { return db; }
            set { db = value; }
        }

        public void setIp(string ip)
        {
            this.ip = ip;
        }
        public void abreConexao()
        {
            mConn = new MySqlConnection("Persist Security Info=False;server=" + ip + ";database=" + db + ";uid=" + user + ";server=" + ip + ";database=" + db + ";uid=" + user + ";pwd=" + senha);
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
            if (verificaConexao())
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
        public void insertFrete(int idRemetente, int idDestinatário, int idTomador, int idMotorista, int numCTE, DateTime dataEmissão, double vlCarga, double vlPedagio, double vlFrete, double pesoBruto)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO frete" +
                                            "(`idPessoaRemetente`," +
                                            "`idPessoaDestinario`," +
                                            "`idPessoaTomador`," +
                                            "`idPessoaMotorista`," +
                                            "`NumeroCTe`," +
                                            "`DataEmissao`," +
                                            "`VlCarga`," +
                                            "`VlPedagio`," +
                                            "`VlFrete`," +
                                            "`PesoBruto`," +
                                            "`Finalizado`)" +
                                            "VALUES(" +
                                            idRemetente + "," +
                                            idDestinatário + "," +
                                            idTomador + "," +
                                            idMotorista + "," +
                                            numCTE + "," +
                                            dataEmissão + "," +
                                            vlCarga + "," +
                                            vlPedagio + "," +
                                            vlFrete + "," +
                                            pesoBruto + "," +
                                            "0); ", mConn);
            }
        }
        public void insertFreteVeiculo(int idFrete, int idVeiculo)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO frete_veiculo (`Frete_idFrete`,`Veiculo_idVeiculo`)VALUES(" + idFrete + "," + idVeiculo + ")", mConn);
            }
        }
        public void insertMarca(string descricao)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO marca (`Descricao`)VALUES(" + descricao + ")", mConn);
            }
        }
        public void insertModelo(int idMarca,int idTipoVeiculo,string descricao)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO modelo (`idMarca`,`idTipoVeiculo`,`Descricao`)VALUES("+idMarca+","+idTipoVeiculo+"," + descricao + ")", mConn);
            }
        }
        public void insertPessoa(int idTipoPessoa,string nomeRS,int cpfCnpj,int RG,int IE,string email, DateTime nasc)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO pessoa (`idTipoPessoa`,`RazaoSocial`,`CpfCpnj`,`RG`,`IE`,`Email`,`DataNasc`))VALUES("+idTipoPessoa+","+nomeRS+","+cpfCnpj+","+RG+","+IE+","+email+","+nasc+")", mConn);
            }
        }
        public void insertTelefone(int idPessoa,string telefone)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO telefone (`idPessoa`,`Telefone`)VALUES(" + idPessoa + "," + telefone+")",mConn);
            }
        }
        public void insertTipoVeiculo(string descricao)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO TipoVeiculo (`Descricao`) VALUES ("  + descricao + ")", mConn);
            }
        }
        public void insertVeiculo(int idModelo,string descricao,string renavam,string placa, string tara, string capkg,string capm3)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("INSERT INTO Veiculo (`idModelo`,`Descricao`,`Renavam`,`Placa`,`Tara`,`CapacidadeKg`,`CapacidadeM3`) VALUES ("+idModelo+"," + descricao + ","+renavam+","+placa+","+tara+","+capkg+","+capm3+")", mConn);
            }
        }
        // UPDATES
        public void finalizaFrete(int idFrete,DateTime datafim)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("UPDATE frete SET DataEntrega = "+datafim+" where idfrete = "+idFrete, mConn);
            }
        }
    }
}
