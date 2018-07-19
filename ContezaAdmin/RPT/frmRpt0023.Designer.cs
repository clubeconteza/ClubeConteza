namespace ContezaAdmin.RPT
{
    partial class frmRpt0023
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt0023));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTRPT0023BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbFiltroMegociador = new System.Windows.Forms.ComboBox();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.ptbFiltrar = new System.Windows.Forms.PictureBox();
            this.dtmFiltroDataInicio = new System.Windows.Forms.DateTimePicker();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rpwRPT0023 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DTRPT0023BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dTRPT0023TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0023TableAdapter();
            this.dtmFiltroDataFim = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0023BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).BeginInit();
            this.mnuRelatorio.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DTRPT0023BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRPT0023BindingSource1
            // 
            this.dTRPT0023BindingSource1.DataMember = "DTRPT0023";
            this.dTRPT0023BindingSource1.DataSource = this.clubeConteza_Relatorios;
            this.dTRPT0023BindingSource1.CurrentChanged += new System.EventHandler(this.dTRPT0023BindingSource1_CurrentChanged);
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1322, 551);
            this.tableLayoutPanel1.TabIndex = 7;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
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
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
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
            this.label13.Text = "Negociação [Recebidos]";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioFechar});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1331, 23);
            this.mnuRelatorio.TabIndex = 1;
            this.mnuRelatorio.Text = "menuStrip1";
            this.mnuRelatorio.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuRelatorio_ItemClicked);
            // 
            // mnuRelatorioFechar
            // 
            this.mnuRelatorioFechar.Name = "mnuRelatorioFechar";
            this.mnuRelatorioFechar.Size = new System.Drawing.Size(54, 19);
            this.mnuRelatorioFechar.Text = "Fechar";
            this.mnuRelatorioFechar.Click += new System.EventHandler(this.mnuRelatorioFechar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtmFiltroDataFim);
            this.panel1.Controls.Add(this.cmbFiltroMegociador);
            this.panel1.Controls.Add(this.txtFiltro);
            this.panel1.Controls.Add(this.ptbFiltrar);
            this.panel1.Controls.Add(this.dtmFiltroDataInicio);
            this.panel1.Controls.Add(this.cmbFiltroTipo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1325, 28);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmbFiltroMegociador
            // 
            this.cmbFiltroMegociador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroMegociador.FormattingEnabled = true;
            this.cmbFiltroMegociador.Location = new System.Drawing.Point(613, 4);
            this.cmbFiltroMegociador.Name = "cmbFiltroMegociador";
            this.cmbFiltroMegociador.Size = new System.Drawing.Size(224, 21);
            this.cmbFiltroMegociador.TabIndex = 26;
            this.cmbFiltroMegociador.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroMegociador_SelectedIndexChanged);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(505, 4);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(102, 20);
            this.txtFiltro.TabIndex = 16;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
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
            // dtmFiltroDataInicio
            // 
            this.dtmFiltroDataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFiltroDataInicio.Location = new System.Drawing.Point(240, 5);
            this.dtmFiltroDataInicio.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmFiltroDataInicio.MinDate = new System.DateTime(2017, 3, 13, 0, 0, 0, 0);
            this.dtmFiltroDataInicio.Name = "dtmFiltroDataInicio";
            this.dtmFiltroDataInicio.Size = new System.Drawing.Size(102, 20);
            this.dtmFiltroDataInicio.TabIndex = 2;
            this.dtmFiltroDataInicio.Value = new System.DateTime(2017, 11, 10, 0, 0, 0, 0);
            this.dtmFiltroDataInicio.ValueChanged += new System.EventHandler(this.dtmFiltroDataReferencia_ValueChanged);
            // 
            // cmbFiltroTipo
            // 
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.FormattingEnabled = true;
            this.cmbFiltroTipo.Items.AddRange(new object[] {
            "Pagamento",
            "Emissão",
            "Vencimento",
            "Negociador",
            "Contrato",
            "CPF"});
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
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1325, 461);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1325, 461);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rpwRPT0023);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1317, 435);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Relatório";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // rpwRPT0023
            // 
            reportDataSource1.Name = "dSRpt0023";
            reportDataSource1.Value = this.dTRPT0023BindingSource1;
            this.rpwRPT0023.LocalReport.DataSources.Add(reportDataSource1);
            this.rpwRPT0023.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0023.rdlc";
            this.rpwRPT0023.Location = new System.Drawing.Point(20, 9);
            this.rpwRPT0023.Name = "rpwRPT0023";
            this.rpwRPT0023.Size = new System.Drawing.Size(1291, 424);
            this.rpwRPT0023.TabIndex = 1;
            this.rpwRPT0023.Load += new System.EventHandler(this.rpwRPT0023_Load);
            // 
            // DTRPT0023BindingSource
            // 
            this.DTRPT0023BindingSource.DataMember = "DTRPT0023";
            this.DTRPT0023BindingSource.DataSource = this.clubeConteza_Relatorios;
            this.DTRPT0023BindingSource.CurrentChanged += new System.EventHandler(this.DTRPT0023BindingSource_CurrentChanged);
            // 
            // dTRPT0023TableAdapter
            // 
            this.dTRPT0023TableAdapter.ClearBeforeFill = true;
            // 
            // dtmFiltroDataFim
            // 
            this.dtmFiltroDataFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFiltroDataFim.Location = new System.Drawing.Point(348, 5);
            this.dtmFiltroDataFim.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmFiltroDataFim.MinDate = new System.DateTime(2017, 3, 13, 0, 0, 0, 0);
            this.dtmFiltroDataFim.Name = "dtmFiltroDataFim";
            this.dtmFiltroDataFim.Size = new System.Drawing.Size(102, 20);
            this.dtmFiltroDataFim.TabIndex = 27;
            this.dtmFiltroDataFim.Value = new System.DateTime(2017, 11, 10, 0, 0, 0, 0);
            // 
            // frmRpt0023
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 551);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRpt0023";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRpt0023";
            this.Load += new System.EventHandler(this.frmRpt0023_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0023BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.mnuRelatorio.ResumeLayout(false);
            this.mnuRelatorio.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DTRPT0023BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pcbFechar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MenuStrip mnuRelatorio;
        private System.Windows.Forms.ToolStripMenuItem mnuRelatorioFechar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.PictureBox ptbFiltrar;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataInicio;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRPT0023;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private System.Windows.Forms.BindingSource DTRPT0023BindingSource;
        private System.Windows.Forms.BindingSource dTRPT0023BindingSource1;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0023TableAdapter dTRPT0023TableAdapter;
        private System.Windows.Forms.ComboBox cmbFiltroMegociador;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataFim;
    }
}