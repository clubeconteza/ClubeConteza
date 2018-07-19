using ContezaAdmin.Atendimento;
using Controller;
using Microsoft.Reporting.WinForms;
using Negocios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.String;

namespace ContezaAdmin.Financeiro
{
    public partial class FrmManutencaoParcela : Form
    {
        Util _validacoes = new Util();
        public FrmManutencaoParcela()
        {
            InitializeComponent();
        }
        private void FrmManutencaoParcela_Load(object sender, EventArgs e)
        {

            cmbTipoContrato.SelectedIndex = 1;
            //if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 29 select i).Count() < 1)
            //{
            //    unirToolStripMenuItem.Enabled = false;
            //    unirToolStripMenuItem.Visible = false;
            //}


            dtmCadContrato.Value = DateTime.Now;
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Remove(tbpDocumentos);
            
            StatusParcela();
            FormasDePagamento();
            ListarFormasPagamento();
            CarregarPaises();
            StatusDeContrato();


           

        }

        private void StatusDeContrato()
        {
            cmbContratoStatus.DataSource = null;
            cmbContratoStatus.Items.Clear();

            var contratoStatus = new List<KeyValuePair<string, string>>();
            var status = Enum.GetValues(typeof(ContratosController.TB012_StatusE));
            foreach (ContratosController.TB012_StatusE statu in status)
            {
                contratoStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbContratoStatus.DataSource = contratoStatus;
            cmbContratoStatus.DisplayMember = "Key";
            cmbContratoStatus.ValueMember = "Value";
        }
        private void CarregarPaises()
        {
            try
            {
                var enderecoN = new EnderecoNegocios();
                cmbTitularPais.DataSource = null;
                cmbTitularPais.Items.Clear();
                cmbTitularPais.DataSource = enderecoN.PaisController().Tables[0];
                cmbTitularPais.DisplayMember = "TB003_Pais";
                cmbTitularPais.ValueMember = "TB003_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopularEstadosTitular(PaisController filtro)
        {
            EnderecoNegocios enderecoN = new EnderecoNegocios();
            cmbContratoTitularEstado.DataSource = null;
            cmbContratoTitularEstado.Items.Clear();
            try
            {
                cmbContratoTitularEstado.DataSource = enderecoN.EstadosController(filtro).Tables[0];
                cmbContratoTitularEstado.DisplayMember = "TB005_Estado";
                cmbContratoTitularEstado.ValueMember = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void PopularMunicipiosTitular(EstadoController filtro)
        {
            EnderecoNegocios enderecoN = new EnderecoNegocios();
            cmbContratoTitularMunicipio.DataSource = null;
            cmbContratoTitularMunicipio.Items.Clear();
            try
            {
                cmbContratoTitularMunicipio.DataSource = enderecoN.MunicipioController(filtro).Tables[0];
                cmbContratoTitularMunicipio.DisplayMember = "TB006_Municipio";
                cmbContratoTitularMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormasDePagamento()
        {
            cmbFormaPagamento.DataSource = null;
            cmbFormaPagamento.Items.Clear();

            var contratoStatus2 = new List<KeyValuePair<string, string>>();
            var status2 = Enum.GetValues(typeof(ParcelaController.TB016_FormaPagamentoE));
            foreach (ParcelaController.TB016_FormaPagamentoE statu2 in status2)
            {
                contratoStatus2.Add(new KeyValuePair<string, string>(statu2.ToString(), ((int)statu2).ToString()));
            }

            cmbFormaPagamento.DataSource = contratoStatus2;
            cmbFormaPagamento.DisplayMember = "Key";
            cmbFormaPagamento.ValueMember = "Value";
        }
        private void StatusParcela()
        {
            cmbParcelaStatus.DataSource = null;
            cmbParcelaStatus.Items.Clear();

            var parcelaStatus = new List<KeyValuePair<string, string>>();
            var status = Enum.GetValues(typeof(ParcelaController.TB016_StatusE));
            foreach (ParcelaController.TB016_StatusE statu in status)
            {
                parcelaStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbParcelaStatus.DataSource = parcelaStatus;
            cmbParcelaStatus.DisplayMember = "Key";
            cmbParcelaStatus.ValueMember = "Value";
        }

        private void ListarFormasPagamento()
        {
            cmbCredBandeira.DataSource = new ParcelaNegocios().ListarBandeiraCartao();
            cmbCredBandeira.DisplayMember = "TB032_BandeiraCartao";
            cmbCredBandeira.ValueMember = "TB032_Id";
        }
        private void dtmCadContrato_ValueChanged(object sender, EventArgs e)
        {
            var dt1 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 00:00:00";
            var dt2 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 23:59:59";


            switch (cmbTipoContrato.SelectedIndex)
            {
                case 0:
                    {
                        CarregarContratos(ListarPessoas(" TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'"));
                        break;
                    }
                case 1:
                    {
                        CarregarContratos(ListarPessoas(" TB012_TipoContrato = 1 and TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'"));
                        break;
                    }
                case 2:
                    {
                        CarregarContratos(ListarPessoas(" TB012_TipoContrato = 2 and TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'"));
                        break;
                    }

                case 3:
                    {
                        CarregarContratos(ListarPessoas(" TB012_TipoContrato = 3 and TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'"));
                        break;
                    }

                case 4:
                    {
                        CarregarContratos(ListarPessoas(" TB012_TipoContrato = 4 and TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'"));
                        break;
                    }

                case 5:
                    {
                        CarregarContratos(ListarPessoas(" TB012_TipoContrato = 5 and TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'"));
                        break;
                    }
            }
            
        }
        private void CarregarContratos(List<ContratosController> listarContratos)
        {
            ddgContratos.AutoGenerateColumns = false;
            ddgContratos.DataSource = null;
            ddgContratos.Refresh();
            ddgContratos.DataSource = listarContratos;
            ddgContratos.Refresh();
        }
        public List<ContratosController> ListarPessoas(string query)
        {
            var contratosL = new List<ContratosController>();
            try
            {
                contratosL = new ContratoNegocios().ContratosFinanceiro(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return contratosL;
        }
        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void txtFiltroAssociado_Leave(object sender, EventArgs e)
        {
            Filtro();
        }
        private void ddgContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                if (ddgContratos.Columns[e.ColumnIndex].HeaderText != @"Contrato") return;
                tabPrincipal.TabPages.Add(tbContrato);
                tabPrincipal.TabPages.Remove(tbLista);
                FiltrarContrato(Convert.ToInt64(ddgContratos.Rows[e.RowIndex].Cells["TB012_Id"].Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuParcelaFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Add(tbLista);
        }
        protected void FiltrarContrato(long vId)
        {
            try
            {
                var retorno = new ContratoNegocios().contratoSelect(vId);
                mskContratoTitularCPF.Enabled = false;
                lblContrato.Text = retorno.TB012_Id.ToString().PadLeft(6, '0');
                dtContratoInicio.Value = retorno.TB012_Inicio;
                dtContratoFim.Value = retorno.TB012_Fim;
                lblParcelaPontoDeVenda.Text = retorno.PontoDeVenda.TB002_id.ToString();
                txtContratoTitularNomeCompleto.Text = retorno.Titular.TB013_NomeCompleto;
                lblCredUnidade.Text = @"1";
                cmbDiaVencimento.SelectedItem = retorno.TB012_DiaVencimento.ToString();
                lblContratoTitularID.Text = retorno.Titular.TB013_id.ToString();
                mskContratoTitularCPF.Text = retorno.Titular.TB013_CPFCNPJ;
                mskContratoTitularCEP.Text = retorno.Titular.TB004_Cep;
                txtContratoTitularLogradouro.Text = retorno.Titular.TB013_Logradouro;
                txtContratoTitularNumero.Text = retorno.Titular.TB013_Numero;
                cmbContratoStatus.SelectedValue = retorno.TB012_StatusS;

                cmbTitularPais.SelectedValue = retorno.Titular.Municipio.Estado.Pais.TB003_id;
                PaisController pais = new PaisController();
                pais.TB003_id = retorno.Titular.Municipio.Estado.Pais.TB003_id;
                PopularEstadosTitular(pais);
                cmbContratoTitularEstado.SelectedValue = retorno.Titular.Municipio.Estado.TB005_Id;
                var municipio = new EstadoController { TB005_Id = retorno.Titular.Municipio.Estado.TB005_Id };
                PopularMunicipiosTitular(municipio);
                cmbContratoTitularMunicipio.SelectedValue = retorno.Titular.Municipio.TB006_id;
                DTContratoDependentes.AutoGenerateColumns = false;
                DTContratoDependentes.DataSource = null;
                DTContratoDependentes.DataSource = retorno.Dependentes;
                DTContratoDependentes.Refresh();
                cmbContratoStatus.SelectedValue = retorno.TB012_StatusS;
                DTContratoDependentes.AutoGenerateColumns = false;
                DTContratoDependentes.DataSource = null;
                DTContratoDependentes.DataSource = retorno.Dependentes;
                DTContratoDependentes.Refresh();
              
                CarregarCiclos(vId);
                if(cmbParcelaCiclo.Items.Count==0)
                { 
                cmbParcelaCiclo.Items.Add(Convert.ToInt64(retorno.TB012_CicloContrato));
                cmbParcelaCiclo.SelectedItem = 0;
                }
                CarregarParcelas();
            }
            catch (Exception ex)
            {
                tabPrincipal.TabPages.Remove(tbContrato);
                tabPrincipal.TabPages.Add(tbLista);
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpar()
        {
            tbFormasPagamento.Visible = false;

        }
        protected void CarregarParcelas()
        {

            ddtParcelas.AutoGenerateColumns = false;
            ddtParcelas.DataSource = null;
            ddtParcelaItens.AutoGenerateColumns = false;
            ddtParcelaItens.DataSource = null;


            if (cmbParcelaCiclo.Text.Trim() != Empty)
            {
                var parcelas =
                new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                    Convert.ToInt64(cmbParcelaCiclo.Text), -1);
                Limpar();

                if (parcelas.Count > 0)
                {
                    ddtParcelas.DataSource = parcelas;
                    ddtParcelas.Refresh();
                }

            }


          
           
        }
        private void cmbParcelaCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarParcelas();
        }
        private void ddtParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                //  cmbFormaPagamento.Enabled = false;
                if (ddtParcelas.Columns[e.ColumnIndex].HeaderText == @"Id")
                    SelecionarParcela(Convert.ToInt64(ddtParcelas.Rows[e.RowIndex].Cells["TB016_id"].Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SelecionarParcela(long idparcela)
        {
            txtParcelaValorUnitario.Text = "";
            txtParcelaDesconto.Text = "";
            txtParcelaSubTotal.Text = "";
            lblParcelaProdutoId.Text = "";
            lblParcelaProduto.Text = "";

            var parcela =
                new ParcelaNegocios().ParcelaPesquisaId(idparcela);
            var parcelasProdutos = parcela.ParcelaProduto_L;
            
            txtParcelaDesconto.Enabled = false;
        

            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
      
                txtParcelaDesconto.Enabled = true;
   
            }
            lblParcelaPlanoId.Text = parcela.TB015_id.ToString();
            
            if (parcela.TB016_DataPagamento.Year==1900)
            {
                parcela.TB016_DataPagamento = DateTime.Now;
            }
            ddtParcelaDataPagamento.Value = parcela.TB016_DataPagamento;
            lblParcelaPlano.Text = parcela.TB015_Plano;
            lblParcelaId.Text = parcela.TB016_id.ToString();
            ddtParcelaVencimento.Value = parcela.TB016_Vencimento;
            cmbDiaVencimento.SelectedValue = parcela.TB016_DiaVencimento;
            lblCicloParcela.Text = parcela.TB012_CicloContrato.ToString();
            txtParcelaBoletoDocBanco.Text = parcela.TB016_NossoNumero;
            lblParcelaValorTotal.Text = Format("{0:C2}",
                Convert.ToDouble(parcela.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));

            cmbParcelaStatus.SelectedValue =
                Convert.ToInt16(
                    (int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE),
                        parcela.TB016_StatusS))).ToString();



            cmbFormaPagamento.SelectedValue = parcela.TB016_FormaPagamentoS;

            tbFormasPagamento.TabPages.Remove(tbBoleto);
            tbFormasPagamento.TabPages.Remove(tbCredito);
            tbFormasPagamento.TabPages.Remove(tbDebito);
            tbFormasPagamento.TabPages.Remove(tbEspecie);

            if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 1)
            {
                tbFormasPagamento.TabPages.Add(tbBoleto);
                txtParcelaCartaoNumero.Text = @"0";
                mskParcelaCartaoCPFTitular.Text = mskContratoTitularCPF.Text;
                txtParcelaCartaoAutorizacao.Text = @"0";
                txtParcelaCartaoCodValidador.Text = @"0";
                lblParcelaCreditoTaxas.Text = @"0";
                txtParcelaCartaoNomeTitular.Text = txtContratoTitularNomeCompleto.Text;
                lblParcelaCartaoValorParcela.Text = lblParcelaValorTotal.Text;
                lblParcelaCartaoDataRecebimentoBanco.Text = ddtParcelaVencimento.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 2)
                {
                    tbFormasPagamento.TabPages.Add(tbEspecie);
                    txtParcelaCartaoNomeTitular.Text = txtContratoTitularNomeCompleto.Text;
                    mskParcelaCartaoCPFTitular.Text = mskContratoTitularCPF.Text;
                    txtParcelaCartaoAutorizacao.Text = @"0";
                    txtParcelaCartaoCodValidador.Text = @"0";
                    lblParcelaCreditoTaxas.Text = @"0";
                    lblParcelaCartaoValorCredito.Text = txtParcelaSubTotal.Text;
                    lblParcelaCartaoDataRecebimentoBanco.Text = @"0";

                }
                else
                {
                    if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 3)
                    {
                        tbFormasPagamento.TabPages.Add(tbDebito);
                        lblParcelaCartaoValorCredito.Text = lblParcelaValorTotal.Text;
                        lblParcelaCartaoValorParcela.Text = lblParcelaValorTotal.Text;
                    }
                    else
                    {
                        if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 4)
                        {
                            tbFormasPagamento.TabPages.Add(tbCredito);
                        }
                        else
                        {
                            if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 5)
                            {
                                tbFormasPagamento.TabPages.Add(tbCredito);
                            }
                            else
                            {
                                if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 6)
                                {
                                    tbFormasPagamento.TabPages.Add(tbCredito);
                                }
                            }
                        }
                    }
                }
            }


            tbFormasPagamento.Visible = true;
            //Limpar();
            //Boleto = 1,
            //Dinheiro = 2,
            //Debito = 3,
            //Cartao_2x = 4,
            //Cartao_3x = 5

            if (Convert.ToInt16(
                    (int)((ParcelaController.TB016_StatusE)Enum.Parse(
                        typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
        
            }

            lblParcelaProdutoId.Text = "";
            lblParcelaProduto.Text = "";




            ddtParcelaItens.Columns["TB017_ValorUnitario"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
    
            ddtParcelaItens.Columns["TB017_ValorDesconto"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
    
            ddtParcelaItens.Columns["TB017_ValorFinal"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            ddtParcelaItens.AutoGenerateColumns = false;
            ddtParcelaItens.DataSource = null;
            ddtParcelaItens.DataSource = parcelasProdutos;
            ddtParcelaItens.Refresh();
        }
        private void pctParcelaStatusAlterar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(lblContrato.Text) == 5)
            {
                MessageBox.Show(MensagensDoSistema._0081, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(@"Deseja altera status da parcela?", @"Parcelas", MessageBoxButtons.YesNo) !=
                DialogResult.Yes) return;
            /**/
            for (var y = 0; y < ddtParcelas.RowCount; y++)
            {
                if (!Convert.ToBoolean(ddtParcelas.Rows[y].Cells["Selecionar"].Value)) continue;
                var parcela = ddtParcelas.Rows[y].Cells["TB016_id"].Value;
                new ParcelaNegocios().ParcelaAlterarStauts(
                    Convert.ToInt64(parcela),
                    ParametrosInterface.objUsuarioLogado.TB011_Id,
                    Convert.ToInt16(cmbParcelaStatus.SelectedValue),
                    Convert.ToInt64(lblContrato.Text));

                ddtParcelas.Rows[y].Cells["Selecionar"].Value = false;
            }






            CarregarCiclos(Convert.ToInt64(lblContrato.Text));
            chkSelecionarTudo.Checked = false;
            /**/

            FiltrarContrato(Convert.ToInt64(lblContrato.Text));


        }
        private void mnuParcelaAnotacoes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmContratoAnotacoes>().Count() > 0)
            {
                Application.OpenForms.OfType<FrmContratoAnotacoes>().First().Focus();
            }
            else
            {
                var anotacoes = new FrmContratoAnotacoes(Convert.ToInt64(lblContrato.Text));
                anotacoes.Show();
            }
        }
        private void ddtParcelas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                int value;
                if (e.Value == null || !int.TryParse(e.Value.ToString(), out value)) return;
                string formaPagamento = ddtParcelas.Rows[e.RowIndex].Cells["FormaPagamento"].Value.ToString();
                if (formaPagamento.Where(char.IsNumber).Any())
                {
                    ddtParcelas.Rows[e.RowIndex].Cells["FormaPagamento"].Value = Enum.GetName(
                        typeof(ParcelaController.TB016_FormaPagamentoE), Convert.ToInt16(formaPagamento));

                }
                if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                        typeof(ParcelaController.TB016_StatusE),
                        ddtParcelas.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) == 2)
                {
                    ddtParcelas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                            typeof(ParcelaController.TB016_StatusE),
                            ddtParcelas.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) == 4)
                    {
                        ddtParcelas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                                typeof(ParcelaController.TB016_StatusE),
                                ddtParcelas.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) == 5)
                        {
                            ddtParcelas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        }
                        else
                        {
                            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                                    typeof(ParcelaController.TB016_StatusE),
                                    ddtParcelas.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) ==
                                3)
                            {
                                ddtParcelas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuParcelaCancelarParcela_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(@"Deseja cancelar a parcela selecionada?", @"Parcelas", MessageBoxButtons.YesNo) ==
                DialogResult.No)
                {
                    return;
                }

                if (lblParcelaId.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", " Parcela"), @"Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var parcela1 = new ParcelaNegocios().ParcelaPesquisaId(Convert.ToInt64(lblParcelaId.Text));

                if (Convert.ToInt16(
                        (int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE),
                            parcela1.TB016_StatusS))) == 3)

                {
                    MessageBox.Show(MensagensDoSistema._0022, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToInt16(
                        (int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE),
                            parcela1.TB016_StatusS))) == 5)

