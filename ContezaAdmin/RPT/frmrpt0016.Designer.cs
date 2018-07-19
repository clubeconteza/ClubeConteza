namespace ContezaAdmin.RPT
{
    partial class Frmrpt0016
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmrpt0016));
            this.dTRPT0016BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.rpwRpt0016 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbContratoPontosDeVenda = new System.Windows.Forms.ComboBox();
            this.dtmDataFim = new System.Windows.Forms.DateTimePicker();
            this.dtmDataInicio = new System.Windows.Forms.DateTimePicker();
            this.ptbGerarRelatorio = new System.Windows.Forms.PictureBox();
            this.dTRPT0016TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0016TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0016BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).BeginInit();
            this.mnuRelatorio.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbGerarRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRPT0016BindingSource
            // 
            this.dTRPT0016BindingSource.DataMember = "DTRPT0016";
            this.dTRPT0016BindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1236F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mnuRelatorio, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rpwRpt0016, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 368F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1236, 460);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1179F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1236, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pcbFechar
            // 
            this.pcbFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbFechar.Image = global::ContezaAdmin.Properties.Resources.Excluir;
            this.pcbFechar.Location = new System.Drawing.Point(1182, 3);
            this.pcbFechar.Name = "pcbFechar";
            this.pcbFechar.Size = new System.Drawing.Size(23, 21);
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
            this.label13.Size = new System.Drawing.Size(1179, 27);
            this.label13.TabIndex = 5;
            this.label13.Text = "Comissões";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioFechar});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1236, 23);
            this.mnuRelatorio.TabIndex = 1;
            this.mnuRelatorio.Text = "menuStrip1";
            // 
            // mnuRelatorioFechar
            // 
            this.mnuRelatorioFechar.Name = "mnuRelatorioFechar";
            this.mnuRelatorioFechar.Size = new System.Drawing.Size(54, 19);
            this.mnuRelatorioFechar.Text = "Fechar";
            this.mnuRelatorioFechar.Click += new System.EventHandler(this.mnuRelatorioFechar_Click);
            // 
            // rpwRpt0016
            // 
            this.rpwRpt0016.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DTRPT0016";
            reportDataSource1.Value = this.dTRPT0016BindingSource;
            this.rpwRpt0016.LocalReport.DataSources.Add(reportDataSource1);
            this.rpwRpt0016.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0016.rdlc";
            this.rpwRpt0016.Location = new System.Drawing.Point(3, 87);
            this.rpwRpt0016.Name = "rpwRpt0016";
            this.rpwRpt0016.Size = new System.Drawing.Size(1230, 362);
            this.rpwRpt0016.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbContratoPontosDeVenda);
            this.panel1.Controls.Add(this.dtmDataFim);
            this.panel1.Controls.Add(this.dtmDataInicio);
            this.panel1.Controls.Add(this.ptbGerarRelatorio);
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 28);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "á";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Local";
            // 
            // cmbContratoPontosDeVenda
            // 
            this.cmbContratoPontosDeVenda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContratoPontosDeVenda.FormattingEnabled = true;
            this.cmbContratoPontosDeVenda.Location = new System.Drawing.Point(305, 3);
            this.cmbContratoPontosDeVenda.Name = "cmbContratoPontosDeVenda";
            this.cmbContratoPontosDeVenda.Size = new System.Drawing.Size(224, 21);
            this.cmbContratoPontosDeVenda.TabIndex = 25;
            // 
            // dtmDataFim
            // 
            this.dtmDataFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDataFim.Location = new System.Drawing.Point(149, 4);
            this.dtmDataFim.Name = "dtmDataFim";
            this.dtmDataFim.Size = new System.Drawing.Size(102, 20);
            this.dtmDataFim.TabIndex = 16;
            this.dtmDataFim.Value = new System.DateTime(2017, 9, 30, 0, 0, 0, 0);
            // 
            // dtmDataInicio
            // 
            this.dtmDataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDataInicio.Location = new System.Drawing.Point(3, 5);
            this.dtmDataInicio.Name = "dtmDataInicio";
            this.dtmDataInicio.Size = new System.Drawing.Size(102, 20);
            this.dtmDataInicio.TabIndex = 15;
            this.dtmDataInicio.Value = new System.DateTime(2017, 9, 30, 0, 0, 0, 0);
            // 
            // ptbGerarRelatorio
            // 
            this.ptbGerarRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbGerarRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("ptbGerarRelatorio.Image")));
            this.ptbGerarRelatorio.Location = new System.Drawing.Point(535, 3);
            this.ptbGerarRelatorio.Name = "ptbGerarRelatorio";
            this.ptbGerarRelatorio.Size = new System.Drawing.Size(24, 21);
            this.ptbGerarRelatorio.TabIndex = 14;
            this.ptbGerarRelatorio.TabStop = false;
            this.ptbGerarRelatorio.Click += new System.EventHandler(this.ptbGerarRelatorio_Click);
            // 
            // dTRPT0016TableAdapter
            // 
            this.dTRPT0016TableAdapter.ClearBeforeFill = true;
            // 
            // Frmrpt0016
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 460);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frmrpt0016";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmrpt0016";
            this.Load += new System.EventHandler(this.Frmrpt0016_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0016BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.mnuRelatorio.ResumeLayout(false);
            this.mnuRelatorio.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbGerarRelatorio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pcbFechar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MenuStrip mnuRelatorio;
        private System.Windows.Forms.ToolStripMenuItem mnuRelatorioFechar;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRpt0016;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtmDataInicio;
        private System.Windows.Forms.PictureBox ptbGerarRelatorio;
        private System.Windows.Forms.BindingSource dTRPT0016BindingSource;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0016TableAdapter dTRPT0016TableAdapter;
        private System.Windows.Forms.DateTimePicker dtmDataFim;
        private System.Windows.Forms.ComboBox cmbContratoPontosDeVenda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}