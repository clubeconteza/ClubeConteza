namespace ContezaAdmin.RPT
{
    partial class frmrpt0018
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmrpt0018));
            this.dTRPT0018BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rpwRPT0018 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbFiltroPlano = new System.Windows.Forms.ComboBox();
            this.txtFiltroCPFCNPJ = new System.Windows.Forms.TextBox();
            this.cmbFiltroPontoDeVenda = new System.Windows.Forms.ComboBox();
            this.txtFiltroContratoFim = new System.Windows.Forms.TextBox();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRelatorioAtualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFiltroContratoInicio = new System.Windows.Forms.TextBox();
            this.dtmFiltroDataReferenciaFim = new System.Windows.Forms.DateTimePicker();
            this.ptbFiltrar = new System.Windows.Forms.PictureBox();
            this.dtmFiltroDataReferencia = new System.Windows.Forms.DateTimePicker();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dTRPT0018TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0018TableAdapter();
            this.mnuRelatorioTipo = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0018BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).BeginInit();
            this.mnuRelatorio.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dTRPT0018BindingSource
            // 
            this.dTRPT0018BindingSource.DataMember = "DTRPT0018";
            this.dTRPT0018BindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1317, 439);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Exportação";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            reportDataSource7.Name = "DSRPT0018";
            reportDataSource7.Value = this.dTRPT0018BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0018b.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(13, 7);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1291, 424);
            this.reportViewer1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rpwRPT0018);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1317, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Relatório";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rpwRPT0018
            // 
            reportDataSource8.Name = "DSRPT0018";
            reportDataSource8.Value = this.dTRPT0018BindingSource;
            this.rpwRPT0018.LocalReport.DataSources.Add(reportDataSource8);
            this.rpwRPT0018.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0018.rdlc";
            this.rpwRPT0018.Location = new System.Drawing.Point(20, 9);
            this.rpwRPT0018.Name = "rpwRPT0018";
            this.rpwRPT0018.Size = new System.Drawing.Size(1291, 424);
            this.rpwRPT0018.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1325, 465);
            this.tabControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1325, 465);
            this.panel2.TabIndex = 3;
            // 
            // cmbFiltroPlano
            // 
            this.cmbFiltroPlano.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroPlano.FormattingEnabled = true;
            this.cmbFiltroPlano.Location = new System.Drawing.Point(1167, 3);
            this.cmbFiltroPlano.Name = "cmbFiltroPlano";
            this.cmbFiltroPlano.Size = new System.Drawing.Size(148, 21);
            this.cmbFiltroPlano.TabIndex = 27;
            // 
            // txtFiltroCPFCNPJ
            // 
            this.txtFiltroCPFCNPJ.Location = new System.Drawing.Point(1058, 4);
            this.txtFiltroCPFCNPJ.Name = "txtFiltroCPFCNPJ";
            this.txtFiltroCPFCNPJ.Size = new System.Drawing.Size(100, 20);
            this.txtFiltroCPFCNPJ.TabIndex = 26;
            // 
            // cmbFiltroPontoDeVenda
            // 
            this.cmbFiltroPontoDeVenda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroPontoDeVenda.FormattingEnabled = true;
            this.cmbFiltroPontoDeVenda.Location = new System.Drawing.Point(828, 3);
            this.cmbFiltroPontoDeVenda.Name = "cmbFiltroPontoDeVenda";
            this.cmbFiltroPontoDeVenda.Size = new System.Drawing.Size(224, 21);
            this.cmbFiltroPontoDeVenda.TabIndex = 25;
            // 
            // txtFiltroContratoFim
            // 
            this.txtFiltroContratoFim.Location = new System.Drawing.Point(725, 4);
            this.txtFiltroContratoFim.Name = "txtFiltroContratoFim";
            this.txtFiltroContratoFim.Size = new System.Drawing.Size(100, 20);
            this.txtFiltroContratoFim.TabIndex = 17;
            // 
            // mnuRelatorioFechar
            // 
            this.mnuRelatorioFechar.Name = "mnuRelatorioFechar";
            this.mnuRelatorioFechar.Size = new System.Drawing.Size(54, 19);
            this.mnuRelatorioFechar.Text = "Fechar";
            this.mnuRelatorioFechar.Click += new System.EventHandler(this.mnuRelatorioFechar_Click);
            // 
            // mnuRelatorioAtualizar
            // 
            this.mnuRelatorioAtualizar.Name = "mnuRelatorioAtualizar";
            this.mnuRelatorioAtualizar.Size = new System.Drawing.Size(65, 19);
            this.mnuRelatorioAtualizar.Text = "Atualizar";
            this.mnuRelatorioAtualizar.ToolTipText = "Atualizar lista de inadimplentes.";
            // 
            // txtFiltroContratoInicio
            // 
            this.txtFiltroContratoInicio.Location = new System.Drawing.Point(617, 4);
            this.txtFiltroContratoInicio.Name = "txtFiltroContratoInicio";
            this.txtFiltroContratoInicio.Size = new System.Drawing.Size(102, 20);
            this.txtFiltroContratoInicio.TabIndex = 16;
            // 
            // dtmFiltroDataReferenciaFim
            // 
            this.dtmFiltroDataReferenciaFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFiltroDataReferenciaFim.Location = new System.Drawing.Point(512, 4);
            this.dtmFiltroDataReferenciaFim.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmFiltroDataReferenciaFim.MinDate = new System.DateTime(2017, 3, 13, 0, 0, 0, 0);
            this.dtmFiltroDataReferenciaFim.Name = "dtmFiltroDataReferenciaFim";
            this.dtmFiltroDataReferenciaFim.Size = new System.Drawing.Size(102, 20);
            this.dtmFiltroDataReferenciaFim.TabIndex = 15;
            this.dtmFiltroDataReferenciaFim.Value = new System.DateTime(2017, 11, 10, 0, 0, 0, 0);
            this.dtmFiltroDataReferenciaFim.Visible = false;
            // 
            // ptbFiltrar
            // 
            this.ptbFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbFiltrar.Image = ((System.Drawing.Image)(resources.GetObject("ptbFiltrar.Image")));
            this.ptbFiltrar.Location = new System.Drawing.Point(4, 4);
            this.ptbFiltrar.Name = "ptbFiltrar";
            this.ptbFiltrar.Size = new System.Drawing.Size(24, 21);
            this.ptbFiltrar.TabIndex = 14;
            this.ptbFiltrar.TabStop = false;
            this.ptbFiltrar.Click += new System.EventHandler(this.ptbFiltrar_Click);
            // 
            // dtmFiltroDataReferencia
            // 
            this.dtmFiltroDataReferencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFiltroDataReferencia.Location = new System.Drawing.Point(239, 5);
            this.dtmFiltroDataReferencia.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmFiltroDataReferencia.MinDate = new System.DateTime(2017, 3, 13, 0, 0, 0, 0);
            this.dtmFiltroDataReferencia.Name = "dtmFiltroDataReferencia";
            this.dtmFiltroDataReferencia.Size = new System.Drawing.Size(102, 20);
            this.dtmFiltroDataReferencia.TabIndex = 2;
            this.dtmFiltroDataReferencia.Value = new System.DateTime(2017, 11, 10, 0, 0, 0, 0);
            this.dtmFiltroDataReferencia.Visible = false;
            // 
            // cmbFiltroTipo
            // 
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.FormattingEnabled = true;
            this.cmbFiltroTipo.Items.AddRange(new object[] {
            "Contrato",
            "CPF",
            "Emissão",
            "Vencimento",
            "Pagamento",
            "Ponto de Venda",
            "Plano"});
            this.cmbFiltroTipo.Location = new System.Drawing.Point(86, 4);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(148, 21);
            this.cmbFiltroTipo.TabIndex = 1;
            this.cmbFiltroTipo.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroTipo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtros";
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioAtualizar,
            this.mnuRelatorioFechar,
            this.mnuRelatorioTipo});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1331, 23);
            this.mnuRelatorio.TabIndex = 1;
            this.mnuRelatorio.Text = "menuStrip1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1298, 27);
            this.label13.TabIndex = 5;
            this.label13.Text = "Recebidos";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1298F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1331, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pcbFechar
            // 
            this.pcbFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbFechar.Image = global::ContezaAdmin.Properties.Resources.Excluir;
            this.pcbFechar.Location = new System.Drawing.Point(1301, 3);
            this.pcbFechar.Name = "pcbFechar";
            this.pcbFechar.Size = new System.Drawing.Size(22, 21);
            this.pcbFechar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbFechar.TabIndex = 165;
            this.pcbFechar.TabStop = false;
            this.pcbFechar.Click += new System.EventHandler(this.pcbFechar_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1331F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mnuRelatorio, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 285F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1331, 555);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbFiltroPlano);
            this.panel1.Controls.Add(this.txtFiltroCPFCNPJ);
            this.panel1.Controls.Add(this.cmbFiltroPontoDeVenda);
            this.panel1.Controls.Add(this.txtFiltroContratoFim);
            this.panel1.Controls.Add(this.txtFiltroContratoInicio);
            this.panel1.Controls.Add(this.dtmFiltroDataReferenciaFim);
            this.panel1.Controls.Add(this.ptbFiltrar);
            this.panel1.Controls.Add(this.dtmFiltroDataReferencia);
            this.panel1.Controls.Add(this.cmbFiltroTipo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1325, 28);
            this.panel1.TabIndex = 2;
            // 
            // dTRPT0018TableAdapter
            // 
            this.dTRPT0018TableAdapter.ClearBeforeFill = true;
            // 
            // mnuRelatorioTipo
            // 
            this.mnuRelatorioTipo.Items.AddRange(new object[] {
            "Geral",
            "Boleto",
            "Dinheiro",
            "Credito",
            "Debito"});
            this.mnuRelatorioTipo.Name = "mnuRelatorioTipo";
            this.mnuRelatorioTipo.Size = new System.Drawing.Size(121, 19);
            // 
            // frmrpt0018
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 555);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmrpt0018";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmrpt0018";
            this.Load += new System.EventHandler(this.frmrpt0018_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0018BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).EndInit();
            this.mnuRelatorio.ResumeLayout(false);
            this.mnuRelatorio.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbFiltroPlano;
        private System.Windows.Forms.TextBox txtFiltroCPFCNPJ;
        private System.Windows.Forms.ComboBox cmbFiltroPontoDeVenda;
        private System.Windows.Forms.TextBox txtFiltroContratoFim;
        private System.Windows.Forms.ToolStripMenuItem mnuRelatorioFechar;
        private System.Windows.Forms.ToolStripMenuItem mnuRelatorioAtualizar;
        private System.Windows.Forms.TextBox txtFiltroContratoInicio;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataReferenciaFim;
        private System.Windows.Forms.PictureBox ptbFiltrar;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataReferencia;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mnuRelatorio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pcbFechar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRPT0018;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private System.Windows.Forms.BindingSource dTRPT0018BindingSource;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0018TableAdapter dTRPT0018TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStripComboBox mnuRelatorioTipo;
    }
}