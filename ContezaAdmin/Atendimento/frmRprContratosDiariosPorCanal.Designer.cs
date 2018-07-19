namespace ContezaAdmin.Atendimento
{
    partial class frmRprContezinosContratosDiariosPorCanal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRprContezinosContratosDiariosPorCanal));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sPSContratosDiariosPorCanalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBClubeContezaDataSet = new ContezaAdmin.DBClubeContezaDataSet();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmDataInicio = new System.Windows.Forms.DateTimePicker();
            this.ptbFiltrar = new System.Windows.Forms.PictureBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtmDataFim = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.SP_S_ContratosDiariosPorCanalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sP_S_ContratosDiariosPorCanalTableAdapter = new ContezaAdmin.DBClubeContezaDataSetTableAdapters.SP_S_ContratosDiariosPorCanalTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sPSContratosDiariosPorCanalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBClubeContezaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_S_ContratosDiariosPorCanalBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // sPSContratosDiariosPorCanalBindingSource
            // 
            this.sPSContratosDiariosPorCanalBindingSource.DataMember = "SP_S_ContratosDiariosPorCanal";
            this.sPSContratosDiariosPorCanalBindingSource.DataSource = this.dBClubeContezaDataSet;
            // 
            // dBClubeContezaDataSet
            // 
            this.dBClubeContezaDataSet.DataSetName = "DBClubeContezaDataSet";
            this.dBClubeContezaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1197, 28);
            this.label13.TabIndex = 4;
            this.label13.Text = "Contratos diarios por Canal de Venda (Contezinos)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Data";
            // 
            // dtmDataInicio
            // 
            this.dtmDataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDataInicio.Location = new System.Drawing.Point(39, 3);
            this.dtmDataInicio.Name = "dtmDataInicio";
            this.dtmDataInicio.Size = new System.Drawing.Size(109, 20);
            this.dtmDataInicio.TabIndex = 0;
            this.dtmDataInicio.Value = new System.DateTime(2017, 10, 16, 0, 0, 0, 0);
            // 
            // ptbFiltrar
            // 
            this.ptbFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbFiltrar.Image = ((System.Drawing.Image)(resources.GetObject("ptbFiltrar.Image")));
            this.ptbFiltrar.Location = new System.Drawing.Point(303, 3);
            this.ptbFiltrar.Name = "ptbFiltrar";
            this.ptbFiltrar.Size = new System.Drawing.Size(24, 21);
            this.ptbFiltrar.TabIndex = 7;
            this.ptbFiltrar.TabStop = false;
            this.ptbFiltrar.Click += new System.EventHandler(this.ptbFiltrar_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.sPSContratosDiariosPorCanalBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0004.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 61);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1197, 460);
            this.reportViewer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.reportViewer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1203, 524);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtmDataFim);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnFechar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtmDataInicio);
            this.panel1.Controls.Add(this.ptbFiltrar);
            this.panel1.Location = new System.Drawing.Point(3, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 24);
            this.panel1.TabIndex = 0;
            // 
            // dtmDataFim
            // 
            this.dtmDataFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDataFim.Location = new System.Drawing.Point(173, 3);
            this.dtmDataFim.Name = "dtmDataFim";
            this.dtmDataFim.Size = new System.Drawing.Size(109, 20);
            this.dtmDataFim.TabIndex = 1;
            this.dtmDataFim.Value = new System.DateTime(2017, 10, 16, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "á";
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(357, -2);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(64, 23);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // SP_S_ContratosDiariosPorCanalBindingSource
            // 
            this.SP_S_ContratosDiariosPorCanalBindingSource.DataMember = "SP_S_ContratosDiariosPorCanal";
            this.SP_S_ContratosDiariosPorCanalBindingSource.DataSource = this.dBClubeContezaDataSet;
            // 
            // sP_S_ContratosDiariosPorCanalTableAdapter
            // 
            this.sP_S_ContratosDiariosPorCanalTableAdapter.ClearBeforeFill = true;
            // 
            // frmRprContezinosContratosDiariosPorCanal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 524);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRprContezinosContratosDiariosPorCanal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contratos diarios por Canal de Venda (Contezinos)";
            this.Load += new System.EventHandler(this.frmRprContezinosContratosDiariosPorCanal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sPSContratosDiariosPorCanalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBClubeContezaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFiltrar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_S_ContratosDiariosPorCanalBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtmDataInicio;
        private System.Windows.Forms.PictureBox ptbFiltrar;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sPSContratosDiariosPorCanalBindingSource;
        private DBClubeContezaDataSet dBClubeContezaDataSet;
        private DBClubeContezaDataSetTableAdapters.SP_S_ContratosDiariosPorCanalTableAdapter sP_S_ContratosDiariosPorCanalTableAdapter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.BindingSource SP_S_ContratosDiariosPorCanalBindingSource;
        private System.Windows.Forms.DateTimePicker dtmDataFim;
        private System.Windows.Forms.Label label2;
    }
}