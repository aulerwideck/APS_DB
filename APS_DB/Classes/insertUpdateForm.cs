using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_DB.Classes
{
    class insertUpdateForm : form
    {
        private string tableName;
        private Banco banco;
        private DataRow editData;
        private List<KeyValuePair<string, string>> pkEdit;
        private column[] data;
        private EventHandler handlerInserido;
        private EventHandler handlerCancelar;
        private EventHandler handlerEditado;

        public void Show(DataRow editData)
        {
            setUpdateData(editData);
            Show();
        }

        public insertUpdateForm(string formName, string tableName, column[] baseData, Banco banco, EventHandler handlerInserido, EventHandler handlerCancelar, EventHandler handlerEditado)
        {
            FormName = formName;
            this.handlerCancelar = handlerCancelar;
            this.handlerInserido = handlerInserido;
            this.handlerEditado = handlerEditado;
            this.tableName = tableName;
            data = new column[baseData.Count()];
            for (int i = 0; i < baseData.Count(); i++) data[i] = new column(baseData[i]);
            this.banco = banco;
            FormName = tableName;
            Panel = new FlowLayoutPanel();
            Panel.Anchor = AnchorStyles.Top;
            Panel.Location = new Point(0, 24);
            Panel.Padding = new Padding(10);
            Panel.Dock = DockStyle.Fill;
            Panel.Visible = true;
            Panel.FlowDirection = FlowDirection.LeftToRight;
            for (int i = 0; i < data.Length; i++)
            {
                if (!data[i].InsertReq) continue;
                var subpnl = new Panel();
                subpnl.Size = new Size(400, 28);
                var label = new Label();
                label.Location = new Point(3, 6);
                label.Text = data[i].FriendlyName;
                var text = new TextBox();
                data[i].Control = text;
                text.Name = "insertUpdateFormField" + data[i].Name;
                text.Location = new Point(100, 3);
                text.Size = new Size(300, 20);
                subpnl.Controls.Add(label);
                subpnl.Controls.Add(text);
                Panel.Controls.Add(subpnl);
            }
            var pnl = new Panel();
            pnl.Size = new Size(1020, 28);
            int bx = 100;
            int of = 70;
            var btn = new Button();
            btn.Location = new Point(bx + 0 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Salvar";
            btn.Click += salvar;
            pnl.Controls.Add(btn);

            btn = new Button();
            btn.Location = new Point(bx + 1 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Cancelar";
            btn.Click += cancelar;
            pnl.Controls.Add(btn);

            btn = new Button();
            btn.Location = new Point(bx + 2 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Limpar Campos";
            btn.Click += limparCampos;
            pnl.Controls.Add(btn);

            Panel.Controls.Add(pnl);
            Panel.Visible = false;
        }
        public void setUpdateData(DataRow row)
        {
            //var nd = new List<KeyValuePair<string, string>>();
            var pk = new List<KeyValuePair<string, string>>();
            editData = row;
            if (row != null)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    var val = row.ItemArray[i].ToString();
                    if (data[i].IsPrimaryKey)
                    {
                        pk.Add(new KeyValuePair<string, string>(data[i].Name, val));
                    }
                    else
                    {
                        //nd.Add(new KeyValuePair<string, string>(data[i].Name, val));
                        data[i].Control.Text = val;
                    }
                }
                pkEdit = pk;
            }
            else
            {
                limpar();
                pkEdit = null;
            }
        }
        private void salvar(object sender, EventArgs e)
        {
            var dados = new List<KeyValuePair<string, string>>();
            foreach (var item in data)
            {
                if (!item.InsertReq) continue;
                dados.Add(new KeyValuePair<string, string>(item.Name, item.Control.Text));
            }
            if (editData == null)
            {
                banco.insert(tableName, dados);
                handlerInserido(sender, e);
            }
            else
            {
                banco.update(tableName, pkEdit, dados);
                handlerEditado(sender, e);
            }
        }
        private void cancelar(object sender, EventArgs e)
        {
            foreach (var item in data)
            {
                if (!item.InsertReq) continue;
                item.Control.Text = string.Empty;
            }
            handlerCancelar(sender, e);
        }
        private void limparCampos(object sender, EventArgs e)
        {
            limpar();
        }
        private void limpar()
        {
            foreach (var item in data)
            {
                if (!item.InsertReq) continue;
                item.Control.Text = string.Empty;
            }
        }
    }
}
