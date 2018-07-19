using Controller;
using Microsoft.Reporting.WinForms;
using Negocios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmPagamento : Form
    {
        Int64 TB016_id { get; set; }
        Util Validacoes = new Util();
        public frmPagamento(Int64 vTB016_id)
        {
            InitializeComponent();
            TB016_id = vTB016_id;
        }

        private void frmPagamento_Load(object sender, EventArgs e)
        {
            mnuCreditoEmitir.Visible = false;
            mnuCreditoDebito.Visible = false;
            mnuCreditoDinheiro.Visible = false;
            tbPrincipal.TabPages.Remove(tbpDebito);
            tbPrincipal.TabPages.Remove(tbpDinheiro);
            tbPrincipal.TabPages.Remove(tbpCredito);

            CarregarDadosParcela();
   
        }

        private void CarregarDadosParcela()
        {
            try
            {
                ParcelaNegocios     Parcela_N = new ParcelaNegocios();
                ParcelaController   Retorno = Parcela_N.ParcelaPagamento(TB016_id);


                lblCredUnidade.Text                     = Retorno.Empresa.TB001_id.ToString();
                lblCredContrato.Text                    = Retorno.TB012_id.ToString();
                lblCredCPFCNPJ.Text                     = Retorno.TB016_CPFCNPJ;
                mskCredCPFTitularCartao.Text            = Retorno.TB016_CPFCNPJ;
                lblCredNomeCompleto.Text                = Retorno.TB016_Pagador;
                txtCredNomeCompletoTitularCartao.Text   = Retorno.TB016_Pagador;
                lblCredPlano.Text                       = Retorno.Plano.TB015_Plano;
                lblCredParcelaId.Text                   = Retorno.TB016_id.ToString();
                lblCredParcela.Text                     = Retorno.TB016_Parcela.ToString();
                lblCredEmissao.Text                     = Retorno.TB016_Emissao.ToString("dd/MM/yyyy");
                lblCredVencimento.Text                  = Retorno.TB016_Vencimento.ToString("dd/MM/yyyy");
                lblCredValorTotal.Text                  = double.Parse(Retorno.TB016_Valor.ToString()).ToString("C2");

                if (Retorno.TB016_FormaPagamentoS=="6")
                {
                    tbPrincipal.TabPages.Add(tbpCredito);

                    cmbCredBandeira.DataSource              = null;
                    cmbCredBandeira.Items.Clear();
                    cmbCredBandeira.DataSource              = Parcela_N.ListarBandeiraCartao();
                    cmbCredBandeira.DisplayMember           = "TB032_BandeiraCartao";
                    cmbCredBandeira.ValueMember             = "TB032_Id";
                    txtCredNCartao.Focus();

                    mnuCreditoEmitir.Visible = true;
                }
                else
                {
                    if (Retorno.TB016_FormaPagamentoS == "3")
                    {
                        tbPrincipal.TabPages.Add(tbpDebito);
                        lblDebValorParcela.Text = double.Parse(Retorno.TB016_Valor.ToString()).ToString("C2");

                        //cmbDebBanco

                        /**/
                        ParcelaController FormaPagamento = Parcela_N.SelectFormaParamento(3);

                        lblDebDescricaoFormaPagamento.Text = FormaPagamento.TB031_Descricao;
                        lblDebVencimento.Text = FormaPagamento.TB031_TipoVencimento.ToString();
                        DateTime VencimentoCartao = DateTime.Now.AddDays(FormaPagamento.TB031_DVencimento);
                        lblDebVencimentoCartao.Text = VencimentoCartao.ToString("dd/MM/yyyy");
                        lblDebTaxas.Text = double.Parse(FormaPagamento.TB031_Taxa.ToString()).ToString("C2");

                        double ValorCredito = Convert.ToDouble(lblDebValorParcela.Text.ToString().Replace("R$", "")) - Convert.ToDouble(lblDebTaxas.Text.ToString().Replace("R$", ""));


                        lblDebValorCredito.Text = double.Parse(ValorCredito.ToString()).ToString("C2");
                        /**/

                        mnuCreditoDebito.Visible = true;
                        txtCredNCartao.Focus();
                    }
                    else
                    {
                        if (Retorno.TB016_FormaPagamentoS == "2")
                        {
                            mnuCreditoDinheiro.Visible = true;

                            txtCredNCartao.Enabled = false;
                            mskCredCPFTitularCartao.Enabled = false;
                            txtCredNomeCompletoTitularCartao.Enabled = false;

                            tbPrincipal.TabPages.Add(tbpDinheiro);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void cmbCredBandeira_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbCredBandeira.SelectedValue) > 0)
                {
                    cmbCredParcelas.DataSource = null;
                    cmbCredParcelas.Items.Clear();

                    ParcelaNegocios Parcela_N = new ParcelaNegocios();
                    ParcelaController Retorno = Parcela_N.ParcelaPagamento(TB016_id);

                    cmbCredParcelas.DataSource      = Parcela_N.ListaParcelamentoPossivelPorBandeira(Convert.ToInt64(cmbCredBandeira.SelectedValue), Convert.ToInt64(lblCredUnidade.Text));
                    cmbCredParcelas.DisplayMember   = "TB031_NParcelas";
                    cmbCredParcelas.ValueMember     = "TB031_Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCredParcelas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ParcelaNegocios Parcela_N = new ParcelaNegocios();
                ParcelaController Retorno = Parcela_N.SelectFormaParamento(Convert.ToInt64(cmbCredParcelas.SelectedValue));

                lblCredValorMinimoParcela.Text  = double.Parse(Retorno.TB031_ValorMinimoParcela.ToString()).ToString("C2");
                lblCredDescricaoPagamento.Text  = Retorno.TB031_Descricao;
                lblCredTipoVencimento.Text      = Retorno.TB031_TipoVencimento.ToString();
                DateTime VencimentoCartao       = DateTime.Now.AddDays(Retorno.TB031_DVencimento);
                lblCredVencimentoCartao.Text    = VencimentoCartao.ToString("dd/MM/yyyy");
                lblCredTaxas.Text               = double.Parse(Retorno.TB031_Taxa.ToString()).ToString("C2");

                double ValorCredito = Convert.ToDouble(lblCredValorTotal.Text.ToString().Replace("R$", ""))- Convert.ToDouble(lblCredTaxas.Text.ToString().Replace("R$", ""));


                lblCredValorCredito.Text = double.Parse(ValorCredito.ToString()).ToString("C2");


                if (Convert.ToInt16(cmbCredParcelas.Text)>0)
                {
                    double Valor = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""));
                    double Minimo = Convert.ToDouble(lblCredValorMinimoParcela.Text.Replace("R$", "")); 
                    double Resultado = Valor / Convert.ToInt16(cmbCredParcelas.Text);
                    lblCredValorParcela.Text = double.Parse(Resultado.ToString()).ToString("C2"); 
                    if(Resultado < Minimo)
                    {
                        mnuCreditoEmitir.Enabled = false;
                        MessageBox.Show(string.Format(MensagensDoSistema._0063, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                    else
                    {
                        mnuCreditoEmitir.Enabled = true;
                    }
                }
                else
                {
                    lblCredValorMinimoParcela.Text  = "";
                    lblCredValorParcela.Text        = "";
                    lblCredDescricaoPagamento.Text  = "";
                    lblCredVencimentoCartao.Text    = "";
                    MessageBox.Show(string.Format(MensagensDoSistema._0062, "N.º Parcelas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCreditoEmitir_Click(object sender, EventArgs e)
        {
            if(ValidarCredito())
            {
                mnuCreditoEmitir.Enabled = false;
                ParcelaController Pagamento                     = new ParcelaController();
                Pagamento.TB016_id = Convert.ToInt64(lblCredParcelaId.Text);
                Pagamento.TB016_DataPagamento                   = DateTime.Now;
                Pagamento.TB016_FormaProcessamentoBaixa         = 1/*Digitação Atendente*/;
                Pagamento.TB016_CredNCartao                     = txtCredNCartao.Text;
                Pagamento.TB016_CredCPFTitularCartaoCartao      = mskCredCPFTitularCartao.Text;
                Pagamento.TB016_CredNomeTitularCartaoCartao     = txtCredNomeCompletoTitularCartao.Text;
                Pagamento.TB016_CredBandeira                    = Convert.ToInt64(cmbCredBandeira.SelectedValue);
                Pagamento.TB016_CredNParcelas                   = Convert.ToInt16(cmbCredParcelas.SelectedIndex);
                Pagamento.TB016_CredValorParcelas               = Convert.ToDouble(lblCredValorParcela.Text.Replace("R$", ""));
                Pagamento.TB016_CredAutorizacao                 = txtCredAutorizacao.Text;
                Pagamento.TB016_CredCodValidador                = txtCredCodValidador.Text;
                Pagamento.TB012_id                              = Convert.ToInt64(lblCredContrato.Text);
                Pagamento.TB016_CredFormaParamentoId            = Convert.ToInt64(cmbCredParcelas.SelectedValue);
                Pagamento.TB016_CredFormaParamentoDescricao     = lblCredDescricaoPagamento.Text;
                Pagamento.TB016_AlteradoEm                      = DateTime.Now;
                Pagamento.TB016_AlteradoPor                     = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Pagamento.TB016_CredBaixaFeitaEm                = DateTime.Now;
                Pagamento.TB016_CredBaixaFeitaPor               = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Pagamento.TB016_StatusS                         = "5";
                Pagamento.TB016_CredDataCredito                 = Convert.ToDateTime(lblCredVencimentoCartao.Text);
                Pagamento.TB016_ValorPago                       = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""));
                Pagamento.TB016_CredValor                       = Convert.ToDouble(lblCredValorCredito.Text.Replace("R$", ""));

                ParcelaNegocios Pagamento_N = new ParcelaNegocios();
                if(Pagamento_N.ParcelaInserirPagamentoCredParcela(Pagamento, 0))
                {
                    tB016_ParcelaTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                    tB016_ParcelaTableAdapter.Fill(clubeContezaRelatorios.TB016_Parcela, Pagamento.TB016_id);
                    this.rptComprovanteCredito.RefreshReport();

                    ContratoDocNegocios Doc_N = new ContratoDocNegocios();

                    Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        ContratoDocController Documento = new ContratoDocController();

                        Documento.TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        Documento.TB029_TipoS = "3";
                        Documento.TB012_id = Pagamento.TB012_id;

                        Documento.TB029_DocImpressao = this.rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        ContratoDocController Doc = Doc_N.DocImpressaoInserir(Documento);
                }
            }
        }

        bool ValidarCredito()
        {
            if (Convert.ToInt16(cmbCredBandeira.SelectedValue) == 0)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Bandeira"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCredBandeira.Focus();
                return false;
            }

            if (Convert.ToInt16(cmbCredParcelas.SelectedValue) == 0)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "N.º Parcelas"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCredParcelas.Focus();
                return false;
            }


            if (lblCredValorParcela.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Valor Parcela"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCredValorParcela.Focus();
                return false;
            }

           
            
            if (Convert.ToDouble(lblCredValorParcela.Text.Replace("R$", "")) < Convert.ToDouble(lblCredValorMinimoParcela.Text.Replace("R$", "")))
            {
                
                MessageBox.Show(string.Format(MensagensDoSistema._0063, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                return false;
            }


            if (txtCredAutorizacao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Autorização"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredAutorizacao.Focus();
                return false;
            }

            if (txtCredCodValidador.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Cod. Validador"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredCodValidador.Focus();
                return false;
            }

            if (txtCredNCartao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "N.º Cartão"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNCartao.Focus();
                return false;
            }

            if (mskCredCPFTitularCartao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CPF Titular Cartão"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskCredCPFTitularCartao.Focus();
                return false;
            }

            if (txtCredNomeCompletoTitularCartao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Nome Completo Titular Cartão"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNomeCompletoTitularCartao.Focus();
                return false;
            }

            if (!Validacoes.CPF(mskCredCPFTitularCartao.Text))
            {
                MessageBox.Show(MensagensDoSistema._0031.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskCredCPFTitularCartao.Focus();
                return false;
            }

            return true;
        }

        private void mnuCreditoDebito_Click(object sender, EventArgs e)
        {
            if(ValidarDebito())
            {
                mnuCreditoDebito.Enabled = false;
                ParcelaController Pagamento = new ParcelaController();
                Pagamento.TB016_id = Convert.ToInt64(lblCredParcelaId.Text);
                Pagamento.TB016_DataPagamento = DateTime.Now;
                Pagamento.TB016_FormaProcessamentoBaixa = 1/*Digitação Atendente*/;
                Pagamento.TB016_CredNCartao = txtCredNCartao.Text;
                Pagamento.TB016_CredCPFTitularCartaoCartao = mskCredCPFTitularCartao.Text;
                Pagamento.TB016_CredNomeTitularCartaoCartao = txtCredNomeCompletoTitularCartao.Text;
                Pagamento.TB016_CredBandeira = 0;
                Pagamento.TB016_CredNParcelas = 1;
                Pagamento.TB016_CredValorParcelas = Convert.ToDouble(lblDebValorParcela.Text.Replace("R$", ""));
                Pagamento.TB016_CredAutorizacao = txtDebAutorizacao.Text;
                Pagamento.TB016_CredCodValidador = txtDebCodValidador.Text;
                Pagamento.TB012_id = Convert.ToInt64(lblCredContrato.Text);
                Pagamento.TB016_CredFormaParamentoId = 2;
                Pagamento.TB016_CredFormaParamentoDescricao = lblDebDescricaoFormaPagamento.Text;
                Pagamento.TB016_AlteradoEm = DateTime.Now;
                Pagamento.TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Pagamento.TB016_CredBaixaFeitaEm = DateTime.Now;
                Pagamento.TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Pagamento.TB016_StatusS = "5";
                Pagamento.TB016_CredDataCredito = Convert.ToDateTime(lblDebVencimentoCartao.Text);
                Pagamento.TB016_ValorPago = Convert.ToDouble(lblDebValorParcela.Text.Replace("R$", ""));
                Pagamento.TB016_CredValor = Convert.ToDouble(lblDebValorCredito.Text.Replace("R$", ""));

                ParcelaNegocios Pagamento_N = new ParcelaNegocios();
                if (Pagamento_N.ParcelaInserirPagamentoCredParcela(Pagamento,0))
                {
                    tB016_ParcelaTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                    tB016_ParcelaTableAdapter.Fill(clubeContezaRelatorios.TB016_Parcela, Pagamento.TB016_id);
                    this.rptComprovanteCredito.RefreshReport();

                    ContratoDocNegocios Doc_N = new ContratoDocNegocios();

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    ContratoDocController Documento = new ContratoDocController();

                    Documento.TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    Documento.TB029_TipoS = "3";
                    Documento.TB012_id = Pagamento.TB012_id;

                    Documento.TB029_DocImpressao = this.rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                    ContratoDocController Doc = Doc_N.DocImpressaoInserir(Documento);
                }
            }
        }

        bool ValidarDebito()
        {
            if (!Validacoes.CPF(mskCredCPFTitularCartao.Text))
            {
                MessageBox.Show(MensagensDoSistema._0031.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskCredCPFTitularCartao.Focus();
                return false;
            }

            if (txtCredNCartao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "N.º Cartão"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNCartao.Focus();
                return false;
            }


            if (txtCredNomeCompletoTitularCartao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Nome Completo Titular Cartão"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNomeCompletoTitularCartao.Focus();
                return false;
            }


            if (txtDebAutorizacao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Autorização"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDebAutorizacao.Focus();
                return false;
            }

            if (txtDebCodValidador.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Cod. Validador"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDebCodValidador.Focus();
                return false;
            }

            return true;
        }

        private void mnuCreditoDinheiro_Click(object sender, EventArgs e)
        {
            ParcelaController Pagamento = new ParcelaController();
            Pagamento.TB016_id = Convert.ToInt64(lblCredParcelaId.Text);
            Pagamento.TB016_DataPagamento = DateTime.Now;
            Pagamento.TB016_FormaProcessamentoBaixa = 1/*Diritação Atendente*/;
            Pagamento.TB016_CredNCartao = "0";
            Pagamento.TB016_CredCPFTitularCartaoCartao = lblCredCPFCNPJ.Text;
            Pagamento.TB016_CredNomeTitularCartaoCartao = lblCredNomeCompleto.Text;
            Pagamento.TB016_CredBandeira = 0;
            Pagamento.TB016_CredNParcelas = 1;
            Pagamento.TB016_CredValorParcelas = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""));
            Pagamento.TB016_CredAutorizacao = "DINHEIRO";
            Pagamento.TB016_CredCodValidador = "DINHEIRO";
            Pagamento.TB012_id = Convert.ToInt64(lblCredContrato.Text);
            Pagamento.TB016_CredFormaParamentoId = 2;
            Pagamento.TB016_CredFormaParamentoDescricao = "DINHEIRO";
            Pagamento.TB016_AlteradoEm = DateTime.Now;
            Pagamento.TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            Pagamento.TB016_CredBaixaFeitaEm = DateTime.Now;
            Pagamento.TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            Pagamento.TB016_StatusS = "5";
            Pagamento.TB016_CredDataCredito = DateTime.Now;
            Pagamento.TB016_ValorPago = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""));
            Pagamento.TB016_CredValor = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""));

            ParcelaNegocios Pagamento_N = new ParcelaNegocios();
            if (Pagamento_N.ParcelaInserirPagamentoCredParcela(Pagamento,0))
            {
                tB016_ParcelaTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                tB016_ParcelaTableAdapter.Fill(clubeContezaRelatorios.TB016_Parcela, Pagamento.TB016_id);
                this.rptComprovanteCredito.RefreshReport();

                ContratoDocNegocios Doc_N = new ContratoDocNegocios();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                ContratoDocController Documento = new ContratoDocController();

                Documento.TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Documento.TB029_TipoS = "3";
                Documento.TB012_id = Pagamento.TB012_id;

                Documento.TB029_DocImpressao = this.rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                ContratoDocController Doc = Doc_N.DocImpressaoInserir(Documento);
            }
        }

        private void mnuCreditoFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
