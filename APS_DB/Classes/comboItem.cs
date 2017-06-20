using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS_DB.Classes
{
    public class comboItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public comboItem(string v, string t)
        {
            Text = t;
            Value = v;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
