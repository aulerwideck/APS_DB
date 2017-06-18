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
using System.Linq;

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
                new column("Nome",null, true, MySqlType.msvarchar, 45, true, false, null)
            });
            meta.Add("endereco", new column[] {
                new column("idEndereco", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoa", "Pessoa", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idCidade", "Cidade", true, MySqlType.msint, 11, true, true, "cidade"),
                new column("Rua", null, true, MySqlType.msvarchar, 45, true),
                new column("Numero", "Número", true, MySqlType.msint, 11, true),
                new column("Complemento", null, false, MySqlType.msvarchar, 45, true),
                new column("Bairro", null, true, MySqlType.msvarchar, 45, true),
                new column("CEP", null, true, MySqlType.mschar, 9, true)
            });
            meta.Add("estado", new column[] {
                new column("idEstado", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPais", "País", true, MySqlType.msint, 11, true, true, "pais"),
                new column("Sigla", null, true, MySqlType.msvarchar, 2, true),
                new column("Nome", null, true, MySqlType.msvarchar, 45, true)
            });
            meta.Add("frete", new column[] {
                new column("idFrete", null, false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoaRemetente", "Remetente", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("dPessoaDestinatario", "Destinatário", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idPessoaTomador", "Tomador", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("idPessoaMotorista", "Motorista", true, MySqlType.msint, 11, true, true, "pessoa"),
                new column("NumeroCTe", "Número CTe", true, MySqlType.msint, 11, true),
                new column("DataEmissao", "Data de Emissão", true, MySqlType.msdate, -1, true),
                new column("DataEntrega", "Data de Entrega", false, MySqlType.msdate, -1, true),
                new column("VlCarga", "Valor Carga", false, MySqlType.msdouble, -1, true),
                new column("VlPedagio", "Valor Pedágio", false, MySqlType.msdouble, -1, true),
                new column("VlFrete", "Valor Frete", true, MySqlType.msdouble, -1, true),
                new column("PesoBruto", "Peso Bruto", false, MySqlType.msdouble, -1, true),
                new column("Finalizado", null, true, MySqlType.mstinyint, -1, true),
            });
            meta.Add("frete_veiculo", new column[] {
                new column("idFrete", "Frete", true, MySqlType.msint, 11, true, true, "frete",true),
                new column("idVeiculo", "Veiculo", true, MySqlType.msint, 11, true, true, "veiculo",true),
            });
            meta.Add("marca", new column[] {
                new column("idMarca",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("modelo", new column[] {
                new column("idModelo",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idMarca","Marca",true,MySqlType.msint,11,true,true,"marca"),
                new column("idTipoVeiculo","Tipo Veiculo",true,MySqlType.msint,11,true,true,"tipoveiculo"),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("pais", new column[] {
                new column("idPais",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Sigla",null,true,MySqlType.msvarchar,3,true),
                new column("Nome",null,true,MySqlType.msvarchar,45,true)
            });
            meta.Add("pessoa", new column[] {
                new column("idPessoa",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idTipoPessoa",null,true,MySqlType.msint,11,true,true,"tipopessoa"),
                new column("RazaoSocial","Razão Social",true,MySqlType.msvarchar,45,true),
                new column("CpfCnpj",null,true,MySqlType.msvarchar,14,true),
                new column("RG",null,false,MySqlType.msvarchar,11,true),
                new column("IE","Inscrição Estadual",false,MySqlType.msvarchar,15,true),
                new column("Email",null,false,MySqlType.msvarchar,45,true),
                new column("DataNasc","Data de Nascimento",false,MySqlType.msdate,-1,true)
            });
            meta.Add("telefone", new column[] {
                new column("idTelefone",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idPessoa","Pessoa",true,MySqlType.msint,11,true,true,"pessoa"),
                new column("Telefone",null,true,MySqlType.msvarchar,15,true)
            });
            meta.Add("tipopessoa", new column[] {
                new column("idTipoPessoa",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("tipoveiculo", new column[] {
                new column("idTipoVeiculo",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true)
            });
            meta.Add("veiculo", new column[] {
                new column("idVeiculo",null,false, MySqlType.notNeeded, -1, false, null, null, true),
                new column("idModelo","Modelo",true,MySqlType.msint,11,true,true,"modelo"),
                new column("Descricao","Descrição",true,MySqlType.msvarchar,45,true),
                new column("Renavam",null,true,MySqlType.msvarchar,45,true),
                new column("Placa",null,true,MySqlType.msvarchar,45,true),
                new column("Tara",null,true,MySqlType.msvarchar,45,true),
                new column("CapacidadeKg",null,false,MySqlType.msvarchar,45,true),
                new column("CapacidadeM3",null,false,MySqlType.msvarchar,45,true)
            });

            //Configuração de conexão.
            banco = new Banco();
            banco.Ip = "localhost";
            banco.Senha = "root";
            banco.User = "root";
            banco.Db = "aps";

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
                var form = new searchForm(fname, tableName, meta[tableName], banco, novo, editar, remover);
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
            if (meta.Where(x => x.Key != table).Any(x => x.Value.Any(y => y.IsFK && y.FkTableName == table))) {
                //tem fk
                if (MessageBox.Show("Esta entrada pode possuir dependências, se excluída, todas as dependências também serão excluídas. Prosseguir?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