                {
                    MessageBox.Show(MensagensDoSistema._0021, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!new ParcelaNegocios().ParcelaCancela(Convert.ToInt64(lblParcelaId.Text),
                    ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text))) return;
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ddtParcelas.AutoGenerateColumns = false;
                ddtParcelas.DataSource = null;
                ddtParcelaItens.AutoGenerateColumns = false;
                ddtParcelaItens.DataSource = null;
                var ciclo = Convert.ToInt32(cmbParcelaCiclo.Text);
                FiltrarContrato(Convert.ToInt64(lblContrato.Text));
                cmbParcelaCiclo.SelectedItem = ciclo;
                CarregarParcelas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuParcelaEmitirParcela_Click(object sender, EventArgs e)
        {
            gpbParcelaAvulsa.Location = new Point(632, 77);
            dtParcelaAvulsaNParcelas.SelectedIndex = 0;
            gpbParcelaAvulsa.Visible = true;
        }
        private void btnParcelaConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ParcelaNegocios().ParcelaIdVencimento(Convert.ToInt64(lblContrato.Text),
                        Convert.ToDateTime(dtParcelaAvulsaVencimento.Value)) > 0)
                {
                    MessageBox.Show(MensagensDoSistema._0073, @"Retorno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dtParcelaAvulsaVencimento.Value <= DateTime.Now)
                {
                    MessageBox.Show(MensagensDoSistema._0076, @"Vencimento", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

                

                var parcelas = new ParcelaNegocios().GerarParcelasFamiliar(
                                                                            Convert.ToInt16(dtParcelaAvulsaNParcelas.Text),
                                                                            dtParcelaAvulsaVencimento.Value,
                                                                            Convert.ToInt16(cmbDiaVencimento.Text),
                                                                            ParametrosInterface.objUsuarioLogado.TB011_Id,
                                                                            Convert.ToInt64(lblContrato.Text),
                                                                            Convert.ToInt32(cmbParcelaCiclo.Text),
                                                                            -1,
                                                                            0,
                                                                            ParametrosInterface.objUsuarioLogado.TB037_Id,
                                                                            0
                                                                            );

                if (!new ParcelaNegocios().FamiliarParcelaInsert(parcelas))
                    gpbParcelaAvulsa.Visible = false;
                gpbParcelaAvulsa.Visible = false;
                var ciclo = Convert.ToInt32(cmbParcelaCiclo.Text);
                cmbParcelaCiclo.SelectedItem = ciclo;
                CarregarParcelas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void alterarCicloToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void CarregarCiclos(long contrato)
        {
            cmbParcelaCiclo.Items.Clear();
            var ciclo = new ParcelaNegocios().ListarCiclosAtivosContrato(contrato);
            if (ciclo.Count > 0)
            {
                foreach (var t in ciclo)
                {
                    cmbParcelaCiclo.Items.Add(Convert.ToInt64(t.TB012_CicloContrato));
                }


                var list = new List<object>();
                foreach (var o in cmbParcelaCiclo.Items)
                {
                    if (!list.Contains(o))
                    {
                        list.Add(o);
                    }
                }
                cmbParcelaCiclo.Items.Clear();
                cmbParcelaCiclo.Items.AddRange(list.ToArray());


                if (cmbParcelaCiclo.Items.Count > 1)
                {
                    cmbParcelaCiclo.SelectedIndex = cmbParcelaCiclo.Items.Count - 1;
                }
                else
                {
                    cmbParcelaCiclo.SelectedIndex = 0;
                }


            }

          

           

        }
        private void btnParcelaAlterarCicloConfirmar_Click(object sender, EventArgs e)
        {
            if (txtcontratoAlterarCiclo.Text.Trim().Length > 4)
            {
                try
                {
                    if (!new ContratoNegocios().CicloContratoAlterar(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(txtcontratoAlterarCiclo.Text.Replace("/", "")), ParametrosInterface.objUsuarioLogado.TB011_Id))

                        CarregarParcelas();
                    grpCicloContrato.Visible = false;
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Format(MensagensDoSistema._0082, "Ciclo", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error));
                txtcontratoAlterarCiclo.Focus();
            }
        }
        private void contratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grpCicloContrato.Location = new Point(563, 90);
            grpCicloContrato.Visible = true;
        }
        private void btnParcelaAlterarCicloFechar_Click(object sender, EventArgs e)
        {
            grpCicloContrato.Visible = false;
        }
        private void parcelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grupCicloParcela.Location = new Point(637, 271);
            grupCicloParcela.Visible = true;

        }
        private void btnParcelaAlterarCicloFechar_Click_1(object sender, EventArgs e)
        {
            grupCicloParcela.Visible = false;
        }
        private void btnParcelaAlterarCicloConfirmar_Click_1(object sender, EventArgs e)
        {
            if (txtParcelaAlterarCiclo.Text.Trim().Length >= 5)
            {
                try
                {
                    for (int y = 0; y < ddtParcelas.RowCount; y++)
                    {
                        if (Convert.ToBoolean(ddtParcelas.Rows[y].Cells["Selecionar"].Value))
                        {
                            var parcela = ddtParcelas.Rows[y].Cells["TB016_id"].Value;
                            if (!new ParcelaNegocios().ParcelasAlterarCiclo(Convert.ToInt64(parcela), Convert.ToInt32(txtParcelaAlterarCiclo.Text.Replace("/", "")), ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text))) return;
                            ddtParcelas.Rows[y].Cells["Selecionar"].Value = false;
                        }
                    }
                    CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                    CarregarParcelas();
                    grupCicloParcela.Visible = false;
                    chkSelecionarTudo.Checked = false;
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Format(MensagensDoSistema._0082, "Ciclo", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error));
                txtParcelaAlterarCiclo.Focus();
            }
        }
        private void pctContratoAlterarDataValidade_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(lblContrato.Text) == 5)
            {
                MessageBox.Show(MensagensDoSistema._0081, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(@"Deseja altera data inicio e fim do contrato?", @"Contrato", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (new ContratoNegocios().ContratoAlterarInicioFim(Convert.ToInt64(lblContrato.Text), dtContratoInicio.Value, dtContratoFim.Value, ParametrosInterface.objUsuarioLogado.TB011_Id))
                {
                    //SelecionarParcela(Convert.ToInt64(lblContrato.Text));
                    FiltrarContrato(Convert.ToInt64(lblContrato.Text));
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        private void ptbParcelaAlterarVencimento_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(lblContrato.Text) == 5)
            {
                MessageBox.Show(MensagensDoSistema._0081, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(@"Deseja o vencimento da parcela?", @"Parcelas", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (new ParcelaNegocios().ParcelaAlterarVencimento(Convert.ToInt64(lblParcelaId.Text), ParametrosInterface.objUsuarioLogado.TB011_Id, ddtParcelaVencimento.Value, Convert.ToInt64(lblContrato.Text)))
                {
                    //SelecionarParcela(Convert.ToInt64(lblContrato.Text));
                    var ciclo = cmbParcelaCiclo.Text;
                    FiltrarContrato(Convert.ToInt64(lblContrato.Text));
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    cmbParcelaCiclo.Text = ciclo;
                }
            }
        }
        private void chkSelecionarTudo_Click(object sender, EventArgs e)
        {

        }
        private void chkSelecionarTudo_CheckedChanged(object sender, EventArgs e)
        {
            for (int y = 0; y < ddtParcelas.RowCount; y++)
            {
                ddtParcelas.Rows[y].Cells["Selecionar"].Value = chkSelecionarTudo.Checked;
            }
        }
        bool ValidarBaixa()
        {
            if (mskParcelaCartaoCPFTitular.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CPF Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskParcelaCartaoCPFTitular.Focus();
                return false;
            }

            if (txtParcelaCartaoNomeTitular.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoNomeTitular.Focus();
                return false;
            }

            if (txtParcelaBoletoDocBanco.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                txtParcelaBoletoDocBanco.Text = @"R$ 0,00";
            }

            if (mskParcelaBoletoIOF.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                mskParcelaBoletoIOF.Text = @"R$ 0,00";
            }

            if (mskParcelaBoletoTarifa.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                mskParcelaBoletoTarifa.Text = @"R$ 0,00";
            }

            if (mskParcelaBoletoMulta.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                mskParcelaBoletoMulta.Text = @"R$ 0,00";
            }

            if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) <= 2) return true;
            if (txtParcelaCartaoNumero.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoNumero.Focus();
                return false;
            }

            if (txtParcelaCartaoAutorizacao.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "")
                    .Trim() != Empty) return true;
            MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtParcelaCartaoAutorizacao.Focus();
            return false;
        }
        private void pctParcelaAlterarFormaPagamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), cmbParcelaStatus.Text))) > 1)
                {
                    MessageBox.Show(MensagensDoSistema._0080, @"Erro", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show(@"Deseja alterar forma de pagamento?", @"Parcelas", MessageBoxButtons.YesNo) ==
                    DialogResult.No)
                {
                    return;
                }

                if (new ParcelaNegocios().AlterarFormaPagamento(Convert.ToInt64(lblParcelaId.Text),
                    Convert.ToInt16(cmbFormaPagamento.SelectedValue), ParametrosInterface.objUsuarioLogado.TB011_Id,
                    Convert.ToInt64(lblContrato.Text), ddtParcelaVencimento.Value))
                {

                    txtParcelaValorUnitario.Text = "";
                    txtParcelaDesconto.Text = "";
                    txtParcelaSubTotal.Text = "";
                    lblParcelaProdutoId.Text = "";
                    lblParcelaProduto.Text = "";

                    ddtParcelas.AutoGenerateColumns = false;
                    ddtParcelas.DataSource = null;
                    ddtParcelaItens.AutoGenerateColumns = false;
                    ddtParcelaItens.DataSource = null;

                    cmbParcelaCiclo.Items.Add(lblCicloParcela.Text.Trim());
                    cmbParcelaCiclo.SelectedItem = lblCicloParcela.Text.Trim();


                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCicloParcela.Text), -1);

                    CarregarParcelas();
                }

                SelecionarParcela(Convert.ToInt64(lblParcelaId.Text));
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuParcelaReencaminharSICOOB_Click(object sender, EventArgs e)
        {
            txtContratoTitularNomeCompleto.Focus();
            try
            {
                for (var y = 0; y < ddtParcelas.RowCount; y++)
                {
                    if (!Convert.ToBoolean(ddtParcelas.Rows[y].Cells["Selecionar"].Value)) continue;
                    try
                    {
                        if (lblContrato.Text.Trim() == Empty)
                        {
                            MessageBox.Show(@"Selecione uma parcela", @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var parcela = ddtParcelas.Rows[y].Cells["TB016_id"].Value;
                        var parcelac = new ParcelaNegocios().ParcelaId(Convert.ToInt64(parcela));
                        if (parcelac.TB016_id <= 0) continue;


                        if(!new ParcelaNegocios().EmitirBoletoAvulsto(parcelac,
                            ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(parcela)))
                        {
                            MessageBox.Show("Boleto não gerado", @"Erro Geração Boleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                CarregarParcelas();
                grupCicloParcela.Visible = false;
                chkSelecionarTudo.Checked = false;
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private void mnuComprovanteFechar_Click(object sender, EventArgs e)
        {
            grpComprovante.Visible = false;
        }
        private void pctParcelaPagamentoEspecie_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", "")) < Convert.ToDouble(lblParcelaValorTotal.Text.Replace("R$", "")))
                {
                    MessageBox.Show(Format(MensagensDoSistema._0083, "Valor", @"Erro", MessageBoxButtons.OK,
                        MessageBoxIcon.Error));
                    lblParcelaCartaoValorParcela.Focus();
                    return;
                }
                var pagamento = new ParcelaController
                {
                    TB016_id = Convert.ToInt64(lblParcelaId.Text),
                    TB016_DataPagamento = ddtParcelaDataPagamento.Value,
                    TB016_FormaProcessamentoBaixa = 1,
                    TB016_CredNCartao = "0",
                    TB016_CredCPFTitularCartaoCartao = mskParcelaCartaoCPFTitular.Text,
                    TB016_CredNomeTitularCartaoCartao = txtParcelaCartaoNomeTitular.Text,
                    TB016_CredBandeira = 0,
                    TB016_CredNParcelas = 1,
                    TB016_CredValorParcelas = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", "")),
                    TB016_CredAutorizacao = "DINHEIRO",
                    TB016_CredCodValidador = "DINHEIRO",
                    TB012_id = Convert.ToInt64(lblContrato.Text),
                    TB016_CredFormaParamentoId = 2,
                    TB016_CredFormaParamentoDescricao = "DINHEIRO",
                    TB016_AlteradoEm = DateTime.Now,
                    TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_CredBaixaFeitaEm = DateTime.Now,
                    TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_StatusS = "5",
                    TB016_CredDataCredito = DateTime.Now,
                    TB016_ValorPago = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", "")),
                    TB016_CredValor = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", ""))
                };

                var pagamentoN = new ParcelaNegocios();
                if (!pagamentoN.ParcelaInserirPagamentoCredParcela(pagamento,
                    Convert.ToInt16(chkParcelaCancelamento.Checked))) return;
                CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                CarregarParcelas();
                new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);
                pagamentoComprovanteTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                pagamentoComprovanteTableAdapter.Fill(clubeConteza_Relatorios.PagamentoComprovante, pagamento.TB016_id);
                rptComprovanteCredito.RefreshReport();
                var docN = new ContratoDocNegocios();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                var documento = new ContratoDocController
                {
                    TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB029_TipoS = "3",
                    TB012_id = pagamento.TB012_id,
                    TB029_DocImpressao = rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType,
                        out encoding, out extension, out streamids, out warnings)
                };



                docN.DocImpressaoInserir(documento);
                grpComprovante.Visible = true;
                grpComprovante.Location = new Point(220, 34);

                try
                {
                    new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
        }
        private void lblParcelaCartaoValorParcela_Leave(object sender, EventArgs e)
        {
            //lblParcelaCartaoValorParcela
            lblParcelaCartaoValorParcela.Text = Format("{0:C2}",
                Convert.ToDouble(lblParcelaCartaoValorParcela.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")));
        }
        bool ValidarDebito()
        {
            if (!_validacoes.CPF(mskParcelaCartaoCPFTitular.Text))
            {
                MessageBox.Show(MensagensDoSistema._0031, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskParcelaCartaoCPFTitular.Focus();
                return false;
            }

            if (txtParcelaCartaoNumero.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoNumero.Focus();
                return false;
            }


            if (txtParcelaCartaoNomeTitular.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoNomeTitular.Focus();
                return false;
            }


            if (txtParcelaCartaoAutorizacao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Autorização"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoAutorizacao.Focus();
                return false;
            }

            if (txtParcelaCartaoCodValidador.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Cod. Validador"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoCodValidador.Focus();
                return false;
            }

            return true;
        }
        private void pctParcelaPagamentoDebito_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDebito()) return;
                lblParcelaCartaoDataRecebimentoBanco.Text = ddtParcelaDataPagamento.Value.ToString("dd/MM/yyyy");
                var pagamento = new ParcelaController
                {
                    TB016_id = Convert.ToInt64(lblParcelaId.Text),
                    TB016_DataPagamento = ddtParcelaDataPagamento.Value,
                    TB016_FormaProcessamentoBaixa = 1,
                    TB016_CredNCartao = txtParcelaCartaoNumero.Text,
                    TB016_CredCPFTitularCartaoCartao = mskParcelaCartaoCPFTitular.Text,
                    TB016_CredNomeTitularCartaoCartao = txtParcelaCartaoNomeTitular.Text,
                    TB016_CredBandeira = 0,
                    TB016_CredNParcelas = 1,
                    TB016_CredValorParcelas = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", "")),
                    TB016_CredAutorizacao = txtParcelaCartaoAutorizacao.Text,
                    TB016_CredCodValidador = txtParcelaCartaoCodValidador.Text,
                    TB012_id = Convert.ToInt64(lblContrato.Text),
                    TB016_CredFormaParamentoId = 2,
                    TB016_CredFormaParamentoDescricao = cmbFormaPagamento.Text,
                    TB016_AlteradoEm = DateTime.Now,
                    TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_CredBaixaFeitaEm = DateTime.Now,
                    TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_StatusS = "5",
                    TB016_CredDataCredito = Convert.ToDateTime(lblParcelaCartaoDataRecebimentoBanco.Text),
                    TB016_ValorPago = Convert.ToDouble(lblParcelaValorTotal.Text.Replace("R$", "")),
                    TB016_CredValor = Convert.ToDouble(lblParcelaCartaoValorCredito.Text.Replace("R$", ""))
                };

                var pagamentoN = new ParcelaNegocios();
                if (pagamentoN.ParcelaInserirPagamentoCredParcela(pagamento, Convert.ToInt16(chkParcelaCancelamento.Checked)))
                {
                    new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);
                    CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                    CarregarParcelas();

                    pagamentoComprovanteTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                    pagamentoComprovanteTableAdapter.Fill(clubeConteza_Relatorios.PagamentoComprovante, pagamento.TB016_id);
                    rptComprovanteCredito.RefreshReport();

                    var docN = new ContratoDocNegocios();

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    var documento = new ContratoDocController
                    {
                        TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                        TB029_TipoS = "3",
                        TB012_id = pagamento.TB012_id,
                        TB029_DocImpressao = rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType,
                            out encoding, out extension, out streamids, out warnings)
                    };



                    docN.DocImpressaoInserir(documento);
                    grpComprovante.Visible = true;
                    grpComprovante.Location = new Point(220, 34);
          
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
        }
        private void ddtParcelaDataPagamento_ValueChanged(object sender, EventArgs e)
        {
            lblParcelaCartaoDataRecebimentoBanco.Text = ddtParcelaDataPagamento.Value.ToString("dd/MM/yyyy");
        }
        private void cmbParcelaCreditoBandeira_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCredParcelas.DataSource = null;
            cmbCredParcelas.Items.Clear();
            try
            {
                if (Convert.ToInt16(cmbCredBandeira.SelectedValue) <= 0) return;
                cmbCredParcelas.DataSource = new ParcelaNegocios().ListaParcelamentoPossivelPorBandeira(Convert.ToInt64(cmbCredBandeira.SelectedValue), Convert.ToInt64(lblCredUnidade.Text));
                cmbCredParcelas.DisplayMember = "TB031_NParcelas";
                cmbCredParcelas.ValueMember = "TB031_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool ValidarCredito()
        {
            if (Convert.ToInt16(cmbCredBandeira.SelectedValue) == 0)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Bandeira"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCredBandeira.Focus();
                return false;
            }

            if (Convert.ToInt16(cmbCredParcelas.SelectedValue) == 0)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Parcelas"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCredParcelas.Focus();
                return false;
            }


            if (lblCredValorParcela.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Valor Parcela"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCredValorParcela.Focus();
                return false;
            }



            if (Convert.ToDouble(lblCredValorParcela.Text.Replace("R$", "")) < Convert.ToDouble(lblCredValorMinimoParcela.Text.Replace("R$", "")))
            {

                MessageBox.Show(Format(MensagensDoSistema._0063, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                return false;
            }


            if (txtParcelaCartaoAutorizacao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Autorização"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoAutorizacao.Focus();
                return false;
            }

            if (txtParcelaCartaoCodValidador.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Cod. Validador"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoCodValidador.Focus();
                return false;
            }

            if (txtParcelaCartaoNumero.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoNumero.Focus();
                return false;
            }

            if (mskParcelaCartaoCPFTitular.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CPF Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskParcelaCartaoCPFTitular.Focus();
                return false;
            }

            if (txtParcelaCartaoNomeTitular.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaCartaoNomeTitular.Focus();
                return false;
            }

            if (_validacoes.CPF(mskParcelaCartaoCPFTitular.Text)) return true;
            MessageBox.Show(MensagensDoSistema._0031, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            mskParcelaCartaoCPFTitular.Focus();
            return false;
        }
        private void pctParcelaPagamentoCredito_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCredito()) return;

                var pagamento = new ParcelaController
                {
                    TB016_id = Convert.ToInt64(lblParcelaId.Text),
                    TB016_DataPagamento = ddtParcelaDataPagamento.Value,
                    TB016_FormaProcessamentoBaixa = 1,
                    TB016_CredNCartao = txtParcelaCartaoNumero.Text,
                    TB016_CredCPFTitularCartaoCartao = mskParcelaCartaoCPFTitular.Text,
                    TB016_CredNomeTitularCartaoCartao = txtParcelaCartaoNomeTitular.Text,
                    TB016_CredBandeira = Convert.ToInt64(cmbCredBandeira.SelectedValue),
                    TB016_CredNParcelas = Convert.ToInt16(cmbCredParcelas.SelectedIndex),
                    TB016_CredValorParcelas = Convert.ToDouble(lblCredValorParcela.Text.Replace("R$", "")),
                    TB016_CredAutorizacao = txtParcelaCartaoAutorizacao.Text,
                    TB016_CredCodValidador = txtParcelaCartaoCodValidador.Text,
                    TB012_id = Convert.ToInt64(lblContrato.Text),
                    TB016_CredFormaParamentoId = Convert.ToInt64(cmbCredParcelas.SelectedValue),
                    TB016_CredFormaParamentoDescricao = cmbFormaPagamento.Text,
                    TB016_AlteradoEm = DateTime.Now,
                    TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_CredBaixaFeitaEm = DateTime.Now,
                    TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_StatusS = "5",
                    TB016_CredDataCredito = Convert.ToDateTime(lblParcelaCartaoDataRecebimentoBanco.Text),
                    TB016_ValorPago = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", "")),
                    TB016_CredValor = Convert.ToDouble(lblCredValorCredito.Text.Replace("R$", ""))
                };


                if (!new ParcelaNegocios().ParcelaInserirPagamentoCredParcela(pagamento, Convert.ToInt16(chkParcelaCancelamento.Checked))) return;
                new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);
 

                CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                CarregarParcelas();

                pagamentoComprovanteTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                pagamentoComprovanteTableAdapter.Fill(clubeConteza_Relatorios.PagamentoComprovante, pagamento.TB016_id);
                rptComprovanteCredito.RefreshReport();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                var documento =
                    new ContratoDocController
                    {
                        TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                        TB029_TipoS = "3",
                        TB012_id = pagamento.TB012_id,
                        TB029_DocImpressao = rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType,
                            out encoding, out extension, out streamids, out warnings)
                    };



                new ContratoDocNegocios().DocImpressaoInserir(documento);

                grpComprovante.Visible = true;
                grpComprovante.Location = new Point(220, 34);
          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
        }
        private void cmbCredParcelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var parcelaN = new ParcelaNegocios();
                var retorno = parcelaN.SelectFormaParamento(Convert.ToInt64(cmbCredParcelas.SelectedValue));

                lblCredValorMinimoParcela.Text = double.Parse(retorno.TB031_ValorMinimoParcela.ToString(CultureInfo.CurrentCulture)).ToString("C2");
                //lblCredDescricaoPagamento.Text = retorno.TB031_Descricao;
                lblParcelaCreditoTipoVencimento.Text = retorno.TB031_TipoVencimento.ToString();
                DateTime vencimentoCartao = DateTime.Now.AddDays(retorno.TB031_DVencimento);
                lblParcelaCartaoDataRecebimentoBanco.Text = vencimentoCartao.ToString("dd/MM/yyyy");
                lblParcelaCreditoTaxas.Text = double.Parse(retorno.TB031_Taxa.ToString(CultureInfo.CurrentCulture)).ToString("C2");

                var valorCredito = Convert.ToDouble(lblParcelaValorTotal.Text.Replace("R$", "")) - Convert.ToDouble(lblParcelaCreditoTaxas.Text.Replace("R$", ""));


                lblCredValorCredito.Text = double.Parse(valorCredito.ToString(CultureInfo.CurrentCulture)).ToString("C2");


                if (Convert.ToInt16(cmbCredParcelas.Text) > 0)
                {
                    var valor = Convert.ToDouble(lblParcelaValorTotal.Text.Replace("R$", ""));
                    var minimo = Convert.ToDouble(lblCredValorMinimoParcela.Text.Replace("R$", ""));
                    var resultado = valor / Convert.ToInt16(cmbCredParcelas.Text);
                    lblCredValorParcela.Text = double.Parse(resultado.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                    if (resultado < minimo)
                    {
                        // mnuPagamentoEmitir.Enabled = false;
                        MessageBox.Show(Format(MensagensDoSistema._0063, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                    else
                    {
                        //mnuPagamentoEmitir.Enabled = true;
                    }
                }
                else
                {
                    lblCredValorMinimoParcela.Text = "";
                    lblCredValorParcela.Text = "";
                    //lblCredDescricaoPagamento.Text = "";
                    lblParcelaCartaoDataRecebimentoBanco.Text = "";
                    MessageBox.Show(Format(MensagensDoSistema._0062, "N.º Parcelas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pctPagamentoBoleto_Click(object sender, EventArgs e)
        {
            if (ValidarBaixa())
            {
                try
                {
                    var Ciclo = cmbParcelaCiclo.Text;

                    ParcelaController parcelaC = new ParcelaController();

                    parcelaC.TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    parcelaC.TB016_NossoNumero = txtParcelaBoletoDocBanco.Text;
                    parcelaC.TB016_IOF = Convert.ToDouble(mskParcelaBoletoIOF.Text.Replace("R$", ""));
                    parcelaC.TB016_ValorTarifa = Convert.ToDouble(mskParcelaBoletoTarifa.Text.Replace("R$", ""));
                    parcelaC.TB016_Multa = Convert.ToDouble(mskParcelaBoletoMulta.Text.Replace("R$", ""));
                    parcelaC.TB016_ValorPago = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", ""));
                    parcelaC.TB016_DataPagamento = ddtParcelaDataPagamento.Value;
                    parcelaC.TB016_CredValor = Convert.ToDouble(lblParcelaCartaoValorCredito.Text.Replace("R$", ""));
                    parcelaC.TB016_FormaProcessamentoBaixa = 2;
                    parcelaC.TB016_CredNCartao = txtParcelaCartaoNumero.Text.TrimEnd();
                    parcelaC.TB016_CredCPFTitularCartaoCartao = mskParcelaCartaoCPFTitular.Text;
                    parcelaC.TB016_CredNomeTitularCartaoCartao = txtParcelaCartaoNomeTitular.Text;
                    parcelaC.TB016_CredBandeira = cmbCredBandeira.Items.Count > 1 ? Convert.ToInt64(cmbCredBandeira.SelectedValue) : 0;
                    parcelaC.TB016_CredNParcelas = Convert.ToInt16(1);
                    parcelaC.TB016_CredValorParcelas = Convert.ToDouble(lblParcelaCartaoValorParcela.Text.Replace("R$", ""));
                    parcelaC.TB016_CredAutorizacao = txtParcelaCartaoAutorizacao.Text.TrimEnd().TrimStart();
                    parcelaC.TB016_CredCodValidador = txtParcelaCartaoCodValidador.Text;
                    parcelaC.TB016_CredFormaParamentoId = Convert.ToInt64(cmbFormaPagamento.SelectedValue);
                    parcelaC.TB016_CredFormaParamentoDescricao = cmbFormaPagamento.Text.TrimEnd();
                    parcelaC.TB016_CredBaixaFeitaEm = DateTime.Now;
                    parcelaC.TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    parcelaC.TB016_StatusS = "5";
                    parcelaC.TB016_CredDataCredito = Convert.ToDateTime(lblParcelaCartaoDataRecebimentoBanco.Text);
                    parcelaC.TB016_id = Convert.ToInt64(lblParcelaId.Text);
                    parcelaC.TB012_id = Convert.ToInt64(lblContrato.Text);

                    var parcelaN = new ParcelaNegocios();
                    if (parcelaN.ParcelaBaixaFinanceiro(parcelaC, Convert.ToInt16(chkParcelaCancelamento.Checked)))
                    {
                        new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);
                        CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                        CarregarParcelas();

                        pagamentoComprovanteTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                        pagamentoComprovanteTableAdapter.Fill(clubeConteza_Relatorios.PagamentoComprovante, Convert.ToInt64(lblParcelaId.Text));
                        rptComprovanteCredito.RefreshReport();

                        var docN = new ContratoDocNegocios();

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        var documento = new ContratoDocController();

                        documento.TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        documento.TB029_TipoS = "3";
                        documento.TB012_id = parcelaC.TB012_id;

                        documento.TB029_DocImpressao = rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                        docN.DocImpressaoInserir(documento);
                        //rptComprovanteCredito.Visible = true;
                        //rptComprovanteCredito.Visible = true;
                        //grpComprovante.Visible = true;
                        //grpComprovante.Location = new Point(220, 34);

                    }
                    else
                    {
                        MessageBox.Show(MensagensDoSistema._0064, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    cmbParcelaCiclo.Text = Ciclo;

                    CarregarParcelas();
                    grpCicloContrato.Visible = false;
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
        }
        private void btnParcelaFechar_Click(object sender, EventArgs e)
        {
            gpbParcelaAvulsa.Visible = false;
        }
        private void unirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContratoTitularNomeCompleto.Focus();

            if (MessageBox.Show(@"Deseja unir parcelas?", @"Parcelas", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            long id1=0;


            ParcelaController parcela1 = new ParcelaController();
            for (var y = 0; y < ddtParcelas.RowCount; y++)
            {


              
                    try
                    {
                    if (Convert.ToBoolean(ddtParcelas.Rows[y].Cells["Selecionar"].Value))
                    {
                        if (id1 == 0)
                        {
                            id1 = Convert.ToInt64(ddtParcelas.Rows[y].Cells["TB016_id"].Value);
                            parcela1 = new ParcelaNegocios().ParcelaPesquisaId(id1);
                        }
                        else
                        {
                            if (Convert.ToBoolean(ddtParcelas.Rows[y].Cells["Selecionar"].Value))
                            {
                                //ParcelaController parcela2 =
                                //new ParcelaNegocios().ParcelaProximaParcelaId(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(ddtParcelas.Rows[y].Cells["TB016_id"].Value));
                                long v1 = id1;
                                long v2 = Convert.ToInt64(ddtParcelas.Rows[y].Cells["TB016_id"].Value);
                                if (new ParcelaNegocios().ParcelaUnir(parcela1, Convert.ToInt64(ddtParcelas.Rows[y].Cells["TB016_id"].Value),
                                ParametrosInterface.objUsuarioLogado.TB011_Id))
                                {
                                    id1 = Convert.ToInt64(ddtParcelas.Rows[y].Cells["TB016_id"].Value);
                                    parcela1 = new ParcelaNegocios().ParcelaPesquisaId(id1);
                                    // MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                                    // MessageBoxIcon.Information);
                                    //ddtParcelas.AutoGenerateColumns = false;
                                    //ddtParcelas.DataSource = null;
                                    //ddtParcelaItens.AutoGenerateColumns = false;
                                    //ddtParcelaItens.DataSource = null;

                                    //if (cmbDiaVencimento.Text.Trim() == Empty)
                                    //{
                                    //    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"),
                                    //        @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //    return;
                                    //}

                                    //cmbParcelaCiclo.Items.Add(parcela1.TB012_CicloContrato);

                                    //cmbParcelaCiclo.SelectedItem = parcela1.TB012_CicloContrato;

                                    //List<ParcelaController> parcelas =
                                    //    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                                    //        Convert.ToInt64(parcela1.TB012_CicloContrato), -1);
                                }
                            }
                        }

                    }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
            }


            ddtParcelas.AutoGenerateColumns = false;
            ddtParcelas.DataSource = null;
            ddtParcelaItens.AutoGenerateColumns = false;
            ddtParcelaItens.DataSource = null;

            if (cmbDiaVencimento.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"),
                    @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbParcelaCiclo.Items.Add(parcela1.TB012_CicloContrato);

            cmbParcelaCiclo.SelectedItem = parcela1.TB012_CicloContrato;

            List<ParcelaController> parcelas =
                new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                    Convert.ToInt64(parcela1.TB012_CicloContrato), -1);

            var Ciclo = cmbParcelaCiclo.Text;
            cmbParcelaCiclo.Text = Ciclo;
            CarregarParcelas();
        }
        private void ddtParcelaItens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                switch (ddtParcelaItens.Columns[e.ColumnIndex].HeaderText)
                {
                    case "id":
                        var parcelaProduto = new ParcelaNegocios().ParcelaProdutoPesquisaId(Convert.ToInt64(ddtParcelaItens.Rows[e.RowIndex].Cells["TB017_id"].Value));
                        lblParcelaProdutoId.Text = parcelaProduto.TB017_id.ToString();
                        lblParcelaProduto.Text = parcelaProduto.TB017_Item;
                        txtParcelaValorUnitario.Text = double.Parse(parcelaProduto.TB017_ValorUnitario.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                        txtParcelaDesconto.Text = double.Parse(parcelaProduto.TB017_ValorDesconto.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                        txtParcelaSubTotal.Text = double.Parse(parcelaProduto.TB017_ValorFinal.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");

                        //if (parcelaProduto.TB017_Maior == 0 | parcelaProduto.TB017_Menor == 0 | parcelaProduto.TB017_Isento == 0)
                        //{
                        //    mnuParcelaAbonarAdesao.Enabled = true;
                        //}
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtParcelaDesconto_Leave(object sender, EventArgs e)
        {
            txtParcelaSubTotal.Text = double.Parse(Convert.ToString(Convert.ToDouble(txtParcelaValorUnitario.Text.Replace("R$", "")) - Convert.ToDouble(txtParcelaDesconto.Text.Replace("R$", "")), CultureInfo.CurrentCulture).Replace(".", ",")).ToString("C2");
            txtParcelaDesconto.Text = double.Parse(txtParcelaDesconto.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");

        }
        private void pctAlterarDadosParcela_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Deseja alterar o valor do desconto no produto?", @"Parcelas", MessageBoxButtons.YesNo) ==
    DialogResult.No)
            {
                return;
            }
            try
            {
                if (!new ParcelaNegocios().ParcelasAlteracaoDesconto(Convert.ToInt64(lblParcelaId.Text),
                    Convert.ToInt64(lblParcelaProdutoId.Text),
                    Convert.ToDouble(txtParcelaDesconto.Text.Replace("R$", "")),
                    Convert.ToDouble(txtParcelaSubTotal.Text.Replace("R$", "")),
                    ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text))) return;
                SelecionarParcela(Convert.ToInt64(lblParcelaId.Text));
                /**/
                ddtParcelas.AutoGenerateColumns = false;
                ddtParcelas.DataSource = null;
                ddtParcelaItens.AutoGenerateColumns = false;
                ddtParcelaItens.DataSource = null;

                if (cmbDiaVencimento.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"), @"Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Debug.Assert(cmbParcelaCiclo.Text != null, "cmbParcelaCiclo.ComboBox != null");
                CarregarCiclos(Convert.ToInt64(lblContrato.Text));
                var parcelas =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(cmbParcelaCiclo.Text), -1);
                ddtParcelas.DataSource = parcelas;
                /**/
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtro();
        }

        private void Filtro()
        {
            if (IsNullOrEmpty(txtFiltroAssociado.Text.Trim()))
            {

                //CarregarContratos(ListarPessoas(" TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                //                                " 23:59:59" + "'"));

                switch (cmbTipoContrato.SelectedIndex)
                {
                    case 0:
                        {
                            CarregarContratos(ListarPessoas(" TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                                              " 23:59:59" + "'"));
                            break;
                        }
                    case 1:
                        {
                            CarregarContratos(ListarPessoas(" TB012_TipoContrato = 1 and TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                                              " 23:59:59" + "'"));
                              break;
                        }
                    case 2:
                        {
                            CarregarContratos(ListarPessoas(" TB012_TipoContrato = 2 and TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                                            " 23:59:59" + "'"));
                            break;
                        }

                    case 3:
                        {
                            CarregarContratos(ListarPessoas(" TB012_TipoContrato = 3 and TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                                            " 23:59:59" + "'"));
                            break;
                        }

                    case 4:
                        {
                            CarregarContratos(ListarPessoas(" TB012_TipoContrato = 4 and TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                                            " 23:59:59" + "'"));
                            break;
                        }

                    case 5:
                        {
                            CarregarContratos(ListarPessoas(" TB012_TipoContrato = 5 and TB012_CadastradoEm BETWEEN '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year + " 00:00:00" + "' AND '" + dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                                            " 23:59:59" + "'"));
                             break;
                        }
                }


            }
            else
            {
                try
                {
                    string tipoCampo;

                    string tipo = " ";

                    if (Convert.ToInt16(cmbTipoContrato.SelectedIndex) == 0)
                    {

                    }
                    else
                    {
                        if (Convert.ToInt16(cmbTipoContrato.SelectedIndex) == 1)
                        {
                            tipo = " TB012_TipoContrato = 1 and ";
                        }
                        else
                        {
                            if (Convert.ToInt16(cmbTipoContrato.SelectedIndex) == 2)
                            {
                                tipo = " TB012_TipoContrato = 2 and ";
                            }
                            else
                            {
                                if (Convert.ToInt16(cmbTipoContrato.SelectedIndex) == 3)
                                {
                                    tipo = " TB012_TipoContrato = 3 and ";
                                }
                                else
                                {
                                    if (Convert.ToInt16(cmbTipoContrato.SelectedIndex) == 4)
                                    {
                                        tipo = " TB012_TipoContrato = 4 and ";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(cmbTipoContrato.SelectedIndex) == 0)
                                        {
                                            tipo = " TB012_TipoContrato = 5 and ";
                                        }
                                    }
                                }
                            }
                        }
                    }

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
                                CarregarContratos(ListarPessoas(tipo + "  dbo.TB013_Pessoa.TB013_NomeCompleto LIKE '" +
                                                                txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'"));
                                break;
                            }
                        case @"Contrato":
                            {

                                CarregarContratos(ListarPessoas(tipo + "  dbo.TB012_Contratos.TB012_id =" +
                                                                txtFiltroAssociado.Text.TrimEnd().TrimStart()));
                                break;
                            }
                        case @"CPF":
                            {
                                CarregarContratos(ListarPessoas(tipo + "  dbo.TB013_Pessoa.TB013_CPFCNPJ = '" + txtFiltroAssociado.Text
                                                                    .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                                    .Replace("-", "").Replace("/", "") + "'"));
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void mnuParcelaEmitirBoleto_Click(object sender, EventArgs e)
        {

        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbpDocumentos);
            CarregarDocumentosContrato();
            tabPrincipal.TabPages.Remove(tbContrato);
        }

        private void CarregarDocumentosContrato()
        {
            try
            {
                var docL = new ContratoDocNegocios().DocContratoLista(Convert.ToInt64(lblContrato.Text));
                /*Carregar Relatorios e Documentos*/
                ddgDocumentos.AutoGenerateColumns = false;
                ddgDocumentos.DataSource = docL;
                ddgDocumentos.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuDocumentosFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpDocumentos);
            tabPrincipal.TabPages.Add(tbContrato);
        }

        private void ddgDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                gprAnexarTermo.Visible = false;
                //label134.Text = "";
                axAcroPDF2.src = "";
                axAcroPDF2.Visible = false;

                axAcroPDF1.Visible = false;
                ContratoDocController docC = new ContratoDocController();
                ContratoDocController docR;
                ContratoDocNegocios docN = new ContratoDocNegocios();

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    docC.TB029_Id = Convert.ToInt64(ddgDocumentos.Rows[e.RowIndex].Cells["TB029_Id"].Value);

                    switch (ddgDocumentos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":
                            docR = docN.DocImpressaoSelect(docC);

                            if (docR.TB029_Id > 0)
                            {
                                string nomeArquivo =
                                    @"C:\Temp\" + docR.TB029_Id.ToString() + docC.TB029_TipoS + ".pdf";
                                FileInfo logoUnidade = new FileInfo(nomeArquivo);
                                File.Delete(logoUnidade.FullName);
                                var fs = new FileStream(nomeArquivo,
                                    FileMode.Create);
                                fs.Write(docR.TB029_DocImpressao, 0, docR.TB029_DocImpressao.Length);
                                fs.Close();
                                axAcroPDF1.src = nomeArquivo;
                                axAcroPDF1.Visible = true;
                            }


                            break;
                        case "Scanner":

                            MessageBox.Show(MensagensDoSistema._0057, @"Retorno", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ddtParcelas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var nossoNumeroColuna = ddtParcelas.Rows[e.RowIndex].Cells["TB016_NossoNumero"].Value != null ? ddtParcelas.Rows[e.RowIndex].Cells["TB016_NossoNumero"].Value.ToString() : string.Empty;
            long.TryParse(nossoNumeroColuna, out long nossoNumero);

            if (nossoNumero <= 0)
            {
                MessageBox.Show("Digite um número válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var parcelaColuna = ddtParcelas.Rows[e.RowIndex].Cells["TB016_id"].Value != null ? ddtParcelas.Rows[e.RowIndex].Cells["TB016_id"].Value.ToString() : string.Empty;
            long.TryParse(parcelaColuna, out long idParcela);

            if (idParcela <= 0)
            {
                MessageBox.Show("Não foi possível encontrar a parcela.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nossoNumeroParcela = new ParcelaNegocios().BuscarNossoNumeroPorIdParcela(idParcela);

            if (nossoNumero != nossoNumeroParcela)
            {
                if (MessageBox.Show("Deseja alterar o nosso número?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    ddtParcelas.Rows[e.RowIndex].Cells["TB016_NossoNumero"].Value = nossoNumeroParcela;
                    return;
                }

                if (new ParcelaNegocios().AlterarNossoNumeroPorParcela(idParcela, nossoNumero))
                {
                    ddtParcelas.Rows[e.RowIndex].Cells["TB016_NossoNumero"].Value = nossoNumero;
                    MessageBox.Show("Nosso Número alterado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro ao alterar o nosso número, tente novamente...", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
