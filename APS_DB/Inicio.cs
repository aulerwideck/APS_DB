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
			meta.Add("TipoPessoa", new searchElement[] { new searchElement("idTipoPessoa"), new searchElement("Descricao") });
			meta.Add("Pessoa", new searchElement[] { new searchElement("idPessoa"), new searchElement("idTipoPessoa"), new searchElement("RazaoSocial"), new searchElement("CpfCnpj"), new searchElement("RG"), new searchElement("IE"), new searchElement("Email"), new searchElement("DataNasc") });
			meta.Add("Veiculo", new searchElement[] { new searchElement("idVeiculo"), new searchElement("idModelo") });

			//Configuração de conexão.
			banco = new Banco();
			banco.Ip = "localhost";
			banco.Senha = "admin";
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
			sf.Panel.Location = new Point(60, 60);
			sf.Panel.Dock = DockStyle.Fill;
			sf.Panel.Visible = true;
			sf.Panel.FlowDirection = FlowDirection.TopDown;
			for (int i = 0; i < data.Length; i++)
			{
				var subpnl = new Panel();
				subpnl.Size = new Size(510, 28);
				var label = new Label();
				label.Location = new Point(3, 6);
				label.Text = data[i].NomeColuna;
				var text = new TextBox();
				data[i].Controle = text;
				text.Name = "pesquisa" + data[i];
				text.Location = new Point(200, 3);
				text.Size = new Size(300, 20);
				subpnl.Controls.Add(label);
				subpnl.Controls.Add(text);
				sf.Panel.Controls.Add(subpnl);
			}
			var btn = new Button();
			btn.Text = "Pesquisar";
			btn.Click += pesquisar;
			sf.Panel.Controls.Add(btn);

			sf.Dgv = new DataGridView();
			sf.Panel.Controls.Add(sf.Dgv);
			sf.Panel.Visible = false;
			forms.Add(name, sf);
			Controls.Add(sf.Panel);
		}

		private void pesquisar(object sender, EventArgs e)
		{
			var m = meta[current];
			var sf = forms[current];
			var res = banco.getTipoPessoa();
			sf.Dgv.DataSource = res;
		}

		private void pessoasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showSearchPanel("Pessoa");
		}
		private void tipoPessoaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showSearchPanel("TipoPessoa");
		}
		private void veíToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showSearchPanel("Veiculo");
		}
	}
}
