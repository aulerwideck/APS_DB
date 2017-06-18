using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Diagnostics;

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
        public void update(string tableName, List<KeyValuePair<string, string>> pk, List<KeyValuePair<string, string>> values)
        {
            if (verificaConexao())
            {
                var updates = new StringBuilder();
                var identifier = new StringBuilder();
                for (int i = 0; i < values.Count; i++)
                {
                    updates.Append(string.Format("{0} = \'{1}\'{2}", values[i].Key, values[i].Value, i < values.Count - 1 ? ", " : ""));
                }
                for (int i = 0; i < pk.Count; i++)
                {
                    /*switch mysqldatatype format*/
                    identifier.Append(string.Format("{0} = \'{1}\'{2}", pk[i].Key, pk[i].Value, i < pk.Count - 1 ? " AND " : ""));
                }
                mCommand = new MySqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2};", tableName, updates.ToString(), identifier.ToString()), mConn);
                mCommand.ExecuteNonQuery();
            }
        }
        public void finalizaFrete(int idFrete,DateTime datafim)
        {
            if (verificaConexao())
            {
                DataTable dt = new DataTable();
                mAdapter = new MySqlDataAdapter("UPDATE frete SET DataEntrega = "+datafim+" where idfrete = "+idFrete, mConn);
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
            catch (Exception ex)
            {
                return -2;
            }
        }
    }
}
