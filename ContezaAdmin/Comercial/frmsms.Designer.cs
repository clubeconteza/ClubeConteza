namespace ContezaAdmin.Comercial
{
    partial class frmsms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmsms));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnuListaFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.dgwLista = new System.Windows.Forms.DataGridView();
            this.pctFiltrar = new System.Windows.Forms.PictureBox();
            this.mnuLista = new System.Windows.Forms.MenuStrip();
            this.dtmReferencia = new System.Windows.Forms.DateTimePicker();
            this.TB012_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB013_NomeCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_Destino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_Assunto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_Conteudo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_DataCriacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_DataAgendamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_EviadoEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_RetornoDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB039_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuListaEnviar = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPrincipal.SuspendLayout();
            this.tpLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctFiltrar)).BeginInit();
            this.mnuLista.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuListaFechar
            // 
            this.mnuListaFechar.Name = "mnuListaFechar";
            this.mnuListaFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuListaFechar.Text = "Fechar";
            this.mnuListaFechar.Click += new System.EventHandler(this.mnuListaFechar_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1292, 28);
            this.label13.TabIndex = 6;
            this.label13.Text = "SMS";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabPrincipal, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1298, 509);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.tpLista);
            this.tabPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPrincipal.Location = new System.Drawing.Point(3, 31);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(1292, 475);
            this.tabPrincipal.TabIndex = 0;
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.dtmReferencia);
            this.tpLista.Controls.Add(this.label15);
            this.tpLista.Controls.Add(this.dgwLista);
            this.tpLista.Controls.Add(this.pctFiltrar);
            this.tpLista.Controls.Add(this.mnuLista);
            this.tpLista.Location = new System.Drawing.Point(4, 22);
            this.tpLista.Name = "tpLista";
            this.tpLista.Padding = new System.Windows.Forms.Padding(3);
            this.tpLista.Size = new System.Drawing.Size(1284, 449);
            this.tpLista.TabIndex = 0;
            this.tpLista.Text = "Lista SMS";
            this.tpLista.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 70;
            this.label15.Text = "Data referencia";
            // 
            // dgwLista
            // 
            this.dgwLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TB012_id,
            this.TB039_id,
            this.TB013_NomeCompleto,
            this.TB039_Destino,
            this.TB039_Assunto,
            this.TB039_Conteudo,
            this.TB039_DataCriacao,
            this.TB039_DataAgendamento,
            this.TB039_EviadoEm,
            this.TB039_RetornoDef,
            this.TB039_Status,
            this.TB012_Status});
            this.dgwLista.Location = new System.Drawing.Point(6, 88);
            this.dgwLista.MultiSelect = false;
            this.dgwLista.Name = "dgwLista";
            this.dgwLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwLista.Size = new System.Drawing.Size(1253, 346);
            this.dgwLista.TabIndex = 60;
            // 
            // pctFiltrar
            // 
            this.pctFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctFiltrar.Image = ((System.Drawing.Image)(resources.GetObject("pctFiltrar.Image")));
            this.pctFiltrar.Location = new System.Drawing.Point(9, 35);
            this.pctFiltrar.Name = "pctFiltrar";
            this.pctFiltrar.Size = new System.Drawing.Size(24, 21);
            this.pctFiltrar.TabIndex = 59;
            this.pctFiltrar.TabStop = false;
            this.pctFiltrar.Click += new System.EventHandler(this.pctFiltrar_Click);
            // 
            // mnuLista
            // 
            this.mnuLista.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuListaEnviar,
            this.mnuListaFechar});
            this.mnuLista.Location = new System.Drawing.Point(3, 3);
            this.mnuLista.Name = "mnuLista";
            this.mnuLista.Size = new System.Drawing.Size(1278, 24);
            this.mnuLista.TabIndex = 63;
            this.mnuLista.Text = "menuStrip1";
            // 
            // dtmReferencia
            // 
            this.dtmReferencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmReferencia.Location = new System.Drawing.Point(125, 35);
            this.dtmReferencia.Name = "dtmReferencia";
            this.dtmReferencia.Size = new System.Drawing.Size(102, 20);
            this.dtmReferencia.TabIndex = 72;
            this.dtmReferencia.Value = new System.DateTime(2017, 12, 20, 0, 0, 0, 0);
            // 
            // TB012_id
            // 
            this.TB012_id.DataPropertyName = "TB012_id";
            this.TB012_id.HeaderText = "Contrato";
            this.TB012_id.Name = "TB012_id";
            this.TB012_id.ReadOnly = true;
            this.TB012_id.Width = 70;
            // 
            // TB039_id
            // 
            this.TB039_id.DataPropertyName = "TB039_id";
            this.TB039_id.HeaderText = "Id";
            this.TB039_id.Name = "TB039_id";
            this.TB039_id.ReadOnly = true;
            this.TB039_id.Width = 70;
            // 
            // TB013_NomeCompleto
            // 
            this.TB013_NomeCompleto.DataPropertyName = "TB013_NomeCompleto";
            this.TB013_NomeCompleto.HeaderText = "Destinatario";
            this.TB013_NomeCompleto.Name = "TB013_NomeCompleto";
            this.TB013_NomeCompleto.ReadOnly = true;
            // 
            // TB039_Destino
            // 
            this.TB039_Destino.DataPropertyName = "TB039_Destino";
            this.TB039_Destino.HeaderText = "Celular";
            this.TB039_Destino.Name = "TB039_Destino";
            this.TB039_Destino.ReadOnly = true;
            // 
            // TB039_Assunto
            // 
            this.TB039_Assunto.DataPropertyName = "TB039_Assunto";
            this.TB039_Assunto.HeaderText = "Assunto";
            this.TB039_Assunto.Name = "TB039_Assunto";
            this.TB039_Assunto.ReadOnly = true;
            // 
            // TB039_Conteudo
            // 
            this.TB039_Conteudo.DataPropertyName = "TB039_Conteudo";
            this.TB039_Conteudo.HeaderText = "Conteudo";
            this.TB039_Conteudo.Name = "TB039_Conteudo";
            this.TB039_Conteudo.ReadOnly = true;
            // 
            // TB039_DataCriacao
            // 
            this.TB039_DataCriacao.DataPropertyName = "TB039_DataCriacao";
            dataGridViewCellStyle4.Format = "g";
            dataGridViewCellStyle4.NullValue = null;
            this.TB039_DataCriacao.DefaultCellStyle = dataGridViewCellStyle4;
            this.TB039_DataCriacao.HeaderText = "Criado em";
            this.TB039_DataCriacao.Name = "TB039_DataCriacao";
            this.TB039_DataCriacao.ReadOnly = true;
            // 
            // TB039_DataAgendamento
            // 
            this.TB039_DataAgendamento.DataPropertyName = "TB039_DataAgendamento";
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.TB039_DataAgendamento.DefaultCellStyle = dataGridViewCellStyle5;
            this.TB039_DataAgendamento.HeaderText = "Agendamento";
            this.TB039_DataAgendamento.Name = "TB039_DataAgendamento";
            this.TB039_DataAgendamento.ReadOnly = true;
            // 
            // TB039_EviadoEm
            // 
            this.TB039_EviadoEm.DataPropertyName = "TB039_EviadoEm";
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.TB039_EviadoEm.DefaultCellStyle = dataGridViewCellStyle6;
            this.TB039_EviadoEm.HeaderText = "Enviado em";
            this.TB039_EviadoEm.Name = "TB039_EviadoEm";
            this.TB039_EviadoEm.ReadOnly = true;
            // 
            // TB039_RetornoDef
            // 
            this.TB039_RetornoDef.DataPropertyName = "TB039_RetornoDef";
            this.TB039_RetornoDef.HeaderText = "Retorno";
            this.TB039_RetornoDef.Name = "TB039_RetornoDef";
            this.TB039_RetornoDef.ReadOnly = true;
            // 
            // TB039_Status
            // 
            this.TB039_Status.DataPropertyName = "TB039_StatusS";
            this.TB039_Status.HeaderText = "Status SMS";
            this.TB039_Status.Name = "TB039_Status";
            this.TB039_Status.ReadOnly = true;
            // 
            // TB012_Status
            // 
            this.TB012_Status.DataPropertyName = "TB012_StatusS";
            this.TB012_Status.HeaderText = "StatusContrato";
            this.TB012_Status.Name = "TB012_Status";
            this.TB012_Status.ReadOnly = true;
            // 
            // mnuListaEnviar
            // 
            this.mnuListaEnviar.Name = "mnuListaEnviar";
            this.mnuListaEnviar.Size = new System.Drawing.Size(51, 20);
            this.mnuListaEnviar.Text = "Enviar";
            this.mnuListaEnviar.Click += new System.EventHandler(this.mnuListaEnviar_Click);
            // 
            // frmsms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmsms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmsms";
            this.Load += new System.EventHandler(this.frmsms_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPrincipal.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.tpLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctFiltrar)).EndInit();
            this.mnuLista.ResumeLayout(false);
            this.mnuLista.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem mnuListaFechar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgwLista;
        private System.Windows.Forms.PictureBox pctFiltrar;
        private System.Windows.Forms.MenuStrip mnuLista;
        private System.Windows.Forms.DateTimePicker dtmReferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB013_NomeCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_Destino;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_Assunto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_Conteudo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_DataCriacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_DataAgendamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_EviadoEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_RetornoDef;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB039_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_Status;
        private System.Windows.Forms.ToolStripMenuItem mnuListaEnviar;
    }
}