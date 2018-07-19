using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.String;

namespace ContezaAdmin.Financeiro
{
    public partial class frmImpArquivoPagamentoBanco : Form
    {

    

        public frmImpArquivoPagamentoBanco()
        {
            InitializeComponent();
        }

        private void frmImpArquivoPagamentoBanco_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0021'. Você pode movê-la ou removê-la conforme necessário.
            this.dTRPT0021TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0021);
            cmbPagMes.SelectedItem = DateTime.Now.Month.ToString();
            cmbPagAno.SelectedItem = DateTime.Now.Year.ToString();
            //DateTime Hoje = new DateTime.


            dtmInicio.Value = Convert.ToDateTime("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            dtmFim.Value = Convert.ToDateTime(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);



            dtInicioLog.Value = Convert.ToDateTime("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            dtFimLog.Value = Convert.ToDateTime(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);

            TratarErrosInicio.Value = DateTime.Now.AddDays(-1);
            TratarErrosFim.Value = DateTime.Now.AddDays(-1);

            CarregarLog();

            tabPrincipal.TabPages.Remove(tbParcelasAbertas);
            tabPrincipal.TabPages.Remove(tbImportacao);
            tabPrincipal.TabPages.Remove(tbLog);
            tabPrincipal.TabPages.Remove(tabLogTratativa);

        }


        private void pctEtapa1_MouseLeave(object sender, EventArgs e)
        {
            gpEtapa1.BackColor = System.Drawing.Color.Transparent;
        }



        private void pctEtapa1_MouseHover(object sender, EventArgs e)
        {
            gpEtapa1.BackColor = System.Drawing.Color.Red;
        }

        private void CarregarInformacoesSICOOB400()
        {
            List<PagamentosController> Pagamentos_L = new List<PagamentosController>();
            try
            {
                Pagamentos_L.Clear();
                dgwImpPagamentos.DataSource = null;
                dgwImpPagamentos.Refresh();

                int counter = 0;
                string line;

                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(txtImpArquivo.Text.TrimEnd());
                while ((line = file.ReadLine()) != null)
                {
                    PagamentosController Paramento_C = new PagamentosController();
                    int Registro = 1;

                    if (line.Contains("RETORNO"))
                    {
                        Registro = 0;
                    }
                    else
                    {
                        if (line.Contains("SICOOB"))
                        {
                            Registro = 0;
                        }
                    }

                    if (Registro == 1)
                    {
                        string Detalhe = line.Replace(" ", "_");
                        Paramento_C.TB025_Tipo = 1;//Confirmar
                        Paramento_C.TB025_ValorAbatimento = 0;//Confirmar

                        var temp                                                                = Detalhe.Length;
                        Paramento_C.TB025_CPFCNPJ                                           = Detalhe.Substring(342, 14);

                        if (Paramento_C.TB025_CPFCNPJ.Contains("29000165"))
                        {
                            var temp2 = Paramento_C.TB025_CPFCNPJ;
                        }
                        string v3                                                               = Detalhe.Substring(110, 6);
                        Paramento_C.TB025_Emissao                                               = DateTime.ParseExact(Detalhe.Substring(110, 6), "ddMMyy", CultureInfo.InvariantCulture);
                        Paramento_C.TB025_Vencimento                                            = DateTime.ParseExact(Detalhe.Substring(146, 6), "ddMMyy", CultureInfo.InvariantCulture);
                        Paramento_C.TB025_DataLiquidacao                                        = DateTime.ParseExact(Detalhe.Substring(175, 6), "ddMMyy", CultureInfo.InvariantCulture);
                        Paramento_C.TB025_DataMovimentacao                                      = DateTime.ParseExact(Detalhe.Substring(175, 6), "ddMMyy", CultureInfo.InvariantCulture);
                        Paramento_C.TB025_DataLiquidacaoCredito                                 = DateTime.ParseExact(Detalhe.Substring(175, 6), "ddMMyy", CultureInfo.InvariantCulture);
                        string vTB016_id                                                        = Detalhe.Substring(116, 9).Replace("_", "").Replace("-", "");
                        if (Detalhe.Substring(116, 9).Replace("_", "").Replace("-", "").Trim()  == string.Empty)
                        {
                            Paramento_C.TB016_id = 0;
                        }
                        else
                        {
                            Paramento_C.TB016_id = Convert.ToInt64(Detalhe.Substring(116, 9).Replace("_", "").Replace("-", ""));//validar  
                        }
                        Paramento_C.TB025_DocumentoBanco = Convert.ToInt64(Detalhe.Substring(68, 6));
                        Paramento_C.TB025_Modalidade = Convert.ToInt16(Detalhe.Substring(14, 2));
                        Paramento_C.TB025_NossoNumero = Convert.ToInt64(Detalhe.Substring(66, 8));

                        if (Paramento_C.TB025_NossoNumero == 1007021)
                        {
                            var NossoNumero = Paramento_C.TB025_NossoNumero;
                        }
                     
                        Paramento_C.TB025_ContaCorrente         = Detalhe.Substring(26, 5);
                        Paramento_C.TB025_BancoRecebedor        = Detalhe.Substring(165, 3);
                        Paramento_C.TB025_AgenciaRecebedora     = Detalhe.Substring(169, 5);
                        Paramento_C.TB025_ValorTitulo           = Convert.ToDouble(Detalhe.Substring(152, 13).TrimStart('0').Insert(Convert.ToInt16(Detalhe.Substring(152, 13).TrimStart('0').Length) - 2, ","));
                        Paramento_C.TB025_ValorIOF              = 0;
                        Paramento_C.TB025_ValorTarifa           = Convert.ToDouble(Detalhe.Substring(184, 4).TrimStart('0').Insert(Convert.ToInt16(Detalhe.Substring(184, 4).TrimStart('0').Length) - 2, ","));
                        Paramento_C.TB025_ValorCobrado          = Convert.ToDouble(Detalhe.Substring(253, 13).TrimStart('0').Insert(Convert.ToInt16(Detalhe.Substring(253, 13).TrimStart('0').Length) - 2, ","));
                        Paramento_C.TB025_CodigoMovimento       = Convert.ToInt16(Detalhe.Substring(108, 2));
                        Paramento_C.TB025_CadastradoEm          = DateTime.Now;
                        Paramento_C.TB025_CadastradoPor         = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        Paramento_C.TB025_AlteradoEm            = DateTime.Now;
                        Paramento_C.TB025_AlteradoPor           = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        Paramento_C.TB025_BancoOrigem           = 756;
                        Paramento_C.TB025_FormaProcessamentoS   = "1";
                        Paramento_C.TB025_FormaPagamentoS       = "1";


                        DateTime InicioAPP = new DateTime(2017, 3, 13);
                        if (Paramento_C.TB025_Emissao >= InicioAPP)
                        {
                            Pagamentos_L.Add(Paramento_C);
                        }
                    }
                    counter++;
                }

                file.Close();
                System.Console.WriteLine("There were {0} lines.", counter);
                // Suspend the screen.  
                System.Console.ReadLine();


                dgwImpPagamentos.DataSource = Pagamentos_L;
                dgwImpPagamentos.Refresh();

                lblImpTotalRegistros.Text = counter.ToString();
                if (Pagamentos_L.Count > 0)
                {
                    CadastrarNoBanco(Pagamentos_L);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarPlanilhaRemessa()
        {
            try
            {
                     OleDbConnection _olecon;
                     OleDbCommand _oleCmdSelect;
                     String _Arquivo            = txtImpArquivo.Text.TrimEnd().TrimStart();
                     String _StringConexao      = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=False';", _Arquivo);
                     
                    _olecon = new OleDbConnection(_StringConexao);
                    _olecon.Open();

                    _oleCmdSelect               = new OleDbCommand();
                    _oleCmdSelect.Connection    = _olecon;
                    _oleCmdSelect.CommandType   = CommandType.Text;

                    _StringConexao              = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=False';", txtImpArquivo.Text.TrimEnd().TrimStart());             
                    _oleCmdSelect.CommandText   = "SELECT Seq,	CPF,	NOME,	VALOR, 	PAGO,	VENCIMENTO,	PAGAMENTO, BANCO	, AGENCIA,	CONTA, Status, Parcela,Contrato FROM [Plan1$] where Status ='aguardando'";

                    OleDbDataReader reader      = _oleCmdSelect.ExecuteReader();

                    List<remessa> remessalist = new List<remessa>();
                    while (reader.Read())
                    {
                        remessa obj     = new remessa();
                        obj.seq         = reader.GetValue(0).ToString();
                        obj.cpf         = reader.GetValue(1).ToString().PadLeft(11,'0');
                        obj.nome        = reader.GetValue(2).ToString();
                        obj.valor       = Convert.ToDouble( reader.GetValue(3).ToString());
                        obj.pago        = Convert.ToDouble(reader.GetValue(4).ToString());
                        obj.vencimento  = Convert.ToDateTime(reader.GetValue(5).ToString());
                        obj.pagamento   = Convert.ToDateTime(reader.GetValue(6).ToString());
                        obj.banco       = reader.GetValue(7).ToString();
                        obj.agencia     = reader.GetValue(8).ToString();
                        obj.conta       = reader.GetValue(9).ToString();
                        obj.status      = reader.GetValue(10).ToString();
                        remessalist.Add(obj);
                    }

                    reader.Close();
                    _olecon.Close();
                    atualizarremessa(remessalist, _StringConexao);

            }
            catch (Exception ex)
            {
            

                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizarremessa(List<remessa> remessalist, String _StringConexao)
        {
            try
            {

                foreach (var item in remessalist)
                {

                    ParcelaController retorno = new ParcelaNegocios().ParceladoArquivoRemessa(item.cpf, Convert.ToDateTime(item.vencimento), ParametrosInterface.objUsuarioLogado.TB011_Id, item.valor , item.pagamento, item.banco,item.agencia,item.conta);

                    OleDbConnection _olecon;
                    OleDbCommand _oleCmdUpdade;
                    String _Arquivo = txtImpArquivo.Text.TrimEnd().TrimStart();
               
                    _olecon = new OleDbConnection(_StringConexao);
                    _olecon.Open();

                    _oleCmdUpdade = new OleDbCommand();
                    _oleCmdUpdade.Connection = _olecon;
                    _oleCmdUpdade.CommandType = CommandType.Text;

                    String _Consulta;

                    _Consulta = "UPDATE [Plan1$] ";
                    _Consulta += "SET [Status] = @Status ";
                    _Consulta += ", [Parcela] = @Parcela ";
                    _Consulta += ", [Contrato] = @Contrato ";
                    _Consulta += "WHERE ";
                    _Consulta += "[Seq] = @seq";


                    _oleCmdUpdade.CommandText = _Consulta;

                    if(retorno.TB012_id>0)
                    {
                        _oleCmdUpdade.Parameters.Add("@Status", OleDbType.VarChar, 255).Value = retorno.TB016_StatusS;
                        _oleCmdUpdade.Parameters.Add("@Parcela", OleDbType.Integer).Value = retorno.TB016_id;
                        _oleCmdUpdade.Parameters.Add("@Contrato", OleDbType.Integer).Value = retorno.TB012_id;
                    }
                    else
                    {
                        _oleCmdUpdade.Parameters.Add("@Status", OleDbType.VarChar, 255).Value = "";
                        _oleCmdUpdade.Parameters.Add("@Parcela", OleDbType.Integer).Value = 0;
                        _oleCmdUpdade.Parameters.Add("@Contrato", OleDbType.Integer).Value = 0;
                    }

                  
                    _oleCmdUpdade.Parameters.Add("@Seq", OleDbType.Integer).Value = item.seq;
                    _oleCmdUpdade.ExecuteNonQuery();
                    _olecon.Close();
                    // lstSexo.Add(new KeyValuePair<string, string>(Sexo.ToString(), ((int)Sexo).ToString()));
                }

                MessageBox.Show("Importação", "Importação da planilha de remessa finalizada.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CadastrarNoBanco(List<PagamentosController> Pagamentos_L)
        {
            try
            {
                PagamentoNegocios Pagamento_N = new PagamentoNegocios();
                Pagamento_N.PagamentoIncluirImporcacao(Pagamentos_L);
                new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);

                MessageBox.Show(@"Importação realizada com sucesso.", @"Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnImpSelimportar.Enabled = true;
                btnImpSelArquivo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnImpSelimportar.Enabled = true;
                btnImpSelArquivo.Enabled = true;
            }

            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
        }

        private void cmbImpBanco_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnImpSelArquivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDlg = new OpenFileDialog();
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                txtImpArquivo.Text = opFilDlg.FileName;
                lblImpTotalRegistros.Text = @"0";
                dgwImpPagamentos.DataSource = null;
                dgwImpPagamentos.Refresh();
                if (txtImpArquivo.Text.Trim() != string.Empty)
                {
                    btnImpSelimportar.Enabled = true;
                }
                else
                {
                    btnImpSelimportar.Enabled = false;
                }
            }
        }

        private void btnImpSelimportar_Click(object sender, EventArgs e)
        {
            if (txtImpArquivo.Text.Trim() != string.Empty)
            {
                btnImpSelimportar.Enabled = false;
                btnImpSelArquivo.Enabled = false;

                if(cmbImpBanco.Text.TrimEnd() == "SICOOB CNAB400")
                {
                    CarregarInformacoesSICOOB400();
                }
                else
                {
                    if (cmbImpBanco.Text.TrimEnd() == "Planilha Remessa")
                    {
                        CarregarPlanilhaRemessa();
                    }
                }
                
            }

            btnImpSelimportar.Enabled = true;
            btnImpSelArquivo.Enabled = true;
        }

        private void mnuImpFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbImportacao);
            tabPrincipal.TabPages.Add(tbPagamentos);

            CarregarLog();
        }

        private void mnuPagImportar_Click(object sender, EventArgs e)
        {

            if (new UsuarioAPPNegocios().VS() != Application.ProductVersion)
            {
                MessageBox.Show(string.Format(MensagensDoSistema._0051, Application.ProductVersion, ParametrosInterface.objUsuarioLogado.VS), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tabPrincipal.TabPages.Remove(tbPagamentos);
            tabPrincipal.TabPages.Add(tbImportacao);
            dgwImpPagamentos.DataSource = null;
            dgwImpPagamentos.Refresh();
            btnImpSelimportar.Enabled = false;
            lblImpTotalRegistros.Text = "";
            txtImpArquivo.Text = "";

        }

        private void ptbFiltrarPagamentos_Click(object sender, EventArgs e)
        {
            CarregarLog();
        }

        private void CarregarLog()
        {
            try
            {
                PagamentoNegocios Pagamento_N = new PagamentoNegocios();

                ddgPagamentos.AutoGenerateColumns = false;
                ddgPagamentos.DataSource = Pagamento_N.PagamentosMesAno(Convert.ToInt16(cmbPagMes.Text), Convert.ToInt16(cmbPagAno.Text));
                ddgPagamentos.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuPagFechar_Click(object sender, EventArgs e)
        {
            Hide();
        }

     

        private void mnuPagMes_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbPagamentos);
            tabPrincipal.TabPages.Add(tbParcelasAbertas);

            Relatorio();
        }

        private void Relatorio()
        {


            dTRPT0019TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            dTRPT0019TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0019,  dtmInicio.Value.ToString(), dtmFim.Value.ToString());
         

            this.rpw0019.RefreshReport();
        }

        private void mnuParcelasFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbParcelasAbertas);
            tabPrincipal.TabPages.Add(tbPagamentos);
        }

        private void mnuParcelasAtualizar_Click(object sender, EventArgs e)
        {
            Relatorio();
        }

        private void CarregarRelatorioLog()
        {
            try
            {

                
                dTRPT0021TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                var sSql = new StringBuilder();

                sSql.Append("SELECT TOP (100) PERCENT dbo.TB025_Pagamentos.TB025_id ");
                sSql.Append(", dbo.TB025_Pagamentos.TB025_DataLiquidacao ");
                sSql.Append(", dbo.TB025_Pagamentos.TB025_NossoNumero ");
                sSql.Append(", dbo.TB016_Parcela.TB016_id ");
                sSql.Append(", dbo.TB025_Pagamentos.TB025_CPFCNPJ ");
                sSql.Append(", dbo.TB016_Parcela.TB012_id ");
                sSql.Append(", dbo.TB016_Parcela.TB016_CPFCNPJ ");
                sSql.Append(", dbo.TB016_Parcela.TB016_Pagador ");
                sSql.Append(", dbo.TB016_Parcela.TB016_Status ");
                sSql.Append(", dbo.TB025_Pagamentos.TB025_DataLiquidacao AS Inicio, ");
                sSql.Append(" dbo.TB025_Pagamentos.TB025_DataLiquidacao AS Fim ");
                sSql.Append("FROM ");
                sSql.Append(" dbo.TB025_Pagamentos ");
                sSql.Append(" LEFT OUTER JOIN ");
                sSql.Append(" dbo.TB016_Parcela ON dbo.TB025_Pagamentos.TB025_NossoNumero = dbo.TB016_Parcela.TB016_NossoNumero ");
                sSql.Append("WHERE ");
                sSql.Append("dbo.TB025_Pagamentos.TB025_DataLiquidacao >= ");
                sSql.Append("'");
                sSql.Append(dtInicioLog.Value.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append("AND ");
                sSql.Append("dbo.TB025_Pagamentos.TB025_DataLiquidacao <= ");
                sSql.Append("'");
                sSql.Append(dtFimLog.Value.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append("and ");
                sSql.Append("dbo.TB016_Parcela.TB012_id is null ");
                sSql.Append("ORDER BY ");
                sSql.Append("dbo.TB016_Parcela.TB016_id ");
                sSql.Append(", dbo.TB025_Pagamentos.TB025_NossoNumero ");

                this.dTRPT0021TableAdapter.Adapter.SelectCommand.CommandText = sSql.ToString();

                try
                {
                    dTRPT0021TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0021);
                }
                catch (Exception)
                {

                }

                rpwRPT0021.RefreshReport();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void logNãoLocalizadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbPagamentos);
            tabPrincipal.TabPages.Add(tbLog);
            CarregarRelatorioLog();
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbLog);
            tabPrincipal.TabPages.Add(tbPagamentos);
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarRelatorioLog();
        }

        class remessa
        {
            public string seq { get; set; }
            public string cpf { get; set; }
            public string nome { get; set; }
            public Double valor { get; set; }
            public Double pago { get; set; }
            public DateTime vencimento { get; set; }
            public DateTime pagamento { get; set; }
            public string agencia { get; set; }
            public string conta { get; set; }
            public string banco { get; set; }
            public string status { get; set; }

        }

        private void mnuTratarErrosAtualizar_Click(object sender, EventArgs e)
        {
            atualizarlogerros();
        }

        private void atualizarlogerros()
        {
            LimparContratoSelecionado();
            pctEtapa1.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox1.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox2.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox3.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox4.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox5.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox6.Image = global::ContezaAdmin.Properties.Resources.Aviso;


            if (new UsuarioAPPNegocios().VS() != Application.ProductVersion)
            {
                MessageBox.Show(
                    Format(MensagensDoSistema._0051, Application.ProductVersion,
                        ParametrosInterface.objUsuarioLogado.VS), @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                //PagamentoNegocios Pagamento_N = new PagamentoNegocios();

                dgvLogErros.AutoGenerateColumns = false;
                dgvLogErros.DataSource =  new PagamentoNegocios().ParcelasListarErrosSICOOB(TratarErrosInicio.Value, TratarErrosFim.Value);
                dgvLogErros.Refresh();
                pctEtapa1.Image = global::ContezaAdmin.Properties.Resources.Feito;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tratarSICOOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbPagamentos);
            tabPrincipal.TabPages.Add(tabLogTratativa);
        }

        private void fecharToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tabLogTratativa);
            tabPrincipal.TabPages.Add(tbPagamentos);
        }

        private void pcbFiltrar_Click(object sender, EventArgs e)
        {
          


            atualizarlogerros();
        }

        private void  LimparContratoSelecionado()
        {
            txtFiltroAssociado.Text = "";
            lblContrato.Text = "";
            ddgContratos.DataSource = null;
            ddgContratos.Refresh();
        }

        private void dgvLogErros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblContrato.Text = "";
            lblTB025_idLog.Text = "";
            pictureBox1.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox2.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox3.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox4.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox5.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox6.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    lblTB025_idLog.Text= dgvLogErros.Rows[e.RowIndex].Cells["TB025_idLog"].Value.ToString();
                    txtFiltroAssociado.Text = dgvLogErros.Rows[e.RowIndex].Cells["TB025_CPFCNPJLog"].Value.ToString();

                    lblTB025_DataLiquidacaoLog.Text = dgvLogErros.Rows[e.RowIndex].Cells["TB025_DataLiquidacaoLog"].Value.ToString();
                    lblTB025_VencimentoLog.Text = dgvLogErros.Rows[e.RowIndex].Cells["TB025_VencimentoLog"].Value.ToString();
                    lblTB025_CPFCNPJLog.Text = dgvLogErros.Rows[e.RowIndex].Cells["TB025_CPFCNPJLog"].Value.ToString();
                    lblTB025_ValorTituloLog.Text = dgvLogErros.Rows[e.RowIndex].Cells["TB025_ValorTituloLog"].Value.ToString();

                    pictureBox2.Image = global::ContezaAdmin.Properties.Resources.Feito;
                    AplicarFiltroContrato();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      
        private void pcbFiltrarContrato_Click(object sender, EventArgs e)
        {
            AplicarFiltroContrato();
        }

        private void AplicarFiltroContrato()
        {

            pictureBox3.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox4.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox5.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox6.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            try
            {
                string tipoCampo;

                if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                {
                    tipoCampo = @"Nome";
                }
                else
                {
                    tipoCampo = txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "")
                                    .Trim().Length > 10 ? @"CPF" : @"Contrato";
                }

                switch (tipoCampo)
                {
                    case @"Nome":
                        {
                            string vQuery = " AND dbo.TB013_Pessoa.TB013_NomeCompleto LIKE '" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                            CarregarContratos(ListarPessoas(vQuery));
                            break;
                        }
                    case @"Contrato":
                        {
                            string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart();
                            CarregarContratos(ListarPessoas(vQuery));
                            break;
                        }
                    case @"CPF":
                        {
                            string vQuery = " AND dbo.TB013_Pessoa.TB013_CPFCNPJ = '" + txtFiltroAssociado.Text
                                                .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                .Replace("-", "").Replace("/", "") + "'";
                            CarregarContratos(ListarPessoas(vQuery));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<PessoaController> ListarPessoas(string query)
        {
            List<PessoaController> pessoaL = new List<PessoaController>();
            PessoaNegocios pessoaN = new PessoaNegocios();

            try
            {
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" TB011_TB002.TB011_Id = ");
                sSql.Append(ParametrosInterface.objUsuarioLogado.TB011_Id);

                pessoaL = pessoaN.pessoaSelect(sSql + query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return pessoaL;
        }

        private void CarregarContratos(List<PessoaController> listarContratos)
        {
            ddgContratos.AutoGenerateColumns = false;

            ddgContratos.DataSource = null;
            ddgContratos.DataSource = listarContratos;
            ddgContratos.Refresh();

            pictureBox3.Image = global::ContezaAdmin.Properties.Resources.Feito;
            //lblTotalRegistrosRetornados.Text = listarContratos.Count.ToString();

            Int64 contrato = 0;
            int contratos = 0;
            for (var i = listarContratos.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt64(listarContratos[i].TB012_Id) == contrato) continue;
                contratos++;
                contrato = Convert.ToInt64(listarContratos[i].TB012_Id);
            }

            //lblTotalContratosRetornados.Text = contratos.ToString();
        }

        private void ddgContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblContrato.Text = "";
            pictureBox4.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox5.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox6.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            try
            {
                //if (e.RowIndex > -1 && e.ColumnIndex > -1)
                //{
                //    lblLogSelecionado.Text = dgvLogErros.Rows[e.RowIndex].Cells["TB025_idLog"].Value.ToString();
                lblContrato.Text = ddgContratos.Rows[e.RowIndex].Cells["TB012_IdLog"].Value.ToString();
                pictureBox4.Image = global::ContezaAdmin.Properties.Resources.Feito;
                CarregarParcelas();
                //    pictureBox2.Image = global::ContezaAdmin.Properties.Resources.Feito;
                //    AplicarFiltroContrato();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarParcelas()
        {
            ddtParcelas.AutoGenerateColumns = false;
            ddtParcelas.DataSource = null;
            //ddtParcelaItens.AutoGenerateColumns = false;
            //ddtParcelaItens.DataSource = null;

            var parcelas =
         new ParcelaNegocios().FamiliarListaTodasContrato(Convert.ToInt64(lblContrato.Text));
            //if (parcelas.Count == 0)
            //{
            //    mnuParcelaGerarParcelas.Enabled = true;
            //}
            //else
            //{
            //    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcelas[parcelas.Count - 1].TB016_StatusS))) == 3)
            //    {
            //        mnuParcelaGerarParcelas.Enabled = true;
            //    }
            //    else
            //    {
            //        mnuParcelaGerarParcelas.Enabled = false;
            //    }

                ddtParcelas.DataSource = parcelas;
                ddtParcelas.Refresh();
            //}
        }

        private void ddtParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                   // cmbFormaPagamento.Enabled = false;
                   // switch (ddtParcelas.Columns[e.ColumnIndex].HeaderText)
                   // {
                     //   case "Id":

                            SelecionarParcela(Convert.ToInt64(ddtParcelas.Rows[e.RowIndex].Cells["TB016_idLog"].Value));
                    //        break;
                   // }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelecionarParcela(long idparcela)
        {
            pictureBox1.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox5.Image = global::ContezaAdmin.Properties.Resources.Aviso;
            pictureBox6.Image = global::ContezaAdmin.Properties.Resources.Aviso;



            txtParcelaValorUnitario.Text = "";
            txtParcelaDesconto.Text = "";
            txtParcelaSubTotal.Text = "";
            lblParcelaProdutoId.Text = "";
            lblParcelaProduto.Text = "";

            var parcela =
                new ParcelaNegocios().ParcelaPesquisaId(idparcela);
            var parcelasProdutos = parcela.ParcelaProduto_L;
            //pctParcelaDescontoConfirmar.Enabled = false;
            txtParcelaDesconto.Enabled = false;
            ddtParcelaVencimento.Enabled = false;

            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
                //pctParcelaDescontoConfirmar.Enabled = true;
                txtParcelaDesconto.Enabled = true;
                ddtParcelaVencimento.Enabled = true;
            }
            lblParcelaPlanoId.Text = parcela.TB015_id.ToString();
            lblParcelaPlano.Text = parcela.TB015_Plano;
            lblParcelaId.Text = parcela.TB016_id.ToString();
            ddtParcelaVencimento.Value = parcela.TB016_Vencimento;
            lblParcelaValorTotal.Text = Format("{0:C2}",
                Convert.ToDouble(parcela.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
            //lblParcelaStatusS.Text = parcela.TB016_StatusS;

            //cmbFormaPagamento.SelectedValue = parcela.TB016_FormaPagamentoS;

            if (Convert.ToInt16(
                    (int)((ParcelaController.TB016_StatusE)Enum.Parse(
                        typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
               // cmbFormaPagamento.Enabled = true;
            }

            lblParcelaProdutoId.Text = "";
            lblParcelaProduto.Text = "";

            if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 1)
            {
                if (parcela.TB016_Boleto.Replace(" ", "") == "---")
                {
                  //  picBoletoVisualizar.Visible = false;
                }
                else
                {
                    //picBoletoVisualizar.Image =
                    //    Properties.Resources.Boletos_30_30;
                   // picBoletoVisualizar.Visible = true;
                }
            }
            else
            {
                //if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 3)
                //{
                //   // picBoletoVisualizar.Image =
                //     //   Properties.Resources.Dinheiro_30_30;
                //   // picBoletoVisualizar.Visible = true;

                //}
                //else
                //{
                //    if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) > 3)
                //    {
                //       // picBoletoVisualizar.Image =
                //        //    Properties.Resources.Cartao_30_30;
                //       // picBoletoVisualizar.Visible = true;
                //    }
                //    else if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 2)
                //    {
                //        picBoletoVisualizar.Image =
                //            Properties.Resources.Dinheiro_30_30;
                //        picBoletoVisualizar.Visible = true;
                //    }
                //}
            }

            //// ReSharper disable once PossibleNullReferenceException
            //ddtParcelaItens.Columns["TB017_ValorUnitario"].DefaultCellStyle.Alignment =
            //    DataGridViewContentAlignment.MiddleRight;
            //// ReSharper disable once PossibleNullReferenceException
            //ddtParcelaItens.Columns["TB017_ValorDesconto"].DefaultCellStyle.Alignment =
            //    DataGridViewContentAlignment.MiddleRight;
            //// ReSharper disable once PossibleNullReferenceException
            //ddtParcelaItens.Columns["TB017_ValorFinal"].DefaultCellStyle.Alignment =
            //    DataGridViewContentAlignment.MiddleRight;

            //ddtParcelaItens.AutoGenerateColumns = false;
            //ddtParcelaItens.DataSource = null;
            //ddtParcelaItens.DataSource = parcelasProdutos;
            //ddtParcelaItens.Refresh();


            pictureBox5.Image = global::ContezaAdmin.Properties.Resources.Feito;
        }
    }
}
