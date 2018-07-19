namespace ContezaAdmin.Atendimento
{
    partial class frmCartoes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tbLista = new System.Windows.Forms.TabPage();
            this.pcbSelecionar = new System.Windows.Forms.PictureBox();
            this.ddgCartoes = new System.Windows.Forms.DataGridView();
            this.LTB012_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LTB013_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LTB013_NomeCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LTB013_Cartao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LTB013_CarteirinhaStatusS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LTB009_Contato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSelecionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtFiltroAssociado = new System.Windows.Forms.TextBox();
            this.cmbFiltroAssociado = new System.Windows.Forms.ComboBox();
            this.mnuLista = new System.Windows.Forms.MenuStrip();
            this.mnuListaConfirmarRecebimento = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListaFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.tbCartoesContrato = new System.Windows.Forms.TabPage();
            this.lblPessoaNomeId = new System.Windows.Forms.Label();
            this.lblPessoaCartaoEntregueEm = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtPessoaCartaoEntreguePara = new System.Windows.Forms.TextBox();
            this.lblPessoaCartaoStatus = new System.Windows.Forms.Label();
            this.lblPessoaCartao = new System.Windows.Forms.Label();
            this.lblStatusContrato = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContrato = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPessoaStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPessoaNome = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddgCartoesContrato = new System.Windows.Forms.DataGridView();
            this.cTB013_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTB013_Cartao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTB013_NomeCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTB013_CartaoEntregueEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTB013_CartaoEntreguePara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTB013_CarteirinhaStatusS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuCartoes = new System.Windows.Forms.MenuStrip();
            this.mnuCartoesFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListaEnviarParaImpressao = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPrincipal.SuspendLayout();
            this.tbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSelecionar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddgCartoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mnuLista.SuspendLayout();
            this.tbCartoesContrato.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddgCartoesContrato)).BeginInit();
            this.mnuCartoes.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(903, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(897, 29);
            this.label13.TabIndex = 4;
            this.label13.Text = "Carteiras de Contezinos";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.tbLista);
            this.tabPrincipal.Controls.Add(this.tbCartoesContrato);
            this.tabPrincipal.Location = new System.Drawing.Point(3, 35);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(888, 453);
            this.tabPrincipal.TabIndex = 5;
            // 
            // tbLista
            // 
            this.tbLista.Controls.Add(this.pcbSelecionar);
            this.tbLista.Controls.Add(this.ddgCartoes);
            this.tbLista.Controls.Add(this.pictureBox1);
            this.tbLista.Controls.Add(this.txtFiltroAssociado);
            this.tbLista.Controls.Add(this.cmbFiltroAssociado);
            this.tbLista.Controls.Add(this.mnuLista);
            this.tbLista.Location = new System.Drawing.Point(4, 22);
            this.tbLista.Name = "tbLista";
            this.tbLista.Padding = new System.Windows.Forms.Padding(3);
            this.tbLista.Size = new System.Drawing.Size(880, 427);
            this.tbLista.TabIndex = 0;
            this.tbLista.Text = "Cartões";
            this.tbLista.UseVisualStyleBackColor = true;
            // 
            // pcbSelecionar
            // 
            this.pcbSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbSelecionar.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pcbSelecionar.Location = new System.Drawing.Point(781, 41);
            this.pcbSelecionar.Name = "pcbSelecionar";
            this.pcbSelecionar.Size = new System.Drawing.Size(24, 21);
            this.pcbSelecionar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbSelecionar.TabIndex = 19;
            this.pcbSelecionar.TabStop = false;
            this.pcbSelecionar.Visible = false;
            this.pcbSelecionar.Click += new System.EventHandler(this.pcbSelecionar_Click);
            // 
            // ddgCartoes
            // 
            this.ddgCartoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddgCartoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LTB012_Id,
            this.LTB013_id,
            this.LTB013_NomeCompleto,
            this.LTB013_Cartao,
            this.LTB013_CarteirinhaStatusS,
            this.LTB009_Contato,
            this.LSelecionar});
            this.ddgCartoes.Location = new System.Drawing.Point(6, 68);
            this.ddgCartoes.Name = "ddgCartoes";
            this.ddgCartoes.Size = new System.Drawing.Size(868, 353);
            this.ddgCartoes.TabIndex = 18;
            this.ddgCartoes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddgCartoes_CellClick);
            // 
            // LTB012_Id
            // 
            this.LTB012_Id.DataPropertyName = "TB012_Id";
            this.LTB012_Id.HeaderText = "Contrato";
            this.LTB012_Id.Name = "LTB012_Id";
            this.LTB012_Id.ReadOnly = true;
            this.LTB012_Id.Width = 50;
            // 
            // LTB013_id
            // 
            this.LTB013_id.DataPropertyName = "TB013_id";
            this.LTB013_id.HeaderText = "Id";
            this.LTB013_id.Name = "LTB013_id";
            this.LTB013_id.ReadOnly = true;
            this.LTB013_id.Width = 50;
            // 
            // LTB013_NomeCompleto
            // 
            this.LTB013_NomeCompleto.DataPropertyName = "TB013_NomeCompleto";
            this.LTB013_NomeCompleto.HeaderText = "Nome";
            this.LTB013_NomeCompleto.Name = "LTB013_NomeCompleto";
            this.LTB013_NomeCompleto.ReadOnly = true;
            this.LTB013_NomeCompleto.Width = 300;
            // 
            // LTB013_Cartao
            // 
            this.LTB013_Cartao.DataPropertyName = "TB013_Cartao";
            this.LTB013_Cartao.HeaderText = "Cartão";
            this.LTB013_Cartao.Name = "LTB013_Cartao";
            this.LTB013_Cartao.ReadOnly = true;
            // 
            // LTB013_CarteirinhaStatusS
            // 
            this.LTB013_CarteirinhaStatusS.DataPropertyName = "TB013_CarteirinhaStatusS";
            this.LTB013_CarteirinhaStatusS.HeaderText = "Status";
            this.LTB013_CarteirinhaStatusS.Name = "LTB013_CarteirinhaStatusS";
            this.LTB013_CarteirinhaStatusS.ReadOnly = true;
            // 
            // LTB009_Contato
            // 
            this.LTB009_Contato.DataPropertyName = "Celular";
            this.LTB009_Contato.HeaderText = "Celular";
            this.LTB009_Contato.Name = "LTB009_Contato";
            this.LTB009_Contato.ReadOnly = true;
            // 
            // LSelecionar
            // 
            this.LSelecionar.HeaderText = "Selecionar";
            this.LSelecionar.Name = "LSelecionar";
            this.LSelecionar.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ContezaAdmin.Properties.Resources.Lupa;
            this.pictureBox1.Location = new System.Drawing.Point(431, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 21);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtFiltroAssociado
            // 
            this.txtFiltroAssociado.Location = new System.Drawing.Point(203, 30);
            this.txtFiltroAssociado.MaxLength = 60;
            this.txtFiltroAssociado.Name = "txtFiltroAssociado";
            this.txtFiltroAssociado.Size = new System.Drawing.Size(222, 20);
            this.txtFiltroAssociado.TabIndex = 16;
            // 
            // cmbFiltroAssociado
            // 
            this.cmbFiltroAssociado.FormattingEnabled = true;
            this.cmbFiltroAssociado.Items.AddRange(new object[] {
            "Contrato",
            "Nome",
            "Status Gerada",
            "Status Gerada Manualmente",
            "Enviada para Impressão",
            "Disponivel para Entrega",
            "Entregue ",
            "Gerados via Baxa Manual"});
            this.cmbFiltroAssociado.Location = new System.Drawing.Point(6, 30);
            this.cmbFiltroAssociado.Name = "cmbFiltroAssociado";
            this.cmbFiltroAssociado.Size = new System.Drawing.Size(191, 21);
            this.cmbFiltroAssociado.TabIndex = 15;
            this.cmbFiltroAssociado.Text = "Disponivel para Entrega";
            // 
            // mnuLista
            // 
            this.mnuLista.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuListaConfirmarRecebimento,
            this.mnuListaEnviarParaImpressao,
            this.mnuListaFechar});
            this.mnuLista.Location = new System.Drawing.Point(3, 3);
            this.mnuLista.Name = "mnuLista";
            this.mnuLista.Size = new System.Drawing.Size(874, 24);
            this.mnuLista.TabIndex = 0;
            this.mnuLista.Text = "menuStrip1";
            // 
            // mnuListaConfirmarRecebimento
            // 
            this.mnuListaConfirmarRecebimento.Name = "mnuListaConfirmarRecebimento";
            this.mnuListaConfirmarRecebimento.Size = new System.Drawing.Size(146, 20);
            this.mnuListaConfirmarRecebimento.Text = "Confirmar Recebimento";
            this.mnuListaConfirmarRecebimento.Click += new System.EventHandler(this.mnuListaConfirmarRecebimento_Click);
            // 
            // mnuListaFechar
            // 
            this.mnuListaFechar.Name = "mnuListaFechar";
            this.mnuListaFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuListaFechar.Text = "Fechar";
            this.mnuListaFechar.Click += new System.EventHandler(this.mnuListaFechar_Click);
            // 
            // tbCartoesContrato
            // 
            this.tbCartoesContrato.Controls.Add(this.lblPessoaNomeId);
            this.tbCartoesContrato.Controls.Add(this.lblPessoaCartaoEntregueEm);
            this.tbCartoesContrato.Controls.Add(this.label6);
            this.tbCartoesContrato.Controls.Add(this.pictureBox2);
            this.tbCartoesContrato.Controls.Add(this.txtPessoaCartaoEntreguePara);
            this.tbCartoesContrato.Controls.Add(this.lblPessoaCartaoStatus);
            this.tbCartoesContrato.Controls.Add(this.lblPessoaCartao);
            this.tbCartoesContrato.Controls.Add(this.lblStatusContrato);
            this.tbCartoesContrato.Controls.Add(this.label10);
            this.tbCartoesContrato.Controls.Add(this.label9);
            this.tbCartoesContrato.Controls.Add(this.label8);
            this.tbCartoesContrato.Controls.Add(this.label7);
            this.tbCartoesContrato.Controls.Add(this.lblContrato);
            this.tbCartoesContrato.Controls.Add(this.label5);
            this.tbCartoesContrato.Controls.Add(this.lblPessoaStatus);
            this.tbCartoesContrato.Controls.Add(this.label3);
            this.tbCartoesContrato.Controls.Add(this.lblPessoaNome);
            this.tbCartoesContrato.Controls.Add(this.label1);
            this.tbCartoesContrato.Controls.Add(this.ddgCartoesContrato);
            this.tbCartoesContrato.Controls.Add(this.mnuCartoes);
            this.tbCartoesContrato.Location = new System.Drawing.Point(4, 22);
            this.tbCartoesContrato.Name = "tbCartoesContrato";
            this.tbCartoesContrato.Padding = new System.Windows.Forms.Padding(3);
            this.tbCartoesContrato.Size = new System.Drawing.Size(880, 427);
            this.tbCartoesContrato.TabIndex = 1;
            this.tbCartoesContrato.Text = "Cartões do Contrato";
            this.tbCartoesContrato.UseVisualStyleBackColor = true;
            // 
            // lblPessoaNomeId
            // 
            this.lblPessoaNomeId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPessoaNomeId.Location = new System.Drawing.Point(47, 222);
            this.lblPessoaNomeId.Name = "lblPessoaNomeId";
            this.lblPessoaNomeId.Size = new System.Drawing.Size(30, 22);
            this.lblPessoaNomeId.TabIndex = 22;
            this.lblPessoaNomeId.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblPessoaNomeId.Visible = false;
            // 
            // lblPessoaCartaoEntregueEm
            // 
            this.lblPessoaCartaoEntregueEm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPessoaCartaoEntregueEm.Location = new System.Drawing.Point(440, 272);
            this.lblPessoaCartaoEntregueEm.Name = "lblPessoaCartaoEntregueEm";
            this.lblPessoaCartaoEntregueEm.Size = new System.Drawing.Size(165, 22);
            this.lblPessoaCartaoEntregueEm.TabIndex = 21;
            this.lblPessoaCartaoEntregueEm.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Status Cartão";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pictureBox2.Location = new System.Drawing.Point(611, 272);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 21);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // txtPessoaCartaoEntreguePara
            // 
            this.txtPessoaCartaoEntreguePara.Location = new System.Drawing.Point(83, 269);
            this.txtPessoaCartaoEntreguePara.MaxLength = 100;
            this.txtPessoaCartaoEntreguePara.Name = "txtPessoaCartaoEntreguePara";
            this.txtPessoaCartaoEntreguePara.Size = new System.Drawing.Size(245, 20);
            this.txtPessoaCartaoEntreguePara.TabIndex = 17;
            // 
            // lblPessoaCartaoStatus
            // 
            this.lblPessoaCartaoStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPessoaCartaoStatus.Location = new System.Drawing.Point(440, 246);
            this.lblPessoaCartaoStatus.Name = "lblPessoaCartaoStatus";
            this.lblPessoaCartaoStatus.Size = new System.Drawing.Size(165, 22);
            this.lblPessoaCartaoStatus.TabIndex = 15;
            this.lblPessoaCartaoStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblPessoaCartao
            // 
            this.lblPessoaCartao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPessoaCartao.Location = new System.Drawing.Point(83, 244);
            this.lblPessoaCartao.Name = "lblPessoaCartao";
            this.lblPessoaCartao.Size = new System.Drawing.Size(245, 22);
            this.lblPessoaCartao.TabIndex = 14;
            this.lblPessoaCartao.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblStatusContrato
            // 
            this.lblStatusContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusContrato.Location = new System.Drawing.Point(440, 27);
            this.lblStatusContrato.Name = "lblStatusContrato";
            this.lblStatusContrato.Size = new System.Drawing.Size(165, 22);
            this.lblStatusContrato.TabIndex = 13;
            this.lblStatusContrato.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Entregue para";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Status Cartão";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 231);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Status Pessoa";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(334, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Status Contrato";
            // 
            // lblContrato
            // 
            this.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContrato.Location = new System.Drawing.Point(73, 27);
            this.lblContrato.Name = "lblContrato";
            this.lblContrato.Size = new System.Drawing.Size(165, 22);
            this.lblContrato.TabIndex = 8;
            this.lblContrato.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Contrato";
            // 
            // lblPessoaStatus
            // 
            this.lblPessoaStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPessoaStatus.Location = new System.Drawing.Point(440, 224);
            this.lblPessoaStatus.Name = "lblPessoaStatus";
            this.lblPessoaStatus.Size = new System.Drawing.Size(165, 22);
            this.lblPessoaStatus.TabIndex = 6;
            this.lblPessoaStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cartão";
            // 
            // lblPessoaNome
            // 
            this.lblPessoaNome.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPessoaNome.Location = new System.Drawing.Point(83, 222);
            this.lblPessoaNome.Name = "lblPessoaNome";
            this.lblPessoaNome.Size = new System.Drawing.Size(245, 22);
            this.lblPessoaNome.TabIndex = 4;
            this.lblPessoaNome.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nome";
            // 
            // ddgCartoesContrato
            // 
            this.ddgCartoesContrato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddgCartoesContrato.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTB013_id,
            this.cTB013_Cartao,
            this.cTB013_NomeCompleto,
            this.cTB013_CartaoEntregueEm,
            this.cTB013_CartaoEntreguePara,
            this.cTB013_CarteirinhaStatusS});
            this.ddgCartoesContrato.Location = new System.Drawing.Point(6, 52);
            this.ddgCartoesContrato.Name = "ddgCartoesContrato";
            this.ddgCartoesContrato.Size = new System.Drawing.Size(868, 167);
            this.ddgCartoesContrato.TabIndex = 2;
            this.ddgCartoesContrato.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddgCartoesContrato_CellClick);
            // 
            // cTB013_id
            // 
            this.cTB013_id.DataPropertyName = "TB013_id";
            this.cTB013_id.HeaderText = "Id";
            this.cTB013_id.Name = "cTB013_id";
            this.cTB013_id.ReadOnly = true;
            this.cTB013_id.Width = 50;
            // 
            // cTB013_Cartao
            // 
            this.cTB013_Cartao.DataPropertyName = "TB013_Cartao";
            this.cTB013_Cartao.HeaderText = "Cartão";
            this.cTB013_Cartao.Name = "cTB013_Cartao";
            this.cTB013_Cartao.ReadOnly = true;
            // 
            // cTB013_NomeCompleto
            // 
            this.cTB013_NomeCompleto.DataPropertyName = "TB013_NomeCompleto";
            this.cTB013_NomeCompleto.HeaderText = "Nome Completo";
            this.cTB013_NomeCompleto.Name = "cTB013_NomeCompleto";
            this.cTB013_NomeCompleto.ReadOnly = true;
            this.cTB013_NomeCompleto.Width = 300;
            // 
            // cTB013_CartaoEntregueEm
            // 
            this.cTB013_CartaoEntregueEm.DataPropertyName = "TB013_CartaoEntregueEm";
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            this.cTB013_CartaoEntregueEm.DefaultCellStyle = dataGridViewCellStyle2;
            this.cTB013_CartaoEntregueEm.HeaderText = "Entregue Em";
            this.cTB013_CartaoEntregueEm.Name = "cTB013_CartaoEntregueEm";
            this.cTB013_CartaoEntregueEm.ReadOnly = true;
            // 
            // cTB013_CartaoEntreguePara
            // 
            this.cTB013_CartaoEntreguePara.DataPropertyName = "TB013_CartaoEntreguePara";
            this.cTB013_CartaoEntreguePara.HeaderText = "Entregue Para";
            this.cTB013_CartaoEntreguePara.Name = "cTB013_CartaoEntreguePara";
            this.cTB013_CartaoEntreguePara.ReadOnly = true;
            // 
            // cTB013_CarteirinhaStatusS
            // 
            this.cTB013_CarteirinhaStatusS.DataPropertyName = "TB013_CarteirinhaStatusS";
            this.cTB013_CarteirinhaStatusS.HeaderText = "Status Cartão";
            this.cTB013_CarteirinhaStatusS.Name = "cTB013_CarteirinhaStatusS";
            this.cTB013_CarteirinhaStatusS.ReadOnly = true;
            // 
            // mnuCartoes
            // 
            this.mnuCartoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCartoesFechar});
            this.mnuCartoes.Location = new System.Drawing.Point(3, 3);
            this.mnuCartoes.Name = "mnuCartoes";
            this.mnuCartoes.Size = new System.Drawing.Size(874, 24);
            this.mnuCartoes.TabIndex = 1;
            this.mnuCartoes.Text = "menuStrip1";
            // 
            // mnuCartoesFechar
            // 
            this.mnuCartoesFechar.Name = "mnuCartoesFechar";
            this.mnuCartoesFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuCartoesFechar.Text = "Fechar";
            this.mnuCartoesFechar.Click += new System.EventHandler(this.mnuCartoesFechar_Click);
            // 
            // mnuListaEnviarParaImpressao
            // 
            this.mnuListaEnviarParaImpressao.Name = "mnuListaEnviarParaImpressao";
            this.mnuListaEnviarParaImpressao.Size = new System.Drawing.Size(134, 20);
            this.mnuListaEnviarParaImpressao.Text = "Enviar para Impressão";
            this.mnuListaEnviarParaImpressao.Click += new System.EventHandler(this.mnuListaEnviarParaImpressao_Click);
            // 
            // frmCartoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCartoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCartoes";
            this.Load += new System.EventHandler(this.frmCartoes_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPrincipal.ResumeLayout(false);
            this.tbLista.ResumeLayout(false);
            this.tbLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSelecionar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddgCartoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mnuLista.ResumeLayout(false);
            this.mnuLista.PerformLayout();
            this.tbCartoesContrato.ResumeLayout(false);
            this.tbCartoesContrato.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddgCartoesContrato)).EndInit();
            this.mnuCartoes.ResumeLayout(false);
            this.mnuCartoes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tbLista;
        private System.Windows.Forms.TabPage tbCartoesContrato;
        private System.Windows.Forms.MenuStrip mnuLista;
        private System.Windows.Forms.MenuStrip mnuCartoes;
        private System.Windows.Forms.ToolStripMenuItem mnuListaFechar;
        private System.Windows.Forms.ComboBox cmbFiltroAssociado;
        private System.Windows.Forms.TextBox txtFiltroAssociado;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView ddgCartoes;
        private System.Windows.Forms.ToolStripMenuItem mnuCartoesFechar;
        private System.Windows.Forms.TextBox txtPessoaCartaoEntreguePara;
        private System.Windows.Forms.Label lblPessoaCartaoStatus;
        private System.Windows.Forms.Label lblPessoaCartao;
        private System.Windows.Forms.Label lblStatusContrato;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblContrato;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPessoaStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPessoaNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ddgCartoesContrato;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pcbSelecionar;
        private System.Windows.Forms.Label lblPessoaCartaoEntregueEm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTB013_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTB013_Cartao;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTB013_NomeCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTB013_CartaoEntregueEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTB013_CartaoEntreguePara;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTB013_CarteirinhaStatusS;
        private System.Windows.Forms.ToolStripMenuItem mnuListaConfirmarRecebimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTB012_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTB013_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTB013_NomeCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTB013_Cartao;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTB013_CarteirinhaStatusS;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTB009_Contato;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LSelecionar;
        private System.Windows.Forms.Label lblPessoaNomeId;
        private System.Windows.Forms.ToolStripMenuItem mnuListaEnviarParaImpressao;
    }
}