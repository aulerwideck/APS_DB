using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_DB.Classes
{
	public class column
	{
		private string name;
        private string friendlyName;
        private string fkTableName;
        private bool isFK;
        private bool insertReq;
        private bool isSearchField;
        private bool isPrimaryKey;
        private MySqlType type;
        private int size;

		private Control control;

		public Control Control { get { return control; } set { control = value; } }
		public string Name { get { return name; } set { name = value; } }

        public string FriendlyName { get => friendlyName; set => friendlyName = value; }
        public bool InsertReq { get => insertReq; set => insertReq = value; }
        public bool IsFk { get => isFK; set => isFK = value; }
        public string FkTableName { get => fkTableName; set => fkTableName = value; }
        public bool IsSearchField { get => isSearchField; set => isSearchField = value; }
        internal MySqlType Type { get => type; set => type = value; }
        public int Size { get => size; set => size = value; }
        public bool IsPrimaryKey { get => isPrimaryKey; set => isPrimaryKey = value; }

        public column(column c)
        {
            name = c.name;
            friendlyName = c.friendlyName;
            fkTableName = c.fkTableName;
            IsFk = c.isFK;
            insertReq = c.insertReq;
            isSearchField = c.isSearchField;
            isPrimaryKey = c.isPrimaryKey;
            type = c.type;
            size = c.size;
        }
        public column(string nomeCol)
        {
            name = nomeCol;
        }
        public column(string name, string friendlyName = null, bool insertReq = true, MySqlType type = MySqlType.notNeeded, int size = -1, bool isSearchField = false, bool? isFk = null, string fkTableName = null, bool isPrimaryKey = false)
        {
            Name = name;
            FriendlyName = string.IsNullOrEmpty(friendlyName) ? name: friendlyName;
            InsertReq = insertReq;
            Type = type;
            Size = size;
            IsSearchField = isSearchField;
            IsFk = isFk.HasValue ? isFk.Value : false;
            FkTableName = fkTableName;
            IsPrimaryKey = isPrimaryKey;
        }
    }
}
