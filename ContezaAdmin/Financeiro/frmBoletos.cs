using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.Financeiro
{
    public partial class frmBoletos : Form
    {
        public frmBoletos()
        {
            InitializeComponent();
        }

        private void frmBoletos_Load(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbImportar);
            tabPrincipal.TabPages.Remove(tbConfirmacaoManual);
            PreencherEstatus();

            cmbConfirmacaoManualFormasPagamento.DataSource = null;
            cmbConfirmacaoManualFormasPagamento.Items.Clear();

            List<KeyValuePair<string, string>> ContratoStatus = new List<KeyValuePair<string, string>>();
            Array Status = Enum.GetValues(typeof(ParcelaController.TB016_FormaPagamentoE));
            foreach (ParcelaController.TB016_FormaPagamentoE Statu in Status)
            {
                ContratoStatus.Add(new KeyValuePair<string, string>(Statu.ToString(), ((int)Statu).ToString()));
            }

            cmbConfirmacaoManualFormasPagamento.DataSource = ContratoStatus;
            cmbConfirmacaoManualFormasPagamento.DisplayMember = "Key";
            cmbConfirmacaoManualFormasPagamento.ValueMember = "Value";

        }

        private void PreencherEstatus()
        {
            cmbBoletoEstatus.DataSource = null;
            cmbBoletoEstatus.Items.Clear();

            List<KeyValuePair<string, string>> ParcelaStatus = new List<KeyValuePair<string, string>>();
            Array Status = Enum.GetValues(typeof(ParcelaController.TB016_StatusE));
            foreach (ParcelaController.TB016_StatusE Stat in Status)
            {
                ParcelaStatus.Add(new KeyValuePair<string, string>(Stat.ToString(), ((int)Stat).ToString()));
            }

            cmbBoletoEstatus.DataSource = ParcelaStatus;
            cmbBoletoEstatus.DisplayMember = "Key";
            cmbBoletoEstatus.ValueMember = "Value";
        }
        private void mnuBoletoImportarArquivo_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbImportar);
            tabPrincipal.TabPages.Remove(tbLista);     
        }

        private void mnuImportarLocalizarArquivo_Click(object sender, EventArgs e)
        {
            txtArquivo.Text = "";
            lblRegistrosNoArquivo.Text = "";
            lblRegistrosProcessados.Text = "";
            lblImportarBoletosEncontrados.Text = "";
            prbRegistros.Maximum = 10;
            prbRegistros.Value = 0;

            OpenFileDialog opFilDlg = new OpenFileDialog();
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                txtArquivo.Text = opFilDlg.FileName;
            }

            switch (txtBanco.Text)
            {
                case "756":
                    //SICOOB
                    SICOOB240Preparar(txtArquivo.Text.TrimEnd());
                    break;
            }
        }  

        private void mnuImportarInicar_Click(object sender, EventArgs e)
        {
            if (txtArquivo.Text.Trim() != string.Empty)
            {
                switch (txtBanco.Text)
                {
                    case "756":
                        //SICOOB
                        SICOOB240ProcessarArquivo(txtArquivo.Text.TrimEnd());
                        break;
                }
            }
            else
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Arquivo"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region SICOOB
            private void SICOOB240Preparar(string PathArquivo)
            {
                TextReader Leitor = new StreamReader(PathArquivo, true);
                int Linhas = 0;
                while (Leitor.Peek() != -1)
                {
                    Linhas++;
                    Leitor.ReadLine();
                }
                
                Leitor.Close();
            //prbRegistros.Maximum = 0;
                prbRegistros.Maximum = Linhas-2;
                lblRegistrosNoArquivo.Text = prbRegistros.Maximum.ToString();
        }

            private void SICOOB240ProcessarArquivo(string PathArquivo)
            {
                List<ParcelaController> Boletos = new List<ParcelaController>();
                int counter = 0;
                string line;

                StreamReader Arquivo =  new StreamReader(PathArquivo);
                while ((line = Arquivo.ReadLine()) != null)
                {

                if(counter>0 & counter< Convert.ToInt16(lblRegistrosNoArquivo.Text)+1)
                {
                    string TB016_id = line.Substring(116, 8).TrimEnd().TrimStart();
                    string TB016_ValorPago = line.Substring(154,11).TrimEnd().TrimStart();
                    
                    TB016_ValorPago = TB016_ValorPago.Insert(TB016_ValorPago.Length - 2, ",").TrimStart('0');

                    string TB016_NossoNumero = line.Substring(62, 12).TrimEnd().TrimStart().TrimStart('0');
                    string TB016_DataPagamento = line.Substring(146, 6).TrimEnd().TrimStart();
                    string Ano = TB016_DataPagamento.Substring(4, 2);
                    string Dia = TB016_DataPagamento.Substring(0, 2);
                    string Mes = TB016_DataPagamento.Substring(2, 2);

                    DateTime Data = new DateTime(Convert.ToInt16("20" + Ano), Convert.ToInt16(Mes), Convert.ToInt16(Dia));
                    ParcelaController Boleto = new ParcelaController();
                    Boleto.TB016_id = Convert.ToInt64(TB016_id);
                    Boleto.TB016_NossoNumero = TB016_NossoNumero;
                    Boleto.TB016_DataPagamento = Data;
                    Boleto.TB016_ValorPago =Convert.ToDouble(TB016_ValorPago);; 

                    Boletos.Add(Boleto);
                    lblRegistrosProcessados.Text = counter.ToString();
                    prbRegistros.Value = counter;
                }               
                counter++;
            }

            Arquivo.Close();

            lblImportarBoletosEncontrados.Text = Boletos.Count.ToString();
            /*Atualizar Boletos*/

            ParcelaNegocios Parcela_N = new ParcelaNegocios();
            int ParcelaInsert = 0;

            foreach (ParcelaController Parcela in Boletos)
            {
                if (Parcela_N.BaixaBoletoImportacao(Parcela, ParametrosInterface.objUsuarioLogado.TB011_Id))
                {
                    ParcelaInsert++;
                }
            }
        }
        #endregion

        private void mnuImportarFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbLista);         
            txtArquivo.Text = "";
            lblRegistrosNoArquivo.Text = "";
            lblRegistrosProcessados.Text = "";
            lblImportarBoletosEncontrados.Text = "";
            prbRegistros.Maximum = 10;
            prbRegistros.Value = 0;
            tabPrincipal.TabPages.Remove(tbImportar);
        }

        private void mnuBoletosFechar_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void mnuBoletosBaixaManual_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbConfirmacaoManual);
            tabPrincipal.TabPages.Remove(tbLista);
        }

        private void mnuBaixaManualFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbLista);
            lblConfirmacaoManualParcela.Text = "";
            lblConfirmacaoManualCPFCNPJ.Text = "";
            lblConfirmacaoManualPagadorID.Text = "";
            lblConfirmacaoManualNomeCompleto.Text = "";
            lblConfirmacaoManualContrato.Text = "";
            lblConfirmacaoManualEmissao.Text = "";
            lblConfirmacaoManualValorParcela.Text = "";
            lblConfirmacaoManualVencimento.Text = "";
            lblConfirmacaoManualStatusPagamento.Text = "";
            mskConfirmacaoManualParcelaValorPago.Text = "R$ 0,00";
            tabPrincipal.TabPages.Remove(tbConfirmacaoManual);
        }

        private void txtBoletoCPFCNPJ_Leave(object sender, EventArgs e)
        {
            string Valor = txtBoletoCPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim();

            if(Valor.Length==11)
            {
                txtBoletoCPFCNPJ.Text = Convert.ToUInt64(Valor).ToString(@"000\.000\.000\-00");
            }
            else
            {
                if (Valor.Length == 14)
                {
                    txtBoletoCPFCNPJ.Text = Convert.ToUInt64(Valor).ToString(@"00\.000\.000\/0000\-00");
                }
                else
                {
                    txtBoletoCPFCNPJ.Text = Valor;
                }
            }
        }

        private void pctBoletoLocalizar_Click(object sender, EventArgs e)
        {
            string vQuery = " WHERE dbo.TB016_Parcela.TB016_Status =  " + Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), cmbBoletoEstatus.SelectedValue.ToString())));

           
            if (txtBoletoCPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() != string.Empty)
            {
                vQuery = vQuery + " and dbo.TB013_Pessoa.TB013_CPFCNPJ = '" + txtBoletoCPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() + "'";
            }

            if (txtBoletoContrato.Text.Trim() != string.Empty)
            {
                vQuery = vQuery + " and dbo.TB016_Parcela.TB012_id = '" + txtBoletoContrato.Text.Trim() + "'";
            }

            if (txtBoletoParcela.Text.Trim() != string.Empty)
            {
                vQuery = vQuery + " and dbo.TB016_Parcela.TB016_id  = " + txtBoletoParcela.Text.Trim();
            }

            ParcelaNegocios Parcela_N = new ParcelaNegocios();

            dtgBoletos.AutoGenerateColumns = false;

            dtgBoletos.DataSource = null;
            dtgBoletos.DataSource = Parcela_N.ParcelasBoletos(vQuery);
            dtgBoletos.Refresh();

        }

        private void dtgBoletos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dtgBoletos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Parcela":
                            ParcelaNegocios Parcela_N = new ParcelaNegocios();
                            ParcelaController Parcela = Parcela_N.ParcelaPesquisaId(Convert.ToInt64(dtgBoletos.Rows[e.RowIndex].Cells["TB016_id"].Value));

                            lblConfirmacaoManualParcela.Text            = Parcela.TB016_id.ToString();
                            lblConfirmacaoManualCPFCNPJ.Text            = Parcela.TB016_CPFCNPJ.ToString();
                            lblConfirmacaoManualNomeCompleto.Text       = Parcela.TB016_Pagador.ToString();
                            lblConfirmacaoManualContrato.Text           = Parcela.TB012_id.ToString();
                            lblConfirmacaoManualEmissao.Text            = Parcela.TB016_Emissao.ToString("dd/MM/yyyy");
                            lblConfirmacaoManualValorParcela.Text       = "R$ " + Parcela.TB016_Valor.ToString("#,##0.00");
                            lblConfirmacaoManualVencimento.Text         = Parcela.TB016_Vencimento.ToString("dd/MM/yyyy");
                            lblConfirmacaoManualStatusPagamento.Text    = Parcela.TB016_StatusS;
                            lblConfirmacaoManualPagadorID.Text          = Parcela.Titular.TB013_id.ToString();


                            tabPrincipal.TabPages.Add(tbConfirmacaoManual);
                            tabPrincipal.TabPages.Remove(tbLista);

                            //FiltrarContrato(Convert.ToInt64(dtgBoletos.Rows[e.RowIndex].Cells["TB016_id"].Value));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuConfirmacaoManualParcelaSalvar_Click(object sender, EventArgs e)
        {
            if(ValidarPagamentoManual())
            {
                try
                {
                    ParcelaController Boleto = new ParcelaController();

                    Boleto.TB016_id = Convert.ToInt64( lblConfirmacaoManualParcela.Text);
                    Boleto.TB016_StatusS = "5";
                    Boleto.TB016_DataPagamento = dtmCadDependenteDataNascimento.Value;
                    Boleto.TB016_ValorPago = Convert.ToDouble(mskConfirmacaoManualParcelaValorPago.Text.Replace("R$",""));

                    ParcelaNegocios Parcela_N = new ParcelaNegocios();

                    if(Parcela_N.BaixaBoletoImportacao(Boleto,ParametrosInterface.objUsuarioLogado.TB011_Id))
                    {
                        PessoaNegocios Pessoa_N = new PessoaNegocios();
                        Pessoa_N.GerarCartoes(Convert.ToInt64(lblConfirmacaoManualPagadorID.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);

                        ContratoNegocios Contrato_N = new ContratoNegocios();
                        Contrato_N.contratoAtivar(Convert.ToInt64(lblConfirmacaoManualContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);


                        MessageBox.Show(MensagensDoSistema._0025.ToString(), "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tabPrincipal.TabPages.Add(tbLista);
                        tabPrincipal.TabPages.Remove(tbConfirmacaoManual);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        Boolean ValidarPagamentoManual()
        {
            if ( Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblConfirmacaoManualStatusPagamento.Text.ToString())))==5)
            {
                MessageBox.Show(MensagensDoSistema._0021.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblConfirmacaoManualStatusPagamento.Text.ToString()))) == 3)
            {
                MessageBox.Show(MensagensDoSistema._0022.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(dtmCadDependenteDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0023.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            double ValorParela = Convert.ToDouble(lblConfirmacaoManualValorParcela.Text.Replace("R$", ""));
            double ValorPago = Convert.ToDouble(mskConfirmacaoManualParcelaValorPago.Text.Replace("R$", ""));
            if (ValorParela > ValorPago)
            {
                MessageBox.Show(MensagensDoSistema._0024.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void mskConfirmacaoManualParcelaValorPago_Leave(object sender, EventArgs e)
        {
            double ValorCampo = Convert.ToDouble(mskConfirmacaoManualParcelaValorPago.Text.Replace("R$", ""));
            mskConfirmacaoManualParcelaValorPago.Text = ValorCampo.ToString("C2");
        }
    }
}
