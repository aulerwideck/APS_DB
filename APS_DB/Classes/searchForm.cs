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

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public FlowLayoutPanel Panel
        {
            get
            {
                return panel;
            }

            set
            {
                panel = value;
            }
        }

        public DataGridView Dgv
        {
            get
            {
                return dgv;
            }

            set
            {
                dgv = value;
            }
        }

        public void Show() { panel.Visible = true; }
		public void Hide() { panel.Visible = false; }
	}
}
