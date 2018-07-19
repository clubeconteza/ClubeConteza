using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.String;


namespace ContezaAdmin.Inadimpentes
{
   
    public partial class frmNegociacao : Form
    {
        Util _validacoes = new Util();
        List<ParcelaController> ParcelasNegociadas = new List<ParcelaController>();
        HtmlDocument _doc;

        public frmNegociacao()
        {
            InitializeComponent();
        }
        private void frmNegociacao_Load(object sender, EventArgs e)
        {
            dtContratoFim.Enabled = true;
            dtContratoInicio.Enabled = true;
            if (new UsuarioAPPNegocios().VS() != Application.ProductVersion)
            {
                MessageBox.Show(
                    Format(MensagensDoSistema._0051, Application.ProductVersion,
                        ParametrosInterface.objUsuarioLogado.VS), @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            dTRPT0020TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            carneTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            dTRPT0020TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0020);
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Remove(tbIDemaisItens);
            tabPrincipal.TabPages.Remove(tbIDocumentos);
            tabPrincipal.TabPages.Remove(tbBoletos);
            tabPrincipal.TabPages.Remove(tbCarne);
            tabPrincipal.TabPages.Remove(tbSimulacao);
            tabPrincipal.TabPages.Remove(tbEnviarPorEmail);
            tabPrincipal.TabPages.Remove(tbRelacao);
            CarregarSexo();
            CarregarPaises();
            string vQuery=@"";
            CarregarContratos(ListarInadimplencias(vQuery));
            var filtro = new PaisController { TB003_id = 1058 };
            PopularEstadosTitular(filtro);
            FormasDePagamento();
            PopularTipoContatos();
            CarregarCondicoes();

            if(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id==1)
            {
                lbMensalidadeDescOutros.Visible = true;
                lbAdesaoDescAdesao.Visible = true;
            }
        }
        private void CarregarSexo()
        {
            cmbContratoTitularSexo.DataSource = null;
            cmbContratoTitularSexo.Items.Clear();
            var lstSexo = new List<KeyValuePair<string, string>>();
            var sexoS = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE sexo in sexoS)
            {
                lstSexo.Add(new KeyValuePair<string, string>(sexo.ToString(), ((int)sexo).ToString()));
            }
            cmbContratoTitularSexo.DataSource = lstSexo;
            cmbContratoTitularSexo.DisplayMember = "Key";
            cmbContratoTitularSexo.ValueMember = "Value";
        }
        private void CarregarCondicoes()
        {
            try
            {
                cmbCondicoes.DataSource = null;
                cmbCondicoes.Items.Clear();
                cmbCondicoes.DataSource = new NegociacaoCondicaoNegocios().NegociacaoCondicaoPorUsuario(ParametrosInterface.objUsuarioLogado.TB011_Id);
                cmbCondicoes.DisplayMember = "TB036_Nome";
                cmbCondicoes.ValueMember = "TB036_Id";
       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarPaises()
        {
            try
            {
                var enderecoN = new EnderecoNegocios();   
                //Contrato
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
            cmbFormaPagamentoContrato.DataSource = null;
            cmbFormaPagamentoContrato.Items.Clear();

            var contratoStatus2Contrato = new List<KeyValuePair<string, string>>();
            var status2Contrato = Enum.GetValues(typeof(ParcelaController.TB016_FormaPagamentoE));
            foreach (ParcelaController.TB016_FormaPagamentoE statu2Contrato in status2Contrato)
            {
                contratoStatus2Contrato.Add(new KeyValuePair<string, string>(statu2Contrato.ToString(), ((int)statu2Contrato).ToString()));
            }

            cmbFormaPagamentoContrato.DataSource = contratoStatus2Contrato;
            cmbFormaPagamentoContrato.DisplayMember = "Key";
            cmbFormaPagamentoContrato.ValueMember = "Value";
        }
        private void PopularTipoContatos()
        {
            cmbContratoTitularContatoTipo.DataSource = null;
            cmbContratoTitularContatoTipo.Items.Clear();
            List<KeyValuePair<string, string>> contratoStatus = new List<KeyValuePair<string, string>>();
            Array status = Enum.GetValues(typeof(ContatoController.TB009_TipoE));
            foreach (ContatoController.TB009_TipoE statu in status)
            {
                contratoStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbContratoTitularContatoTipo.DataSource = contratoStatus;
            cmbContratoTitularContatoTipo.DisplayMember = "Key";
            cmbContratoTitularContatoTipo.ValueMember = "Value";
            /*Popular Motivo de Cancelamentos*/
            List<KeyValuePair<string, string>> motivosCancelamentos = new List<KeyValuePair<string, string>>();
            Array motivos = Enum.GetValues(typeof(PessoaController.TB013_CancelamentoMotivoE));
            foreach (PessoaController.TB013_CancelamentoMotivoE motivo in motivos)
            {
                motivosCancelamentos.Add(
                    new KeyValuePair<string, string>(motivo.ToString(), ((int)motivo).ToString()));
            }
        }
        private void mnuPrincipalFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ptbPrincipalFiltrar_Click(object sender, EventArgs e)
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
                        CarregarContratos(ListarInadimplencias(vQuery));
                        break;
                    }
                case @"Contrato":
                    {
                        string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                        txtFiltroAssociado.Text.TrimEnd().TrimStart();
                        CarregarContratos(ListarInadimplencias(vQuery));
                        break;
                    }
                case @"CPF":
                    {
                        string vQuery = " AND dbo.TB013_Pessoa.TB013_CPFCNPJ = '" + txtFiltroAssociado.Text
                                            .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                            .Replace("-", "").Replace("/", "") + "'";
                        CarregarContratos(ListarInadimplencias(vQuery));
                        break;
                    }
            }

        }
        public List<ParcelaController> ListarInadimplencias(string query)
        {
            List<ParcelaController> inadimplencia = new List<ParcelaController>();         
            try
            {
                 inadimplencia = new ParcelaNegocios().Inadimplencia(ParametrosInterface.objUsuarioLogado.TB037_Id, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return inadimplencia;
        }
        private void CarregarContratos(List<ParcelaController> listarContratos)
        {
            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);

            ddgContratos.AutoGenerateColumns = false;
            ddgContratos.DataSource = null;
            ddgContratos.DataSource = listarContratos;
            ddgContratos.Refresh();
            lblTotalRegistrosRetornados.Text = listarContratos.Count.ToString();
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
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgContratos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":
                            tabPrincipal.TabPages.Add(tbContrato);

                            tabPrincipal.TabPages.Remove(tbLista);
                            FiltrateContrato(Convert.ToInt64(ddgContratos.Rows[e.RowIndex].Cells["TB012_id"].Value));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                tabPrincipal.TabPages.Add(tbLista);
                tabPrincipal.TabPages.Remove(tbContrato);
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected void FiltrateContrato(long vId)
        {
            try
            {
                dtContratoFim.Enabled                   = false;
                dtContratoInicio.Enabled                = false;
                //PontosDeVenda();
                DTContratoTitularContatos.Rows.Clear();
                DTContratoTitularContatos.Refresh();
                var retorno                             = new ContratoNegocios().contratoSelect(vId);
                mskContratoTitularCPF.Enabled           = false;
                lblPontoVenda.Text                      = retorno.PontoDeVenda.TB002_id.ToString();
                lblContrato.Text                        = retorno.TB012_Id.ToString().PadLeft(6, '0');
                dtContratoInicio.Value                  = retorno.TB012_Inicio;
                dtContratoFim.Value                     = retorno.TB012_Fim;
                cmbDiaVencimento.Text                   = retorno.TB012_DiaVencimento.ToString();
                dtContratoInicio.Enabled                = false;
                txtContratoTitularNomeCompleto.Text     = retorno.Titular.TB013_NomeCompleto;
                lblCiclo.Text                           = retorno.TB012_CicloContrato;
                txtTB013_MaeNome.Text                   = retorno.Titular.TB013_MaeNome.TrimEnd();
                cmbContratoTitularSexo.SelectedValue    = retorno.Titular.TB013_SexoS;
                ddtTB013_MaeDataNascimento.Value        = retorno.Titular.TB013_MaeDataNascimento;
                txtTB013_PaiNome.Text                   = retorno.Titular.TB013_PaiNome.TrimEnd();
                ddtTB013_PaiDataNascimento.Value        = retorno.Titular.TB013_PaiDataNascimento;
                dtContratoTitularDataNascimento.Value   = retorno.Titular.TB013_DataNascimento;
                mskContratoTitularCPF.Text              = retorno.Titular.TB013_CPFCNPJ;       
                mskContratoTitularCEP.Text              = retorno.Titular.TB004_Cep;
                lblContratoTitularID.Text               = retorno.Titular.TB013_id.ToString();
                txtContratoTitularLogradouro.Text       = retorno.Titular.TB013_Logradouro;
                txtContratoTitularNumero.Text           = retorno.Titular.TB013_Numero;
                txtContratoTitularBairro.Text           = retorno.Titular.TB013_Bairro;
                txtContratoTitularComplemento.Text      = retorno.Titular.TB013_Complemento;
                cmbDiaVencimento.SelectedItem           = retorno.TB012_DiaVencimento;
                cmbTitularPais.SelectedValue            = retorno.Titular.Municipio.Estado.Pais.TB003_id;
                PaisController pais                     = new PaisController { TB003_id = retorno.Titular.Municipio.Estado.Pais.TB003_id };
                PopularEstadosTitular(pais);
                cmbContratoTitularEstado.SelectedValue  = retorno.Titular.Municipio.Estado.TB005_Id;
                var municipio = new EstadoController { TB005_Id = retorno.Titular.Municipio.Estado.TB005_Id };
                PopularMunicipiosTitular(municipio);
                cmbContratoTitularMunicipio.SelectedValue = retorno.Titular.Municipio.TB006_id;
                for (int i = 0; i < retorno.Titular.Contatos.Count; i++)
                {
                    DTContratoTitularContatos.Rows.Add(new object[] { retorno.Titular.Contatos[i].TB009_id });
                    DTContratoTitularContatos.Rows[i].Cells["cmbContratoTitularContatoTipo"].Value =
                    retorno.Titular.Contatos[i].TB009_TipoS;
                    DTContratoTitularContatos.Rows[i].Cells["txtContratoTitularContato"].Value =
                    retorno.Titular.Contatos[i].TB009_Contato;
                }
                DTContratoDependentes.AutoGenerateColumns   = false;
                DTContratoDependentes.DataSource            = null;
                DTContratoDependentes.DataSource            = retorno.Dependentes;
                DTContratoDependentes.Refresh();
                cmbContratoTitularSexo.SelectedValue        = retorno.Titular.TB013_SexoS;
                DTContratoDependentes.AutoGenerateColumns   = false;
                DTContratoDependentes.DataSource            = null;
                DTContratoDependentes.DataSource            = retorno.Dependentes;
                DTContratoDependentes.Refresh();
                cmbDiaVencimento.Enabled                    = false;
                PreencherAnotacoes(vId);
                CarregarParcelasVencidas();
                CarregarParcelas();
            }
            catch (Exception ex)
            {
                tabPrincipal.TabPages.Remove(tbContrato);
                tabPrincipal.TabPages.Add(tbLista);
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mskContratoTitularCEP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mskContratoTitularCEP.Text.Replace("-", "").Replace(" ", "").Length == 8)
                {
                    EnderecoNegocios enderecoN = new EnderecoNegocios();
                    CEPController filtro = new CEPController();
                    filtro.TB004_Cep = Convert.ToInt32(mskContratoTitularCEP.Text.Replace("-", "").Replace(" ", ""));
                    DataSet cep = enderecoN.Cep(filtro);

                    if (Convert.ToInt64(cep.Tables[0].Rows[0]["TB004_id"].ToString()) > 0)
                    {
                        txtContratoTitularLogradouro.Text = cep.Tables[0].Rows[0]["TB004_Logradouro"].ToString().Replace("'", ". ").Replace("$", ". ").Replace("%", ". ").Replace("*", ". ").Replace("# ", ". ");
                        txtContratoTitularBairro.Text = cep.Tables[0].Rows[0]["TB004_Bairro"].ToString().Replace("'", ". ").Replace("$", ". ").Replace("%", ". ").Replace("*", ". ").Replace("# ", ". ");
                        cmbContratoTitularEstado.SelectedValue = cep.Tables[0].Rows[0]["TB005_Id"].ToString();
                        cmbContratoTitularMunicipio.SelectedValue = cep.Tables[0].Rows[0]["TB006_id"].ToString();
                        txtContratoTitularNumero.Focus();
                    }
                    else
                    {
                        MessageBox.Show(MensagensDoSistema._0005, @"Retorno", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimparContrato()
        {
            lblTb011NomeExibicao.Text               = "";
            lblTb026Data.Text                       = "";
            lblTb026Id.Text                         = "";
            txtTb026Anotacao.Text                   = "";
            novaAnotaçãoToolStripMenuItem.Visible   = true;
            ParcelasNegociadas.Clear();
            lblContrato.Text                        = "";
            lblCiclo.Text                           = "";
            lblPontoVenda.Text                      = "";
            dataGridView1.DataSource                = null;
            dataGridView1.Refresh();
            ddtParcelas.DataSource                  = null;
            ddtParcelas.Refresh();
            ddtParcelaItens.DataSource              = null;
            ddtParcelaItens.Refresh();
            DTContratoTitularContatos.DataSource    = null;
            DTContratoTitularContatos.Refresh();
            DTContratoDependentes.DataSource        = null;
            DTContratoDependentes.Refresh();
            ddtParcelasContratos.DataSource         = null;
            ddtParcelasContratos.Refresh();
            dgAnotacoes.DataSource                  = null;
            dgAnotacoes.Refresh();
            label85.Text                            = "R$0,00";
            label57.Text                            = "R$0,00";
            label40.Text                            = "R$0,00";
            lblCondicoeValorMinimoParcela.Text      = "R$0,00";
            lblValorAdesao.Text                     = "R$0,00";
            label30.Text                            = "R$0,00";
            label29.Text                            = "R$0,00";
            lblTotalDescontos.Text                  = "R$0,00";
            label82.Text                            = "R$0,00";
            label51.Text                            = "R$0,00";
            label83.Text                            = "R$0,00";
            label82.Text                            = "R$0,00";
            label81.Text                            = "R$0,00";
            label81.Text                            = "R$0,00";
            label83.Text                            = "R$0,00";
            label80.Text                            = "R$0,00";
            lblValorMensalidade.Text                = "R$0,00";
            label64.Text                            = "R$0,00";
            label53.Text                            = "R$0,00";
            label76.Text                            = "R$0,00";
            label72.Text                            = "R$0,00";
            label23.Text                            = "R$0,00";
            label54.Text                            = "R$0,00";
            label27.Text                            = "R$0,00";
            label47.Text                            = "R$0,00";
            lblValorTotalCorrigido.Text             = "R$0,00";
            label73.Text                            = "R$0,00";
            label74.Text                            = "R$0,00";
            label78.Text                            = "R$0,00";
            label75.Text                            = "R$0,00";
            lblCondicaoDescricao.Text = "";  
        }
        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grpAnotacao.Visible = false;
            novaAnotaçãoToolStripMenuItem.Visible = true;
            tabPrincipal.TabPages.Add(tbLista);
            tabPrincipal.TabPages.Remove(tbContrato);
            LimparContrato();
        }
        bool ValidaTitular()
        {
            if (dtContratoInicio.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0085, @"Contrato", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (dtContratoFim.Value < dtContratoInicio.Value)
            {
                MessageBox.Show(MensagensDoSistema._0086, @"Contrato", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            if (dtContratoTitularDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0084, @"Titular", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (ddtTB013_MaeDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0084, @"Mãe", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (ddtTB013_PaiDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0084, @"Pai", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (txtTB013_MaeNome.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome da Mâe"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTB013_MaeNome.Focus();
                return false;
            }

            if (txtTB013_PaiNome.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome do Pai"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTB013_PaiNome.Focus();
                return false;
            }

            if (ddtTB013_MaeDataNascimento.Value > dtContratoTitularDataNascimento.Value)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "A mãe não pode ser mais nova que o titular"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (ddtTB013_PaiDataNascimento.Value > dtContratoTitularDataNascimento.Value)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "O pai não pode ser mais novo que o titular"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (txtContratoTitularNomeCompleto.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTitularNomeCompleto.Focus();
                return false;
            }

            if (mskContratoTitularCEP.Text.Trim().Replace("-", "").Length < 8)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CEP"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoTitularCEP.Focus();
                return false;
            }

            if (!_validacoes.CPF(mskContratoTitularCPF.Text))
            {
                MessageBox.Show(MensagensDoSistema._0031, @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                mskContratoTitularCPF.Focus();
                return false;
            }

            if (mskContratoTitularCPF.Text.Replace(",", "").Replace("-", "").Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CPF"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoTitularCPF.Focus();
                return false;
            }

            if (mskContratoTitularCPF.Text.Replace(",", "").Replace("-", "").Trim().Length < 8)
            {
                MessageBox.Show(MensagensDoSistema._0032, @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                mskContratoTitularCPF.Focus();
                return false;
            }

            if (lblContrato.Text.Trim() == Empty)
            {
                var pessoaN = new PessoaNegocios();
                var cpf = pessoaN.pessoaSelectCPFCNPJ((mskContratoTitularCPF.Text.Replace(",", "")
                    .Replace("-", "").Trim().Replace(".", "")));

                if (cpf.TB013_id > 0)
                {
                    if (Convert.ToInt16(cpf.TB013_StatusS) < 2)
                    {
                        if (cpf.Contrato.TB012_TipoContrato == 1)
                        {
                            MessageBox.Show(MensagensDoSistema._0040, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            mskContratoTitularCPF.Focus();
                            return false;
                        }
                    }

                }
            }

            if (txtContratoTitularLogradouro.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Logradouro"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTitularLogradouro.Focus();
                return false;
            }


            if (txtContratoTitularBairro.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Bairro"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTitularBairro.Focus();
                return false;
            }

            if (txtContratoTitularNumero.Text.Trim() == Empty)
            {
                txtContratoTitularNumero.Text = @"S/N";
            }

            if (txtContratoTitularComplemento.Text.Trim() == Empty)
            {
                txtContratoTitularComplemento.Text = @".";
            }

            DateTime dttFromDate = DateTime.Now;
            DateTime dttToDate = Convert.ToDateTime(dtContratoTitularDataNascimento.Text);
            TimeSpan tsDuration;
            tsDuration = dttFromDate - dttToDate;

            if (Convert.ToInt32((tsDuration.Days) / 365) < 18)
            {
                MessageBox.Show(MensagensDoSistema._0006, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtContratoTitularDataNascimento.Focus();
                return false;
            }


            return true;
        }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidaTitular())
            {
                return;
            }

            PessoaController titular = new PessoaController
            {
                Municipio = new MunicipioController
                {
                    TB006_id = Convert.ToInt64(cmbContratoTitularMunicipio.SelectedValue)
                },
                TB013_CPFCNPJ = mskContratoTitularCPF.Text,
                TB013_NomeCompleto = txtContratoTitularNomeCompleto.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                TB013_NomeExibicao = txtContratoTitularNomeCompleto.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                TB013_SexoS = cmbContratoTitularSexo.SelectedValue.ToString(),
                TB013_DataNascimento = dtContratoTitularDataNascimento.Value,
                TB004_Cep = mskContratoTitularCEP.Text,
                TB013_Logradouro = txtContratoTitularLogradouro.Text,
                TB013_MaeNome = txtTB013_MaeNome.Text.TrimEnd().Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                TB013_CartaoSolicitado = 1,
                TB013_Numero = txtContratoTitularNumero.Text,
                TB013_Bairro = txtContratoTitularBairro.Text,
                TB013_Complemento = txtContratoTitularComplemento.Text,
                TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,                
                TB013_MaeDataNascimento = ddtTB013_MaeDataNascimento.Value,
                TB013_PaiNome = txtTB013_PaiNome.Text.TrimEnd().Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                TB013_PaiDataNascimento = ddtTB013_PaiDataNascimento.Value,
                TB013_id = Convert.ToInt64(lblContratoTitularID.Text),
                TB012_Id = Convert.ToInt64(lblContrato.Text)
            };

            PreencherAnotacoes(Convert.ToInt64(lblContrato.Text));

            if (new PessoaNegocios().PessoaFisicaUpdate(titular,1))
            {

                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        bool ValidaContato(ContatoController contato)
        {
            if (cmbDiaVencimento.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (contato.TB009_TipoS.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Tipo"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (contato.TB009_Contato.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Contato"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void DTContratoTitularContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ContatoController contato = new ContatoController();
                ContatoNegocios contatoN = new ContatoNegocios();

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    contato.TB009_id = Convert.ToInt64(DTContratoTitularContatos.Rows[e.RowIndex]
                        .Cells["txtContratoTitularContatoiD"].Value);

                    switch (DTContratoTitularContatos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Excluir":

                            if (contato.TB009_id > 0)
                            {
                                contato.TB009_Contato = DTContratoTitularContatos.Rows[e.RowIndex]
                                    .Cells["txtContratoTitularContato"].Value.ToString();
                                contatoN.contatosContratoExcluir(contato, Convert.ToInt64(lblContrato.Text),
                                    ParametrosInterface.objUsuarioLogado.TB011_Id);
                                if (DTContratoTitularContatos.CurrentRow != null)
                                    DTContratoTitularContatos.Rows.RemoveAt(DTContratoTitularContatos.CurrentRow.Index);
                            }
                            else
                            {
                                if (DTContratoTitularContatos.CurrentRow != null)
                                    DTContratoTitularContatos.Rows.RemoveAt(DTContratoTitularContatos.CurrentRow.Index);
                            }

                            break;
                        case "Salvar":
                            PessoaController objPessoa = new PessoaController();
                            contato.Pessoa = objPessoa;

                            contato.Pessoa.TB013_id = Convert.ToInt64(lblContratoTitularID.Text);
                            contato.TB009_TipoS = Convert.ToString(DTContratoTitularContatos.Rows[e.RowIndex]
                                .Cells["cmbContratoTitularContatoTipo"].Value);

                            if (Convert.ToInt16(contato.TB009_TipoS) == 3) //Email
                            {
                                contato.TB009_Contato = Convert.ToString(DTContratoTitularContatos.Rows[e.RowIndex]
                                    .Cells["txtContratoTitularContato"].Value);
                            }
                            else
                            {
                                string sContato = Convert
                                    .ToString(DTContratoTitularContatos.Rows[e.RowIndex]
                                        .Cells["txtContratoTitularContato"].Value).Replace("-", "")
                                    .Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');


                                if (Convert.ToInt16(contato.TB009_TipoS) < 4)
                                {
                                    if (sContato.Length == 10)
                                    {
                                        contato.TB009_Contato = Convert.ToUInt64(sContato).ToString(@"(00\)0000\-0000");
                                    }
                                    else
                                    {
                                        if (sContato.Length == 11)
                                        {
                                            contato.TB009_Contato =
                                                Convert.ToUInt64(sContato).ToString(@"(00\)000\-000\-000");
                                        }
                                        else
                                        {
                                            if (sContato.Length > 11)
                                            {
                                                MessageBox.Show(MensagensDoSistema._0055, @"Erro", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                                            }
                                        }
                                    }

                                    DTContratoTitularContatos.Rows[e.RowIndex].Cells[2].Value = contato.TB009_Contato;
                                }
                            }

                            contato.TB009_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            contato.TB009_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            contato.TB009_Nota = "Cadastrado via APP";

                            if (ValidaContato(contato))
                            {
                                if (contato.TB009_id > 0)
                                {
                                    contatoN.contatosContratoUpdate(contato);
                                }
                                else
                                {
                          
                                    DTContratoTitularContatos.Rows[e.RowIndex].Cells[0].Value =
                                        contatoN.contatosContratoInsert(contato, Convert.ToInt64(lblContrato.Text),
                                            ParametrosInterface.objUsuarioLogado.TB011_Id);
                                }
                            }

                            MessageBox.Show(MensagensDoSistema._0013, @"Atualização", MessageBoxButtons.OK,
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
        private void PreencherAnotacoes(long Tb012Id)
        {
            try
            {
                dgAnotacoes.AutoGenerateColumns = false;
                dgAnotacoes.DataSource = null;
                dgAnotacoes.DataSource = new AnotacoesNegocios().AnotacoesDoContrato(Tb012Id,"00000",1);
                dgAnotacoes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgAnotacoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgAnotacoes.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":

                            Anotacao(Convert.ToInt64(dgAnotacoes.Rows[e.RowIndex].Cells["Tb026Id"].Value));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Anotacao(long tb026Id)
        {
            try
            {
                var anotacao = new AnotacoesNegocios().AnotacaoSelect(tb026Id);
                lblTb026Id.Text = anotacao.Tb026Id.ToString();
                lblTb011NomeExibicao.Text = anotacao.Tb011NomeExibicao;
                lblTb026Data.Text = anotacao.Tb026Data.ToString("dd//MM/yyyy HH:mm");
                txtTb026Anotacao.Text = anotacao.Tb026Anotacao.TrimEnd();
                txtTb026Anotacao.ReadOnly = true;

                grpAnotacao.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuAnotacoesSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTb026Anotacao.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Anotação"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTb026Anotacao.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(lblTb026Id.Text.Trim())) return;
            var anotacao = new AnotacoesController
            {
                Tb012Id = Convert.ToInt64(lblContrato.Text),
                Tb011Id = ParametrosInterface.objUsuarioLogado.TB011_Id,
                Tb026Data = DateTime.Now,
                Tb026Cod="00000",
                TB026_Negociacao = 1,
                Tb026Anotacao = txtTb026Anotacao.Text
            };

            var retorno = new AnotacoesNegocios().Anotacaoinsert(anotacao);
            if (retorno <= 0) return;
            txtTb026Anotacao.ReadOnly = true;
            Anotacao(retorno);
            PreencherAnotacoes(Convert.ToInt64(lblContrato.Text));
            MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            
            grpAnotacao.Visible = false;
            novaAnotaçãoToolStripMenuItem.Visible = true;
        }
        private void mnuAnotacoesNovo_Click(object sender, EventArgs e)
        {
            lblTb026Id.Text = "";
            lblTb011NomeExibicao.Text = "";
            lblTb026Data.Text = "";
            txtTb026Anotacao.Text = "";
            txtTb026Anotacao.ReadOnly = false;
        }
        private void fecharToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PreencherAnotacoes(Convert.ToInt64(lblContrato.Text));
            grpAnotacao.Visible = false;
            novaAnotaçãoToolStripMenuItem.Visible = true;

        }
        private void novaAnotaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblTb026Id.Text = "";
            lblTb011NomeExibicao.Text = "";
            lblTb026Data.Text = "";
            txtTb026Anotacao.Text = "";
            txtTb026Anotacao.ReadOnly = false;
            grpAnotacao.Visible = true;
            novaAnotaçãoToolStripMenuItem.Visible = false;
        }
        private void CarregarParcelasVencidas()
        {
            ddtParcelas.AutoGenerateColumns = false;
            ddtParcelas.DataSource = null;

            ddtParcelas.DataSource = new ParcelaNegocios().ListaParcelasVencidas(Convert.ToInt64(lblContrato.Text)); 
            ddtParcelas.Refresh();

            double Valor = 0;
            for (int i = 0; i < ddtParcelas.RowCount; i++)
            {
                double Valor1 = Convert.ToDouble(ddtParcelas.Rows[i].Cells["TB016_Valor"].Value.ToString().Replace("R$", ""));
                Valor = Valor + Valor1;
            }
            lblValorEmAtraso.Text = Valor.ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);

        }
        private void ddtParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                            SelecionarParcela(Convert.ToInt64(ddtParcelas.Rows[e.RowIndex].Cells["TB016_id"].Value));
                }
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
            ddtParcelaVencimento.Enabled = false;

            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {

                txtParcelaDesconto.Enabled = true;
                ddtParcelaVencimento.Enabled = true;
            }
            lblParcelaPlanoId.Text = parcela.TB015_id.ToString();
            lblParcelaPlano.Text = parcela.TB015_Plano;
            lblParcelaId.Text = parcela.TB016_id.ToString();
            ddtParcelaVencimento.Value = parcela.TB016_Vencimento;
            lblParcelaValorTotal.Text = Format("{0:C2}",
                Convert.ToDouble(parcela.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));


            cmbFormaPagamento.SelectedValue = parcela.TB016_FormaPagamentoS;

            if (Convert.ToInt16(
                    (int)((ParcelaController.TB016_StatusE)Enum.Parse(
                        typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
                cmbFormaPagamento.Enabled = true;
            }

            lblParcelaProdutoId.Text = "";
            lblParcelaProduto.Text = "";

            if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 1)
            {
                if (parcela.TB016_Boleto.Replace(" ", "") == "---")
                {
                    picBoletoVisualizar.Visible = false;
                }
                else
                {
                    picBoletoVisualizar.Image =
                        Properties.Resources.Boletos_30_30;
                    picBoletoVisualizar.Visible = true;
                }
            }
            else
            {
                if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 3)
                {
                    picBoletoVisualizar.Image =
                        Properties.Resources.Dinheiro_30_30;
                    picBoletoVisualizar.Visible = true;

                }
                else
                {
                    if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) > 3)
                    {
                        picBoletoVisualizar.Image =
                            Properties.Resources.Cartao_30_30;
                        picBoletoVisualizar.Visible = true;
                    }
                    else if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 2)
                    {
                        picBoletoVisualizar.Image =
                            Properties.Resources.Dinheiro_30_30;
                        picBoletoVisualizar.Visible = true;
                    }
                }
            }
            ddtParcelaItens.AutoGenerateColumns = false;
            ddtParcelaItens.DataSource = null;
            ddtParcelaItens.DataSource = parcelasProdutos;
            ddtParcelaItens.Refresh();
        }
        private void ddtParcelaItens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                            var parcelaProduto = new ParcelaNegocios().ParcelaProdutoPesquisaId(Convert.ToInt64(ddtParcelaItens.Rows[e.RowIndex].Cells["TB017_id"].Value));
                            lblParcelaProdutoId.Text = parcelaProduto.TB017_id.ToString();
                            lblParcelaProduto.Text = parcelaProduto.TB017_Item;
                            txtParcelaValorUnitario.Text = double.Parse(parcelaProduto.TB017_ValorUnitario.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                            txtParcelaDesconto.Text = double.Parse(parcelaProduto.TB017_ValorDesconto.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                            txtParcelaSubTotal.Text = double.Parse(parcelaProduto.TB017_ValorFinal.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuContratoSimulacao_Click(object sender, EventArgs e)
        {

            if(Convert.ToDouble(lblValorEmAtraso.Text.Replace("R$",""))==0)
            {
                MessageBox.Show(MensagensDoSistema._0088, @"Erro", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Add(tbSimulacao);
            DadosParaSimulacao();
        }
        private void mnuSimulacaoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbSimulacao);
            textBox1.Text               = @"0";
            textBox2.Text               = @"0";
            lblParcelasEmAtrazo.Text    = @"0";
            tabPrincipal.TabPages.Add(tbContrato);
        }
        private void cmbCondicoes_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbCondicoes.SelectedIndex>0)
            { 
            try
            {
                cmbNumeroParclas.Items.Clear();
                lblCondicoeValorMinimoParcela.Text      = @"R$ 0,00";

                if (Convert.ToInt64(cmbCondicoes.SelectedValue)==0)
                {
                    lblCondicaoDescricao.Text           = "";
                    lbAdesaolDescJurosAliquota.Text     = @"0%";
                    lbMensalidadeDescJurosAliquota.Text = @"0%";
                    lbMensalidadeDescMultaAliquota.Text = @"0%";
                    lbAdesaoDescMultaAliquota.Text      = @"0%";
                    lbAdesaoDescAdesao.Text             = @"0";
                    lbMensalidadeDescOutros.Text        = @"0";
                }
                else
                {
                    var Condicao = new NegociacaoCondicaoNegocios().NegociacaoCondicaoId(Convert.ToInt64(cmbCondicoes.SelectedValue));
                    lblCondicaoDescricao.Text = Condicao.TB036_Descricao;
                    lbMensalidadeDescJurosAliquota.Text = Condicao.TB036_DescontoJurosMensalidade.ToString() + "%";
                    lbMensalidadeDescMultaAliquota.Text = Condicao.TB036_DescontoMultaMensalidade.ToString() + "%";
                    lbAdesaoDescAdesao.Text             = @"0";
                    lbMensalidadeDescOutros.Text        = Condicao.TB036_DescontoParcela.ToString();
                    lbAdesaoDescAdesao.Text             = Condicao.TB036_DescontoAdesao.ToString();
                    lblCondicoeValorMinimoParcela.Text  = Condicao.TB036_ValorMinimoParcela.ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                    cmbNumeroParclas.Items.Add(0);
                    for (int i = Condicao.TB036_NParcelasMinimo; i <  Condicao.TB036_NParcelasMaximo+1;)
                    {
                        cmbNumeroParclas.Items.Add(i);
                        i++;
                    }

                    cmbNumeroParclas.SelectedIndex = 0;
                    CalcularDescontos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
        }
        private void lbMensalidadeDescMultaAliquota_Click(object sender, EventArgs e)
        {

        }
        private void DadosParaSimulacao()
        {

            lblValorAdesao.Text = @"R$ 0,00";
            lblValorMensalidade.Text = @"R$ 0,00";
            try
            {
                var negociacao = new ParcelaNegocios().NegociacaoSimulacao(Convert.ToInt64(lblContrato.Text));
                lblValorAdesao.Text = negociacao.TB016_ValorAdesao.ToString("C2").ToString(CultureInfo.InvariantCulture);
                lblAdesaoJuroAliquota.Text = negociacao.TB016_Juros.ToString() + "% (a.m)";
                lblAdesaoMultaAliquota.Text = negociacao.TB016_Multa.ToString() + "%";
                label64.Text = (negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Juros * negociacao.TB016_NParcelasAtrazo).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label49.Text = (negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Multa).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label72.Text = (negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Multa + negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Juros).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label27.Text = (negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Multa + negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Juros+ negociacao.TB016_ValorAdesao).ToString("C2").ToString(CultureInfo.InvariantCulture);
                lblValorMensalidade.Text = negociacao.TB016_ValorMensalidades.ToString("C2").ToString(CultureInfo.InvariantCulture);
                lblAMensalidadeJuroAliquota.Text = negociacao.TB016_Juros.ToString() + "% (a.m)";
                lblMesalidadeMultaAliquota.Text = negociacao.TB016_Multa.ToString() + "%";
                label89.Text = (negociacao.TB016_ValorMensalidades /100 * negociacao.TB016_Multa).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label66.Text = (negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Juros* negociacao.TB016_NParcelasAtrazo).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label23.Text = (negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Juros * negociacao.TB016_NParcelasAtrazo + negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Multa).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label47.Text = (negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Juros * negociacao.TB016_NParcelasAtrazo + negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Multa + negociacao.TB016_ValorMensalidades).ToString("C2").ToString(CultureInfo.InvariantCulture);
                label54.Text = (negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Juros * negociacao.TB016_NParcelasAtrazo + negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Multa).ToString("C2").ToString(CultureInfo.InvariantCulture);
                lblValorTotalCorrigido.Text = (negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Juros * negociacao.TB016_NParcelasAtrazo + negociacao.TB016_ValorMensalidades / 100 * negociacao.TB016_Multa + negociacao.TB016_ValorMensalidades + negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Multa + negociacao.TB016_ValorAdesao / 100 * negociacao.TB016_Juros + negociacao.TB016_ValorAdesao).ToString("C2").ToString(CultureInfo.InvariantCulture);      
                lblParcelasEmAtrazo.Text = negociacao.TB016_NParcelasAtrazo.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcularDescontos()
        {
            label73.Text                = "R$ 0,00";
            label74.Text                = "R$ 0,00";
            label78.Text                = "R$ 0,00";
            label75.Text                = "R$ 0,00";
            label76.Text                = "R$ 0,00";
            label53.Text                = "R$ 0,00";
            label80.Text                = "R$ 0,00";
            label81.Text                = "R$ 0,00";
            label83.Text                = "R$ 0,00";
            label82.Text                = "R$ 0,00";
            label81.Text                = "R$ 0,00";
            label83.Text                = "R$ 0,00";
            label82.Text                = "R$ 0,00";
            label51.Text                = "R$ 0,00";
            lblTotalDescontos.Text      = "R$ 0,00";
            label29.Text                = "R$ 0,00";
            label30.Text                = "R$ 0,00";
            label40.Text                = "R$ 0,00";
            label85.Text                = "R$ 0,00";
            label57.Text                = "R$ 0,00";
           // lblParcelasEmAtrazo.Text    = "0";
            textBox1.Text               = "0";
            textBox2.Text               = "0";
            try
            {
                /*Adesão*/
                label73.Text = (Convert.ToDouble(label64.Text.Replace("R$", "")) / 100 * Convert.ToDouble(lbAdesaolDescJurosAliquota.Text.Replace("%", ""))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                label75.Text = (Convert.ToDouble(label49.Text.Replace("R$", "")) / 100 * Convert.ToDouble(lbAdesaoDescMultaAliquota.Text.Replace("%", ""))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                /*Mensalidade*/
                label74.Text = (Convert.ToDouble(label66.Text.Replace("R$", "")) / 100 * Convert.ToDouble(lbMensalidadeDescJurosAliquota.Text.Replace("%", ""))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                label76.Text= (Convert.ToDouble(label89.Text.Replace("R$", "")) / 100 * Convert.ToDouble(lbMensalidadeDescMultaAliquota.Text.Replace("%", ""))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                label78.Text = (Convert.ToDouble(label73.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label74.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                label53.Text = (Convert.ToDouble(label76.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label75.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
                ValorFinal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbNumeroParclas_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            CalcularParcelas();
        }

        private void CalcularParcelas()
        {
            if (Convert.ToInt64(cmbCondicoes.SelectedValue) > 0 && Convert.ToInt64(cmbNumeroParclas.SelectedIndex) > 0)
            {
                try
                {
                    ParcelasNegociadas.Clear();
                    short nParcelas = Convert.ToInt16(cmbNumeroParclas.Text);
                    DateTime vendimento = DateTime.Now.AddDays(2);

                    var objTitular = new PessoaNegocios().pessoaSelectId(Convert.ToInt64(lblContratoTitularID.Text));
                    var pontoDevendaEmpresa = new PontoDeVendaNegocios().PontoDeVendaEmpresa(Convert.ToInt64(lblPontoVenda.Text));

                    var ProdutosAtrasados = new ParcelaNegocios().NegociacaoSimulacao(Convert.ToInt64(lblContrato.Text));

                    for (int i = 0; i < nParcelas; i++)
                    {
                        var ParcelaNova = new ParcelaController
                        {
                            Pessoa = new PessoaController
                            {
                                Municipio = new MunicipioController { Estado = new EstadoController() }
                            }
                        };


                        ParcelaNova.Plano = new PlanoController();
                        ParcelaNova.Empresa = new EmpresaController { TB001_id = pontoDevendaEmpresa.Empresa.TB001_id };
                        ParcelaNova.Titular = objTitular;
                        ParcelaNova.TB033_Parcela = Convert.ToInt16(i) + 1;
                        ParcelaNova.TB016_TotalParcelas = nParcelas;
                        ParcelaNova.TB016_Emissao = DateTime.Now;
                        ParcelaNova.TB016_Vencimento = vendimento;
                        ParcelaNova.TB016_DiaFechamento = vendimento.Day - 2;
                        ParcelaNova.TB016_ValorOutrosDesconto = Convert.ToDouble(lblTotalDescontos.Text.Replace("R$", "").Replace(".", ",")) / Convert.ToInt16(cmbNumeroParclas.Text);
                        vendimento = new DateTime(vendimento.Year, vendimento.Month, vendimento.Day);
                        vendimento = vendimento.AddMonths(1);
                        ParcelaNova.TB016_Beneficiario = pontoDevendaEmpresa.Empresa.TB001_RazaoSocial;
                        ParcelaNova.TB016_ParcelasAgrupadas = 1;
                        ParcelaNova.TB016_BeneficiarioCidade = pontoDevendaEmpresa.Empresa.Cidade;
                        ParcelaNova.TB016_BeneficiarioCPFCNPJ = pontoDevendaEmpresa.Empresa.TB001_CNPJ;
                        ParcelaNova.TB016_BeneficiarioEndereco = pontoDevendaEmpresa.Empresa.TB001_Logradouro;
                        ParcelaNova.TB016_BeneficiarioUF = pontoDevendaEmpresa.Empresa.TB001_UF;
                        ParcelaNova.TB016_CadastradoEm = DateTime.Now;
                        ParcelaNova.TB016_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        ParcelaNova.TB016_AlteradoEm = DateTime.Now;
                        ParcelaNova.TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        ParcelaNova.Pessoa.Municipio.TB006_Municipio = objTitular.Municipio.TB006_Municipio.TrimEnd().ToUpper();
                        ParcelaNova.Pessoa.Municipio.Estado.TB005_Estado = objTitular.Municipio.Estado.TB005_Sigla;
                        ParcelaNova.TB016_CPFCNPJ = objTitular.TB013_CPFCNPJ;
                        ParcelaNova.TB016_Pagador = objTitular.TB013_NomeCompleto.TrimEnd().ToUpper().Replace("'", "/")
                                                                            .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                        ParcelaNova.TB016_PagadorCEP = objTitular.TB004_Cep;
                        ParcelaNova.TB016_PagadorCidade = objTitular.Municipio.TB006_Municipio.TrimEnd().ToUpper().Replace("'", "/")
                                                                            .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                        ParcelaNova.TB016_PagadorUF = objTitular.Municipio.Estado.TB005_Sigla.ToUpper().Replace("'", "/")
                                                                            .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                        ParcelaNova.TB012_id = Convert.ToInt64(lblContrato.Text);
                        ParcelaNova.TB016_EnderecoPagador = objTitular.TB013_Logradouro.TrimEnd().ToUpper() + ", " +
                                                                            objTitular.TB013_Numero.TrimEnd().ToUpper();
                        ParcelaNova.TB016_EnderecoPagador = ParcelaNova.TB016_EnderecoPagador.Replace("'", "/").Replace("*", "")
                                                                            .Replace("%", "/").Replace("&", "E").Replace("#", "");
                        ParcelaNova.TB012_CicloContrato = Convert.ToInt32(lblCiclo.Text);
                        ParcelaNova.TB016_FormaPagamentoS = "1";

                        ParcelaNova.TB016_EmitirBoleto = 1;
                        ParcelaNova.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(0));
                        var participantesL = new PessoaNegocios().MembrosAtivosDoConrato(Convert.ToInt64(lblContrato.Text),
                                ParcelaNova.TB016_Vencimento);
                        var dadosFiltroIdade = new CategoriaIdadeNegocios().DistribuicaoIsencaoIdade(participantesL);

                        //Localiar Plano's conforme itens de filtro
                        var plano = new PlanoNegocios().PlanoNegociacao();

                        ParcelaNova.Plano.TB015_IOF = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_IOF"].ToString());
                        ParcelaNova.TB016_TipoVencimento = Convert.ToInt16(plano.Tables[0].Rows[0]["TB015_TipoVencimento"].ToString());
                        ParcelaNova.TB016_EspecieDocumento = plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                        ParcelaNova.TB016_BoletoDesc1 = plano.Tables[0].Rows[0]["TB015_BoletoDesc1"].ToString();
                        ParcelaNova.TB016_BoletoDesc2 = plano.Tables[0].Rows[0]["TB015_BoletoDesc2"].ToString();
                        ParcelaNova.TB016_BoletoDesc3 = plano.Tables[0].Rows[0]["TB015_BoletoDesc3"].ToString();
                        ParcelaNova.TB016_BoletoDesc4 = plano.Tables[0].Rows[0]["TB015_BoletoDesc4"].ToString();
                        ParcelaNova.TB016_BoletoDesc5 = plano.Tables[0].Rows[0]["TB015_BoletoDesc5"].ToString();
                        ParcelaNova.TB016_Multa = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_Multa"].ToString());
                        ParcelaNova.TB016_Juros = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_Juros"].ToString());


                        ParcelaNova.TB016_EspecieDocumento = plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                        double ValorParcela = Convert.ToDouble(label40.Text.Replace("R$", "").Replace(".", ",")) / nParcelas;


                        double ValorMinimo = Convert.ToDouble(lblCondicoeValorMinimoParcela.Text.Replace("R$", "").Replace(".", ","));
                        if (ValorParcela < ValorMinimo)
                        {

                            MessageBox.Show(MensagensDoSistema._0087, @"Contrato", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                            ParcelasNegociadas.Clear();

                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = ParcelasNegociadas;
                            dataGridView1.Refresh();
                            return;
                        }

                        ParcelaNova.TB016_Valor = ValorParcela;
                        ParcelaNova.TB016_ValorBruto = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(lblValorTotalCorrigido.Text.Replace("R$", "")) / Convert.ToInt16(cmbNumeroParclas.Text)));
                        if (ParametrosInterface.objUsuarioLogado.TB037_TipoComissao == 1)
                        {
                            ParcelaNova.TB037_Comissao = ParametrosInterface.objUsuarioLogado.TB037_Aliquota / ParcelaNova.TB016_Valor * 100;
                        }
                        else
                        {
                            if (ParametrosInterface.objUsuarioLogado.TB037_TipoComissao == 2)
                            {
                                ParcelaNova.TB037_Comissao = ParametrosInterface.objUsuarioLogado.TB037_Valor;
                            }
                        }
                        ParcelaNova.TB015_id = Convert.ToInt64(plano.Tables[0].Rows[0]["TB015_id"].ToString());
                        ParcelaNova.TB015_Plano = plano.Tables[0].Rows[0]["TB015_Plano"].ToString();
                        ParcelaNova.TB016_LoteExportacao = -1;
                        ParcelaNova.TB037_Id = ParametrosInterface.objUsuarioLogado.TB037_Id;

                        var parcelaItensL = new List<ParcelaProdutosController>();
                        int divDesconto = 0;
                        foreach (var produto in ProdutosAtrasados.ParcelaProduto_L)
                        {
                           if(produto.TB017_ValorFinal>0)
                            {
                                divDesconto++;
                            }


                        }


                        foreach (var produto in ProdutosAtrasados.ParcelaProduto_L)
                        {
                            ParcelaProdutosController parcelaItem = new ParcelaProdutosController();

                            parcelaItem.TB017_TipoS         = "4";
                            parcelaItem.TB017_Item          = "Negociação " + produto.TB017_Item + " Ref. Parcela " + produto.TB016_id;
                            parcelaItem.TB017_ValorUnitario = produto.TB017_ValorFinal;
                            
                            if(produto.TB017_ValorFinal>0)
                            {
                                double desconto = (Convert.ToDouble(label83.Text.Replace("R$", "").Replace(".", ",")) / divDesconto);
                                parcelaItem.TB017_ValorDesconto =  desconto;


                            }
                            else
                            {
                                parcelaItem.TB017_ValorDesconto = parcelaItem.TB017_ValorUnitario - parcelaItem.TB017_ValorFinal;
                            }

                            parcelaItem.TB017_ValorFinal = parcelaItem.TB017_ValorUnitario- parcelaItem.TB017_ValorDesconto / nParcelas;

                            parcelaItensL.Add(parcelaItem);
                        

                        }

                        ParcelaNova.ParcelaProduto_L = parcelaItensL;
                        ParcelasNegociadas.Add(ParcelaNova);
                    }


                    label85.Text = ParcelasNegociadas[0].TB016_Valor.ToString("C2").ToString(CultureInfo.InvariantCulture);

                    /**************************/
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ParcelasNegociadas;
                    dataGridView1.Refresh();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void mnuSimulacaoConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if(ParcelasNegociadas.Count>0)
                {
                    if (new ParcelaNegocios().FamiliarParcelaInsert(ParcelasNegociadas))
                        new ContratoNegocios().ContratoNegociado(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id, ParametrosInterface.objUsuarioLogado.TB037_Id);

                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);

                    tabPrincipal.TabPages.Remove(tbSimulacao);
                    tabPrincipal.TabPages.Add(tbContrato);
                    CarregarParcelas();
                    CarregarParcelasVencidas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarParcelas()
        {
            ddtParcelasContratos.AutoGenerateColumns = false;

            ddtParcelasContratos.DataSource =
                   new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                       Convert.ToInt64(lblCiclo.Text), -1);
            ddtParcelasContratos.Refresh();
        }
        private void mnuContratoEmitirBoletos_Click(object sender, EventArgs e)
        {
            try
            {
                new ParcelaNegocios().ParcelasParaEmissaoBoleto(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id,1);
                /**/
                ddtParcelasContratos.AutoGenerateColumns = false;
                ddtParcelasContratos.DataSource = null;
                if (cmbDiaVencimento.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"), @"Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var parcelas =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);

                CarregarParcelas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ddtParcelasContratos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                int value;
                if (e.Value == null || !int.TryParse(e.Value.ToString(), out value)) return;
                string formaPagamento = ddtParcelasContratos.Rows[e.RowIndex].Cells["FormaPagamento"].Value.ToString();
                if (formaPagamento.Where(char.IsNumber).Any())
                {
                    ddtParcelasContratos.Rows[e.RowIndex].Cells["FormaPagamento"].Value = Enum.GetName(
                        typeof(ParcelaController.TB016_FormaPagamentoE), Convert.ToInt16(formaPagamento));

                }
                if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                        typeof(ParcelaController.TB016_StatusE),
                        ddtParcelasContratos.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) == 2)
                {
                    ddtParcelasContratos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                            typeof(ParcelaController.TB016_StatusE),
                            ddtParcelasContratos.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) == 4)
                    {
                        ddtParcelasContratos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                                typeof(ParcelaController.TB016_StatusE),
                                ddtParcelasContratos.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) == 5)
                        {
                            ddtParcelasContratos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        }
                        else
                        {
                            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                                    typeof(ParcelaController.TB016_StatusE),
                                    ddtParcelasContratos.Rows[e.RowIndex].Cells["TB016_StatusS"].Value.ToString()))) ==
                                3)
                            {
                                ddtParcelasContratos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
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
        private void ddtParcelasContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    cmbFormaPagamento.Enabled = false;
                    switch (ddtParcelasContratos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":

                            SelecionarParcelaContrato(Convert.ToInt64(ddtParcelasContratos.Rows[e.RowIndex].Cells["TB016_idContrato"].Value));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SelecionarParcelaContrato(long idparcela)
        {
            var parcela =
                new ParcelaNegocios().ParcelaPesquisaId(idparcela);
            var parcelasProdutos = parcela.ParcelaProduto_L;
            picBoletoVisualizarContrato.Enabled = false;
            txtParcelaDesconto.Enabled = false;
            ddtParcelaVencimento.Enabled = false;


            lblParcelaStatusSContrato.Text = parcela.TB016_StatusS;

            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
             picBoletoVisualizarContrato.Enabled = true;

            }

            lblParcelaIdContrato.Text = parcela.TB016_id.ToString();
            ddtParcelaVencimentoContrato.Value = parcela.TB016_Vencimento;
            lblParcelaValorTotalContrato.Text = Format("{0:C2}",
                Convert.ToDouble(parcela.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));


            cmbFormaPagamentoContrato.SelectedValue = parcela.TB016_FormaPagamentoS;

            if (Convert.ToInt16(
                    (int)((ParcelaController.TB016_StatusE)Enum.Parse(
                        typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
                cmbFormaPagamentoContrato.Enabled = true;
            }


            if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 1)
            {
                if (parcela.TB016_Boleto.Replace(" ", "") == "---")
                {
                    picBoletoVisualizarContrato.Visible = false;
                }
                else
                {
                    picBoletoVisualizarContrato.Image =
                        Properties.Resources.Boletos_30_30;
                    picBoletoVisualizarContrato.Visible = true;
                }
            }
            else
            {
                if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 3)
                {
                    picBoletoVisualizarContrato.Image =
                        Properties.Resources.Dinheiro_30_30;
                    picBoletoVisualizarContrato.Visible = true;

                }
                else
                {
                    if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) > 3)
                    {
                        picBoletoVisualizarContrato.Image =
                            Properties.Resources.Cartao_30_30;
                        picBoletoVisualizarContrato.Visible = true;
                    }
                    else if (Convert.ToInt16(parcela.TB016_FormaPagamentoS) == 2)
                    {
                        picBoletoVisualizarContrato.Image =
                            Properties.Resources.Dinheiro_30_30;
                        picBoletoVisualizarContrato.Visible = true;
                    }
                }
            }
        }
        private void pctAlterarDadosParcela_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusSContrato.Text))) > 1)
                {
                    MessageBox.Show(MensagensDoSistema._0080, @"Erro", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show(@"Deseja alterar a informações da parcela?", @"Parcelas", MessageBoxButtons.YesNo) ==
                    DialogResult.No)
                {
                    return;
                }

                if (new ParcelaNegocios().AlterarFormaPagamento(Convert.ToInt64(lblParcelaIdContrato.Text),
                    Convert.ToInt16(cmbFormaPagamentoContrato.SelectedValue), ParametrosInterface.objUsuarioLogado.TB011_Id,
                    Convert.ToInt64(lblContrato.Text), ddtParcelaVencimentoContrato.Value))
                {


                    ddtParcelasContratos.AutoGenerateColumns = false;
                    ddtParcelasContratos.DataSource = null;


                    if (cmbDiaVencimento.Text.Trim() == Empty)
                    {
                        MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"), @"Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    CarregarParcelas();
                }

                SelecionarParcela(Convert.ToInt64(lblParcelaIdContrato.Text));
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void picBoletoVisualizarContrato_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbFormaPagamentoContrato.SelectedValue) == 1)
                {
                    var boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(lblCiclo.Text));

                    tabPrincipal.TabPages.Add(tbBoletos);
                    tabPrincipal.TabPages.Remove(tbContrato);
                    _doc = webBoleto.Document;
                    if (_doc != null)
                    {
                        if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[0].TB016_Boleto;
                        _doc.Title = boletosEmitidos[0].TB016_id.ToString();
                    }
                    lblBoletosQuant.Text = @"0";
                }
                else
                {
                    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusSContrato.Text))) == 1 || Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusSContrato.Text))) == 2)
                    {
                        tabPrincipal.TabPages.Add(tbContrato);
                    }
                    else
                    {
                        try
                        {
                       
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuBoletoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbContrato);
            tabPrincipal.TabPages.Remove(tbBoletos);
        }
        private void btnBoletoProximo_Click(object sender, EventArgs e)
        {
            List<ParcelaController> boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(lblCiclo.Text));
            if (boletosEmitidos.Count > 1)
            {
                try
                {
                    if (Convert.ToInt16(lblBoletosQuant.Text) < boletosEmitidos.Count)
                    {
                        int cont = Convert.ToInt16(lblBoletosQuant.Text) + 1;
                        _doc = webBoleto.Document;
                        if (_doc != null)
                        {
                            if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[cont].TB016_Boleto;
                            _doc.Title = boletosEmitidos[cont].TB016_id.ToString();
                        }
                        lblBoletosQuant.Text = cont.ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnBoletoAnterior_Click(object sender, EventArgs e)
        {
            List<ParcelaController> boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(lblCiclo.Text));
            if (Convert.ToInt16(lblBoletosQuant.Text) > 0)
            {
                int cont = Convert.ToInt16(lblBoletosQuant.Text) - 1;
                _doc = webBoleto.Document;
                if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[cont].TB016_Boleto;
                _doc.Title = boletosEmitidos[cont].TB016_id.ToString();
                lblBoletosQuant.Text = cont.ToString();
            }
        }
        private void mnuImprimirAtual_Click(object sender, EventArgs e)
        {
            webBoleto.Print();
            MessageBox.Show(@"Documento enviado para impressora", @"Impressão", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void mnuBoletoImprimirTodos_Click(object sender, EventArgs e)
        {
            List<ParcelaController> boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(lblCiclo.Text));
            for (int i = 0; i < boletosEmitidos.Count; i++)
            {
                _doc = webBoleto.Document;
                Debug.Assert(_doc != null, nameof(_doc) + " != null");
                if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[i].TB016_Boleto;
                _doc.Title = boletosEmitidos[i].TB016_id.ToString();
                webBoleto.Print();
            }
        }
        private void mnuBoletoImprimirConfigurar_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            printDialog1.ShowDialog();
        }
        private void mnuBoletoImpressorar_Click(object sender, EventArgs e)
        {
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                // Exibe o nome da impressora
                Console.WriteLine(@"Impressora: {0}", printerName);

                // Retorna as configurações da impressora
                PrinterSettings printer = new PrinterSettings { PrinterName = printerName };

                if (printer.IsValid)
                {
                    // Exibe a lista de resoluções válidas
                    Console.WriteLine(@"Resoluções suportadas:");

                    foreach (PrinterResolution resolution in printer.PrinterResolutions)
                    {
                        Console.WriteLine(@"{0}", resolution);
                    }
                    Console.WriteLine();

                    // Exibe uma lista de tamanho de papeis validos
                    Console.WriteLine(@"Tamanho de papeis suportados:");

                    foreach (PaperSize size in printer.PaperSizes)
                    {
                        if (Enum.IsDefined(size.Kind.GetType(), size.Kind))
                        {
                            Console.WriteLine(@"{0}", size);
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
        private void mnuBoletoImprimir_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            Debug.Assert(webBoleto.Document != null, "webBoleto.Document != null");


            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        private void mnuBoletoImprimirVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                WebBrowser wbPrintString = new WebBrowser() { DocumentText = Empty };
                if (wbPrintString.Document != null)
                {
                    if (webBoleto.Document != null)
                    {
                        wbPrintString.Document.Write(webBoleto.Document.Body?.InnerHtml);
                        wbPrintString.Document.Title = "Boleto Clube Conteza";
                    }
                }
                Microsoft.Win32.RegistryKey rgkySetting = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", true);
                if (rgkySetting != null)
                {
                    rgkySetting.SetValue("footer", DateTime.Now);
                    rgkySetting.Close();
                }
                wbPrintString.Parent = this;
                wbPrintString.ShowPrintPreviewDialog();
                wbPrintString.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(@"Erro : " + exp.Message);
            }
        }
        private void carneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbCarne);

            var gravarCodigoBarraCarne = new ParcelaNegocios().GravarCodigoBarraCarne(Convert.ToInt64(lblContrato.Text));

            userControl11.BarCodeHeight = 80;
            userControl11.Font = new Font("Interleaved 2of5 NT", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            userControl11.FooterFont = new Font("Microsoft Sans Serif", 8F);
            userControl11.HeaderFont = new Font("Comic Sans MS", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            userControl11.HeaderText = "";
            userControl11.LeftMargin = 10;
            userControl11.Location = new Point(8, 8);
            userControl11.Name = "userControl11";
            userControl11.ShowFooter = true;
            userControl11.ShowHeader = true;
            userControl11.Size = new Size(336, 99);
            userControl11.TabIndex = 0;
            userControl11.TopMargin = 5;
            userControl11.VertAlign = DSBarCode.BarCodeCtrl.AlignType.Center;
            userControl11.Weight = DSBarCode.BarCodeCtrl.BarCodeWeight.Small;

            foreach (var codBarra in gravarCodigoBarraCarne)
            {
                userControl11.BarCode = codBarra.TB016_NBoleto.Replace(".", "").Replace(" ", "").Trim();
                userControl11.SaveImage(@"C:\\Temp\" + codBarra.TB016_id.ToString() + ".bmp");
            }

            rptCarne.LocalReport.EnableExternalImages = true;

            carneTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            carneTableAdapter.Fill(clubeConteza_Relatorios.Carne, Convert.ToInt64(lblContrato.Text), Convert.ToInt32(lblCiclo.Text));
            rptCarne.RefreshReport();
            tabPrincipal.TabPages.Remove(tbBoletos);
            var setup = rptCarne.GetPageSettings();
            setup.Margins = new Margins(1, 1, 1, 1);
            rptCarne.SetPageSettings(setup);
        }
        private void mnuCarneFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbBoletos);
            tabPrincipal.TabPages.Remove(tbCarne);
        }
        private void enviarEMailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var Contato = new ContatoNegocios().ContatosTitularContratoEmail(Convert.ToInt64(lblContrato.Text));
            label69.Text = "Parcelas Clube Conteza";
            tabPrincipal.TabPages.Add(tbEnviarPorEmail);
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DataSource = Contato;
            comboBox1.DisplayMember = "TB009_Contato";
            comboBox1.ValueMember = "TB009_Contato";

            var Parcelas = new ParcelaNegocios().ListaBoletosParaEnvio(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_ftpServidor, ParametrosInterface.objUsuarioLogado.TB011_ftpUsuario, ParametrosInterface.objUsuarioLogado.TB011_ftpSenha);
            StringBuilder html = new StringBuilder();
            html.Append("<html xmlns= 'http://www.w3.org/1999/xhtml'>");
            html.Append("<head runat = 'server'>");
            html.Append("<meta http-equiv='Content-Type' content='text/html;charset=utf-8'/>");
            html.Append("<title>Parcelas</title>");
            html.Append("<style>");
            html.Append("body");
            html.Append("{");
            html.Append("margin: 0px");
            html.Append("}");
            html.Append(".container");
            html.Append("{");
            html.Append("width: 100%;");
            html.Append("height: 100%;");
            html.Append("background: #EDEDED;");
            html.Append("display: flex;");
            html.Append("flex-direction: row;");
            html.Append("justify-content: center;");
            html.Append("align-items: center");
            html.Append("}");
            html.Append(".box {");
            html.Append(" width: 760px;");
            html.Append(" height: 300px;");
            html.Append(" background: #FFFFFF;");
            html.Append("}");
            html.Append(".auto-style1{");
            html.Append(" text-align: left;");
            html.Append(" }");
            html.Append(".auto-style2{");
            html.Append(" width: 760px;");
            html.Append(" height: 300px;");
            html.Append(" background: #FFFFFF;");
            html.Append(" text-align: center;");
            html.Append(" }");
            html.Append(".auto-style3 {");
            html.Append(" width: 158px;");
            html.Append(" }");
            html.Append("</style>");
            html.Append("</head>");
            html.Append("<body>");
            html.Append("<form id='form1' runat ='server'>");
            html.Append(" <div class='container'>");
            html.Append(" <div class='auto-style2'>");
            html.Append(" <img alt = ''src='http://www.clubeconteza.com.br/img/sis/LogoRPT.png'/><br/>");
            html.Append(" <div style = 'background-color:#F3F3F3; text-align: center;'>");
            html.Append(txtContratoTitularNomeCompleto.Text);
            html.Append("</div>");
            html.Append(" <br/>");
            html.Append(" Segue detalhes dos boletos pendentes em nosso sistema<br/>");
            html.Append(" <br/>");
            html.Append(" <div>");
            html.Append(" <table style = 'padding: 2px; margin: inherit; border: medium solid #000000; width:100%; table-layout: auto; border-spacing: inherit; border-collapse: collapse;'>");
            html.Append(" <tr>");
            html.Append(" <td style= 'border-style: solid; background-color: #4472C4;' class='auto-style3'>Parcela</td>");
            html.Append(" <td style = 'border-style: solid; background-color: #4472C4;'> Vencimento</td>");
            html.Append(" <td style='border-style: solid; background-color: #4472C4;'>Tipo</td>");
            html.Append(" <td style='border-style: solid; background-color: #4472C4;'>Link para Download</td>");
            html.Append(" </tr>");

            int linha = 0;

                foreach (ParcelaController parcela in Parcelas)
                {
                    html.Append("<tr>");
                    if (linha == 0)
                    {
                        html.Append("<td style ='border-style: solid; background-color: #D9E2F3;' class='auto-style3'>");
                    }
                    else
                    {
                        html.Append("<td style ='border-style: solid; background-color: #FFFFFF;' class='auto-style3'>");
                    }

                html.Append(parcela.TB016_id);


                html.Append(" </td>");
                /***/

                if (linha == 0)
                {
                    html.Append("<td style = 'border-style: solid; background-color: #D9E2F3;'> ");
                }
                else
                {
                    html.Append("<td style = 'border-style: solid; background-color: #FFFFFF;'> ");
                }
           
                html.Append(parcela.TB016_Vencimento.ToString("dd/MM/yyyy"));
                html.Append("</td>");

                /********/

                if (linha == 0)
                {
                    html.Append("<td style = 'border-style: solid; background-color: #D9E2F3;'> ");
                }
                else
                {
                    html.Append("<td style = 'border-style: solid; background-color: #FFFFFF;'> ");
                }
   
                html.Append(parcela.DesParcela);
                html.Append("</td>");

                if (linha == 0)
                {
                    html.Append("<td style = 'border-style: solid; background-color: #D9E2F3;'> ");
                }
                else
                {
                    html.Append("<td style = 'border-style: solid; background-color: #FFFFFF;'> ");
                }

                html.Append("http://www.clubeconteza.com.br/boletos/");
                html.Append(parcela.TB016_id.ToString());
                html.Append(".htm");
                html.Append("</td>");

                if (linha == 0)
                {
                    linha = 1;
                }
                else
                {
                    linha = 0;
                }
            }
             
            html.Append(" </table>");
            html.Append(" </div>");
            html.Append(" <div>");
            html.Append(" <br/>");
            html.Append(" Qualquer duvida, entre em contato com nossa central de atendimento no telefone: 42 3623 1441</div>");
            html.Append(" </div>");
            html.Append(" </div>");
            html.Append(" </form>");
            html.Append("</body>");
            html.Append("</html>");

            _doc = webBrowser1.Document;
            if (_doc != null)
            {
                if (_doc.Body != null) _doc.Body.InnerHtml = html.ToString();
                _doc.Title = "TITULO";
            }
            tabPrincipal.TabPages.Remove(tbContrato);
        }
        private void mnuEnviarEmailEnviar_Click(object sender, EventArgs e)
        {
            string str_from_address = ParametrosNegocios.EmailSaidaLogin;//sender
            string str_name = ParametrosNegocios.NomeConta;
            //The To address (Email ID)
            string str_to_address = comboBox1.Text;//recipient
  
            System.Net.Mail.MailMessage email_msg = new MailMessage();

            email_msg.From = new MailAddress(str_from_address, str_name);
            email_msg.Sender = new MailAddress(str_from_address, str_name);
            //email_msg.ReplyTo = new MailAddress(str_from_address, str_name);

            //The To Email id
            email_msg.To.Add(str_to_address);

            email_msg.Subject = label69.Text;//Subject of email
                                             // StringBuilder HTML = new StringBuilder();
            email_msg.IsBodyHtml = true;
            email_msg.Body = webBrowser1.Document.Body.InnerHtml;

            //HTML.ToString();//body
            //Create an object for SmtpClient class
            SmtpClient mail_client = new SmtpClient();

            //Providing Credentials (Username & password)
            NetworkCredential network_cdr = new NetworkCredential();
            network_cdr.UserName = str_from_address;
            network_cdr.Password = ParametrosNegocios.EmailSaidaSenha;

            mail_client.Credentials = network_cdr;

            //Specify the SMTP Port
            mail_client.Port = ParametrosNegocios.EmailSaidaPorta;

            //Specify the name/IP address of Host
            mail_client.Host = ParametrosNegocios.EmailSaidaSMTP;

            //Uses Secure Sockets Layer(SSL) to encrypt the connection
            mail_client.EnableSsl = true;

            //Now Send the message
            mail_client.Send(email_msg);

            var anotacao = new AnotacoesController
            {
                Tb012Id = Convert.ToInt64(lblContrato.Text),
                Tb011Id = ParametrosInterface.objUsuarioLogado.TB011_Id,
                Tb026Data = DateTime.Now,
                Tb026Cod = "00000",
                TB026_Negociacao = 1,
                Tb026Anotacao = "E-mail das parcelas em aberto + negociação envidos para " + comboBox1.Text
            };

            var retorno = new AnotacoesNegocios().Anotacaoinsert(anotacao);


            MessageBox.Show("Mensagem enviada", @"Aviso", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void fecharToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbEnviarPorEmail);
            tabPrincipal.TabPages.Add(tbContrato);
             PreencherAnotacoes(Convert.ToInt64(lblContrato.Text));
        }
        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
            this.reportViewer1.RefreshReport();
        }
        private void mnuPrincipalAtualizar_Click(object sender, EventArgs e)
        {
            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
        }
        private void relaçãoParaImportaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbLista);
            tabPrincipal.TabPages.Add(tbRelacao);
        }
        private void fecharToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbRelacao);
            tabPrincipal.TabPages.Add(tbLista);
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {

            if (Convert.ToInt16(lblParcelasEmAtrazo.Text) < 7)
            {
                MessageBox.Show(MensagensDoSistema._0108, @"6", MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Trim() == Empty)
            {
                textBox1.Text = @"0";
            }

            if (Convert.ToDouble(textBox1.Text)>100)
            {
                MessageBox.Show(MensagensDoSistema._0107, @"Erro", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                return;
            }


            if(ParametrosInterface.objUsuarioLogado.TB037_Id != 1)
            {
                if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(lbAdesaoDescAdesao.Text))
                {
                    MessageBox.Show(MensagensDoSistema._0106, lbAdesaoDescAdesao.Text, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    textBox1.Text = @"0";
                    return;
                }
            }



            label80.Text = (Convert.ToDouble(lblValorAdesao.Text.Replace("R$", "").Replace(".", ",")) / 100 * Convert.ToDouble(textBox1.Text.Replace("%", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);

            label83.Text = (Convert.ToDouble(label80.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label81.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);

            CalcularParcelas();
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt16(lblParcelasEmAtrazo.Text) < 7)
            {
                MessageBox.Show(MensagensDoSistema._0108, @"6", MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text.Trim() == Empty)
            {
                textBox2.Text = @"0";
            }

            if (Convert.ToDouble(textBox2.Text) > Convert.ToDouble(lbMensalidadeDescOutros.Text))
            {
                MessageBox.Show(MensagensDoSistema._0107, @"Erro", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                return;
            }

            if (ParametrosInterface.objUsuarioLogado.TB037_Id != 1)
            {
                if (Convert.ToDouble(textBox2.Text) > Convert.ToDouble(lbMensalidadeDescOutros.Text))
                {
                    MessageBox.Show(MensagensDoSistema._0106, lbMensalidadeDescOutros.Text, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    textBox2.Text = @"0";
                    return;
                }
            }
            label81.Text= (Convert.ToDouble(lblValorMensalidade.Text.Replace("R$", "").Replace(".", ","))/100 * Convert.ToDouble(textBox2.Text.Replace("%", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            label83.Text = (Convert.ToDouble(label80.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label81.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            ValorFinal();
            CalcularParcelas();
        }
        private void ValorFinal()
        {
            label82.Text= (Convert.ToDouble(label73.Text.Replace("R$", "").Replace(".", ","))+ Convert.ToDouble(label75.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label80.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            label51.Text = (Convert.ToDouble(label74.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label76.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label81.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            lblTotalDescontos.Text = (Convert.ToDouble(label74.Text.Replace("R$", "").Replace(".", ",")) + Convert.ToDouble(label76.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            label29.Text = (Convert.ToDouble(label27.Text.Replace("R$", "").Replace(".", ","))- Convert.ToDouble(label82.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            label30.Text = (Convert.ToDouble(label47.Text.Replace("R$", "").Replace(".", ",")) - Convert.ToDouble(label51.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
            label40.Text = (Convert.ToDouble(label29.Text.Replace("R$", "").Replace(".", ","))+ Convert.ToDouble(label30.Text.Replace("R$", "").Replace(".", ","))).ToString("C2").Replace(",", ".").ToString(CultureInfo.InvariantCulture);
        }
    }
}
