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
        //Metadados de tabelas e controles associados.
        private Dictionary<string, column[]> meta = new Dictionary<string, column[]> { };
        //Telas de pesquisa.
        private Dictionary<string, form> forms = new Dictionary<string, form>();
        public Inicio()
        {
            InitializeComponent();

            //Metadados de tabelas.
            meta.Add("cidade", new column[] {
                new column("idCidade", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idEstado", "Estado", true, MySqlType.msint, 11, true, true, "estado"),
                new column("nome", "Nome", true, MySqlType.msvarchar, 45, true, false, null),
            });
            meta.Add("endereco", new column[] {
                new column("idEndereco", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoa", null, true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idCidade", null, true, MySqlType.msint, 11, true, true, "cidade"),
                new column("Rua", null, true, MySqlType.msvarchar, 45, true),
                new column("Numero", "Número", true, MySqlType.msint, 11, true),
                new column("Complemento", null, false, MySqlType.msvarchar, 45, true),
                new column("Bairro", null, true, MySqlType.msvarchar, 45, true),
                new column("CEP", null, true, MySqlType.mschar, 9, true),
            });
            meta.Add("estado", new column[] {
                new column("idEstado", null, false),
                new column("nome", "Nome", true, MySqlType.msvarchar, 45, true),
                new column("sigla", "Sigla", true, MySqlType.msvarchar, 2, true),
                new column("pais", "País", true, MySqlType.msint, 11, true, true, "pais"),
            });
            meta.Add("frete", new column[] {
                new column("NumeroCTe")
            });
            meta.Add("marca", new column[] {
                new column("Descricao")
            });
            meta.Add("modelo", new column[] {
                new column("Descricao")
            });
            meta.Add("pais", new column[] {
                new column("Sigla"),
                new column("Nome")
            });
            meta.Add("pessoa", new column[] {
                new column("RazaoSocial"),
                new column("CpfCnpj"),
                new column("RG"),
                new column("IE"),
                new column("Email"),
                new column("DataNasc")
            });
            meta.Add("telefone", new column[] {
                new column("Telefone")
            });
            meta.Add("tipopessoa", new column[] {
                new column("Descricao")
            });
            meta.Add("tipoveiculo", new column[] {
                new column("Descricao")
            });
            meta.Add("veiculo", new column[] {
                new column("Descricao"),
                new column("Renavam"),
                new column("Placa"),
                new column("Tara")
            });

            //Configuração de conexão.
            banco = new Banco();
            banco.Ip = "localhost";
            banco.Senha = "root";
            banco.User = "root";
            banco.Db = "mydb";

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
                var form = new searchForm(fname, tableName, meta[tableName], banco, novo, editar);
                forms.Add(fname, form);
                mainPanel.Controls.Add(form.Panel);
            }
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
            if (!forms.ContainsKey(fname))
            {
                var form = new insertUpdateForm(fname, tableName, meta[tableName], banco, concluirInsert, cancelarInsert, concluirEdit);
                forms.Add(fname, form);
                mainPanel.Controls.Add(form.Panel);
            }
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
            if (!forms.ContainsKey(fname))
            {
                var form = new insertUpdateForm(fname, tableName, meta[tableName], banco, concluirInsert, cancelarInsert, concluirEdit);
                forms.Add(fname, form);
                mainPanel.Controls.Add(form.Panel);
            }
            var data = (forms[current] as searchForm).getEditData();
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
        private void voltar()
        {
            forms[current].Hide();
            forms[previous].Show();
            current = previous;
            previous = string.Empty;
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
        #endregion
    }
}
