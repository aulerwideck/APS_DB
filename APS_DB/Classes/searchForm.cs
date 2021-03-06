﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APS_DB.Classes
{
    public class searchForm : form
    {
        private string tableName;
        private Banco banco;
        private column[] data;
        private DataGridView dgv;
        private event stringDelegate handlerInserir;
        private event stringDelegate handlerEditar;
        private event datarowDelegate handlerRemover;
        public delegate void stringDelegate(string name);
        public delegate void datarowDelegate(DataRow data, string tableName, column[] meta);
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

        public override void Show()
        {
            base.Show();
            pesquisar();
        }

        public searchForm(string formName, string tableName, column[] baseData, Banco banco, stringDelegate handlerInserir, stringDelegate handlerEditar, datarowDelegate handlerRemover)
        {
            FormName = FormName;
            this.handlerInserir = handlerInserir;
            this.handlerEditar = handlerEditar;
            this.handlerRemover = handlerRemover;
            this.tableName = tableName;

            data = new column[baseData.Count()];
            for (int i = 0; i < baseData.Count(); i++) data[i] = new column(baseData[i]);
            this.banco = banco;
            Panel = new FlowLayoutPanel();
            Panel.Anchor = AnchorStyles.Top;
            Panel.Location = new Point(0, 24);
            Panel.Padding = new Padding(10);
            Panel.Dock = DockStyle.Fill;
            Panel.Visible = true;
            Panel.FlowDirection = FlowDirection.LeftToRight;
            for (int i = 0; i < data.Length; i++)
            {
                if (!data[i].IsSearchField) continue;
                var subpnl = new Panel();
                subpnl.Size = new Size(400, 28);
                var label = new Label();
                label.Location = new Point(3, 6);
                label.Text = data[i].FriendlyName;

                Control control = null;
                if (!data[i].IsFK)
                {
                    var text = new TextBox();
                    data[i].Control = text;
                    text.Name = "searchFormField" + data[i].Name;
                    text.Location = new Point(100, 3);
                    text.Size = new Size(300, 20);
                    control = text;
                }
                else
                {
                    var ddl = new ComboBox();
                    ddl.DisplayMember = "Text";
                    ddl.ValueMember = "Value";
                    data[i].Control = ddl;
                    ddl.DropDownStyle = ComboBoxStyle.DropDownList;
                    var ddldata = banco.get(data[i].FkTableName, null, banco.tableLabel[data[i].FkTableName]);
                    var ddlConvert = new List<comboItem>();
                    ddlConvert.Add(new comboItem("0", "[Todos]"));
                    for (int j = 0; j < ddldata.Rows.Count; j++)
                    {
                        var sb = new StringBuilder();
                        for (int k = 0; k < ddldata.Rows[j].ItemArray.Count(); k++)
                        {
                            sb.Append(string.Format("{0}{1}", ddldata.Rows[j][k], k + 1 < ddldata.Rows[j].ItemArray.Count() ? " - " : ""));
                        }
                        var val = ddldata.Rows[j].ItemArray[0].ToString();
                        var tex = sb.ToString();
                        ddlConvert.Add(new comboItem(val, string.IsNullOrEmpty(tex) ? val : tex));
                    }
                    ddl.DataSource = ddlConvert;
                    ddl.Location = new Point(100, 3);
                    ddl.Size = new Size(300, 20);
                    control = ddl;
                }
                subpnl.Controls.Add(label);
                subpnl.Controls.Add(control);
                Panel.Controls.Add(subpnl);
            }
            var pnl = new Panel();
            pnl.Size = new Size(1020, 28);
            int bx = 100;
            int of = 70;
            var btn = new Button();
            btn.Location = new Point(bx + 0 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Pesquisar";
            btn.Click += pesquisar;
            pnl.Controls.Add(btn);

            btn = new Button();
            btn.Location = new Point(bx + 1 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Novo";
            btn.Click += inserir;
            pnl.Controls.Add(btn);

            btn = new Button();
            btn.Location = new Point(bx + 2 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Editar";
            btn.Click += editar;
            pnl.Controls.Add(btn);

            btn = new Button();
            btn.Location = new Point(bx + 3 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Remover";
            btn.Click += remover;
            pnl.Controls.Add(btn);

            btn = new Button();
            btn.Location = new Point(bx + 4 * of, 0);
            btn.Size = new Size(of - 10, 23);
            btn.Text = "Limpar Campos";
            btn.Click += limparCampos;
            pnl.Controls.Add(btn);



            Panel.Controls.Add(pnl);

            pnl = new Panel();
            pnl.Size = new Size(800, 400);
            pnl.AutoScroll = true;
            pnl.HorizontalScroll.Enabled = true;
            Dgv = new DataGridView();
            Dgv.Size = new Size(800, 480);
            Dgv.Dock = DockStyle.Fill;
            pnl.Controls.Add(Dgv);
            Dgv.AllowDrop = false;
            Dgv.AllowUserToAddRows = false;
            Dgv.AllowUserToDeleteRows = false;
            Dgv.AllowUserToOrderColumns = false;
            Dgv.AllowUserToResizeRows = false;
            Dgv.MultiSelect = false;
            Dgv.ReadOnly = true;
            Dgv.ScrollBars = ScrollBars.Both;

            Panel.Controls.Add(pnl);
            Panel.Visible = false;
        }

        public DataRow getSelectedRow()
        {
            if (dgv.CurrentCell != null)
            {
                var index = dgv.CurrentCell.RowIndex;
                return (dgv.DataSource as DataTable).Rows[index];
            }
            return null;
        }
        private void pesquisar(object sender, EventArgs e)
        {
            pesquisar();
        }

        private void pesquisar()
        {
            var where = new List<KeyValuePair<string, string>>();
            foreach (var item in data)
            {
                if (!item.IsSearchField) continue;
                if (item.IsFK)
                {
                    if ((item.Control as ComboBox).SelectedValue.ToString() != "0") where.Add(new KeyValuePair<string, string>(item.Name, (item.Control as ComboBox).SelectedValue.ToString()));
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.Control.Text)) where.Add(new KeyValuePair<string, string>(item.Name, item.Control.Text));
                }
            }
            var res = banco.get(tableName, where);
            Dgv.DataSource = res;
            foreach (DataGridViewColumn column in Dgv.Columns)
            {
                //column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Frozen = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void inserir(object sender, EventArgs e)
        {
            handlerInserir(tableName);
            pesquisar();
        }
        private void limparCampos(object sender, EventArgs e)
        {
            foreach (var item in data)
            {
                if (!item.IsSearchField) continue;
                if (item.IsFK)
                {
                    (item.Control as ComboBox).SelectedIndex = 0;
                }
                else item.Control.Text = string.Empty;
            }
        }
        private void editar(object sender, EventArgs e)
        {
            handlerEditar(tableName);
            pesquisar();
        }
        private void remover(object sender, EventArgs e)
        {
            var row = getSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Selecione a entrada a ser removida.");
            }
            else
            {
                handlerRemover(row, tableName, data);
                pesquisar();
            }
        }
    }
}
