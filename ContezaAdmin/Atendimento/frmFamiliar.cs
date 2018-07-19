using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static System.String;
using static System.Windows.Forms.Application;
using Warning = Microsoft.Reporting.WinForms.Warning;

namespace ContezaAdmin.Atendimento
{
    public partial class FrmFamiliar : Form
    {
        Util _validacoes = new Util();
        private Color _enter = Color.GreenYellow;
        long _tb002Id;
        int _privilegioOn;
        int _privilegioConfirmar;
        int _tb016DiaVencimento;
        HtmlDocument _doc;
        int _tb012Status;
        //cmbContratoStatus

        public FrmFamiliar()
        {
            InitializeComponent();

        }

        private void frmFamiliar_Load(object sender, EventArgs e)
        {
            _privilegioOn = 0;
            if (ParametrosInterface.objUsuarioLogado.Perfil.TB010_id == 1)
            {
                lblParcelaProdutoId.Visible = true;
                lblParcelaPlanoId.Visible = true;
            }

            dtmCadContrato.Value = DateTime.Now;
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Remove(tbpDependenteCancelar);
            tabPrincipal.TabPages.Remove(tbpDependente);
            tabPrincipal.TabPages.Remove(tbpParcelas);
            tabPrincipal.TabPages.Remove(tbpEnderecoFatura);
            tabPrincipal.TabPages.Remove(tbpParcelas);
            //tabPrincipal.TabPages.Remove(tbpContratoCancelar);
            tabPrincipal.TabPages.Remove(tbpDocumentos);
            tabPrincipal.TabPages.Remove(tbpTermoDeUso);
            tabPrincipal.TabPages.Remove(tbpAlteracaoContrato);
            tabPrincipal.TabPages.Remove(tbBoletos);
            tabPrincipal.TabPages.Remove(tbCarne);
            tabPrincipal.TabPages.Remove(tpPagamento);
            tabPrincipal.TabPages.Remove(tbCancelarPlano);
            tabPrincipal.TabPages.Remove(tbContratoCancelamentoTermo);
            tabPrincipal.TabPages.Remove(tbReativarContrato);
            tabPrincipal.TabPages.Remove(tbDependentesReativar);
            

            CarregarStatusParcela();
            StatusDeContrato();
            CarregarSexo();
            CarregarPaises();
            PopularTipoContatos();
            var filtro = new PaisController { TB003_id = 1058 };
            PopularEstadosTitular(filtro);
            FormasDePagamento();

            //CarregarMotivoCancelamentoContrato();

            var dt1 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 00:00:00";
            var dt2 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 23:59:59";
            var vQuery = " AND TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            CarregarContratos(ListarPessoas(vQuery));

        }

        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
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
        private void PontosDeVenda()
        {
            try
            {
                var pontoDeVendaN = new PontoDeVendaNegocios();
                cmbContratoPontosDeVenda.DataSource = null;
                cmbContratoPontosDeVenda.Items.Clear();
                cmbContratoPontosDeVenda.DataSource = pontoDeVendaN
                    .PontosDeVendaLiberadosParaUsuario(ParametrosInterface.objUsuarioLogado).Tables[0];
                cmbContratoPontosDeVenda.DisplayMember = "TB002_Ponto";
                cmbContratoPontosDeVenda.ValueMember = "TB002_id";
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
                //Dependente
                cmbDependentePais.DataSource = null;
                cmbDependentePais.Items.Clear();
                cmbDependentePais.DataSource = enderecoN.PaisController().Tables[0];
                cmbDependentePais.DisplayMember = "TB003_Pais";
                cmbDependentePais.ValueMember = "TB003_id";
                //Contrato
                cmbTitularPais.DataSource = null;
                cmbTitularPais.Items.Clear();
                cmbTitularPais.DataSource = enderecoN.PaisController().Tables[0];
                cmbTitularPais.DisplayMember = "TB003_Pais";
                cmbTitularPais.ValueMember = "TB003_id";
                //Fatura
                cmbEnderecoFaturaPais.DataSource = null;
                cmbEnderecoFaturaPais.Items.Clear();
                cmbEnderecoFaturaPais.DataSource = enderecoN.PaisController().Tables[0];
                cmbEnderecoFaturaPais.DisplayMember = "TB003_Pais";
                cmbEnderecoFaturaPais.ValueMember = "TB003_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void CarregarStatusParcela()
        {
            if (cmbParcelaStatus.ComboBox != null)
            {
                cmbParcelaStatus.ComboBox.DataSource = null;
                cmbParcelaStatus.ComboBox.Items.Clear();
            }
            var lstStatus = new List<KeyValuePair<string, string>>();
            var statu = Enum.GetValues(typeof(ParcelaController.TB016_StatusE));
            foreach (ParcelaController.TB016_StatusE st in statu)
            {
                lstStatus.Add(new KeyValuePair<string, string>(st.ToString(), ((int)st).ToString()));
            }

            lstStatus.Add(new KeyValuePair<string, string>("Todos", "-1"));

            Debug.Assert(cmbParcelaStatus.ComboBox != null, "cmbParcelaStatus.ComboBox != null");
            cmbParcelaStatus.ComboBox.DataSource = lstStatus;
            cmbParcelaStatus.ComboBox.DisplayMember = "Key";
            cmbParcelaStatus.ComboBox.ValueMember = "Value";
        }
        private void CarregarSexo()
        {
            cmbContratoTitularSexo.DataSource = null;
            cmbContratoTitularSexo.Items.Clear();


            cmbContratoDependenteSexo.DataSource = null;
            cmbContratoDependenteSexo.Items.Clear();

            var lstSexo = new List<KeyValuePair<string, string>>();
            var sexoS = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE sexo in sexoS)
            {
                lstSexo.Add(new KeyValuePair<string, string>(sexo.ToString(), ((int)sexo).ToString()));
            }

            cmbContratoTitularSexo.DataSource = lstSexo;
            cmbContratoTitularSexo.DisplayMember = "Key";
            cmbContratoTitularSexo.ValueMember = "Value";

            cmbContratoDependenteSexo.DataSource = lstSexo;
            cmbContratoDependenteSexo.DisplayMember = "Key";
            cmbContratoDependenteSexo.ValueMember = "Value";
        }
        private void PopularTipoContatos()
        {
            cmbContratoTitularContatoTipo.DataSource = null;
            cmbContratoTitularContatoTipo.Items.Clear();

            cmbContratoDependenteContatoTipo.DataSource = null;
            cmbContratoDependenteContatoTipo.Items.Clear();

            List<KeyValuePair<string, string>> contratoStatus = new List<KeyValuePair<string, string>>();
            Array status = Enum.GetValues(typeof(ContatoController.TB009_TipoE));
            foreach (ContatoController.TB009_TipoE statu in status)
            {
                contratoStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbContratoTitularContatoTipo.DataSource = contratoStatus;
            cmbContratoTitularContatoTipo.DisplayMember = "Key";
            cmbContratoTitularContatoTipo.ValueMember = "Value";

            cmbContratoDependenteContatoTipo.DataSource = contratoStatus;
            cmbContratoDependenteContatoTipo.DisplayMember = "Key";
            cmbContratoDependenteContatoTipo.ValueMember = "Value";

            /*Popular Motivo de Cancelamentos*/
            cmbCancelamentoDependenteMotivo.DataSource = null;
            cmbCancelamentoDependenteMotivo.Items.Clear();
            List<KeyValuePair<string, string>> motivosCancelamentos = new List<KeyValuePair<string, string>>();
            Array motivos = Enum.GetValues(typeof(PessoaController.TB013_CancelamentoMotivoE));
            foreach (PessoaController.TB013_CancelamentoMotivoE motivo in motivos)
            {
                motivosCancelamentos.Add(
                    new KeyValuePair<string, string>(motivo.ToString(), ((int)motivo).ToString()));
            }

            cmbCancelamentoDependenteMotivo.DataSource = motivosCancelamentos;
            cmbCancelamentoDependenteMotivo.DisplayMember = "Key";
            cmbCancelamentoDependenteMotivo.ValueMember = "Value";

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
        private void PopularEstadosDependente(PaisController filtro)
        {
            cmbContratoDependenteEstado.DataSource = null;
            cmbContratoDependenteEstado.Items.Clear();
            try
            {
                cmbContratoDependenteEstado.DataSource = new EnderecoNegocios().EstadosController(filtro).Tables[0];
                cmbContratoDependenteEstado.DisplayMember = "TB005_Estado";
                cmbContratoDependenteEstado.ValueMember = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void PopularMunicipiosDependente(EstadoController filtro)
        {
            cmbContratoDependenteMunicipio.DataSource = null;
            cmbContratoDependenteMunicipio.Items.Clear();
            try
            {
                cmbContratoDependenteMunicipio.DataSource = new EnderecoNegocios().MunicipioController(filtro).Tables[0];
                cmbContratoDependenteMunicipio.DisplayMember = "TB006_Municipio";
                cmbContratoDependenteMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ptbFiltrarContratosCampoPersonalizado_Click(object sender, EventArgs e)
        {
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

            lblTotalRegistrosRetornados.Text = listarContratos.Count.ToString();

            Int64 contrato = 0;
            int contratos = 0;
            for (var i = listarContratos.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt64(listarContratos[i].TB012_Id) == contrato) continue;
                contratos++;
                contrato = Convert.ToInt64(listarContratos[i].TB012_Id);
            }

            lblTotalContratosRetornados.Text = contratos.ToString();
        }
        protected void FiltrarContrato(long vId)
        {
            try
            {
                var retorno = new ContratoNegocios().contratoSelect(vId);

                if(retorno.TB012_TipoContrato==2)
                {
                    MessageBox.Show(Format(MensagensDoSistema._0101, "Parceiro", "Erro", MessageBoxButtons.OK,
                  MessageBoxIcon.Error));
                    //1 - Familiar
                    //2 - Parceiro
                    //3 - Corporativo
                    //4 - Familiar Corporativo
                    //5 - Familiar Parceiro
                    return;
                }

                if (retorno.TB012_TipoContrato == 3)
                {
                    MessageBox.Show(Format(MensagensDoSistema._0101, "Corporativo", "Erro", MessageBoxButtons.OK,
                  MessageBoxIcon.Error));
                    return;
                }
                if (retorno.TB012_TipoContrato == 4)
                {
                    MessageBox.Show(Format(MensagensDoSistema._0101, "Familiar Corporativo", "Erro", MessageBoxButtons.OK,
                  MessageBoxIcon.Error));
                    return;
                }

                tabPrincipal.TabPages.Add(tbContrato);
                tabPrincipal.TabPages.Remove(tbLista);


                dtContratoFim.Enabled = false;
                dtContratoInicio.Enabled = false;

                PontosDeVenda();
                DTContratoTitularContatos.Rows.Clear();
                DTContratoTitularContatos.Refresh();


                if (Convert.ToInt16((int)((ContratosController.TB012_StatusE)Enum.Parse(typeof(ContratosController.TB012_StatusE), cmbContratoStatus.SelectedValue.ToString()))) == 50)
                {
                    MessageBox.Show(MensagensDoSistema._0089, @"Alerta", MessageBoxButtons.OK,
                       MessageBoxIcon.Asterisk);
                }
                mskContratoTitularCPF.Enabled = false;
                lblContrato.Text = retorno.TB012_Id.ToString().PadLeft(6, '0');
                dtContratoInicio.Value = retorno.TB012_Inicio;
                dtContratoFim.Value = retorno.TB012_Fim;
                cmbDiaVencimento.Text = retorno.TB012_DiaVencimento.ToString();

                mnuContratoSolicitarAlteracao.Visible = Convert.ToBoolean(retorno.TB012_Edicao);

                cmbContratoPontosDeVenda.SelectedValue = retorno.PontoDeVenda.TB002_id;
                dtContratoInicio.Enabled = false;
                mnuContratoDocumentos.Enabled = true;
                mnuContratoCancelar.Enabled = true;
                mnuContratoDependentes.Enabled = true;
                mnuContratoParcelas.Enabled = true;

                chkContratoTitularAceiteContrato.Checked = retorno.TB012_AceiteContrato == 1;

                chkEraContezino.Checked = retorno.Titular.TB012_EraContezino == 1;

                chkContratoTitularDeclaroSerMaiorCapaz.Checked = true;

                txtContratoTitularNomeCompleto.Text = retorno.Titular.TB013_NomeCompleto;

                lblCiclo.Text = retorno.TB012_CicloContrato;

                txtTB013_MaeNome.Text                   = retorno.Titular.TB013_MaeNome.TrimEnd();
                cmbContratoTitularSexo.SelectedValue    = retorno.Titular.TB013_SexoS;
                ddtTB013_MaeDataNascimento.Value        = retorno.Titular.TB013_MaeDataNascimento;
                txtTB013_PaiNome.Text                   = retorno.Titular.TB013_PaiNome.TrimEnd();
                ddtTB013_PaiDataNascimento.Value        = retorno.Titular.TB013_PaiDataNascimento;
                dtContratoTitularDataNascimento.Value   = retorno.Titular.TB013_DataNascimento;
                lblContratoTitularID.Text               = retorno.Titular.TB013_id.ToString();
                mskContratoTitularCPF.Text              = retorno.Titular.TB013_CPFCNPJ;
                txtContratoTitularRG.Text               = retorno.Titular.TB013_RG;
                txtContratoTitularRGOrgaoEmissor.Text   = retorno.Titular.TB013_RGOrgaoEmissor;
                lblCarteirinha.Text                     = retorno.Titular.TB013_Cartao;
                lblStatusCarteirinha.Text               = @"Cartão [" + retorno.Titular.TB013_CarteirinhaStatusS + @"]";
                mskContratoTitularCEP.Text              = retorno.Titular.TB004_Cep;
                txtContratoTitularLogradouro.Text       = retorno.Titular.TB013_Logradouro;
                txtContratoTitularNumero.Text           = retorno.Titular.TB013_Numero;
                txtContratoTitularBairro.Text           = retorno.Titular.TB013_Bairro;
                txtContratoTitularComplemento.Text      = retorno.Titular.TB013_Complemento;
                cmbDiaVencimento.SelectedItem           = retorno.TB012_DiaVencimento;
                cmbTitularPais.SelectedValue            = retorno.Titular.Municipio.Estado.Pais.TB003_id;
                lblNumeroDaSorte.Text                   = retorno.TB012_NumeroDaSorte.ToString();
                PaisController pais                     = new PaisController { TB003_id = retorno.Titular.Municipio.Estado.Pais.TB003_id };
                PopularEstadosTitular(pais);

                cmbContratoTitularEstado.SelectedValue  = retorno.Titular.Municipio.Estado.TB005_Id;
                var municipio                           = new EstadoController { TB005_Id = retorno.Titular.Municipio.Estado.TB005_Id };
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
                cmbContratoPontosDeVenda.Enabled            = false;
                cmbContratoStatus.SelectedValue             = retorno.TB012_StatusS;

                if(Convert.ToInt16(retorno.TB012_StatusS)==5)
                {
                    mnuContratoReativar.Visible = true;
                }
                cmbContratoTitularSexo.SelectedValue        = retorno.Titular.TB013_SexoS;
                DTContratoDependentes.AutoGenerateColumns   = false;
                DTContratoDependentes.DataSource            = null;
                DTContratoDependentes.DataSource            = retorno.Dependentes;
                DTContratoDependentes.Refresh();
                cmbDiaVencimento.Enabled                    = false;
            }
            catch (Exception ex)
            {
                tabPrincipal.TabPages.Remove(tbContrato);
                tabPrincipal.TabPages.Add(tbLista);
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuContratoFechar_Click(object sender, EventArgs e)
        {
            LimparCamposContrato();
            tabPrincipal.TabPages.Remove(tbContrato);

            tabPrincipal.TabPages.Add(tbLista);

            var dt1 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 00:00:00";
            var dt2 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 23:59:59";

            var vQuery = " AND TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            CarregarContratos(ListarPessoas(vQuery));
        }
        private void mnuListaNovo_Click(object sender, EventArgs e)
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
            cmbDiaVencimento.SelectedIndex = 0;
            mskContratoTitularCPF.Enabled = true;
            LimparCamposContrato();
            dtContratoInicio.Enabled = true;
            tabPrincipal.TabPages.Add(tbContrato);
            cmbContratoPontosDeVenda.Enabled = true;
            dtContratoInicio.Enabled = true;
            PontosDeVenda();
            var hoje = DateTime.Now;
            dtContratoInicio.Enabled = true;
            dtContratoInicio.Value = hoje;
            dtContratoFim.Value = hoje.AddMonths(12);
            cmbContratoStatus.SelectedValue = "0";
            cmbDiaVencimento.Enabled = true;
            var filtro = new EstadoController
            {
                TB005_Id = Convert.ToInt16(ParametrosInterface.objUsuarioLogado.Estado.TB005_Id)
            };
            PopularMunicipiosTitular(filtro);

            cmbContratoTitularMunicipio.SelectedValue = ParametrosInterface.objUsuarioLogado.Municipio.TB006_id;
            mskContratoTitularCPF.Enabled = true;
            tabPrincipal.TabPages.Remove(tbLista);
            cmbContratoTitularEstado.SelectedValue = ParametrosInterface.objUsuarioLogado.Estado.TB005_Id.ToString();
            mskContratoTitularCPF.Focus();
        }
        private void LimparCamposContrato()
        {
            mnuContratoReativar.Visible = false;
            lblNumeroDaSorte.Text = "";
            _privilegioOn = 0;
            txtDependenteMae.Text = "";
            txtDependentePai.Text = "";
            mnuContratoCartaoManual.Visible = false;
            mskContratoTitularCPF.Enabled = false;
            dtContratoInicio.Enabled = false;
            pcbMudarLocalCadastro.Visible = false;
            cmbContratoPontosDeVenda.Enabled = false;
            mnuContratoDependentes.Enabled = false;
            chkEraContezino.Checked = false;
            lblContratoTitularID.Text = "";
            dtContratoInicio.Enabled = false;
            lblContrato.Text = "";
            lblCarteirinha.Text = "";
            cmbContratoStatus.SelectedValue = "0";
            txtContratoTitularNomeCompleto.Text = "";
            mskContratoTitularCPF.Text = "";
            txtContratoTitularRG.Text = "";
            txtContratoTitularRGOrgaoEmissor.Text = "";
            mskContratoTitularCEP.Text = "";
            txtContratoTitularLogradouro.Text = "";
            txtContratoTitularNumero.Text = "";
            txtContratoTitularBairro.Text = "";
            txtContratoTitularComplemento.Text = "";
            chkEraContezino.Checked = false;
            DTContratoTitularContatos.Rows.Clear();
            DTContratoDependentes.DataSource = null;
            DTContratoDependentes.Refresh();
            chkContratoTitularAceiteContrato.Checked = false;
            chkContratoTitularDeclaroSerMaiorCapaz.Checked = false;
            chkEraContezino.Checked = false;

        }
        private void mskContratoTitularCPF_Leave(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() != Empty) return;
            //Verificar se o CPF já não esta cadastrado
            if (mskContratoTitularCPF.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("_", "")
                    .Replace(" ", "") != Empty)
            {
                Util validaCpf = new Util();
                if (validaCpf.CPF(mskContratoTitularCPF.Text))
                {
                    //Consultar base de dados                  
                    var retorno = new PessoaNegocios().pessoaSelectCPFCNPJ(mskContratoTitularCPF.Text.Trim());
                    if (retorno.TB013_id > 0)
                    {
                        //Encontrado CPF na Base
                        if (retorno.Contrato.TB012_TipoContrato == 1)
                        {
                            if (Convert.ToInt64(retorno.TB012_Id) > 0)
                            {
                                //CPF Vinculado a um contrato
                                if (MessageBox.Show(MensagensDoSistema._0012, @"Contrato",
                                        MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                                LimparCamposContrato();
                                tabPrincipal.TabPages.Remove(tbContrato);
                                //tabPrincipal.TabPages.Add(tbLista);
                                //tabPrincipal.TabPages.Add(tbContrato);
                                FiltrarContrato(Convert.ToInt64(retorno.TB012_Id));
                                tabPrincipal.TabPages.Remove(tbLista);
                            }
                            else
                            {
                                //Implementar popular campos no caso da pessoa estar cadastrada mas não vinculada a contrato
                            }
                        }
                        else
                        {
                            txtContratoTitularNomeCompleto.Text = retorno.TB013_NomeCompleto;
                            cmbContratoTitularSexo.SelectedValue = retorno.TB013_SexoS;
                            lblContratoTitularID.Text = retorno.TB013_id.ToString();
                            txtContratoTitularRG.Text = retorno.TB013_RG;
                            txtContratoTitularRGOrgaoEmissor.Text = retorno.TB013_RGOrgaoEmissor;
                            dtContratoTitularDataNascimento.Value =
                                Convert.ToDateTime(retorno.TB013_DataNascimento);

                            mskContratoTitularCEP.Text = retorno.TB004_Cep;

                            if (retorno.TB012_EraContezino == 1)
                                chkEraContezino.Checked = true;
                            else
                                chkEraContezino.Checked = false;


                            if (retorno.TB013_DeclaroSerMaiorCapaz == 1)
                            {
                                chkContratoTitularDeclaroSerMaiorCapaz.Checked = true;
                            }
                            else
                            {
                                chkContratoTitularDeclaroSerMaiorCapaz.Checked = false;
                            }

                            lblCarteirinha.Text = retorno.TB013_Cartao;


                            cmbTitularPais.Text = retorno.TB004_Cep;
                            cmbTitularPais.SelectedValue = retorno.Municipio.Estado.Pais.TB003_id;
                            cmbContratoTitularEstado.SelectedValue = retorno.Municipio.Estado.TB005_Id;
                            cmbContratoTitularMunicipio.SelectedValue = retorno.Municipio.TB006_id;
                            txtContratoTitularLogradouro.Text = retorno.TB013_Logradouro;
                            txtContratoTitularNumero.Text = retorno.TB013_Numero;
                            txtContratoTitularBairro.Text = retorno.TB013_Bairro;
                            txtContratoTitularComplemento.Text = retorno.TB013_Complemento;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(MensagensDoSistema._0031, @"Erro", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
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


            if (cmbContratoPontosDeVenda.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Ponto de Venda"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbContratoPontosDeVenda.Focus();
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
                txtTB013_PaiNome.Text = "NÃO INFORMADO";
                //MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome do Pai"), @"Erro",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txtTB013_PaiNome.Focus();
                //return false;
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


            if (txtContratoTitularRG.Text.Replace(",", "").Replace("-", "").Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "RG"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTitularRG.Focus();
                return false;
            }

            if (txtContratoTitularRGOrgaoEmissor.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Orgão Emissor do RG"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTitularRGOrgaoEmissor.Focus();
                return false;
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

            if (chkContratoTitularDeclaroSerMaiorCapaz.Checked == false)
            {
                chkContratoTitularDeclaroSerMaiorCapaz.Checked = true;
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
        private void mnuContratoSalvar_Click(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() == Empty)
            {
                NovoContrato();
                mnuContratoCartaoManual.Visible = true;
            }
            else
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
                    TB013_CPFCNPJ           = mskContratoTitularCPF.Text,
                    TB013_NomeCompleto      = txtContratoTitularNomeCompleto.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                    TB013_NomeExibicao      = txtContratoTitularNomeCompleto.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                    TB013_SexoS             = cmbContratoTitularSexo.SelectedValue.ToString(),
                    TB013_RG                = txtContratoTitularRG.Text,
                    TB013_RGOrgaoEmissor    = txtContratoTitularRGOrgaoEmissor.Text,
                    TB013_DataNascimento    = dtContratoTitularDataNascimento.Value,
                    TB004_Cep               = mskContratoTitularCEP.Text,
                    TB013_Logradouro        = txtContratoTitularLogradouro.Text,
                    TB013_MaeNome           = txtTB013_MaeNome.Text.TrimEnd().Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                    TB013_CartaoSolicitado  = 1,
                    TB013_Numero            = txtContratoTitularNumero.Text,
                    TB013_Bairro            = txtContratoTitularBairro.Text,
                    TB013_Complemento       = txtContratoTitularComplemento.Text,
                    TB013_AlteradoPor       = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB013_MaeDataNascimento = ddtTB013_MaeDataNascimento.Value,
                    TB013_PaiNome           = txtTB013_PaiNome.Text.TrimEnd().Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                    TB013_PaiDataNascimento = ddtTB013_PaiDataNascimento.Value,
                    TB013_id                = Convert.ToInt64(lblContratoTitularID.Text),
                    TB012_Id                = Convert.ToInt64(lblContrato.Text)
                };

                if (new PessoaNegocios().PessoaUpdateTitularContratoFamiliar(titular))
                {

                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        private void NovoContrato()
        {
            if (!ValidaTitular())
            {
                return;
            }
            try
            {
                ContratosController contrato = new ContratosController();
                contrato.PontoDeVenda = new PontoDeVendaController();
                contrato.Municipio = new MunicipioController();
                contrato.Titular = new PessoaController();

                contrato.PontoDeVenda.TB002_id = Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue);
                contrato.TB012_Inicio = dtContratoInicio.Value;
                contrato.TB012_Fim = dtContratoFim.Value;

                contrato.TB012_DiaVencimento = Convert.ToInt16(cmbDiaVencimento.Text);

                contrato.TB012_AceiteContrato = Convert.ToInt16(chkContratoTitularAceiteContrato.Checked);
                contrato.TB012_CadastradorPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                contrato.TB012_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                contrato.TB004_Cep = mskContratoTitularCEP.Text;
                contrato.Municipio.TB006_id = Convert.ToInt64(cmbContratoTitularMunicipio.SelectedValue);
                contrato.TB012_Logradouro = txtContratoTitularLogradouro.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"); ;
                contrato.TB012_Numero = txtContratoTitularNumero.Text;
                contrato.TB012_Bairro = txtContratoTitularBairro.Text;
                contrato.TB012_Complemento = txtContratoTitularComplemento.Text;
                contrato.TB012_CicloContrato = dtContratoInicio.Value.Month.ToString() + dtContratoInicio.Value.Year;
                contrato.TB012_DiaVencimento = Convert.ToInt16(cmbDiaVencimento.Text);

                if (lblContratoTitularID.Text.Trim() == Empty)
                {
                    contrato.Titular.TB013_id = 0;
                }
                else
                {
                    contrato.Titular.TB013_id = Convert.ToInt64(lblContratoTitularID.Text);
                }

                contrato.Titular.TB012_EraContezino = Convert.ToInt16(chkEraContezino.Checked);
                contrato.Titular.TB013_TipoS = "1";
                contrato.Titular.TB013_CodigoDependente = 1001;
                contrato.Titular.TB013_CPFCNPJ = mskContratoTitularCPF.Text;
                contrato.Titular.TB013_NomeCompleto = txtContratoTitularNomeCompleto.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a");
                contrato.Titular.TB013_SexoS = cmbContratoTitularSexo.SelectedValue.ToString();
                contrato.Titular.TB013_MaeNome = txtTB013_MaeNome.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"); ;
                contrato.Titular.TB013_MaeDataNascimento = ddtTB013_MaeDataNascimento.Value;
                contrato.Titular.TB013_PaiNome = txtTB013_PaiNome.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"); ;
                contrato.Titular.TB013_PaiDataNascimento = ddtTB013_PaiDataNascimento.Value;


                contrato.Titular.TB013_RG = txtContratoTitularRG.Text;
                contrato.Titular.TB013_RGOrgaoEmissor = txtContratoTitularRGOrgaoEmissor.Text;
                contrato.Titular.TB013_DataNascimento = dtContratoTitularDataNascimento.Value;
                contrato.Titular.TB013_DeclaroSerMaiorCapaz =
                Convert.ToInt16(chkContratoTitularDeclaroSerMaiorCapaz.Checked);


                ContratosController retorno = new ContratoNegocios().ContratoFamiliarInserir(contrato);

                if (retorno.TB012_Id > 0)
                {
                    lblContratoTitularID.Text = retorno.Titular.TB013_id.ToString();
                    lblContrato.Text = retorno.TB012_Id.ToString().PadLeft(6, '0');
                    mnuContratoDependentes.Enabled = true;
                    mnuContratoParcelas.Enabled = true;
                    mnuContratoDocumentos.Enabled = true;
                    mnuContratoCancelar.Enabled = true;
                    lblCiclo.Text = dtContratoInicio.Value.Month.ToString() + dtContratoInicio.Value.Year.ToString();

                    MessageBox.Show(MensagensDoSistema._0017, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
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
        private void txtFiltroAssociado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
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
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void ddgContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgContratos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "id":
                           
                            FiltrarContrato(Convert.ToInt64(ddgContratos.Rows[e.RowIndex].Cells["TB012_Id"].Value));
                            mnuContratoCartaoManual.Visible = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DTContratoTitularContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (lblContrato.Text.Trim() == Empty)
                {
                    NovoContrato();
                    mnuContratoCartaoManual.Visible = true;
                }

                ContatoController contato = new ContatoController();
                ContatoNegocios contatoN = new ContatoNegocios();

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {


                    if (lblContrato.Text.Trim() == Empty)
                    {
                        if (ValidaTitular())
                        {
                            NovoContrato();
                        }
                    }

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


                                    contato.TB009_Contato = sContato.Replace("(", "").Replace(")", "").Replace("-", "").Trim();

                                    if(contato.TB009_Contato.Length <9 && Convert.ToInt16(contato.TB009_TipoS) != 3)
                                    {
                                        MessageBox.Show(MensagensDoSistema._0103, contato.TB009_Contato, MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                                    }
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
                                    if (lblContrato.Text.Trim() == Empty)
                                    {
                                        if (ValidaTitular())
                                        {
                                            NovoContrato();
                                        }
                                    }
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

                /*Recarregar Log*/
                // RecuperarLog(Convert.ToInt64(lblContrato.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtmCadContrato_ValueChanged(object sender, EventArgs e)
        {
            var dt1 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 00:00:00";
            var dt2 = dtmCadContrato.Value.Month + "/" + dtmCadContrato.Value.Day + "/" + dtmCadContrato.Value.Year +
                         " 23:59:59";
            var vQuery = " AND TB012_CadastradoEm BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            CarregarContratos(ListarPessoas(vQuery));
        }
        private void mnuParcelaFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpParcelas);
            tabPrincipal.TabPages.Add(tbContrato);
            _tb002Id = 0;
            _tb016DiaVencimento = 0;
        }
        private void mnuContratoParcelas_Click(object sender, EventArgs e)
        {
            _tb012Status = Convert.ToInt16(cmbContratoStatus.SelectedValue);
     

            cmbParcelaCiclo.Items.Clear();
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

            Debug.Assert(cmbParcelaCiclo.ComboBox != null, "cmbParcelaCiclo.ComboBox != null");

            var ciclo = new ParcelaNegocios().ListarCiclosAtivosContrato(Convert.ToInt64(lblContrato.Text));
            if (ciclo.Count > 0)
            {
                for (int i = 0; i < ciclo.Count; i++)
                {
                    cmbParcelaCiclo.Items.Add(Convert.ToInt64(ciclo[i].TB012_CicloContrato));
                }
                cmbParcelaCiclo.SelectedIndex = ciclo.Count - 1;
            }
            else
            {
                if (lblCiclo.Text.Trim() == Empty | lblCiclo.Text.Trim() == @"0")
                {
                    lblCiclo.Text = dtContratoInicio.Value.Month.ToString() + dtContratoInicio.Value.Year.ToString();
                }

                cmbParcelaCiclo.Items.Add(Convert.ToInt64(lblCiclo.Text));
                cmbParcelaCiclo.SelectedIndex = 0;
            }

            ListarParcelas(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(lblCiclo.Text));
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Add(tbpParcelas);

            if (cmbParcelaStatus.ComboBox != null) cmbParcelaStatus.ComboBox.SelectedValue = "-1";
        }
        private void ListarParcelas(long parcela, Int32 ciclo)
        {
            var parcelas =
                new ParcelaNegocios().FamiliarListaParcelasContrato(parcela, ciclo
                    , -1);
            ddtParcelas.DataSource = parcelas;
            ddtParcelas.Refresh();
           

            _tb002Id = Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue);
            _tb016DiaVencimento = Convert.ToInt16(cmbDiaVencimento.Text);
        }
        private void mnuParcelaGerarParcelas_Click(object sender, EventArgs e)
        {
            if (new ContratoNegocios().EdicaoContrato(Convert.ToInt64(lblContrato.Text)) > 0)
            {
                MessageBox.Show(MensagensDoSistema._0077, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(@"Deseja gerar as parcelas?", @"Parcelas", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GerarParcelasDoClicro();
            }

        }
        private void GerarParcelasDoClicro()
        {
            try
            {
                var parcelas = new ParcelaNegocios().GerarParcelasFamiliar(
                    12,
                    dtContratoInicio.Value.AddDays(2),
                    _tb016DiaVencimento,
                    ParametrosInterface.objUsuarioLogado.TB011_Id,
                    Convert.ToInt64(lblContrato.Text),
                    Convert.ToInt32(cmbParcelaCiclo.Text),
                    -1,
                    1,
                    ParametrosInterface.objUsuarioLogado.TB037_Id,
                    0
                );

                if (!new ParcelaNegocios().FamiliarParcelaInsert(parcelas)) return;
                mnuParcelaGerarParcelas.Enabled = false;
                ddtParcelas.DataSource =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
                ddtParcelas.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ddtParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    cmbFormaPagamento.Enabled = false;
                    switch (ddtParcelas.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":

                            SelecionarParcela(Convert.ToInt64(ddtParcelas.Rows[e.RowIndex].Cells["TB016_id"].Value));
                            break;
                    }
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
            pctParcelaDescontoConfirmar.Enabled = false;
            txtParcelaDesconto.Enabled = false;
            ddtParcelaVencimento.Enabled = false;

            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcela.TB016_StatusS))) < 3)
            {
                pctParcelaDescontoConfirmar.Enabled = true;
                txtParcelaDesconto.Enabled = true;
                ddtParcelaVencimento.Enabled = true;
            }
            lblParcelaPlanoId.Text = parcela.TB015_id.ToString();
            lblParcelaPlano.Text = parcela.TB015_Plano;
            lblParcelaId.Text = parcela.TB016_id.ToString();
            ddtParcelaVencimento.Value = parcela.TB016_Vencimento;
            lblParcelaValorTotal.Text = Format("{0:C2}",
                Convert.ToDouble(parcela.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
            lblParcelaStatusS.Text = parcela.TB016_StatusS;

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

            // ReSharper disable once PossibleNullReferenceException
            ddtParcelaItens.Columns["TB017_ValorUnitario"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            // ReSharper disable once PossibleNullReferenceException
            ddtParcelaItens.Columns["TB017_ValorDesconto"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            // ReSharper disable once PossibleNullReferenceException
            ddtParcelaItens.Columns["TB017_ValorFinal"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            ddtParcelaItens.AutoGenerateColumns = false;
            ddtParcelaItens.DataSource = null;
            ddtParcelaItens.DataSource = parcelasProdutos;
            ddtParcelaItens.Refresh();
        }
        private void mnuParcelaUnirProximaParcela_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Deseja unir parcelas?", @"Parcelas", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            if (lblParcelaId.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", " Parcela"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ParcelaController parcela1 = new ParcelaNegocios().ParcelaPesquisaId(Convert.ToInt64(lblParcelaId.Text));

            if (Convert.ToInt16(
                    (int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE),
                        parcela1.TB016_StatusS))) > 1)

            {
                MessageBox.Show(Format(MensagensDoSistema._0072, parcela1.TB016_id.ToString(), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            else
            {
                ParcelaController parcela2 =
                    new ParcelaNegocios().ParcelaProximaParcelaId(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblParcelaId.Text));

                if (Convert.ToInt16(parcela2.TB016_StatusS) > 1)
                {
                    MessageBox.Show(Format(MensagensDoSistema._0072, parcela1.TB016_id.ToString(), "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error));
                }
                else
                {
                    if (new ParcelaNegocios().ParcelaUnir(parcela1, parcela2.TB016_id,
                        ParametrosInterface.objUsuarioLogado.TB011_Id))
                    {
                        MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
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

                        cmbParcelaCiclo.Items.Add(lblCiclo.Text.Trim());

                        cmbParcelaCiclo.SelectedItem = lblCiclo.Text.Trim();

                        List<ParcelaController> parcelas =
                            new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                                Convert.ToInt64(lblCiclo.Text), -1);
                        if (parcelas.Count == 0)
                        {
                            mnuParcelaGerarParcelas.Enabled = true;
                        }
                        else
                        {
                            mnuParcelaGerarParcelas.Enabled = false;
                            ddtParcelas.DataSource = parcelas;
                            ddtParcelas.Refresh();
                        }
                    }
                    /*Unir Parcelas*/
                }
            }


        }
        private void mnuParcelaCancelarSelecionada_Click(object sender, EventArgs e)
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
            cmbParcelaCiclo.Items.Add(lblCiclo.Text.Trim());
            cmbParcelaCiclo.SelectedItem = lblCiclo.Text.Trim();

            var parcelas =
                new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                    Convert.ToInt64(lblCiclo.Text), -1);
            if (parcelas.Count == 0)
            {
                mnuParcelaGerarParcelas.Enabled = true;
            }
            else
            {
                mnuParcelaGerarParcelas.Enabled = false;
                ddtParcelas.DataSource = parcelas;
                ddtParcelas.Refresh();
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
        private void btnParcelaFechar_Click(object sender, EventArgs e)
        {
            gpbParcelaAvulsa.Visible = false;
        }
        private void mnuParcelaEmitirAvulsa_Click(object sender, EventArgs e)
        {
            btnParcelaConfirmar.Enabled = true;
            dtParcelaAvulsaNParcelas.SelectedIndex = 0;
            gpbParcelaAvulsa.Visible = true;

        }
        private void btnParcelaConfirmar_Click(object sender, EventArgs e)
        {
            btnParcelaConfirmar.Enabled = false;
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
                ddtParcelas.DataSource =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
                ddtParcelas.Refresh();
                gpbParcelaAvulsa.Visible = false;

            }
            catch (Exception ex)
            {
                btnParcelaConfirmar.Enabled = true;
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpTermoDeUso);
            tabPrincipal.TabPages.Add(tbContrato);
        }
        private void likTermosCondicoes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (new ContratoDocNegocios().VerificaExistenciaDocumento(Convert.ToInt64(lblContrato.Text), 1) > 0)
                {

                    CarregarDocumentosContrato();
                    tabPrincipal.TabPages.Remove(tbContrato);
                    tabPrincipal.TabPages.Add(tbpDocumentos);
                }
                else
                {
                    termosCondicoesTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                    termosCondicoesTableAdapter.Fill(clubeConteza_Relatorios.TermosCondicoes,
                        Convert.ToInt64(lblContrato.Text));

                    rpvTermosECondicoes.LocalReport.SetParameters(
                        new Microsoft.Reporting.WinForms.ReportParameter("Path",
                            @"File://C:\Temp\" + lblContrato.Text + ".jpg"));

                    rpvTermosECondicoes.RefreshReport();
                    tabPrincipal.TabPages.Remove(tbContrato);
                    tabPrincipal.TabPages.Add(tbpTermoDeUso);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuTermoDeUsoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpTermoDeUso);
            tabPrincipal.TabPages.Add(tbContrato);

            var assinatura = new frmAssinatura(Convert.ToInt64(lblContrato.Text));
            assinatura.Show();
        }
        private void mnuDocumentosFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpDocumentos);
            tabPrincipal.TabPages.Add(tbContrato);
        }
        private void mnuTermoDeUsoSalvar_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            ContratoDocController documento =
                new ContratoDocController
                {
                    TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB029_TipoS = "1",
                    TB012_VSContrato = 1,
                    TB012_id = Convert.ToInt64(lblContrato.Text),
                    TB029_DocImpressao = rpvTermosECondicoes.LocalReport.Render("Pdf", null, out mimeType,
                        out encoding, out extension, out streamids, out warnings)
                };

            ContratoDocNegocios docN = new ContratoDocNegocios();
            ContratoDocController doc = docN.DocImpressaoInserir(documento);

            if (doc.TB029_Id > 0)
            {
                MessageBox.Show(MensagensDoSistema._0017, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mnuContratoDocumentos_Click(object sender, EventArgs e)
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
        private void ddgDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                gprAnexarTermo.Visible = false;
                label134.Text = "";
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
        private void mnuContratoDependentesIncluir_Click(object sender, EventArgs e)
        {
            List<ParcelaController> parcelaR = new ParcelaNegocios().ParcelasVencidasContrato(Convert.ToInt64(lblContrato.Text), DateTime.Now);

            if (parcelaR.Count > 0)
            {
                MessageBox.Show(Format(MensagensDoSistema._0058, parcelaR.Count.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                return;
            }

            tabPrincipal.TabPages.Add(tbpDependente);

            mskContratoDependenteCEP.Text = mskContratoTitularCEP.Text;
            cmbDependentePais.SelectedValue = cmbTitularPais.SelectedValue;

            var filtroUfDependente = new PaisController { TB003_id = 1058 };
            PopularEstadosDependente(filtroUfDependente);

            cmbContratoDependenteEstado.SelectedValue = cmbContratoTitularEstado.SelectedValue;

            var filtroNunicipioDependente = new EstadoController { TB005_Id = Convert.ToInt64(cmbContratoDependenteEstado.SelectedValue) };
            PopularMunicipiosDependente(filtroNunicipioDependente);

            cmbContratoDependenteMunicipio.SelectedValue = cmbContratoTitularMunicipio.SelectedValue;

            txtContratoDependenteLogradouro.Text = txtContratoTitularLogradouro.Text;
            txtContratoDependenteNumero.Text = txtContratoTitularNumero.Text;
            txtContratoDependenteBairro.Text = txtContratoTitularBairro.Text;
            txtContratoDependenteComplemento.Text = txtContratoTitularComplemento.Text;

            if (Convert.ToInt16(cmbContratoTitularSexo.SelectedValue) == 2)
            {
                txtDependentePai.Text = txtContratoTitularNomeCompleto.Text;
                dtDependentePaiDataNascimento.Value = dtContratoTitularDataNascimento.Value;
            }
            else
            {
                txtDependenteMae.Text = txtContratoTitularNomeCompleto.Text;
                dtDependenteMaeDataNascimento.Value = dtContratoTitularDataNascimento.Value;
            }

            tabPrincipal.TabPages.Remove(tbContrato);
        }
        private void mnuDependenteFechar_Click(object sender, EventArgs e)
        {


            FiltrarContrato(Convert.ToInt64(lblContrato.Text));

            DTContratoDependenteContatos.Rows.Clear();
            DTContratoDependenteContatos.Refresh();


            lblDependenteId.Text = "";
            mskContratoDependenteCPF.Text = "";
            txtDependenteNomeCompleto.Text = "";

            mskContratoDependenteCEP.Text = "";
            txtContratoDependenteLogradouro.Text = "";
            txtContratoDependenteNumero.Text = "";
            txtContratoDependenteBairro.Text = "";
            txtContratoDependenteComplemento.Text = "";
            cmbContratoDependenteMunicipio.DataSource = null;
            cmbContratoDependenteMunicipio.Items.Clear();

            cmbContratoDependenteEstado.DataSource = null;
            cmbContratoDependenteEstado.Items.Clear();

            DTContratoDependenteContatos.DataSource = null;
            DTContratoDependenteContatos.Refresh();
            tabPrincipal.TabPages.Remove(tbpDependente);

        }
        private void mnuDependenteSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidaDependente()) return;
            var pessoa = new PessoaController
            {
                Municipio = new MunicipioController(),
                TB013_id = lblDependenteId.Text.Trim() != Empty ? Convert.ToInt64(lblDependenteId.Text) : 0,
                TB013_TipoS = "1",
                TB012_Id = Convert.ToInt64(lblContrato.Text),
                TB013_CPFCNPJ = mskContratoDependenteCPF.Text.Trim(),
                TB013_RG = txtDependenteRG.Text,
                TB013_RGOrgaoEmissor = txtDependenteEmissor.Text,
                TB012_EraContezino = Convert.ToInt16(chkEraContezino.Checked),
                TB013_ListaNegra = 0,
                TB013_NomeCompleto = txtDependenteNomeCompleto.Text.Replace("Ç", "C").Replace("ç", "c").Replace("é", "e").Replace("â", "a").Replace("ã", "a"),
                TB013_NomeExibicao = txtDependenteNomeCompleto.Text.Replace("Ç", "C").Replace("Á", "A")
                    .Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U"),
                TB013_SexoS = cmbContratoDependenteSexo.SelectedValue.ToString(),
                TB013_DataNascimento = dtContratoDependenteDataNascimento.Value,
                TB004_Cep = mskContratoDependenteCEP.Text,
                TB013_CodigoDependente =
                    new PessoaNegocios().PessoaCodigoDependenteNovo(Convert.ToInt64(lblContrato.Text)),
                TB012_Corporativo = lblContratoCorporativo.Text.Trim() == Empty
                    ? 0
                    : Convert.ToInt64(lblContratoCorporativo.Text)
            };


            pessoa.Municipio.TB006_id = Convert.ToInt64(cmbContratoDependenteMunicipio.SelectedValue);
            pessoa.TB013_Logradouro = txtContratoDependenteLogradouro.Text;
            pessoa.TB013_Numero = txtContratoDependenteNumero.Text;
            pessoa.TB013_Bairro = txtContratoDependenteBairro.Text;
            pessoa.TB013_Complemento = txtContratoDependenteComplemento.Text;
            pessoa.TB013_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            pessoa.TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            pessoa.TB013_MaeNome = txtDependenteMae.Text;
            pessoa.TB013_MaeDataNascimento = dtDependenteMaeDataNascimento.Value;
            pessoa.TB013_PaiNome = txtDependentePai.Text;
            pessoa.TB013_PaiDataNascimento = dtDependentePaiDataNascimento.Value;

            pessoa.TB013_CartaoSolicitado = Convert.ToInt16(chkPortadorCartaoChip.Checked);


            pessoa.TB012_Parceiro = lblContratoParceiro.Text.Trim() != Empty ? Convert.ToInt64(lblContratoParceiro.Text) : 0;


            if (lblDependenteId.Text.Trim() == Empty)
            {
                NovoDependente(pessoa);
            }
            else
            {
                if (!new PessoaNegocios().PessoaFamiliarUpdate(pessoa)) return;
                MessageBox.Show(MensagensDoSistema._0018, @"Atualização", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            /**/


        }
        protected virtual bool ValidaDependente()
        {
            if (dtContratoDependenteDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0084, @"Dependente", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (dtDependenteMaeDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0084, @"Mãe", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (dtDependentePaiDataNascimento.Value > DateTime.Now)
            {
                MessageBox.Show(MensagensDoSistema._0084, @"Pai", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (txtDependenteNomeCompleto.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo do Dependente"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDependenteNomeCompleto.Focus();
                return false;
            }



            if (mskContratoDependenteCEP.Text.Trim().Replace("-", "") == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CEP"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoDependenteCEP.Focus();
                return false;
            }

            if (mskContratoDependenteCEP.Text.Trim().Replace("-", "").Length < 8)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CEP"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoDependenteCEP.Focus();
                return false;
            }

            if (txtContratoDependenteLogradouro.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Logradouro"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoDependenteLogradouro.Focus();
                return false;
            }

            if (txtContratoDependenteNumero.Text.Trim() == Empty)
            {
                txtContratoDependenteNumero.Text = @"S/N.";
                return false;
            }

            if (txtContratoDependenteBairro.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Bairro"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoDependenteBairro.Focus();
                return false;
            }

            if (txtDependenteMae.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome da Mâe"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDependenteMae.Focus();
                return false;
            }

            if (txtDependentePai.Text.Trim() == Empty)
            {
                //MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome do pai"), @"Erro",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txtDependentePai.Focus();
                //return false;
                txtDependentePai.Text = "NÃO INFORMADO";
            }

            if (dtDependenteMaeDataNascimento.Value > dtContratoDependenteDataNascimento.Value)
            {
                MessageBox.Show(@"Data Nascimento mãe menor que titular", @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtDependenteMaeDataNascimento.Focus();
                return false;
            }

            if (dtDependentePaiDataNascimento.Value > dtContratoDependenteDataNascimento.Value)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Data Nascimento pai"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtDependentePaiDataNascimento.Focus();
                return false;
            }

            return true;
        }
        private void NovoDependente(PessoaController pessoa)
        {
            try
            {
                lblDependenteId.Text = new PessoaNegocios().DependenteFamiliarInsert(pessoa).ToString();
                MessageBox.Show(MensagensDoSistema._0017, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DTContratoDependentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (DTContratoDependentes.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "id":
                            var dependente = new PessoaNegocios().DependenteFamiliarSelect(Convert.ToInt64(DTContratoDependentes.Rows[e.RowIndex].Cells["DependenteTB013_id"].Value));

                            tabPrincipal.TabPages.Add(tbpDependente);

                            lblDependenteId.Text = dependente.TB013_id.ToString();
                            txtDependenteEmissor.Text = dependente.TB013_Cartao;
                            txtDependenteNomeCompleto.Text = dependente.TB013_NomeCompleto;
                            dtContratoDependenteDataNascimento.Value = dependente.TB013_DataNascimento;
                            mskContratoDependenteCPF.Text = dependente.TB013_CPFCNPJ;
                            txtDependenteRG.Text = dependente.TB013_RG;
                            txtDependenteEmissor.Text = dependente.TB013_RGOrgaoEmissor;
                            mskContratoDependenteCEP.Text = dependente.TB004_Cep;
                            txtContratoDependenteLogradouro.Text = dependente.TB013_Logradouro;
                            txtContratoDependenteNumero.Text = dependente.TB013_Numero;
                            txtContratoDependenteBairro.Text = dependente.TB013_Bairro;
                            txtContratoDependenteComplemento.Text = dependente.TB013_Complemento;
                            cmbDependentePais.SelectedValue = dependente.Municipio.Estado.Pais.TB003_id;
                            lblDependenteStatus.Text = Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(dependente.TB013_StatusS));
                            PaisController pais = new PaisController();
                            pais.TB003_id = dependente.Municipio.Estado.Pais.TB003_id;
                            PopularEstadosDependente(pais);
                            cmbContratoDependenteEstado.SelectedValue = dependente.Municipio.Estado.TB005_Id;
                            EstadoController municipio = new EstadoController();
                            municipio.TB005_Id = dependente.Municipio.Estado.TB005_Id;
                            PopularMunicipiosDependente(municipio);
                            cmbContratoDependenteMunicipio.SelectedValue = dependente.Municipio.TB006_id;

                            List<ContatoController> contatosDependente = new ContatoNegocios().contatosDaPessoa(dependente.TB013_id);

                            for (int i = 0; i < contatosDependente.Count; i++)
                            {
                                DTContratoDependenteContatos.Rows.Add(new object[] { contatosDependente[i].TB009_id });
                                DTContratoDependenteContatos.Rows[i].Cells["cmbContratoDependenteContatoTipo"].Value = contatosDependente[i].TB009_TipoS;
                                DTContratoDependenteContatos.Rows[i].Cells["txtContratoDependenteContato"].Value = contatosDependente[i].TB009_Contato;
                            }
                            txtDependenteMae.Text = dependente.TB013_MaeNome;
                            dtDependenteMaeDataNascimento.Value = dependente.TB013_MaeDataNascimento;
                            txtDependentePai.Text = dependente.TB013_PaiNome;
                            dtDependentePaiDataNascimento.Value = dependente.TB013_PaiDataNascimento;
                            tabPrincipal.TabPages.Remove(tbContrato);
                            cmbContratoDependenteSexo.SelectedValue = dependente.TB013_SexoS;

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCancelDependenteFechar_Click(object sender, EventArgs e)
        {
            FiltrarContrato(Convert.ToInt64(lblContrato.Text));

            lblCancelamentoDependenteId.Text = "";
            lblCancelamentoDependenteNomeCompleto.Text = "";
            txtCancelamentoDependenteDescricao.Text = "";
            tabPrincipal.TabPages.Remove(tbpDependenteCancelar);
        }

        private void mnuContratoDependentesCancelar_Click(object sender, EventArgs e)
        {
            List<ParcelaController> parcelaR = new ParcelaNegocios().ParcelasVencidasContrato(Convert.ToInt64(lblContrato.Text), DateTime.Now);

            if (parcelaR.Count > 0)
            {
                MessageBox.Show(Format(MensagensDoSistema._0058, parcelaR.Count.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                return;
            }

            try
            {
                ContratoNegocios contratoN = new ContratoNegocios();
                if (Convert.ToInt16((int)((ContratosController.TB012_StatusE)Enum.Parse(typeof(ContratosController.TB012_StatusE), cmbContratoStatus.SelectedValue.ToString()))) == 0 || Convert.ToInt16((int)((ContratosController.TB012_StatusE)Enum.Parse(typeof(ContratosController.TB012_StatusE), cmbContratoStatus.SelectedValue.ToString()))) == 1)
                {
                    tabPrincipal.TabPages.Add(tbpDependenteCancelar);

                    dtgDependentesCancelar.AutoGenerateColumns = false;
                    dgwCancelarDependenteParcelaEmAberto.AutoGenerateColumns = false;
                    dtgDependentesCancelar.DataSource = null;
                    dgwCancelarDependenteParcelaEmAberto.DataSource = null;

                    var retorno = contratoN.contratoSelect(Convert.ToInt64(lblContrato.Text));

                    dtgDependentesCancelar.DataSource = retorno.Dependentes;
                    dtgDependentesCancelar.Refresh();

                    /*Listar Parcelas em aberto*/

                    dgwCancelarDependenteParcelaEmAberto.DataSource = new ParcelaNegocios().ParcelasEmAberto(Convert.ToInt64(lblContrato.Text));
                    dgwCancelarDependenteParcelaEmAberto.Refresh();

                    tabPrincipal.TabPages.Remove(tbContrato);
                }
                else
                {
                    MessageBox.Show("Não é possível cancelar os dependentes pois o status do titular não está como Cadastrado ou Ativo.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dtgDependentesCancelar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dtgDependentesCancelar.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "id":


                            PessoaNegocios dependenteN = new PessoaNegocios();
                            PessoaController dependente = dependenteN.DependenteFamiliarSelect(Convert.ToInt64(dtgDependentesCancelar.Rows[e.RowIndex].Cells["DependenteCancelarTB013_id"].Value));

                            if (Convert.ToInt16((int)((PessoaController.TB013_StatusE)Enum.Parse(typeof(PessoaController.TB013_StatusE), dtgDependentesCancelar.Rows[e.RowIndex].Cells["DependenteCancelarTB013_StatusS"].Value.ToString()))) == 2)
                            {
                                mnuCancelDependenteAcao.Text = @"Ativar";
                            }
                            else
                            {
                                mnuCancelDependenteAcao.Text = @"Inativar";
                            }

                            lblCancelamentoDependenteId.Text = dependente.TB013_id.ToString();
                            lblCancelamentoDependenteNomeCompleto.Text = dependente.TB013_NomeCompleto;
                            lblCancelamentoDependenteStatus.Text = dependente.TB013_StatusS;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCancelamento()
        {
            if (lblCancelamentoDependenteId.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dependente"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbCancelamentoDependenteMotivo.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Motivo"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtCancelamentoDependenteDescricao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Descrição do cancelamento"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ParcelaNegocios parcelaN = new ParcelaNegocios();
            List<ParcelaController> parcelaR = parcelaN.ParcelasVencidasContrato(Convert.ToInt64(lblContrato.Text), DateTime.Now);

            if (parcelaR.Count > 0)
            {
                MessageBox.Show(Format(MensagensDoSistema._0058, parcelaR.Count.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));

                return false;
            }

            return true;
        }
        private void mnuCancelDependenteAcao_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt16(lblCancelamentoDependenteStatus.Text) == 3)
            {
                MessageBox.Show(MensagensDoSistema._0074, @"Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Convert.ToInt16(lblCancelamentoDependenteStatus.Text) == 2)
            {
                MessageBox.Show(MensagensDoSistema._0075, @"Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(@"Deseja realmente initivar este dependente?", @"Dependente", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;

            }


            if (mnuCancelDependenteAcao.Text == @"Inativar")
            {
                if (ValidarCancelamento())
                {
                    InativarDependente();
                    mnuContratoSolicitarAlteracao.Visible = true;
                }
            }
        }
        private void InativarDependente()
        {
            /*Alterar Status Dependente para 3 = Inativar*/
            PessoaNegocios pessoaN = new PessoaNegocios();

            if (pessoaN.DependenteAlterarStatus(Convert.ToInt64(lblCancelamentoDependenteId.Text), 3, Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id))
            {
                if (dgwCancelarDependenteParcelaEmAberto.RowCount > 0)
                {
                    List<ParcelaController> parcelasCancelar = new List<ParcelaController>();
                    for (int i = 0; i < dgwCancelarDependenteParcelaEmAberto.RowCount; i++)
                    {
                        ParcelaController parcelaCancelar = new ParcelaController();
                        parcelaCancelar.TB016_id = Convert.ToInt64(dgwCancelarDependenteParcelaEmAberto.Rows[i].Cells["DependenteCancelarTB016_id"].Value);
                        parcelaCancelar.TB016_Vencimento = Convert.ToDateTime(dgwCancelarDependenteParcelaEmAberto.Rows[i].Cells["DependenteCancelarTB016_Vencimento"].Value);
                        parcelaCancelar.TB016_Valor = Convert.ToDouble(dgwCancelarDependenteParcelaEmAberto.Rows[i].Cells["DependenteCancelarTB016_Valor"].Value);

                        parcelasCancelar.Add(parcelaCancelar);
                    }
                    /*Cancelar Parcelas*/
                    ParcelaNegocios parcelaN = new ParcelaNegocios();

                    if (parcelaN.ParcelasCancelar(parcelasCancelar, ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text), txtContratoTitularNomeCompleto.Text.TrimEnd(), mskContratoTitularCPF.Text))
                    {


                        dgwCancelarDependenteParcelaEmAberto.DataSource = parcelaN.ParcelasEmAberto(Convert.ToInt64(lblContrato.Text));
                        dgwCancelarDependenteParcelaEmAberto.Refresh();

                        lblCancelamentoDependenteNomeCompleto.Text = "";
                        txtCancelamentoDependenteDescricao.Text = "";
                        lblCancelamentoDependenteId.Text = "";

                        ContratoNegocios contratoN = new ContratoNegocios();
                        ContratosController retorno = contratoN.contratoSelect(Convert.ToInt64(lblContrato.Text));

                        dtgDependentesCancelar.DataSource = retorno.Dependentes;
                        dtgDependentesCancelar.Refresh();

                        MessageBox.Show(MensagensDoSistema._0034, @"Inativar Dependente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DTContratoDependentes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    int value;
                    if (e.Value != null && int.TryParse(e.Value.ToString(), out value))
                    {
                        if (Convert.ToInt16((int)((PessoaController.TB013_StatusE)Enum.Parse(
                                typeof(PessoaController.TB013_StatusE),
                                DTContratoDependentes.Rows[e.RowIndex].Cells["DependenteTB013_StatusS"].Value
                                    .ToString()))) == 0)
                        {
                            DTContratoDependentes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else
                        {
                            if (Convert.ToInt16((int)((PessoaController.TB013_StatusE)Enum.Parse(
                                    typeof(PessoaController.TB013_StatusE),
                                    DTContratoDependentes.Rows[e.RowIndex].Cells["DependenteTB013_StatusS"].Value.ToString()))) == 1)
                            {
                                DTContratoDependentes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                            }
                            else
                            {
                                if (Convert.ToInt16((int)((PessoaController.TB013_StatusE)Enum.Parse(
                                        typeof(PessoaController.TB013_StatusE),
                                        DTContratoDependentes.Rows[e.RowIndex].Cells["DependenteTB013_StatusS"].Value.ToString()))) == 2)
                                {
                                    DTContratoDependentes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                                }
                                else
                                {
                                    if (Convert.ToInt16((int)((PessoaController.TB013_StatusE)Enum.Parse(
                                            typeof(PessoaController.TB013_StatusE),
                                            DTContratoDependentes.Rows[e.RowIndex].Cells["DependenteTB013_StatusS"].Value.ToString()))) == 3)
                                    {
                                        DTContratoDependentes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
                                    }
                                }
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
        private void mnuContratoSolicitarAlteracao_Click(object sender, EventArgs e)
        {
            try
            {
                tabPrincipal.TabPages.Add(tbpAlteracaoContrato);

                rptAlteracaoContrato.LocalReport.EnableExternalImages = true;

                planoFamiliarAlteracaoTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                rptAlteracaoContrato.LocalReport.SetParameters(
                    new Microsoft.Reporting.WinForms.ReportParameter("Path",
                        @"File://C:\Temp\" + lblContrato.Text + ".jpg"));

                planoFamiliarAlteracaoTableAdapter.Fill(clubeConteza_Relatorios.PlanoFamiliarAlteracao, Convert.ToInt64(lblContrato.Text));



                rptAlteracaoContrato.RefreshReport();

                tabPrincipal.TabPages.Remove(tbContrato);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuContratoAlteracaoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbContrato);
            tabPrincipal.TabPages.Remove(tbpAlteracaoContrato);
        }
        private void assinaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpAlteracaoContrato);
            tabPrincipal.TabPages.Add(tbContrato);

            frmAssinatura assinatura = new frmAssinatura(Convert.ToInt64(lblContrato.Text));
            assinatura.Show();
        }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                var documento = new ContratoDocController
                {
                    TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB029_TipoS = "2",
                    TB012_VSContrato = new ContratoNegocios().ContratoVSAtual(Convert.ToInt64(lblContrato.Text))
                };

                documento.TB012_VSContrato++;
                documento.TB012_id = Convert.ToInt64(lblContrato.Text);
                documento.TB029_DocImpressao = rptAlteracaoContrato.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                var doc = new ContratoDocNegocios().DocImpressaoInserir(documento);

                if (doc.TB029_Id > 0)
                {

                    new PessoaNegocios().DependentesParaInativacao(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id, documento.TB012_VSContrato);
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuParcelaEmitirBoletos_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tb012Status == 2)
                {
                    MessageBox.Show(Format(MensagensDoSistema._0102, "Bloqueado", "Erro", MessageBoxButtons.OK,
                  MessageBoxIcon.Error));
                    return;
                }

                if (_tb012Status == 5)
                {
                    MessageBox.Show(Format(MensagensDoSistema._0102, "Cancelado", "Erro", MessageBoxButtons.OK,
                  MessageBoxIcon.Error));
                    return;
                }

                new ParcelaNegocios().ParcelasParaEmissaoBoleto(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id, 0);
                /**/
                ddtParcelas.AutoGenerateColumns     = false;
                ddtParcelas.DataSource              = null;
                ddtParcelaItens.AutoGenerateColumns = false;
                ddtParcelaItens.DataSource          = null;

                if (cmbDiaVencimento.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"), @"Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Debug.Assert(cmbParcelaCiclo.ComboBox != null, "cmbParcelaCiclo.ComboBox != null");

                var ciclo = new ParcelaNegocios().ListarCiclosAtivosContrato(Convert.ToInt64(lblContrato.Text));
                if (ciclo.Count > 0)
                {
                    for (int i = 0; i < ciclo.Count; i++)
                    {
                        cmbParcelaCiclo.Items.Add(Convert.ToInt64(ciclo[i].TB012_CicloContrato));
                    }
                    cmbParcelaCiclo.SelectedIndex = ciclo.Count - 1;
                }
                else
                {
                    cmbParcelaCiclo.Items.Add(Convert.ToInt64(lblCiclo.Text));
                    cmbParcelaCiclo.SelectedIndex = 0;
                }

                var parcelas =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
                if (parcelas.Count == 0)
                {
                    mnuParcelaGerarParcelas.Enabled = true;
                }
                else
                {
                    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcelas[parcelas.Count - 1].TB016_StatusS))) == 3)
                    {
                        mnuParcelaGerarParcelas.Enabled = true;
                    }
                    else
                    {
                        mnuParcelaGerarParcelas.Enabled = false;
                    }

                    ddtParcelas.DataSource = parcelas;
                    ddtParcelas.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuBoletoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbpParcelas);
            tabPrincipal.TabPages.Remove(tbBoletos);
        }
        private void picBoletoVisualizar_Click(object sender, EventArgs e)
        {
            try
            {

                //if (Directory.Exists(@"c:\Temp"))
                //{
                //    var dir = new DirectoryInfo(@"c:\Temp");

                //    foreach (var fi in dir.GetFiles())
                //    {
                //        fi.Delete();
                //    }
                //}
                //else
                //{
                //    Directory.CreateDirectory(@"c:\Temp");
                //}

                if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 1)
                {
                    var boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(cmbParcelaCiclo.Text));

                    tabPrincipal.TabPages.Add(tbBoletos);
                    tabPrincipal.TabPages.Remove(tbpParcelas);

                    //int cont = Convert.ToInt16(lblBoletosQuant.Text) - 1;
                    //_doc = webBoleto.Document;
                    //// ReSharper disable once PossibleNullReferenceException
                    //if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[cont].TB016_Boleto;
                    //_doc.Title = boletosEmitidos[cont].TB016_id.ToString();
                    //lblBoletosQuant.Text = cont.ToString();

                    byte[] boleto = new byte[boletosEmitidos[0].TB016_Boleto.Length * sizeof(char)];
                    System.Buffer.BlockCopy(boletosEmitidos[0].TB016_Boleto.ToCharArray(), 0, boleto, 0, boleto.Length);
                   

                    string path = @"C:\Temp\" + boletosEmitidos[0].TB016_id + ".htm";
                    FileInfo arquivo = new FileInfo(path);
                    arquivo.Delete();
                    FileStream fs2 = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                    fs2.Write(boleto, 0, boleto.Length);
                    fs2.Flush();
                    fs2.Close();

                    this.webBoleto.Url = new System.Uri("file:///" + path, System.UriKind.Absolute);
                    /**/
                    //webBoleto.Url = path.ToString();

                    //    _doc = webBoleto.Document;
                    //    if (_doc != null)
                    //    {

                    //        var html = boletosEmitidos[0].TB016_Boleto;

                    //        //_doc.Body.InnerHtml = html.ToString().Replace("class=p0", "class='p0'").Replace("class=b0", "class='b0'").Replace("class=b1", "class='b1'").Replace("class=p1", "class='p1'");

                    //        webBoleto.Document.Body.InnerHtml = boletosEmitidos[0].TB016_Boleto;

                    //        if (_doc.Body != null) _doc.Body.InnerHtml = html;// boletosEmitidos[0].TB016_Boleto;
                    //        _doc.Title = boletosEmitidos[0].TB016_id.ToString();
                    //    }
                    lblBoletosQuant.Text = @"0";
                }
                else
                {
                    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusS.Text))) == 1 || Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusS.Text))) == 2)
                    {
                        tabPrincipal.TabPages.Add(tpPagamento);
                        mnuPagamentoEmitir.Visible = false;
                        mnuPagamentoDebito.Visible = false;
                        mnuPagamentoDinheiro.Visible = false;
                        tbPrincipal.TabPages.Remove(tbpDebito);
                        tbPrincipal.TabPages.Remove(tbpDinheiro);
                        tbPrincipal.TabPages.Remove(tbpCredito);
                        CarregarDadosParcela();
                        tabPrincipal.TabPages.Remove(tbpParcelas);
                    }
                    else
                    {
                        try
                        {
                            CarregarDocumentosContrato();
                            tabPrincipal.TabPages.Remove(tbContrato);
                            tabPrincipal.TabPages.Add(tbpDocumentos);
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
        private void btnBoletoAnterior_Click(object sender, EventArgs e)
        {
            List<ParcelaController> boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(cmbParcelaCiclo.Text));
            if (Convert.ToInt16(lblBoletosQuant.Text) > 0)
            {
                int cont = Convert.ToInt16(lblBoletosQuant.Text) - 1;
                _doc = webBoleto.Document;
                // ReSharper disable once PossibleNullReferenceException
                if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[cont].TB016_Boleto;
                _doc.Title = boletosEmitidos[cont].TB016_id.ToString();
                lblBoletosQuant.Text = cont.ToString();
            }
        }
        private void btnBoletoProximo_Click(object sender, EventArgs e)
        {
            List<ParcelaController> boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(cmbParcelaCiclo.Text));
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
        private void mnuBoletoImprimirTodos_Click(object sender, EventArgs e)
        {
            List<ParcelaController> boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(cmbParcelaCiclo.Text));
            for (int i = 0; i < boletosEmitidos.Count; i++)
            {
                _doc = webBoleto.Document;
                Debug.Assert(_doc != null, nameof(_doc) + " != null");
                if (_doc.Body != null) _doc.Body.InnerHtml = boletosEmitidos[i].TB016_Boleto;
                _doc.Title = boletosEmitidos[i].TB016_id.ToString();
                webBoleto.Print();
            }
        }
        private void mnuImprimirAtual_Click(object sender, EventArgs e)
        {
            webBoleto.Print();
            MessageBox.Show(@"Documento enviado para impressora", @"Impressão", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        wbPrintString.Document.Title = "Boletro Clube Conteza";
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
        private void mnuCarneFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbBoletos);
            tabPrincipal.TabPages.Remove(tbCarne);
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
            carneTableAdapter.Fill(clubeConteza_Relatorios.Carne, Convert.ToInt64(lblContrato.Text), Convert.ToInt32(cmbParcelaCiclo.Text));
            rptCarne.RefreshReport();
            tabPrincipal.TabPages.Remove(tbBoletos);

            var setup = rptCarne.GetPageSettings();
            setup.Margins = new Margins(1, 1, 1, 1);
            rptCarne.SetPageSettings(setup);
        }
        private void mnuPagamentoFechar_Click(object sender, EventArgs e)
        {
            rptComprovanteCredito.Visible = false;
            chkParcelaCancelamento.Checked = false;
            lblCredCPFCNPJ.Text = "";
            lblCredNomeCompleto.Text = "";
            lblCredUnidade.Text = "";
            lblCredContrato.Text = "";
            lblCredPlano.Text = "";
            lblCredParcelaId.Text = "";
            lblCredParcela.Text = "";
            lblCredEmissao.Text = "";
            lblCredVencimento.Text = "";
            lblCredValorTotal.Text = "";
            txtCredNCartao.Text = "";
            mskCredCPFTitularCartao.Text = "";
            txtCredNomeCompletoTitularCartao.Text = "";
            lblCredValorMinimoParcela.Text = "";
            lblCredValorParcela.Text = "";
            lblCredDescricaoPagamento.Text = "";
            txtCredAutorizacao.Text = "";
            txtCredCodValidador.Text = "";
            lblCredTipoVencimento.Text = "";
            lblCredVencimentoCartao.Text = "";
            lblCredTaxas.Text = "";
            lblCredValorCredito.Text = "";
            txtDebAutorizacao.Text = "";
            txtDebCodValidador.Text = "";
            lblDebValorParcela.Text = "";
            lblDebTaxas.Text = "";
            lblDebValorCredito.Text = "";
            lblDebDescricaoFormaPagamento.Text = "";
            lblDebVencimentoCartao.Text = "";
            lblDebVencimento.Text = "";


            tabPrincipal.TabPages.Add(tbpParcelas);
            tabPrincipal.TabPages.Remove(tpPagamento);
            ListarParcelas(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(cmbParcelaCiclo.SelectedItem));
        }
        private void CarregarDadosParcela()
        {
            try
            {
                ParcelaNegocios parcelaN = new ParcelaNegocios();
                ParcelaController retorno = parcelaN.ParcelaPagamento(Convert.ToInt64(lblParcelaId.Text));
                chkParcelaCancelamento.Checked = Convert.ToBoolean(retorno.TB016_ParcelaCancelamento);

                lblCredUnidade.Text = retorno.Empresa.TB001_id.ToString();
                lblCredContrato.Text = retorno.TB012_id.ToString();
                lblCredCPFCNPJ.Text = retorno.TB016_CPFCNPJ;
                mskCredCPFTitularCartao.Text = retorno.TB016_CPFCNPJ;
                lblCredNomeCompleto.Text = retorno.TB016_Pagador;
                txtCredNomeCompletoTitularCartao.Text = retorno.TB016_Pagador;
                lblCredPlano.Text = retorno.Plano.TB015_Plano;
                lblCredParcelaId.Text = retorno.TB016_id.ToString();
                lblCredParcela.Text = retorno.TB016_Parcela.ToString();
                lblCredEmissao.Text = retorno.TB016_Emissao.ToString("dd/MM/yyyy");
                lblCredVencimento.Text = retorno.TB016_Vencimento.ToString("dd/MM/yyyy");
                lblCredValorTotal.Text = Format("{0:C2}", Convert.ToDouble(retorno.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));


                switch (retorno.TB016_FormaPagamentoS)
                {
                    case "6":
                        tbPrincipal.TabPages.Add(tbpCredito);

                        cmbCredBandeira.DataSource = null;
                        cmbCredBandeira.Items.Clear();
                        cmbCredBandeira.DataSource = parcelaN.ListarBandeiraCartao();
                        cmbCredBandeira.DisplayMember = "TB032_BandeiraCartao";
                        cmbCredBandeira.ValueMember = "TB032_Id";
                        txtCredNCartao.Focus();
                        mnuPagamentoEmitir.Visible = true;
                        break;
                    case "3":
                        tbPrincipal.TabPages.Add(tbpDebito);
                        lblDebValorParcela.Text = Format("{0:C2}", Convert.ToDouble(retorno.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));


                        var formaPagamento = parcelaN.SelectFormaParamento(3);

                        lblDebDescricaoFormaPagamento.Text = formaPagamento.TB031_Descricao;
                        lblDebVencimento.Text = formaPagamento.TB031_TipoVencimento.ToString();
                        var vencimentoCartao = DateTime.Now.AddDays(formaPagamento.TB031_DVencimento);
                        lblDebVencimentoCartao.Text = vencimentoCartao.ToString("dd/MM/yyyy");
                        lblDebTaxas.Text = double.Parse(formaPagamento.TB031_Taxa.ToString(CultureInfo.InvariantCulture)).ToString("C2");

                        var valorCredito = Format("{0:C2}", Convert.ToDouble(retorno.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));


                        lblDebValorCredito.Text = Format("{0:C2}", Convert.ToDouble(retorno.TB016_Valor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                        /**/

                        mnuPagamentoDebito.Visible = true;
                        txtCredNCartao.Focus();
                        break;
                    case "2":
                        mnuPagamentoDinheiro.Visible = true;

                        txtCredNCartao.Enabled = false;
                        mskCredCPFTitularCartao.Enabled = false;
                        txtCredNomeCompletoTitularCartao.Enabled = false;

                        tbPrincipal.TabPages.Add(tbpDinheiro);
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmbCredBandeira_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbCredBandeira.SelectedValue) <= 0) return;
                cmbCredParcelas.DataSource = null;
                cmbCredParcelas.Items.Clear();

                cmbCredParcelas.DataSource = new ParcelaNegocios().ListaParcelamentoPossivelPorBandeira(Convert.ToInt64(cmbCredBandeira.SelectedValue), Convert.ToInt64(lblCredUnidade.Text));
                cmbCredParcelas.DisplayMember = "TB031_NParcelas";
                cmbCredParcelas.ValueMember = "TB031_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbCredParcelas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                var retorno = new ParcelaNegocios().SelectFormaParamento(Convert.ToInt64(cmbCredParcelas.SelectedValue));

                lblCredValorMinimoParcela.Text = double.Parse(retorno.TB031_ValorMinimoParcela.ToString(CultureInfo.CurrentCulture).Replace(".", ",")).ToString("C2");
                lblCredDescricaoPagamento.Text = retorno.TB031_Descricao;
                lblCredTipoVencimento.Text = retorno.TB031_TipoVencimento.ToString();
                var vencimentoCartao = DateTime.Now.AddDays(retorno.TB031_DVencimento);
                lblCredVencimentoCartao.Text = vencimentoCartao.ToString("dd/MM/yyyy");
                lblCredTaxas.Text = double.Parse(retorno.TB031_Taxa.ToString(CultureInfo.CurrentCulture).Replace(".", ",")).ToString("C2");

                double valorCredito = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", "")) - Convert.ToDouble(lblCredTaxas.Text.Replace("R$", ""));


                lblCredValorCredito.Text = double.Parse(valorCredito.ToString(CultureInfo.CurrentCulture).Replace(".", ",")).ToString("C2");


                if (Convert.ToInt16(cmbCredParcelas.Text) > 0)
                {
                    double valor = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""));
                    double minimo = Convert.ToDouble(lblCredValorMinimoParcela.Text.Replace("R$", ""));
                    double resultado = valor / Convert.ToInt16(cmbCredParcelas.Text);
                    lblCredValorParcela.Text = double.Parse(resultado.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                    if (resultado < minimo)
                    {
                        mnuPagamentoEmitir.Enabled = false;
                        MessageBox.Show(Format(MensagensDoSistema._0063, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                    else
                    {
                        mnuPagamentoEmitir.Enabled = true;
                    }
                }
                else
                {
                    lblCredValorMinimoParcela.Text = "";
                    lblCredValorParcela.Text = "";
                    lblCredDescricaoPagamento.Text = "";
                    lblCredVencimentoCartao.Text = "";
                    MessageBox.Show(Format(MensagensDoSistema._0062, "N.º Parcelas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error));
                }
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


            if (txtCredAutorizacao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Autorização"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredAutorizacao.Focus();
                return false;
            }

            if (txtCredCodValidador.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Cod. Validador"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredCodValidador.Focus();
                return false;
            }

            if (txtCredNCartao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNCartao.Focus();
                return false;
            }

            if (mskCredCPFTitularCartao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CPF Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskCredCPFTitularCartao.Focus();
                return false;
            }

            if (txtCredNomeCompletoTitularCartao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNomeCompletoTitularCartao.Focus();
                return false;
            }

            if (!_validacoes.CPF(mskCredCPFTitularCartao.Text))
            {
                MessageBox.Show(MensagensDoSistema._0031, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskCredCPFTitularCartao.Focus();
                return false;
            }

            return true;
        }
        private void mnuPagamentoEmitir_Click(object sender, EventArgs e)
        {
            rptComprovanteCredito.Visible = true;
            if (!ValidarCredito()) return;
            mnuPagamentoEmitir.Enabled = false;
            var pagamento = new ParcelaController
            {
                TB016_id = Convert.ToInt64(lblCredParcelaId.Text),
                TB016_DataPagamento = DateTime.Now,
                TB016_FormaProcessamentoBaixa = 1,
                TB016_CredNCartao = txtCredNCartao.Text,
                TB016_CredCPFTitularCartaoCartao = mskCredCPFTitularCartao.Text,
                TB016_CredNomeTitularCartaoCartao = txtCredNomeCompletoTitularCartao.Text,
                TB016_CredBandeira = Convert.ToInt64(cmbCredBandeira.SelectedValue),
                TB016_CredNParcelas = Convert.ToInt16(cmbCredParcelas.SelectedIndex),
                TB016_CredValorParcelas = Convert.ToDouble(lblCredValorParcela.Text.Replace("R$", "")),
                TB016_CredAutorizacao = txtCredAutorizacao.Text,
                TB016_CredCodValidador = txtCredCodValidador.Text,
                TB012_id = Convert.ToInt64(lblCredContrato.Text),
                TB016_CredFormaParamentoId = Convert.ToInt64(cmbCredParcelas.SelectedValue),
                TB016_CredFormaParamentoDescricao = lblCredDescricaoPagamento.Text,
                TB016_AlteradoEm = DateTime.Now,
                TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                TB016_CredBaixaFeitaEm = DateTime.Now,
                TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                TB016_StatusS = "5",
                TB016_CredDataCredito = Convert.ToDateTime(lblCredVencimentoCartao.Text),
                TB016_ValorPago = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", "")),
                TB016_CredValor = Convert.ToDouble(lblCredValorCredito.Text.Replace("R$", ""))
            };


            if (!new ParcelaNegocios().ParcelaInserirPagamentoCredParcela(pagamento, Convert.ToInt16(chkParcelaCancelamento.Checked))) return;
            new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);
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
        }
        bool ValidarDebito()
        {
            if (!_validacoes.CPF(mskCredCPFTitularCartao.Text))
            {
                MessageBox.Show(MensagensDoSistema._0031, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskCredCPFTitularCartao.Focus();
                return false;
            }

            if (txtCredNCartao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "N.º Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNCartao.Focus();
                return false;
            }


            if (txtCredNomeCompletoTitularCartao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo Titular Cartão"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCredNomeCompletoTitularCartao.Focus();
                return false;
            }


            if (txtDebAutorizacao.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Autorização"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDebAutorizacao.Focus();
                return false;
            }

            if (txtDebCodValidador.Text.Trim() != Empty) return true;
            MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Cod. Validador"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtDebCodValidador.Focus();
            return false;
        }
        private void mnuPagamentoDebito_Click(object sender, EventArgs e)
        {
            rptComprovanteCredito.Visible = true;
            if (!ValidarDebito()) return;
            mnuPagamentoDebito.Enabled = false;
            ParcelaController pagamento = new ParcelaController
            {
                TB016_id = Convert.ToInt64(lblCredParcelaId.Text),
                TB016_DataPagamento = DateTime.Now,
                TB016_FormaProcessamentoBaixa = 1,
                TB016_CredNCartao = txtCredNCartao.Text,
                TB016_CredCPFTitularCartaoCartao = mskCredCPFTitularCartao.Text,
                TB016_CredNomeTitularCartaoCartao = txtCredNomeCompletoTitularCartao.Text,
                TB016_CredBandeira = 0,
                TB016_CredNParcelas = 1,
                TB016_CredValorParcelas = Convert.ToDouble(lblDebValorParcela.Text.Replace("R$", "")),
                TB016_CredAutorizacao = txtDebAutorizacao.Text,
                TB016_CredCodValidador = txtDebCodValidador.Text,
                TB012_id = Convert.ToInt64(lblCredContrato.Text),
                TB016_CredFormaParamentoId = 2,
                TB016_CredFormaParamentoDescricao = lblDebDescricaoFormaPagamento.Text,
                TB016_AlteradoEm = DateTime.Now,
                TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                TB016_CredBaixaFeitaEm = DateTime.Now,
                TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                TB016_StatusS = "5",
                TB016_CredDataCredito = Convert.ToDateTime(lblDebVencimentoCartao.Text),
                TB016_ValorPago = Convert.ToDouble(lblDebValorParcela.Text.Replace("R$", "")),
                TB016_CredValor = Convert.ToDouble(lblDebValorCredito.Text.Replace("R$", ""))
            };

            var pagamentoN = new ParcelaNegocios();
            if (pagamentoN.ParcelaInserirPagamentoCredParcela(pagamento, Convert.ToInt16(chkParcelaCancelamento.Checked)))
            {
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
            }
        }
        private void mnuPagamentoDinheiro_Click(object sender, EventArgs e)
        {
            rptComprovanteCredito.Visible = true;
            var pagamento = new ParcelaController
            {
                TB016_id = Convert.ToInt64(lblCredParcelaId.Text),
                TB016_DataPagamento = DateTime.Now,
                TB016_FormaProcessamentoBaixa = 1,
                TB016_CredNCartao = "0",
                TB016_CredCPFTitularCartaoCartao = lblCredCPFCNPJ.Text,
                TB016_CredNomeTitularCartaoCartao = lblCredNomeCompleto.Text,
                TB016_CredBandeira = 0,
                TB016_CredNParcelas = 1,
                TB016_CredValorParcelas = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", "")),
                TB016_CredAutorizacao = "DINHEIRO",
                TB016_CredCodValidador = "DINHEIRO",
                TB012_id = Convert.ToInt64(lblCredContrato.Text),
                TB016_CredFormaParamentoId = 2,
                TB016_CredFormaParamentoDescricao = "DINHEIRO",
                TB016_AlteradoEm = DateTime.Now,
                TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                TB016_CredBaixaFeitaEm = DateTime.Now,
                TB016_CredBaixaFeitaPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                TB016_StatusS = "5",
                TB016_CredDataCredito = DateTime.Now,
                TB016_ValorPago = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", "")),
                TB016_CredValor = Convert.ToDouble(lblCredValorTotal.Text.Replace("R$", ""))
            };

            ParcelaNegocios pagamentoN = new ParcelaNegocios();
            if (pagamentoN.ParcelaInserirPagamentoCredParcela(pagamento, Convert.ToInt16(chkParcelaCancelamento.Checked)))
            {
                new ComissaoNegocios().ComissaoProcessamento(ParametrosInterface.objUsuarioLogado.TB011_Id);
                pagamentoComprovanteTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                pagamentoComprovanteTableAdapter.Fill(clubeConteza_Relatorios.PagamentoComprovante, pagamento.TB016_id);
                rptComprovanteCredito.RefreshReport();

                ContratoDocNegocios docN = new ContratoDocNegocios();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                ContratoDocController documento = new ContratoDocController();

                documento.TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                documento.TB029_TipoS = "3";
                documento.TB012_id = pagamento.TB012_id;

                documento.TB029_DocImpressao = rptComprovanteCredito.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                docN.DocImpressaoInserir(documento);
            }
        }
        private void mnuCancelarPlanoFechar_Click(object sender, EventArgs e)
        {
            txtDescricao.Text = "";
            tabPrincipal.TabPages.Remove(tbCancelarPlano);           
            FiltrarContrato(Convert.ToInt64(lblContrato.Text));
        }

        private void mnuContratoCancelar_Click(object sender, EventArgs e)
        {


            _tb002Id = Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue);
            tabPrincipal.TabPages.Add(tbCancelarPlano);

            if (Convert.ToInt16(cmbContratoStatus.SelectedValue) == 5)
            {
                mnuCancelarEmitirMulta.Enabled = true;
            }
            else
            {
                mnuCancelarEmitirMulta.Enabled = false;
            }


            tabPrincipal.TabPages.Remove(tbContrato);
            lblCancelarPlanoId.Text = @"28";
            txtCancelamentoDesconto.Text = @"R$ 0,00";

            ddlCancelamentoPlanoVencimento.Value = DateTime.Now.AddDays(2);
            lblValorMulta.Text = Format(Thread.CurrentThread.CurrentCulture, "{0:C}", new ParcelaNegocios().ValorParcela(new ParcelaNegocios().UltimaParcelaContrato(Convert.ToInt64(lblContrato.Text)).TB016_id).TB016_Valor * 3);
            lblCancelamentoValorMulta.Text = lblValorMulta.Text;

            cmbCancelarPlanoMotivo.DataSource = null;
            cmbCancelarPlanoMotivo.Items.Clear();

            cbmCancelamentoFormaPagamento.DataSource = null;
            cbmCancelamentoFormaPagamento.Items.Clear();

            List<KeyValuePair<string, string>> contratoStatus2 = new List<KeyValuePair<string, string>>();
            Array status2 = Enum.GetValues(typeof(ParcelaController.TB016_FormaPagamentoE));
            foreach (ParcelaController.TB016_FormaPagamentoE statu2 in status2)
            {
                contratoStatus2.Add(new KeyValuePair<string, string>(statu2.ToString(), ((int)statu2).ToString()));
            }

            cbmCancelamentoFormaPagamento.DataSource = contratoStatus2;
            cbmCancelamentoFormaPagamento.DisplayMember = "Key";
            cbmCancelamentoFormaPagamento.ValueMember = "Value";

            List<KeyValuePair<string, string>> motivoL = new List<KeyValuePair<string, string>>();
            Array motivoS = Enum.GetValues(typeof(ContratosController.TB012_ContratoCancelarMotivoE));
            foreach (ContratosController.TB012_ContratoCancelarMotivoE motivo in motivoS)
            {
                motivoL.Add(new KeyValuePair<string, string>(motivo.ToString(), ((int)motivo).ToString()));
            }

            cmbCancelarPlanoMotivo.DataSource = motivoL;
            cmbCancelarPlanoMotivo.DisplayMember = "Key";
            cmbCancelarPlanoMotivo.ValueMember = "Value";



        }

        private void btnFecharCartaoManual_Click(object sender, EventArgs e)
        {
            gbxIncluirCartaoManualmente.Visible = false;
        }

        private void btnSalvarCartaoManual_Click(object sender, EventArgs e)
        {
            try
            {
                PessoaNegocios pessoaN = new PessoaNegocios();
                PessoaController cartao = pessoaN.Cartao(mskCartaoManual.Text);

                if (cartao.TB013_id > 0)
                {
                    MessageBox.Show(MensagensDoSistema._0045.Replace("$NOME", cartao.TB013_NomeCompleto).Replace("$ID", cartao.TB013_id.ToString()), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int statuscartao = 1;
                    DateTime dataLegado = Convert.ToDateTime("23/03/2017");

                    if (Convert.ToDateTime(dtContratoInicio.Value) < dataLegado)
                    {
                        statuscartao = 8;
                    }
                    else
                    {
                        if (Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue) == 9)
                        {
                            statuscartao = 2;
                        }
                        else
                        {

                            if (Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue) == 10)
                            {
                                statuscartao = 2;
                            }
                        }
                    }

                    if (pessoaN.CartaoManual(Convert.ToInt64(lblContratoTitularID.Text), mskCartaoManual.Text, statuscartao, ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text)))
                    {
                        lblCarteirinha.Text = mskCartaoManual.Text;
                        mskCartaoManual.Text = "";

                        lblStatusCarteirinha.Text = @"Cartão(" + cbmStatusCartaoManual.Text.Trim() + @")";

                        MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        gbxIncluirCartaoManualmente.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuContratoCartaoManual_Click(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0043, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // if (lblCarteirinha.Text.Trim() != Empty)
                //{
                //    MessageBox.Show(MensagensDoSistema._0044, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //{
                StatusCartao();

                cbmStatusCartaoManual.SelectedValue = "1";
                gbxIncluirCartaoManualmente.Visible = true;
                //}
            }
        }

        private void StatusCartao()
        {
            cbmStatusCartaoManual.DataSource = null;
            cbmStatusCartaoManual.Items.Clear();

            List<KeyValuePair<string, string>> cartaoStatus = new List<KeyValuePair<string, string>>();
            Array status = Enum.GetValues(typeof(PessoaController.TB013_CarteirinhaStatusE));
            foreach (PessoaController.TB013_CarteirinhaStatusE statu in status)
            {
                cartaoStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cbmStatusCartaoManual.DataSource = cartaoStatus;
            cbmStatusCartaoManual.DisplayMember = "Key";
            cbmStatusCartaoManual.ValueMember = "Value";
        }

        private void txtCancelamentoDesconto_Leave(object sender, EventArgs e)
        {

            double desconto = Convert.ToDouble(txtCancelamentoDesconto.Text.Replace("R$", ""));
            txtCancelamentoDesconto.Text = Format(Thread.CurrentThread.CurrentCulture, "{0:C}", desconto);

            lblCancelamentoValorMulta.Text = Format(Thread.CurrentThread.CurrentCulture, "{0:C}", Convert.ToDouble(lblValorMulta.Text.Replace("R$", "")) - desconto);

        }

        private void mnuCancelarEmitirMulta_Click(object sender, EventArgs e)
        {
            try
            {
                #region Gerar Parcela de Multa


                var pontoDevendaEmpresa = new PontoDeVendaNegocios().PontoDeVendaEmpresa(_tb002Id);
                var objTitular = new PessoaNegocios().pessoaSelectId(Convert.ToInt64(lblContratoTitularID.Text));
                var parcela = new ParcelaController
                {
                    Plano = new PlanoController(),
                    Titular = objTitular,
                    TB016_Emissao = DateTime.Now,
                    TB016_BeneficiarioCidade = pontoDevendaEmpresa.Empresa.Cidade,
                    Pessoa = objTitular
                };
                var parcelaItensL = new List<ParcelaProdutosController>();
                parcela.Empresa = new EmpresaController { TB001_id = pontoDevendaEmpresa.Empresa.TB001_id };
                parcela.TB033_Parcela = 1;

                parcela.TB016_ParcelaCancelamento = 1;
                parcela.TB016_TotalParcelas = 1;
                parcela.TB016_Vencimento = ddlCancelamentoPlanoVencimento.Value;
                parcela.TB016_DiaVencimento = parcela.TB016_Vencimento.Day;
                parcela.TB016_DiaFechamento = parcela.TB016_DiaVencimento - 1;
                parcela.TB016_Beneficiario = pontoDevendaEmpresa.Empresa.TB001_RazaoSocial;
                parcela.TB016_BeneficiarioCPFCNPJ = pontoDevendaEmpresa.Empresa.TB001_CNPJ;
                parcela.TB016_BeneficiarioEndereco = pontoDevendaEmpresa.Empresa.TB001_Logradouro.Replace("'", ". ")
                    .Replace("$", ". ").Replace("%", ". ").Replace("*", ". ").Replace("# ", ". ");
                parcela.TB016_BeneficiarioUF = pontoDevendaEmpresa.Empresa.TB001_UF;
                parcela.TB016_CadastradoEm = DateTime.Now;
                parcela.TB016_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                parcela.TB016_AlteradoEm = DateTime.Now;
                parcela.TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                parcela.Pessoa.Municipio.TB006_Municipio        = cmbContratoTitularMunicipio.Text.TrimEnd();

                parcela.Pessoa.Municipio.Estado.TB005_Estado    = objTitular.Municipio.Estado.TB005_Sigla;// cmbContratoTitularEstado.Text.TrimEnd();
                parcela.TB016_TipoSacadoS                       = "1";
                parcela.TB016_TipoVencimento                    = 1;

                parcela.TB016_CPFCNPJ                           = mskContratoTitularCPF.Text.Replace(".", "").Replace(",", "")
                .Replace("/", "").Replace("-", "").Trim();
                parcela.TB016_Pagador                           = txtContratoTitularNomeCompleto.Text.TrimEnd().ToUpper().Replace("'", "/")
                .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                parcela.TB016_PagadorCEP                        = mskContratoTitularCEP.Text.TrimEnd().ToUpper();
                parcela.TB016_PagadorCidade                     = cmbContratoTitularMunicipio.Text.TrimEnd().ToUpper().Replace("'", "/")
                .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");

                parcela.TB016_PagadorUF                         = cmbContratoTitularEstado.Text.TrimEnd().ToUpper().Replace("'", "/")
                .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");

                parcela.TB012_id                                = Convert.ToInt64(lblContrato.Text);
                parcela.TB016_EnderecoPagador                   = txtContratoTitularLogradouro.Text.TrimEnd().ToUpper() + ", " +
                                                            txtContratoTitularNumero.Text.TrimEnd().ToUpper().Replace("'", ". ").Replace("$", ". ").Replace("%", ". ").Replace("*", ". ").Replace("# ", ". ");
                parcela.TB012_CicloContrato                     = Convert.ToInt32(lblCiclo.Text);
                parcela.TB016_FormaPagamentoS                   = cbmCancelamentoFormaPagamento.SelectedValue.ToString();
                parcela.TB016_EmitirBoleto                      = 1;
                parcela.TB016_StatusS                           = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(1));
                PlanoController PlanoVendaSelectId              = new PlanoNegocios().PlanoVendaSelectId(28, 0, 0, 0);
                parcela.Plano.TB015_Maiores                     = 0;
                parcela.Plano.TB015_Menores                     = 0;
                parcela.Plano.TB015_Isentos                     = 0;

                parcela.Plano.TB015_IOF                         = Convert.ToDouble(PlanoVendaSelectId.TB015_IOF);
                parcela.Plano.TB015_TipoVencimento              = Convert.ToInt16(PlanoVendaSelectId.TB015_TipoVencimento);
                parcela.TB016_EspecieDocumento                  = PlanoVendaSelectId.TB015_EspecieDocumento.TrimEnd();
                parcela.TB016_BoletoDesc1                       = PlanoVendaSelectId.TB015_BoletoDesc1.TrimEnd();
                parcela.TB016_BoletoDesc2                       = PlanoVendaSelectId.TB015_BoletoDesc2;
                parcela.TB016_BoletoDesc3                       = PlanoVendaSelectId.TB015_BoletoDesc3;
                parcela.TB016_BoletoDesc4                       = PlanoVendaSelectId.TB015_BoletoDesc4;
                parcela.TB016_BoletoDesc5                       = PlanoVendaSelectId.TB015_BoletoDesc5;
               // parcela.TB016_EspecieDocumento = PlanoVendaSelectId.TB015_EspecieDocumento;
                parcela.TB031_TipoVencimento                    = Convert.ToInt16(PlanoVendaSelectId.TB015_TipoVencimento);
                parcela.TB016_Valor                             = Convert.ToDouble(lblCancelamentoValorMulta.Text.Replace("R$", ""));
                parcela.TB015_id                                = PlanoVendaSelectId.TB015_id;
                parcela.TB015_Plano                             = PlanoVendaSelectId.TB015_Plano;
                parcela.TB016_LoteExportacao                    = -1;

                var planoProdutoN                               = new ProdutoNegocios();
                var planoProdutoL                               = planoProdutoN.ProdutoPlano(parcela.TB015_id);


                foreach (ProdutoController produto in planoProdutoL)
                {
                    ParcelaProdutosController parcelaItem = new ParcelaProdutosController
                    {
                        TB017_id                = produto.TB014_id,
                        TB017_IdProteus         = produto.TB014_IdProtheus,
                        TB017_Item              = produto.TB014_Produto,
                        TB017_Maior             = produto.TB014_Maiores,
                        TB017_Menor             = produto.TB014_Menores,
                        TB017_Isento            = produto.TB014_Isentos
                    };
                    produto.TB014_ValorUnitario     = Convert.ToDouble(lblCancelamentoValorMulta.Text.Replace("R$", ""));
                    parcelaItem.TB017_Item          = parcelaItem.TB017_Item;
                    parcelaItem.TB017_ValorDesconto = Convert.ToDouble(txtCancelamentoDesconto.Text.Replace("R$", ""));
                    var ValorFinal                  = Convert.ToDouble(lblCancelamentoValorMulta.Text.Replace("R$", ""));
                    parcelaItem.TB017_ValorFinal    = Convert.ToDouble(lblCancelamentoValorMulta.Text.Replace("R$", ""));
                    parcelaItem.TB017_ValorUnitario = parcelaItem.TB017_ValorFinal;
                    parcelaItem.TB017_TipoS = "3";
                    parcelaItensL.Add(parcelaItem);
                }

                parcela.ParcelaProduto_L            = parcelaItensL;
                var Valor                           = lblCancelamentoValorMulta.Text.Replace("R$", "");
                parcela.TB016_Valor                 = Convert.ToDouble(lblCancelamentoValorMulta.Text.Replace("R$", ""));
                var parcelasList                    = new List<ParcelaController>();

                parcelasList.Add(parcela);

                if (new ParcelaNegocios().FamiliarParcelaInsert(parcelasList))
                {
                    cmbContratoStatus.SelectedValue = 3;
                    MessageBox.Show(Format(MensagensDoSistema._0078, "\n", "\n"), @"Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCancelarSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ContratoNegocios().FamiliarCancelarContrato(Convert.ToInt64(lblContrato.Text),
                    ParametrosInterface.objUsuarioLogado.TB011_Id, txtDescricao.Text.TrimEnd(),
                    cmbCancelarPlanoMotivo.SelectedValue.ToString()))
                {
                    //return;
                    cmbContratoStatus.SelectedValue = 5;
                    mnuCancelarEmitirMulta.Enabled = true;
                    MessageBox.Show(MensagensDoSistema._0079, @"Cancelamento", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show(@"Erro", @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ddtParcelaItens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                mnuParcelaAbonarAdesao.Enabled = false;
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddtParcelaItens.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "id":
                            var parcelaProduto = new ParcelaNegocios().ParcelaProdutoPesquisaId(Convert.ToInt64(ddtParcelaItens.Rows[e.RowIndex].Cells["TB017_id"].Value));
                            lblParcelaProdutoId.Text = parcelaProduto.TB017_id.ToString();
                            lblParcelaProduto.Text = parcelaProduto.TB017_Item;
                            txtParcelaValorUnitario.Text = double.Parse(parcelaProduto.TB017_ValorUnitario.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                            txtParcelaDesconto.Text = double.Parse(parcelaProduto.TB017_ValorDesconto.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                            txtParcelaSubTotal.Text = double.Parse(parcelaProduto.TB017_ValorFinal.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");

                            if (parcelaProduto.TB017_Maior == 0 | parcelaProduto.TB017_Menor == 0 | parcelaProduto.TB017_Isento == 0)
                            {
                                mnuParcelaAbonarAdesao.Enabled = true;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal new void BackColor()
        {
            mskContratoTitularCPF.BackColor = Color.White;
            txtContratoTitularRG.BackColor = Color.White;
            txtContratoTitularRGOrgaoEmissor.BackColor = Color.White;
            txtContratoTitularNomeCompleto.BackColor = Color.White;
            dtContratoTitularDataNascimento.BackColor = Color.White;
            cmbContratoTitularSexo.BackColor = Color.White;
            cmbTitularPais.BackColor = Color.White;
            mskContratoTitularCEP.BackColor = Color.White;
            cmbContratoTitularEstado.BackColor = Color.White;
            cmbContratoTitularMunicipio.BackColor = Color.White;
            txtContratoTitularBairro.BackColor = Color.White;
            txtContratoTitularLogradouro.BackColor = Color.White;
            txtContratoTitularNumero.BackColor = Color.White;
            cmbDiaVencimento.BackColor = Color.White;
            ddtTB013_MaeDataNascimento.BackColor = Color.White;
            chkContratoTitularAceiteContrato.BackColor = Color.White;
            txtTB013_MaeNome.BackColor = Color.White;
            txtTB013_PaiNome.BackColor = Color.White;
            ddtTB013_PaiDataNascimento.BackColor = Color.White;
            txtContratoTitularNumero.BackColor = Color.White;
            txtContratoTitularComplemento.BackColor = Color.White;
        }
        private void mskContratoTitularCPF_Enter(object sender, EventArgs e)
        {
            BackColor();
            mskContratoTitularCPF.BackColor = _enter;
        }

        private void txtContratoTitularRG_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularRG.BackColor = _enter;
        }

        private void txtContratoTitularRGOrgaoEmissor_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularRGOrgaoEmissor.BackColor = _enter;
        }

        private void txtContratoTitularNomeCompleto_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularNomeCompleto.BackColor = _enter;
        }

        private void dtContratoTitularDataNascimento_Enter(object sender, EventArgs e)
        {
            BackColor();
            dtContratoTitularDataNascimento.BackColor = _enter;
        }

        private void cmbContratoTitularSexo_Enter(object sender, EventArgs e)
        {
            BackColor();
            cmbContratoTitularSexo.BackColor = _enter;
        }

        private void cmbTitularPais_Enter(object sender, EventArgs e)
        {
            BackColor();
            cmbTitularPais.BackColor = _enter;
        }

        private void mskContratoTitularCEP_Enter(object sender, EventArgs e)
        {
            BackColor();
            mskContratoTitularCEP.BackColor = _enter;
        }

        private void cmbContratoTitularEstado_Enter(object sender, EventArgs e)
        {
            BackColor();
            cmbContratoTitularEstado.BackColor = _enter;
        }

        private void cmbContratoTitularMunicipio_Enter(object sender, EventArgs e)
        {
            BackColor();
            cmbContratoTitularMunicipio.BackColor = _enter;
        }

        private void txtContratoTitularBairro_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularBairro.BackColor = _enter;
        }

        private void txtContratoTitularLogradouro_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularLogradouro.BackColor = _enter;
        }

        private void txtContratoTitularNumero_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularNumero.BackColor = _enter;
        }

        private void txtContratoTitularComplemento_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtContratoTitularComplemento.BackColor = _enter;
        }

        private void cmbDiaVencimento_Enter(object sender, EventArgs e)
        {
            BackColor();
            cmbDiaVencimento.BackColor = _enter;
        }

        private void chkContratoTitularAceiteContrato_Enter(object sender, EventArgs e)
        {
            BackColor();
            chkContratoTitularAceiteContrato.BackColor = _enter;
        }

        private void txtTB013_MaeNome_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtTB013_MaeNome.BackColor = _enter;
        }

        private void ddtTB013_MaeDataNascimento_Enter(object sender, EventArgs e)
        {
            BackColor();
            ddtTB013_MaeDataNascimento.BackColor = _enter;
        }

        private void txtTB013_PaiNome_Enter(object sender, EventArgs e)
        {
            BackColor();
            txtTB013_PaiNome.BackColor = _enter;
        }

        private void ddtTB013_PaiDataNascimento_Enter(object sender, EventArgs e)
        {
            BackColor();
            ddtTB013_PaiDataNascimento.BackColor = _enter;
        }

        private void pctAlterarDadosParcela_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusS.Text))) > 1)
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

                    if (cmbDiaVencimento.Text.Trim() == Empty)
                    {
                        MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Dia Vencimento"), @"Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cmbParcelaCiclo.Items.Add(lblCiclo.Text.Trim());
                    cmbParcelaCiclo.SelectedItem = lblCiclo.Text.Trim();

                    var parcelas =
                        new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                            Convert.ToInt64(lblCiclo.Text), -1);
                    if (parcelas.Count == 0)
                    {
                        mnuParcelaGerarParcelas.Enabled = true;
                    }
                    else
                    {
                        mnuParcelaGerarParcelas.Enabled = false;
                        ddtParcelas.DataSource = parcelas;
                        ddtParcelas.Refresh();
                    }
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

        private void txtParcelaDesconto_Leave(object sender, EventArgs e)
        {
            txtParcelaSubTotal.Text = double.Parse(Convert.ToString(Convert.ToDouble(txtParcelaValorUnitario.Text.Replace("R$", "")) - Convert.ToDouble(txtParcelaDesconto.Text.Replace("R$", "")), CultureInfo.CurrentCulture).Replace(".", ",")).ToString("C2");
            txtParcelaDesconto.Text = double.Parse(txtParcelaDesconto.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");
        }

        private void pctParcelaDescontoConfirmar_Click(object sender, EventArgs e)
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

                Debug.Assert(cmbParcelaCiclo.ComboBox != null, "cmbParcelaCiclo.ComboBox != null");

                var ciclo = new ParcelaNegocios().ListarCiclosAtivosContrato(Convert.ToInt64(lblContrato.Text));
                if (ciclo.Count > 0)
                {
                    for (int i = 0; i < ciclo.Count; i++)
                    {
                        cmbParcelaCiclo.Items.Add(Convert.ToInt64(ciclo[i].TB012_CicloContrato));
                    }
                    cmbParcelaCiclo.SelectedIndex = ciclo.Count - 1;
                }
                else
                {
                    cmbParcelaCiclo.Items.Add(Convert.ToInt64(lblCiclo.Text));
                    cmbParcelaCiclo.SelectedIndex = 0;
                }

                var parcelas =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
                if (parcelas.Count == 0)
                {
                    mnuParcelaGerarParcelas.Enabled = true;
                }
                else
                {
                    if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), parcelas[parcelas.Count - 1].TB016_StatusS))) == 3)
                    {
                        mnuParcelaGerarParcelas.Enabled = true;
                    }
                    else
                    {
                        mnuParcelaGerarParcelas.Enabled = false;
                    }

                    ddtParcelas.DataSource = parcelas;
                    ddtParcelas.Refresh();
                    SelecionarParcela(Convert.ToInt64(lblParcelaId.Text));
                }
                /**/
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnuContratoAnotacoes_Click(object sender, EventArgs e)
        {
            if (OpenForms.OfType<FrmContratoAnotacoes>().Any())
            {
                OpenForms.OfType<FrmContratoAnotacoes>().First().Focus();
            }
            else
            {
                var anotacoes = new FrmContratoAnotacoes(Convert.ToInt64(lblContrato.Text));
                anotacoes.Show();
            }
        }

        private void cmbParcelaCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(cmbParcelaCiclo.SelectedItem) > 0)
            {
                ListarParcelas(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(cmbParcelaCiclo.SelectedItem));
            }

        }

        private void cmbParcelaCiclo_TextChanged(object sender, EventArgs e)
        {

        }

        private void mnuDependenteIncluirCartao_Click(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0043, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (lblCarteirinha.Text.Replace("-", "").Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0047, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cbmStatusCartaoManualDependente.DataSource = null;
                cbmStatusCartaoManualDependente.Items.Clear();

                var cartaoStatus = new List<KeyValuePair<string, string>>();
                var status = Enum.GetValues(typeof(PessoaController.TB013_CarteirinhaStatusE));
                foreach (var statu in status)
                {
                    cartaoStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
                }

                cbmStatusCartaoManualDependente.DataSource = cartaoStatus;
                cbmStatusCartaoManualDependente.DisplayMember = "Key";
                cbmStatusCartaoManualDependente.ValueMember = "Value";
                mskCartaoManualDependente.Text = lblCarteirinha.Text.Substring(0, 10);
                cbmStatusCartaoManualDependente.SelectedValue = "1";
                gbxIncluirCartaoManualmenteDependente.Visible = true;
            }
        }

        private void btnCartaoManualDependenteFechar_Click(object sender, EventArgs e)
        {
            gbxIncluirCartaoManualmenteDependente.Visible = false;
        }

        private void btnSalvarCartaoManualDependente_Click(object sender, EventArgs e)
        {
            try
            {
                var cartao = new PessoaNegocios().Cartao(mskCartaoManualDependente.Text);
                if (cartao.TB013_id > 0)
                {
                    MessageBox.Show(MensagensDoSistema._0045.Replace("$NOME", cartao.TB013_NomeCompleto).Replace("$ID", cartao.TB013_id.ToString()), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var statuscartao = 1;
                    var dataLegado = Convert.ToDateTime("13/03/2017");

                    if (Convert.ToDateTime(dtContratoInicio.Value) < dataLegado)
                    {
                        statuscartao = 8;
                    }
                    else
                    {
                        switch (Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue))
                        {
                            case 9:
                                statuscartao = 2;
                                break;
                            case 10:
                                statuscartao = 2;
                                break;
                        }
                    }
                    if (!new PessoaNegocios().CartaoManual(Convert.ToInt64(lblDependenteId.Text),
                        mskCartaoManualDependente.Text, statuscartao, ParametrosInterface.objUsuarioLogado.TB011_Id,
                        Convert.ToInt64(lblContrato.Text))) return;
                    mskCartaoManualDependente.Text = "";
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gbxIncluirCartaoManualmenteDependente.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuContratoTitularcartaoProvisorio_Click(object sender, EventArgs e)
        {
            try
            {
                var cartaoProvisorio = new frmRptCarteiraProvisoria(Convert.ToInt64(lblContrato.Text));
                cartaoProvisorio.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuParcelaAbonarAdesao_Click(object sender, EventArgs e)
        {
            try
            {
                new ParcelaNegocios().AbonarAdesaoContrato(Convert.ToInt64(lblParcelaId.Text),
                    Convert.ToInt64(lblParcelaProdutoId.Text), Convert.ToInt64(lblContrato.Text),
                    Convert.ToDouble(txtParcelaSubTotal.Text.Replace("R$", "")),
                    ParametrosInterface.objUsuarioLogado.TB011_Id);

                ddtParcelas.DataSource =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
                ddtParcelas.Refresh();
                SelecionarParcela(Convert.ToInt64(lblParcelaId.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFiltroAssociado_Leave(object sender, EventArgs e)
        {
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

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblDependenteId.Text = "";
            mskContratoDependenteCPF.Text = "";
            txtDependenteNomeCompleto.Text = "";

            DTContratoDependenteContatos.DataSource = null;
            DTContratoDependenteContatos.Refresh();
        }

        private void solicitarBoletoSICOOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (var y = 0; y < ddtParcelas.RowCount; y++)
                {
                    // if (!Convert.ToBoolean(ddtParcelas.Rows[y].Cells["Selecionar"].Value)) continue;
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
                        new ParcelaNegocios().EmitirBoletoAvulsto(parcelac,
                            ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(parcela));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                ddtParcelas.DataSource =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
                ddtParcelas.Refresh();
                MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void mnuCancelamentoTermoFechar_Click(object sender, EventArgs e)
        {
            txtDescricao.Text = "";
            tabPrincipal.TabPages.Add(tbContrato);
            tabPrincipal.TabPages.Remove(tbContratoCancelamentoTermo);
            FiltrarContrato(Convert.ToInt64(lblContrato.Text));
        }

        private void mnuCancelarTermo_Click(object sender, EventArgs e)
        {
            try
            {
                tabPrincipal.TabPages.Add(tbContratoCancelamentoTermo);

                dTRPT0014TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                dTRPT0014TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0014,
                    Convert.ToInt64(lblContrato.Text));

                var setup = rpwTermoCancelamento.GetPageSettings();
                setup.Margins = new Margins(1, 1, 1, 1);
                rpwTermoCancelamento.SetPageSettings(setup);

                rpwTermoCancelamento.LocalReport.SetParameters(
                        new Microsoft.Reporting.WinForms.ReportParameter("Path",
                            @"File://C:\Temp\" + lblContrato.Text + ".jpg"));

                rpwTermoCancelamento.RefreshReport();

                tabPrincipal.TabPages.Remove(tbCancelarPlano);
            }
            catch (Exception ex)
            {
                tabPrincipal.TabPages.Add(tbCancelarPlano);
                tabPrincipal.TabPages.Remove(tbContratoCancelamentoTermo);
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCancelamentoAssinatura_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbContratoCancelamentoTermo);
            tabPrincipal.TabPages.Add(tbCancelarPlano);
            var assinatura = new frmAssinatura(Convert.ToInt64(lblContrato.Text));
            assinatura.Show();
        }

        private void salvarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            var documento =
                new ContratoDocController
                {
                    TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB029_TipoS = "4",
                    TB012_VSContrato = new ContratoNegocios().ContratoVSAtual(Convert.ToInt64(lblContrato.Text)),
                    TB012_id = Convert.ToInt64(lblContrato.Text),
                    TB029_DocImpressao = rpwTermoCancelamento.LocalReport.Render("Pdf", null, out mimeType,
                        out encoding, out extension, out streamids, out warnings)
                };

            var doc = new ContratoDocNegocios().DocImpressaoInserir(documento);
            if (doc.TB029_Id > 0)
            {
                MessageBox.Show(MensagensDoSistema._0017, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pctAlterarValidadeContrato_Click(object sender, EventArgs e)
        {

            if (_privilegioOn == 0)
            {
                _privilegioConfirmar =20;
                //pctAlterarValidadeContrato.Image = Properties.Resources.Confirmar;
                mskCPF.Text = ParametrosInterface.objUsuarioLogado.TB011_CPF;
                txtSenha.Text = ParametrosInterface.objUsuarioLogado.TB011_Senha;
                gtpCredenciaisPrivilegio.Location = new Point(319, 82);
                gtpCredenciaisPrivilegio.Visible = true;
                pctAlterarValidadeContrato.Enabled = false;
            }
            else
            {
                pctAlterarValidadeContrato.Image = Properties.Resources.Cadeado;
                //Trocar data de validade do contrato
                if (new ContratoNegocios().ContratoAlterarInicioFim(Convert.ToInt64(lblContrato.Text), dtContratoInicio.Value, dtContratoFim.Value, ParametrosInterface.objUsuarioLogado.TB011_Id))
                {
                    //SelecionarParcela(Convert.ToInt64(lblContrato.Text));
                    //FiltrarContrato(Convert.ToInt64(lblContrato.Text));
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            _privilegioOn = 0;
            gtpCredenciaisPrivilegio.Visible = false;
            pctAlterarValidadeContrato.Enabled = true;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            if(_privilegioConfirmar == 20)
            {
                if (new UsuarioAPPNegocios().VerificaPrivilarioAcaoPontual(_privilegioConfirmar, mskCPF.Text, txtSenha.Text) > 0)
                {
                    pctAlterarValidadeContrato.Image = Properties.Resources.Confirmar;
                    _privilegioOn = 1;
                    dtContratoInicio.Enabled = true;
                    dtContratoFim.Enabled = true;
                    gtpCredenciaisPrivilegio.Visible = false;
                    mskCPF.Text = "";
                    txtSenha.Text = "";
                    pctAlterarValidadeContrato.Enabled = true;
                }
            }

            else
            {
                if (_privilegioConfirmar == 10)
                {
                    if (new UsuarioAPPNegocios().VerificaPrivilarioAcaoPontual(_privilegioConfirmar, mskCPF.Text, txtSenha.Text) > 0)
                    {
                        pcbMudarLocalCadastro.Image         = Properties.Resources.Confirmar;
                        _privilegioOn                       = 1;
                        cmbContratoPontosDeVenda.Enabled            = true;
             
                        gtpCredenciaisPrivilegio.Visible    = false;
                        mskCPF.Text                         = "";
                        txtSenha.Text                       = "";
                        pcbMudarLocalCadastro.Enabled       = true;
                    }
                }
            }


            _privilegioConfirmar = 0;
        }

        private void ajudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(ParametrosInterface.Intranet + "/#diagram/dd0de8bb-a277-4258-9a2b-1590d55060d8");

            Process.Start(sInfo);
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            timer1.Enabled = true;
        }

        //private void Gerar()
        //{
        //    string Conect = "Data Source=FGE\\FGE;Initial Catalog=DBClubeConteza_Local;User ID =sa;Password=root;Persist Security Info=True";
        //    //LogNegocios Log_N = new LogNegocios();
        //    //LogController Log_C = new LogController();
        //    ///*Gerar Numero da Sorte*/
        //    //Contrato.TB012_NumeroDaSorte = new Util().numeroDaSorteGerar();

        //    List<UnidadeController> retornoList = new List<UnidadeController>();
        //    try
        //    {
        //        var con = new SqlConnection(Conect);
        //        var sSql = new StringBuilder();

        //        sSql.Append("SELECT top(1)  TB012_id, TB012_NumeroDaSorte FROM dbo.GeracaoNumero WHERE(TB012_NumeroDaSorte IS NULL) ");


        //        SqlCommand command = new SqlCommand(sSql.ToString(), con);

        //        con.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            UnidadeController obj = new UnidadeController();



        //            obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
        //            retornoList.Add(obj);
        //        }

        //        con.Close();

        //        /**/

        //        foreach (UnidadeController Cartao in retornoList)
        //        {
        //            bool Numeroexistente = true;
        //            long numerodasorte = 0;

        //            while (Numeroexistente==true)
        //            {
        //                numerodasorte = new Util().numeroDaSorteGerar();

        //                Numeroexistente = new ContratoNegocios().NumeroDaSorte(numerodasorte);

        //            }




        //            var sSqlInsert = new StringBuilder();

        //            sSqlInsert.Append(" UPDATE[dbo].[GeracaoNumero] SET ");
        //            sSqlInsert.Append(" TB012_NumeroDaSorte = ");
        //            sSqlInsert.Append(numerodasorte);
        //            sSqlInsert.Append(" where TB012_id = ");
        //            sSqlInsert.Append(Cartao.TB012_id);

        //            using (SqlConnection con2 = new SqlConnection(Conect))
        //            {
        //                con2.Open();
        //                SqlCommand myCommand = new SqlCommand(sSqlInsert.ToString(), con2);
        //                myCommand.ExecuteScalar();
        //                con2.Close();
        //                numerodasorte = 0;
        //            }

        //        }

        //        timer1.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //Gerar();
            //lblTotalRegistrosRetornados.Text = DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss");
        }

        private void senhaPortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSenhaPortal.Text = "";
            txtSenhaPortalConfirmar.Text = "";


            if (lblContrato.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0043, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                gblSenhaPortal.Visible = true;
            }
        }

        private void btnSenhaPortalFechar_Click(object sender, EventArgs e)
        {
            txtSenhaPortal.Text = "";
            txtSenhaPortalConfirmar.Text = "";
            gblSenhaPortal.Visible = false;
        }

        private void btnSenhaPortalConfirmar_Click(object sender, EventArgs e)
        {
            if (txtSenhaPortal.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", " Senha "), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenhaPortal.Focus();
                return;
            }

            if (txtSenhaPortalConfirmar.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", " Confirmar Senha "), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenhaPortalConfirmar.Focus();
                return;
            }


            if (txtSenhaPortal.Text.Trim() != txtSenhaPortalConfirmar.Text.Trim())
            {
                MessageBox.Show(MensagensDoSistema._0090, @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenhaPortal.Focus();
                return;
            }

            try
            {
                PessoaController pessoa = new PessoaController();

                pessoa.TB013_id = Convert.ToInt64(lblContratoTitularID.Text);
                pessoa.TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                pessoa.TB033_Senha = txtSenhaPortal.Text.Trim();
                if (new PessoaNegocios().ContezinoSenha(pessoa))
                {
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                    gblSenhaPortal.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnuDocumentosAnexarContrato_Click(object sender, EventArgs e)
        {
            gprAnexarTermo.Visible = true;
            label135.Text = "1";
            label134.Text = "";
        }

        private void btnTermoFechar_Click(object sender, EventArgs e)
        {
            gprAnexarTermo.Visible = false;
            label135.Text = "4";
            label134.Text = "";
            axAcroPDF2.src = "";
            axAcroPDF2.Visible = false;
        }
        private void btnTermoLocalizar_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Arquivo PDF|*.pdf";
            OpenFileDialog1.Title = "Anexar PDF";
            OpenFileDialog1.ShowDialog();

            if (OpenFileDialog1.FileName != "")
            {
                axAcroPDF2.src = OpenFileDialog1.FileName;
                axAcroPDF2.Visible = true;
                label134.Text = OpenFileDialog1.FileName;
            }

        }

        private void btnTermoAnexar_Click(object sender, EventArgs e)
        {

            var sample = new FileInfo(label134.Text.TrimEnd());
            var fileBinary = new byte[sample.Length];
            using (var fileStream = sample.OpenRead())
            {
                fileStream.Read(fileBinary, 0, (int)sample.Length);

                ContratoDocController documento =
                    new ContratoDocController
                    {
                        TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                        TB029_TipoS = label135.Text,
                        TB012_VSContrato = 1,
                        TB012_id = Convert.ToInt64(lblContrato.Text),
                        TB029_DocImpressao = fileBinary
                    };

                ContratoDocNegocios docN = new ContratoDocNegocios();
                ContratoDocController doc = docN.DocImpressaoInserir(documento);

                if (doc.TB029_Id > 0)
                {
                    MessageBox.Show(MensagensDoSistema._0017, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gprAnexarTermo.Visible = false;
                    label134.Text = "";
                    axAcroPDF2.src = "";
                    axAcroPDF2.Visible = false;
                    CarregarDocumentosContrato();
                }
            }
        }

        private void mnuReativarContratoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbReativarContrato);
            tabPrincipal.TabPages.Add(tbContrato);
            
        }

        private void mnuReativarContratoAssinatura_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbReativarContrato);
            tabPrincipal.TabPages.Add(tbContrato);

            var assinatura = new frmAssinatura(Convert.ToInt64(lblContrato.Text));
            assinatura.Show();
        }

        private void mnuContratoReativar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Funcionalidade não liberada", @"Aguardando Liberação TI", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //return;

            //long pendencia = new ParcelaNegocios().validarPendenciaFinanceira(Convert.ToInt64(lblContrato.Text));
            if(new ParcelaNegocios().validarPendenciaFinanceira(Convert.ToInt64(lblContrato.Text))>0)
            {
                MessageBox.Show(MensagensDoSistema._0100, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            tabPrincipal.TabPages.Add(tbReativarContrato);
            try
            {
                dTRPT0022TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                dTRPT0022TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0022, Convert.ToInt64(lblContrato.Text));
                rptContratoReativar.LocalReport.SetParameters(
                new Microsoft.Reporting.WinForms.ReportParameter("Path",
                @"File://C:\Temp\" + lblContrato.Text + ".jpg"));

                rptContratoReativar.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tabPrincipal.TabPages.Remove(tbContrato);
        }


        private void mnuReativarDependenteFechar_Click(object sender, EventArgs e)
        {
           
            tabPrincipal.TabPages.Add(tbContrato);

            FiltrarContrato(Convert.ToInt64(lblContrato.Text));
            tabPrincipal.TabPages.Remove(tbDependentesReativar);
        }

        private void mnuContratoDependentesReativar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Add(tbDependentesReativar);
            carregarDependentesParaReativacao();
        }

        private void carregarDependentesParaReativacao()
        {
            ddgDependentesReativar.AutoGenerateColumns   = false;
            ddgDependentesReativar.DataSource            = null;
            ddgDependentesReativar.DataSource            = new PessoaNegocios().dependentesNaoAtivos(Convert.ToInt64(lblContrato.Text));
            ddgDependentesReativar.Refresh();
        }

        private void ddgDependentesReativar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
       
                    PessoaNegocios dependenteN = new PessoaNegocios();
                    PessoaController dependente = dependenteN.DependenteFamiliarSelect(Convert.ToInt64(ddgDependentesReativar.Rows[e.RowIndex].Cells["ReativarDependenteTB013_id"].Value));

                    label142.Text = dependente.TB013_id.ToString();
                    textBox6.Text = dependente.TB013_NomeCompleto;
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReativarDependenteAtivar_Click(object sender, EventArgs e)
        {
            try
            {
                PessoaNegocios pessoaN = new PessoaNegocios();

                if (pessoaN.DependenteAlterarStatus(Convert.ToInt64(label142.Text), 1, Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id))
                {
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    carregarDependentesParaReativacao();
             
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void finalizarProcessoReativaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(new ContratoNegocios().finalizarProcessoEdicaoContrato(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id))
                {

                    PessoaNegocios pessoaN = new PessoaNegocios();

                    if (pessoaN.DependenteAlterarStatus(Convert.ToInt64(lblContratoTitularID.Text), 1, Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id))
                    {
                        tabPrincipal.TabPages.Add(tbContrato);

                        FiltrarContrato(Convert.ToInt64(lblContrato.Text));
                        tabPrincipal.TabPages.Remove(tbDependentesReativar);

                    }

                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReativarContratoReativar_Click(object sender, EventArgs e)
        {
            txtReativarContratoNovoCiclo.Text = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            grpReativarContrato.Location = new Point(550, 90);
            grpReativarContrato.Visible = true;
        }

        private void btnReativarContratoFechar_Click(object sender, EventArgs e)
        {
            txtReativarContratoNovoCiclo.Text = "";
            grpReativarContrato.Visible = false;
        }

        private void btnReativarContratoConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                var contrato = new ContratosController();
            
                contrato.TB013_id               = Convert.ToInt64(lblContratoTitularID.Text);           
                contrato.TB012_Id = Convert.ToInt64(lblContrato.Text);
                contrato.TB012_CicloContrato    = txtReativarContratoNovoCiclo.Text;
                contrato.TB012_AlteradoPor      = ParametrosInterface.objUsuarioLogado.TB011_Id;
                contrato.TB012_AlteradoEm       = DateTime.Now;

                var documento = new ContratoDocController
                {
                    TB029_DocImpressaoPor = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB029_TipoS = "5",
                    TB012_VSContrato = new ContratoNegocios().ContratoVSAtual(Convert.ToInt64(lblContrato.Text))
                };

                documento.TB012_VSContrato++;
                contrato.TB012_VSContrato = documento.TB012_VSContrato;
        
                if (new ContratoNegocios().familiarReativarContrato(contrato))
                {
                    /*Salvar termo em documentos*/
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                  
                    documento.TB012_id              = Convert.ToInt64(lblContrato.Text);
                    documento.TB029_DocImpressao    = rptContratoReativar.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                    var doc = new ContratoDocNegocios().DocImpressaoInserir(documento);

                    if (doc.TB029_Id > 0)
                    {
                         MessageBox.Show(MensagensDoSistema._0096, @"Aviso", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    txtReativarContratoNovoCiclo.Text = "";
                    grpReativarContrato.Visible = false;
                    mnuContratoReativar.Visible = false;
                }
                else
                {
                    MessageBox.Show("Erro Interno", @"Erro ao executar operação", MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnParcelaProdutoIncluirFechar_Click(object sender, EventArgs e)
        {
            txtParcelaProdutoIncluirIdParcela.Text      = "";
            txtParcelaProdutoIncluirVencimento.Text     = "";
            txtParcelaProdutoIncluirValorOriginal.Text  = "";


            grbParcelaProdutoIncluir.Location = new Point(130, 100);
            grbParcelaProdutoIncluir.Visible = false;
        }

        private void mnuParcelaProdutoIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblParcelaId.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0097, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);      
                    return;
                }

                if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(
                typeof(ParcelaController.TB016_StatusE), lblParcelaStatusS.Text))) > 1)
                {
                    MessageBox.Show(MensagensDoSistema._0098, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtParcelaProdutoIncluirIdParcela.Text              = lblParcelaId.Text.Trim();
                txtParcelaProdutoIncluirVencimento.Text             = ddtParcelaVencimento.Value.ToString().Trim();
                txtParcelaProdutoIncluirValorOriginal.Text          = lblParcelaValorTotal.Text.Trim();

                cmbParcelaProdutoIncluirLista.DataSource            = null;
                cmbParcelaProdutoIncluirLista.Items.Clear();

                cmbParcelaProdutoIncluirLista.DataSource            = new ProdutoNegocios().produtosContezinos(); 
                cmbParcelaProdutoIncluirLista.DisplayMember         = "TB014_Produto";
                cmbParcelaProdutoIncluirLista.ValueMember           = "TB014_id";

                cmbParcelaProdutoIncluirQuantidade.SelectedIndex    = 0;
                cmbParcelaProdutoIncluirLista.SelectedIndex         = 0;

                grbParcelaProdutoIncluir.Location = new Point(130, 100);
                grbParcelaProdutoIncluir.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbParcelaProdutoIncluirLista_SelectedValueChanged(object sender, EventArgs e)
        {
           if (Convert.ToInt64(cmbParcelaProdutoIncluirLista.SelectedIndex) > 0)
            {
                try
                {
                    var produto                                 = new ProdutoNegocios().ProdutoID(Convert.ToInt64(cmbParcelaProdutoIncluirLista.SelectedValue));
                    txtParcelaProdutoIncluirValorUnitario.Text  = Format("{0:C2}", Convert.ToDouble(produto.TB014_ValorUnitario.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                    lblParcelaProdutoIncluirTipo.Text           = produto.TB014_TipoS;
                    calcularProdutoIncluido();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void calcularProdutoIncluido()
        {
            try
            {
                if (Convert.ToInt64(cmbParcelaProdutoIncluirLista.SelectedIndex) > 0)
                {
                    double vl = Convert.ToDouble(txtParcelaProdutoIncluirValorUnitario.Text.Replace("R$", "")) * Convert.ToDouble(cmbParcelaProdutoIncluirQuantidade.Text);
                    txtParcelaProdutoIncluirValorProduto.Text = Format("{0:C2}", Convert.ToDouble(vl.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                }
                else
                {
                    txtParcelaProdutoIncluirValorProduto.Text = "";
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbParcelaProdutoIncluirQuantidade_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                calcularProdutoIncluido();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnParcelaProdutoIncluirConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbParcelaProdutoIncluirLista.SelectedIndex==0)
            {
                MessageBox.Show(MensagensDoSistema._0099, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (MessageBox.Show(@"Deseja incluir este produto?", @"Produto", MessageBoxButtons.YesNo) ==
                            DialogResult.No)
            {
                return;
            }
            try
            {
                var produto = new ParcelaProdutosController
                {
                    TB016_id = Convert.ToInt64(txtParcelaProdutoIncluirIdParcela.Text),
                    TB017_ValorUnitario     = Convert.ToDouble(txtParcelaProdutoIncluirValorUnitario.Text.Replace("R$", "")),
                    TB017_ValorDesconto     = 0,
                    TB017_ValorFinal        = Convert.ToDouble(txtParcelaProdutoIncluirValorProduto.Text.Replace("R$", "")),
                    TB017_Item              = cmbParcelaProdutoIncluirLista.Text,
                    TB017_TipoS             = lblParcelaProdutoIncluirTipo.Text

                };

                var parcela = new ParcelaController
                {
                    TB016_Valor         = produto.TB017_ValorFinal,
                    TB016_AlteradoEm    = DateTime.Now,
                    TB016_AlteradoPor   = ParametrosInterface.objUsuarioLogado.TB011_Id,
                    TB016_id            = produto.TB016_id
                };


                var parcelas =
                    new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                        Convert.ToInt64(lblCiclo.Text), -1);
            
                if(new ParcelaNegocios().produtoIncluirManualmente(produto, parcela,Convert.ToInt64(cmbParcelaProdutoIncluirLista.SelectedValue)))
                {

                    var ciclo = new ParcelaNegocios().ListarCiclosAtivosContrato(Convert.ToInt64(lblContrato.Text));
                    if (ciclo.Count > 0)
                    {
                        for (int i = 0; i < ciclo.Count; i++)
                        {
                            cmbParcelaCiclo.Items.Add(Convert.ToInt64(ciclo[i].TB012_CicloContrato));
                        }
                        cmbParcelaCiclo.SelectedIndex = ciclo.Count - 1;
                    }
                    else
                    {
                        cmbParcelaCiclo.Items.Add(Convert.ToInt64(lblCiclo.Text));
                        cmbParcelaCiclo.SelectedIndex = 0;
                    }


                    ddtParcelas.DataSource = parcelas;
                    ddtParcelas.Refresh();
                    SelecionarParcela(Convert.ToInt64(lblParcelaId.Text));
                    
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                    txtParcelaProdutoIncluirIdParcela.Text = "";
                    txtParcelaProdutoIncluirVencimento.Text = "";
                    txtParcelaProdutoIncluirValorOriginal.Text = "";
                    
                    grbParcelaProdutoIncluir.Location = new Point(130, 100);
                    grbParcelaProdutoIncluir.Visible = false;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuLista_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ajudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file://" + System.Environment.CurrentDirectory + "\\Gestor.chm", HelpNavigator.Topic, "Familiar.htm");
        }

        private void ajudaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file://" + System.Environment.CurrentDirectory + "\\Gestor.chm", HelpNavigator.Topic, "Reativar Contrato.htm");
        }

        private void cmbContratoTitularEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            EstadoController filtro = new EstadoController();
            //TB005_Id = Convert.ToInt16(ParametrosInterface.objUsuarioLogado.Estado.TB005_Id)
            filtro.TB005_Id = Convert.ToInt64(cmbContratoTitularEstado.SelectedValue);
            PopularMunicipiosTitular(filtro);
        }

        private void mnuDocumentosAnexarCancelamento_Click(object sender, EventArgs e)
        {
            gprAnexarTermo.Visible = true;
            label135.Text = "4";
            label134.Text = "";
        }

        private void pcbMudarLocalCadastro_Click(object sender, EventArgs e)
        {
            if (_privilegioOn == 0)
            {
                _privilegioConfirmar = 10;
             
                mskCPF.Text = ParametrosInterface.objUsuarioLogado.TB011_CPF;
                txtSenha.Text = ParametrosInterface.objUsuarioLogado.TB011_Senha;
                gtpCredenciaisPrivilegio.Location = new Point(319, 82);
                gtpCredenciaisPrivilegio.Visible = true;
                pcbMudarLocalCadastro.Enabled = false;
            }
            else
            {
                pcbMudarLocalCadastro.Image = Properties.Resources.Cadeado;
                pcbMudarLocalCadastro.Enabled = false;


               if (new ContratoNegocios().ContratoAlterarLocalCadastro(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue), ParametrosInterface.objUsuarioLogado.TB011_Id))
                {
                //    //SelecionarParcela(Convert.ToInt64(lblContrato.Text));
                //    //FiltrarContrato(Convert.ToInt64(lblContrato.Text));
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

        }
    }
}

