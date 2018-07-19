namespace ContezaAdmin.RPT
{
    partial class frmRPT0017
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPT0017));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.dTRPT0017BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.mnuRelatorioAtualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbFiltroPlano = new System.Windows.Forms.ComboBox();
            this.txtFiltroCPFCNPJ = new System.Windows.Forms.TextBox();
            this.cmbFiltroPontoDeVenda = new System.Windows.Forms.ComboBox();
            this.txtFiltroContratoFim = new System.Windows.Forms.TextBox();
            this.txtFiltroContratoInicio = new System.Windows.Forms.TextBox();
            this.dtmFiltroDataReferenciaFim = new System.Windows.Forms.DateTimePicker();
            this.ptbFiltrar = new System.Windows.Forms.PictureBox();
            this.dtmFiltroDataReferencia = new System.Windows.Forms.DateTimePicker();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rpwRPT0017 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dTRPT0017TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0017TableAdapter();
            this.chtAnual = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0017BindingSource)).BeginInit();
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
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtAnual)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRPT0017BindingSource
            // 
            this.dTRPT0017BindingSource.DataMember = "DTRPT0017";
            this.dTRPT0017BindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1327F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1327, 549);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1298F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1327, 27);
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
            this.label13.Text = "Inadimplencia";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioAtualizar,
            this.mnuRelatorioFechar});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1327, 23);
            this.mnuRelatorio.TabIndex = 1;
            this.mnuRelatorio.Text = "menuStrip1";
            // 
            // mnuRelatorioAtualizar
            // 
            this.mnuRelatorioAtualizar.Name = "mnuRelatorioAtualizar";
            this.mnuRelatorioAtualizar.Size = new System.Drawing.Size(65, 19);
            this.mnuRelatorioAtualizar.Text = "Atualizar";
            this.mnuRelatorioAtualizar.ToolTipText = "Atualizar lista de inadimplentes.";
            this.mnuRelatorioAtualizar.Click += new System.EventHandler(this.mnuRelatorioAtualizar_Click);
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
            this.panel1.Size = new System.Drawing.Size(1321, 28);
            this.panel1.TabIndex = 2;
            // 
            // cmbFiltroPlano
            // 
            this.cmbFiltroPlano.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroPlano.FormattingEnabled = true;
            this.cmbFiltroPlano.Location = new System.Drawing.Point(1005, 6);
            this.cmbFiltroPlano.Name = "cmbFiltroPlano";
            this.cmbFiltroPlano.Size = new System.Drawing.Size(224, 21);
            this.cmbFiltroPlano.TabIndex = 27;
            // 
            // txtFiltroCPFCNPJ
            // 
            this.txtFiltroCPFCNPJ.Location = new System.Drawing.Point(899, 7);
            this.txtFiltroCPFCNPJ.Name = "txtFiltroCPFCNPJ";
            this.txtFiltroCPFCNPJ.Size = new System.Drawing.Size(100, 20);
            this.txtFiltroCPFCNPJ.TabIndex = 26;
            // 
            // cmbFiltroPontoDeVenda
            // 
            this.cmbFiltroPontoDeVenda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroPontoDeVenda.FormattingEnabled = true;
            this.cmbFiltroPontoDeVenda.Location = new System.Drawing.Point(669, 6);
            this.cmbFiltroPontoDeVenda.Name = "cmbFiltroPontoDeVenda";
            this.cmbFiltroPontoDeVenda.Size = new System.Drawing.Size(224, 21);
            this.cmbFiltroPontoDeVenda.TabIndex = 25;
            // 
            // txtFiltroContratoFim
            // 
            this.txtFiltroContratoFim.Location = new System.Drawing.Point(563, 6);
            this.txtFiltroContratoFim.Name = "txtFiltroContratoFim";
            this.txtFiltroContratoFim.Size = new System.Drawing.Size(100, 20);
            this.txtFiltroContratoFim.TabIndex = 17;
            // 
            // txtFiltroContratoInicio
            // 
            this.txtFiltroContratoInicio.Location = new System.Drawing.Point(455, 6);
            this.txtFiltroContratoInicio.Name = "txtFiltroContratoInicio";
            this.txtFiltroContratoInicio.Size = new System.Drawing.Size(102, 20);
            this.txtFiltroContratoInicio.TabIndex = 16;
            // 
            // dtmFiltroDataReferenciaFim
            // 
            this.dtmFiltroDataReferenciaFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFiltroDataReferenciaFim.Location = new System.Drawing.Point(350, 6);
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
            this.dtmFiltroDataReferencia.Location = new System.Drawing.Point(240, 5);
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
            "Ponto de Venda",
            "Plano"});
            this.cmbFiltroTipo.Location = new System.Drawing.Point(87, 4);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(148, 21);
            this.cmbFiltroTipo.TabIndex = 1;
            this.cmbFiltroTipo.SelectedValueChanged += new System.EventHandler(this.cmbFiltroTipo_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtros";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1321, 459);
            this.panel2.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1321, 459);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chtAnual);
            this.tabPage1.Controls.Add(this.rpwRPT0017);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1313, 433);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Relatório";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rpwRPT0017
            // 
            reportDataSource1.Name = "DSRPT0017";
            reportDataSource1.Value = this.dTRPT0017BindingSource;
            this.rpwRPT0017.LocalReport.DataSources.Add(reportDataSource1);
            this.rpwRPT0017.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0017.rdlc";
            this.rpwRPT0017.Location = new System.Drawing.Point(470, 6);
            this.rpwRPT0017.Name = "rpwRPT0017";
            this.rpwRPT0017.Size = new System.Drawing.Size(840, 424);
            this.rpwRPT0017.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1313, 433);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Exportação";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            reportDataSource2.Name = "DSRPT0017";
            reportDataSource2.Value = this.dTRPT0017BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0017b .rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(51, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1174, 427);
            this.reportViewer1.TabIndex = 4;
            // 
            // dTRPT0017TableAdapter
            // 
            this.dTRPT0017TableAdapter.ClearBeforeFill = true;
            // 
            // chtAnual
            // 
            this.chtAnual.BackSecondaryColor = System.Drawing.Color.Blue;
            this.chtAnual.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chtAnual.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.chtAnual.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chtAnual.Legends.Add(legend1);
            this.chtAnual.Location = new System.Drawing.Point(3, 3);
            this.chtAnual.Name = "chtAnual";
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Red;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.LabelBackColor = System.Drawing.Color.Black;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.Transparent;
            series1.MarkerColor = System.Drawing.Color.Transparent;
            series1.Name = "Series1";
            series1.SmartLabelStyle.CalloutBackColor = System.Drawing.Color.Silver;
            series1.SmartLabelStyle.CalloutLineColor = System.Drawing.Color.White;
            series1.XValueMember = "mes";
            series1.YValueMembers = "total";
            this.chtAnual.Series.Add(series1);
            this.chtAnual.Size = new System.Drawing.Size(461, 260);
            this.chtAnual.TabIndex = 2;
            this.chtAnual.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Anual";
            this.chtAnual.Titles.Add(title1);
            // 
            // frmRPT0017
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 549);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRPT0017";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRPT0017";
            this.Load += new System.EventHandler(this.frmRPT0017_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0017BindingSource)).EndInit();
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
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtAnual)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem mnuRelatorioAtualizar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataReferencia;
        private System.Windows.Forms.PictureBox ptbFiltrar;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private System.Windows.Forms.BindingSource dTRPT0017BindingSource;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0017TableAdapter dTRPT0017TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRPT0017;
        private System.Windows.Forms.DateTimePicker dtmFiltroDataReferenciaFim;
        private System.Windows.Forms.TextBox txtFiltroContratoFim;
        private System.Windows.Forms.TextBox txtFiltroContratoInicio;
        private System.Windows.Forms.ComboBox cmbFiltroPontoDeVenda;
        private System.Windows.Forms.TextBox txtFiltroCPFCNPJ;
        private System.Windows.Forms.ComboBox cmbFiltroPlano;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtAnual;
    }
}