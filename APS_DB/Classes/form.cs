using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_DB.Classes
{
    public class form
    {
        private string formName;
        private FlowLayoutPanel panel;
        public string FormName
        {
            get
            {
                return formName;
            }

            set
            {
                formName = value;
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
        public void Show() { panel.Visible = true; }
        public void Hide() { panel.Visible = false; }
    }
}
