namespace APS_DB
{
    partial class Inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.gerenciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pessoasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.veíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.relatóriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tipoPessoaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerenciarToolStripMenuItem,
            this.relatóriosToolStripMenuItem,
            this.sairToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(842, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// gerenciarToolStripMenuItem
			// 
			this.gerenciarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pessoasToolStripMenuItem,
            this.veíToolStripMenuItem,
            this.tipoPessoaToolStripMenuItem});
			this.gerenciarToolStripMenuItem.Name = "gerenciarToolStripMenuItem";
			this.gerenciarToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this.gerenciarToolStripMenuItem.Text = "Gerenciar";
			// 
			// pessoasToolStripMenuItem
			// 
			this.pessoasToolStripMenuItem.Name = "pessoasToolStripMenuItem";
			this.pessoasToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.pessoasToolStripMenuItem.Text = "Pessoas";
			this.pessoasToolStripMenuItem.Click += new System.EventHandler(this.pessoasToolStripMenuItem_Click);
			// 
			// veíToolStripMenuItem
			// 
			this.veíToolStripMenuItem.Name = "veíToolStripMenuItem";
			this.veíToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.veíToolStripMenuItem.Text = "Veículos";
			this.veíToolStripMenuItem.Click += new System.EventHandler(this.veíToolStripMenuItem_Click);
			// 
			// relatóriosToolStripMenuItem
			// 
			this.relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
			this.relatóriosToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
			this.relatóriosToolStripMenuItem.Text = "Relatórios";
			// 
			// sairToolStripMenuItem
			// 
			this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
			this.sairToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.sairToolStripMenuItem.Text = "Sair";
			this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
			// 
			// tipoPessoaToolStripMenuItem
			// 
			this.tipoPessoaToolStripMenuItem.Name = "tipoPessoaToolStripMenuItem";
			this.tipoPessoaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.tipoPessoaToolStripMenuItem.Text = "Tipo Pessoa";
			this.tipoPessoaToolStripMenuItem.Click += new System.EventHandler(this.tipoPessoaToolStripMenuItem_Click);
			// 
			// mainPanel
			// 
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 24);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(842, 699);
			this.mainPanel.TabIndex = 1;
			// 
			// Inicio
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 723);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "Inicio";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "APS - Banco de Dados";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gerenciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatóriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pessoasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem veíToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tipoPessoaToolStripMenuItem;
		private System.Windows.Forms.Panel mainPanel;
	}
}