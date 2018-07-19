namespace ContezaAdmin.RPT
{
    partial class frmRpt0024
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt0024));
            this.dTRPT0024BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rpwRPT0023 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DTRPT0023BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbFiltroMegociador = new System.Windows.Forms.ComboBox();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.ptbFiltrar = new System.Windows.Forms.PictureBox();
            this.dtmFiltroDataInicio = new System.Windows.Forms.DateTimePicker();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.dTRPT0024TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0024TableAdapter();
            this.dtmFiltroDataFim = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0024BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DTRPT0023BindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mnuRelatorio.SuspendLayout();
            this.SuspendLayout();
            // 
            // dTRPT0024BindingSource
            // 
            this.dTRPT0024BindingSource.DataMember = "DTRPT0024";
            this.dTRPT0024BindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rpwRPT0023);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1318, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Relatório";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rpwRPT0023
            // 
            reportDataSource4.Name = "DSRPT0024";
            reportDataSource4.Value = this.dTRPT0024BindingSource;
            this.rpwRPT0023.LocalReport.DataSources.Add(reportDataSource4);
            this.rpwRPT0023.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0024.rdlc";
            this.rpwRPT0023.Location = new System.Drawing.Point(6, 9);
            this.rpwRPT0023.Name = "rpwRPT0023";
            this.rpwRPT0023.Size = new System.Drawing.Size(1305, 424);
            this.rpwRPT0023.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1326, 460);
            this.tabControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1326, 460);
            this.panel2.TabIndex = 3;
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
            this.panel1.Size = new System.Drawing.Size(1326, 28);
            this.panel1.TabIndex = 2;
            // 
            // cmbFiltroMegociador
            // 
            this.cmbFiltroMegociador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroMegociador.FormattingEnabled = true;
            this.cmbFiltroMegociador.Location = new System.Drawing.Point(469, 5);
            this.cmbFiltroMegociador.Name = "cmbFiltroMegociador";
            this.cmbFiltroMegociador.Size = new System.Drawing.Size(224, 21);
            this.cmbFiltroMegociador.TabIndex = 26;
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(361, 5);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(102, 20);
            this.txtFiltro.TabIndex = 16;
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
            // 
            // mnuRelatorioFechar
            // 
            this.mnuRelatorioFechar.Name = "mnuRelatorioFechar";
            this.mnuRelatorioFechar.Size = new System.Drawing.Size(54, 19);
            this.mnuRelatorioFechar.Text = "Fechar";
            this.mnuRelatorioFechar.Click += new System.EventHandler(this.mnuRelatorioFechar_Click);
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
            this.label13.Text = "Negociação [Pendente]";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1298F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1332, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1332F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1332, 550);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioFechar});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1332, 23);
            this.mnuRelatorio.TabIndex = 1;
            this.mnuRelatorio.Text = "menuStrip1";
            // 
            // dTRPT0024TableAdapter
            // 
            this.dTRPT0024TableAdapter.ClearBeforeFill = true;
            // 
            // dtmFiltroDataFim
            // 
            this.dtmFiltroDataFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFiltroDataFim.Location = new System.Drawing.Point(699, 5);
            this.dtmFiltroDataFim.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmFiltroDataFim.MinDate = new System.DateTime(2017, 3, 13, 0, 0, 0, 0);
            this.dtmFiltroDataFim.Name = "dtmFiltroDataFim";
            this.dtmFiltroDataFim.Size = new System.Drawing.Size(102, 20);
            this.dtmFiltroDataFim.TabIndex = 28;
            this.dtmFiltroDataFim.Value = new System.DateTime(2017, 11, 10, 0, 0, 0, 0);
            // 
            // frmRpt0024
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 550);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRpt0024";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRpt0024";
            this.Load += new System.EventHandler(this.frmRpt0024_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0024BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DTRPT0023BindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mnuRelatorio.ResumeLayout(false);
            this.mnuRelatorio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRPT0023;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource DTRPT0023BindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbFiltroMegociador;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.PictureBox ptbFiltrar;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataInicio;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mnuRelatorioFechar;
        private System.Windows.Forms.PictureBox pcbFechar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip mnuRelatorio;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private System.Windows.Forms.BindingSource dTRPT0024BindingSource;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0024TableAdapter dTRPT0024TableAdapter;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataFim;
    }
}