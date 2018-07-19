namespace ContezaAdmin.Atendimento
{
    partial class FrmContratoAnotacoes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContratoAnotacoes));
            this.dgAnotacoes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTb011NomeExibicao = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTb026Data = new System.Windows.Forms.Label();
            this.txtTb026Anotacao = new System.Windows.Forms.TextBox();
            this.mnuAnotacoes = new System.Windows.Forms.MenuStrip();
            this.mnuAnotacoesSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnotacoesNovo = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTb026Id = new System.Windows.Forms.Label();
            this.Tb026Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tb026Anotacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tb026Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgAnotacoes)).BeginInit();
            this.mnuAnotacoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgAnotacoes
            // 
            this.dgAnotacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAnotacoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tb026Id,
            this.Tb026Anotacao,
            this.Tb026Data});
            this.dgAnotacoes.Location = new System.Drawing.Point(3, -1);
            this.dgAnotacoes.MultiSelect = false;
            this.dgAnotacoes.Name = "dgAnotacoes";
            this.dgAnotacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAnotacoes.Size = new System.Drawing.Size(405, 306);
            this.dgAnotacoes.TabIndex = 0;
            this.dgAnotacoes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAnotacoes_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuário";
            // 
            // lblTb011NomeExibicao
            // 
            this.lblTb011NomeExibicao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTb011NomeExibicao.Location = new System.Drawing.Point(3, 354);
            this.lblTb011NomeExibicao.Name = "lblTb011NomeExibicao";
            this.lblTb011NomeExibicao.Size = new System.Drawing.Size(235, 21);
            this.lblTb011NomeExibicao.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data";
            // 
            // lblTb026Data
            // 
            this.lblTb026Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTb026Data.Location = new System.Drawing.Point(266, 354);
            this.lblTb026Data.Name = "lblTb026Data";
            this.lblTb026Data.Size = new System.Drawing.Size(137, 21);
            this.lblTb026Data.TabIndex = 4;
            // 
            // txtTb026Anotacao
            // 
            this.txtTb026Anotacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTb026Anotacao.Location = new System.Drawing.Point(3, 378);
            this.txtTb026Anotacao.MaxLength = 500;
            this.txtTb026Anotacao.Multiline = true;
            this.txtTb026Anotacao.Name = "txtTb026Anotacao";
            this.txtTb026Anotacao.Size = new System.Drawing.Size(400, 141);
            this.txtTb026Anotacao.TabIndex = 5;
            // 
            // mnuAnotacoes
            // 
            this.mnuAnotacoes.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuAnotacoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAnotacoesSalvar,
            this.mnuAnotacoesNovo});
            this.mnuAnotacoes.Location = new System.Drawing.Point(3, 308);
            this.mnuAnotacoes.Name = "mnuAnotacoes";
            this.mnuAnotacoes.Size = new System.Drawing.Size(106, 24);
            this.mnuAnotacoes.TabIndex = 6;
            this.mnuAnotacoes.Text = "menuStrip1";
            // 
            // mnuAnotacoesSalvar
            // 
            this.mnuAnotacoesSalvar.Name = "mnuAnotacoesSalvar";
            this.mnuAnotacoesSalvar.Size = new System.Drawing.Size(50, 20);
            this.mnuAnotacoesSalvar.Text = "Salvar";
            this.mnuAnotacoesSalvar.Click += new System.EventHandler(this.mnuAnotacoesSalvar_Click);
            // 
            // mnuAnotacoesNovo
            // 
            this.mnuAnotacoesNovo.Name = "mnuAnotacoesNovo";
            this.mnuAnotacoesNovo.Size = new System.Drawing.Size(48, 20);
            this.mnuAnotacoesNovo.Text = "Novo";
            this.mnuAnotacoesNovo.Click += new System.EventHandler(this.mnuAnotacoesNovo_Click);
            // 
            // lblTb026Id
            // 
            this.lblTb026Id.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTb026Id.Location = new System.Drawing.Point(326, 333);
            this.lblTb026Id.Name = "lblTb026Id";
            this.lblTb026Id.Size = new System.Drawing.Size(66, 21);
            this.lblTb026Id.TabIndex = 7;
            this.lblTb026Id.Visible = false;
            // 
            // Tb026Id
            // 
            this.Tb026Id.DataPropertyName = "Tb026Id";
            this.Tb026Id.HeaderText = "Id";
            this.Tb026Id.Name = "Tb026Id";
            this.Tb026Id.ReadOnly = true;
            this.Tb026Id.Visible = false;
            this.Tb026Id.Width = 80;
            // 
            // Tb026Anotacao
            // 
            this.Tb026Anotacao.DataPropertyName = "Tb026Anotacao";
            this.Tb026Anotacao.HeaderText = "Anotação";
            this.Tb026Anotacao.Name = "Tb026Anotacao";
            this.Tb026Anotacao.ReadOnly = true;
            this.Tb026Anotacao.Width = 210;
            // 
            // Tb026Data
            // 
            this.Tb026Data.DataPropertyName = "Tb026Data";
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.Tb026Data.DefaultCellStyle = dataGridViewCellStyle1;
            this.Tb026Data.HeaderText = "Data";
            this.Tb026Data.Name = "Tb026Data";
            this.Tb026Data.ReadOnly = true;
            this.Tb026Data.Width = 120;
            // 
            // FrmContratoAnotacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 520);
            this.Controls.Add(this.lblTb026Id);
            this.Controls.Add(this.txtTb026Anotacao);
            this.Controls.Add(this.lblTb026Data);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTb011NomeExibicao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgAnotacoes);
            this.Controls.Add(this.mnuAnotacoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuAnotacoes;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContratoAnotacoes";
            this.Text = "Anotações contrato [00000000000]";
            this.Load += new System.EventHandler(this.FrmContratoAnotacoes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAnotacoes)).EndInit();
            this.mnuAnotacoes.ResumeLayout(false);
            this.mnuAnotacoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgAnotacoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTb011NomeExibicao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTb026Data;
        private System.Windows.Forms.TextBox txtTb026Anotacao;
        private System.Windows.Forms.MenuStrip mnuAnotacoes;
        private System.Windows.Forms.ToolStripMenuItem mnuAnotacoesSalvar;
        private System.Windows.Forms.ToolStripMenuItem mnuAnotacoesNovo;
        private System.Windows.Forms.Label lblTb026Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tb026Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tb026Anotacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tb026Data;
    }
}