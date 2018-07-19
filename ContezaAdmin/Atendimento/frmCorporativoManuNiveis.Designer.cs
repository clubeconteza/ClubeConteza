namespace ContezaAdmin.Atendimento
{
    partial class frmCorporativoManuNiveis
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
            this.label13 = new System.Windows.Forms.Label();
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tbLista = new System.Windows.Forms.TabPage();
            this.ddgLista = new System.Windows.Forms.DataGridView();
            this.TB021_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB021_Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB022_Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB023_Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuLista = new System.Windows.Forms.MenuStrip();
            this.mnuListaNovo = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbCadastro = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsbSessoes = new System.Windows.Forms.ListBox();
            this.mnuSessao = new System.Windows.Forms.MenuStrip();
            this.mnuSessaoVincular = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSessaoDesvincular = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSessao = new System.Windows.Forms.ComboBox();
            this.lblNivel1Id = new System.Windows.Forms.Label();
            this.GrpNivel3 = new System.Windows.Forms.GroupBox();
            this.lblNivel3d = new System.Windows.Forms.Label();
            this.txtNivel3Desc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mnuNivel3 = new System.Windows.Forms.MenuStrip();
            this.mnuNivel3Salvar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNivel3Limpar = new System.Windows.Forms.ToolStripMenuItem();
            this.lsbNivel3 = new System.Windows.Forms.ListBox();
            this.GrpNivel2 = new System.Windows.Forms.GroupBox();
            this.lblNivel2Id = new System.Windows.Forms.Label();
            this.txtNivel2Desc = new System.Windows.Forms.TextBox();
            this.lsbNivel2 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mnuNivel2 = new System.Windows.Forms.MenuStrip();
            this.mnuNivel2Salvar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNivel2Limpar = new System.Windows.Forms.ToolStripMenuItem();
            this.txtNivel1Desc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuCadastro = new System.Windows.Forms.MenuStrip();
            this.mnuCadastroSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCadastroFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPrincipal.SuspendLayout();
            this.tbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddgLista)).BeginInit();
            this.mnuLista.SuspendLayout();
            this.tbCadastro.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnuSessao.SuspendLayout();
            this.GrpNivel3.SuspendLayout();
            this.mnuNivel3.SuspendLayout();
            this.GrpNivel2.SuspendLayout();
            this.mnuNivel2.SuspendLayout();
            this.mnuCadastro.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(908, 29);
            this.label13.TabIndex = 5;
            this.label13.Text = "Cadastro de Categorias, Especialidades e Subniveis";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.tbLista);
            this.tabPrincipal.Controls.Add(this.tbCadastro);
            this.tabPrincipal.Location = new System.Drawing.Point(4, 32);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(892, 476);
            this.tabPrincipal.TabIndex = 6;
            // 
            // tbLista
            // 
            this.tbLista.Controls.Add(this.ddgLista);
            this.tbLista.Controls.Add(this.mnuLista);
            this.tbLista.Location = new System.Drawing.Point(4, 22);
            this.tbLista.Name = "tbLista";
            this.tbLista.Padding = new System.Windows.Forms.Padding(3);
            this.tbLista.Size = new System.Drawing.Size(884, 450);
            this.tbLista.TabIndex = 0;
            this.tbLista.Text = "Lista";
            this.tbLista.UseVisualStyleBackColor = true;
            // 
            // ddgLista
            // 
            this.ddgLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddgLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TB021_id,
            this.TB021_Descricao,
            this.TB022_Descricao,
            this.TB023_Descricao});
            this.ddgLista.Location = new System.Drawing.Point(6, 30);
            this.ddgLista.Name = "ddgLista";
            this.ddgLista.Size = new System.Drawing.Size(597, 414);
            this.ddgLista.TabIndex = 1;
            this.ddgLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddgLista_CellClick);
            // 
            // TB021_id
            // 
            this.TB021_id.DataPropertyName = "TB021_id";
            this.TB021_id.HeaderText = "Id";
            this.TB021_id.Name = "TB021_id";
            this.TB021_id.ReadOnly = true;
            this.TB021_id.Width = 80;
            // 
            // TB021_Descricao
            // 
            this.TB021_Descricao.DataPropertyName = "TB021_Descricao";
            this.TB021_Descricao.HeaderText = "Nivel1";
            this.TB021_Descricao.Name = "TB021_Descricao";
            this.TB021_Descricao.ReadOnly = true;
            this.TB021_Descricao.Width = 150;
            // 
            // TB022_Descricao
            // 
            this.TB022_Descricao.DataPropertyName = "TB022_Descricao";
            this.TB022_Descricao.HeaderText = "Nivel2";
            this.TB022_Descricao.Name = "TB022_Descricao";
            this.TB022_Descricao.ReadOnly = true;
            this.TB022_Descricao.Width = 150;
            // 
            // TB023_Descricao
            // 
            this.TB023_Descricao.DataPropertyName = "TB023_Descricao";
            this.TB023_Descricao.HeaderText = "Nivel3";
            this.TB023_Descricao.Name = "TB023_Descricao";
            this.TB023_Descricao.ReadOnly = true;
            this.TB023_Descricao.Width = 150;
            // 
            // mnuLista
            // 
            this.mnuLista.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuListaNovo,
            this.fecharToolStripMenuItem1});
            this.mnuLista.Location = new System.Drawing.Point(3, 3);
            this.mnuLista.Name = "mnuLista";
            this.mnuLista.Size = new System.Drawing.Size(878, 24);
            this.mnuLista.TabIndex = 0;
            this.mnuLista.Text = "menuStrip1";
            // 
            // mnuListaNovo
            // 
            this.mnuListaNovo.Name = "mnuListaNovo";
            this.mnuListaNovo.Size = new System.Drawing.Size(48, 20);
            this.mnuListaNovo.Text = "Novo";
            this.mnuListaNovo.Click += new System.EventHandler(this.mnuListaNovo_Click);
            // 
            // fecharToolStripMenuItem1
            // 
            this.fecharToolStripMenuItem1.Name = "fecharToolStripMenuItem1";
            this.fecharToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.fecharToolStripMenuItem1.Text = "Fechar";
            this.fecharToolStripMenuItem1.Click += new System.EventHandler(this.fecharToolStripMenuItem1_Click);
            // 
            // tbCadastro
            // 
            this.tbCadastro.Controls.Add(this.groupBox1);
            this.tbCadastro.Controls.Add(this.lblNivel1Id);
            this.tbCadastro.Controls.Add(this.GrpNivel3);
            this.tbCadastro.Controls.Add(this.GrpNivel2);
            this.tbCadastro.Controls.Add(this.txtNivel1Desc);
            this.tbCadastro.Controls.Add(this.label1);
            this.tbCadastro.Controls.Add(this.mnuCadastro);
            this.tbCadastro.Location = new System.Drawing.Point(4, 22);
            this.tbCadastro.Name = "tbCadastro";
            this.tbCadastro.Padding = new System.Windows.Forms.Padding(3);
            this.tbCadastro.Size = new System.Drawing.Size(884, 450);
            this.tbCadastro.TabIndex = 1;
            this.tbCadastro.Text = "Cadastro";
            this.tbCadastro.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsbSessoes);
            this.groupBox1.Controls.Add(this.mnuSessao);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbSessao);
            this.groupBox1.Location = new System.Drawing.Point(18, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 332);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sessão";
            // 
            // lsbSessoes
            // 
            this.lsbSessoes.FormattingEnabled = true;
            this.lsbSessoes.Location = new System.Drawing.Point(6, 77);
            this.lsbSessoes.Name = "lsbSessoes";
            this.lsbSessoes.Size = new System.Drawing.Size(253, 251);
            this.lsbSessoes.TabIndex = 9;
            // 
            // mnuSessao
            // 
            this.mnuSessao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSessaoVincular,
            this.mnuSessaoDesvincular});
            this.mnuSessao.Location = new System.Drawing.Point(3, 16);
            this.mnuSessao.Name = "mnuSessao";
            this.mnuSessao.Size = new System.Drawing.Size(259, 24);
            this.mnuSessao.TabIndex = 8;
            this.mnuSessao.Text = "menuStrip1";
            // 
            // mnuSessaoVincular
            // 
            this.mnuSessaoVincular.Name = "mnuSessaoVincular";
            this.mnuSessaoVincular.Size = new System.Drawing.Size(62, 20);
            this.mnuSessaoVincular.Text = "Vincular";
            this.mnuSessaoVincular.Click += new System.EventHandler(this.mnuSessaoVincular_Click);
            // 
            // mnuSessaoDesvincular
            // 
            this.mnuSessaoDesvincular.Name = "mnuSessaoDesvincular";
            this.mnuSessaoDesvincular.Size = new System.Drawing.Size(80, 20);
            this.mnuSessaoDesvincular.Text = "Desvincular";
            this.mnuSessaoDesvincular.Click += new System.EventHandler(this.mnuSessaoDesvincular_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sessão";
            // 
            // cmbSessao
            // 
            this.cmbSessao.FormattingEnabled = true;
            this.cmbSessao.Location = new System.Drawing.Point(61, 50);
            this.cmbSessao.Name = "cmbSessao";
            this.cmbSessao.Size = new System.Drawing.Size(198, 21);
            this.cmbSessao.TabIndex = 7;
            // 
            // lblNivel1Id
            // 
            this.lblNivel1Id.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNivel1Id.Location = new System.Drawing.Point(243, 27);
            this.lblNivel1Id.Name = "lblNivel1Id";
            this.lblNivel1Id.Size = new System.Drawing.Size(40, 20);
            this.lblNivel1Id.TabIndex = 5;
            this.lblNivel1Id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNivel1Id.Visible = false;
            // 
            // GrpNivel3
            // 
            this.GrpNivel3.Controls.Add(this.lblNivel3d);
            this.GrpNivel3.Controls.Add(this.txtNivel3Desc);
            this.GrpNivel3.Controls.Add(this.label5);
            this.GrpNivel3.Controls.Add(this.mnuNivel3);
            this.GrpNivel3.Controls.Add(this.lsbNivel3);
            this.GrpNivel3.Location = new System.Drawing.Point(591, 79);
            this.GrpNivel3.Name = "GrpNivel3";
            this.GrpNivel3.Size = new System.Drawing.Size(277, 332);
            this.GrpNivel3.TabIndex = 4;
            this.GrpNivel3.TabStop = false;
            this.GrpNivel3.Text = "Nivel 3";
            // 
            // lblNivel3d
            // 
            this.lblNivel3d.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNivel3d.Location = new System.Drawing.Point(200, 40);
            this.lblNivel3d.Name = "lblNivel3d";
            this.lblNivel3d.Size = new System.Drawing.Size(40, 20);
            this.lblNivel3d.TabIndex = 10;
            this.lblNivel3d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNivel3d.Visible = false;
            // 
            // txtNivel3Desc
            // 
            this.txtNivel3Desc.Location = new System.Drawing.Point(46, 51);
            this.txtNivel3Desc.MaxLength = 50;
            this.txtNivel3Desc.Name = "txtNivel3Desc";
            this.txtNivel3Desc.Size = new System.Drawing.Size(216, 20);
            this.txtNivel3Desc.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nome";
            // 
            // mnuNivel3
            // 
            this.mnuNivel3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNivel3Salvar,
            this.mnuNivel3Limpar});
            this.mnuNivel3.Location = new System.Drawing.Point(3, 16);
            this.mnuNivel3.Name = "mnuNivel3";
            this.mnuNivel3.Size = new System.Drawing.Size(271, 24);
            this.mnuNivel3.TabIndex = 7;
            this.mnuNivel3.Text = "menuStrip1";
            // 
            // mnuNivel3Salvar
            // 
            this.mnuNivel3Salvar.Name = "mnuNivel3Salvar";
            this.mnuNivel3Salvar.Size = new System.Drawing.Size(50, 20);
            this.mnuNivel3Salvar.Text = "Salvar";
            this.mnuNivel3Salvar.Click += new System.EventHandler(this.mnuNivel3Salvar_Click);
            // 
            // mnuNivel3Limpar
            // 
            this.mnuNivel3Limpar.Name = "mnuNivel3Limpar";
            this.mnuNivel3Limpar.Size = new System.Drawing.Size(56, 20);
            this.mnuNivel3Limpar.Text = "Limpar";
            this.mnuNivel3Limpar.Click += new System.EventHandler(this.mnuNivel3Limpar_Click);
            // 
            // lsbNivel3
            // 
            this.lsbNivel3.FormattingEnabled = true;
            this.lsbNivel3.Location = new System.Drawing.Point(0, 75);
            this.lsbNivel3.Name = "lsbNivel3";
            this.lsbNivel3.Size = new System.Drawing.Size(259, 251);
            this.lsbNivel3.TabIndex = 6;
            this.lsbNivel3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsbNivel3_MouseClick);
            // 
            // GrpNivel2
            // 
            this.GrpNivel2.Controls.Add(this.lblNivel2Id);
            this.GrpNivel2.Controls.Add(this.txtNivel2Desc);
            this.GrpNivel2.Controls.Add(this.lsbNivel2);
            this.GrpNivel2.Controls.Add(this.label2);
            this.GrpNivel2.Controls.Add(this.mnuNivel2);
            this.GrpNivel2.Enabled = false;
            this.GrpNivel2.Location = new System.Drawing.Point(289, 79);
            this.GrpNivel2.Name = "GrpNivel2";
            this.GrpNivel2.Size = new System.Drawing.Size(277, 332);
            this.GrpNivel2.TabIndex = 3;
            this.GrpNivel2.TabStop = false;
            this.GrpNivel2.Text = "Nivel 2";
            // 
            // lblNivel2Id
            // 
            this.lblNivel2Id.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNivel2Id.Location = new System.Drawing.Point(203, 40);
            this.lblNivel2Id.Name = "lblNivel2Id";
            this.lblNivel2Id.Size = new System.Drawing.Size(40, 20);
            this.lblNivel2Id.TabIndex = 6;
            this.lblNivel2Id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNivel2Id.Visible = false;
            // 
            // txtNivel2Desc
            // 
            this.txtNivel2Desc.Location = new System.Drawing.Point(49, 51);
            this.txtNivel2Desc.MaxLength = 50;
            this.txtNivel2Desc.Name = "txtNivel2Desc";
            this.txtNivel2Desc.Size = new System.Drawing.Size(216, 20);
            this.txtNivel2Desc.TabIndex = 3;
            // 
            // lsbNivel2
            // 
            this.lsbNivel2.FormattingEnabled = true;
            this.lsbNivel2.Location = new System.Drawing.Point(6, 75);
            this.lsbNivel2.Name = "lsbNivel2";
            this.lsbNivel2.Size = new System.Drawing.Size(259, 251);
            this.lsbNivel2.TabIndex = 0;
            this.lsbNivel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsbNivel2_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome";
            // 
            // mnuNivel2
            // 
            this.mnuNivel2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNivel2Salvar,
            this.mnuNivel2Limpar});
            this.mnuNivel2.Location = new System.Drawing.Point(3, 16);
            this.mnuNivel2.Name = "mnuNivel2";
            this.mnuNivel2.Size = new System.Drawing.Size(271, 24);
            this.mnuNivel2.TabIndex = 1;
            this.mnuNivel2.Text = "menuStrip1";
            // 
            // mnuNivel2Salvar
            // 
            this.mnuNivel2Salvar.Name = "mnuNivel2Salvar";
            this.mnuNivel2Salvar.Size = new System.Drawing.Size(50, 20);
            this.mnuNivel2Salvar.Text = "Salvar";
            this.mnuNivel2Salvar.Click += new System.EventHandler(this.mnuNivel2Salvar_Click);
            // 
            // mnuNivel2Limpar
            // 
            this.mnuNivel2Limpar.Name = "mnuNivel2Limpar";
            this.mnuNivel2Limpar.Size = new System.Drawing.Size(56, 20);
            this.mnuNivel2Limpar.Text = "Limpar";
            this.mnuNivel2Limpar.Click += new System.EventHandler(this.mnuNivel2Limpar_Click);
            // 
            // txtNivel1Desc
            // 
            this.txtNivel1Desc.Location = new System.Drawing.Point(61, 43);
            this.txtNivel1Desc.MaxLength = 50;
            this.txtNivel1Desc.Name = "txtNivel1Desc";
            this.txtNivel1Desc.Size = new System.Drawing.Size(222, 20);
            this.txtNivel1Desc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nivel 1";
            // 
            // mnuCadastro
            // 
            this.mnuCadastro.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCadastroSalvar,
            this.mnuCadastroFechar});
            this.mnuCadastro.Location = new System.Drawing.Point(3, 3);
            this.mnuCadastro.Name = "mnuCadastro";
            this.mnuCadastro.Size = new System.Drawing.Size(878, 24);
            this.mnuCadastro.TabIndex = 0;
            this.mnuCadastro.Text = "menuStrip2";
            // 
            // mnuCadastroSalvar
            // 
            this.mnuCadastroSalvar.Name = "mnuCadastroSalvar";
            this.mnuCadastroSalvar.Size = new System.Drawing.Size(50, 20);
            this.mnuCadastroSalvar.Text = "Salvar";
            this.mnuCadastroSalvar.Click += new System.EventHandler(this.mnuCadastroSalvar_Click);
            // 
            // mnuCadastroFechar
            // 
            this.mnuCadastroFechar.Name = "mnuCadastroFechar";
            this.mnuCadastroFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuCadastroFechar.Text = "Fechar";
            this.mnuCadastroFechar.Click += new System.EventHandler(this.mnuCadastroFechar_Click);
            // 
            // frmCorporativoManuNiveis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 516);
            this.Controls.Add(this.tabPrincipal);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mnuLista;
            this.Name = "frmCorporativoManuNiveis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCorporativoManuNiveis";
            this.Load += new System.EventHandler(this.frmCorporativoManuNiveis_Load);
            this.tabPrincipal.ResumeLayout(false);
            this.tbLista.ResumeLayout(false);
            this.tbLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddgLista)).EndInit();
            this.mnuLista.ResumeLayout(false);
            this.mnuLista.PerformLayout();
            this.tbCadastro.ResumeLayout(false);
            this.tbCadastro.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mnuSessao.ResumeLayout(false);
            this.mnuSessao.PerformLayout();
            this.GrpNivel3.ResumeLayout(false);
            this.GrpNivel3.PerformLayout();
            this.mnuNivel3.ResumeLayout(false);
            this.mnuNivel3.PerformLayout();
            this.GrpNivel2.ResumeLayout(false);
            this.GrpNivel2.PerformLayout();
            this.mnuNivel2.ResumeLayout(false);
            this.mnuNivel2.PerformLayout();
            this.mnuCadastro.ResumeLayout(false);
            this.mnuCadastro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tbLista;
        private System.Windows.Forms.TabPage tbCadastro;
        private System.Windows.Forms.MenuStrip mnuLista;
        private System.Windows.Forms.GroupBox GrpNivel3;
        private System.Windows.Forms.GroupBox GrpNivel2;
        private System.Windows.Forms.TextBox txtNivel2Desc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lsbNivel2;
        private System.Windows.Forms.TextBox txtNivel1Desc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mnuCadastro;
        private System.Windows.Forms.ToolStripMenuItem mnuListaNovo;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuCadastroSalvar;
        private System.Windows.Forms.ToolStripMenuItem mnuCadastroFechar;
        private System.Windows.Forms.DataGridView ddgLista;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB021_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB021_Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB022_Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB023_Descricao;
        private System.Windows.Forms.Label lblNivel1Id;
        private System.Windows.Forms.ListBox lsbNivel3;
        private System.Windows.Forms.MenuStrip mnuNivel2;
        private System.Windows.Forms.ToolStripMenuItem mnuNivel2Salvar;
        private System.Windows.Forms.ToolStripMenuItem mnuNivel2Limpar;
        private System.Windows.Forms.Label lblNivel2Id;
        private System.Windows.Forms.Label lblNivel3d;
        private System.Windows.Forms.TextBox txtNivel3Desc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip mnuNivel3;
        private System.Windows.Forms.ToolStripMenuItem mnuNivel3Salvar;
        private System.Windows.Forms.ToolStripMenuItem mnuNivel3Limpar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSessao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip mnuSessao;
        private System.Windows.Forms.ToolStripMenuItem mnuSessaoVincular;
        private System.Windows.Forms.ToolStripMenuItem mnuSessaoDesvincular;
        private System.Windows.Forms.ListBox lsbSessoes;
    }
}