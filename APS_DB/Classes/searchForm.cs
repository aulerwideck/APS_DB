using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_DB.Classes
{
	public class searchForm
	{
		private string name;
		private FlowLayoutPanel panel;
		private DataGridView dgv;

		public DataGridView Dgv { get => dgv; set => dgv = value; }
		public string Name { get => name; set => name = value; }
		public FlowLayoutPanel Panel { get => panel; set => panel = value; }

		public void Show() { panel.Visible = true; }
		public void Hide() { panel.Visible = false; }
	}
}
