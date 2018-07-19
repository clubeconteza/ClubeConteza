namespace ContezaAdmin.RPT
{
    partial class frmRpt0025
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt0025));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTRPT0025BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mnuRelatorio = new System.Windows.Forms.MenuStrip();
            this.mnuRelatorioFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ptbFiltrar = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rpwRPT0025 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dTRPT0025TableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.DTRPT0025TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0025BindingSource)).BeginInit();
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
            this.SuspendLayout();
            // 
            // dTRPT0025BindingSource
            // 
            this.dTRPT0025BindingSource.DataMember = "DTRPT0025";
            this.dTRPT0025BindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1336F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(831, 553);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 801F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 535F));
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1336, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pcbFechar
            // 
            this.pcbFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbFechar.Image = global::ContezaAdmin.Properties.Resources.Excluir;
            this.pcbFechar.Location = new System.Drawing.Point(804, 3);
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
            this.label13.Size = new System.Drawing.Size(801, 27);
            this.label13.TabIndex = 5;
            this.label13.Text = "Inadimplencia (Parceiros)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuRelatorio
            // 
            this.mnuRelatorio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRelatorioFechar});
            this.mnuRelatorio.Location = new System.Drawing.Point(0, 27);
            this.mnuRelatorio.Name = "mnuRelatorio";
            this.mnuRelatorio.Size = new System.Drawing.Size(1336, 23);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.ptbFiltrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1330, 28);
            this.panel1.TabIndex = 2;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1330, 463);
            this.panel2.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1330, 463);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rpwRPT0025);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1322, 437);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Relatório";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rpwRPT0025
            // 
            reportDataSource1.Name = "DS25";
            reportDataSource1.Value = this.dTRPT0025BindingSource;
            this.rpwRPT0025.LocalReport.DataSources.Add(reportDataSource1);
            this.rpwRPT0025.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0025..rdlc";
            this.rpwRPT0025.Location = new System.Drawing.Point(6, 9);
            this.rpwRPT0025.Name = "rpwRPT0025";
            this.rpwRPT0025.Size = new System.Drawing.Size(812, 424);
            this.rpwRPT0025.TabIndex = 1;
            // 
            // dTRPT0025TableAdapter
            // 
            this.dTRPT0025TableAdapter.ClearBeforeFill = true;
            // 
            // frmRpt0025
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRpt0025";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRpt0025";
            this.Load += new System.EventHandler(this.frmRpt0025_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRPT0025BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.mnuRelatorio.ResumeLayout(false);
            this.mnuRelatorio.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
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
        private System.Windows.Forms.PictureBox ptbFiltrar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRPT0025;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private System.Windows.Forms.BindingSource dTRPT0025BindingSource;
        private ClubeConteza_RelatoriosTableAdapters.DTRPT0025TableAdapter dTRPT0025TableAdapter;
    }
}