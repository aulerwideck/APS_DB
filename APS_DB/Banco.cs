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
            mConn = new MySqlConnection("Persist Security Info=False;server="+ip+";database="+db+";uid="+user+";server = "+ip+"; database = "+db+"; uid = "+user+"; pwd = "+senha+"");
            mConn.Open();
        }

        public Boolean verificaConexao()
        {
            return mConn.State == ConnectionState.Open;
        }
		//select *
		public DataTable get(string table, List<KeyValuePair<string, string>> where = null)
		{
			if (verificaConexao())
			{
				DataTable dt = new DataTable();
				var query = new StringBuilder();
				query.Append(string.Format("SELECT * FROM {0} ", table));
				if (where != null && where.Count > 0)
				{
					query.Append("WHERE ");
					query.Append(string.Format("{0} = \"{1}\" ", where[0].Key, where[0].Value));
					for (int i = 1; i < where.Count; i++)
					{
						query.Append(string.Format("AND {0} = \"{1}\" ", where[i].Key, where[i].Value));
					}
				}
				mAdapter = new MySqlDataAdapter(query.ToString(), mConn);
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
