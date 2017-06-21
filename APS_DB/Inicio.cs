using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APS_DB.Classes;

namespace APS_DB
{
    public partial class Inicio : Form
    {
        private Banco banco;
        private string previous = string.Empty;
        private string current = string.Empty;
        //Telas de pesquisa.
        private Dictionary<string, form> forms = new Dictionary<string, form>();
        public Inicio()
        {
            InitializeComponent();
            //Configuração de conexão.
            banco = new Banco("localhost", "root", "root", "aps");
            //Teste.
            banco.abreConexao();
            var res = banco.verificaConexao();
        }
        private string getSearchFormName(string table)
        {
            return string.Format("searchForm-{0}", table);
        }
        private string getInsertFormName(string table)
        {
            return string.Format("insertForm-{0}", table);
        }
        private void showSearchPanel(string tableName)
        {
            var fname = getSearchFormName(tableName);
            if (forms.ContainsKey(current))
            {
                forms[current].Hide();
            }
            if (!forms.ContainsKey(fname))
            {
                var form = new searchForm(fname, tableName, banco.meta[tableName], banco, novo, editar, remover);
                forms.Add(fname, form);
                mainPanel.Controls.Add(form.Panel);
            }
            makeInsertUpdateForm(tableName, getInsertFormName(tableName));
            forms[fname].Show();
            previous = current;
            current = fname;
        }
        private void novo(string tableName)
        {
            var fname = getInsertFormName(tableName);
            if (forms.ContainsKey(current))
            {
                forms[current].Hide();
            }
            makeInsertUpdateForm(tableName, fname);
            forms[fname].Show();
            previous = current;
            current = fname;
        }
        private void editar(string tableName)
        {
            var fname = getInsertFormName(tableName);
            if (forms.ContainsKey(current))
            {
                forms[current].Hide();
            }
            makeInsertUpdateForm(tableName, fname);
            var data = (forms[current] as searchForm).getSelectedRow();
            if (data != null)
            {
                (forms[fname] as insertUpdateForm).Show(data);
                previous = current;
                current = fname;
            }
            else
            {
                MessageBox.Show("Selecione uma entrada para editar");
            }
        }

        private void makeInsertUpdateForm(string tableName, string fname)
        {
            if (!forms.ContainsKey(fname))
            {
                var form = new insertUpdateForm(fname, tableName, banco.meta[tableName], banco, concluirInsert, cancelarInsert, concluirEdit);
                forms.Add(fname, form);
                mainPanel.Controls.Add(form.Panel);
            }
        }

        private void voltar()
        {
            forms[current].Hide();
            forms[previous].Show();
            current = previous;
            previous = string.Empty;
        }
        private void remover(DataRow dr, string table, column[] data)
        {
            var pk = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i].IsPrimaryKey)
                {
                    pk.Add(new KeyValuePair<string, string>(data[i].Name, dr.ItemArray[i].ToString()));
                }
            }
            if (banco.meta.Where(x => x.Key != table).Any(x => x.Value.Any(y => y.IsFK && y.FkTableName == table))) {
                //tem fk
                if (MessageBox.Show("Deseja mesmo excluir esta tupla?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (banco.delete(table, pk) < 0)
                        {
                            MessageBox.Show("Ocorreu um erro na operação. O registro possui outras entradas dependentes.");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ocorreu um erro na operação. O registro possui outras entradas dependentes.");
                    }
                }
            }
            else
            {
                try
                {
                    banco.delete(table, pk);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro na operação.");
                }
            }
        }
        #region Events
        private void concluirInsert(object sender, EventArgs e)
        {
            voltar();
        }
        private void concluirEdit(object sender, EventArgs e)
        {
            voltar();
        }
        private void cancelarInsert(object sender, EventArgs e)
        {
            voltar();
        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("cidade");
        }
        private void endereçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("endereco");
        }
        private void estadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("estado");
        }
        private void fretesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("frete");
        }
        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("marca");
        }
        private void modelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("modelo");
        }
        private void paísesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("pais");
        }
        private void pessoasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("pessoa");
        }
        private void telefonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("telefone");
        }
        private void tipoPessoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("tipopessoa");
        }
        private void tipoVeículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("tipoveiculo");
        }
        private void veíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("veiculo");
        }
        private void freteVeiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSearchPanel("frete_veiculo");
        }
        #endregion

    }
}
