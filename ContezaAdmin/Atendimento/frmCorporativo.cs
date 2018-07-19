using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace ContezaAdmin.Atendimento
{
    public partial class frmCorporativo : Form
    {
        Util Validacoes = new Util();
        Int64 UsuarioPermissao;
        int Campo;
        public frmCorporativo()
        {
            InitializeComponent();
        }
        private void frmCorporativo_Load(object sender, EventArgs e)
        {

            this.StartPosition = FormStartPosition.CenterScreen;
            tabPrincipal.TabPages.Remove(tbContratoPrincipal);
            tabPrincipal.TabPages.Remove(tbFilial);
            tabPrincipal.TabPages.Remove(tbColaborador);
            tabPrincipal.TabPages.Remove(tbBeneficiados);
            tabPrincipal.TabPages.Remove(tbParcelas);




            CarregarSexo();
            PontosDeVenda();

            StatusDeContrato();
            StatusDependente();
            CarregarPaises();
            PopularTipoContatos();

            PaisController filtro = new PaisController();
            filtro.TB003_id = 1058;
            PopularEstados(filtro);

            string vQuery = "";
            CarregarContratos(vQuery);
            //this.rpvBeneficiados.RefreshReport();
        }
        private void pcbFiltrarLista_Click(object sender, EventArgs e)
        {
            try
            {
                string vQuery = "";

                if (cmbFiltroAssociado.Text.TrimEnd() == "Contrato")
                {
                    vQuery = " AND dbo.TB012_Contratos.TB012_id =  " + txtFiltroAssociado.Text.Trim(); ;
                }
                else
                {
                    if (cmbFiltroAssociado.Text.TrimEnd() == "Nome Fantasia")
                    {
                        vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" + txtFiltroAssociado.Text.TrimEnd() + "%'";
                    }
                }
                CarregarContratos(vQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarContratos(string Query)
        {
            try
            {
                dgwContratos.AutoGenerateColumns = false;
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(Query);

                ContratoNegocios Contratos_N = new ContratoNegocios();
                dgwContratos.DataSource = Contratos_N.ContratosCorporativoSelect(sSQL.ToString()); ;
                dgwContratos.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarSexo()
        {
            cmbColaboradorSexo.DataSource = null;
            cmbColaboradorSexo.Items.Clear();

            cmbDependenteSexo.DataSource = null;
            cmbDependenteSexo.Items.Clear();

            cmbContratoPrincipalTitularSexo.DataSource = null;
            cmbContratoPrincipalTitularSexo.Items.Clear();

            List<KeyValuePair<string, string>> lstSexo = new List<KeyValuePair<string, string>>();
            Array SexoS = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE Sexo in SexoS)
            {
                lstSexo.Add(new KeyValuePair<string, string>(Sexo.ToString(), ((int)Sexo).ToString()));
            }

            cmbColaboradorSexo.DataSource = lstSexo;
            cmbColaboradorSexo.DisplayMember = "Key";
            cmbColaboradorSexo.ValueMember = "Value";



            List<KeyValuePair<string, string>> lstSexoDependente = new List<KeyValuePair<string, string>>();
            Array SexoSDependente = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE SexoDependente in SexoSDependente)
            {
                lstSexoDependente.Add(new KeyValuePair<string, string>(SexoDependente.ToString(), ((int)SexoDependente).ToString()));
            }
            cmbDependenteSexo.DataSource = lstSexoDependente;
            cmbDependenteSexo.DisplayMember = "Key";
            cmbDependenteSexo.ValueMember = "Value";


            List<KeyValuePair<string, string>> lstSexoTitular = new List<KeyValuePair<string, string>>();
            Array SexoSTitular = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE SexoTitular in SexoSTitular)
            {
                lstSexoTitular.Add(new KeyValuePair<string, string>(SexoTitular.ToString(), ((int)SexoTitular).ToString()));
            }
            cmbContratoPrincipalTitularSexo.DataSource = lstSexoTitular;
            cmbContratoPrincipalTitularSexo.DisplayMember = "Key";
            cmbContratoPrincipalTitularSexo.ValueMember = "Value";
        }
        private void PontosDeVenda()
        {
            try
            {
                PontoDeVendaNegocios PontoDeVendaN = new PontoDeVendaNegocios();

                cmbContratoPrincipalPontoVenda.DataSource = null;
                cmbContratoPrincipalPontoVenda.Items.Clear();

                DataTable PontoDeVenda = PontoDeVendaN.PontosDeVendaLiberadosParaUsuario(ParametrosInterface.objUsuarioLogado).Tables[0];

                cmbContratoPrincipalPontoVenda.DataSource = PontoDeVenda;
                cmbContratoPrincipalPontoVenda.DisplayMember = "TB002_Ponto";
                cmbContratoPrincipalPontoVenda.ValueMember = "TB002_id";

                cmbFilialPontoVenda.DataSource = null;
                cmbFilialPontoVenda.Items.Clear();

                cmbFilialPontoVenda.DataSource = PontoDeVenda;
                cmbFilialPontoVenda.DisplayMember = "TB002_Ponto";
                cmbFilialPontoVenda.ValueMember = "TB002_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StatusDeContrato()
        {
            cmbContratoPrincipalStatusContrato.DataSource = null;
            cmbContratoPrincipalStatusContrato.Items.Clear();

            cmbFilialStatusContrato.DataSource = null;
            cmbFilialStatusContrato.Items.Clear();

            cmbColaboradorStatus.DataSource = null;
            cmbColaboradorStatus.Items.Clear();

            List<KeyValuePair<string, string>> ContratoStatus = new List<KeyValuePair<string, string>>();
            Array Status = Enum.GetValues(typeof(ContratosController.TB012_StatusE));
            foreach (ContratosController.TB012_StatusE Statu in Status)
            {
                ContratoStatus.Add(new KeyValuePair<string, string>(Statu.ToString(), ((int)Statu).ToString()));
            }

            cmbContratoPrincipalStatusContrato.DataSource = ContratoStatus;
            cmbContratoPrincipalStatusContrato.DisplayMember = "Key";
            cmbContratoPrincipalStatusContrato.ValueMember = "Value";


            cmbFilialStatusContrato.DataSource = ContratoStatus;
            cmbFilialStatusContrato.DisplayMember = "Key";
            cmbFilialStatusContrato.ValueMember = "Value";

            cmbColaboradorStatus.DataSource = ContratoStatus;
            cmbColaboradorStatus.DisplayMember = "Key";
            cmbColaboradorStatus.ValueMember = "Value";


        }
        private void StatusDependente()
        {
            cmbDependenteStatus.DataSource = null;
            cmbDependenteStatus.Items.Clear();

            List<KeyValuePair<string, string>> DependenteStatus = new List<KeyValuePair<string, string>>();
            Array Status = Enum.GetValues(typeof(PessoaController.TB013_StatusE));
            foreach (PessoaController.TB013_StatusE Statu in Status)
            {
                DependenteStatus.Add(new KeyValuePair<string, string>(Statu.ToString(), ((int)Statu).ToString()));
            }

            cmbDependenteStatus.DataSource = DependenteStatus;
            cmbDependenteStatus.DisplayMember = "Key";
            cmbDependenteStatus.ValueMember = "Value";
        }
        private void CarregarPaises()
        {
            try
            {
                EnderecoNegocios EnderecoN = new EnderecoNegocios();

                cmbContratoPrincipalPais.DataSource = null;
                cmbContratoPrincipalPais.Items.Clear();
                cmbFilialPais.DataSource = null;
                cmbFilialPais.Items.Clear();
                cmbContratoPrincipalTitularPais.DataSource = null;
                cmbContratoPrincipalTitularPais.Items.Clear();
                cmbFilialTitularPais.DataSource = null;
                cmbFilialTitularPais.Items.Clear();
                cmbColaboradorPais.DataSource = null;
                cmbColaboradorPais.Items.Clear();
                DataTable PaisController = EnderecoN.PaisController().Tables[0];
                cmbContratoPrincipalPais.DataSource = PaisController;
                cmbContratoPrincipalPais.DisplayMember = "TB003_Pais";
                cmbContratoPrincipalPais.ValueMember = "TB003_id";
                cmbFilialPais.DataSource = PaisController;
                cmbFilialPais.DisplayMember = "TB003_Pais";
                cmbFilialPais.ValueMember = "TB003_id";
                cmbContratoPrincipalTitularPais.DataSource = PaisController;
                cmbContratoPrincipalTitularPais.DisplayMember = "TB003_Pais";
                cmbContratoPrincipalTitularPais.ValueMember = "TB003_id";
                cmbFilialTitularPais.DataSource = PaisController;
                cmbFilialTitularPais.DisplayMember = "TB003_Pais";
                cmbFilialTitularPais.ValueMember = "TB003_id";
                cmbColaboradorPais.DataSource = PaisController;
                cmbColaboradorPais.DisplayMember = "TB003_Pais";
                cmbColaboradorPais.ValueMember = "TB003_id";



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularTipoContatos()
        {
            cmbContratoPrincipalContatoTipo.DataSource = null;
            cmbContratoPrincipalContatoTipo.Items.Clear();

            cmbContratoFilialContatoTipo.DataSource = null;
            cmbContratoFilialContatoTipo.Items.Clear();

            cmbContratoColaboradorContatoTipo.DataSource = null;
            cmbContratoColaboradorContatoTipo.Items.Clear();

            List<KeyValuePair<string, string>> ContatoTipo_L = new List<KeyValuePair<string, string>>();
            Array Contatos = Enum.GetValues(typeof(ContatoController.TB009_TipoE));
            foreach (ContatoController.TB009_TipoE Contato in Contatos)
            {
                ContatoTipo_L.Add(new KeyValuePair<string, string>(Contato.ToString(), ((int)Contato).ToString()));
            }

            cmbContratoPrincipalContatoTipo.DataSource = ContatoTipo_L;
            cmbContratoPrincipalContatoTipo.DisplayMember = "Key";
            cmbContratoPrincipalContatoTipo.ValueMember = "Value";
            cmbContratoFilialContatoTipo.DataSource = ContatoTipo_L;
            cmbContratoFilialContatoTipo.DisplayMember = "Key";
            cmbContratoFilialContatoTipo.ValueMember = "Value";
            cmbContratoColaboradorContatoTipo.DataSource = ContatoTipo_L;
            cmbContratoColaboradorContatoTipo.DisplayMember = "Key";
            cmbContratoColaboradorContatoTipo.ValueMember = "Value";
        }
        private void PopularEstados(PaisController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbContratoPrincipalEstado.DataSource = null;
            cmbContratoPrincipalEstado.Items.Clear();

            cmbContratoPrincipalTitularEstado.DataSource = null;
            cmbContratoPrincipalTitularEstado.Items.Clear();

            cmbColaboradorEstado.DataSource = null;
            cmbColaboradorEstado.Items.Clear();


            try
            {
                cmbContratoPrincipalEstado.DataSource = Endereco_N.EstadosController(filtro).Tables[0];
                cmbContratoPrincipalEstado.DisplayMember = "TB005_Estado";
                cmbContratoPrincipalEstado.ValueMember = "TB005_Id";
                cmbContratoPrincipalTitularEstado.DataSource = cmbContratoPrincipalEstado.DataSource;
                cmbContratoPrincipalTitularEstado.DisplayMember = "TB005_Estado";
                cmbContratoPrincipalTitularEstado.ValueMember = "TB005_Id";
                cmbColaboradorEstado.DataSource = cmbContratoPrincipalTitularEstado.DataSource;
                cmbColaboradorEstado.DisplayMember = "TB005_Estado";
                cmbColaboradorEstado.ValueMember = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void mnuListaNovo_Click(object sender, EventArgs e)
        {
            UsuarioAPPNegocios Usuario_N = new UsuarioAPPNegocios();
            if (Usuario_N.VS() != Application.ProductVersion)
            {
                MessageBox.Show(string.Format(MensagensDoSistema._0051, Application.ProductVersion, ParametrosInterface.objUsuarioLogado.VS).ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mskContratoPrincipalDoc.Enabled = true;
            mskContratoPrincipalTitularCNPJ.Enabled = true;

            tabPrincipal.TabPages.Add(tbContratoPrincipal);
            cmbContratoPrincipalPontoVenda.Enabled = true;
            DateTime Hoje = DateTime.Now;
            dtpContratoPrincipalInicio.Enabled = true;
            dtpContratoPrincipalInicio.Value = Hoje;
            dtpContratoPrincipalFim.Value = Hoje.AddMonths(12);

            cmbContratoPrincipalEstado.SelectedValue = ParametrosInterface.objUsuarioLogado.Estado.TB005_Id;
            cmbContratoPrincipalTitularEstado.SelectedValue = ParametrosInterface.objUsuarioLogado.Estado.TB005_Id;

            EstadoController filtro = new EstadoController();
            filtro.TB005_Id = Convert.ToInt16(ParametrosInterface.objUsuarioLogado.Estado.TB005_Id);
            PopularContratoPrincipalMunicipios(filtro);
            PopularContratoPrincipalTitularEstados(filtro);


            cmbContatoPrincipalMunicipio.SelectedValue = ParametrosInterface.objUsuarioLogado.Municipio.TB006_id;
            cmbContatoPrincipalTitularMunicipio.SelectedValue = ParametrosInterface.objUsuarioLogado.Municipio.TB006_id;


            cmbContratoPrincipalStatusContrato.SelectedValue = "0";
            tabPrincipal.TabPages.Remove(tbLista);
        }
        private void LimparContratoPrincipal()
        {
            mskContratoPrincipalTitularCNPJ.Text = "";
            txtTB020_NomeExibicaoDetalhes.Text = "";
            txtTB020_CategoriaExibicao.Text = "";
            txtTB013_NomeExibicaoDetalhes.Text = "";
            txtContratoPrincipalUnidadeId.Text = "";
            mnuContratoPrincipalColaboradores.Enabled = false;
            lblContratoPrincipal.Text = "";
            txtContratoPrincipalTitularIdProtheus.Text = "";
            dtpContratoPrincipalInicio.Value = DateTime.Now;
            dtpContratoPrincipalFim.Value = DateTime.Now.AddMonths(12);
            mskContratoPrincipalDoc.Text = "";
            cmbContratoPrincipalStatusContrato.SelectedValue = "0";
            txtContratoPrincipalNomeFantasia.Text = "";
            txtContratoPrincipalRazaoSocial.Text = "";
            mskContratoPrincipalCEP.Text = "";
            txtContratoPrincipalComplemento.Text = "";
            txtContratoPrincipalLogradouro.Text = "";
            txtContratoPrincipalNumero.Text = "";
            txtContratoPrincipalBairro.Text = "";
            lblContratoPrincipalTitularId.Text = "";
            lblCarteirinha.Text = "";
            txtContratoPrincipalTitularRG.Text = "";
            txtContratoPrincipalTitularRGOrgaoEmissor.Text = "";
            txtContratoPrincipalTitularNomeCompleto.Text = "";
            mskContratoPrincipalTitularCEP.Text = "";
            txtContratoPrincipalTitularBairro.Text = "";
            txtContratoPrincipalTitularLogradouro.Text = "";
            txtContratoPrincipalTitularNumero.Text = "";
            txtContratoPrincipalTitularComplemento.Text = "";
            cnkContratoPrincipalTitularEraContezino.Checked = false;
            DTContratoPrincipalContatos.DataSource = null;
            DTContratoPrincipalContatos.Refresh();
            DTContratoPrincipalContatos.Rows.Clear();
        }
        private void cmbContratoPrincipalPais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cmbContratoPrincipalEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbContratoPrincipalEstado.SelectedValue) > 0)
                {
                    EstadoController filtro = new EstadoController();
                    filtro.TB005_Id = Convert.ToInt16(cmbContratoPrincipalEstado.SelectedValue);
                    PopularContratoPrincipalMunicipios(filtro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularContratoPrincipalMunicipios(EstadoController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbContatoPrincipalMunicipio.DataSource = null;
            cmbContatoPrincipalMunicipio.Items.Clear();
            try
            {
                cmbContatoPrincipalMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbContatoPrincipalMunicipio.DisplayMember = "TB006_Municipio";
                cmbContatoPrincipalMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mskContratoPrincipalCEP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mskContratoPrincipalCEP.Text.Replace("-", "").Replace(" ", "").Length == 8)
                {

                    EnderecoNegocios Endereco_N = new EnderecoNegocios();
                    CEPController filtro = new CEPController();
                    filtro.TB004_Cep = Convert.ToInt32(mskContratoPrincipalCEP.Text.Replace("-", "").Replace(" ", ""));
                    DataSet CEP = Endereco_N.Cep(filtro);

                    if (Convert.ToInt64(CEP.Tables[0].Rows[0]["TB004_id"].ToString()) > 0)
                    {
                        txtContratoPrincipalLogradouro.Text = CEP.Tables[0].Rows[0]["TB004_Logradouro"].ToString();
                        txtContratoPrincipalBairro.Text = CEP.Tables[0].Rows[0]["TB004_Bairro"].ToString();

                        cmbContratoPrincipalEstado.SelectedValue = CEP.Tables[0].Rows[0]["TB005_Id"].ToString();
                        cmbContatoPrincipalMunicipio.SelectedValue = CEP.Tables[0].Rows[0]["TB006_id"].ToString();

                        txtContratoPrincipalNumero.Focus();
                    }
                    else
                    {
                        MessageBox.Show(MensagensDoSistema._0005, "Retorno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtpContratoPrincipalInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime Hoje = dtpContratoPrincipalInicio.Value;
            dtpContratoPrincipalFim.Value = Hoje.AddMonths(12);
        }
        private void cmbContratoPrincipalTitularPais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopularContratoPrincipalTitularEstados(EstadoController filtro)
        {

            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbContatoPrincipalTitularMunicipio.DataSource = null;
            cmbContatoPrincipalTitularMunicipio.Items.Clear();
            try
            {
                cmbContatoPrincipalTitularMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbContatoPrincipalTitularMunicipio.DisplayMember = "TB006_Municipio";
                cmbContatoPrincipalTitularMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbContratoPrincipalTitularEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbContratoPrincipalTitularEstado.SelectedValue) > 0)
                {
                    EstadoController filtro = new EstadoController();
                    filtro.TB005_Id = Convert.ToInt16(cmbContratoPrincipalTitularEstado.SelectedValue);
                    PopularContratoPrincipalTitularMunicipios(filtro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularContratoPrincipalTitularMunicipios(EstadoController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbContatoPrincipalTitularMunicipio.DataSource = null;
            cmbContatoPrincipalTitularMunicipio.Items.Clear();
            try
            {
                cmbContatoPrincipalTitularMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbContatoPrincipalTitularMunicipio.DisplayMember = "TB006_Municipio";
                cmbContatoPrincipalTitularMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void mskContratoPrincipalTitularCEP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mskContratoPrincipalTitularCEP.Text.Replace("-", "").Replace(" ", "").Length == 8)
                {

                    EnderecoNegocios Endereco_N = new EnderecoNegocios();
                    CEPController filtro = new CEPController();
                    filtro.TB004_Cep = Convert.ToInt32(mskContratoPrincipalTitularCEP.Text.Replace("-", "").Replace(" ", ""));
                    DataSet CEP = Endereco_N.Cep(filtro);

                    if (Convert.ToInt64(CEP.Tables[0].Rows[0]["TB004_id"].ToString()) > 0)
                    {
                        txtContratoPrincipalTitularLogradouro.Text = CEP.Tables[0].Rows[0]["TB004_Logradouro"].ToString();
                        txtContratoPrincipalTitularBairro.Text = CEP.Tables[0].Rows[0]["TB004_Bairro"].ToString();
                        cmbContratoPrincipalTitularEstado.SelectedValue = CEP.Tables[0].Rows[0]["TB005_Id"].ToString();
                        cmbContatoPrincipalTitularMunicipio.SelectedValue = CEP.Tables[0].Rows[0]["TB006_id"].ToString();
                        txtContratoPrincipalTitularNumero.Focus();
                    }
                    else
                    {
                        MessageBox.Show(MensagensDoSistema._0005, "Retorno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuContratoPrincipalSalvar_Click(object sender, EventArgs e)
        {
            if (lblContratoPrincipal.Text.Trim() == string.Empty)
            {
                SalvarContratoPrincipal();
                mskContratoPrincipalDoc.Enabled = false;
                mskContratoPrincipalTitularCNPJ.Enabled = false;

            }
            else
            {
                AtualizarContratoPrincipal();
            }
        }
        private void mskContratoPrincipalDoc_Leave(object sender, EventArgs e)
        {
            if (lblContratoPrincipal.Text.Trim() == string.Empty)
            {
                if (mskContratoPrincipalDoc.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") != string.Empty)
                {
                    Util ValidaDOC = new Util();
                    //Pessoa Juridica
                    if (!ValidaDOC.CNPJ(mskContratoPrincipalDoc.Text))
                    {
                        MessageBox.Show(MensagensDoSistema._0048.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    /*Passou pela validação, verifica se o registro já não existe*/
                    UnidadeNegocios Unidade_N = new UnidadeNegocios();
                    UnidadeController Retorno = Unidade_N.UnidadeCNPJJaCadastradoCorporativo(mskContratoPrincipalDoc.Text); /*3 para contrato corporativo*/
                    if (Retorno.TB012_idCorporativo > 0)
                    {
                        MessageBox.Show(MensagensDoSistema._0052.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mnuContratoPrincipalColaboradores.Enabled = true;
                        if (!RecuperarContratoPrincipal(Convert.ToInt64(Retorno.TB012_idCorporativo), 1))
                        {
                            tabPrincipal.TabPages.Add(tbLista);
                            tabPrincipal.TabPages.Remove(tbContratoPrincipal);
                            mnuContratoPrincipalColaboradores.Enabled = false;
                        }
                    }
                    else
                    {
                        Retorno = Unidade_N.UnidadeCNPJJaCadastrado(mskContratoPrincipalDoc.Text, 2); /*2 para contrato Parceiro*/
                        if (Retorno.TB020_id > 0)
                        {
                            UnidadeController Unidade_C = Unidade_N.UnidadeSelect(Retorno.TB020_id);

                            txtTB020_NomeExibicaoDetalhes.Text = Unidade_C.TB020_NomeExibicaoDetalhes;
                            txtTB020_CategoriaExibicao.Text = Unidade_C.TB020_CategoriaExibicao;
                            txtContratoPrincipalUnidadeId.Text = Unidade_C.TB020_id.ToString();
                            txtContratoPrincipalNomeFantasia.Text = Unidade_C.TB020_NomeFantasia;
                            txtContratoPrincipalRazaoSocial.Text = Unidade_C.TB020_RazaoSocial;
                            mskContratoPrincipalCEP.Text = Unidade_C.TB020_Cep;
                            txtTB020_NomeExibicaoDetalhes.Text = Unidade_C.TB020_NomeExibicaoDetalhes;
                            txtTB020_CategoriaExibicao.Text = Unidade_C.TB020_CategoriaExibicao;
                            txtContratoPrincipalLogradouro.Text = Unidade_C.TB020_Logradouro;
                            txtContratoPrincipalNumero.Text = Unidade_C.TB020_Numero;
                            txtContratoPrincipalComplemento.Text = Unidade_C.TB020_Complemento;
                            cmbContratoPrincipalEstado.SelectedValue = Convert.ToInt64(Unidade_C.Estado.TB005_Id);
                            cmbContatoPrincipalMunicipio.SelectedValue = Convert.ToInt64(Unidade_C.Municipio.TB006_id);
                            txtContratoPrincipalBairro.Text = Unidade_C.TB020_Bairro;

                        }
                    }
                }
            }
        }
        private void mskContratoPrincipalTitularCNPJ_Leave(object sender, EventArgs e)
        {

            if (lblContratoPrincipalTitularId.Text.Trim() == string.Empty)
            {
                if (mskContratoPrincipalTitularCNPJ.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") != string.Empty)
                {
                    Util ValidaDOC = new Util();

                    //Pessoa Fisica
                    if (!ValidaDOC.CPF(mskContratoPrincipalTitularCNPJ.Text))
                    {
                        MessageBox.Show(MensagensDoSistema._0031.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    /*Passou pela validação, verifica se o registro já não existe*/
                    PessoaNegocios Pessoa_N = new PessoaNegocios();
                    PessoaController Titular = Pessoa_N.pessoaSelectCPFCNPJ(mskContratoPrincipalTitularCNPJ.Text);
                    if (Titular.TB013_id > 0)
                    {
                        lblContratoPrincipalTitularId.Text = Titular.TB013_id.ToString();
                        dtpContratoPrincipalTitularCNPJDataNascimento.Value = Titular.TB013_DataNascimento;
                        lblCarteirinha.Text = Titular.TB013_Cartao;
                        txtContratoPrincipalTitularRG.Text = Titular.TB013_RG;
                        txtContratoPrincipalTitularRGOrgaoEmissor.Text = Titular.TB013_RGOrgaoEmissor;
                        txtContratoPrincipalTitularNomeCompleto.Text = Titular.TB013_NomeCompleto;
                        mskContratoPrincipalTitularCEP.Text = Titular.TB004_Cep;
                        cmbContratoPrincipalTitularEstado.SelectedValue = Titular.Municipio.Estado.TB005_Id;
                        EstadoController filtro = new EstadoController();
                        filtro.TB005_Id = Convert.ToInt16(Titular.Municipio.Estado.TB005_Id);
                        PopularContratoPrincipalTitularMunicipios(filtro);
                        cmbContatoPrincipalTitularMunicipio.SelectedValue = Convert.ToInt64(Titular.Municipio.TB006_id);
                        txtContratoPrincipalTitularBairro.Text = Titular.TB013_Bairro;
                        txtContratoPrincipalTitularLogradouro.Text = Titular.TB013_Logradouro;
                        txtContratoPrincipalTitularNumero.Text = Titular.TB013_Numero;
                        txtContratoPrincipalTitularComplemento.Text = Titular.TB013_Complemento;
                        txtContratoPrincipalTitularIdProtheus.Text = Titular.TB013_IdProtheus;
                    }
                }
            }
        }
        private void mnuContratoPrincipalColaboradores_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbBeneficiados);
            try
            {
                view_CorporativoListaBeneficiariosTableAdapter1.Connection.ConnectionString = ParametrosInterface.ConectReport;
                view_CorporativoListaBeneficiariosTableAdapter1.Fill(clubeContezaCorporativoListaBeneficiarios.View_CorporativoListaBeneficiarios, Convert.ToInt64(lblContratoPrincipal.Text));
                this.rpvBeneficiados.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tabPrincipal.TabPages.Remove(tbContratoPrincipal);
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbColaborador);

            PessoaNegocios Pessoa_N = new PessoaNegocios();

            ddgColaboradoresContratoPrincipal.AutoGenerateColumns = false;
            ddgColaboradoresContratoPrincipal.DataSource = Pessoa_N.ColaboradoresCorporativo(Convert.ToInt64(lblColaboradorContratoPai.Text));
            ddgColaboradoresContratoPrincipal.Refresh();
            lblColaboradorContratoPai.Text = "";
            lblStatusCarteirinha.Text = "Cartão";
            txtDependenteNomeCompleto.Text = "";
            lblDependenteId.Text = "";
            lblColaboradorUnidadeId.Text = "";
            lblColaboradorPontoDeVenda.Text = "";
            lblColaboradorUnidadeNomeFantasia.Text = "";
            lblColaboradorUnidadeMunicipio.Text = "";
            lblColaboradorUnidadeBairro.Text = "";
            lblColaboradorUnidadeEnd.Text = "";
            lblColaboradorId.Text = "";
            lblColaboradorContrato.Text = "";
            mskColaboradorCPF.Text = "";
            txtColaboradorMatricula.Text = "";
            grpAcessoManutencao.Text = "";
            txtColaboradorNomeCompleto.Text = "";
            mskColaboradorCEP.Text = "";
            txtColaboradorBairro.Text = "";
            txtColaboradorLogradouro.Text = "";
            txtColaboradorNumero.Text = "";
            txtColaboradorComplemento.Text = "";
            ddgDependentes.DataSource = null;
            ddgDependentes.Refresh();

            tabPrincipal.TabPages.Add(tbContratoPrincipal);
        }

        private void CarregarUnidadesDoContrato()
        {
            try
            {
                UnidadeNegocios Unidade_N = new UnidadeNegocios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Boolean ValidarContratoPrincipal()
        {
            if (mskContratoPrincipalDoc.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Documento do contrato principal "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoPrincipalDoc.Focus();
                return false;
            }

            if (txtContratoPrincipalNomeFantasia.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Nome Fantasia "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalNomeFantasia.Focus();
                return false;
            }

            if (txtContratoPrincipalRazaoSocial.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Razão Social "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalRazaoSocial.Focus();
                return false;
            }

            if (mskContratoPrincipalCEP.Text.Trim().Replace("-", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CEP "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoPrincipalCEP.Focus();
                return false;
            }
            if (mskContratoPrincipalCEP.Text.Trim().Replace("-", "").Length < 8)
            {
                MessageBox.Show(MensagensDoSistema._0032.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoPrincipalCEP.Focus();
                return false;
            }

            if (txtContratoPrincipalBairro.Text.Trim().Replace("-", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Bairro "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalBairro.Focus();
                return false;
            }

            if (txtContratoPrincipalLogradouro.Text.Trim().Replace("-", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Logradouro do contrato principal "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalLogradouro.Focus();
                return false;
            }

            if (txtContratoPrincipalNumero.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalNumero.Text = "S/N";
            }



            if (txtContratoPrincipalNumero.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalNumero.Text = "S/N";
            }

            if (txtContratoPrincipalComplemento.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalComplemento.Text = " ";
            }
            if (txtContratoPrincipalComplemento.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalComplemento.Text = " ";
            }

            /**/
            if (mskContratoPrincipalTitularCNPJ.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CNPJ Titular "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoPrincipalTitularCNPJ.Focus();
                return false;
            }

            DateTime dttFromDate = DateTime.Now;
            DateTime dttToDate = Convert.ToDateTime(dtpContratoPrincipalTitularCNPJDataNascimento.Text);
            TimeSpan tsDuration;
            tsDuration = dttFromDate - dttToDate;

            if (Convert.ToInt32((tsDuration.Days) / 365) < 18)
            {
                MessageBox.Show(MensagensDoSistema._0006, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpContratoPrincipalTitularCNPJDataNascimento.Focus();
                return false;
            }
            if (txtContratoPrincipalTitularRG.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "RG Titular "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalTitularRG.Focus();
                return false;
            }

            if (txtContratoPrincipalTitularRGOrgaoEmissor.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Orgao Emissor RG"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalTitularRGOrgaoEmissor.Focus();
                return false;
            }

            if (txtContratoPrincipalTitularNomeCompleto.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Nome Completo Titular"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalTitularNomeCompleto.Focus();
                return false;
            }

            if (mskContratoPrincipalTitularCEP.Text.Trim().Replace("-", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CEP "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoPrincipalTitularCEP.Focus();
                return false;
            }
            if (mskContratoPrincipalTitularCEP.Text.Trim().Replace("-", "").Length < 8)
            {
                MessageBox.Show(MensagensDoSistema._0032.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoPrincipalTitularCEP.Focus();
                return false;
            }

            if (txtContratoPrincipalTitularBairro.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Bairro do Titular "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalTitularBairro.Focus();
                return false;
            }

            if (txtContratoPrincipalTitularLogradouro.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Logradouro Titular "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoPrincipalTitularLogradouro.Focus();
                return false;
            }

            if (txtContratoPrincipalTitularNumero.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalTitularNumero.Text = "S/N";
            }



            if (txtContratoPrincipalTitularComplemento.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalTitularComplemento.Text = " ";
            }

            if (txtContratoPrincipalTitularIdProtheus.Text.Trim() == string.Empty)
            {
                txtContratoPrincipalTitularIdProtheus.Text = "Id Protheus";
            }


            return true;
        }
        private void DTContratoPrincipalContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ContatoController contato = new ContatoController();
                ContatoNegocios ContatoN = new ContatoNegocios();

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    contato.TB009_id = Convert.ToInt64(DTContratoPrincipalContatos.Rows[e.RowIndex].Cells["txtContratoPrincipalContatoId"].Value);

                    switch (DTContratoPrincipalContatos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Excluir":
                            break;
                        case "Salvar":

                            if (lblContratoPrincipal.Text.Trim() == string.Empty)
                            {
                                SalvarContratoPrincipal();
                            }
                            PessoaController Pessoa_C = new PessoaController();
                            contato.Pessoa = Pessoa_C;
                            contato.Pessoa.TB013_id = 0;
                            contato.TB009_ExibirPortal = Convert.ToInt16(DTContratoPrincipalContatos.Rows[e.RowIndex].Cells["chkContratoPrincipalContatoExibirPortal"].Value);
                            contato.TB020_id = Convert.ToInt64(txtContratoPrincipalUnidadeId.Text);
                            contato.TB009_TipoS = Convert.ToString(DTContratoPrincipalContatos.Rows[e.RowIndex].Cells["cmbContratoPrincipalContatoTipo"].Value);
               
                            if (Convert.ToInt16(contato.TB009_TipoS) == 3)//Email
                            {
                                contato.TB009_Contato = Convert.ToString(DTContratoPrincipalContatos.Rows[e.RowIndex].Cells["txtContratoPrincipalContato"].Value);
                            }
                            else
                            {
                                String Contato = Convert.ToString(DTContratoPrincipalContatos.Rows[e.RowIndex].Cells["txtContratoPrincipalContato"].Value).ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');


                                if (Convert.ToInt16(contato.TB009_TipoS) < 4)
                                {
                                    if (Contato.Length == 10)
                                    {
                                        contato.TB009_Contato = Convert.ToUInt64(Contato).ToString(@"(00\)0000\-0000");
                                    }
                                    else
                                    {
                                        if (Contato.Length == 11)
                                        {
                                            contato.TB009_Contato = Convert.ToUInt64(Contato).ToString(@"(00\)000\-000\-000");
                                        }
                                    }

                                    DTContratoPrincipalContatos.Rows[e.RowIndex].Cells[3].Value = contato.TB009_Contato;
                                }
                            }


                            /**/
                            contato.TB009_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            contato.TB009_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            contato.TB009_Nota = "Cadastrado via Gestor";

                            if (ValidaContato(contato))
                            {
                                if (contato.TB009_id > 0)
                                {
                                    ContatoN.contatosContratoUpdate(contato);
                                }
                                else
                                {
                                    DTContratoPrincipalContatos.Rows[e.RowIndex].Cells[0].Value = ContatoN.contatosContratoInsert(contato, Convert.ToInt64(lblContratoPrincipal.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Boolean ValidaContato(ContatoController contato)
        {

            if (contato.TB009_TipoS.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Tipo"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (contato.TB009_Contato.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Contato"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void SalvarContratoPrincipal()
        {
            if (ValidarContratoPrincipal())
            {
                /*Se o titular não estiver cadastrado, cadastre antes de continuar*/
                Int64 ContratoPrincipalTitularId = 0;
                if (lblContratoPrincipalTitularId.Text.Trim() == string.Empty)
                {
                    PessoaController ContratoPrincipalTitular = new PessoaController();
                    ContratoPrincipalTitular.TB013_IdProtheus = txtContratoPrincipalTitularIdProtheus.Text;
                    ContratoPrincipalTitular.TB013_TipoS = "1";
                    ContratoPrincipalTitular.TB013_CPFCNPJ = mskContratoPrincipalTitularCNPJ.Text;
                    ContratoPrincipalTitular.TB013_RG = txtContratoPrincipalTitularRG.Text;
                    ContratoPrincipalTitular.TB013_RGOrgaoEmissor = txtContratoPrincipalTitularRGOrgaoEmissor.Text;



                    if (cnkContratoPrincipalTitularEraContezino.Checked == true)
                    {
                        ContratoPrincipalTitular.TB012_EraContezino = 1;
                    }
                    else
                    {
                        ContratoPrincipalTitular.TB012_EraContezino = 0;
                    }

                    ContratoPrincipalTitular.TB013_ListaNegra = 0;
                    ContratoPrincipalTitular.TB013_NomeCompleto = txtContratoPrincipalTitularNomeCompleto.Text;
                    ContratoPrincipalTitular.TB013_NomeExibicao = txtContratoPrincipalTitularNomeCompleto.Text;
                    ContratoPrincipalTitular.TB013_NomeExibicaoDetalhes = txtContratoPrincipalTitularNomeCompleto.Text;
                    ContratoPrincipalTitular.TB013_StatusS = "0";
                    ContratoPrincipalTitular.TB013_SexoS = cmbContratoPrincipalTitularSexo.SelectedValue.ToString();
                    ContratoPrincipalTitular.TB013_DataNascimento = dtpContratoPrincipalTitularCNPJDataNascimento.Value;
                    ContratoPrincipalTitular.TB013_DeclaroSerMaiorCapaz = 1;
                    ContratoPrincipalTitular.TB004_Cep = mskContratoPrincipalTitularCEP.Text;

                    MunicipioController objMunicipio = new MunicipioController();
                    ContratoPrincipalTitular.Municipio = objMunicipio;
                    ContratoPrincipalTitular.Municipio.TB006_id = Convert.ToInt64(cmbContatoPrincipalMunicipio.SelectedValue);
                    ContratoPrincipalTitular.TB013_Logradouro = txtContratoPrincipalTitularLogradouro.Text;
                    ContratoPrincipalTitular.TB013_Numero = txtContratoPrincipalTitularNumero.Text;
                    ContratoPrincipalTitular.TB013_Bairro = txtContratoPrincipalTitularBairro.Text;
                    ContratoPrincipalTitular.TB013_Complemento = txtContratoPrincipalTitularComplemento.Text;
                    ContratoPrincipalTitular.TB013_CadastradoEm = DateTime.Now;
                    ContratoPrincipalTitular.TB013_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    ContratoPrincipalTitular.TB013_AlteradoEm = DateTime.Now;
                    ContratoPrincipalTitular.TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    ContratoPrincipalTitular.TB013_CodigoDependente = 1001;


                    PessoaNegocios ContratoPrincipalTitular_N = new PessoaNegocios();

                    /**/

                    if (lblContratoPrincipalTitularId.Text.Trim() == string.Empty)
                    {
                        PessoaController Retorno = ContratoPrincipalTitular_N.PessoaInsert(ContratoPrincipalTitular);

                        ContratoPrincipalTitularId = Retorno.TB013_id;

                        if (ContratoPrincipalTitularId > 0)
                        {
                            lblContratoPrincipalTitularId.Text = ContratoPrincipalTitularId.ToString();
                        }
                        else
                        {
                            MessageBox.Show(MensagensDoSistema._0048.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        ContratoPrincipalTitularId = Convert.ToInt64(lblContratoPrincipalTitularId.Text);
                    }
                }

                else
                {
                    ContratoPrincipalTitularId = Convert.ToInt64(lblContratoPrincipalTitularId.Text);
                }

                /*Cadastrar Contrato & Unidade*/
                ContratosController contratoPrincipal = new ContratosController();
                contratoPrincipal.PontoDeVenda = new PontoDeVendaController();
                contratoPrincipal.Titular = new PessoaController();
                contratoPrincipal.TB012_CicloContrato = dtpContratoPrincipalInicio.Value.Month.ToString() + dtpContratoPrincipalInicio.Value.Year.ToString();
                contratoPrincipal.PontoDeVenda.TB002_id = Convert.ToInt64(cmbContratoPrincipalPontoVenda.SelectedValue);
                contratoPrincipal.Titular.TB013_id = ContratoPrincipalTitularId;
                contratoPrincipal.TB012_Inicio = dtpContratoPrincipalInicio.Value;
                contratoPrincipal.TB012_Fim = dtpContratoPrincipalFim.Value;
                contratoPrincipal.TB012_AceiteContrato = 1;
                contratoPrincipal.TB012_DataAceiteContrato = dtpContratoPrincipalInicio.Value;
                contratoPrincipal.TB012_CadastradorPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                contratoPrincipal.TB012_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                contratoPrincipal.TB012_StatusS = "0";
                contratoPrincipal.TB004_Cep = mskContratoPrincipalCEP.Text;
                contratoPrincipal.TB006_id = Convert.ToInt64(cmbContatoPrincipalMunicipio.SelectedValue);
                contratoPrincipal.TB012_Logradouro = txtContratoPrincipalLogradouro.Text;
                contratoPrincipal.TB012_Numero = txtContratoPrincipalNumero.Text;
                contratoPrincipal.TB012_Bairro = txtContratoPrincipalBairro.Text;
                contratoPrincipal.TB012_Complemento = txtContratoPrincipalComplemento.Text;
                contratoPrincipal.TB012_TipoContrato = 3;
                contratoPrincipal.TB012_CodCartao = 1;

                ContratoNegocios contratoPrincipal_N = new ContratoNegocios();

                ContratosController contratoPrincipal_R = contratoPrincipal_N.InsertContrato(contratoPrincipal);
                lblContratoPrincipal.Text = contratoPrincipal_R.TB012_Id.ToString();


                /*Cadastrar Unidade*/
                UnidadeController UnidadePrincipal = new UnidadeController();
                UnidadeController UnidadePrincipal_R = new UnidadeController();
                UnidadeNegocios UnidadePrincipal_N = new UnidadeNegocios();

                if (txtContratoPrincipalUnidadeId.Text.Trim() == string.Empty)
                {
                    UnidadePrincipal.TB012_idCorporativo = contratoPrincipal_R.TB012_Id;
                    UnidadePrincipal.TB020_NomeExibicaoDetalhes = txtContratoPrincipalNomeFantasia.Text;
                    UnidadePrincipal.TB020_CategoriaExibicao = "CORPORATIVO";
                    UnidadePrincipal.TB020_Matriz = 1;
                    UnidadePrincipal.TB020_RazaoSocial = txtContratoPrincipalRazaoSocial.Text;
                    UnidadePrincipal.TB020_NomeFantasia = txtContratoPrincipalNomeFantasia.Text;
                    UnidadePrincipal.TB020_TipoPessoa = 2;
                    UnidadePrincipal.TB020_Documento = mskContratoPrincipalDoc.Text;
                    UnidadePrincipal.TB006_id = Convert.ToInt64(cmbContatoPrincipalMunicipio.SelectedValue);
                    UnidadePrincipal.TB020_Cep = mskContratoPrincipalCEP.Text;
                    UnidadePrincipal.TB020_Logradouro = txtContratoPrincipalLogradouro.Text;
                    UnidadePrincipal.TB020_Numero = txtContratoPrincipalNumero.Text;
                    UnidadePrincipal.TB020_Bairro = txtContratoPrincipalBairro.Text;
                    UnidadePrincipal.TB020_Complemento = txtContratoPrincipalComplemento.Text;
                    UnidadePrincipal.TB020_TextoPortal = "Cadastro Corporativo";

                    UnidadePrincipal.TB020_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;

                    UnidadePrincipal.TB020_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    UnidadePrincipal.TB020_StatusS = "0";
                    //UnidadePrincipal.TB021_id = 0;
                    //UnidadePrincipal.TB020_Desconto = "Cadastro Corporativo";



                    UnidadePrincipal_R = UnidadePrincipal_N.UnidadeInsert(UnidadePrincipal);
                }
                else
                {
                    UnidadePrincipal_R.TB020_id = Convert.ToInt64(txtContratoPrincipalUnidadeId.Text);



                }


                /*Vincular Unidade ao Contrto Corporativo*/
                UnidadePrincipal_N.CorporativoVinculaUnidadeContrato(UnidadePrincipal_R.TB020_id, contratoPrincipal_R.TB012_Id);


                if (UnidadePrincipal_R.TB020_id > 0)
                {
                    txtContratoPrincipalUnidadeId.Text = UnidadePrincipal_R.TB020_id.ToString();
                    MessageBox.Show(MensagensDoSistema._0017, "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mnuContratoPrincipalColaboradores.Enabled = true;
                }

            }
        }
        private void dgwContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgwContratos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":

                            tabPrincipal.TabPages.Add(tbContratoPrincipal);
                            tabPrincipal.TabPages.Remove(tbLista);
                            mnuContratoPrincipalColaboradores.Enabled = true;
                            cmbContratoPrincipalPontoVenda.Enabled = false;
                            PessoaNegocios Pessoa_N = new PessoaNegocios();


                            if (!RecuperarContratoPrincipal(Convert.ToInt64(dgwContratos.Rows[e.RowIndex].Cells["LTB012_id"].Value), 1))
                            {
                                tabPrincipal.TabPages.Add(tbLista);
                                tabPrincipal.TabPages.Remove(tbContratoPrincipal);
                                mnuContratoPrincipalColaboradores.Enabled = false;
                            }

                            mskContratoPrincipalDoc.Enabled = false;
                            mskContratoPrincipalTitularCNPJ.Enabled = false;

                            ddgColaboradoresContratoPrincipal.AutoGenerateColumns = false;
                            ddgColaboradoresContratoPrincipal.DataSource = Pessoa_N.ColaboradoresCorporativo(Convert.ToInt64(dgwContratos.Rows[e.RowIndex].Cells["LTB012_id"].Value));
                            ddgColaboradoresContratoPrincipal.Refresh();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool RecuperarContratoPrincipal(long TB012_id, int TB020_Matriz)
        {
            try
            {
                ContratoNegocios Contrato_N = new ContratoNegocios();

                ContratosController Retorno = Contrato_N.ContratoCorporativoSelect(TB012_id, TB020_Matriz);

              




                    lblCicloAtual.Text = Retorno.TB012_CicloContrato;
                lblContratoPrincipal.Text = Retorno.TB012_Id.ToString();
                cmbContratoPrincipalPontoVenda.SelectedValue = Convert.ToInt64(Retorno.PontoDeVenda.TB002_id);
                dtpContratoPrincipalInicio.Value = Retorno.TB012_Inicio;
                dtpContratoPrincipalFim.Value = Retorno.TB012_Fim;
                mskContratoPrincipalDoc.Text = Retorno.Unidade.TB020_Documento;
                cmbContratoPrincipalStatusContrato.SelectedValue = Retorno.TB012_StatusS;

                txtContratoPrincipalUnidadeId.Text = Retorno.Unidade.TB020_id.ToString();
                txtContratoPrincipalNomeFantasia.Text = Retorno.Unidade.TB020_NomeFantasia;
                txtContratoPrincipalRazaoSocial.Text = Retorno.Unidade.TB020_RazaoSocial;
                txtTB020_NomeExibicaoDetalhes.Text = Retorno.Unidade.TB020_NomeExibicaoDetalhes;
                txtTB020_CategoriaExibicao.Text = Retorno.Unidade.TB020_CategoriaExibicao;

                mskContratoPrincipalCEP.Text = Retorno.Unidade.TB020_Cep;

                cmbContratoPrincipalPais.SelectedValue = Convert.ToInt64(Retorno.Pais.TB003_id);



                cmbContratoPrincipalEstado.SelectedValue = Convert.ToInt64(Retorno.Unidade.Estado.TB005_Id);

                try
                {
                    if (Convert.ToInt16(cmbContratoPrincipalEstado.SelectedValue) > 0)
                    {
                        EstadoController filtro = new EstadoController();
                        filtro.TB005_Id = Convert.ToInt16(Retorno.Unidade.Estado.TB005_Id);
                        PopularContratoPrincipalMunicipios(filtro);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                cmbContatoPrincipalMunicipio.SelectedValue = Convert.ToInt64(Retorno.Unidade.Municipio.TB006_id);
                txtContratoPrincipalBairro.Text = Retorno.Unidade.TB020_Bairro;
                txtContratoPrincipalLogradouro.Text = Retorno.Unidade.TB020_Logradouro;
                txtContratoPrincipalNumero.Text = Retorno.Unidade.TB020_Numero;
                txtContratoPrincipalComplemento.Text = Retorno.Unidade.TB020_Complemento;

                lblContratoPrincipalTitularId.Text = Retorno.Titular.TB013_id.ToString();
                mskContratoPrincipalTitularCNPJ.Text = Retorno.Titular.TB013_CPFCNPJ;
                dtpContratoPrincipalTitularCNPJDataNascimento.Value = Retorno.Titular.TB013_DataNascimento;
                lblCarteirinha.Text = Retorno.Titular.TB013_Cartao;
                txtContratoPrincipalTitularRG.Text = Retorno.Titular.TB013_RG;
                txtContratoPrincipalTitularRGOrgaoEmissor.Text = Retorno.Titular.TB013_RGOrgaoEmissor;
                cmbContratoPrincipalTitularSexo.SelectedValue = Retorno.Titular.TB013_SexoS;
                cmbContratoPrincipalTitularPais.SelectedValue = Retorno.Titular.Pais.TB003_id;

                if (Retorno.Titular.TB012_EraContezino == 1)
                {
                    cnkContratoPrincipalTitularEraContezino.Checked = true;
                }
                else
                {
                    cnkContratoPrincipalTitularEraContezino.Checked = false;
                }


                txtContratoPrincipalTitularNomeCompleto.Text = Retorno.Titular.TB013_NomeCompleto;
                txtContratoPrincipalTitularIdProtheus.Text = Retorno.Titular.TB013_IdProtheus;
                mskContratoPrincipalTitularCEP.Text = Retorno.Titular.TB004_Cep;
                cmbContratoPrincipalTitularPais.SelectedValue = Retorno.Titular.Pais.TB003_id;
                cmbContratoPrincipalTitularEstado.SelectedValue = Retorno.Titular.Estado.TB005_Id;
                cmbContatoPrincipalTitularMunicipio.SelectedValue = Retorno.Titular.Municipio.TB006_id;
                txtContratoPrincipalTitularBairro.Text = Retorno.Titular.TB013_Bairro;
                txtContratoPrincipalTitularLogradouro.Text = Retorno.Titular.TB013_Logradouro;
                txtContratoPrincipalTitularNumero.Text = Retorno.Titular.TB013_Numero;
                RecuperarContatosContratoPrincipal(Retorno.Unidade.TB020_id);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void RecuperarContatosContratoPrincipal(long TB020_id)
        {
            try
            {
                ContatoNegocios Contato_N = new ContatoNegocios();
                List<ContatoController> Retorno = Contato_N.contatosUnidade(TB020_id);

                for (int i = 0; i < Retorno.Count; i++)
                {
                    DTContratoPrincipalContatos.Rows.Add(new object[] { Retorno[i].TB009_id });
                    DTContratoPrincipalContatos.Rows[i].Cells["cmbContratoPrincipalContatoTipo"].Value = Retorno[i].TB009_TipoS.ToString();
                    DTContratoPrincipalContatos.Rows[i].Cells["txtContratoPrincipalContato"].Value = Retorno[i].TB009_Contato;
                    DTContratoPrincipalContatos.Rows[i].Cells["chkContratoPrincipalContatoExibirPortal"].Value = Retorno[i].TB009_ExibirPortal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AtualizarContratoPrincipal()
        {
            try
            {
                ContratosController ContratoCorporativo = new ContratosController();

                ContratoCorporativo.PontoDeVenda = new PontoDeVendaController();

                ContratoCorporativo.TB012_Id = Convert.ToInt64(lblContratoPrincipal.Text);
                ContratoCorporativo.TB012_Inicio = dtpContratoPrincipalInicio.Value;
                ContratoCorporativo.TB012_Fim = dtpContratoPrincipalFim.Value;
                ContratoCorporativo.TB004_Cep = mskContratoPrincipalCEP.Text;
                ContratoCorporativo.TB006_id = Convert.ToInt64(cmbContatoPrincipalMunicipio.SelectedValue);
                ContratoCorporativo.TB012_Logradouro = txtContratoPrincipalLogradouro.Text;
                ContratoCorporativo.TB012_Numero = txtContratoPrincipalNumero.Text;
                ContratoCorporativo.TB012_Bairro = txtContratoPrincipalBairro.Text;
                ContratoCorporativo.TB012_Complemento = txtContratoPrincipalComplemento.Text;
                ContratoCorporativo.TB012_StatusS = cmbContratoPrincipalStatusContrato.SelectedValue.ToString();
                ContratoCorporativo.PontoDeVenda.TB002_id = Convert.ToInt64(cmbContratoPrincipalPontoVenda.SelectedValue);


                UnidadeController Unidade_C = new UnidadeController();
                Unidade_C.TB020_id = Convert.ToInt64(txtContratoPrincipalUnidadeId.Text);
                Unidade_C.TB020_RazaoSocial = txtContratoPrincipalRazaoSocial.Text;

                Unidade_C.TB020_NomeExibicaoDetalhes = txtTB020_NomeExibicaoDetalhes.Text;
                Unidade_C.TB020_CategoriaExibicao = txtTB020_CategoriaExibicao.Text;


                Unidade_C.TB020_NomeFantasia = txtContratoPrincipalNomeFantasia.Text;
                Unidade_C.TB020_Documento = mskContratoPrincipalDoc.Text;
                Unidade_C.TB006_id = Convert.ToInt64(cmbContatoPrincipalMunicipio.SelectedValue);
                Unidade_C.TB020_Cep = mskContratoPrincipalCEP.Text;
                Unidade_C.TB020_Logradouro = txtContratoPrincipalLogradouro.Text;
                Unidade_C.TB020_Numero = txtContratoPrincipalNumero.Text;
                Unidade_C.TB020_Bairro = txtContratoPrincipalBairro.Text;
                Unidade_C.TB020_Complemento = txtContratoPrincipalComplemento.Text;
                Unidade_C.TB020_TextoPortal = "";
                Unidade_C.TB020_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Unidade_C.TB020_StatusS = RetornarStatusUnidade(cmbContratoPrincipalStatusContrato.SelectedValue.ToString());
                ContratoCorporativo.Unidade = Unidade_C;


                PessoaController Titular_C = new PessoaController();
                Titular_C.Municipio = new MunicipioController();

                Titular_C.TB013_IdProtheus = txtContratoPrincipalTitularIdProtheus.Text;
                Titular_C.TB013_NomeExibicaoDetalhes = txtTB013_NomeExibicaoDetalhes.Text;


                Titular_C.TB013_TipoS = "1";
                Titular_C.TB013_CPFCNPJ = mskContratoPrincipalTitularCNPJ.Text;
                Titular_C.TB013_NomeCompleto = txtContratoPrincipalTitularNomeCompleto.Text;
                Titular_C.TB013_NomeExibicao = txtContratoPrincipalTitularNomeCompleto.Text;
                Titular_C.TB013_SexoS = cmbContratoPrincipalTitularSexo.SelectedValue.ToString();
                Titular_C.TB013_RG = txtContratoPrincipalTitularRG.Text;
                Titular_C.TB013_RGOrgaoEmissor = txtContratoPrincipalTitularRGOrgaoEmissor.Text;
                Titular_C.TB013_DataNascimento = dtpContratoPrincipalTitularCNPJDataNascimento.Value;
                Titular_C.TB004_Cep = mskContratoPrincipalTitularCEP.Text;
                Titular_C.Municipio.TB006_id = Convert.ToInt64(cmbContatoPrincipalTitularMunicipio.SelectedValue);
                Titular_C.TB013_Logradouro = txtContratoPrincipalTitularLogradouro.Text;
                Titular_C.TB013_Numero = txtContratoPrincipalTitularNumero.Text;
                Titular_C.TB013_Bairro = txtContratoPrincipalTitularBairro.Text;
                Titular_C.TB013_Complemento = txtContratoPrincipalTitularComplemento.Text;
                Titular_C.TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Titular_C.TB013_id = Convert.ToInt64(lblContratoPrincipalTitularId.Text);

                ContratoCorporativo.Titular = Titular_C;

                ContratoNegocios Contrato_N = new ContratoNegocios();
                if (Contrato_N.ContratoCorporativoUpdade(ContratoCorporativo, ParametrosInterface.objUsuarioLogado.TB011_Id))
                {
                    MessageBox.Show(MensagensDoSistema._0018, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RetornarStatusUnidade(string statusContrato)
        {
            if (statusContrato == "0")
                return "0";

            if (statusContrato == "2" || statusContrato == "3" || statusContrato == "5")
                return "2";

            return "1";
        }

        private void mnuContratoPrincipalFechar_Click(object sender, EventArgs e)
        {
            LimparContratoPrincipal();

            tabPrincipal.TabPages.Remove(tbContratoPrincipal);
            tabPrincipal.TabPages.Add(tbLista);
        }
        private void mnuColaboradoresFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbBeneficiados);
            tabPrincipal.TabPages.Add(tbContratoPrincipal);
        }

        private void mnuContratoPrincipalNovoColaborador_Click(object sender, EventArgs e)
        {
            UsuarioAPPNegocios Usuario_N = new UsuarioAPPNegocios();
            if (Usuario_N.VS() != Application.ProductVersion)
            {
                MessageBox.Show(string.Format(MensagensDoSistema._0051, Application.ProductVersion, ParametrosInterface.objUsuarioLogado.VS).ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {


                tabPrincipal.TabPages.Add(tbColaborador);

                lblColaboradorContratoPai.Text = lblContratoPrincipal.Text;
                lblColaboradorPontoDeVenda.Text = cmbContratoPrincipalPontoVenda.SelectedValue.ToString();
                lblColaboradorUnidadeId.Text = txtContratoPrincipalUnidadeId.Text;
                lblColaboradorUnidadeNomeFantasia.Text = txtContratoPrincipalNomeFantasia.Text;
                lblColaboradorUnidadeMunicipio.Text = cmbContatoPrincipalMunicipio.Text;
                lblColaboradorUnidadeBairro.Text = txtContratoPrincipalBairro.Text;
                lblColaboradorUnidadeEnd.Text = txtContratoPrincipalLogradouro.Text.TrimEnd() + " , N.º " + txtContratoPrincipalNumero.Text.TrimEnd();
                txtColaboradorNumero.Text = txtContratoPrincipalNumero.Text;

                EstadoController filtro = new EstadoController();
                filtro.TB005_Id = Convert.ToInt16(cmbContratoPrincipalEstado.SelectedValue);
                PopularColaboradorMunicipios(filtro);

                mskColaboradorCEP.Text = mskContratoPrincipalCEP.Text;

                mskColaboradorCPF.Enabled = true;
                tabPrincipal.TabPages.Remove(tbContratoPrincipal);
                mskColaboradorCPF.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbColaboradorEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbColaboradorEstado.SelectedValue) > 0)
                {
                    EstadoController filtro = new EstadoController();
                    filtro.TB005_Id = Convert.ToInt16(cmbColaboradorEstado.SelectedValue);
                    PopularColaboradorMunicipios(filtro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopularColaboradorMunicipios(EstadoController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbColaboradorMunicipio.DataSource = null;
            cmbColaboradorMunicipio.Items.Clear();
            try
            {
                cmbColaboradorMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbColaboradorMunicipio.DisplayMember = "TB006_Municipio";
                cmbColaboradorMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mskColaboradorCEP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mskColaboradorCEP.Text.Replace("-", "").Replace(" ", "").Length == 8)
                {

                    EnderecoNegocios Endereco_N = new EnderecoNegocios();
                    CEPController filtro = new CEPController();
                    filtro.TB004_Cep = Convert.ToInt32(mskColaboradorCEP.Text.Replace("-", "").Replace(" ", ""));
                    DataSet CEP = Endereco_N.Cep(filtro);

                    if (Convert.ToInt64(CEP.Tables[0].Rows[0]["TB004_id"].ToString()) > 0)
                    {
                        txtColaboradorLogradouro.Text = CEP.Tables[0].Rows[0]["TB004_Logradouro"].ToString();
                        txtColaboradorBairro.Text = CEP.Tables[0].Rows[0]["TB004_Bairro"].ToString();

                        cmbColaboradorEstado.SelectedValue = CEP.Tables[0].Rows[0]["TB005_Id"].ToString();
                        cmbColaboradorMunicipio.SelectedValue = CEP.Tables[0].Rows[0]["TB006_id"].ToString();

                        txtColaboradorNumero.Focus();
                    }
                    else
                    {
                        MessageBox.Show(MensagensDoSistema._0005, "Retorno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mskColaboradorCPF_Leave(object sender, EventArgs e)
        {
            if (lblColaboradorId.Text.Trim() == string.Empty)
            {
                if (mskColaboradorCPF.Text.Trim().Replace("-", "").Replace("/", "").Length > 11)
                {
                    /*Valida CPF*/
                    if (!Validacoes.CPF(mskColaboradorCPF.Text))
                    {
                        MessageBox.Show(MensagensDoSistema._0031.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mskColaboradorCPF.Focus();
                        return;
                    }
                    else
                    {


                        PessoaNegocios Pessoa_N = new PessoaNegocios();
                        PessoaController Pessoa = Pessoa_N.pessoaSelectCPFCNPJ(mskColaboradorCPF.Text.Trim().Replace("-", "").Replace("/", ""));


                        if (Pessoa.TB012_Id == Convert.ToInt64(lblContratoPrincipal.Text))
                        {

                            lblColaboradorId.Text = Pessoa.TB013_id.ToString();
                            //lblColaboradorContrato.Text = Pessoa.TB012_Id.ToString();
                            // lblColaboradorCartao.Text = Pessoa.TB013_Cartao;
                            txtColaboradorNomeCompleto.Text = Pessoa.TB013_NomeCompleto;
                            dtoColaboradorDataNascimento.Value = Pessoa.TB013_DataNascimento;
                            cmbColaboradorSexo.SelectedValue = Pessoa.TB013_SexoS;
                            mskColaboradorCEP.Text = Pessoa.TB004_Cep;
                            cmbColaboradorEstado.SelectedValue = Convert.ToInt64(Pessoa.Municipio.Estado.TB005_Id);
                            cmbColaboradorMunicipio.SelectedValue = Convert.ToInt64(Pessoa.Municipio.TB006_id);
                            txtColaboradorBairro.Text = Pessoa.TB013_Bairro;
                            txtColaboradorLogradouro.Text = Pessoa.TB013_Logradouro;
                            txtColaboradorNumero.Text = Pessoa.TB013_Numero;
                            txtColaboradorComplemento.Text = Pessoa.TB013_Complemento;

                            CarregarDependentes(Pessoa.TB012_Id, Pessoa.TB013_id);

                            RecuperarContatosColaborador(Pessoa.TB013_id);


                        }
                        else
                        {

                            if (Pessoa.TB013_id > 0)
                            {

                                if (Pessoa.TB012_Corporativo > 0)
                                {
                                    if (Pessoa.TB012_Corporativo == Convert.ToInt64(lblContratoPrincipal.Text))
                                    {
                                        MessageBox.Show(MensagensDoSistema._0065, "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format(MensagensDoSistema._0066, Pessoa.TB012_Corporativo.ToString(), "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information));
                                    }
                                }
                                else
                                {
                                    /*Verifica se existe parcelas em aberto*/

                                    try
                                    {
                                        ParcelaNegocios Parcela_N = new ParcelaNegocios();
                                        List<ParcelaController> ParcelaEmAberto = Parcela_N.ParcelasEmAbertoPorData(Pessoa.TB012_Id);

                                        if (ParcelaEmAberto.Count > 0)
                                        {

                                            StringBuilder Parcelas = new StringBuilder();

                                            for (int i = 0; i < ParcelaEmAberto.Count; i++)
                                            {
                                                Parcelas.Append("\n");
                                                Parcelas.Append("Parcela ");
                                                Parcelas.Append(ParcelaEmAberto[i].TB016_id);
                                                Parcelas.Append(", Vencimento em ");
                                                Parcelas.Append(ParcelaEmAberto[i].TB016_Vencimento.ToString("dd/MM/yyyy"));
                                                Parcelas.Append(", Valor ");
                                                Parcelas.Append(double.Parse(ParcelaEmAberto[i].TB016_Valor.ToString()).ToString("C2"));

                                            }

                                            mnuColaborador.Enabled = false;
                                            MessageBox.Show(string.Format(MensagensDoSistema._0067, Parcelas.ToString(), "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information));

                                            return;
                                        }
                                        else
                                        {
                                            if (MessageBox.Show(MensagensDoSistema._0068.ToString(), "Contrato", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {
                                                //Help.ShowHelp(this, "file://" + System.Environment.CurrentDirectory + "\\Gestor.chm", HelpNavigator.Topic, "Corporativo.htm");
                                                /*Se o contrato ja possui parcelas pendentes, cancelar as mesmas*/
                                                ContratoNegocios Contrato_N = new ContratoNegocios();
                                                //if(Contrato_N.ParcelasCancelar(Pessoa.TB012_Id, ParametrosInterface.objUsuarioLogado.TB011_Id))
                                                //{
                                                //    lblColaboradorId.Text = Pessoa.TB013_id.ToString();
                                                //    lblColaboradorContrato.Text = Pessoa.TB012_Id.ToString();
                                                //    lblColaboradorCartao.Text = Pessoa.TB013_Cartao;
                                                //    txtColaboradorNomeCompleto.Text = Pessoa.TB013_NomeCompleto;
                                                //    dtoColaboradorDataNascimento.Value = Pessoa.TB013_DataNascimento;
                                                //    cmbColaboradorSexo.SelectedValue = Pessoa.TB013_SexoS;
                                                //    mskColaboradorCEP.Text = Pessoa.TB004_Cep;
                                                //    cmbColaboradorEstado.SelectedValue = Convert.ToInt64(Pessoa.Municipio.Estado.TB005_Id);
                                                //    cmbColaboradorMunicipio.SelectedValue = Convert.ToInt64(Pessoa.Municipio.TB006_id);
                                                //    txtColaboradorBairro.Text = Pessoa.TB013_Bairro;
                                                //    txtColaboradorLogradouro.Text = Pessoa.TB013_Logradouro;
                                                //    txtColaboradorNumero.Text = Pessoa.TB013_Numero;
                                                //    txtColaboradorComplemento.Text = Pessoa.TB013_Complemento;

                                                //    CarregarDependentes(Pessoa.TB012_Id, Pessoa.TB013_id);

                                                //    RecuperarContatosColaborador(Pessoa.TB013_id);


                                                //    if (Pessoa_N.VincularFamiliarCorporativo(Convert.ToInt64(lblColaboradorContratoPai.Text), Pessoa.TB013_id, ParametrosInterface.objUsuarioLogado.TB011_Id))
                                                //    {
                                                //        MessageBox.Show(MensagensDoSistema._0017, "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    }

                                                //}
                                            }
                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                /*Caso não encontre este CPF*/
                                txtColaboradorMatricula.Focus();

                            }
                        }
                    }
                }
            }
        }

        private void mnuColaboradorSalvar_Click(object sender, EventArgs e)
        {

            PessoaNegocios Colaborador_N = new PessoaNegocios();
            ContratoNegocios Contrato_N = new ContratoNegocios();

            ContratosController contrato_R = new ContratosController();
            ContratosController contrato = new ContratosController();
            contrato.PontoDeVenda = new PontoDeVendaController();
            contrato.Titular = new PessoaController();


            PessoaController Colaborador_R = new PessoaController();
            PessoaController Colaborador = new PessoaController();
            Colaborador.Municipio = new MunicipioController();


            if (ValidarContratoColaborador())
            {


                Colaborador.TB013_CPFCNPJ = mskColaboradorCPF.Text;
                Colaborador.TB013_TipoS = "1";
                Colaborador.TB012_Corporativo = Convert.ToInt64(lblColaboradorContratoPai.Text);
                Colaborador.TB013_NomeCompleto = txtColaboradorNomeCompleto.Text;
                Colaborador.TB013_NomeExibicao = txtColaboradorNomeCompleto.Text;
                Colaborador.TB013_StatusS = cmbColaboradorStatus.SelectedValue.ToString();
                Colaborador.TB013_SexoS = cmbColaboradorSexo.SelectedValue.ToString();
                Colaborador.TB013_DataNascimento = dtoColaboradorDataNascimento.Value;
                Colaborador.TB004_Cep = mskColaboradorCEP.Text;
                Colaborador.Municipio.TB006_id = Convert.ToInt64(cmbColaboradorMunicipio.SelectedValue);
                Colaborador.TB013_Logradouro = txtColaboradorLogradouro.Text;
                Colaborador.TB013_Numero = txtColaboradorNumero.Text;
                Colaborador.TB013_Bairro = txtColaboradorBairro.Text;
                Colaborador.TB013_Complemento = txtColaboradorComplemento.Text;
                Colaborador.TB013_CadastradoEm = DateTime.Now;
                Colaborador.TB013_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Colaborador.TB013_AlteradoEm = DateTime.Now;
                Colaborador.TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Colaborador.TB013_Matricula = txtColaboradorMatricula.Text;
                Colaborador.TB013_CodigoDependente = 1001;
                Colaborador.TB013_RG = null;
                Colaborador.TB013_RGOrgaoEmissor = null;

                if (lblColaboradorId.Text.Trim() == string.Empty)
                {
                    /*Novo Registro Pessoa*/
                    Colaborador_R = Colaborador_N.ColaboradorInsert(Colaborador);




                    lblColaboradorId.Text = Colaborador_R.TB013_id.ToString();
                    contrato.Titular.TB013_id = Colaborador_R.TB013_id;
                    cmbColaboradorStatus.Enabled = false;
                    mskColaboradorCPF.Enabled = false;


                }
                else
                {
                    /*Atualizar*/
                    Colaborador.TB013_id = Convert.ToInt64(lblColaboradorId.Text);
                    if (Convert.ToInt64(lblContratoPrincipalTitularId.Text) != Convert.ToInt64(lblColaboradorId.Text))
                    {
                        if (Colaborador_N.ColaboradorUpdate(Colaborador, Convert.ToInt64(lblColaboradorContrato.Text)))
                        {
                            MessageBox.Show(MensagensDoSistema._0013, "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                /*Contrato Familiar Corporativo*/
                if (lblColaboradorContrato.Text.Trim() == string.Empty)
                {
                    /*Novo Contrato Colaborador*/

                    contrato.PontoDeVenda.TB002_id = Convert.ToInt64(cmbContratoPrincipalPontoVenda.SelectedValue);
                    contrato.TB012_Corporativo = Convert.ToInt64(lblContratoPrincipal.Text);
                    contrato.TB012_Inicio = DateTime.Now;
                    contrato.TB012_Fim = dtpContratoPrincipalFim.Value;
                    contrato.TB012_AceiteContrato = 1;
                    contrato.TB012_DataAceiteContrato = DateTime.Now;
                    contrato.TB012_CadastradorPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    contrato.TB012_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    contrato.TB012_StatusS = cmbColaboradorStatus.SelectedValue.ToString();
                    contrato.TB004_Cep = mskColaboradorCEP.Text;
                    contrato.TB006_id = Convert.ToInt64(cmbColaboradorMunicipio.SelectedValue);
                    contrato.TB012_Logradouro = txtColaboradorLogradouro.Text;
                    contrato.TB012_Numero = txtColaboradorNumero.Text;
                    contrato.TB012_Bairro = txtColaboradorBairro.Text;
                    contrato.TB012_Complemento = txtColaboradorComplemento.Text;
                    contrato.TB012_TipoContrato = 4;

                    Int64 Retorno = Contrato_N.ContratoColaboradorInsert(contrato);

                    lblColaboradorContrato.Text = Retorno.ToString();
                    if (Colaborador_N.VincularTitularContato(Retorno, Colaborador_R.TB013_id))/*Vincula Titular do Plano Familiar Corporativo*/
                    {
                        /*Gerar Cartão para o Colaborador*/
                        if (grpAcessoManutencao.Text.Trim() == string.Empty)
                        {
                            EnderecoNegocios Endereco_N = new EnderecoNegocios();
                            string Cartao = "1" + Endereco_N.DddMunicipio(Convert.ToInt64(cmbColaboradorMunicipio.SelectedValue)).ToString() + "-" + Retorno.ToString() + "-1001";

                            if (Colaborador_N.CartaoManual(Colaborador_R.TB013_id, Cartao, 1, ParametrosInterface.objUsuarioLogado.TB011_Id, Retorno))
                            {
                                grpAcessoManutencao.Text = Cartao;
                                lblStatusCarteirinha.Text = "Cartão (Gerado)";
                                /*Vincular Plano Familiar ao Plano Corporativo*/

                                if (Convert.ToInt64(lblContratoPrincipalTitularId.Text) == Convert.ToInt64(lblColaboradorId.Text))
                                {
                                    if (Colaborador_N.VincularFamiliarCorporativo(Convert.ToInt64(lblColaboradorContratoPai.Text), Convert.ToInt64(lblColaboradorId.Text), ParametrosInterface.objUsuarioLogado.TB011_Id))
                                    {

                                        ContratoNegocios Contato_N = new ContratoNegocios();
                                        Contato_N.ContratoCoprorativoAoTitularFamiliar(Retorno, Convert.ToInt64(lblColaboradorId.Text));
                                        MessageBox.Show(MensagensDoSistema._0017, "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    if (Colaborador_N.VincularFamiliarCorporativo(Convert.ToInt64(lblColaboradorContratoPai.Text), Colaborador_R.TB013_id, ParametrosInterface.objUsuarioLogado.TB011_Id))
                                    {
                                        MessageBox.Show(MensagensDoSistema._0017, "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Boolean ValidarContratoColaborador()
        {
            if (mskColaboradorCPF.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "").Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CPF Colaborador"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskColaboradorCPF.Focus();
                return false;
            }

            if (txtColaboradorMatricula.Text.Trim() == string.Empty)
            {
                txtColaboradorMatricula.Text = "0";
            }

            if (txtColaboradorNomeCompleto.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Nome Completo do Colaborador "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtColaboradorNomeCompleto.Focus();
                return false;
            }

            DateTime dttFromDate = DateTime.Now;
            DateTime dttToDate = Convert.ToDateTime(dtoColaboradorDataNascimento.Text);
            TimeSpan tsDuration;
            tsDuration = dttFromDate - dttToDate;

            if (Convert.ToInt32((tsDuration.Days) / 365) < 18)
            {
                MessageBox.Show(MensagensDoSistema._0006, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtoColaboradorDataNascimento.Focus();
                return false;
            }

            if (txtColaboradorBairro.Text.Trim().Replace("-", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Bairro "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtColaboradorBairro.Focus();
                return false;
            }

            if (txtColaboradorLogradouro.Text.Trim().Replace("-", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Logradouro do contrato principal "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtColaboradorLogradouro.Focus();
                return false;
            }

            if (txtColaboradorNumero.Text.Trim() == string.Empty)
            {
                txtColaboradorNumero.Text = "S/N";
            }


            if (txtColaboradorComplemento.Text.Trim() == string.Empty)
            {
                txtColaboradorComplemento.Text = ".";
            }

            if (mskColaboradorCEP.Text.Trim().Replace("-", "").Length < 8)
            {
                MessageBox.Show(MensagensDoSistema._0032.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskColaboradorCEP.Focus();
                return false;
            }

            return true;
        }

        private void ddgColaboradoresContratoPrincipal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UsuarioAPPNegocios Usuario_N = new UsuarioAPPNegocios();
            if (Usuario_N.VS() != Application.ProductVersion)
            {
                MessageBox.Show(string.Format(MensagensDoSistema._0051, Application.ProductVersion, ParametrosInterface.objUsuarioLogado.VS).ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgColaboradoresContratoPrincipal.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":

                            PessoaNegocios Colaborador_N = new PessoaNegocios();
                            PessoaController Retorno = new PessoaController();
                            Int64 Id = Convert.ToInt64(ddgColaboradoresContratoPrincipal.Rows[e.RowIndex].Cells["TB013_id"].Value);

                            if (Id == Convert.ToInt64(lblContratoPrincipalTitularId.Text))
                            {
                                Retorno = Colaborador_N.ColaboradorCorporativoTitular(Id);
                            }
                            else
                            {
                                Retorno = Colaborador_N.ColaboradorCorporativo(Id);
                            }

                            if (Retorno.TB013_id > 0)
                            {
                                tabPrincipal.TabPages.Add(tbColaborador);

                                lblColaboradorUnidadeId.Text = Retorno.Contrato.Unidade.TB020_id.ToString();
                                mskColaboradorCartao.Text = Retorno.TB013_Cartao;
                                lblColaboradorUnidadeNomeFantasia.Text = Retorno.Contrato.Unidade.TB020_NomeFantasia;
                                lblColaboradorUnidadeMunicipio.Text = Retorno.Contrato.Unidade.Municipio.TB006_Municipio;
                                lblColaboradorUnidadeBairro.Text = Retorno.Contrato.Unidade.TB020_Bairro;
                                lblColaboradorUnidadeEnd.Text = Retorno.Contrato.Unidade.TB020_Logradouro + " N.º " + Retorno.Contrato.Unidade.TB020_Numero;
                                lblColaboradorContratoPai.Text = Retorno.Contrato.TB012_Corporativo.ToString();
                                lblColaboradorPontoDeVenda.Text = Retorno.Contrato.PontoDeVenda.TB002_id.ToString();
                                lblColaboradorId.Text = Retorno.TB013_id.ToString();
                                lblColaboradorContrato.Text = Retorno.Contrato.TB012_Id.ToString();
                                mskColaboradorCPF.Text = Retorno.TB013_CPFCNPJ;
                                txtColaboradorMatricula.Text = Retorno.TB013_Matricula;
                                grpAcessoManutencao.Text = Retorno.TB013_Cartao;
                                lblStatusCarteirinha.Text = "Cartão (" + Retorno.TB013_CarteirinhaStatusS + ")";
                                txtColaboradorNomeCompleto.Text = Retorno.TB013_NomeCompleto;
                                dtoColaboradorDataNascimento.Value = Retorno.TB013_DataNascimento;
                                cmbColaboradorSexo.SelectedValue = Retorno.TB013_SexoS.ToString();
                                cmbColaboradorStatus.SelectedValue = Retorno.TB013_StatusS.ToString();
                                mskColaboradorCEP.Text = Retorno.TB004_Cep;
                                cmbColaboradorPais.SelectedValue = Convert.ToInt64(Retorno.Pais.TB003_id);

                                EstadoController filtro = new EstadoController();
                                filtro.TB005_Id = Convert.ToInt16(Retorno.Estado.TB005_Id);
                                PopularColaboradorMunicipios(filtro);

                                cmbColaboradorEstado.SelectedValue = Convert.ToInt64(Retorno.Estado.TB005_Id);
                                cmbColaboradorMunicipio.SelectedValue = Convert.ToInt64(Retorno.Municipio.TB006_id);
                                txtColaboradorBairro.Text = Retorno.TB013_Bairro;
                                txtColaboradorLogradouro.Text = Retorno.TB013_Logradouro;
                                txtColaboradorNumero.Text = Retorno.TB013_Numero;
                                txtColaboradorComplemento.Text = Retorno.TB013_Complemento;


                                RecuperarContatosColaborador(Retorno.TB013_id);


                                tabPrincipal.TabPages.Remove(tbContratoPrincipal);

                                txtContratoPrincipalNomeFantasia.Focus();

                                CarregarDependentes(Retorno.Contrato.TB012_Id, Retorno.TB013_id);
                            }
                            //
                            //
                            //mnuContratoPrincipalColaboradores.Enabled = true;
                            //PessoaNegocios Pessoa_N = new PessoaNegocios();


                            //if (!RecuperarContratoPrincipal(Convert.ToInt64(dgwContratos.Rows[e.RowIndex].Cells["LTB012_id"].Value), 1))
                            //{
                            //    tabPrincipal.TabPages.Add(tbLista);
                            //    tabPrincipal.TabPages.Remove(tbContratoPrincipal);
                            //    mnuContratoPrincipalColaboradores.Enabled = false;
                            //}

                            //ddgColaboradoresContratoPrincipal.AutoGenerateColumns = false;
                            //ddgColaboradoresContratoPrincipal.DataSource = Pessoa_N.ColaboradoresCorporativo(Convert.ToInt64(dgwContratos.Rows[e.RowIndex].Cells["LTB012_id"].Value));
                            //ddgColaboradoresContratoPrincipal.Refresh();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DTColaboradorContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ContatoController contato = new ContatoController();
                ContatoNegocios ContatoN = new ContatoNegocios();

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    contato.TB009_id = Convert.ToInt64(DTColaboradorContatos.Rows[e.RowIndex].Cells["txtColaboradorContatoId"].Value);

                    switch (DTColaboradorContatos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Excluir":




                            break;
                        case "Salvar":

                            if (lblContratoPrincipal.Text.Trim() == string.Empty)
                            {
                                SalvarContratoPrincipal();
                            }
                            PessoaController Pessoa_C = new PessoaController();
                            contato.Pessoa = Pessoa_C;
                            contato.Pessoa.TB013_id = 0;
                            contato.TB009_ExibirPortal = 0;
                            contato.Pessoa.TB013_id = Convert.ToInt64(lblColaboradorId.Text);
                            contato.TB009_TipoS = Convert.ToString(DTColaboradorContatos.Rows[e.RowIndex].Cells["cmbContratoColaboradorContatoTipo"].Value);
                            //contato.TB009_Contato = Convert.ToString(DTColaboradorContatos.Rows[e.RowIndex].Cells["txtColaboradorContato"].Value);


                            if (Convert.ToInt16(contato.TB009_TipoS) == 3)//Email
                            {
                                contato.TB009_Contato = Convert.ToString(DTColaboradorContatos.Rows[e.RowIndex].Cells["txtColaboradorContato"].Value);
                            }
                            else
                            {
                                String Contato = Convert.ToString(DTColaboradorContatos.Rows[e.RowIndex].Cells["txtColaboradorContato"].Value).ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');


                                if (Convert.ToInt16(contato.TB009_TipoS) < 4)
                                {
                                    if (Contato.Length == 10)
                                    {
                                        contato.TB009_Contato = Convert.ToUInt64(Contato).ToString(@"(00\)0000\-0000");
                                    }
                                    else
                                    {
                                        if (Contato.Length == 11)
                                        {
                                            contato.TB009_Contato = Convert.ToUInt64(Contato).ToString(@"(00\)000\-000\-000");
                                        }
                                    }

                                    DTColaboradorContatos.Rows[e.RowIndex].Cells[2].Value = contato.TB009_Contato;
                                }
                            }

                            /**/
                            contato.TB009_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            contato.TB009_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            contato.TB009_Nota = "Cadastrado via Gestor";

                            if (ValidaContato(contato))
                            {
                                if (contato.TB009_id > 0)
                                {
                                    ContatoN.contatosContratoUpdate(contato);
                                }
                                else
                                {
                                    DTColaboradorContatos.Rows[e.RowIndex].Cells[0].Value = ContatoN.contatosContratoInsert(contato, Convert.ToInt64(lblColaboradorContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);
                                }
                            }
                            break;
                    }
                }

                /*Recarregar Log*/
                //RecuperarLog(Convert.ToInt64(lblContrato.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecuperarContatosColaborador(long TB013_id)
        {
            try
            {
                ContatoNegocios Contato_N = new ContatoNegocios();
                List<ContatoController> Retorno = Contato_N.contatosDaPessoa(TB013_id);

                for (int i = 0; i < Retorno.Count; i++)
                {
                    DTColaboradorContatos.Rows.Add(new object[] { Retorno[i].TB009_id });
                    DTColaboradorContatos.Rows[i].Cells["cmbContratoColaboradorContatoTipo"].Value = Retorno[i].TB009_TipoS.ToString();
                    DTColaboradorContatos.Rows[i].Cells["txtColaboradorContato"].Value = Retorno[i].TB009_Contato;
                    //DTColaboradorContatos.Rows[i].Cells["chkContratoPrincipalContatoExibirPortal"].Value = Retorno[i].TB009_ExibirPortal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuColaboradorDependenteNovo_Click(object sender, EventArgs e)
        {
            lblDependenteId.Text = "";

            txtDependenteNomeCompleto.Text = "";

            txtDependenteNomeCompleto.Focus();
        }

        private void mnuColaboradorDependenteSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                PessoaNegocios Dependente_N = new PessoaNegocios();
                PessoaController Dependente_C = new PessoaController();
                Dependente_C.Municipio = new MunicipioController();

                Dependente_C.TB013_CPFCNPJ = "";
                Dependente_C.TB013_IdProtheus = "Id Protheus ";
                Dependente_C.TB013_TipoS = "1";
                Dependente_C.TB013_NomeCompleto = txtDependenteNomeCompleto.Text;
                Dependente_C.TB013_NomeExibicao = txtDependenteNomeCompleto.Text;
                Dependente_C.TB013_StatusS = cmbDependenteStatus.SelectedValue.ToString();
                Dependente_C.TB013_SexoS = cmbDependenteSexo.SelectedValue.ToString();
                Dependente_C.TB013_DataNascimento = ddpDependenteNascimento.Value;
                Dependente_C.TB004_Cep = mskColaboradorCEP.Text;
                Dependente_C.Municipio.TB006_id = Convert.ToInt64(cmbColaboradorMunicipio.SelectedValue);
                Dependente_C.TB013_Logradouro = txtColaboradorLogradouro.Text;
                Dependente_C.TB013_Numero = txtColaboradorNumero.Text;
                Dependente_C.TB013_Bairro = txtColaboradorBairro.Text;
                Dependente_C.TB013_Complemento = txtColaboradorComplemento.Text;
                Dependente_C.TB013_CadastradoEm = DateTime.Now;
                Dependente_C.TB013_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Dependente_C.TB013_AlteradoEm = DateTime.Now;
                Dependente_C.TB013_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;


                Dependente_C.TB013_NomeExibicaoDetalhes = txtDependenteNomeCompleto.Text;
                //Dependente_C.TB013_Matricula = txtColaboradorMatricula.Text;
                //
                Dependente_C.TB013_RG = null;
                Dependente_C.TB013_RGOrgaoEmissor = null;


                if (lblDependenteId.Text.Trim() == string.Empty)
                {
                    /*Novo Registro Pessoa*/
                    ContratoNegocios Contrato_N = new ContratoNegocios();
                    Dependente_C.TB013_CodigoDependente = Dependente_N.PessoaCodigoDependenteNovo(Convert.ToInt64(lblColaboradorContrato.Text));

                    string Cartao = grpAcessoManutencao.Text.Substring(0, 11) + Dependente_C.TB013_CodigoDependente;


                    PessoaController Retorno = Dependente_N.PessoaInsert(Dependente_C);
                    if (Retorno.TB013_id > 0)
                    {
                        Dependente_N.VincularDependenteContato(Convert.ToInt64(lblColaboradorContrato.Text), Retorno);
                        lblDependenteId.Text = Retorno.TB013_id.ToString();

                        if (Dependente_N.CartaoManual(Retorno.TB013_id, Cartao, 1, ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblColaboradorContrato.Text)))
                        {
                            Contrato_N.contratoCodDependente(Convert.ToInt64(lblColaboradorContrato.Text), Dependente_C.TB013_CodigoDependente);
                        }

                        MessageBox.Show(MensagensDoSistema._0017, "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    /*Atualizar*/
                    Dependente_C.TB013_id = Convert.ToInt64(lblDependenteId.Text);
                    if (Dependente_N.DependenteUpdate(Dependente_C, Convert.ToInt64(lblColaboradorContrato.Text)))
                    {
                        MessageBox.Show(MensagensDoSistema._0018, "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CarregarDependentes(Convert.ToInt64(lblColaboradorContrato.Text), Convert.ToInt64(lblColaboradorId.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarDependentes(Int64 TB012_id, Int64 TB013_id)
        {
            try
            {
                PessoaNegocios Dependente_N = new PessoaNegocios();

                ddgDependentes.AutoGenerateColumns = false;

                ddgDependentes.DataSource = null;
                ddgDependentes.DataSource = Dependente_N.DependentesSelect(TB012_id, TB013_id);
                ddgDependentes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ddgDependentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgDependentes.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":

                            PessoaNegocios Colaborador_N = new PessoaNegocios();

                            PessoaController Retorno = Colaborador_N.pessoaSelectId(Convert.ToInt64(ddgDependentes.Rows[e.RowIndex].Cells["CTB013_id"].Value));
                            if (Retorno.TB013_id > 0)
                            {
                                lblDependenteId.Text = Retorno.TB013_id.ToString();
                                txtDependenteNomeCompleto.Text = Retorno.TB013_NomeCompleto;
                                ddpDependenteNascimento.Value = Retorno.TB013_DataNascimento;
                                cmbDependenteSexo.SelectedValue = Retorno.TB013_SexoS.ToString();

                                cmbDependenteStatus.SelectedValue = Retorno.TB013_StatusS.ToString();

                            }


                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuContratoPrincipalParcelas_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbContratoPrincipal);
            lblParcelaContrato.Text = lblContratoPrincipal.Text;
            lblParcelaUnidade.Text = txtContratoPrincipalNomeFantasia.Text;


            ContratoNegocios Contrato_N = new ContratoNegocios();
            lblParcelaTotalContratosLigados.Text = Convert.ToInt64(Contrato_N.ContadorContratoCorporativoFamiliar(Convert.ToInt64(lblContratoPrincipal.Text))).ToString();


            lblParcelaDataInicio.Text = dtpContratoPrincipalInicio.Value.ToString("dd/MM/yyyy");
            lblParcelaDataFim.Text = dtpContratoPrincipalFim.Value.ToString("dd/MM/yyyy");
            lblParcelaCicloAtual.Text = Convert.ToInt64(Contrato_N.ContadorCicloAtual(Convert.ToInt64(lblContratoPrincipal.Text))).ToString();

            PlanoNegocios Plano_n = new PlanoNegocios();

            cmbParcelaPlanos.DataSource = null;
            cmbParcelaPlanos.Items.Clear();
            try
            {
                cmbParcelaPlanos.DataSource = Plano_n.PlanosCorporativo();
                cmbParcelaPlanos.DisplayMember = "TB015_Plano";
                cmbParcelaPlanos.ValueMember = "TB015_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            CarregarParcelasContrato();

            tabPrincipal.TabPages.Add(tbParcelas);



        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbParcelas);


            lblParcelaContrato.Text = "";
            lblParcelaUnidade.Text = "";
            lblParcelaTotalContratosLigados.Text = "";
            lblParcelaCicloAtual.Text = "";
            lblParcelaDataInicio.Text = "";
            lblParcelaDataFim.Text = "";
            lblParcelaId.Text = "";
            txtParcelaValorUnitario.Text = "R$ 0,00";


            ddgParcelas.DataSource = null;
            ddgParcelas.Refresh();

            ddtParcelaItens.DataSource = null;
            ddtParcelaItens.Refresh();

            tabPrincipal.TabPages.Add(tbContratoPrincipal);
        }

        private void PreencherCredencial()
        {
            mskCPF.Text = ParametrosInterface.objUsuarioLogado.TB011_CPF;
            txtSenha.Text = ParametrosInterface.objUsuarioLogado.TB011_Senha;
        }

        private void pcbLiberarCartaoTitular_Click(object sender, EventArgs e)
        {
            if (UsuarioPermissao == 0)
            {
                grpAcessoManutencao.Location = new System.Drawing.Point(480, 116);
                grpAcessoManutencao.Visible = true;
                Campo = 18;
                PreencherCredencial();
            }
            else
            {
                PessoaNegocios Pessoa_N = new PessoaNegocios();
                Pessoa_N.CartaoManual(Convert.ToInt64(lblColaboradorId.Text), mskColaboradorCartao.Text, 4, ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblColaboradorContrato.Text));


                mskColaboradorCartao.Enabled = false;
                pcbLiberarCartaoTitular.Image = Properties.Resources.Cadeado;
                grpAcessoManutencao.Visible = false;
                mskCPF.Text = "";
                txtSenha.Text = "";
                UsuarioPermissao = 0;
                Campo = 0;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioAPPNegocios UsuarioAPP_N = new UsuarioAPPNegocios();

                UsuarioPermissao = UsuarioAPP_N.VerificaPrivilarioAcaoPontual(Campo, mskCPF.Text, txtSenha.Text.TrimEnd());
                if (UsuarioPermissao > 0)
                {
                    if (Campo == 18)
                    {
                        this.pcbLiberarCartaoTitular.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
                        mskColaboradorCartao.Enabled = true;
                    }

                    if (Campo == 19)
                    {
                        this.pcbLiberarCartaoDependente.Image = global::ContezaAdmin.Properties.Resources.Confirmar;
                        mskDependenteCartao.Enabled = true;

                    }

                }

                Campo = 0;
                grpAcessoManutencao.Visible = false;
                mskCPF.Text = "";
                txtSenha.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcbLiberarCartaoDependente_Click(object sender, EventArgs e)
        {
            if (UsuarioPermissao == 0)
            {
                grpAcessoManutencao.Location = new System.Drawing.Point(590, 135);
                grpAcessoManutencao.Visible = true;
                Campo = 19;
                PreencherCredencial();
            }
            else
            {
                PessoaNegocios Pessoa_N = new PessoaNegocios();
                Pessoa_N.CartaoManual(Convert.ToInt64(lblDependenteId.Text), mskDependenteCartao.Text, 4, ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblColaboradorContrato.Text));


                mskDependenteCartao.Enabled = false;
                pcbLiberarCartaoDependente.Image = global::ContezaAdmin.Properties.Resources.Cadeado;
                grpAcessoManutencao.Visible = false;
                mskCPF.Text = "";
                txtSenha.Text = "";
                UsuarioPermissao = 0;
                Campo = 0;
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar o colaborador do contrato Corporativo?", "Corporativo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    ContratoNegocios Contrato_N = new ContratoNegocios();

                    ParcelaController UltimaParcelaContrato = new ParcelaNegocios().UltimaParcelaContratoCiclo(Convert.ToInt64(lblColaboradorContratoPai.Text), Convert.ToInt32(lblCicloAtual.Text));
                    double ValorUnitario = 0;

                    if (UltimaParcelaContrato.TB016_Parcela > 0)
                    {
                        ValorUnitario = new ParcelaNegocios().CorporativoValorUnitarioParcela(Convert.ToInt64(lblColaboradorContratoPai.Text));
                    }

                    if (Contrato_N.CorporativoCancelarColaborador(Convert.ToInt64(lblColaboradorContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id, ValorUnitario, Convert.ToInt64(lblColaboradorContratoPai.Text)))
                    {

                        MessageBox.Show("Colaborador Cancelado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        tabPrincipal.TabPages.Remove(tbColaborador);

                        PessoaNegocios Pessoa_N = new PessoaNegocios();

                        ddgColaboradoresContratoPrincipal.AutoGenerateColumns = false;
                        ddgColaboradoresContratoPrincipal.DataSource = Pessoa_N.ColaboradoresCorporativo(Convert.ToInt64(lblColaboradorContratoPai.Text));
                        ddgColaboradoresContratoPrincipal.Refresh();


                        lblColaboradorContratoPai.Text = "";
                        lblStatusCarteirinha.Text = "Cartão";


                        txtDependenteNomeCompleto.Text = "";

                        lblDependenteId.Text = "";
                        lblColaboradorUnidadeId.Text = "";
                        lblColaboradorPontoDeVenda.Text = "";
                        lblColaboradorUnidadeNomeFantasia.Text = "";
                        lblColaboradorUnidadeMunicipio.Text = "";
                        lblColaboradorUnidadeBairro.Text = "";
                        lblColaboradorUnidadeEnd.Text = "";
                        lblColaboradorId.Text = "";
                        lblColaboradorContrato.Text = "";
                        mskColaboradorCPF.Text = "";
                        txtColaboradorMatricula.Text = "";
                        grpAcessoManutencao.Text = "";
                        txtColaboradorNomeCompleto.Text = "";
                        mskColaboradorCEP.Text = "";
                        txtColaboradorBairro.Text = "";
                        txtColaboradorLogradouro.Text = "";
                        txtColaboradorNumero.Text = "";
                        txtColaboradorComplemento.Text = "";
                        ddgDependentes.DataSource = null;
                        ddgDependentes.Refresh();



                        tabPrincipal.TabPages.Add(tbContratoPrincipal);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbParcelaPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbParcelaPlanos.SelectedValue) > 0)
                {
                    PlanoNegocios Plano_N = new PlanoNegocios();
                    double ValorParcela = Plano_N.PlanoCorporativoValor(Convert.ToInt64(cmbParcelaPlanos.SelectedValue));
                    txtParcelaValorUnitario.Text = String.Format("{0:C2}", Convert.ToDouble(ValorParcela.ToString()));



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gerarParcelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmbParcelaFechamentoCobranca.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Dia de fechamento da Cobrança"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbParcelaFechamentoCobranca.Focus();
                return;
            }

            if (cmbParcelaVencimento.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Dia de vencimento"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbParcelaVencimento.Focus();
                return;
            }

            if (cmbParcelaPlanos.SelectedIndex == 0)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Plano"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbParcelaPlanos.Focus();
                return;
            }

            if (txtParcelaValorUnitario.Text.Trim() == string.Empty)
            {
                txtParcelaValorUnitario.Text = "R$ 0,00";
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Valor unitário"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaValorUnitario.Focus();
                return;
            }

            if (txtParcelaValorUnitario.Text.Trim() == "R$ 0,00")
            {
                txtParcelaValorUnitario.Text = "R$ 0,00";
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Valor unitário"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParcelaValorUnitario.Focus();
                return;
            }

            if (Convert.ToInt16(cmbParcelaFechamentoCobranca.SelectedItem) > Convert.ToInt16(ddtParcelaVencimento.Value.Day))
            {
                MessageBox.Show(MensagensDoSistema._0071, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lblParcelaId.Text.Trim() == string.Empty)
            {
                NovaParcela();
            }
            else
            {
                /*Salvar Parcela*/
            }

        }

        private void NovaParcela()
        {

            PontoDeVendaNegocios PontoDeVenda_N = new PontoDeVendaNegocios();
            EmpresaNegocios Empresa_N = new EmpresaNegocios();
            PlanoNegocios Plano_N = new PlanoNegocios();
            EnderecoNegocios Endereco_N = new EnderecoNegocios();

            int Matriz = 1;

            if (Convert.ToInt64(lblParcelaContrato.Text.Trim()) != Convert.ToInt64(lblContratoPrincipal.Text.Trim()))
            {
                Matriz = 0;
            }
            ContratosController Contrato = new ContratoNegocios().ContratoCorporativoSelect(Convert.ToInt64(lblParcelaContrato.Text), Matriz);
            EmpresaController Empresa = Empresa_N.Empresa(Convert.ToInt64(PontoDeVenda_N.PontoDeVendaEmpresa(Convert.ToInt64(Contrato.PontoDeVenda.TB002_id))));
            MunicipioController PagadorMunicipio = Endereco_N.Municipio(Contrato.Unidade.Municipio.TB006_id);
            EstadoController PagadorEstado = Endereco_N.Estado(Contrato.Unidade.Estado.TB005_Id);
            DataSet Plano = Plano_N.RecuperarPlano(Convert.ToInt64(cmbParcelaPlanos.SelectedValue));

            ParcelaController Parcela = new ParcelaController();
            Parcela.Empresa = new EmpresaController();
            Parcela.Pessoa = new PessoaController();


            Parcela.TB012_id = Convert.ToInt64(lblParcelaContrato.Text);

            Parcela.TB015_id = Convert.ToInt64(cmbParcelaPlanos.SelectedValue);


            ParcelaController UltimaParcelaContrato = new ParcelaNegocios().UltimaParcelaContratoCiclo(Parcela.TB012_id, Convert.ToInt32(lblParcelaCicloAtual.Text));



            Parcela.TB016_Parcela = UltimaParcelaContrato.TB016_Parcela + 1;


            Parcela.TB016_TotalParcelas = 12;


            Parcela.TB016_Emissao = DateTime.Now;

            if (UltimaParcelaContrato.TB016_Parcela > 0)
            {
                Parcela.TB016_Vencimento = UltimaParcelaContrato.TB016_Vencimento.AddMonths(+1);
                ddtParcelaVencimento.Value = Parcela.TB016_Vencimento;
            }
            else
            {
                Parcela.TB016_Vencimento = ddtParcelaVencimento.Value;
            }

            Parcela.TB016_Pagador = lblParcelaUnidade.Text.TrimEnd();
            Parcela.TB016_CPFCNPJ = Convert.ToUInt64(Contrato.Unidade.TB020_Documento.ToString().TrimEnd().TrimStart().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "")).ToString(@"00\.000\.000\/0000\-00");
            Parcela.TB016_EnderecoPagador = Contrato.Unidade.TB020_Logradouro.TrimEnd() + ",N.º " + Contrato.Unidade.TB020_Numero.TrimEnd();
            Parcela.TB016_TipoSacadoS = "2"; //Juridica.
            Parcela.TB016_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            Parcela.TB033_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            Parcela.TB016_FormaPagamentoS = "7";
            Parcela.TB016_EmitirBoleto = 0;
            Parcela.TB016_Entrada = 1;
            Parcela.TB016_ValorAdesao = Convert.ToDouble(Plano.Tables[0].Rows[0]["TB015_ValorAdesao"].ToString());
            Parcela.TB016_IOF = Convert.ToDouble(Plano.Tables[0].Rows[0]["TB015_IOF"]);
            Parcela.TB016_TipoVencimento = Convert.ToInt16(Plano.Tables[0].Rows[0]["TB015_TipoVencimento"]);
            Parcela.TB016_BoletoDesc1 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc1"].ToString().TrimEnd();
            Parcela.TB016_BoletoDesc2 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc2"].ToString().TrimEnd();
            Parcela.TB016_BoletoDesc3 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc3"].ToString().TrimEnd();
            Parcela.TB016_BoletoDesc4 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc4"].ToString().TrimEnd();
            Parcela.TB016_BoletoDesc5 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc5"].ToString().TrimEnd();
            Parcela.TB016_EspecieDocumento = Plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString().TrimEnd();
            Parcela.TB016_Beneficiario = Empresa.TB001_RazaoSocial.TrimEnd();
            Parcela.Empresa.TB001_id = Empresa.TB001_id;
            Parcela.TB016_BeneficiarioEndereco = Empresa.TB001_Logradouro.TrimEnd() + ", Nº " + Empresa.TB001_Numero.TrimEnd();
            Parcela.TB016_BeneficiarioCPFCNPJ = Convert.ToUInt64(Empresa.TB001_CNPJ.ToString().TrimEnd().TrimStart().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "")).ToString(@"00\.000\.000\/0000\-00");
            Parcela.TB016_BeneficiarioCidade = Empresa.Cidade.TrimEnd();
            Parcela.TB016_BeneficiarioUF = Empresa.UF.TrimEnd();
            Parcela.TB016_PagadorCEP = Contrato.Unidade.TB020_Cep;
            Parcela.Pessoa.TB013_id = Contrato.Titular.TB013_id;
            Parcela.TB016_PagadorCidade = PagadorMunicipio.TB006_Municipio.TrimEnd();
            Parcela.TB016_PagadorUF = PagadorEstado.TB005_Sigla;
            Parcela.TB016_StatusS = "0";
            Parcela.TB012_CicloContrato = Convert.ToInt32(lblParcelaCicloAtual.Text);
            Parcela.TB016_LoteExportacao = 0;
            Parcela.TB016_DiaFechamento = Convert.ToInt16(cmbParcelaFechamentoCobranca.Text);
            Parcela.TB016_DiaVencimento = Convert.ToInt16(cmbParcelaVencimento.Text);
            /*Carregar Produto*/
            List<ProdutoController> ProdutoPlano_L = new ProdutoNegocios().ProdutoPlano(Convert.ToInt64(Plano.Tables[0].Rows[0]["TB015_id"].ToString()));

            List<ParcelaProdutosController> ProdutosDaParcela = new List<ParcelaProdutosController>();

            foreach (ProdutoController Produto in ProdutoPlano_L)
            {
                ParcelaProdutosController Obj = new ParcelaProdutosController();
                Obj.TB017_IdProteus = Produto.TB014_IdProtheus;
                Obj.TB017_Item = Produto.TB014_Produto;
                Obj.TB017_ValorUnitario = Convert.ToDouble(txtParcelaValorUnitario.Text.Replace("R$", ""));
                Obj.TB017_ValorDesconto = 0;
                Obj.TB017_ValorFinal = Obj.TB017_ValorUnitario * Convert.ToInt64(lblParcelaTotalContratosLigados.Text);
                ProdutosDaParcela.Add(Obj);
            }


            Parcela.TB016_Valor = ProdutosDaParcela[0].TB017_ValorFinal;
            Parcela.ParcelaProduto_L = ProdutosDaParcela;
            /*Cadastro Parcela*/
            long ParcelaNova = new ParcelaNegocios().CorporativoParcelaInsert(Parcela);

            /**/
            List<ParcelaProdutosController> ProdutoAdesao_l = new List<ParcelaProdutosController>();
            PessoaNegocios Adesao_N = new PessoaNegocios();
            List<PessoaController> Adesao_L = Adesao_N.CorporativoPessoasNaoAtivadas(Convert.ToInt64(lblParcelaContrato.Text));
            double ValorTotalAdesao = 0;
            foreach (PessoaController Adesao in Adesao_L)
            {
                ParcelaProdutosController Obj = new ParcelaProdutosController();
                Obj.TB017_IdProteus = "-";
                Obj.TB017_Item = "Adesão de " + txtAdesaoPadrao.Text.TrimStart().TrimEnd() + " referente a " + Adesao.TB013_NomeCompleto.TrimEnd().TrimStart() + ", do contrato " + Adesao.TB012_Id.ToString();
                Obj.TB017_ValorUnitario = Convert.ToDouble(txtAdesaoPadrao.Text.Replace("R$", ""));
                Obj.TB017_ValorDesconto = 0;
                Obj.TB017_ValorFinal = Obj.TB017_ValorUnitario;
                ValorTotalAdesao = ValorTotalAdesao + Obj.TB017_ValorFinal;
                ProdutoAdesao_l.Add(Obj);
            }

            ParcelaNegocios Parcela_N = new ParcelaNegocios();


            Parcela_N.CorporativoAdesaoParcelaInsert(ProdutoAdesao_l, ValorTotalAdesao, ParcelaNova, Convert.ToInt64(lblParcelaContrato.Text));

            /**/

            CarregarParcelasContrato();

        }

        private void CarregarParcelasContrato()
        {

            ddgParcelas.AutoGenerateColumns = false;
            ddgParcelas.DataSource = new ParcelaNegocios().ParcelasContratoExistente(Convert.ToInt64(lblParcelaContrato.Text));
            ddgParcelas.Refresh();

            ParcelaController UltimaParcelaContrato = new ParcelaNegocios().UltimaParcelaContratoCiclo(Convert.ToInt64(lblParcelaContrato.Text), Convert.ToInt32(lblParcelaCicloAtual.Text));

            if (UltimaParcelaContrato.TB016_DiaFechamento > 0)
            {
                cmbParcelaFechamentoCobranca.Text = UltimaParcelaContrato.TB016_DiaFechamento.ToString();
                cmbParcelaVencimento.Text = UltimaParcelaContrato.TB016_DiaVencimento.ToString();
            }
        }

        private void ddgParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgParcelas.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Transação":

                            ProdutoNegocios Produto_N = new ProdutoNegocios();

                            ddtParcelaItens.AutoGenerateColumns = false;
                            ddtParcelaItens.DataSource = Produto_N.ProdutosParcelaID(Convert.ToInt64(ddgParcelas.Rows[e.RowIndex].Cells["TB016_id"].Value));
                            ddtParcelaItens.Refresh();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
