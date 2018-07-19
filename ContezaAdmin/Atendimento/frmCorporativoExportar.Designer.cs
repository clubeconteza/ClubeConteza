namespace ContezaAdmin.Atendimento
{
    partial class frmCorporativoExportar
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.View_CorporativoListaBeneficiariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeContezaCorporativoListaBeneficiarios = new ContezaAdmin.ClubeContezaCorporativoListaBeneficiarios();
            this.view_CorporativoListaBeneficiariosTableAdapter1 = new ContezaAdmin.ClubeContezaCorporativoListaBeneficiariosTableAdapters.View_CorporativoListaBeneficiariosTableAdapter();
            this.pcbFiltrarLista = new System.Windows.Forms.PictureBox();
            this.txtFiltroAssociado = new System.Windows.Forms.TextBox();
            this.dgwContratos = new System.Windows.Forms.DataGridView();
            this.TB012_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lTB020_NomeFantasia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_StatusS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lTB012_Edicao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbLista = new System.Windows.Forms.TabPage();
            this.mnuLista = new System.Windows.Forms.MenuStrip();
            this.mnuListaNovo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListaFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tbColaboradores = new System.Windows.Forms.TabPage();
            this.clubeContezaCorporativoListaBeneficiariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.mnuListaExportar = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.View_CorporativoListaBeneficiariosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeContezaCorporativoListaBeneficiarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFiltrarLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwContratos)).BeginInit();
            this.tbLista.SuspendLayout();
            this.mnuLista.SuspendLayout();
            this.tabPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clubeContezaCorporativoListaBeneficiariosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // View_CorporativoListaBeneficiariosBindingSource
            // 
            this.View_CorporativoListaBeneficiariosBindingSource.DataMember = "View_CorporativoListaBeneficiarios";
            this.View_CorporativoListaBeneficiariosBindingSource.DataSource = this.clubeContezaCorporativoListaBeneficiarios;
            // 
            // clubeContezaCorporativoListaBeneficiarios
            // 
            this.clubeContezaCorporativoListaBeneficiarios.DataSetName = "ClubeContezaCorporativoListaBeneficiarios";
            this.clubeContezaCorporativoListaBeneficiarios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // view_CorporativoListaBeneficiariosTableAdapter1
            // 
            this.view_CorporativoListaBeneficiariosTableAdapter1.ClearBeforeFill = true;
            // 
            // pcbFiltrarLista
            // 
            this.pcbFiltrarLista.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbFiltrarLista.Image = global::ContezaAdmin.Properties.Resources.Lupa;
            this.pcbFiltrarLista.Location = new System.Drawing.Point(237, 33);
            this.pcbFiltrarLista.Name = "pcbFiltrarLista";
            this.pcbFiltrarLista.Size = new System.Drawing.Size(24, 21);
            this.pcbFiltrarLista.TabIndex = 17;
            this.pcbFiltrarLista.TabStop = false;
            this.pcbFiltrarLista.Click += new System.EventHandler(this.pcbFiltrarLista_Click);
            // 
            // txtFiltroAssociado
            // 
            this.txtFiltroAssociado.Location = new System.Drawing.Point(9, 35);
            this.txtFiltroAssociado.MaxLength = 60;
            this.txtFiltroAssociado.Name = "txtFiltroAssociado";
            this.txtFiltroAssociado.Size = new System.Drawing.Size(222, 20);
            this.txtFiltroAssociado.TabIndex = 19;
            this.txtFiltroAssociado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroAssociado_KeyPress);
            this.txtFiltroAssociado.Leave += new System.EventHandler(this.txtFiltroAssociado_Leave);
            // 
            // dgwContratos
            // 
            this.dgwContratos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwContratos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TB012_id,
            this.lTB020_NomeFantasia,
            this.TB012_StatusS,
            this.lTB012_Edicao});
            this.dgwContratos.Location = new System.Drawing.Point(9, 60);
            this.dgwContratos.Name = "dgwContratos";
            this.dgwContratos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwContratos.Size = new System.Drawing.Size(1258, 401);
            this.dgwContratos.TabIndex = 20;
            // 
            // TB012_id
            // 
            this.TB012_id.DataPropertyName = "TB012_id";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TB012_id.DefaultCellStyle = dataGridViewCellStyle2;
            this.TB012_id.HeaderText = "Contrato";
            this.TB012_id.Name = "TB012_id";
            this.TB012_id.ReadOnly = true;
            // 
            // lTB020_NomeFantasia
            // 
            this.lTB020_NomeFantasia.DataPropertyName = "TB020_NomeFantasia";
            this.lTB020_NomeFantasia.HeaderText = "Nome Fantasia";
            this.lTB020_NomeFantasia.Name = "lTB020_NomeFantasia";
            this.lTB020_NomeFantasia.ReadOnly = true;
            this.lTB020_NomeFantasia.Width = 400;
            // 
            // TB012_StatusS
            // 
            this.TB012_StatusS.DataPropertyName = "TB012_StatusS";
            this.TB012_StatusS.HeaderText = "Status";
            this.TB012_StatusS.Name = "TB012_StatusS";
            this.TB012_StatusS.ReadOnly = true;
            // 
            // lTB012_Edicao
            // 
            this.lTB012_Edicao.DataPropertyName = "TB012_Edicao";
            this.lTB012_Edicao.HeaderText = "Edição";
            this.lTB012_Edicao.Name = "lTB012_Edicao";
            this.lTB012_Edicao.ReadOnly = true;
            // 
            // tbLista
            // 
            this.tbLista.Controls.Add(this.dgwContratos);
            this.tbLista.Controls.Add(this.pcbFiltrarLista);
            this.tbLista.Controls.Add(this.txtFiltroAssociado);
            this.tbLista.Controls.Add(this.mnuLista);
            this.tbLista.Location = new System.Drawing.Point(4, 22);
            this.tbLista.Name = "tbLista";
            this.tbLista.Padding = new System.Windows.Forms.Padding(3);
            this.tbLista.Size = new System.Drawing.Size(1273, 471);
            this.tbLista.TabIndex = 0;
            this.tbLista.Text = "Lista";
            this.tbLista.UseVisualStyleBackColor = true;
            // 
            // mnuLista
            // 
            this.mnuLista.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuListaNovo,
            this.mnuListaExportar,
            this.mnuListaFechar});
            this.mnuLista.Location = new System.Drawing.Point(3, 3);
            this.mnuLista.Name = "mnuLista";
            this.mnuLista.Size = new System.Drawing.Size(1267, 24);
            this.mnuLista.TabIndex = 1;
            this.mnuLista.Text = "menuStrip1";
            // 
            // mnuListaNovo
            // 
            this.mnuListaNovo.Name = "mnuListaNovo";
            this.mnuListaNovo.Size = new System.Drawing.Size(48, 20);
            this.mnuListaNovo.Text = "Novo";
            // 
            // mnuListaFechar
            // 
            this.mnuListaFechar.Name = "mnuListaFechar";
            this.mnuListaFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuListaFechar.Text = "Fechar";
            this.mnuListaFechar.Click += new System.EventHandler(this.mnuListaFechar_Click);
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.tbLista);
            this.tabPrincipal.Controls.Add(this.tbColaboradores);
            this.tabPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPrincipal.Location = new System.Drawing.Point(0, 29);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(1281, 497);
            this.tabPrincipal.TabIndex = 8;
            // 
            // tbColaboradores
            // 
            this.tbColaboradores.Location = new System.Drawing.Point(4, 22);
            this.tbColaboradores.Name = "tbColaboradores";
            this.tbColaboradores.Size = new System.Drawing.Size(1273, 471);
            this.tbColaboradores.TabIndex = 1;
            this.tbColaboradores.Text = "Colaboradores";
            this.tbColaboradores.UseVisualStyleBackColor = true;
            // 
            // clubeContezaCorporativoListaBeneficiariosBindingSource
            // 
            this.clubeContezaCorporativoListaBeneficiariosBindingSource.DataSource = this.clubeContezaCorporativoListaBeneficiarios;
            this.clubeContezaCorporativoListaBeneficiariosBindingSource.Position = 0;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1281, 29);
            this.label13.TabIndex = 7;
            this.label13.Text = "Corporativo (Exportar)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuListaExportar
            // 
            this.mnuListaExportar.Name = "mnuListaExportar";
            this.mnuListaExportar.Size = new System.Drawing.Size(62, 20);
            this.mnuListaExportar.Text = "Exportar";
            this.mnuListaExportar.Click += new System.EventHandler(this.mnuListaExportar_Click);
            // 
            // frmCorporativoExportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 526);
            this.Controls.Add(this.tabPrincipal);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCorporativoExportar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCorporativoExportar";
            this.Load += new System.EventHandler(this.frmCorporativoExportar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.View_CorporativoListaBeneficiariosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeContezaCorporativoListaBeneficiarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFiltrarLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwContratos)).EndInit();
            this.tbLista.ResumeLayout(false);
            this.tbLista.PerformLayout();
            this.mnuLista.ResumeLayout(false);
            this.mnuLista.PerformLayout();
            this.tabPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clubeContezaCorporativoListaBeneficiariosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource View_CorporativoListaBeneficiariosBindingSource;
        private ClubeContezaCorporativoListaBeneficiarios clubeContezaCorporativoListaBeneficiarios;
        private ClubeContezaCorporativoListaBeneficiariosTableAdapters.View_CorporativoListaBeneficiariosTableAdapter view_CorporativoListaBeneficiariosTableAdapter1;
        private System.Windows.Forms.PictureBox pcbFiltrarLista;
        private System.Windows.Forms.TextBox txtFiltroAssociado;
        private System.Windows.Forms.DataGridView dgwContratos;
        private System.Windows.Forms.TabPage tbLista;
        private System.Windows.Forms.MenuStrip mnuLista;
        private System.Windows.Forms.ToolStripMenuItem mnuListaNovo;
        private System.Windows.Forms.ToolStripMenuItem mnuListaFechar;
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.BindingSource clubeContezaCorporativoListaBeneficiariosBindingSource;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tbColaboradores;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn lTB020_NomeFantasia;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_StatusS;
        private System.Windows.Forms.DataGridViewTextBoxColumn lTB012_Edicao;
        private System.Windows.Forms.ToolStripMenuItem mnuListaExportar;
    }
}