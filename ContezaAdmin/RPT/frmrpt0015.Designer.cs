namespace ContezaAdmin.RPT
{
    partial class Frmrpt0015
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmrpt0015));
            this.dTRPT0015BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.rpwRpt0015 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtmContratoCanc = new System.Windows.Forms.DateTimePicker();
            this.ptbGerarRelatorio = new System.Windows.Forms.PictureBox();
            this.dTRPT0015TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0015TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0015BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).BeginInit();
            this.mnuRelatorio.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbGerarRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRPT0015BindingSource
            // 
            this.dTRPT0015BindingSource.DataMember = "DTRPT0015";
            this.dTRPT0015BindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1210F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mnuRelatorio, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rpwRpt0015, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1210, 477);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1179F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1210, 27);
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
            this.label13.Text = "Cancelamentos";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioFechar});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1210, 23);
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
            // rpwRpt0015
            // 
            this.rpwRpt0015.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DSRPT0015";
            reportDataSource2.Value = this.dTRPT0015BindingSource;
            this.rpwRpt0015.LocalReport.DataSources.Add(reportDataSource2);
            this.rpwRpt0015.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0015.rdlc";
            this.rpwRpt0015.Location = new System.Drawing.Point(3, 87);
            this.rpwRpt0015.Name = "rpwRpt0015";
            this.rpwRpt0015.Size = new System.Drawing.Size(1204, 387);
            this.rpwRpt0015.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtmContratoCanc);
            this.panel1.Controls.Add(this.ptbGerarRelatorio);
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 28);
            this.panel1.TabIndex = 2;
            // 
            // dtmContratoCanc
            // 
            this.dtmContratoCanc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmContratoCanc.Location = new System.Drawing.Point(3, 5);
            this.dtmContratoCanc.Name = "dtmContratoCanc";
            this.dtmContratoCanc.Size = new System.Drawing.Size(102, 20);
            this.dtmContratoCanc.TabIndex = 15;
            this.dtmContratoCanc.Value = new System.DateTime(2017, 9, 30, 0, 0, 0, 0);
            // 
            // ptbGerarRelatorio
            // 
            this.ptbGerarRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbGerarRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("ptbGerarRelatorio.Image")));
            this.ptbGerarRelatorio.Location = new System.Drawing.Point(111, 5);
            this.ptbGerarRelatorio.Name = "ptbGerarRelatorio";
            this.ptbGerarRelatorio.Size = new System.Drawing.Size(24, 21);
            this.ptbGerarRelatorio.TabIndex = 14;
            this.ptbGerarRelatorio.TabStop = false;
            this.ptbGerarRelatorio.Click += new System.EventHandler(this.ptbGerarRelatorio_Click);
            // 
            // dTRPT0015TableAdapter
            // 
            this.dTRPT0015TableAdapter.ClearBeforeFill = true;
            // 
            // Frmrpt0015
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 477);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frmrpt0015";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmrpt0015";
            this.Load += new System.EventHandler(this.Frmrpt0015_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0015BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.mnuRelatorio.ResumeLayout(false);
            this.mnuRelatorio.PerformLayout();
            this.panel1.ResumeLayout(false);
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
        private Microsoft.Reporting.WinForms.ReportViewer rpwRpt0015;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ptbGerarRelatorio;
        private System.Windows.Forms.BindingSource dTRPT0015BindingSource;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0015TableAdapter dTRPT0015TableAdapter;
        private System.Windows.Forms.DateTimePicker dtmContratoCanc;
    }
}