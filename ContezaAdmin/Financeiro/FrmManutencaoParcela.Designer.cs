namespace ContezaAdmin.Financeiro
{
    partial class FrmManutencaoParcela
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManutencaoParcela));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pagamentoComprovanteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clubeConteza_Relatorios = new ContezaAdmin.ClubeConteza_Relatorios();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.pcbFechar = new System.Windows.Forms.PictureBox();
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tbLista = new System.Windows.Forms.TabPage();
            this.cmbTipoContrato = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.dtmCadContrato = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.ddgContratos = new System.Windows.Forms.DataGridView();
            this.TB012_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_CicloContrato_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB013_CPFCNPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB013_NomeCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_AlteradoEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB011_NomeExibicao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_StatusS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtFiltroAssociado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbContrato = new System.Windows.Forms.TabPage();
            this.lblCredUnidade = new System.Windows.Forms.Label();
            this.chkSelecionarTudo = new System.Windows.Forms.CheckBox();
            this.ptbParcelaAlterarVencimento = new System.Windows.Forms.PictureBox();
            this.lblCicloParcela = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbDiaVencimento = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.pctContratoAlterarDataValidade = new System.Windows.Forms.PictureBox();
            this.pctParcelaAlterarFormaPagamento = new System.Windows.Forms.PictureBox();
            this.cmbParcelaStatus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.ddtParcelaDataPagamento = new System.Windows.Forms.DateTimePicker();
            this.lblParcelaCreditoTaxas = new System.Windows.Forms.Label();
            this.lblParcelaCartaoValorCredito = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblParcelaCartaoValorParcela = new System.Windows.Forms.MaskedTextBox();
            this.lblParcelaCartaoDataRecebimentoBanco = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtParcelaCartaoNomeTitular = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.mskParcelaCartaoCPFTitular = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtParcelaCartaoNumero = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbFormasPagamento = new System.Windows.Forms.TabControl();
            this.tbBoleto = new System.Windows.Forms.TabPage();
            this.chkParcelaCancelamento = new System.Windows.Forms.CheckBox();
            this.pctPagamentoBoleto = new System.Windows.Forms.PictureBox();
            this.mskParcelaBoletoMulta = new System.Windows.Forms.MaskedTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.mskParcelaBoletoTarifa = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.mskParcelaBoletoIOF = new System.Windows.Forms.MaskedTextBox();
            this.txtParcelaBoletoDocBanco = new System.Windows.Forms.TextBox();
            this.tbCredito = new System.Windows.Forms.TabPage();
            this.label89 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.lblCredValorCredito = new System.Windows.Forms.Label();
            this.lblCredValorParcela = new System.Windows.Forms.Label();
            this.pctParcelaPagamentoCredito = new System.Windows.Forms.PictureBox();
            this.lblParcelaCreditoTipoVencimento = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblCredValorMinimoParcela = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.cmbCredParcelas = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.cmbCredBandeira = new System.Windows.Forms.ComboBox();
            this.tbDebito = new System.Windows.Forms.TabPage();
            this.pctParcelaPagamentoDebito = new System.Windows.Forms.PictureBox();
            this.tbEspecie = new System.Windows.Forms.TabPage();
            this.pctParcelaPagamentoEspecie = new System.Windows.Forms.PictureBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtParcelaCartaoAutorizacao = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtParcelaCartaoCodValidador = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.pctParcelaStatusAlterar = new System.Windows.Forms.PictureBox();
            this.lblParcelaProdutoId = new System.Windows.Forms.Label();
            this.lblParcelaProduto = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txtParcelaSubTotal = new System.Windows.Forms.TextBox();
            this.txtParcelaDesconto = new System.Windows.Forms.TextBox();
            this.txtParcelaValorUnitario = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.lblParcelaPlanoId = new System.Windows.Forms.Label();
            this.pctAlterarDadosParcela = new System.Windows.Forms.PictureBox();
            this.ddtParcelaVencimento = new System.Windows.Forms.DateTimePicker();
            this.cmbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblParcelaPlano = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.lblParcelaValorTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblParcelaId = new System.Windows.Forms.Label();
            this.ddtParcelaItens = new System.Windows.Forms.DataGridView();
            this.ListID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB017_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB017_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB017_ValorUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB017_ValorDesconto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB017_ValorFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ddtParcelas = new System.Windows.Forms.DataGridView();
            this.DTContratoDependentes = new System.Windows.Forms.DataGridView();
            this.DependenteTB013_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DependenteTB013_NomeExibicao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DependenteTB013_Idade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DependenteTB013_Cartao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DependenteTB013_StatusS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbContratoStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtContratoTitularNomeCompleto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mskContratoTitularCPF = new System.Windows.Forms.MaskedTextBox();
            this.cmbParcelaCiclo = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.dtContratoFim = new System.Windows.Forms.DateTimePicker();
            this.dtContratoInicio = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.lblContrato = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mnuParcela = new System.Windows.Forms.MenuStrip();
            this.mnuParcelaAnotacoes = new System.Windows.Forms.ToolStripMenuItem();
            this.documentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaIncluirProduto = new System.Windows.Forms.ToolStripMenuItem();
            this.unirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaCancelarParcela = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaEmitirParcela = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaEmitirBoleto = new System.Windows.Forms.ToolStripMenuItem();
            this.alterarCicloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contratoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parcelaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaAlterarCiclo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaReencaminharSICOOB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParcelaFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.tbpDocumentos = new System.Windows.Forms.TabPage();
            this.gprAnexarTermo = new System.Windows.Forms.GroupBox();
            this.axAcroPDF2 = new AxAcroPDFLib.AxAcroPDF();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.ddgDocumentos = new System.Windows.Forms.DataGridView();
            this.TB029_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB029_Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_VSContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuDocumentos = new System.Windows.Forms.MenuStrip();
            this.mnuDocumentosFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.grpParcelaEnd = new System.Windows.Forms.GroupBox();
            this.lblParcelaPontoDeVenda = new System.Windows.Forms.Label();
            this.cmbTitularPais = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtContratoTitularNumero = new System.Windows.Forms.TextBox();
            this.txtContratoTitularLogradouro = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.mskContratoTitularCEP = new System.Windows.Forms.MaskedTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbContratoTitularEstado = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.lblContratoTitularID = new System.Windows.Forms.Label();
            this.cmbContratoTitularMunicipio = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.grpComprovante = new System.Windows.Forms.GroupBox();
            this.rptComprovanteCredito = new Microsoft.Reporting.WinForms.ReportViewer();
            this.mnuComprovante = new System.Windows.Forms.MenuStrip();
            this.mnuComprovanteFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.grpCicloContrato = new System.Windows.Forms.GroupBox();
            this.txtcontratoAlterarCiclo = new System.Windows.Forms.TextBox();
            this.btnContratoAlterarCicloFechar = new System.Windows.Forms.Button();
            this.btnContratoAlterarCicloConfirmar = new System.Windows.Forms.Button();
            this.gpbParcelaAvulsa = new System.Windows.Forms.GroupBox();
            this.dtParcelaAvulsaNParcelas = new System.Windows.Forms.ComboBox();
            this.label86 = new System.Windows.Forms.Label();
            this.btnParcelaFechar = new System.Windows.Forms.Button();
            this.btnParcelaConfirmar = new System.Windows.Forms.Button();
            this.dtParcelaAvulsaVencimento = new System.Windows.Forms.DateTimePicker();
            this.label57 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grupCicloParcela = new System.Windows.Forms.GroupBox();
            this.txtParcelaAlterarCiclo = new System.Windows.Forms.TextBox();
            this.btnParcelaAlterarCicloFechar = new System.Windows.Forms.Button();
            this.btnParcelaAlterarCicloConfirmar = new System.Windows.Forms.Button();
            this.pagamentoComprovanteTableAdapter = new ContezaAdmin.ClubeConteza_RelatoriosTableAdapters.PagamentoComprovanteTableAdapter();
            this.clubeConteza_Relatorios1 = new ContezaAdmin.ClubeConteza_Relatorios();
            this.TB016_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_NossoNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB012_CicloContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_Emissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_Vencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_DataPagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_ValorPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelaTB012_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelaTB015_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelaPlano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormaPagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TB016_StatusS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selecionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pagamentoComprovanteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).BeginInit();
            this.tabPrincipal.SuspendLayout();
            this.tbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddgContratos)).BeginInit();
            this.tbContrato.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbParcelaAlterarVencimento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctContratoAlterarDataValidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaAlterarFormaPagamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tbFormasPagamento.SuspendLayout();
            this.tbBoleto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctPagamentoBoleto)).BeginInit();
            this.tbCredito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaPagamentoCredito)).BeginInit();
            this.tbDebito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaPagamentoDebito)).BeginInit();
            this.tbEspecie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaPagamentoEspecie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaStatusAlterar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctAlterarDadosParcela)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddtParcelaItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddtParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTContratoDependentes)).BeginInit();
            this.mnuParcela.SuspendLayout();
            this.tbpDocumentos.SuspendLayout();
            this.gprAnexarTermo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddgDocumentos)).BeginInit();
            this.mnuDocumentos.SuspendLayout();
            this.grpParcelaEnd.SuspendLayout();
            this.grpComprovante.SuspendLayout();
            this.mnuComprovante.SuspendLayout();
            this.grpCicloContrato.SuspendLayout();
            this.gpbParcelaAvulsa.SuspendLayout();
            this.grupCicloParcela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios1)).BeginInit();
            this.SuspendLayout();
            // 
            // pagamentoComprovanteBindingSource
            // 
            this.pagamentoComprovanteBindingSource.DataMember = "PagamentoComprovante";
            this.pagamentoComprovanteBindingSource.DataSource = this.clubeConteza_Relatorios;
            // 
            // clubeConteza_Relatorios
            // 
            this.clubeConteza_Relatorios.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1232F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabPrincipal, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1232, 544);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1198F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pcbFechar, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1226, 26);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(140)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1192, 26);
            this.label13.TabIndex = 5;
            this.label13.Text = "Manutenção de Parcela";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pcbFechar
            // 
            this.pcbFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbFechar.Image = global::ContezaAdmin.Properties.Resources.Excluir;
            this.pcbFechar.Location = new System.Drawing.Point(1201, 3);
            this.pcbFechar.Name = "pcbFechar";
            this.pcbFechar.Size = new System.Drawing.Size(22, 20);
            this.pcbFechar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbFechar.TabIndex = 165;
            this.pcbFechar.TabStop = false;
            this.pcbFechar.Click += new System.EventHandler(this.pcbFechar_Click);
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.tbLista);
            this.tabPrincipal.Controls.Add(this.tbContrato);
            this.tabPrincipal.Controls.Add(this.tbpDocumentos);
            this.tabPrincipal.Location = new System.Drawing.Point(3, 36);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(1226, 505);
            this.tabPrincipal.TabIndex = 7;
            // 
            // tbLista
            // 
            this.tbLista.Controls.Add(this.cmbTipoContrato);
            this.tbLista.Controls.Add(this.label35);
            this.tbLista.Controls.Add(this.dtmCadContrato);
            this.tbLista.Controls.Add(this.label12);
            this.tbLista.Controls.Add(this.ddgContratos);
            this.tbLista.Controls.Add(this.txtFiltroAssociado);
            this.tbLista.Controls.Add(this.label1);
            this.tbLista.Location = new System.Drawing.Point(4, 22);
            this.tbLista.Name = "tbLista";
            this.tbLista.Padding = new System.Windows.Forms.Padding(3);
            this.tbLista.Size = new System.Drawing.Size(1218, 479);
            this.tbLista.TabIndex = 0;
            this.tbLista.Text = "Contratos";
            this.tbLista.UseVisualStyleBackColor = true;
            // 
            // cmbTipoContrato
            // 
            this.cmbTipoContrato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoContrato.FormattingEnabled = true;
            this.cmbTipoContrato.Items.AddRange(new object[] {
            "Todos",
            "Familiar",
            "Parceiro",
            "Corporativo",
            "Familiar Corporativo",
            "Familiar Parceiro"});
            this.cmbTipoContrato.Location = new System.Drawing.Point(411, 22);
            this.cmbTipoContrato.Name = "cmbTipoContrato";
            this.cmbTipoContrato.Size = new System.Drawing.Size(210, 21);
            this.cmbTipoContrato.TabIndex = 215;
            this.cmbTipoContrato.SelectedIndexChanged += new System.EventHandler(this.cmbTipoContrato_SelectedIndexChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(408, 8);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(70, 13);
            this.label35.TabIndex = 18;
            this.label35.Text = "Tipo contrato";
            // 
            // dtmCadContrato
            // 
            this.dtmCadContrato.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmCadContrato.Location = new System.Drawing.Point(6, 22);
            this.dtmCadContrato.Name = "dtmCadContrato";
            this.dtmCadContrato.Size = new System.Drawing.Size(102, 20);
            this.dtmCadContrato.TabIndex = 16;
            this.dtmCadContrato.Value = new System.DateTime(2017, 8, 15, 0, 0, 0, 0);
            this.dtmCadContrato.ValueChanged += new System.EventHandler(this.dtmCadContrato_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Mostrar Registro de?";
            // 
            // ddgContratos
            // 
            this.ddgContratos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddgContratos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TB012_Id,
            this.TB012_CicloContrato_,
            this.TB013_CPFCNPJ,
            this.TB013_NomeCompleto,
            this.TB012_AlteradoEm,
            this.TB011_NomeExibicao,
            this.TB012_StatusS});
            this.ddgContratos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ddgContratos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ddgContratos.Location = new System.Drawing.Point(5, 48);
            this.ddgContratos.MultiSelect = false;
            this.ddgContratos.Name = "ddgContratos";
            this.ddgContratos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ddgContratos.Size = new System.Drawing.Size(1207, 424);
            this.ddgContratos.TabIndex = 15;
            this.ddgContratos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddgContratos_CellClick);
            // 
            // TB012_Id
            // 
            this.TB012_Id.DataPropertyName = "TB012_Id";
            this.TB012_Id.HeaderText = "Contrato";
            this.TB012_Id.Name = "TB012_Id";
            this.TB012_Id.Width = 60;
            // 
            // TB012_CicloContrato_
            // 
            this.TB012_CicloContrato_.DataPropertyName = "TB012_CicloContrato";
            this.TB012_CicloContrato_.HeaderText = "Ciclo do Contrato";
            this.TB012_CicloContrato_.Name = "TB012_CicloContrato_";
            this.TB012_CicloContrato_.ReadOnly = true;
            this.TB012_CicloContrato_.Width = 150;
            // 
            // TB013_CPFCNPJ
            // 
            this.TB013_CPFCNPJ.DataPropertyName = "TB013_CPFCNPJ";
            this.TB013_CPFCNPJ.HeaderText = "CPF";
            this.TB013_CPFCNPJ.Name = "TB013_CPFCNPJ";
            // 
            // TB013_NomeCompleto
            // 
            this.TB013_NomeCompleto.DataPropertyName = "TB013_NomeCompleto";
            this.TB013_NomeCompleto.HeaderText = "Nome Completo";
            this.TB013_NomeCompleto.Name = "TB013_NomeCompleto";
            this.TB013_NomeCompleto.Width = 300;
            // 
            // TB012_AlteradoEm
            // 
            this.TB012_AlteradoEm.DataPropertyName = "TB012_AlteradoEm";
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.TB012_AlteradoEm.DefaultCellStyle = dataGridViewCellStyle1;
            this.TB012_AlteradoEm.HeaderText = "Alterado Em";
            this.TB012_AlteradoEm.Name = "TB012_AlteradoEm";
            // 
            // TB011_NomeExibicao
            // 
            this.TB011_NomeExibicao.DataPropertyName = "TB011_NomeExibicao";
            this.TB011_NomeExibicao.HeaderText = "Alterado Por";
            this.TB011_NomeExibicao.Name = "TB011_NomeExibicao";
            this.TB011_NomeExibicao.Width = 250;
            // 
            // TB012_StatusS
            // 
            this.TB012_StatusS.DataPropertyName = "TB012_StatusS";
            this.TB012_StatusS.HeaderText = "Status";
            this.TB012_StatusS.Name = "TB012_StatusS";
            this.TB012_StatusS.Width = 80;
            // 
            // txtFiltroAssociado
            // 
            this.txtFiltroAssociado.Location = new System.Drawing.Point(146, 22);
            this.txtFiltroAssociado.Name = "txtFiltroAssociado";
            this.txtFiltroAssociado.Size = new System.Drawing.Size(256, 20);
            this.txtFiltroAssociado.TabIndex = 2;
            this.txtFiltroAssociado.Leave += new System.EventHandler(this.txtFiltroAssociado_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtro";
            // 
            // tbContrato
            // 
            this.tbContrato.Controls.Add(this.lblCredUnidade);
            this.tbContrato.Controls.Add(this.chkSelecionarTudo);
            this.tbContrato.Controls.Add(this.ptbParcelaAlterarVencimento);
            this.tbContrato.Controls.Add(this.lblCicloParcela);
            this.tbContrato.Controls.Add(this.label36);
            this.tbContrato.Controls.Add(this.cmbDiaVencimento);
            this.tbContrato.Controls.Add(this.label33);
            this.tbContrato.Controls.Add(this.pctContratoAlterarDataValidade);
            this.tbContrato.Controls.Add(this.pctParcelaAlterarFormaPagamento);
            this.tbContrato.Controls.Add(this.cmbParcelaStatus);
            this.tbContrato.Controls.Add(this.label10);
            this.tbContrato.Controls.Add(this.pictureBox3);
            this.tbContrato.Controls.Add(this.groupBox1);
            this.tbContrato.Controls.Add(this.pctParcelaStatusAlterar);
            this.tbContrato.Controls.Add(this.lblParcelaProdutoId);
            this.tbContrato.Controls.Add(this.lblParcelaProduto);
            this.tbContrato.Controls.Add(this.label34);
            this.tbContrato.Controls.Add(this.txtParcelaSubTotal);
            this.tbContrato.Controls.Add(this.txtParcelaDesconto);
            this.tbContrato.Controls.Add(this.txtParcelaValorUnitario);
            this.tbContrato.Controls.Add(this.label60);
            this.tbContrato.Controls.Add(this.label59);
            this.tbContrato.Controls.Add(this.label58);
            this.tbContrato.Controls.Add(this.lblParcelaPlanoId);
            this.tbContrato.Controls.Add(this.pctAlterarDadosParcela);
            this.tbContrato.Controls.Add(this.ddtParcelaVencimento);
            this.tbContrato.Controls.Add(this.cmbFormaPagamento);
            this.tbContrato.Controls.Add(this.label52);
            this.tbContrato.Controls.Add(this.label42);
            this.tbContrato.Controls.Add(this.lblParcelaPlano);
            this.tbContrato.Controls.Add(this.label79);
            this.tbContrato.Controls.Add(this.label77);
            this.tbContrato.Controls.Add(this.lblParcelaValorTotal);
            this.tbContrato.Controls.Add(this.label4);
            this.tbContrato.Controls.Add(this.lblParcelaId);
            this.tbContrato.Controls.Add(this.ddtParcelaItens);
            this.tbContrato.Controls.Add(this.ddtParcelas);
            this.tbContrato.Controls.Add(this.DTContratoDependentes);
            this.tbContrato.Controls.Add(this.cmbContratoStatus);
            this.tbContrato.Controls.Add(this.label8);
            this.tbContrato.Controls.Add(this.txtContratoTitularNomeCompleto);
            this.tbContrato.Controls.Add(this.label9);
            this.tbContrato.Controls.Add(this.label7);
            this.tbContrato.Controls.Add(this.mskContratoTitularCPF);
            this.tbContrato.Controls.Add(this.cmbParcelaCiclo);
            this.tbContrato.Controls.Add(this.label22);
            this.tbContrato.Controls.Add(this.dtContratoFim);
            this.tbContrato.Controls.Add(this.dtContratoInicio);
            this.tbContrato.Controls.Add(this.label6);
            this.tbContrato.Controls.Add(this.lblContrato);
            this.tbContrato.Controls.Add(this.label2);
            this.tbContrato.Controls.Add(this.mnuParcela);
            this.tbContrato.Location = new System.Drawing.Point(4, 22);
            this.tbContrato.Name = "tbContrato";
            this.tbContrato.Padding = new System.Windows.Forms.Padding(3);
            this.tbContrato.Size = new System.Drawing.Size(1218, 479);
            this.tbContrato.TabIndex = 1;
            this.tbContrato.Text = "Contrato & Parcelas";
            this.tbContrato.UseVisualStyleBackColor = true;
            // 
            // lblCredUnidade
            // 
            this.lblCredUnidade.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblCredUnidade.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCredUnidade.ForeColor = System.Drawing.Color.White;
            this.lblCredUnidade.Location = new System.Drawing.Point(1164, 27);
            this.lblCredUnidade.Name = "lblCredUnidade";
            this.lblCredUnidade.Size = new System.Drawing.Size(47, 21);
            this.lblCredUnidade.TabIndex = 261;
            this.lblCredUnidade.Visible = false;
            // 
            // chkSelecionarTudo
            // 
            this.chkSelecionarTudo.AutoSize = true;
            this.chkSelecionarTudo.Location = new System.Drawing.Point(613, 54);
            this.chkSelecionarTudo.Name = "chkSelecionarTudo";
            this.chkSelecionarTudo.Size = new System.Drawing.Size(110, 17);
            this.chkSelecionarTudo.TabIndex = 259;
            this.chkSelecionarTudo.Text = "Selecionar Tudo?";
            this.chkSelecionarTudo.UseVisualStyleBackColor = true;
            this.chkSelecionarTudo.CheckedChanged += new System.EventHandler(this.chkSelecionarTudo_CheckedChanged);
            this.chkSelecionarTudo.Click += new System.EventHandler(this.chkSelecionarTudo_Click);
            // 
            // ptbParcelaAlterarVencimento
            // 
            this.ptbParcelaAlterarVencimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbParcelaAlterarVencimento.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.ptbParcelaAlterarVencimento.Location = new System.Drawing.Point(948, 192);
            this.ptbParcelaAlterarVencimento.Name = "ptbParcelaAlterarVencimento";
            this.ptbParcelaAlterarVencimento.Size = new System.Drawing.Size(28, 21);
            this.ptbParcelaAlterarVencimento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptbParcelaAlterarVencimento.TabIndex = 258;
            this.ptbParcelaAlterarVencimento.TabStop = false;
            this.toolTip1.SetToolTip(this.ptbParcelaAlterarVencimento, "Altera status da parcela.");
            this.ptbParcelaAlterarVencimento.Click += new System.EventHandler(this.ptbParcelaAlterarVencimento_Click);
            // 
            // lblCicloParcela
            // 
            this.lblCicloParcela.BackColor = System.Drawing.Color.White;
            this.lblCicloParcela.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCicloParcela.Location = new System.Drawing.Point(787, 193);
            this.lblCicloParcela.Name = "lblCicloParcela";
            this.lblCicloParcela.Size = new System.Drawing.Size(66, 20);
            this.lblCicloParcela.TabIndex = 257;
            this.lblCicloParcela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(784, 177);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(69, 13);
            this.label36.TabIndex = 256;
            this.label36.Text = "Ciclo Parcela";
            // 
            // cmbDiaVencimento
            // 
            this.cmbDiaVencimento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiaVencimento.FormattingEnabled = true;
            this.cmbDiaVencimento.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.cmbDiaVencimento.Location = new System.Drawing.Point(299, 52);
            this.cmbDiaVencimento.Name = "cmbDiaVencimento";
            this.cmbDiaVencimento.Size = new System.Drawing.Size(77, 21);
            this.cmbDiaVencimento.TabIndex = 253;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(296, 33);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(35, 13);
            this.label33.TabIndex = 254;
            this.label33.Text = "Venc.";
            // 
            // pctContratoAlterarDataValidade
            // 
            this.pctContratoAlterarDataValidade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctContratoAlterarDataValidade.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctContratoAlterarDataValidade.Location = new System.Drawing.Point(265, 52);
            this.pctContratoAlterarDataValidade.Name = "pctContratoAlterarDataValidade";
            this.pctContratoAlterarDataValidade.Size = new System.Drawing.Size(28, 21);
            this.pctContratoAlterarDataValidade.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctContratoAlterarDataValidade.TabIndex = 251;
            this.pctContratoAlterarDataValidade.TabStop = false;
            this.toolTip1.SetToolTip(this.pctContratoAlterarDataValidade, "Selecione esta comando para alterar a data de validade do contrato");
            this.pctContratoAlterarDataValidade.Click += new System.EventHandler(this.pctContratoAlterarDataValidade_Click);
            // 
            // pctParcelaAlterarFormaPagamento
            // 
            this.pctParcelaAlterarFormaPagamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctParcelaAlterarFormaPagamento.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctParcelaAlterarFormaPagamento.Location = new System.Drawing.Point(1183, 226);
            this.pctParcelaAlterarFormaPagamento.Name = "pctParcelaAlterarFormaPagamento";
            this.pctParcelaAlterarFormaPagamento.Size = new System.Drawing.Size(28, 21);
            this.pctParcelaAlterarFormaPagamento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctParcelaAlterarFormaPagamento.TabIndex = 250;
            this.pctParcelaAlterarFormaPagamento.TabStop = false;
            this.toolTip1.SetToolTip(this.pctParcelaAlterarFormaPagamento, "Atualiza status da parcela");
            this.pctParcelaAlterarFormaPagamento.Click += new System.EventHandler(this.pctParcelaAlterarFormaPagamento_Click);
            // 
            // cmbParcelaStatus
            // 
            this.cmbParcelaStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParcelaStatus.FormattingEnabled = true;
            this.cmbParcelaStatus.Location = new System.Drawing.Point(729, 226);
            this.cmbParcelaStatus.Name = "cmbParcelaStatus";
            this.cmbParcelaStatus.Size = new System.Drawing.Size(94, 21);
            this.cmbParcelaStatus.TabIndex = 248;
            this.toolTip1.SetToolTip(this.cmbParcelaStatus, "Status da parcela");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(729, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 249;
            this.label10.Text = "Status";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Enabled = false;
            this.pictureBox3.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pictureBox3.Location = new System.Drawing.Point(496, 52);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(28, 21);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 247;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Selecione este comando para alterar o status do contrato");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.ddtParcelaDataPagamento);
            this.groupBox1.Controls.Add(this.lblParcelaCreditoTaxas);
            this.groupBox1.Controls.Add(this.lblParcelaCartaoValorCredito);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblParcelaCartaoValorParcela);
            this.groupBox1.Controls.Add(this.lblParcelaCartaoDataRecebimentoBanco);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtParcelaCartaoNomeTitular);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.mskParcelaCartaoCPFTitular);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtParcelaCartaoNumero);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.tbFormasPagamento);
            this.groupBox1.Controls.Add(this.txtParcelaCartaoAutorizacao);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txtParcelaCartaoCodValidador);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Location = new System.Drawing.Point(729, 290);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 189);
            this.groupBox1.TabIndex = 246;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pagamento";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(381, 16);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(87, 13);
            this.label37.TabIndex = 261;
            this.label37.Text = "Data Pagamento";
            // 
            // ddtParcelaDataPagamento
            // 
            this.ddtParcelaDataPagamento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ddtParcelaDataPagamento.Location = new System.Drawing.Point(382, 30);
            this.ddtParcelaDataPagamento.MinDate = new System.DateTime(2016, 9, 1, 0, 0, 0, 0);
            this.ddtParcelaDataPagamento.Name = "ddtParcelaDataPagamento";
            this.ddtParcelaDataPagamento.Size = new System.Drawing.Size(90, 20);
            this.ddtParcelaDataPagamento.TabIndex = 5;
            this.ddtParcelaDataPagamento.Value = new System.DateTime(2017, 10, 10, 19, 45, 20, 0);
            this.ddtParcelaDataPagamento.ValueChanged += new System.EventHandler(this.ddtParcelaDataPagamento_ValueChanged);
            // 
            // lblParcelaCreditoTaxas
            // 
            this.lblParcelaCreditoTaxas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaCreditoTaxas.Location = new System.Drawing.Point(327, 32);
            this.lblParcelaCreditoTaxas.Name = "lblParcelaCreditoTaxas";
            this.lblParcelaCreditoTaxas.Size = new System.Drawing.Size(49, 21);
            this.lblParcelaCreditoTaxas.TabIndex = 4;
            this.lblParcelaCreditoTaxas.Text = "0";
            this.lblParcelaCreditoTaxas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblParcelaCartaoValorCredito
            // 
            this.lblParcelaCartaoValorCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaCartaoValorCredito.Location = new System.Drawing.Point(238, 74);
            this.lblParcelaCartaoValorCredito.Name = "lblParcelaCartaoValorCredito";
            this.lblParcelaCartaoValorCredito.Size = new System.Drawing.Size(67, 21);
            this.lblParcelaCartaoValorCredito.TabIndex = 7;
            this.lblParcelaCartaoValorCredito.Text = "R$ 0,00";
            this.lblParcelaCartaoValorCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 158;
            this.label3.Text = "Taxas (%)";
            // 
            // lblParcelaCartaoValorParcela
            // 
            this.lblParcelaCartaoValorParcela.Location = new System.Drawing.Point(311, 74);
            this.lblParcelaCartaoValorParcela.Name = "lblParcelaCartaoValorParcela";
            this.lblParcelaCartaoValorParcela.Size = new System.Drawing.Size(74, 20);
            this.lblParcelaCartaoValorParcela.TabIndex = 8;
            this.lblParcelaCartaoValorParcela.Text = "R$ 0,00";
            this.lblParcelaCartaoValorParcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lblParcelaCartaoValorParcela.Leave += new System.EventHandler(this.lblParcelaCartaoValorParcela_Leave);
            // 
            // lblParcelaCartaoDataRecebimentoBanco
            // 
            this.lblParcelaCartaoDataRecebimentoBanco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaCartaoDataRecebimentoBanco.Location = new System.Drawing.Point(391, 73);
            this.lblParcelaCartaoDataRecebimentoBanco.Name = "lblParcelaCartaoDataRecebimentoBanco";
            this.lblParcelaCartaoDataRecebimentoBanco.Size = new System.Drawing.Size(86, 21);
            this.lblParcelaCartaoDataRecebimentoBanco.TabIndex = 9;
            this.lblParcelaCartaoDataRecebimentoBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(308, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 49;
            this.label14.Text = "Valor Pago";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(398, 56);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(69, 13);
            this.label23.TabIndex = 156;
            this.label23.Text = "Venc. Cartão";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(235, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 13);
            this.label17.TabIndex = 160;
            this.label17.Text = "Credito";
            // 
            // txtParcelaCartaoNomeTitular
            // 
            this.txtParcelaCartaoNomeTitular.Location = new System.Drawing.Point(9, 76);
            this.txtParcelaCartaoNomeTitular.MaxLength = 50;
            this.txtParcelaCartaoNomeTitular.Name = "txtParcelaCartaoNomeTitular";
            this.txtParcelaCartaoNomeTitular.Size = new System.Drawing.Size(223, 20);
            this.txtParcelaCartaoNomeTitular.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Nome Completo Titular Cartão";
            // 
            // mskParcelaCartaoCPFTitular
            // 
            this.mskParcelaCartaoCPFTitular.Location = new System.Drawing.Point(106, 34);
            this.mskParcelaCartaoCPFTitular.Mask = "000,000,000-00";
            this.mskParcelaCartaoCPFTitular.Name = "mskParcelaCartaoCPFTitular";
            this.mskParcelaCartaoCPFTitular.Size = new System.Drawing.Size(89, 20);
            this.mskParcelaCartaoCPFTitular.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 137;
            this.label5.Text = "CPF Cartão";
            // 
            // txtParcelaCartaoNumero
            // 
            this.txtParcelaCartaoNumero.Location = new System.Drawing.Point(9, 34);
            this.txtParcelaCartaoNumero.MaxLength = 20;
            this.txtParcelaCartaoNumero.Name = "txtParcelaCartaoNumero";
            this.txtParcelaCartaoNumero.Size = new System.Drawing.Size(94, 20);
            this.txtParcelaCartaoNumero.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 136;
            this.label16.Text = "N.º Cartão";
            // 
            // tbFormasPagamento
            // 
            this.tbFormasPagamento.Controls.Add(this.tbBoleto);
            this.tbFormasPagamento.Controls.Add(this.tbCredito);
            this.tbFormasPagamento.Controls.Add(this.tbDebito);
            this.tbFormasPagamento.Controls.Add(this.tbEspecie);
            this.tbFormasPagamento.Location = new System.Drawing.Point(6, 101);
            this.tbFormasPagamento.Name = "tbFormasPagamento";
            this.tbFormasPagamento.SelectedIndex = 0;
            this.tbFormasPagamento.Size = new System.Drawing.Size(465, 83);
            this.tbFormasPagamento.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbFormasPagamento, "Confirma pagamento via cartão de credito");
            this.tbFormasPagamento.Visible = false;
            // 
            // tbBoleto
            // 
            this.tbBoleto.Controls.Add(this.chkParcelaCancelamento);
            this.tbBoleto.Controls.Add(this.pctPagamentoBoleto);
            this.tbBoleto.Controls.Add(this.mskParcelaBoletoMulta);
            this.tbBoleto.Controls.Add(this.label19);
            this.tbBoleto.Controls.Add(this.label18);
            this.tbBoleto.Controls.Add(this.label20);
            this.tbBoleto.Controls.Add(this.mskParcelaBoletoTarifa);
            this.tbBoleto.Controls.Add(this.label15);
            this.tbBoleto.Controls.Add(this.mskParcelaBoletoIOF);
            this.tbBoleto.Controls.Add(this.txtParcelaBoletoDocBanco);
            this.tbBoleto.Location = new System.Drawing.Point(4, 22);
            this.tbBoleto.Name = "tbBoleto";
            this.tbBoleto.Padding = new System.Windows.Forms.Padding(3);
            this.tbBoleto.Size = new System.Drawing.Size(457, 57);
            this.tbBoleto.TabIndex = 0;
            this.tbBoleto.Text = "Boleto";
            this.tbBoleto.UseVisualStyleBackColor = true;
            // 
            // chkParcelaCancelamento
            // 
            this.chkParcelaCancelamento.AutoSize = true;
            this.chkParcelaCancelamento.Enabled = false;
            this.chkParcelaCancelamento.Location = new System.Drawing.Point(315, 0);
            this.chkParcelaCancelamento.Name = "chkParcelaCancelamento";
            this.chkParcelaCancelamento.Size = new System.Drawing.Size(139, 17);
            this.chkParcelaCancelamento.TabIndex = 260;
            this.chkParcelaCancelamento.Text = "Parcela Cancelamento?";
            this.chkParcelaCancelamento.UseVisualStyleBackColor = true;
            // 
            // pctPagamentoBoleto
            // 
            this.pctPagamentoBoleto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctPagamentoBoleto.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctPagamentoBoleto.Location = new System.Drawing.Point(423, 26);
            this.pctPagamentoBoleto.Name = "pctPagamentoBoleto";
            this.pctPagamentoBoleto.Size = new System.Drawing.Size(28, 21);
            this.pctPagamentoBoleto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctPagamentoBoleto.TabIndex = 235;
            this.pctPagamentoBoleto.TabStop = false;
            this.toolTip1.SetToolTip(this.pctPagamentoBoleto, "Confirma pagamento via boleto");
            this.pctPagamentoBoleto.Click += new System.EventHandler(this.pctPagamentoBoleto_Click);
            // 
            // mskParcelaBoletoMulta
            // 
            this.mskParcelaBoletoMulta.Location = new System.Drawing.Point(236, 26);
            this.mskParcelaBoletoMulta.Name = "mskParcelaBoletoMulta";
            this.mskParcelaBoletoMulta.Size = new System.Drawing.Size(75, 20);
            this.mskParcelaBoletoMulta.TabIndex = 3;
            this.mskParcelaBoletoMulta.Text = "R$ 0,00";
            this.mskParcelaBoletoMulta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 16;
            this.label19.Text = "Doc. Banco";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(152, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 15;
            this.label18.Text = "Tarifa";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(233, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(33, 13);
            this.label20.TabIndex = 17;
            this.label20.Text = "Multa";
            // 
            // mskParcelaBoletoTarifa
            // 
            this.mskParcelaBoletoTarifa.Location = new System.Drawing.Point(155, 26);
            this.mskParcelaBoletoTarifa.Name = "mskParcelaBoletoTarifa";
            this.mskParcelaBoletoTarifa.Size = new System.Drawing.Size(75, 20);
            this.mskParcelaBoletoTarifa.TabIndex = 2;
            this.mskParcelaBoletoTarifa.Text = "R$ 0,00";
            this.mskParcelaBoletoTarifa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(75, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "IOF";
            // 
            // mskParcelaBoletoIOF
            // 
            this.mskParcelaBoletoIOF.Location = new System.Drawing.Point(76, 26);
            this.mskParcelaBoletoIOF.Name = "mskParcelaBoletoIOF";
            this.mskParcelaBoletoIOF.Size = new System.Drawing.Size(75, 20);
            this.mskParcelaBoletoIOF.TabIndex = 1;
            this.mskParcelaBoletoIOF.Text = "R$ 0,00";
            this.mskParcelaBoletoIOF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtParcelaBoletoDocBanco
            // 
            this.txtParcelaBoletoDocBanco.Location = new System.Drawing.Point(9, 26);
            this.txtParcelaBoletoDocBanco.Name = "txtParcelaBoletoDocBanco";
            this.txtParcelaBoletoDocBanco.Size = new System.Drawing.Size(61, 20);
            this.txtParcelaBoletoDocBanco.TabIndex = 0;
            this.txtParcelaBoletoDocBanco.Text = "0";
            // 
            // tbCredito
            // 
            this.tbCredito.Controls.Add(this.label89);
            this.tbCredito.Controls.Add(this.label97);
            this.tbCredito.Controls.Add(this.lblCredValorCredito);
            this.tbCredito.Controls.Add(this.lblCredValorParcela);
            this.tbCredito.Controls.Add(this.pctParcelaPagamentoCredito);
            this.tbCredito.Controls.Add(this.lblParcelaCreditoTipoVencimento);
            this.tbCredito.Controls.Add(this.label24);
            this.tbCredito.Controls.Add(this.lblCredValorMinimoParcela);
            this.tbCredito.Controls.Add(this.label88);
            this.tbCredito.Controls.Add(this.cmbCredParcelas);
            this.tbCredito.Controls.Add(this.label29);
            this.tbCredito.Controls.Add(this.label87);
            this.tbCredito.Controls.Add(this.cmbCredBandeira);
            this.tbCredito.Location = new System.Drawing.Point(4, 22);
            this.tbCredito.Name = "tbCredito";
            this.tbCredito.Size = new System.Drawing.Size(457, 57);
            this.tbCredito.TabIndex = 2;
            this.tbCredito.Text = "Credito";
            this.tbCredito.UseVisualStyleBackColor = true;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(318, 12);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(40, 13);
            this.label89.TabIndex = 262;
            this.label89.Text = "Credito";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(251, 12);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(61, 13);
            this.label97.TabIndex = 237;
            this.label97.Text = "Vlr. Parcela";
            // 
            // lblCredValorCredito
            // 
            this.lblCredValorCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCredValorCredito.Location = new System.Drawing.Point(314, 25);
            this.lblCredValorCredito.Name = "lblCredValorCredito";
            this.lblCredValorCredito.Size = new System.Drawing.Size(58, 21);
            this.lblCredValorCredito.TabIndex = 258;
            // 
            // lblCredValorParcela
            // 
            this.lblCredValorParcela.BackColor = System.Drawing.Color.White;
            this.lblCredValorParcela.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCredValorParcela.Location = new System.Drawing.Point(250, 25);
            this.lblCredValorParcela.Name = "lblCredValorParcela";
            this.lblCredValorParcela.Size = new System.Drawing.Size(61, 20);
            this.lblCredValorParcela.TabIndex = 236;
            this.lblCredValorParcela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pctParcelaPagamentoCredito
            // 
            this.pctParcelaPagamentoCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctParcelaPagamentoCredito.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctParcelaPagamentoCredito.Location = new System.Drawing.Point(426, 26);
            this.pctParcelaPagamentoCredito.Name = "pctParcelaPagamentoCredito";
            this.pctParcelaPagamentoCredito.Size = new System.Drawing.Size(28, 21);
            this.pctParcelaPagamentoCredito.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctParcelaPagamentoCredito.TabIndex = 235;
            this.pctParcelaPagamentoCredito.TabStop = false;
            this.pctParcelaPagamentoCredito.Click += new System.EventHandler(this.pctParcelaPagamentoCredito_Click);
            // 
            // lblParcelaCreditoTipoVencimento
            // 
            this.lblParcelaCreditoTipoVencimento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaCreditoTipoVencimento.Location = new System.Drawing.Point(376, 25);
            this.lblParcelaCreditoTipoVencimento.Name = "lblParcelaCreditoTipoVencimento";
            this.lblParcelaCreditoTipoVencimento.Size = new System.Drawing.Size(45, 21);
            this.lblParcelaCreditoTipoVencimento.TabIndex = 149;
            this.lblParcelaCreditoTipoVencimento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(371, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 13);
            this.label24.TabIndex = 155;
            this.label24.Text = "Tipo Venc.";
            // 
            // lblCredValorMinimoParcela
            // 
            this.lblCredValorMinimoParcela.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCredValorMinimoParcela.Location = new System.Drawing.Point(186, 26);
            this.lblCredValorMinimoParcela.Name = "lblCredValorMinimoParcela";
            this.lblCredValorMinimoParcela.Size = new System.Drawing.Size(60, 21);
            this.lblCredValorMinimoParcela.TabIndex = 8;
            this.lblCredValorMinimoParcela.Text = "R$ 0,00";
            this.lblCredValorMinimoParcela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(192, 6);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(40, 13);
            this.label88.TabIndex = 145;
            this.label88.Text = "Minimo";
            // 
            // cmbCredParcelas
            // 
            this.cmbCredParcelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCredParcelas.FormattingEnabled = true;
            this.cmbCredParcelas.Location = new System.Drawing.Point(117, 26);
            this.cmbCredParcelas.Name = "cmbCredParcelas";
            this.cmbCredParcelas.Size = new System.Drawing.Size(63, 21);
            this.cmbCredParcelas.TabIndex = 7;
            this.cmbCredParcelas.SelectedIndexChanged += new System.EventHandler(this.cmbCredParcelas_SelectedIndexChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(119, 6);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(66, 13);
            this.label29.TabIndex = 150;
            this.label29.Text = "N.º Parcelas";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(3, 7);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(49, 13);
            this.label87.TabIndex = 144;
            this.label87.Text = "Bandeira";
            // 
            // cmbCredBandeira
            // 
            this.cmbCredBandeira.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCredBandeira.FormattingEnabled = true;
            this.cmbCredBandeira.Location = new System.Drawing.Point(6, 26);
            this.cmbCredBandeira.Name = "cmbCredBandeira";
            this.cmbCredBandeira.Size = new System.Drawing.Size(108, 21);
            this.cmbCredBandeira.TabIndex = 6;
            this.cmbCredBandeira.SelectedIndexChanged += new System.EventHandler(this.cmbParcelaCreditoBandeira_SelectedIndexChanged);
            // 
            // tbDebito
            // 
            this.tbDebito.Controls.Add(this.pctParcelaPagamentoDebito);
            this.tbDebito.Location = new System.Drawing.Point(4, 22);
            this.tbDebito.Name = "tbDebito";
            this.tbDebito.Padding = new System.Windows.Forms.Padding(3);
            this.tbDebito.Size = new System.Drawing.Size(457, 57);
            this.tbDebito.TabIndex = 1;
            this.tbDebito.Text = "Debito";
            this.tbDebito.UseVisualStyleBackColor = true;
            // 
            // pctParcelaPagamentoDebito
            // 
            this.pctParcelaPagamentoDebito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctParcelaPagamentoDebito.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctParcelaPagamentoDebito.Location = new System.Drawing.Point(203, 18);
            this.pctParcelaPagamentoDebito.Name = "pctParcelaPagamentoDebito";
            this.pctParcelaPagamentoDebito.Size = new System.Drawing.Size(28, 21);
            this.pctParcelaPagamentoDebito.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctParcelaPagamentoDebito.TabIndex = 235;
            this.pctParcelaPagamentoDebito.TabStop = false;
            this.toolTip1.SetToolTip(this.pctParcelaPagamentoDebito, "Confirma pagamento via debito");
            this.pctParcelaPagamentoDebito.Click += new System.EventHandler(this.pctParcelaPagamentoDebito_Click);
            // 
            // tbEspecie
            // 
            this.tbEspecie.Controls.Add(this.pctParcelaPagamentoEspecie);
            this.tbEspecie.Controls.Add(this.label51);
            this.tbEspecie.Location = new System.Drawing.Point(4, 22);
            this.tbEspecie.Name = "tbEspecie";
            this.tbEspecie.Size = new System.Drawing.Size(457, 57);
            this.tbEspecie.TabIndex = 3;
            this.tbEspecie.Text = "Especie";
            this.tbEspecie.UseVisualStyleBackColor = true;
            // 
            // pctParcelaPagamentoEspecie
            // 
            this.pctParcelaPagamentoEspecie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctParcelaPagamentoEspecie.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctParcelaPagamentoEspecie.Location = new System.Drawing.Point(243, 16);
            this.pctParcelaPagamentoEspecie.Name = "pctParcelaPagamentoEspecie";
            this.pctParcelaPagamentoEspecie.Size = new System.Drawing.Size(28, 21);
            this.pctParcelaPagamentoEspecie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctParcelaPagamentoEspecie.TabIndex = 235;
            this.pctParcelaPagamentoEspecie.TabStop = false;
            this.toolTip1.SetToolTip(this.pctParcelaPagamentoEspecie, "Confirma pagamento via dinheiro");
            this.pctParcelaPagamentoEspecie.Click += new System.EventHandler(this.pctParcelaPagamentoEspecie_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(21, 24);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(216, 13);
            this.label51.TabIndex = 148;
            this.label51.Text = "Confirmar recebimento do valor em dinheiro?";
            // 
            // txtParcelaCartaoAutorizacao
            // 
            this.txtParcelaCartaoAutorizacao.Location = new System.Drawing.Point(201, 34);
            this.txtParcelaCartaoAutorizacao.MaxLength = 20;
            this.txtParcelaCartaoAutorizacao.Name = "txtParcelaCartaoAutorizacao";
            this.txtParcelaCartaoAutorizacao.Size = new System.Drawing.Size(68, 20);
            this.txtParcelaCartaoAutorizacao.TabIndex = 2;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(198, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(63, 13);
            this.label27.TabIndex = 152;
            this.label27.Text = "Autorização";
            // 
            // txtParcelaCartaoCodValidador
            // 
            this.txtParcelaCartaoCodValidador.Location = new System.Drawing.Point(273, 33);
            this.txtParcelaCartaoCodValidador.Name = "txtParcelaCartaoCodValidador";
            this.txtParcelaCartaoCodValidador.Size = new System.Drawing.Size(48, 20);
            this.txtParcelaCartaoCodValidador.TabIndex = 3;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(270, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(51, 13);
            this.label26.TabIndex = 153;
            this.label26.Text = "Validador";
            // 
            // pctParcelaStatusAlterar
            // 
            this.pctParcelaStatusAlterar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctParcelaStatusAlterar.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctParcelaStatusAlterar.Location = new System.Drawing.Point(829, 226);
            this.pctParcelaStatusAlterar.Name = "pctParcelaStatusAlterar";
            this.pctParcelaStatusAlterar.Size = new System.Drawing.Size(28, 21);
            this.pctParcelaStatusAlterar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctParcelaStatusAlterar.TabIndex = 245;
            this.pctParcelaStatusAlterar.TabStop = false;
            this.toolTip1.SetToolTip(this.pctParcelaStatusAlterar, "Altera status da parcela.");
            this.pctParcelaStatusAlterar.Click += new System.EventHandler(this.pctParcelaStatusAlterar_Click);
            // 
            // lblParcelaProdutoId
            // 
            this.lblParcelaProdutoId.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblParcelaProdutoId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaProdutoId.Location = new System.Drawing.Point(729, 266);
            this.lblParcelaProdutoId.Name = "lblParcelaProdutoId";
            this.lblParcelaProdutoId.Size = new System.Drawing.Size(44, 20);
            this.lblParcelaProdutoId.TabIndex = 243;
            this.lblParcelaProdutoId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblParcelaProdutoId.Visible = false;
            // 
            // lblParcelaProduto
            // 
            this.lblParcelaProduto.BackColor = System.Drawing.Color.White;
            this.lblParcelaProduto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaProduto.Location = new System.Drawing.Point(729, 266);
            this.lblParcelaProduto.Name = "lblParcelaProduto";
            this.lblParcelaProduto.Size = new System.Drawing.Size(204, 20);
            this.lblParcelaProduto.TabIndex = 244;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(729, 250);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(44, 13);
            this.label34.TabIndex = 242;
            this.label34.Text = "Produto";
            // 
            // txtParcelaSubTotal
            // 
            this.txtParcelaSubTotal.Location = new System.Drawing.Point(1113, 266);
            this.txtParcelaSubTotal.Name = "txtParcelaSubTotal";
            this.txtParcelaSubTotal.ReadOnly = true;
            this.txtParcelaSubTotal.Size = new System.Drawing.Size(64, 20);
            this.txtParcelaSubTotal.TabIndex = 237;
            this.txtParcelaSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtParcelaDesconto
            // 
            this.txtParcelaDesconto.Enabled = false;
            this.txtParcelaDesconto.Location = new System.Drawing.Point(1025, 266);
            this.txtParcelaDesconto.Name = "txtParcelaDesconto";
            this.txtParcelaDesconto.Size = new System.Drawing.Size(80, 20);
            this.txtParcelaDesconto.TabIndex = 236;
            this.txtParcelaDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtParcelaDesconto.Leave += new System.EventHandler(this.txtParcelaDesconto_Leave);
            // 
            // txtParcelaValorUnitario
            // 
            this.txtParcelaValorUnitario.Location = new System.Drawing.Point(939, 266);
            this.txtParcelaValorUnitario.Name = "txtParcelaValorUnitario";
            this.txtParcelaValorUnitario.ReadOnly = true;
            this.txtParcelaValorUnitario.Size = new System.Drawing.Size(80, 20);
            this.txtParcelaValorUnitario.TabIndex = 235;
            this.txtParcelaValorUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(1110, 250);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(50, 13);
            this.label60.TabIndex = 240;
            this.label60.Text = "SubTotal";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(1022, 250);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(53, 13);
            this.label59.TabIndex = 239;
            this.label59.Text = "Desconto";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(938, 250);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(31, 13);
            this.label58.TabIndex = 238;
            this.label58.Text = "Valor";
            // 
            // lblParcelaPlanoId
            // 
            this.lblParcelaPlanoId.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblParcelaPlanoId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaPlanoId.Location = new System.Drawing.Point(983, 192);
            this.lblParcelaPlanoId.Name = "lblParcelaPlanoId";
            this.lblParcelaPlanoId.Size = new System.Drawing.Size(41, 20);
            this.lblParcelaPlanoId.TabIndex = 227;
            this.lblParcelaPlanoId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblParcelaPlanoId.Visible = false;
            // 
            // pctAlterarDadosParcela
            // 
            this.pctAlterarDadosParcela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctAlterarDadosParcela.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
            this.pctAlterarDadosParcela.Location = new System.Drawing.Point(1184, 266);
            this.pctAlterarDadosParcela.Name = "pctAlterarDadosParcela";
            this.pctAlterarDadosParcela.Size = new System.Drawing.Size(28, 21);
            this.pctAlterarDadosParcela.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctAlterarDadosParcela.TabIndex = 234;
            this.pctAlterarDadosParcela.TabStop = false;
            this.toolTip1.SetToolTip(this.pctAlterarDadosParcela, "Informa novo desconto e valor final do produto/parcela");
            this.pctAlterarDadosParcela.Click += new System.EventHandler(this.pctAlterarDadosParcela_Click);
            // 
            // ddtParcelaVencimento
            // 
            this.ddtParcelaVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ddtParcelaVencimento.Location = new System.Drawing.Point(868, 192);
            this.ddtParcelaVencimento.Name = "ddtParcelaVencimento";
            this.ddtParcelaVencimento.Size = new System.Drawing.Size(78, 20);
            this.ddtParcelaVencimento.TabIndex = 233;
            this.toolTip1.SetToolTip(this.ddtParcelaVencimento, "Vencimento da parcela");
            // 
            // cmbFormaPagamento
            // 
            this.cmbFormaPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPagamento.FormattingEnabled = true;
            this.cmbFormaPagamento.Location = new System.Drawing.Point(961, 226);
            this.cmbFormaPagamento.Name = "cmbFormaPagamento";
            this.cmbFormaPagamento.Size = new System.Drawing.Size(216, 21);
            this.cmbFormaPagamento.TabIndex = 232;
            this.toolTip1.SetToolTip(this.cmbFormaPagamento, "Forma de pagamento da parcela");
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(866, 211);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(58, 13);
            this.label52.TabIndex = 230;
            this.label52.Text = "Valor Total";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(865, 177);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(63, 13);
            this.label42.TabIndex = 229;
            this.label42.Text = "Vencimento";
            // 
            // lblParcelaPlano
            // 
            this.lblParcelaPlano.BackColor = System.Drawing.Color.White;
            this.lblParcelaPlano.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaPlano.Location = new System.Drawing.Point(983, 194);
            this.lblParcelaPlano.Name = "lblParcelaPlano";
            this.lblParcelaPlano.Size = new System.Drawing.Size(223, 20);
            this.lblParcelaPlano.TabIndex = 228;
            this.lblParcelaPlano.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(980, 177);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(34, 13);
            this.label79.TabIndex = 226;
            this.label79.Text = "Plano";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(959, 212);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(36, 13);
            this.label77.TabIndex = 224;
            this.label77.Text = "Forma";
            // 
            // lblParcelaValorTotal
            // 
            this.lblParcelaValorTotal.BackColor = System.Drawing.Color.White;
            this.lblParcelaValorTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaValorTotal.Location = new System.Drawing.Point(867, 227);
            this.lblParcelaValorTotal.Name = "lblParcelaValorTotal";
            this.lblParcelaValorTotal.Size = new System.Drawing.Size(88, 20);
            this.lblParcelaValorTotal.TabIndex = 222;
            this.lblParcelaValorTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(729, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 221;
            this.label4.Text = "Id";
            // 
            // lblParcelaId
            // 
            this.lblParcelaId.BackColor = System.Drawing.Color.White;
            this.lblParcelaId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaId.Location = new System.Drawing.Point(729, 193);
            this.lblParcelaId.Name = "lblParcelaId";
            this.lblParcelaId.Size = new System.Drawing.Size(52, 20);
            this.lblParcelaId.TabIndex = 220;
            this.lblParcelaId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ddtParcelaItens
            // 
            this.ddtParcelaItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddtParcelaItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ListID,
            this.TB017_id,
            this.TB017_Item,
            this.TB017_ValorUnitario,
            this.TB017_ValorDesconto,
            this.TB017_ValorFinal});
            this.ddtParcelaItens.Location = new System.Drawing.Point(729, 77);
            this.ddtParcelaItens.MultiSelect = false;
            this.ddtParcelaItens.Name = "ddtParcelaItens";
            this.ddtParcelaItens.RowHeadersVisible = false;
            this.ddtParcelaItens.Size = new System.Drawing.Size(483, 96);
            this.ddtParcelaItens.TabIndex = 218;
            this.ddtParcelaItens.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddtParcelaItens_CellClick);
            // 
            // ListID
            // 
            this.ListID.DataPropertyName = "LstID";
            this.ListID.HeaderText = "ListID";
            this.ListID.Name = "ListID";
            this.ListID.ReadOnly = true;
            this.ListID.Visible = false;
            this.ListID.Width = 50;
            // 
            // TB017_id
            // 
            this.TB017_id.DataPropertyName = "TB017_id";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TB017_id.DefaultCellStyle = dataGridViewCellStyle2;
            this.TB017_id.HeaderText = "id";
            this.TB017_id.Name = "TB017_id";
            this.TB017_id.ReadOnly = true;
            this.TB017_id.Width = 60;
            // 
            // TB017_Item
            // 
            this.TB017_Item.DataPropertyName = "TB017_Item";
            this.TB017_Item.HeaderText = "Produto";
            this.TB017_Item.Name = "TB017_Item";
            this.TB017_Item.ReadOnly = true;
            this.TB017_Item.Width = 200;
            // 
            // TB017_ValorUnitario
            // 
            this.TB017_ValorUnitario.DataPropertyName = "TB017_ValorUnitario";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = "0";
            this.TB017_ValorUnitario.DefaultCellStyle = dataGridViewCellStyle3;
            this.TB017_ValorUnitario.HeaderText = "Valor";
            this.TB017_ValorUnitario.Name = "TB017_ValorUnitario";
            this.TB017_ValorUnitario.ReadOnly = true;
            this.TB017_ValorUnitario.Width = 70;
            // 
            // TB017_ValorDesconto
            // 
            this.TB017_ValorDesconto.DataPropertyName = "TB017_ValorDesconto";
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = "0";
            this.TB017_ValorDesconto.DefaultCellStyle = dataGridViewCellStyle4;
            this.TB017_ValorDesconto.HeaderText = "Desconto";
            this.TB017_ValorDesconto.Name = "TB017_ValorDesconto";
            this.TB017_ValorDesconto.ReadOnly = true;
            this.TB017_ValorDesconto.Width = 70;
            // 
            // TB017_ValorFinal
            // 
            this.TB017_ValorFinal.DataPropertyName = "TB017_ValorFinal";
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = "0";
            this.TB017_ValorFinal.DefaultCellStyle = dataGridViewCellStyle5;
            this.TB017_ValorFinal.HeaderText = "Total";
            this.TB017_ValorFinal.Name = "TB017_ValorFinal";
            this.TB017_ValorFinal.ReadOnly = true;
            this.TB017_ValorFinal.Width = 70;
            // 
            // ddtParcelas
            // 
            this.ddtParcelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddtParcelas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TB016_id,
            this.TB016_NossoNumero,
            this.TB012_CicloContrato,
            this.TB016_Emissao,
            this.TB016_Vencimento,
            this.TB016_Valor,
            this.TB016_DataPagamento,
            this.TB016_ValorPago,
            this.ParcelaTB012_Id,
            this.ParcelaTB015_id,
            this.ParcelaPlano,
            this.FormaPagamento,
            this.TB016_StatusS,
            this.Selecionar});
            this.ddtParcelas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.ddtParcelas.Location = new System.Drawing.Point(6, 78);
            this.ddtParcelas.MultiSelect = false;
            this.ddtParcelas.Name = "ddtParcelas";
            this.ddtParcelas.RowHeadersVisible = false;
            this.ddtParcelas.Size = new System.Drawing.Size(717, 314);
            this.ddtParcelas.TabIndex = 217;
            this.ddtParcelas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddtParcelas_CellClick);
            this.ddtParcelas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddtParcelas_CellEndEdit);
            this.ddtParcelas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ddtParcelas_CellFormatting);
            // 
            // DTContratoDependentes
            // 
            this.DTContratoDependentes.AllowUserToAddRows = false;
            this.DTContratoDependentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTContratoDependentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DependenteTB013_id,
            this.DependenteTB013_NomeExibicao,
            this.DependenteTB013_Idade,
            this.DependenteTB013_Cartao,
            this.DependenteTB013_StatusS});
            this.DTContratoDependentes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DTContratoDependentes.Location = new System.Drawing.Point(5, 393);
            this.DTContratoDependentes.MultiSelect = false;
            this.DTContratoDependentes.Name = "DTContratoDependentes";
            this.DTContratoDependentes.RowHeadersVisible = false;
            this.DTContratoDependentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DTContratoDependentes.Size = new System.Drawing.Size(718, 81);
            this.DTContratoDependentes.TabIndex = 216;
            // 
            // DependenteTB013_id
            // 
            this.DependenteTB013_id.DataPropertyName = "TB013_id";
            this.DependenteTB013_id.HeaderText = "id";
            this.DependenteTB013_id.Name = "DependenteTB013_id";
            this.DependenteTB013_id.ToolTipText = "Click para selecionar o registro.";
            this.DependenteTB013_id.Width = 60;
            // 
            // DependenteTB013_NomeExibicao
            // 
            this.DependenteTB013_NomeExibicao.DataPropertyName = "TB013_NomeExibicao";
            this.DependenteTB013_NomeExibicao.HeaderText = "Nome";
            this.DependenteTB013_NomeExibicao.Name = "DependenteTB013_NomeExibicao";
            this.DependenteTB013_NomeExibicao.Width = 300;
            // 
            // DependenteTB013_Idade
            // 
            this.DependenteTB013_Idade.DataPropertyName = "TB013_Idade";
            this.DependenteTB013_Idade.HeaderText = "Idade";
            this.DependenteTB013_Idade.Name = "DependenteTB013_Idade";
            this.DependenteTB013_Idade.Width = 40;
            // 
            // DependenteTB013_Cartao
            // 
            this.DependenteTB013_Cartao.DataPropertyName = "TB013_Cartao";
            this.DependenteTB013_Cartao.HeaderText = "Cartão";
            this.DependenteTB013_Cartao.Name = "DependenteTB013_Cartao";
            this.DependenteTB013_Cartao.ReadOnly = true;
            this.DependenteTB013_Cartao.Width = 150;
            // 
            // DependenteTB013_StatusS
            // 
            this.DependenteTB013_StatusS.DataPropertyName = "TB013_StatusS";
            this.DependenteTB013_StatusS.HeaderText = "Status";
            this.DependenteTB013_StatusS.Name = "DependenteTB013_StatusS";
            this.DependenteTB013_StatusS.Width = 65;
            // 
            // cmbContratoStatus
            // 
            this.cmbContratoStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContratoStatus.Enabled = false;
            this.cmbContratoStatus.FormattingEnabled = true;
            this.cmbContratoStatus.Location = new System.Drawing.Point(382, 51);
            this.cmbContratoStatus.Name = "cmbContratoStatus";
            this.cmbContratoStatus.Size = new System.Drawing.Size(108, 21);
            this.cmbContratoStatus.TabIndex = 214;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(379, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 215;
            this.label8.Text = "Status Contrato";
            // 
            // txtContratoTitularNomeCompleto
            // 
            this.txtContratoTitularNomeCompleto.BackColor = System.Drawing.Color.White;
            this.txtContratoTitularNomeCompleto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContratoTitularNomeCompleto.Location = new System.Drawing.Point(810, 50);
            this.txtContratoTitularNomeCompleto.MaxLength = 150;
            this.txtContratoTitularNomeCompleto.Name = "txtContratoTitularNomeCompleto";
            this.txtContratoTitularNomeCompleto.ReadOnly = true;
            this.txtContratoTitularNomeCompleto.Size = new System.Drawing.Size(402, 20);
            this.txtContratoTitularNomeCompleto.TabIndex = 212;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(807, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 213;
            this.label9.Text = "Nome Completo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(726, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 211;
            this.label7.Text = "CPF";
            // 
            // mskContratoTitularCPF
            // 
            this.mskContratoTitularCPF.Enabled = false;
            this.mskContratoTitularCPF.Location = new System.Drawing.Point(729, 50);
            this.mskContratoTitularCPF.Mask = "999,999,999-99";
            this.mskContratoTitularCPF.Name = "mskContratoTitularCPF";
            this.mskContratoTitularCPF.ReadOnly = true;
            this.mskContratoTitularCPF.Size = new System.Drawing.Size(78, 20);
            this.mskContratoTitularCPF.TabIndex = 210;
            // 
            // cmbParcelaCiclo
            // 
            this.cmbParcelaCiclo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParcelaCiclo.FormattingEnabled = true;
            this.cmbParcelaCiclo.Location = new System.Drawing.Point(530, 51);
            this.cmbParcelaCiclo.Name = "cmbParcelaCiclo";
            this.cmbParcelaCiclo.Size = new System.Drawing.Size(75, 21);
            this.cmbParcelaCiclo.TabIndex = 172;
            this.toolTip1.SetToolTip(this.cmbParcelaCiclo, "Lista de todos os ciclos do contrato");
            this.cmbParcelaCiclo.SelectedIndexChanged += new System.EventHandler(this.cmbParcelaCiclo_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(527, 33);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(30, 13);
            this.label22.TabIndex = 171;
            this.label22.Text = "Ciclo";
            // 
            // dtContratoFim
            // 
            this.dtContratoFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtContratoFim.Location = new System.Drawing.Point(180, 52);
            this.dtContratoFim.Name = "dtContratoFim";
            this.dtContratoFim.Size = new System.Drawing.Size(79, 20);
            this.dtContratoFim.TabIndex = 170;
            // 
            // dtContratoInicio
            // 
            this.dtContratoInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtContratoInicio.Location = new System.Drawing.Point(88, 52);
            this.dtContratoInicio.Name = "dtContratoInicio";
            this.dtContratoInicio.Size = new System.Drawing.Size(86, 20);
            this.dtContratoInicio.TabIndex = 168;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(135, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 169;
            this.label6.Text = "Validade Contrato";
            // 
            // lblContrato
            // 
            this.lblContrato.BackColor = System.Drawing.Color.White;
            this.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContrato.Location = new System.Drawing.Point(9, 52);
            this.lblContrato.Name = "lblContrato";
            this.lblContrato.Size = new System.Drawing.Size(71, 20);
            this.lblContrato.TabIndex = 4;
            this.lblContrato.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contrato";
            // 
            // mnuParcela
            // 
            this.mnuParcela.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuParcelaAnotacoes,
            this.documentosToolStripMenuItem,
            this.mnuParcelaIncluirProduto,
            this.unirToolStripMenuItem,
            this.mnuParcelaCancelarParcela,
            this.mnuParcelaEmitirParcela,
            this.mnuParcelaEmitirBoleto,
            this.alterarCicloToolStripMenuItem,
            this.mnuParcelaAlterarCiclo,
            this.mnuParcelaReencaminharSICOOB,
            this.mnuParcelaFechar});
            this.mnuParcela.Location = new System.Drawing.Point(3, 3);
            this.mnuParcela.Name = "mnuParcela";
            this.mnuParcela.Size = new System.Drawing.Size(1212, 24);
            this.mnuParcela.TabIndex = 1;
            this.mnuParcela.Text = "menuStrip2";
            // 
            // mnuParcelaAnotacoes
            // 
            this.mnuParcelaAnotacoes.Name = "mnuParcelaAnotacoes";
            this.mnuParcelaAnotacoes.Size = new System.Drawing.Size(75, 20);
            this.mnuParcelaAnotacoes.Text = "Anotações";
            this.mnuParcelaAnotacoes.ToolTipText = "Abre anotações do contrato";
            this.mnuParcelaAnotacoes.Click += new System.EventHandler(this.mnuParcelaAnotacoes_Click);
            // 
            // documentosToolStripMenuItem
            // 
            this.documentosToolStripMenuItem.Name = "documentosToolStripMenuItem";
            this.documentosToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.documentosToolStripMenuItem.Text = "Documentos";
            this.documentosToolStripMenuItem.Click += new System.EventHandler(this.documentosToolStripMenuItem_Click);
            // 
            // mnuParcelaIncluirProduto
            // 
            this.mnuParcelaIncluirProduto.Enabled = false;
            this.mnuParcelaIncluirProduto.Name = "mnuParcelaIncluirProduto";
            this.mnuParcelaIncluirProduto.Size = new System.Drawing.Size(98, 20);
            this.mnuParcelaIncluirProduto.Text = "Incluir Produto";
            this.mnuParcelaIncluirProduto.ToolTipText = "Inclui novo produto para a parcela";
            // 
            // unirToolStripMenuItem
            // 
            this.unirToolStripMenuItem.Name = "unirToolStripMenuItem";
            this.unirToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.unirToolStripMenuItem.Text = "Unir Parcela";
            this.unirToolStripMenuItem.Click += new System.EventHandler(this.unirToolStripMenuItem_Click);
            // 
            // mnuParcelaCancelarParcela
            // 
            this.mnuParcelaCancelarParcela.Name = "mnuParcelaCancelarParcela";
            this.mnuParcelaCancelarParcela.Size = new System.Drawing.Size(106, 20);
            this.mnuParcelaCancelarParcela.Text = "Cancelar Parcela";
            this.mnuParcelaCancelarParcela.ToolTipText = "Cancela a parcela selecionada";
            this.mnuParcelaCancelarParcela.Click += new System.EventHandler(this.mnuParcelaCancelarParcela_Click);
            // 
            // mnuParcelaEmitirParcela
            // 
            this.mnuParcelaEmitirParcela.Name = "mnuParcelaEmitirParcela";
            this.mnuParcelaEmitirParcela.Size = new System.Drawing.Size(88, 20);
            this.mnuParcelaEmitirParcela.Text = "Gerar Parcela";
            this.mnuParcelaEmitirParcela.ToolTipText = "Emite parcelas avulsas";
            this.mnuParcelaEmitirParcela.Click += new System.EventHandler(this.mnuParcelaEmitirParcela_Click);
            // 
            // mnuParcelaEmitirBoleto
            // 
            this.mnuParcelaEmitirBoleto.Enabled = false;
            this.mnuParcelaEmitirBoleto.Name = "mnuParcelaEmitirBoleto";
            this.mnuParcelaEmitirBoleto.Size = new System.Drawing.Size(87, 20);
            this.mnuParcelaEmitirBoleto.Text = "Emitir Boleto";
            this.mnuParcelaEmitirBoleto.ToolTipText = "Solicita emissão de boleto pelo SICOOB";
            this.mnuParcelaEmitirBoleto.Click += new System.EventHandler(this.mnuParcelaEmitirBoleto_Click);
            // 
            // alterarCicloToolStripMenuItem
            // 
            this.alterarCicloToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contratoToolStripMenuItem,
            this.parcelaToolStripMenuItem});
            this.alterarCicloToolStripMenuItem.Name = "alterarCicloToolStripMenuItem";
            this.alterarCicloToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.alterarCicloToolStripMenuItem.Text = "Alterar Ciclo";
            this.alterarCicloToolStripMenuItem.Click += new System.EventHandler(this.alterarCicloToolStripMenuItem_Click);
            // 
            // contratoToolStripMenuItem
            // 
            this.contratoToolStripMenuItem.Name = "contratoToolStripMenuItem";
            this.contratoToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.contratoToolStripMenuItem.Text = "Contrato";
            this.contratoToolStripMenuItem.Click += new System.EventHandler(this.contratoToolStripMenuItem_Click);
            // 
            // parcelaToolStripMenuItem
            // 
            this.parcelaToolStripMenuItem.Name = "parcelaToolStripMenuItem";
            this.parcelaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.parcelaToolStripMenuItem.Text = "Parcela";
            this.parcelaToolStripMenuItem.Click += new System.EventHandler(this.parcelaToolStripMenuItem_Click);
            // 
            // mnuParcelaAlterarCiclo
            // 
            this.mnuParcelaAlterarCiclo.Name = "mnuParcelaAlterarCiclo";
            this.mnuParcelaAlterarCiclo.Size = new System.Drawing.Size(104, 20);
            this.mnuParcelaAlterarCiclo.Text = "Endereço Fatura";
            // 
            // mnuParcelaReencaminharSICOOB
            // 
            this.mnuParcelaReencaminharSICOOB.Name = "mnuParcelaReencaminharSICOOB";
            this.mnuParcelaReencaminharSICOOB.Size = new System.Drawing.Size(140, 20);
            this.mnuParcelaReencaminharSICOOB.Text = "Reencaminhar SICOOB";
            this.mnuParcelaReencaminharSICOOB.ToolTipText = "Reencaminha solicitação do boleto ao SICOOB";
            this.mnuParcelaReencaminharSICOOB.Click += new System.EventHandler(this.mnuParcelaReencaminharSICOOB_Click);
            // 
            // mnuParcelaFechar
            // 
            this.mnuParcelaFechar.Name = "mnuParcelaFechar";
            this.mnuParcelaFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuParcelaFechar.Text = "Fechar";
            this.mnuParcelaFechar.Click += new System.EventHandler(this.mnuParcelaFechar_Click);
            // 
            // tbpDocumentos
            // 
            this.tbpDocumentos.Controls.Add(this.gprAnexarTermo);
            this.tbpDocumentos.Controls.Add(this.axAcroPDF1);
            this.tbpDocumentos.Controls.Add(this.ddgDocumentos);
            this.tbpDocumentos.Controls.Add(this.mnuDocumentos);
            this.tbpDocumentos.Location = new System.Drawing.Point(4, 22);
            this.tbpDocumentos.Name = "tbpDocumentos";
            this.tbpDocumentos.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDocumentos.Size = new System.Drawing.Size(1218, 479);
            this.tbpDocumentos.TabIndex = 2;
            this.tbpDocumentos.Text = "Documentos";
            this.tbpDocumentos.UseVisualStyleBackColor = true;
            // 
            // gprAnexarTermo
            // 
            this.gprAnexarTermo.Controls.Add(this.axAcroPDF2);
            this.gprAnexarTermo.Location = new System.Drawing.Point(402, 79);
            this.gprAnexarTermo.Name = "gprAnexarTermo";
            this.gprAnexarTermo.Size = new System.Drawing.Size(622, 361);
            this.gprAnexarTermo.TabIndex = 62;
            this.gprAnexarTermo.TabStop = false;
            this.gprAnexarTermo.Text = "Anexar Termo";
            this.gprAnexarTermo.Visible = false;
            // 
            // axAcroPDF2
            // 
            this.axAcroPDF2.Enabled = true;
            this.axAcroPDF2.Location = new System.Drawing.Point(6, 19);
            this.axAcroPDF2.Name = "axAcroPDF2";
            this.axAcroPDF2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF2.OcxState")));
            this.axAcroPDF2.Size = new System.Drawing.Size(606, 286);
            this.axAcroPDF2.TabIndex = 58;
            this.axAcroPDF2.Visible = false;
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(402, 40);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(685, 423);
            this.axAcroPDF1.TabIndex = 61;
            this.axAcroPDF1.Visible = false;
            // 
            // ddgDocumentos
            // 
            this.ddgDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddgDocumentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TB029_Id,
            this.TB029_Tipo,
            this.TB012_VSContrato});
            this.ddgDocumentos.Location = new System.Drawing.Point(6, 40);
            this.ddgDocumentos.Name = "ddgDocumentos";
            this.ddgDocumentos.Size = new System.Drawing.Size(319, 400);
            this.ddgDocumentos.TabIndex = 60;
            this.ddgDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddgDocumentos_CellClick);
            // 
            // TB029_Id
            // 
            this.TB029_Id.DataPropertyName = "TB029_Id";
            this.TB029_Id.HeaderText = "Id";
            this.TB029_Id.Name = "TB029_Id";
            this.TB029_Id.ReadOnly = true;
            this.TB029_Id.Width = 50;
            // 
            // TB029_Tipo
            // 
            this.TB029_Tipo.DataPropertyName = "TB029_TipoS";
            this.TB029_Tipo.HeaderText = "Tipo";
            this.TB029_Tipo.Name = "TB029_Tipo";
            this.TB029_Tipo.ReadOnly = true;
            this.TB029_Tipo.Width = 150;
            // 
            // TB012_VSContrato
            // 
            this.TB012_VSContrato.DataPropertyName = "TB012_VSContrato";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TB012_VSContrato.DefaultCellStyle = dataGridViewCellStyle13;
            this.TB012_VSContrato.HeaderText = "VS Contrato";
            this.TB012_VSContrato.Name = "TB012_VSContrato";
            this.TB012_VSContrato.ReadOnly = true;
            this.TB012_VSContrato.Width = 70;
            // 
            // mnuDocumentos
            // 
            this.mnuDocumentos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDocumentosFechar});
            this.mnuDocumentos.Location = new System.Drawing.Point(3, 3);
            this.mnuDocumentos.Name = "mnuDocumentos";
            this.mnuDocumentos.Size = new System.Drawing.Size(1212, 24);
            this.mnuDocumentos.TabIndex = 59;
            this.mnuDocumentos.Text = "menuStrip7";
            // 
            // mnuDocumentosFechar
            // 
            this.mnuDocumentosFechar.Name = "mnuDocumentosFechar";
            this.mnuDocumentosFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuDocumentosFechar.Text = "Fechar";
            this.mnuDocumentosFechar.Click += new System.EventHandler(this.mnuDocumentosFechar_Click);
            // 
            // grpParcelaEnd
            // 
            this.grpParcelaEnd.Controls.Add(this.lblParcelaPontoDeVenda);
            this.grpParcelaEnd.Controls.Add(this.cmbTitularPais);
            this.grpParcelaEnd.Controls.Add(this.label32);
            this.grpParcelaEnd.Controls.Add(this.label31);
            this.grpParcelaEnd.Controls.Add(this.txtContratoTitularNumero);
            this.grpParcelaEnd.Controls.Add(this.txtContratoTitularLogradouro);
            this.grpParcelaEnd.Controls.Add(this.label30);
            this.grpParcelaEnd.Controls.Add(this.mskContratoTitularCEP);
            this.grpParcelaEnd.Controls.Add(this.label28);
            this.grpParcelaEnd.Controls.Add(this.cmbContratoTitularEstado);
            this.grpParcelaEnd.Controls.Add(this.label25);
            this.grpParcelaEnd.Controls.Add(this.lblContratoTitularID);
            this.grpParcelaEnd.Controls.Add(this.cmbContratoTitularMunicipio);
            this.grpParcelaEnd.Controls.Add(this.label21);
            this.grpParcelaEnd.Location = new System.Drawing.Point(1453, 6);
            this.grpParcelaEnd.Name = "grpParcelaEnd";
            this.grpParcelaEnd.Size = new System.Drawing.Size(464, 192);
            this.grpParcelaEnd.TabIndex = 262;
            this.grpParcelaEnd.TabStop = false;
            this.grpParcelaEnd.Text = "Endereço de Cobrança";
            this.grpParcelaEnd.Visible = false;
            // 
            // lblParcelaPontoDeVenda
            // 
            this.lblParcelaPontoDeVenda.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblParcelaPontoDeVenda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParcelaPontoDeVenda.ForeColor = System.Drawing.Color.White;
            this.lblParcelaPontoDeVenda.Location = new System.Drawing.Point(402, 47);
            this.lblParcelaPontoDeVenda.Name = "lblParcelaPontoDeVenda";
            this.lblParcelaPontoDeVenda.Size = new System.Drawing.Size(47, 21);
            this.lblParcelaPontoDeVenda.TabIndex = 274;
            // 
            // cmbTitularPais
            // 
            this.cmbTitularPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTitularPais.Enabled = false;
            this.cmbTitularPais.FormattingEnabled = true;
            this.cmbTitularPais.Location = new System.Drawing.Point(10, 103);
            this.cmbTitularPais.Name = "cmbTitularPais";
            this.cmbTitularPais.Size = new System.Drawing.Size(71, 21);
            this.cmbTitularPais.TabIndex = 272;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(14, 89);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(27, 13);
            this.label32.TabIndex = 273;
            this.label32.Text = "Pais";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(402, 139);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(22, 13);
            this.label31.TabIndex = 271;
            this.label31.Text = "N.º";
            // 
            // txtContratoTitularNumero
            // 
            this.txtContratoTitularNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContratoTitularNumero.Enabled = false;
            this.txtContratoTitularNumero.Location = new System.Drawing.Point(405, 155);
            this.txtContratoTitularNumero.MaxLength = 11;
            this.txtContratoTitularNumero.Name = "txtContratoTitularNumero";
            this.txtContratoTitularNumero.Size = new System.Drawing.Size(44, 20);
            this.txtContratoTitularNumero.TabIndex = 270;
            // 
            // txtContratoTitularLogradouro
            // 
            this.txtContratoTitularLogradouro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContratoTitularLogradouro.Enabled = false;
            this.txtContratoTitularLogradouro.Location = new System.Drawing.Point(10, 155);
            this.txtContratoTitularLogradouro.MaxLength = 150;
            this.txtContratoTitularLogradouro.Name = "txtContratoTitularLogradouro";
            this.txtContratoTitularLogradouro.Size = new System.Drawing.Size(389, 20);
            this.txtContratoTitularLogradouro.TabIndex = 268;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(10, 139);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(61, 13);
            this.label30.TabIndex = 269;
            this.label30.Text = "Logradouro";
            // 
            // mskContratoTitularCEP
            // 
            this.mskContratoTitularCEP.Enabled = false;
            this.mskContratoTitularCEP.Location = new System.Drawing.Point(91, 104);
            this.mskContratoTitularCEP.Mask = "00000-000";
            this.mskContratoTitularCEP.Name = "mskContratoTitularCEP";
            this.mskContratoTitularCEP.Size = new System.Drawing.Size(61, 20);
            this.mskContratoTitularCEP.TabIndex = 266;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(88, 92);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 13);
            this.label28.TabIndex = 267;
            this.label28.Text = "CEP";
            // 
            // cmbContratoTitularEstado
            // 
            this.cmbContratoTitularEstado.DisplayMember = "TB005_Estado";
            this.cmbContratoTitularEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContratoTitularEstado.Enabled = false;
            this.cmbContratoTitularEstado.FormattingEnabled = true;
            this.cmbContratoTitularEstado.Location = new System.Drawing.Point(158, 103);
            this.cmbContratoTitularEstado.Name = "cmbContratoTitularEstado";
            this.cmbContratoTitularEstado.Size = new System.Drawing.Size(118, 21);
            this.cmbContratoTitularEstado.TabIndex = 264;
            this.cmbContratoTitularEstado.ValueMember = "TB005_Id";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(155, 87);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(21, 13);
            this.label25.TabIndex = 265;
            this.label25.Text = "UF";
            // 
            // lblContratoTitularID
            // 
            this.lblContratoTitularID.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblContratoTitularID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContratoTitularID.ForeColor = System.Drawing.Color.White;
            this.lblContratoTitularID.Location = new System.Drawing.Point(402, 74);
            this.lblContratoTitularID.Name = "lblContratoTitularID";
            this.lblContratoTitularID.Size = new System.Drawing.Size(47, 21);
            this.lblContratoTitularID.TabIndex = 263;
            // 
            // cmbContratoTitularMunicipio
            // 
            this.cmbContratoTitularMunicipio.DisplayMember = "TB006_Municipio";
            this.cmbContratoTitularMunicipio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContratoTitularMunicipio.Enabled = false;
            this.cmbContratoTitularMunicipio.FormattingEnabled = true;
            this.cmbContratoTitularMunicipio.Location = new System.Drawing.Point(282, 103);
            this.cmbContratoTitularMunicipio.Name = "cmbContratoTitularMunicipio";
            this.cmbContratoTitularMunicipio.Size = new System.Drawing.Size(167, 21);
            this.cmbContratoTitularMunicipio.TabIndex = 201;
            this.cmbContratoTitularMunicipio.ValueMember = "TB006_id";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(279, 87);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 202;
            this.label21.Text = "Município";
            // 
            // grpComprovante
            // 
            this.grpComprovante.Controls.Add(this.rptComprovanteCredito);
            this.grpComprovante.Controls.Add(this.mnuComprovante);
            this.grpComprovante.Location = new System.Drawing.Point(1235, 327);
            this.grpComprovante.Name = "grpComprovante";
            this.grpComprovante.Size = new System.Drawing.Size(397, 436);
            this.grpComprovante.TabIndex = 260;
            this.grpComprovante.TabStop = false;
            this.grpComprovante.Text = "Conprovante";
            this.grpComprovante.Visible = false;
            // 
            // rptComprovanteCredito
            // 
            this.rptComprovanteCredito.DocumentMapWidth = 92;
            reportDataSource1.Name = "DataSetComprovanteCredito";
            reportDataSource1.Value = this.pagamentoComprovanteBindingSource;
            this.rptComprovanteCredito.LocalReport.DataSources.Add(reportDataSource1);
            this.rptComprovanteCredito.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0010.rdlc";
            this.rptComprovanteCredito.Location = new System.Drawing.Point(11, 45);
            this.rptComprovanteCredito.Name = "rptComprovanteCredito";
            this.rptComprovanteCredito.Size = new System.Drawing.Size(374, 385);
            this.rptComprovanteCredito.TabIndex = 133;
            // 
            // mnuComprovante
            // 
            this.mnuComprovante.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuComprovanteFechar});
            this.mnuComprovante.Location = new System.Drawing.Point(3, 16);
            this.mnuComprovante.Name = "mnuComprovante";
            this.mnuComprovante.Size = new System.Drawing.Size(391, 24);
            this.mnuComprovante.TabIndex = 134;
            this.mnuComprovante.Text = "menuStrip1";
            // 
            // mnuComprovanteFechar
            // 
            this.mnuComprovanteFechar.Name = "mnuComprovanteFechar";
            this.mnuComprovanteFechar.Size = new System.Drawing.Size(54, 20);
            this.mnuComprovanteFechar.Text = "Fechar";
            this.mnuComprovanteFechar.Click += new System.EventHandler(this.mnuComprovanteFechar_Click);
            // 
            // grpCicloContrato
            // 
            this.grpCicloContrato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grpCicloContrato.Controls.Add(this.txtcontratoAlterarCiclo);
            this.grpCicloContrato.Controls.Add(this.btnContratoAlterarCicloFechar);
            this.grpCicloContrato.Controls.Add(this.btnContratoAlterarCicloConfirmar);
            this.grpCicloContrato.Location = new System.Drawing.Point(1237, 119);
            this.grpCicloContrato.Name = "grpCicloContrato";
            this.grpCicloContrato.Size = new System.Drawing.Size(210, 100);
            this.grpCicloContrato.TabIndex = 256;
            this.grpCicloContrato.TabStop = false;
            this.grpCicloContrato.Text = "Ciclo do contrato";
            this.grpCicloContrato.Visible = false;
            // 
            // txtcontratoAlterarCiclo
            // 
            this.txtcontratoAlterarCiclo.Location = new System.Drawing.Point(53, 27);
            this.txtcontratoAlterarCiclo.Name = "txtcontratoAlterarCiclo";
            this.txtcontratoAlterarCiclo.Size = new System.Drawing.Size(100, 20);
            this.txtcontratoAlterarCiclo.TabIndex = 7;
            // 
            // btnContratoAlterarCicloFechar
            // 
            this.btnContratoAlterarCicloFechar.Location = new System.Drawing.Point(111, 71);
            this.btnContratoAlterarCicloFechar.Name = "btnContratoAlterarCicloFechar";
            this.btnContratoAlterarCicloFechar.Size = new System.Drawing.Size(86, 23);
            this.btnContratoAlterarCicloFechar.TabIndex = 6;
            this.btnContratoAlterarCicloFechar.Text = "Fechar";
            this.btnContratoAlterarCicloFechar.UseVisualStyleBackColor = true;
            this.btnContratoAlterarCicloFechar.Click += new System.EventHandler(this.btnParcelaAlterarCicloFechar_Click);
            // 
            // btnContratoAlterarCicloConfirmar
            // 
            this.btnContratoAlterarCicloConfirmar.Location = new System.Drawing.Point(10, 71);
            this.btnContratoAlterarCicloConfirmar.Name = "btnContratoAlterarCicloConfirmar";
            this.btnContratoAlterarCicloConfirmar.Size = new System.Drawing.Size(86, 23);
            this.btnContratoAlterarCicloConfirmar.TabIndex = 5;
            this.btnContratoAlterarCicloConfirmar.Text = "Confirmar";
            this.btnContratoAlterarCicloConfirmar.UseVisualStyleBackColor = true;
            this.btnContratoAlterarCicloConfirmar.Click += new System.EventHandler(this.btnParcelaAlterarCicloConfirmar_Click);
            // 
            // gpbParcelaAvulsa
            // 
            this.gpbParcelaAvulsa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gpbParcelaAvulsa.Controls.Add(this.dtParcelaAvulsaNParcelas);
            this.gpbParcelaAvulsa.Controls.Add(this.label86);
            this.gpbParcelaAvulsa.Controls.Add(this.btnParcelaFechar);
            this.gpbParcelaAvulsa.Controls.Add(this.btnParcelaConfirmar);
            this.gpbParcelaAvulsa.Controls.Add(this.dtParcelaAvulsaVencimento);
            this.gpbParcelaAvulsa.Controls.Add(this.label57);
            this.gpbParcelaAvulsa.Location = new System.Drawing.Point(1238, 6);
            this.gpbParcelaAvulsa.Name = "gpbParcelaAvulsa";
            this.gpbParcelaAvulsa.Size = new System.Drawing.Size(209, 107);
            this.gpbParcelaAvulsa.TabIndex = 252;
            this.gpbParcelaAvulsa.TabStop = false;
            this.gpbParcelaAvulsa.Text = "Parcela Avulsa";
            this.gpbParcelaAvulsa.Visible = false;
            // 
            // dtParcelaAvulsaNParcelas
            // 
            this.dtParcelaAvulsaNParcelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dtParcelaAvulsaNParcelas.FormattingEnabled = true;
            this.dtParcelaAvulsaNParcelas.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.dtParcelaAvulsaNParcelas.Location = new System.Drawing.Point(111, 38);
            this.dtParcelaAvulsaNParcelas.Name = "dtParcelaAvulsaNParcelas";
            this.dtParcelaAvulsaNParcelas.Size = new System.Drawing.Size(80, 21);
            this.dtParcelaAvulsaNParcelas.TabIndex = 217;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(108, 23);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(66, 13);
            this.label86.TabIndex = 5;
            this.label86.Text = "N.º Parcelas";
            // 
            // btnParcelaFechar
            // 
            this.btnParcelaFechar.Location = new System.Drawing.Point(110, 69);
            this.btnParcelaFechar.Name = "btnParcelaFechar";
            this.btnParcelaFechar.Size = new System.Drawing.Size(86, 23);
            this.btnParcelaFechar.TabIndex = 4;
            this.btnParcelaFechar.Text = "Fechar";
            this.btnParcelaFechar.UseVisualStyleBackColor = true;
            this.btnParcelaFechar.Click += new System.EventHandler(this.btnParcelaFechar_Click);
            // 
            // btnParcelaConfirmar
            // 
            this.btnParcelaConfirmar.Location = new System.Drawing.Point(9, 69);
            this.btnParcelaConfirmar.Name = "btnParcelaConfirmar";
            this.btnParcelaConfirmar.Size = new System.Drawing.Size(86, 23);
            this.btnParcelaConfirmar.TabIndex = 3;
            this.btnParcelaConfirmar.Text = "Confirmar";
            this.btnParcelaConfirmar.UseVisualStyleBackColor = true;
            this.btnParcelaConfirmar.Click += new System.EventHandler(this.btnParcelaConfirmar_Click);
            // 
            // dtParcelaAvulsaVencimento
            // 
            this.dtParcelaAvulsaVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtParcelaAvulsaVencimento.Location = new System.Drawing.Point(12, 39);
            this.dtParcelaAvulsaVencimento.Name = "dtParcelaAvulsaVencimento";
            this.dtParcelaAvulsaVencimento.Size = new System.Drawing.Size(90, 20);
            this.dtParcelaAvulsaVencimento.TabIndex = 2;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(9, 23);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(93, 13);
            this.label57.TabIndex = 0;
            this.label57.Text = "Vencimento Inicial";
            // 
            // grupCicloParcela
            // 
            this.grupCicloParcela.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupCicloParcela.Controls.Add(this.txtParcelaAlterarCiclo);
            this.grupCicloParcela.Controls.Add(this.btnParcelaAlterarCicloFechar);
            this.grupCicloParcela.Controls.Add(this.btnParcelaAlterarCicloConfirmar);
            this.grupCicloParcela.Location = new System.Drawing.Point(1235, 221);
            this.grupCicloParcela.Name = "grupCicloParcela";
            this.grupCicloParcela.Size = new System.Drawing.Size(210, 100);
            this.grupCicloParcela.TabIndex = 257;
            this.grupCicloParcela.TabStop = false;
            this.grupCicloParcela.Text = "Ciclo da parcela";
            this.grupCicloParcela.Visible = false;
            // 
            // txtParcelaAlterarCiclo
            // 
            this.txtParcelaAlterarCiclo.Location = new System.Drawing.Point(53, 27);
            this.txtParcelaAlterarCiclo.Name = "txtParcelaAlterarCiclo";
            this.txtParcelaAlterarCiclo.Size = new System.Drawing.Size(100, 20);
            this.txtParcelaAlterarCiclo.TabIndex = 7;
            // 
            // btnParcelaAlterarCicloFechar
            // 
            this.btnParcelaAlterarCicloFechar.Location = new System.Drawing.Point(111, 71);
            this.btnParcelaAlterarCicloFechar.Name = "btnParcelaAlterarCicloFechar";
            this.btnParcelaAlterarCicloFechar.Size = new System.Drawing.Size(86, 23);
            this.btnParcelaAlterarCicloFechar.TabIndex = 6;
            this.btnParcelaAlterarCicloFechar.Text = "Fechar";
            this.btnParcelaAlterarCicloFechar.UseVisualStyleBackColor = true;
            this.btnParcelaAlterarCicloFechar.Click += new System.EventHandler(this.btnParcelaAlterarCicloFechar_Click_1);
            // 
            // btnParcelaAlterarCicloConfirmar
            // 
            this.btnParcelaAlterarCicloConfirmar.Location = new System.Drawing.Point(10, 71);
            this.btnParcelaAlterarCicloConfirmar.Name = "btnParcelaAlterarCicloConfirmar";
            this.btnParcelaAlterarCicloConfirmar.Size = new System.Drawing.Size(86, 23);
            this.btnParcelaAlterarCicloConfirmar.TabIndex = 5;
            this.btnParcelaAlterarCicloConfirmar.Text = "Confirmar";
            this.btnParcelaAlterarCicloConfirmar.UseVisualStyleBackColor = true;
            this.btnParcelaAlterarCicloConfirmar.Click += new System.EventHandler(this.btnParcelaAlterarCicloConfirmar_Click_1);
            // 
            // pagamentoComprovanteTableAdapter
            // 
            this.pagamentoComprovanteTableAdapter.ClearBeforeFill = true;
            // 
            // clubeConteza_Relatorios1
            // 
            this.clubeConteza_Relatorios1.DataSetName = "ClubeConteza_Relatorios";
            this.clubeConteza_Relatorios1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TB016_id
            // 
            this.TB016_id.DataPropertyName = "TB016_id";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TB016_id.DefaultCellStyle = dataGridViewCellStyle6;
            this.TB016_id.HeaderText = "Id";
            this.TB016_id.Name = "TB016_id";
            this.TB016_id.ReadOnly = true;
            this.TB016_id.Width = 60;
            // 
            // TB016_NossoNumero
            // 
            this.TB016_NossoNumero.DataPropertyName = "TB016_NossoNumero";
            this.TB016_NossoNumero.HeaderText = "N. Nº";
            this.TB016_NossoNumero.Name = "TB016_NossoNumero";
            this.TB016_NossoNumero.Width = 60;
            // 
            // TB012_CicloContrato
            // 
            this.TB012_CicloContrato.DataPropertyName = "TB012_CicloContrato";
            this.TB012_CicloContrato.HeaderText = "Ciclo";
            this.TB012_CicloContrato.Name = "TB012_CicloContrato";
            this.TB012_CicloContrato.ReadOnly = true;
            this.TB012_CicloContrato.Width = 50;
            // 
            // TB016_Emissao
            // 
            this.TB016_Emissao.DataPropertyName = "TB016_Emissao";
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            this.TB016_Emissao.DefaultCellStyle = dataGridViewCellStyle7;
            this.TB016_Emissao.HeaderText = "Emissão";
            this.TB016_Emissao.Name = "TB016_Emissao";
            this.TB016_Emissao.ReadOnly = true;
            this.TB016_Emissao.Width = 70;
            // 
            // TB016_Vencimento
            // 
            this.TB016_Vencimento.DataPropertyName = "TB016_Vencimento";
            dataGridViewCellStyle8.Format = "d";
            dataGridViewCellStyle8.NullValue = null;
            this.TB016_Vencimento.DefaultCellStyle = dataGridViewCellStyle8;
            this.TB016_Vencimento.HeaderText = "Vencimento";
            this.TB016_Vencimento.Name = "TB016_Vencimento";
            this.TB016_Vencimento.Width = 70;
            // 
            // TB016_Valor
            // 
            this.TB016_Valor.DataPropertyName = "TB016_Valor";
            dataGridViewCellStyle9.Format = "C2";
            dataGridViewCellStyle9.NullValue = "0";
            this.TB016_Valor.DefaultCellStyle = dataGridViewCellStyle9;
            this.TB016_Valor.HeaderText = "Valor";
            this.TB016_Valor.Name = "TB016_Valor";
            this.TB016_Valor.ReadOnly = true;
            this.TB016_Valor.Width = 70;
            // 
            // TB016_DataPagamento
            // 
            this.TB016_DataPagamento.DataPropertyName = "TB016_DataPagamento";
            dataGridViewCellStyle10.Format = "d";
            dataGridViewCellStyle10.NullValue = null;
            this.TB016_DataPagamento.DefaultCellStyle = dataGridViewCellStyle10;
            this.TB016_DataPagamento.HeaderText = "Pagamento";
            this.TB016_DataPagamento.Name = "TB016_DataPagamento";
            this.TB016_DataPagamento.ReadOnly = true;
            this.TB016_DataPagamento.Width = 70;
            // 
            // TB016_ValorPago
            // 
            this.TB016_ValorPago.DataPropertyName = "TB016_ValorPago";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "C2";
            dataGridViewCellStyle11.NullValue = "0";
            this.TB016_ValorPago.DefaultCellStyle = dataGridViewCellStyle11;
            this.TB016_ValorPago.HeaderText = "Pago";
            this.TB016_ValorPago.Name = "TB016_ValorPago";
            this.TB016_ValorPago.ReadOnly = true;
            this.TB016_ValorPago.Width = 70;
            // 
            // ParcelaTB012_Id
            // 
            this.ParcelaTB012_Id.DataPropertyName = "TB012_id";
            dataGridViewCellStyle12.Format = "000000";
            dataGridViewCellStyle12.NullValue = null;
            this.ParcelaTB012_Id.DefaultCellStyle = dataGridViewCellStyle12;
            this.ParcelaTB012_Id.HeaderText = "Contrato";
            this.ParcelaTB012_Id.Name = "ParcelaTB012_Id";
            this.ParcelaTB012_Id.ReadOnly = true;
            this.ParcelaTB012_Id.Visible = false;
            this.ParcelaTB012_Id.Width = 50;
            // 
            // ParcelaTB015_id
            // 
            this.ParcelaTB015_id.DataPropertyName = "TB015_id";
            this.ParcelaTB015_id.HeaderText = "ID Plano";
            this.ParcelaTB015_id.Name = "ParcelaTB015_id";
            this.ParcelaTB015_id.ReadOnly = true;
            this.ParcelaTB015_id.Visible = false;
            this.ParcelaTB015_id.Width = 80;
            // 
            // ParcelaPlano
            // 
            this.ParcelaPlano.DataPropertyName = "TB015_Plano";
            this.ParcelaPlano.HeaderText = "Plano";
            this.ParcelaPlano.Name = "ParcelaPlano";
            this.ParcelaPlano.ReadOnly = true;
            this.ParcelaPlano.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ParcelaPlano.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ParcelaPlano.Visible = false;
            this.ParcelaPlano.Width = 95;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.DataPropertyName = "TB016_FormaPagamentoS";
            this.FormaPagamento.HeaderText = "Forma";
            this.FormaPagamento.Name = "FormaPagamento";
            this.FormaPagamento.ReadOnly = true;
            this.FormaPagamento.Width = 65;
            // 
            // TB016_StatusS
            // 
            this.TB016_StatusS.DataPropertyName = "TB016_StatusS";
            this.TB016_StatusS.HeaderText = "Status";
            this.TB016_StatusS.Name = "TB016_StatusS";
            this.TB016_StatusS.ReadOnly = true;
            this.TB016_StatusS.Width = 60;
            // 
            // Selecionar
            // 
            this.Selecionar.HeaderText = "Sk";
            this.Selecionar.Name = "Selecionar";
            this.Selecionar.Width = 50;
            // 
            // FrmManutencaoParcela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 548);
            this.Controls.Add(this.grpParcelaEnd);
            this.Controls.Add(this.grupCicloParcela);
            this.Controls.Add(this.grpComprovante);
            this.Controls.Add(this.gpbParcelaAvulsa);
            this.Controls.Add(this.grpCicloContrato);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mnuComprovante;
            this.Name = "FrmManutencaoParcela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManutencaoParcela";
            this.Load += new System.EventHandler(this.FrmManutencaoParcela_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pagamentoComprovanteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFechar)).EndInit();
            this.tabPrincipal.ResumeLayout(false);
            this.tbLista.ResumeLayout(false);
            this.tbLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddgContratos)).EndInit();
            this.tbContrato.ResumeLayout(false);
            this.tbContrato.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbParcelaAlterarVencimento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctContratoAlterarDataValidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaAlterarFormaPagamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbFormasPagamento.ResumeLayout(false);
            this.tbBoleto.ResumeLayout(false);
            this.tbBoleto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctPagamentoBoleto)).EndInit();
            this.tbCredito.ResumeLayout(false);
            this.tbCredito.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaPagamentoCredito)).EndInit();
            this.tbDebito.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaPagamentoDebito)).EndInit();
            this.tbEspecie.ResumeLayout(false);
            this.tbEspecie.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaPagamentoEspecie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctParcelaStatusAlterar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctAlterarDadosParcela)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddtParcelaItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddtParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTContratoDependentes)).EndInit();
            this.mnuParcela.ResumeLayout(false);
            this.mnuParcela.PerformLayout();
            this.tbpDocumentos.ResumeLayout(false);
            this.tbpDocumentos.PerformLayout();
            this.gprAnexarTermo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddgDocumentos)).EndInit();
            this.mnuDocumentos.ResumeLayout(false);
            this.mnuDocumentos.PerformLayout();
            this.grpParcelaEnd.ResumeLayout(false);
            this.grpParcelaEnd.PerformLayout();
            this.grpComprovante.ResumeLayout(false);
            this.grpComprovante.PerformLayout();
            this.mnuComprovante.ResumeLayout(false);
            this.mnuComprovante.PerformLayout();
            this.grpCicloContrato.ResumeLayout(false);
            this.grpCicloContrato.PerformLayout();
            this.gpbParcelaAvulsa.ResumeLayout(false);
            this.gpbParcelaAvulsa.PerformLayout();
            this.grupCicloParcela.ResumeLayout(false);
            this.grupCicloParcela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clubeConteza_Relatorios1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pcbFechar;
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tbLista;
        private System.Windows.Forms.TabPage tbContrato;
        private System.Windows.Forms.TextBox txtFiltroAssociado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ddgContratos;
        private System.Windows.Forms.MenuStrip mnuParcela;
        private System.Windows.Forms.Label lblContrato;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtContratoFim;
        private System.Windows.Forms.DateTimePicker dtContratoInicio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbParcelaCiclo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mskContratoTitularCPF;
        private System.Windows.Forms.TextBox txtContratoTitularNomeCompleto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbContratoStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView DTContratoDependentes;
        private System.Windows.Forms.DataGridView ddtParcelas;
        private System.Windows.Forms.DataGridView ddtParcelaItens;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB017_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB017_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB017_ValorUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB017_ValorDesconto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB017_ValorFinal;
        private System.Windows.Forms.Label lblParcelaPlanoId;
        private System.Windows.Forms.PictureBox pctAlterarDadosParcela;
        private System.Windows.Forms.DateTimePicker ddtParcelaVencimento;
        private System.Windows.Forms.ComboBox cmbFormaPagamento;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblParcelaPlano;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label lblParcelaValorTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblParcelaId;
        private System.Windows.Forms.Label lblParcelaProdutoId;
        private System.Windows.Forms.Label lblParcelaProduto;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtParcelaSubTotal;
        private System.Windows.Forms.TextBox txtParcelaDesconto;
        private System.Windows.Forms.TextBox txtParcelaValorUnitario;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.PictureBox pctParcelaStatusAlterar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblParcelaCreditoTaxas;
        private System.Windows.Forms.Label lblParcelaCartaoValorCredito;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblParcelaCartaoDataRecebimentoBanco;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtParcelaCartaoNomeTitular;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox mskParcelaCartaoCPFTitular;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtParcelaCartaoNumero;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabControl tbFormasPagamento;
        private System.Windows.Forms.TabPage tbBoleto;
        private System.Windows.Forms.MaskedTextBox mskParcelaBoletoMulta;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.MaskedTextBox mskParcelaBoletoTarifa;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox mskParcelaBoletoIOF;
        private System.Windows.Forms.TextBox txtParcelaBoletoDocBanco;
        private System.Windows.Forms.TabPage tbCredito;
        private System.Windows.Forms.Label lblParcelaCreditoTipoVencimento;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblCredValorMinimoParcela;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.ComboBox cmbCredParcelas;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.ComboBox cmbCredBandeira;
        private System.Windows.Forms.TabPage tbDebito;
        private System.Windows.Forms.TabPage tbEspecie;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtParcelaCartaoAutorizacao;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtParcelaCartaoCodValidador;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pctParcelaAlterarFormaPagamento;
        private System.Windows.Forms.ComboBox cmbParcelaStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaAnotacoes;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaIncluirProduto;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaCancelarParcela;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaEmitirParcela;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaEmitirBoleto;
        private System.Windows.Forms.PictureBox pctPagamentoBoleto;
        private System.Windows.Forms.PictureBox pctParcelaPagamentoCredito;
        private System.Windows.Forms.PictureBox pctParcelaPagamentoDebito;
        private System.Windows.Forms.PictureBox pctParcelaPagamentoEspecie;
        private System.Windows.Forms.PictureBox pctContratoAlterarDataValidade;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtmCadContrato;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaFechar;
        private System.Windows.Forms.GroupBox gpbParcelaAvulsa;
        private System.Windows.Forms.ComboBox dtParcelaAvulsaNParcelas;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Button btnParcelaFechar;
        private System.Windows.Forms.Button btnParcelaConfirmar;
        private System.Windows.Forms.DateTimePicker dtParcelaAvulsaVencimento;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cmbDiaVencimento;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridViewTextBoxColumn DependenteTB013_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DependenteTB013_NomeExibicao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DependenteTB013_Idade;
        private System.Windows.Forms.DataGridViewTextBoxColumn DependenteTB013_Cartao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DependenteTB013_StatusS;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaAlterarCiclo;
        private System.Windows.Forms.GroupBox grpCicloContrato;
        private System.Windows.Forms.Button btnContratoAlterarCicloFechar;
        private System.Windows.Forms.Button btnContratoAlterarCicloConfirmar;
        private System.Windows.Forms.ToolStripMenuItem alterarCicloToolStripMenuItem;
        private System.Windows.Forms.TextBox txtcontratoAlterarCiclo;
        private System.Windows.Forms.ToolStripMenuItem contratoToolStripMenuItem;
        private System.Windows.Forms.Label lblCicloParcela;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ToolStripMenuItem parcelaToolStripMenuItem;
        private System.Windows.Forms.GroupBox grupCicloParcela;
        private System.Windows.Forms.TextBox txtParcelaAlterarCiclo;
        private System.Windows.Forms.Button btnParcelaAlterarCicloFechar;
        private System.Windows.Forms.Button btnParcelaAlterarCicloConfirmar;
        private System.Windows.Forms.PictureBox ptbParcelaAlterarVencimento;
        private System.Windows.Forms.CheckBox chkSelecionarTudo;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.DateTimePicker ddtParcelaDataPagamento;
        private System.Windows.Forms.CheckBox chkParcelaCancelamento;
        private System.Windows.Forms.GroupBox grpComprovante;
        private System.Windows.Forms.ToolStripMenuItem mnuParcelaReencaminharSICOOB;
        private System.Windows.Forms.MaskedTextBox lblParcelaCartaoValorParcela;
        private Microsoft.Reporting.WinForms.ReportViewer rptComprovanteCredito;
        private System.Windows.Forms.MenuStrip mnuComprovante;
        private System.Windows.Forms.ToolStripMenuItem mnuComprovanteFechar;
        private ClubeConteza_RelatoriosTableAdapters.PagamentoComprovanteTableAdapter pagamentoComprovanteTableAdapter;
        private ClubeConteza_Relatorios clubeConteza_Relatorios;
        private ClubeConteza_Relatorios clubeConteza_Relatorios1;
        private System.Windows.Forms.BindingSource pagamentoComprovanteBindingSource;
        private System.Windows.Forms.Label lblCredUnidade;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label lblCredValorParcela;
        private System.Windows.Forms.Label lblCredValorCredito;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.GroupBox grpParcelaEnd;
        private System.Windows.Forms.Label lblContratoTitularID;
        private System.Windows.Forms.ComboBox cmbContratoTitularMunicipio;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbContratoTitularEstado;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.MaskedTextBox mskContratoTitularCEP;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtContratoTitularLogradouro;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtContratoTitularNumero;
        private System.Windows.Forms.ComboBox cmbTitularPais;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblParcelaPontoDeVenda;
        private System.Windows.Forms.ToolStripMenuItem unirToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_CicloContrato_;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB013_CPFCNPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB013_NomeCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_AlteradoEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB011_NomeExibicao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_StatusS;
        private System.Windows.Forms.ComboBox cmbTipoContrato;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ToolStripMenuItem documentosToolStripMenuItem;
        private System.Windows.Forms.TabPage tbpDocumentos;
        private System.Windows.Forms.GroupBox gprAnexarTermo;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF2;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.DataGridView ddgDocumentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB029_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB029_Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_VSContrato;
        private System.Windows.Forms.MenuStrip mnuDocumentos;
        private System.Windows.Forms.ToolStripMenuItem mnuDocumentosFechar;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_NossoNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB012_CicloContrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_Emissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_Vencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_DataPagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_ValorPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelaTB012_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelaTB015_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelaPlano;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormaPagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TB016_StatusS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selecionar;
    }
}