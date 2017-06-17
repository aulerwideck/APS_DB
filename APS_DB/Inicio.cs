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
		private string current = string.Empty;
		//Metadados de tabelas e controles associados.
		private Dictionary<string, searchElement[]> meta = new Dictionary<string, searchElement[]> {};
		//Telas de pesquisa.
		private Dictionary<string, searchForm> forms = new Dictionary<string, searchForm>();
        public Inicio()
        {
            InitializeComponent();

			//Metadados de tabelas.
			meta.Add("cidade", new searchElement[] { new searchElement("nome") });
			meta.Add("endereco", new searchElement[] { new searchElement("Rua"), new searchElement("Numero"), new searchElement("Complemento"), new searchElement("Bairro"), new searchElement("CEP") });
			meta.Add("estado", new searchElement[] { new searchElement("Sigla"), new searchElement("Nome") });
			meta.Add("frete", new searchElement[] { new searchElement("NumeroCTe") });
			meta.Add("marca", new searchElement[] { new searchElement("Descricao") });
			meta.Add("modelo", new searchElement[] { new searchElement("Descricao") });
			meta.Add("pais", new searchElement[] { new searchElement("Sigla"), new searchElement("Nome") });
			meta.Add("pessoa", new searchElement[] { new searchElement("RazaoSocial"), new searchElement("CpfCnpj"), new searchElement("RG"), new searchElement("IE"), new searchElement("Email"), new searchElement("DataNasc") });
			meta.Add("telefone", new searchElement[] { new searchElement("Telefone") });
			meta.Add("tipopessoa", new searchElement[] { new searchElement("Descricao") });
			meta.Add("tipoveiculo", new searchElement[] { new searchElement("Descricao") });
			meta.Add("veiculo", new searchElement[] { new searchElement("Descricao"), new searchElement("Renavam"), new searchElement("Placa"), new searchElement("Tara") });

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

		private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
		private void showSearchPanel(string name)
		{
			if (forms.ContainsKey(current))
			{
				forms[current].Hide();
				current = string.Empty;
			}
			if (!forms.ContainsKey(name))
			{
				generateSearchPanel(name);
			}
			forms[name].Show();
			current = name;
		}
		private void generateSearchPanel(string name)
		{
			var sf = new searchForm();
			sf.Name = name;
			var data = meta[name];
			sf.Panel = new FlowLayoutPanel();
			sf.Panel.Anchor = AnchorStyles.Top;
			sf.Panel.Location = new Point(0, 24);
			sf.Panel.Padding = new Padding(10);
			sf.Panel.Dock = DockStyle.Fill;
			sf.Panel.Visible = true;
			sf.Panel.FlowDirection = FlowDirection.LeftToRight;
			for (int i = 0; i < data.Length; i++)
			{
				var subpnl = new Panel();
				subpnl.Size = new Size(400, 28);
				var label = new Label();
				label.Location = new Point(3, 6);
				label.Text = data[i].NomeColuna;
				var text = new TextBox();
				data[i].Controle = text;
				text.Name = "pesquisa" + data[i];
				text.Location = new Point(100, 3);
				text.Size = new Size(300, 20);
				subpnl.Controls.Add(label);
				subpnl.Controls.Add(text);
				sf.Panel.Controls.Add(subpnl);
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
			pnl.Controls.Add(btn);

			btn = new Button();
			btn.Location = new Point(bx + 3 * of, 0);
			btn.Size = new Size(of - 10, 23);
			btn.Text = "Remover";
			pnl.Controls.Add(btn);

			btn = new Button();
			btn.Location = new Point(bx + 4 * of, 0);
			btn.Size = new Size(of - 10, 23);
			btn.Text = "Limpar Campos";
			btn.Click += limparCampos;
			pnl.Controls.Add(btn);



			sf.Panel.Controls.Add(pnl);

			pnl = new Panel();
			pnl.Size = new Size(800, 480);
			sf.Dgv = new DataGridView();
			sf.Dgv.Size = new Size(800, 480);
			sf.Dgv.Dock = DockStyle.Fill;
			pnl.Controls.Add(sf.Dgv);
			sf.Dgv.AllowDrop = false;
			sf.Dgv.AllowUserToAddRows = false;
			sf.Dgv.AllowUserToDeleteRows = false;
			sf.Dgv.AllowUserToOrderColumns = false;
			sf.Dgv.AllowUserToResizeRows = false;
			sf.Dgv.ReadOnly = true;

			sf.Panel.Controls.Add(pnl);
			sf.Panel.Visible = false;


			forms.Add(name, sf);
			mainPanel.Controls.Add(sf.Panel);
		}

        private void pesquisar(object sender, EventArgs e)
        {
            var m = meta[current];
            var sf = forms[current];
            var where = new List<KeyValuePair<string, string>>();
            foreach (var item in m)
            {
                if (!string.IsNullOrEmpty(item.Controle.Text)) where.Add(new KeyValuePair<string, string>(item.NomeColuna, item.Controle.Text));
            }
            var res = banco.get(current, where);
            sf.Dgv.DataSource = res;
            foreach (DataGridViewColumn column in sf.Dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void inserir(object sender, EventArgs e)
        {
            
        }
        private void limparCampos(object sender, EventArgs e)
		{
			var m = meta[current];
			foreach (var item in m)
			{
				item.Controle.Text = string.Empty;
			}
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
	}
}
