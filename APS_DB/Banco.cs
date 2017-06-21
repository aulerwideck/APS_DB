using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Diagnostics;
using APS_DB.Classes;

namespace APS_DB
{
    public class Banco
    {
        private string ip = "";
        private string user = "";
        private string senha = "";
        private string db = "";
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private MySqlCommand mCommand;
        public Dictionary<string, List<string>> tableLabel;
        //Metadados de tabelas e controles associados.
        public Dictionary<string, column[]> meta = new Dictionary<string, column[]> { };
        public Banco(string ip, string senha, string user, string db)
        {

            Ip = ip;
            Senha = senha;
            User = user;
            Db = db;

            tableLabel = new Dictionary<string, List<string>>();
            tableLabel.Add("cidade", new List<string> { "idCidade", "nome" });
            tableLabel.Add("endereco", new List<string> { "idEndereco", "cep", "rua", "numero", "bairro" });
            tableLabel.Add("estado", new List<string> { "idEstado", "nome" });
            tableLabel.Add("frete", new List<string> { "idFrete", "numeroCTe" });
            tableLabel.Add("frete_veiculo", new List<string> { "idFrete, idVeiculo" });
            tableLabel.Add("marca", new List<string> { "idMarca", "descricao" });
            tableLabel.Add("modelo", new List<string> { "idModelo", "descricao" });
            tableLabel.Add("pais", new List<string> { "idPais", "nome" });
            tableLabel.Add("pessoa", new List<string> { "idPessoa", "RazaoSocial" });
            tableLabel.Add("telefone", new List<string> { "idTelefone", "Telefone" });
            tableLabel.Add("tipopessoa", new List<string> { "idTipoPessoa", "Descricao" });
            tableLabel.Add("tipoveiculo", new List<string> { "idTipoVeiculo", "Descricao" });
            tableLabel.Add("veiculo", new List<string> { "idVeiculo", "Placa" });

            //Metadados de tabelas.
            meta.Add("cidade", new column[] {
                new column("idCidade", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idEstado", "Estado", true, MySqlType.msint, 11, true, true, "estado"),
                new column("Nome",null, true, MySqlType.msvarchar, 45, true, false, null)
            });
            meta.Add("endereco", new column[] {
                new column("idEndereco", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoa", "Pessoa", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idCidade", "Cidade", true, MySqlType.msint, 11, true, true, "cidade"),
                new column("Rua", null, true, MySqlType.msvarchar, 45, true),
                new column("Numero", "Número", true, MySqlType.msint, 11, true),
                new column("Complemento", null, false, MySqlType.msvarchar, 45, true),
                new column("Bairro", null, true, MySqlType.msvarchar, 45, true),
                new column("CEP", null, true, MySqlType.mschar, 9, true)
            });
            meta.Add("estado", new column[] {
                new column("idEstado", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPais", "País", true, MySqlType.msint, 11, true, true, "pais"),
                new column("Sigla", null, true, MySqlType.msvarchar, 2, true),
                new column("Nome", null, true, MySqlType.msvarchar, 45, true)
            });
            meta.Add("frete", new column[] {
                new column("idFrete", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoaRemetente", "Remetente", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idPessoaDestinatario", "Destinatário", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idPessoaTomador", "Tomador", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idPessoaMotorista", "Motorista", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("NumeroCTe", "Número CTe", true, MySqlType.msint, 11, true),
                new column("DataEmissao", "Data de Emissão", true, MySqlType.msdate, -1, true),
                new column("DataEntrega", "Data de Entrega", false, MySqlType.msdate, -1, true),
                new column("VlCarga", "Valor Carga", false, MySqlType.msdouble, -1, true),
                new column("VlPedagio", "Valor Pedágio", false, MySqlType.msdouble, -1, true),
                new column("VlFrete", "Valor Frete", true, MySqlType.msdouble, -1, true),
                new column("PesoBruto", "Peso Bruto", false, MySqlType.msdouble, -1, true),
                new column("Finalizado", null, true, MySqlType.msboolean, -1, true),
            });
            meta.Add("frete_veiculo", new column[] {
                new column("idFrete", "Frete", true, MySqlType.msint, 11, true, true, "frete",true),
                new column("idVeiculo", "Veiculo", true, MySqlType.msint, 11, true, true, "veiculo",true),
            });
            meta.Add("marca", new column[] {
                new column("idMarca",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("modelo", new column[] {
                new column("idModelo",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idMarca","Marca",true,MySqlType.msint,11,true,true,"marca"),
                new column("idTipoVeiculo","Tipo Veiculo",true,MySqlType.msint,11,true,true,"tipoveiculo"),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("pais", new column[] {
                new column("idPais",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Sigla",null,true,MySqlType.msvarchar,3,true),
                new column("Nome",null,true,MySqlType.msvarchar,45,true)
            });
            meta.Add("pessoa", new column[] {
                new column("idPessoa",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idTipoPessoa","Tipo Pessoa",true,MySqlType.msint,11,true,true,"tipopessoa"),
                new column("RazaoSocial","Razão Social",true,MySqlType.msvarchar,45,true),
                new column("CpfCnpj","CPF / CNPJ",true,MySqlType.msvarchar,14,true),
                new column("RG",null,false,MySqlType.msvarchar,11,true),
                new column("IE","Inscrição Estadual",false,MySqlType.msvarchar,15,true),
                new column("Email",null,false,MySqlType.msvarchar,45,true),
                new column("DataNasc","Data de Nascimento",false,MySqlType.msdate,-1,true)
            });
            meta.Add("telefone", new column[] {
                new column("idTelefone",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoa","Pessoa",true,MySqlType.msint,11,true,true,"pessoa"),
                new column("Telefone",null,true,MySqlType.msvarchar,15,true)
            });
            meta.Add("tipopessoa", new column[] {
                new column("idTipoPessoa",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("tipoveiculo", new column[] {
                new column("idTipoVeiculo",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("veiculo", new column[] {
                new column("idVeiculo",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idModelo","Modelo",true,MySqlType.msint,11,true,true,"modelo"),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true),
                new column("Renavam",null,true,MySqlType.msvarchar,45,true),
                new column("Placa",null,true,MySqlType.msvarchar,45,true),
                new column("Tara",null,true,MySqlType.msvarchar,45,true),
                new column("CapacidadeKg","Capacidade em Kg",false,MySqlType.msvarchar,45,true),
                new column("CapacidadeM3","Capacidade em M³",false,MySqlType.msvarchar,45,true)
            });
        }
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
            mConn = new MySqlConnection("Persist Security Info=False;server=" + ip + ";database=" + db + ";uid=" + user + ";server = " + ip + "; database = " + db + "; uid = " + user + "; pwd = " + senha + "");
            mConn.Open();
        }

        public Boolean verificaConexao()
        {
            return mConn.State == ConnectionState.Open;
        }
        //select *
        public DataTable get(string table, List<KeyValuePair<string, string>> where = null, List<string> select = null, bool rawData = false)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                var query = new StringBuilder();

                var selects = new StringBuilder();
                var joins = new Dictionary<string, StringBuilder>();
                var joinsSt = new StringBuilder();
                if (select != null && select.Count() > 0)
                {
                    for (int i = 0; i < select.Count(); i++)
                    {
                        selects.Append(string.Format("{0}{1}", select[i], i < select.Count() - 1 ? ", " : ""));
                    }
                }
                else if (rawData)
                {
                    for (int i = 0; i < meta[table].Count(); i++)
                    {
                        selects.Append(string.Format("{0}.{1}{2}", table, meta[table][i].Name, i + 1 < meta[table].Count() ? ", " : ""));
                    }
                }
                else
                {
                    for (int i = 0; i < meta[table].Count(); i++)
                    {
                        if (meta[table][i].IsFK)
                        {

                            var fktable = meta[table][i].FkTableName;
                            var fkTableLabel = tableLabel[fktable];
                            var label = fkTableLabel.Count() > 1 ? fkTableLabel[1] : fkTableLabel[0];
                            var col = meta[fktable].First(x => x.Name.ToLower() == label.ToLower());
                            var varname = string.Format("{0}{1}", fktable, i);
                            selects.Append(string.Format("{0}.{1} as '{2}'{3}", varname, label, meta[table][i].FriendlyName, i + 1 < meta[table].Count() ? ", " : ""));
                            joins.Add(varname, new StringBuilder(string.Format(" join {0} {1} on {1}.{2} = {3}.{4}", fktable, varname, fkTableLabel[0], table, meta[table][i].Name)));
                        }
                        else
                        {
                            selects.Append(string.Format("{0}.{1} as '{2}'{3}", table, meta[table][i].Name, meta[table][i].FriendlyName, i + 1 < meta[table].Count() ? ", " : ""));
                        }
                    }
                    foreach (var item in joins)
                    {
                        joinsSt.Append(item.Value.ToString());
                    }
                }
                query.Append(string.Format("SELECT {0} FROM {1} {2} ", selects.ToString(), table, joinsSt.ToString()));
                if (where != null && where.Count > 0)
                {
                    query.Append("WHERE ");
                    query.Append(string.Format("{2}.{0} = \"{1}\" ", where[0].Key, where[0].Value, table));
                    for (int i = 1; i < where.Count; i++)
                    {
                        query.Append(string.Format("AND {2}.{0} = \"{1}\" ", where[i].Key, where[i].Value, table));
                    }
                }
                Debug.Write(query.ToString());
                mAdapter = new MySqlDataAdapter(query.ToString(), mConn);
                mAdapter.Fill(dt);
                return dt;
            }
            return null;
        }
        //inserts
        public DataTable insert(string table, List<KeyValuePair<string, string>> data)
        {
            if (verificaConexao())
            {
                var columns = new StringBuilder();
                var values = new StringBuilder();
                for (int i = 0; i < data.Count; i++)
                {
                    /*switch mysqldatatype format*/
                    columns.Append(string.Format("{0}{1}", data[i].Key, i < data.Count - 1 ? ", " : ""));
                }
                for (int i = 0; i < data.Count; i++)
                {
                    /*switch mysqldatatype format*/
                    values.Append(string.Format("\'{0}\'{1}", data[i].Value, i < data.Count - 1 ? ", " : ""));
                }
                mCommand = new MySqlCommand(string.Format("INSERT INTO {0} ({1}) VALUES ({2});", table, columns.ToString(), values.ToString()), mConn);
                mCommand.ExecuteNonQuery();
            }
            return null;
        }
        // UPDATES
        public bool update(string tableName, List<KeyValuePair<string, string>> pk, List<KeyValuePair<string, string>> values)
        {
            if (verificaConexao())
            {
                var updates = new StringBuilder();
                var identifier = new StringBuilder();
                for (int i = 0; i < values.Count; i++)
                {
                    var col = meta[tableName].First(x => x.Name == values[i].Key);
                    var val = values[i].Value;
                    switch (col.Type)
                    {
                        case MySqlType.msdate:
                            var sp = val.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                            if (sp.Count() != 3)
                            {
                                return false;
                            }
                            else
                            {
                                val = string.Format("'{0}-{1}-{2}'", sp[2], sp[1], sp[0]);
                            }
                            break;
                        case MySqlType.msboolean:
                            break;
                        default:
                            val = string.Format("'{0}'", val);
                            break;
                    }
                    updates.Append(string.Format("{0} = {1}{2}", values[i].Key, val, i < values.Count - 1 ? ", " : ""));
                }
                for (int i = 0; i < pk.Count; i++)
                {
                    /*switch mysqldatatype format*/
                    identifier.Append(string.Format("{0} = \'{1}\'{2}", pk[i].Key, pk[i].Value, i < pk.Count - 1 ? " AND " : ""));
                }
                var q = string.Format("UPDATE {0} SET {1} WHERE {2};", tableName, updates.ToString(), identifier.ToString());
                mCommand = new MySqlCommand(q, mConn);
                mCommand.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        public void finalizaFrete(int idFrete, DateTime datafim)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("UPDATE frete SET DataEntrega = " + datafim + " where idfrete = " + idFrete, mConn);
            }
        }
        //REMOVE
        public int delete(string table, List<KeyValuePair<string, string>> pk)
        {
            try
            {
                if (verificaConexao())
                {
                    var pks = new StringBuilder();
                    for (int i = 0; i < pk.Count; i++)
                    {
                        pks.Append(string.Format("{0} = \'{1}\'{2}", pk[i].Key, pk[i].Value, i < pk.Count - 1 ? " AND " : ""));
                    }
                    mCommand = new MySqlCommand(string.Format("DELETE FROM {0} WHERE {1};", table, pks.ToString()), mConn);
                    return mCommand.ExecuteNonQuery();
                }
                return -1;
            }
            catch (Exception)
            {
                return -2;
            }
        }
    }
}
