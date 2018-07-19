using Controller;
using Microsoft.Reporting.WinForms;
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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.String; 
using Word = Microsoft.Office.Interop.Word;

namespace ContezaAdmin.Atendimento
{
    public partial class frmParceiros : Form
    {

        #region "Declaration"
            List<ParcelaController> Parcelas = new List<ParcelaController>();
            List<ParcelaController> BoletosEmitidos = new List<ParcelaController>();
            List<ParcelaController> ParcelasContrato = new List<ParcelaController>();
            HtmlDocument doc;
            int _tb012Status;
            long _tb002Id;
            int _tb016DiaVencimento;
            HtmlDocument _doc;
            Util _validacoes = new Util();
            int TB013_Tipo;
            private string currentFile;
            private long tamanhoArquivoImagem = 0;
            private byte[] vetorImagens;
        #endregion

        public frmParceiros()
        {
            InitializeComponent();
        }

        

        private void frmParceiros_Load(object sender, EventArgs e)
        {

            if(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id==1)
            {
                lblAcessoEmailId.Visible    = true;
                label72.Visible             = true;
                lblAcessoId.Visible         = true;
                label66.Visible             = true;
            }
            this.StartPosition = FormStartPosition.CenterScreen;

            tabPrincipal.TabPages.Remove(tbContrato);
            tabPrincipal.TabPages.Remove(tbUnidades);
            tabPrincipal.TabPages.Remove(tbpParcelas);
            tabPrincipal.TabPages.Remove(tbBoletos);
            tabPrincipal.TabPages.Remove(tbpParcelas);
            tabPrincipal.TabPages.Remove(tbDescontoUnidade);
            tabPrincipal.TabPages.Remove(tbAcessos);
            tabPrincipal.TabPages.Remove(tpPagamento);
            tabPrincipal.TabPages.Remove(tbpDocumentos);
            tabPrincipal.TabPages.Remove(tbCarne);

            FormasDePagamento();
            CarregarSexo();
            CarregarTipoPessoa();
            CarregarPaises();
            PopularTipoContatos();
            CarregarStatusParcela();
            StatusDeContrato();
            StatusUnidade();
            CarregarCategoriaNivel1();

            currentFile = "";

            PaisController filtro = new PaisController();
            filtro.TB003_id = 1058;
            PopularEstadosContrato(filtro);
            PopularEstadosTitular(filtro);

            string vQuery = "";

            CarregarContratos(ListarContratos(vQuery));

            if(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id==1)
            {
                lblUnidadeMatrizId.Visible = true;
                lblTitularTB013id.Visible = true;
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
        private void CarregarCategoriaNivel1()
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                cmbContratoCategoriaNivel1.DataSource = null;
                cmbContratoCategoriaNivel1.Items.Clear();
                cmbContratoCategoriaNivel1.DataSource = Categoria_N.RetoranarTodasAsCategorias();
                cmbContratoCategoriaNivel1.DisplayMember = "TB021_Descricao";
                cmbContratoCategoriaNivel1.ValueMember = "TB021_id";

                cmbContratoCategoriaNivel1.SelectedValue = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarCategoriaNivel1DoContrato(long TB012_id)
        {
            try
            {
                var Categoria_N = new CategoriaNegocios();

                List<CategoriaController> Retorno = Categoria_N.RetoranarcCategoriaNivel1DoContrato(TB012_id);

                lsbNivel1.DataSource = Retorno;
                lsbNivel1.DisplayMember = "TB021_Descricao";
                lsbNivel1.ValueMember = "TB021_id";

                var categoriaNivel1 = Retorno.FirstOrDefault();
                cmbContratoCategoriaNivel1.SelectedValue = categoriaNivel1 != null ? categoriaNivel1.TB021_id : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PreencherNivel2(Int64 TB021_id)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();
                cmbNivel2.DataSource = null;
                cmbNivel2.Items.Clear();
                cmbNivel2.DataSource = Categoria_N.RetoranarcCategoriaNivel2(Convert.ToInt64(TB021_id));
                cmbNivel2.DisplayMember = "TB022_Descricao";
                cmbNivel2.ValueMember = "TB022_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarNivel2DoContrato(long TB021_id, long TB012_id)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                List<CategoriaController> Retorno = Categoria_N.RetoranarcCategoriaNivel2DoContrato(TB021_id, TB012_id);

                lsbNivel2.DataSource = Categoria_N.RetoranarcCategoriaNivel2DoContrato(TB021_id, TB012_id);
                lsbNivel2.DisplayMember = "TB022_Descricao";
                lsbNivel2.ValueMember = "TB022_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PreencherNivel3(Int64 TB022_id)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                cmbNivel3.DataSource = null;
                cmbNivel3.Items.Clear();
                cmbNivel3.DataSource = Categoria_N.RetoranarcCategoriaNivel3(Convert.ToInt64(TB022_id));
                cmbNivel3.DisplayMember = "TB023_Descricao";
                cmbNivel3.ValueMember = "TB023_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarNivel3DoContrato(Int64 TB022_id, Int64 TB012_id)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                lsbNivel3.DataSource = Categoria_N.RetoranarcCategoriaNivel3DoContrato(TB022_id, TB012_id);
                lsbNivel3.DisplayMember = "TB023_Descricao";
                lsbNivel3.ValueMember = "TB023_id";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void CarregarContratos(List<ContratosController> listarContratos)
        {
            ddgContratos.AutoGenerateColumns = false;
            ddgContratos.DataSource = null;
            ddgContratos.DataSource = listarContratos;
            ddgContratos.Refresh();

            Int64 contrato = 0;
            int contratos = 0;
            for (var i = listarContratos.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt64(listarContratos[i].TB012_Id) == contrato) continue;
                contratos++;
                contrato = Convert.ToInt64(listarContratos[i].TB012_Id);
            }
        }

        public List<ContratosController> ListarContratos(string query)
        {
            List<ContratosController> contratoL = new List<ContratosController>();
            try
            {
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" and TB011_TB002.TB011_Id = ");
                sSql.Append(ParametrosInterface.objUsuarioLogado.TB011_Id);
                contratoL = new ContratoNegocios().ContratoParceirosSelect(sSql + query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return contratoL;
        }
        private void CarregarSexo()
        {
            cmbTitularTB013Sexo.DataSource = null;
            cmbTitularTB013Sexo.Items.Clear();

            List<KeyValuePair<string, string>> lstSexo = new List<KeyValuePair<string, string>>();
            Array SexoS = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE Sexo in SexoS)
            {
                lstSexo.Add(new KeyValuePair<string, string>(Sexo.ToString(), ((int)Sexo).ToString()));
            }

            cmbTitularTB013Sexo.DataSource = lstSexo;
            cmbTitularTB013Sexo.DisplayMember = "Key";
            cmbTitularTB013Sexo.ValueMember = "Value";
            cmbAcessoSexo.DataSource = null;
            cmbAcessoSexo.Items.Clear();

            List<KeyValuePair<string, string>> lstSexoAcesso = new List<KeyValuePair<string, string>>();
            Array SexoSAcesso = Enum.GetValues(typeof(PessoaController.TB013_SexoE));
            foreach (PessoaController.TB013_SexoE Sexo in SexoSAcesso)
            {
                lstSexoAcesso.Add(new KeyValuePair<string, string>(Sexo.ToString(), ((int)Sexo).ToString()));
            }

            cmbAcessoSexo.DataSource = lstSexoAcesso;
            cmbAcessoSexo.DisplayMember = "Key";
            cmbAcessoSexo.ValueMember = "Value";
        }
        private void CarregarTipoPessoa()
        {
            cmbContratoTB013Tipo.DataSource = null;
            cmbContratoTB013Tipo.Items.Clear();

            List<KeyValuePair<string, string>> TipoPessoa_L = new List<KeyValuePair<string, string>>();
            Array TipoS = Enum.GetValues(typeof(PessoaController.TB013_TipoE));
            foreach (PessoaController.TB013_TipoE Tipo in TipoS)
            {
                TipoPessoa_L.Add(new KeyValuePair<string, string>(Tipo.ToString(), ((int)Tipo).ToString()));
            }

            cmbContratoTB013Tipo.DataSource = TipoPessoa_L;
            cmbContratoTB013Tipo.DisplayMember = "Key";
            cmbContratoTB013Tipo.ValueMember = "Value";
        }
        private void CarregarTipoPessoaUnidade()
        {
            cmbUnidadeTipoPessoa.DataSource = null;
            cmbUnidadeTipoPessoa.Items.Clear();

            List<KeyValuePair<string, string>> TipoPessoa_L = new List<KeyValuePair<string, string>>();
            Array TipoS = Enum.GetValues(typeof(PessoaController.TB013_TipoE));
            foreach (PessoaController.TB013_TipoE Tipo in TipoS)
            {
                TipoPessoa_L.Add(new KeyValuePair<string, string>(Tipo.ToString(), ((int)Tipo).ToString()));
            }

            cmbUnidadeTipoPessoa.DataSource = TipoPessoa_L;
            cmbUnidadeTipoPessoa.DisplayMember = "Key";
            cmbUnidadeTipoPessoa.ValueMember = "Value";
        }
        private void CarregarPaises()
        {
            try
            {
                EnderecoNegocios EnderecoN = new EnderecoNegocios();

                cmbContratoPais.DataSource = null;
                cmbContratoPais.Items.Clear();

                cmbContratoPais.DataSource = EnderecoN.PaisController().Tables[0];
                cmbContratoPais.DisplayMember = "TB003_Pais";
                cmbContratoPais.ValueMember = "TB003_id";

                cmbTitularPais.DataSource = null;
                cmbTitularPais.Items.Clear();

                cmbTitularPais.DataSource = EnderecoN.PaisController().Tables[0];
                cmbTitularPais.DisplayMember = "TB003_Pais";
                cmbTitularPais.ValueMember = "TB003_id";

                cmbUnidadePais.DataSource = null;
                cmbUnidadePais.Items.Clear();

                cmbUnidadePais.DataSource = EnderecoN.PaisController().Tables[0];
                cmbUnidadePais.DisplayMember = "TB003_Pais";
                cmbUnidadePais.ValueMember = "TB003_id";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularTipoContatos()
        {
            cmbContratoUnidadeContatoTipo.DataSource = null;
            cmbContratoUnidadeContatoTipo.Items.Clear();

            List<KeyValuePair<string, string>> ContatoTipo_L = new List<KeyValuePair<string, string>>();
            Array Contatos = Enum.GetValues(typeof(ContatoController.TB009_TipoE));
            foreach (ContatoController.TB009_TipoE Contato in Contatos)
            {
                ContatoTipo_L.Add(new KeyValuePair<string, string>(Contato.ToString(), ((int)Contato).ToString()));
            }

            cmbContratoUnidadeContatoTipo.DataSource = ContatoTipo_L;
            cmbContratoUnidadeContatoTipo.DisplayMember = "Key";
            cmbContratoUnidadeContatoTipo.ValueMember = "Value";
        }
        private void StatusDeContrato()
        {
            cmbContratoStatus.DataSource = null;
            cmbContratoStatus.Items.Clear();

            List<KeyValuePair<string, string>> ContratoStatus = new List<KeyValuePair<string, string>>();
            Array Status = Enum.GetValues(typeof(ContratosController.TB012_StatusE));
            foreach (ContratosController.TB012_StatusE Statu in Status)
            {
                ContratoStatus.Add(new KeyValuePair<string, string>(Statu.ToString(), ((int)Statu).ToString()));
            }

            cmbContratoStatus.DataSource = ContratoStatus;
            cmbContratoStatus.DisplayMember = "Key";
            cmbContratoStatus.ValueMember = "Value";
        }
        private void StatusUnidade()
        {
            cmbUnidadeStatus.DataSource = null;
            cmbUnidadeStatus.Items.Clear();

            List<KeyValuePair<string, string>> UnidadeStatus = new List<KeyValuePair<string, string>>();
            Array Unidades = Enum.GetValues(typeof(UnidadeController.TB020_StatusE));
            foreach (UnidadeController.TB020_StatusE Unidade in Unidades)
            {
                UnidadeStatus.Add(new KeyValuePair<string, string>(Unidade.ToString(), ((int)Unidade).ToString()));
            }

            cmbUnidadeStatus.DataSource = UnidadeStatus;
            cmbUnidadeStatus.DisplayMember = "Key";
            cmbUnidadeStatus.ValueMember = "Value";
        }
        private void PontosDeVenda()
        {
            try
            {
                PontoDeVendaNegocios PontoDeVendaN = new PontoDeVendaNegocios();

                cmbContratoPontosDeVenda.DataSource = null;
                cmbContratoPontosDeVenda.Items.Clear();

                cmbContratoPontosDeVenda.DataSource = PontoDeVendaN.PontosDeVendaLiberadosParaUsuario(ParametrosInterface.objUsuarioLogado).Tables[0];
                cmbContratoPontosDeVenda.DisplayMember = "TB002_Ponto";
                cmbContratoPontosDeVenda.ValueMember = "TB002_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimparDadosContrato()
        {
            txtCategoriaExibicao.Text = "";
            lblContratoPrincipalImgAlterado.Text = "";
            lblCarteirinha.Text = "";
            lblUnidadeMatrizId.Text = "";
            lblContrato.Text = "";
            txtContatoTB013NomeCompleto.Text = "";
            txtContratoTB013NomeExibicao.Text = "";
            dtContratoInicio.Value = DateTime.Now;
            dtpContratoTB012Fim.Value = DateTime.Now;
            mskContratoTB013_CPFCNPJ.Text = "";
            txtContatoTB013NomeCompleto.Text = "";
            txtContratoTB013NomeExibicao.Text = "";
            txtContratoTB013Logradouro.Text = "";
            txtContratoTB013Bairro.Text = "";
            txtContratoTB013Complemento.Text = "";
            mskTitularTB004Cep.Text = "";
            txtTitularEndereco.Text = "";
            txtTitularNumero.Text = "";
            txtTitularComplemento.Text = "";
            txtTitularTB013_NomeCompleto.Text = "";
            //txtTitularTB013_NomeExibicao.Text       = "";
            lblTitularTB013id.Text = "";
            //txtTitularTB013_IdProtheus.Text         = "";
            txtTitularTB013_RG.Text = "";
            txtTitularTB013_RGOrgaoEmissor.Text = "";
            mskContratoCEP.Text = "";
            txtContratoTB013Numero.Text = "";
            mskTitularTB013_CPFCNPJ.Text = "";
            txtTitularBairro.Text = "";
            txtContratoTB013NomeExibicaoDetalhes.Text = "";
            mnuContratoUnidades.Enabled = false;
            //mnuContratoParcela.Enabled = false;
            mnuContratoUnidades.Enabled = false;
            //btnImagemContrato.Visible               = false;
            cmbContratoTB013Tipo.Enabled = true;
            mskContratoTB013_CPFCNPJ.Enabled = true;
            cmbContratoStatus.Enabled = true;
            cmbContratoPais.Enabled = true;
            mskContratoCEP.Enabled = true;
            cmbContratoEstado.Enabled = true;
            cmbContatoMunicipio.Enabled = true;
            txtContratoTB013Logradouro.Enabled = true;
            txtContratoTB013Numero.Enabled = true;
            txtContratoTB013Bairro.Enabled = true;
            txtContratoTB013Complemento.Enabled = true;
            dtContratoInicio.Enabled = true;
            dtpContratoTB012Fim.Enabled = true;
            //DTContratoUnidadeContatos.DataSource = null;
            DTContratoUnidadeContatos.Refresh();
            DTContratoUnidadeContatos.Rows.Clear();
            txtContratoTB020_TextoPortal.Text = "";
            pctLogoContrato.Image = null;
        }
        private void mnuContatoFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbLista);
            LimparDadosContrato();
            tabPrincipal.TabPages.Remove(tbContrato);

            string tipoCampo = @"Exibição";

            if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
            {
                tipoCampo = @"Exibição";
            }
            else
            {
                if (this._validacoes.contemNumeros(txtFiltroAssociado.Text.Trim()))
                {
                    if (txtFiltroAssociado.Text.Trim().Length == 6)
                    {
                        tipoCampo = @"Contrato";
                    }
                    else
                    {
                        tipoCampo = @"Documento";
                    }
                }
            }

            switch (tipoCampo)
            {
                case @"Exibição":
                    {
                        string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                        txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                        CarregarContratos(ListarContratos(vQuery));
                        break;
                    }
                case @"Contrato":
                    {
                        string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                        txtFiltroAssociado.Text.TrimEnd().TrimStart();
                        CarregarContratos(ListarContratos(vQuery));
                        break;
                    }
                case @"Documento":
                    {
                        string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                            .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                            .Replace("-", "").Replace("/", "") + "'";
                        CarregarContratos(ListarContratos(vQuery));
                        break;
                    }
            }
        }
        private void mnuListaNovo_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbContrato);
            //LimparDadosContrato();
            PontosDeVenda();


            DateTime Hoje = DateTime.Now;
            dtContratoInicio.Enabled = true;
            dtContratoInicio.Value = Hoje;
            dtpContratoTB012Fim.Value = Hoje.AddMonths(12);

            cmbContratoStatus.SelectedValue = "0";
            cmbContratoEstado.SelectedValue = ParametrosInterface.objUsuarioLogado.Estado.TB005_Id;
            cmbTitularEstado.SelectedValue = ParametrosInterface.objUsuarioLogado.Estado.TB005_Id;

            EstadoController filtro = new EstadoController();
            filtro.TB005_Id = Convert.ToInt16(ParametrosInterface.objUsuarioLogado.Estado.TB005_Id);
            PopularMunicipiosContrato(filtro);
            PopularMunicipiosTitular(filtro);

            cmbContatoMunicipio.SelectedValue = ParametrosInterface.objUsuarioLogado.Municipio.TB006_id;
            cmbTitularMunicipio.SelectedValue = ParametrosInterface.objUsuarioLogado.Municipio.TB006_id;
            //btnImagemContrato.Visible = true;



            tabPrincipal.TabPages.Remove(tbLista);

            mskContratoTB013_CPFCNPJ.Focus();
        }
        private void mnuContatoSalvar_Click(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() == string.Empty)
            {
                SalvarContrato();
            }
            else
            {
                AtualizarContrato();
            }

        }
        private void SalvarContrato()
        {
            if (ValidarContrato())
            {
                try
                {

                    ContratoNegocios Contrato_N = new ContratoNegocios();
                    PessoaNegocios Pessoa_N = new PessoaNegocios();
                    ContratosController Contrato_C = new ContratosController();
                    PessoaController Titular_C = new PessoaController();

                    MunicipioController ComercialMunicipio_C = new MunicipioController();

                    if (ValidarContrato())//Validar Informações Necessarias para Contrato
                    {
                        PontoDeVendaController PontoDeVenda_C   = new PontoDeVendaController();

                        PontoDeVenda_C.TB002_id                 = Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue);
                        Contrato_C.TB012_DiaVencimento          = Convert.ToInt16(cmbDiaVencimento.Text);
                        Contrato_C.TB012_CicloContrato          = dtContratoInicio.Value.Month.ToString() + dtContratoInicio.Value.Year.ToString();
                        Contrato_C.PontoDeVenda                 = PontoDeVenda_C;
                        Contrato_C.TB012_Inicio                 = dtContratoInicio.Value;
                        Contrato_C.TB012_Fim                    = dtpContratoTB012Fim.Value;
                        Contrato_C.TB012_StatusS                = cmbContratoStatus.SelectedValue.ToString();
                        Contrato_C.TB006_id                     = Convert.ToInt64(cmbContatoMunicipio.SelectedValue);
                        Contrato_C.TB012_Logradouro             = txtContratoTB013Logradouro.Text;
                        Contrato_C.TB012_Numero                 = txtContratoTB013Numero.Text;
                        Contrato_C.TB012_Bairro                 = txtContratoTB013Bairro.Text;
                        Contrato_C.TB012_Complemento            = txtContratoTB013Complemento.Text;
                        Contrato_C.TB012_AlteradoPor            = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        Contrato_C.TB004_Cep                    = mskContratoCEP.Text.Replace("-", "");

                        /*Dados do Titular Inicio*/
                        Titular_C.TB013_CPFCNPJ                 = mskTitularTB013_CPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                        Titular_C.TB013_RG                      = txtTitularTB013_RG.Text;
                        Titular_C.TB013_RGOrgaoEmissor          = txtTitularTB013_RGOrgaoEmissor.Text;
                        Titular_C.TB013_NomeCompleto            = txtTitularTB013_NomeCompleto.Text;
                        Titular_C.TB013_NomeExibicao            = txtContratoTB013NomeExibicaoDetalhes.Text;
                        Titular_C.TB013_NomeExibicaoDetalhes    = txtContratoTB013NomeExibicaoDetalhes.Text;
                        Titular_C.TB013_SexoS                   = cmbTitularTB013Sexo.SelectedValue.ToString();
                        Titular_C.TB013_DataNascimento          = Convert.ToDateTime(dtpTitularTB013_DataNascimento.Value);
                        Titular_C.TB004_Cep                     = mskTitularTB004Cep.Text.Replace("-", "");
                        Titular_C.TB013_StatusS                 = "1";
                        Titular_C.TB013_MaeNome                 = @"NÃO INFORMADO";
                        Titular_C.TB013_PaiNome                 = @"NÃO INFORMADO";
                        Titular_C.TB013_MaeDataNascimento       = dtpTitularTB013_DataNascimento.Value.AddYears(-18);
                        Titular_C.TB013_PaiDataNascimento       = dtpTitularTB013_DataNascimento.Value.AddYears(-18);
                       // Titular_C.TB012_Parceiro                = Convert.ToInt64(lblContrato.Text);

                        MunicipioController MunicipioTitular_C = new MunicipioController();
                        Titular_C.Municipio                     = MunicipioTitular_C;
                        Titular_C.Municipio.TB006_id            = Convert.ToInt64(cmbTitularMunicipio.SelectedValue);
                        Titular_C.TB013_Logradouro              = txtTitularEndereco.Text;
                        Titular_C.TB013_Numero                  = txtTitularNumero.Text;
                        Titular_C.TB013_Bairro                  = txtTitularBairro.Text;
                        Titular_C.TB013_Complemento             = txtTitularComplemento.Text;
                        Titular_C.TB013_AlteradoPor             = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        Titular_C.TB013_AlteradoEm              = DateTime.Now;
                        Titular_C.TB013_CodigoDependente        = 1001;
                        /*Dados do Titular Fim*/
                        /*#######################################################*/
                        //#1 - Cadastro Titular
                        if (lblTitularTB013id.Text.Trim() == string.Empty)
                        {//Titular Novo
                            Titular_C.TB013_TipoS = "1";
                            Titular_C.TB013_IdProtheus = "IdProtheus";
                            Titular_C.TB013_StatusS = "0";

                            Titular_C.TB013_CadastradoEm = DateTime.Now;
                            Titular_C.TB013_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;

                            Titular_C = Pessoa_N.PessoaInsert(Titular_C);
                            lblTitularTB013id.Text = Titular_C.TB013_id.ToString();

                            if (lblContrato.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").TrimStart('0') == mskTitularTB013_CPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").TrimStart('0'))
                            {
                                lblTitularTB013id.Text = Titular_C.TB013_id.ToString();
                            }
                        }
                        else
                        {//Atualizar Titular
                            Titular_C.TB013_id = Convert.ToInt64(lblTitularTB013id.Text);
                        }
                        ///*#######################################################*/

                        /*#######################################################*/
                        //#2 - Cadastro Contrato
                        Contrato_C.Titular = Titular_C;
                        if (lblContrato.Text.Trim() == string.Empty)
                        {
                            Contrato_C.TB012_AceiteContrato = 1;
                            Contrato_C.TB012_DataAceiteContrato = DateTime.Now;
                            Contrato_C.TB012_CadastradorPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            Contrato_C.TB012_TipoContrato = 2;//Parceiro

                            Contrato_C = Contrato_N.contratoParceiroInsert(Contrato_C);

                            lblContrato.Text = Contrato_C.TB012_Id.ToString();

                            UnidadeController Unidade_C = new UnidadeController();

                            Unidade_C.TB020_Matriz                  = 1;
                            Unidade_C.TB012_id                      = Contrato_C.TB012_Id;
                            Unidade_C.TB020_RazaoSocial             = txtContatoTB013NomeCompleto.Text;
                            Unidade_C.TB020_NomeFantasia            = txtContratoTB013NomeExibicao.Text;
                            Unidade_C.TB020_NomeExibicaoDetalhes    = txtContratoTB013NomeExibicaoDetalhes.Text;
                            Unidade_C.TB020_CategoriaExibicao       = txtCategoriaExibicao.Text.TrimEnd();
                            Unidade_C.TB020_TipoPessoa              = Convert.ToInt16(cmbContratoTB013Tipo.SelectedValue);
                            Unidade_C.TB020_Documento               = mskContratoTB013_CPFCNPJ.Text;
                            Unidade_C.TB006_id                      = Convert.ToInt64(cmbContatoMunicipio.SelectedValue);
                            Unidade_C.TB020_Cep                     = mskContratoCEP.Text.Replace("-", "");
                            Unidade_C.TB020_Logradouro              = txtContratoTB013Logradouro.Text;
                            Unidade_C.TB020_Numero                  = txtContratoTB013Numero.Text;
                            Unidade_C.TB020_Bairro                  = txtContratoTB013Bairro.Text;
                            Unidade_C.TB020_Complemento             = txtContratoTB013Complemento.Text;
                            Unidade_C.TB020_TextoPortal             = txtContratoTB020_TextoPortal.Text.TrimEnd();
                            Unidade_C.TB020_CadastradoPor           = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            Unidade_C.TB020_AlteradoPor             = ParametrosInterface.objUsuarioLogado.TB011_Id;
                            Unidade_C.TB020_StatusS                 = cmbContratoStatus.SelectedValue.ToString();
                            Unidade_C.TB020_CategoriaExibicao       = txtCategoriaExibicao.Text.TrimEnd();
                            //Unidade_C.TB020_Desconto = "-";

                            UnidadeNegocios Unidade_N = new UnidadeNegocios();

                            Unidade_C = Unidade_N.UnidadeInsert(Unidade_C);

                            if (Unidade_C != null)
                            {
                                lblUnidadeMatrizId.Text = Unidade_C.TB020_id.ToString().Trim();
                            }

                            if (lblContratoPrincipalImgAlterado.Text.Trim() != string.Empty)
                            {
                                AtualizarImagem(Unidade_C.TB020_id);
                            }

                            // GerarParcelas(Convert.ToInt16(lblCiclo.Text), Convert.ToInt64(lblContratoTB012id.Text));

                            //txtContratoTB013iD.Text = Unidade_C.TB020_id.ToString();
                            mnuContratoUnidades.Enabled = true;
                            //mnuContratoParcela.Enabled = true;

                            Pessoa_N.VincularTitularContato(Contrato_C.TB012_Id, Titular_C.TB013_id);

                            MessageBox.Show(MensagensDoSistema._0017, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        /*-----------------------------------------------------------------------------------------------------------------------------------------------*/
                        else
                        {
                            /*Atualização*/
                            Contrato_C.TB012_Id = Convert.ToInt64(lblContrato.Text);
                        }
                    }

                    txtFiltroAssociado.Text = lblContrato.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AtualizarContrato()
        {
            try
            {
                /*Atualizar Matriz*/
                UnidadeController Unidade_C = new UnidadeController();
                Unidade_C.TB020_id                      = Convert.ToInt64(lblUnidadeMatrizId.Text);
                Unidade_C.TB020_RazaoSocial             = txtContatoTB013NomeCompleto.Text.TrimEnd();
                Unidade_C.TB020_NomeFantasia            = txtContratoTB013NomeExibicao.Text.TrimEnd();
                Unidade_C.TB020_CategoriaExibicao       = txtCategoriaExibicao.Text.TrimEnd();
                Unidade_C.TB020_Documento               = mskContratoTB013_CPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                Unidade_C.TB006_id                      = Convert.ToInt64(cmbContatoMunicipio.SelectedValue);
                Unidade_C.TB020_Cep                     = mskContratoCEP.Text.Replace("-", "").Replace("_", "").Replace(" ", "");
                Unidade_C.TB020_Numero                  = txtContratoTB013Numero.Text;
                Unidade_C.TB020_Logradouro              = txtContratoTB013Logradouro.Text.TrimEnd();
                Unidade_C.TB020_Bairro                  = txtContratoTB013Bairro.Text;
                Unidade_C.TB020_Complemento             = txtContratoTB013Complemento.Text;
                Unidade_C.TB020_TextoPortal             = txtContratoTB020_TextoPortal.Text.TrimEnd();
                Unidade_C.TB020_AlteradoPor             = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Unidade_C.TB020_NomeExibicaoDetalhes    = txtContratoTB013NomeExibicaoDetalhes.Text.TrimEnd();
                Unidade_C.TB020_CategoriaExibicao       = txtCategoriaExibicao.Text.TrimEnd();

                UnidadeNegocios Unidade_N               = new UnidadeNegocios();
                Unidade_C                               = Unidade_N.UnidadeAtualizar(Unidade_C);
                /*Atualizar titular*/
                PessoaController Titular                = new PessoaController();
                Titular.Municipio                       = new MunicipioController();

                Titular.TB013_IdProtheus                = lblTitularTB013id.Text.Trim();
                Titular.TB013_TipoS                     = "1";
                Titular.TB013_CPFCNPJ                   = mskTitularTB013_CPFCNPJ.Text;
                Titular.TB013_NomeCompleto              = txtTitularTB013_NomeCompleto.Text;
                Titular.TB013_NomeExibicao              = txtContratoTB013NomeExibicao.Text;
                Titular.TB013_NomeExibicaoDetalhes      = txtContratoTB013NomeExibicaoDetalhes.Text;
                Titular.TB013_SexoS                     = Convert.ToString(cmbTitularTB013Sexo.SelectedValue);
                Titular.TB013_RG                        = txtTitularTB013_RG.Text;
                Titular.TB013_RGOrgaoEmissor            = txtTitularTB013_RGOrgaoEmissor.Text;
                Titular.TB013_DataNascimento            = dtpTitularTB013_DataNascimento.Value;
                Titular.TB004_Cep                       = mskTitularTB004Cep.Text;
                Titular.Municipio.TB006_id              = Convert.ToInt64(cmbTitularMunicipio.SelectedValue);
                Titular.TB013_Logradouro                = txtTitularEndereco.Text;
                Titular.TB013_Numero                    = txtTitularNumero.Text;
                Titular.TB013_Bairro                    = txtTitularBairro.Text;
                Titular.TB013_Complemento               = txtTitularComplemento.Text;
                Titular.TB013_AlteradoPor               = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Titular.TB013_id                        = Convert.ToInt64(lblTitularTB013id.Text);

                PessoaNegocios titular_N = new PessoaNegocios();
                titular_N.PessoaUpdate(Titular, ParametrosInterface.objUsuarioLogado.TB011_Id);
                /**/
                ContratosController Contrato_C          = new ContratosController();
                Contrato_C.TB012_Id                     = Convert.ToInt64(lblContrato.Text);
                Contrato_C.TB012_Logradouro             = txtContratoTB013Logradouro.Text.TrimEnd();
                Contrato_C.TB012_Numero                 = txtContratoTB013Numero.Text;
                Contrato_C.TB012_Bairro                 = txtContratoTB013Bairro.Text;
                Contrato_C.TB004_Cep                    = mskContratoCEP.Text.Replace(".", "").Replace(",", "");
                Contrato_C.TB006_id                     = Convert.ToInt64(cmbContatoMunicipio.SelectedValue);
                Contrato_C.TB012_Complemento            = txtContratoTB013Complemento.Text;
                Contrato_C.TB020_NomeFantasia           = txtContatoTB013NomeCompleto.Text;
                Contrato_C.TB020_RazaoSocial            = txtContatoTB013NomeCompleto.Text;
                Contrato_C.TB012_StatusS                = cmbContratoStatus.SelectedValue.ToString();
                Contrato_C.TB012_DiaVencimento          = Convert.ToInt16(cmbDiaVencimento.Text);

                ContratoNegocios Contrato_N = new ContratoNegocios();

                Contrato_N.ContratoParceiroAlteracao(Contrato_C);
                if (lblContratoPrincipalImgAlterado.Text.Trim() != string.Empty)
                {
                    AtualizarImagem(Unidade_C.TB020_id);
                }
                {

                }
                MessageBox.Show(MensagensDoSistema._0013, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AtualizarImagem(long TB020_id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                /**/
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ParametrosInterface.objUsuarioLogado.TB011_ftpServidor + "/portal" + ParametrosInterface.PastaLogoUnidade + "/" + TB020_id.ToString() + ".jpg");
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // Get network credentials.
                request.Credentials = new NetworkCredential(ParametrosInterface.objUsuarioLogado.TB011_ftpUsuario, ParametrosInterface.objUsuarioLogado.TB011_ftpSenha);

                // Read the file's contents into a byte array.
                byte[] bytes = System.IO.File.ReadAllBytes(lblContratoPrincipalImgAlterado.Text.TrimEnd().TrimStart());

                // Write the bytes into the request stream.
                request.ContentLength = bytes.Length;
                using (Stream request_stream = request.GetRequestStream())
                {
                    request_stream.Write(bytes, 0, bytes.Length);
                    request_stream.Close();
                }
                /**/

                this.Cursor = Cursors.Default;
                MessageBox.Show(MensagensDoSistema._0041, "Logo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var requestFPT = WebRequest.Create(ParametrosInterface.objUsuarioLogado.TB011_ftpServidor.Replace("ftp:", "https:") + ParametrosInterface.PastaLogoUnidade + "/" + TB020_id.ToString() + ".jpg");

                using (var response = requestFPT.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pctLogoContrato.Image = Bitmap.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool ValidarContrato()
        {
            if (string.IsNullOrEmpty(cmbDiaVencimento.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "\"Vencimento\""), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbDiaVencimento.Focus();
                return false;
            }

            if (txtCategoriaExibicao.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Categoria de Exibição "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCategoriaExibicao.Focus();
                return false;
            }

            if (mskContratoTB013_CPFCNPJ.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Doc"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoTB013_CPFCNPJ.Focus();
                return false;
            }

            if (mskTitularTB013_CPFCNPJ.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CPF"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskTitularTB013_CPFCNPJ.Focus();
                return false;
            }

            if (txtContatoTB013NomeCompleto.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Razão Social"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContatoTB013NomeCompleto.Focus();
                return false;
            }
            if (txtContratoTB013NomeExibicaoDetalhes.Text.Trim() == string.Empty)
            {
                txtContratoTB013NomeExibicaoDetalhes.Text = txtContratoTB013NomeExibicao.Text;
            }



            if (txtContratoTB013NomeExibicao.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Exibição Sitel"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTB013NomeExibicao.Focus();
                return false;
            }
            if (mskContratoCEP.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CEP Contrato"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskContratoCEP.Focus();
                return false;
            }

            if (mskTitularTB004Cep.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "CEP Titular"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskTitularTB004Cep.Focus();
                return false;
            }

            if (txtTitularTB013_RG.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "RG"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTitularTB013_RG.Focus();
                return false;
            }

            if (txtTitularTB013_RGOrgaoEmissor.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Orgão Emissor RG"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTitularTB013_RGOrgaoEmissor.Focus();
                return false;
            }

            if (txtContratoTB013Logradouro.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Logradouro do contrato"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTB013Logradouro.Focus();
                return false;
            }

            if (txtTitularEndereco.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Logradouro do titular"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTitularEndereco.Focus();
                return false;
            }

            if (txtContratoTB013Numero.Text.Trim() == string.Empty)
            {
                txtContratoTB013Numero.Text = "S/N";
            }

            if (txtTitularNumero.Text.Trim() == string.Empty)
            {
                txtTitularNumero.Text = "S/N";
            }



            if (txtContratoTB013Complemento.Text.Trim() == string.Empty)
            {
                txtContratoTB013Complemento.Text = " ";
            }
            if (txtTitularComplemento.Text.Trim() == string.Empty)
            {
                txtTitularComplemento.Text = " ";
            }

            if (txtContratoTB013Bairro.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Bairro contrato"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContratoTB013Bairro.Focus();
                return false;
            }

            if (txtTitularBairro.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Bairro titular"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTitularBairro.Focus();
                return false;
            }

            if (txtContratoTB020_TextoPortal.Text.Trim() == string.Empty)
            {
                txtContratoTB020_TextoPortal.Text = " ";
            }

            if (pctLogoContrato.Image == null)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Logo não informada"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void cmbContratoPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbContratoPais.SelectedValue) > 0)
                {
                    PaisController filtro = new PaisController();
                    filtro.TB003_id = Convert.ToInt16(cmbContratoPais.SelectedValue);
                    PopularEstadosContrato(filtro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularEstadosContrato(PaisController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbContratoEstado.DataSource = null;
            cmbContratoEstado.Items.Clear();
            try
            {
                cmbContratoEstado.DataSource = Endereco_N.EstadosController(filtro).Tables[0];
                cmbContratoEstado.DisplayMember = "TB005_Estado";
                cmbContratoEstado.ValueMember = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void PopularEstadosTitular(PaisController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbTitularEstado.DataSource = null;
            cmbTitularEstado.Items.Clear();
            try
            {
                cmbTitularEstado.DataSource = Endereco_N.EstadosController(filtro).Tables[0];
                cmbTitularEstado.DisplayMember = "TB005_Estado";
                cmbTitularEstado.ValueMember = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmbContratoEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbContratoEstado.SelectedValue) > 0)
                {
                    EstadoController filtro = new EstadoController();
                    filtro.TB005_Id = Convert.ToInt16(cmbContratoEstado.SelectedValue);
                    PopularMunicipiosContrato(filtro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularMunicipiosContrato(EstadoController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbContatoMunicipio.DataSource = null;
            cmbContatoMunicipio.Items.Clear();
            try
            {
                cmbContatoMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbContatoMunicipio.DisplayMember = "TB006_Municipio";
                cmbContatoMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void PopularMunicipiosTitular(EstadoController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbTitularMunicipio.DataSource = null;
            cmbTitularMunicipio.Items.Clear();
            try
            {
                cmbTitularMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbTitularMunicipio.DisplayMember = "TB006_Municipio";
                cmbTitularMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void mskContratoCEP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mskContratoCEP.Text.Replace("-", "").Replace(" ", "").Length == 8)
                {

                    EnderecoNegocios Endereco_N = new EnderecoNegocios();
                    CEPController filtro = new CEPController();
                    filtro.TB004_Cep = Convert.ToInt32(mskContratoCEP.Text.Replace("-", "").Replace(" ", ""));
                    DataSet CEP = Endereco_N.Cep(filtro);

                    if (Convert.ToInt64(CEP.Tables[0].Rows[0]["TB004_id"].ToString()) > 0)
                    {
                        txtContratoTB013Logradouro.Text = CEP.Tables[0].Rows[0]["TB004_Logradouro"].ToString();
                        txtContratoTB013Bairro.Text = CEP.Tables[0].Rows[0]["TB004_Bairro"].ToString();

                        cmbContratoEstado.SelectedValue = CEP.Tables[0].Rows[0]["TB005_Id"].ToString();
                        cmbContatoMunicipio.SelectedValue = CEP.Tables[0].Rows[0]["TB006_id"].ToString();

                        txtContratoTB013Numero.Focus();
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
        private void mskTitularTB004_Cep_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mskTitularTB004Cep.Text.Replace("-", "").Replace(" ", "").Length == 8)
                {

                    EnderecoNegocios Endereco_N = new EnderecoNegocios();
                    CEPController filtro = new CEPController();
                    filtro.TB004_Cep = Convert.ToInt32(mskTitularTB004Cep.Text.Replace("-", "").Replace(" ", ""));
                    DataSet CEP = Endereco_N.Cep(filtro);

                    if (Convert.ToInt64(CEP.Tables[0].Rows[0]["TB004_id"].ToString()) > 0)
                    {
                        txtTitularEndereco.Text = CEP.Tables[0].Rows[0]["TB004_Logradouro"].ToString();
                        txtTitularBairro.Text = CEP.Tables[0].Rows[0]["TB004_Bairro"].ToString();

                        cmbTitularEstado.SelectedValue = CEP.Tables[0].Rows[0]["TB005_Id"].ToString();
                        cmbTitularMunicipio.SelectedValue = CEP.Tables[0].Rows[0]["TB006_id"].ToString();

                        txtTitularNumero.Focus();
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
        private void mnuContratoUnidades_Click(object sender, EventArgs e)
        {
            try
            {
                tabPrincipal.TabPages.Add(tbUnidades);
                dgwUnidades.AutoGenerateColumns = false;
                StringBuilder sSQL = new StringBuilder();

                UnidadeNegocios Unidade_N = new UnidadeNegocios();
                dgwUnidades.DataSource = Unidade_N.UnidadesContrato(Convert.ToInt64(lblContrato.Text)); ;
                dgwUnidades.Refresh();

                tabPrincipal.TabPages.Remove(tbContrato);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuUnidadesFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbContrato);
            tabPrincipal.TabPages.Remove(tbUnidades);
        }
        private void dgwContratos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgContratos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":
                            try
                            {

                               
                                LimparDadosContrato();
                                tabPrincipal.TabPages.Remove(tbLista);
                                tabPrincipal.TabPages.Add(tbContrato);

                                PontosDeVenda();
                                ContratoNegocios Contrato_N = new ContratoNegocios();
                                ContratosController Contrato_C = new ContratosController();
                                Contrato_C = Contrato_N.contratoParceiroSelect(Convert.ToInt64(ddgContratos.Rows[e.RowIndex].Cells["TB012_id"].Value));

                                if (Contrato_C.TB012_TipoContrato == 1)
                                {
                                    MessageBox.Show(Format(MensagensDoSistema._0101, "Familiar", "Erro", MessageBoxButtons.OK,
                                  MessageBoxIcon.Error));
                                    //1 - Familiar
                                    //2 - Parceiro
                                    //3 - Corporativo
                                    //4 - Familiar Corporativo
                                    //5 - Familiar Parceiro
                                    return;
                                }

                                if (Contrato_C.TB012_TipoContrato == 3)
                                {
                                    MessageBox.Show(Format(MensagensDoSistema._0101, "Corporativo", "Erro", MessageBoxButtons.OK,
                                  MessageBoxIcon.Error));
                                    return;
                                }
                                if (Contrato_C.TB012_TipoContrato == 4)
                                {
                                    MessageBox.Show(Format(MensagensDoSistema._0101, "Familiar Corporativo", "Erro", MessageBoxButtons.OK,
                                  MessageBoxIcon.Error));
                                }
                                if (Contrato_C.TB012_TipoContrato == 5)
                                {
                                    MessageBox.Show(Format(MensagensDoSistema._0101, "Familiar Parceiro", "Erro", MessageBoxButtons.OK,
                                  MessageBoxIcon.Error));
                                    return;
                                }

                                lblContrato.Text = Contrato_C.TB012_Id.ToString();
                                cmbContratoPontosDeVenda.SelectedValue = Convert.ToInt64(Contrato_C.PontoDeVenda.TB002_id);
                            
                                lblCiclo.Text = Contrato_C.TB012_CicloContrato;
                                cmbDiaVencimento.Text = Contrato_C.TB012_DiaVencimento.ToString();
                                cmbContratoTB013Tipo.SelectedValue = Contrato_C.Unidade.TB020_TipoPessoa.ToString();
                                dtContratoInicio.Value = Contrato_C.TB012_Inicio;
                                dtpContratoTB012Fim.Value = Contrato_C.TB012_Fim;
                                cmbContratoStatus.SelectedValue = Contrato_C.TB012_StatusS;
                                mskContratoCEP.Text = Contrato_C.TB004_Cep;
                                txtContratoTB013Logradouro.Text = Contrato_C.TB012_Logradouro;
                                txtContratoTB013Numero.Text = Contrato_C.TB012_Numero;
                                txtContratoTB013Bairro.Text = Contrato_C.TB012_Bairro;
                                txtContratoTB013Complemento.Text = Contrato_C.TB012_Complemento;
                                cmbContratoPais.SelectedValue = Convert.ToInt64(Contrato_C.Unidade.Pais.TB003_id);//Convert.ToInt64(Contrato_C.Pais.TB003_id);
                                cmbContratoEstado.SelectedValue = Convert.ToInt64(Contrato_C.Unidade.Estado.TB005_Id);//Convert.ToInt64(Contrato_C.Estado.TB005_Id);

                                EstadoController filtroMunicipios = new EstadoController();
                                filtroMunicipios.TB005_Id = Convert.ToInt64(Contrato_C.Unidade.Estado.TB005_Id);
                                PopularMunicipiosContrato(filtroMunicipios);

                                cmbContatoMunicipio.SelectedValue = Convert.ToInt64(Contrato_C.Unidade.Municipio.TB006_id);//Convert.ToInt64(Contrato_C.Municipio.TB006_id);
                                lblTitularTB013id.Text = Contrato_C.Titular.TB013_id.ToString();

                                UnidadeNegocios Unidade_N = new UnidadeNegocios();
                                UnidadeController Unidade_C = Unidade_N.UnidadeMatriz(Contrato_C.TB012_Id);

                                mskContratoTB013_CPFCNPJ.Text = Unidade_C.TB020_Documento;
                                txtContatoTB013NomeCompleto.Text = Unidade_C.TB020_RazaoSocial;
                                txtContratoTB013NomeExibicao.Text = Unidade_C.TB020_NomeFantasia;
                                txtCategoriaExibicao.Text = Contrato_C.Unidade.TB020_CategoriaExibicao;
                                txtContratoTB013NomeExibicaoDetalhes.Text = Unidade_C.TB020_NomeExibicaoDetalhes;
                                //txtContratoTB013iD.Text = Unidade_C.TB020_id.ToString();
                                //txtCategoriaExibicao.Text                   = Unidade_C.TB020_CategoriaExibicao;
                                CarregarCategoriaNivel1DoContrato(Unidade_C.TB012_id);
                                txtContratoTB020_TextoPortal.Text = Unidade_C.TB020_TextoPortal;
                                lblUnidadeMatrizId.Text = Unidade_C.TB020_id.ToString();
                                RecuperarContatosMatriz(Unidade_C.TB020_id);
                                /*

                                /*Recuperar Titular do Contrato*/

                                PessoaNegocios Titular_N = new PessoaNegocios();
                                PessoaController Titular_C = Titular_N.pessoaSelectId(Contrato_C.Titular.TB013_id);

                                mskTitularTB013_CPFCNPJ.Text = Titular_C.TB013_CPFCNPJ;
                                lblCarteirinha.Text = Titular_C.TB013_Cartao;
                                //txtTitularTB013_IdProtheus.Text = Titular_C.TB013_IdProtheus.ToString();
                                txtTitularTB013_RG.Text = Titular_C.TB013_RG.ToString();
                                txtTitularTB013_RGOrgaoEmissor.Text = Titular_C.TB013_RGOrgaoEmissor.ToString();
                                txtTitularTB013_NomeCompleto.Text = Titular_C.TB013_NomeCompleto.ToString();
                                //txtTitularTB013_NomeExibicao.Text = Titular_C.TB013_NomeExibicao.ToString();
                                txtTitularEndereco.Text = Titular_C.TB013_Logradouro.ToString();
                                txtTitularNumero.Text = Titular_C.TB013_Numero.ToString();
                                txtTitularBairro.Text = Titular_C.TB013_Bairro.ToString();
                                txtTitularComplemento.Text = Titular_C.TB013_Complemento.ToString();
                                dtpTitularTB013_DataNascimento.Value = Titular_C.TB013_DataNascimento;
                                mskTitularTB004Cep.Text = Titular_C.TB004_Cep;
                                cmbTitularTB013Sexo.SelectedValue = Titular_C.TB013_SexoS;
                                cmbTitularPais.SelectedValue = Titular_C.Municipio.Estado.Pais.TB003_id;

                                PaisController filtro = new PaisController();
                                filtro.TB003_id = Titular_C.Municipio.Estado.Pais.TB003_id;
                                PopularEstadosTitular(filtro);

                                cmbTitularEstado.SelectedValue = Titular_C.Municipio.Estado.TB005_Id;

                                EstadoController filtroEstado = new EstadoController();
                                filtroEstado.TB005_Id = Titular_C.Municipio.Estado.TB005_Id;
                                PopularMunicipiosTitular(filtroEstado);
                                cmbTitularMunicipio.SelectedValue = Titular_C.Municipio.TB006_id;
                                mnuContratoUnidades.Enabled = true;
                                cmbContratoTB013Tipo.Enabled = false;
                                mskContratoTB013_CPFCNPJ.Enabled = false;
                                cmbContratoStatus.Enabled = false;
                                cmbContratoPais.Enabled = true;
                                mskContratoCEP.Enabled = true;
                                cmbContratoEstado.Enabled = true;
                                cmbContatoMunicipio.Enabled = true;
                                txtContratoTB013Logradouro.Enabled = true;
                                txtContratoTB013Numero.Enabled = true;
                                txtContratoTB013Bairro.Enabled = true;
                                txtContratoTB013Complemento.Enabled = true;
                                dtContratoInicio.Enabled = false;
                                dtpContratoTB012Fim.Enabled = false;

                                //Recuperar Plano de Cadastro

                                ParcelaNegocios Parcela_N = new ParcelaNegocios();
                                List<ParcelaController> ParcelasContrato = Parcela_N.ParcelasContratoExistente(Convert.ToInt64(lblContrato.Text));
                                try
                                {
                                    var request = WebRequest.Create(ParametrosInterface.objUsuarioLogado.TB011_ftpServidor.Replace("ftp:", "https:") + ParametrosInterface.PastaLogoUnidade + "/" + Unidade_C.TB020_id.ToString() + ".jpg");
         
                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    pctLogoContrato.Image = Bitmap.FromStream(stream);
                                }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                
                                }

                                txtContatoTB013NomeCompleto.Focus();

                               
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                tabPrincipal.TabPages.Remove(tbContrato);
                                tabPrincipal.TabPages.Add(tbLista);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabPrincipal.TabPages.Remove(tbContrato);
                tabPrincipal.TabPages.Add(tbLista);
            }
        }
        public bool IsFileOpen(string filePath)
        {
            bool fileOpened = false;
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenWrite(filePath);
                fs.Close();
            }
            catch (System.IO.IOException)
            {
                fileOpened = true;
            }

            return fileOpened;
        }
        private void cmbTitularEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbTitularEstado.SelectedValue) > 0)
                {
                    EstadoController filtro = new EstadoController();
                    filtro.TB005_Id = Convert.ToInt16(cmbTitularEstado.SelectedValue);
                    PopularMunicipiosTitular(filtro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimparUnidade()
        {
            pcbUnidadeLogo.ImageLocation = "";
            txtUnidadeDocumento.Text = "";
            txtUnidadeId.Text = "";
            txtUnidadeRazaoSocial.Text = "";
            txtUnidadeNomeFantasia.Text = "";
            mskUnidadeCep.Text = "";
            txtUnidadeLogradouro.Text = "";
            txtUnidadeNumero.Text = "";
            txtUnidadeBairro.Text = "";
            txtUnidadeComplemento.Text = "";
            txtUnidadeTextoPortal.Text = "";
        }
        private void dgwUnidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgwUnidades.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":
                            try
                            {

                                //LimparDadosContrato();
                                PontosDeVenda();
                                CarregarTipoPessoaUnidade();
                                UnidadeNegocios Unidade_N = new UnidadeNegocios();
                                UnidadeController Unidade_C = Unidade_N.UnidadeSelect(Convert.ToInt64(dgwUnidades.Rows[e.RowIndex].Cells["TB020_id"].Value));

                                txtUnidadeDocumento.Text = Unidade_C.TB020_Documento;
                                txtUnidadeId.Text = Unidade_C.TB020_id.ToString();
                                txtUnidadeRazaoSocial.Text = Unidade_C.TB020_RazaoSocial;
                                txtUnidadeNomeFantasia.Text = Unidade_C.TB020_NomeFantasia;
                                mskUnidadeCep.Text = Unidade_C.TB020_Cep;
                                txtUnidadeLogradouro.Text = Unidade_C.TB020_Logradouro;
                                txtUnidadeNumero.Text = Unidade_C.TB020_Numero;
                                txtUnidadeBairro.Text = Unidade_C.TB020_Bairro;
                                txtUnidadeComplemento.Text = Unidade_C.TB020_Complemento;
                                txtUnidadeTextoPortal.Text = Unidade_C.TB020_TextoPortal;
                                cmbUnidadeTipoPessoa.SelectedValue = Convert.ToInt16(Unidade_C.TB020_TipoPessoa);
                                cmbUnidadeStatus.SelectedValue = Unidade_C.TB020_StatusS;
                                cmbUnidadePais.SelectedValue = Unidade_C.Pais.TB003_id;

                                PaisController filtroEstado = new PaisController();
                                filtroEstado.TB003_id = Unidade_C.Pais.TB003_id;
                                PopularEstadosUnidade(filtroEstado);

                                cmbUnidadeUF.SelectedValue = Unidade_C.Estado.TB005_Id;

                                EstadoController filtroMunicipio = new EstadoController();
                                filtroMunicipio.TB005_Id = Unidade_C.Estado.TB005_Id;
                                PopularMunicipiosUnidade(filtroMunicipio);

                                cmbUnidadeMunicipio.SelectedValue = Unidade_C.Municipio.TB006_id;

                                string strNomeArquivo = Convert.ToString(txtUnidadeId.Text);

                                FileInfo arquivo = new FileInfo(strNomeArquivo);
                                arquivo.Delete();

                                vetorImagens = Unidade_C.TB020_logo;
                                FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                fs.Write(vetorImagens, 0, vetorImagens.Length);
                                fs.Flush();
                                fs.Close();

                                pcbUnidadeLogo.Image = Image.FromFile(strNomeArquivo);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void PopularEstadosUnidade(PaisController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbUnidadeUF.DataSource = null;
            cmbUnidadeUF.Items.Clear();
            try
            {
                cmbUnidadeUF.DataSource = Endereco_N.EstadosController(filtro).Tables[0];
                cmbUnidadeUF.DisplayMember = "TB005_Estado";
                cmbUnidadeUF.ValueMember = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void PopularMunicipiosUnidade(EstadoController filtro)
        {
            EnderecoNegocios Endereco_N = new EnderecoNegocios();
            cmbUnidadeMunicipio.DataSource = null;
            cmbUnidadeMunicipio.Items.Clear();
            try
            {
                cmbUnidadeMunicipio.DataSource = Endereco_N.MunicipioController(filtro).Tables[0];
                cmbUnidadeMunicipio.DisplayMember = "TB006_Municipio";
                cmbUnidadeMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void mnuUnidadesLimpar_Click(object sender, EventArgs e)
        {
            LimparUnidade();
        }
        private void btnUnidadeIncluirLogo_Click(object sender, EventArgs e)
        {
            if (txtUnidadeId.Text == string.Empty)
            {
                MessageBox.Show("Informe o código da imagem no Banco de dados", "Código da Imagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CarregaImagem();
        }
        protected void CarregaImagem()
        {
            try
            {
                this.openFileDialog1.ShowDialog(this);
                string strFn = this.openFileDialog1.FileName;

                if (string.IsNullOrEmpty(strFn))
                    return;

                this.pcbUnidadeLogo.Image = Image.FromFile(strFn);
                FileInfo arqImagem = new FileInfo(strFn);
                tamanhoArquivoImagem = arqImagem.Length;
                FileStream fs = new FileStream(strFn, FileMode.Open, FileAccess.Read, FileShare.Read);
                vetorImagens = new byte[Convert.ToInt32(this.tamanhoArquivoImagem)];
                int iBytesRead = fs.Read(vetorImagens, 0, Convert.ToInt32(this.tamanhoArquivoImagem));
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void mnuUnidadeSalvar_Click(object sender, EventArgs e)
        {
            UnidadeNegocios Unidade_N = new UnidadeNegocios();
            UnidadeController Unidade_C = new UnidadeController();

            Unidade_C.TB020_logo = vetorImagens;
            Unidade_C.TB020_RazaoSocial = txtUnidadeRazaoSocial.Text;
            Unidade_C.TB020_NomeFantasia = txtContratoTB013NomeExibicao.Text;
            Unidade_C.TB020_TipoPessoa = Convert.ToInt16(cmbContratoTB013Tipo.SelectedValue);
            Unidade_C.TB020_Documento = txtUnidadeDocumento.Text;
            Unidade_C.TB006_id = Convert.ToInt64(cmbUnidadeMunicipio.SelectedValue);
            Unidade_C.TB020_Cep = mskUnidadeCep.Text.Replace("-", "");
            Unidade_C.TB020_Logradouro = txtUnidadeLogradouro.Text;
            Unidade_C.TB020_Numero = txtUnidadeNumero.Text;
            Unidade_C.TB020_Bairro = txtUnidadeBairro.Text;
            Unidade_C.TB020_Complemento = txtUnidadeComplemento.Text;
            Unidade_C.TB020_TextoPortal = txtUnidadeTextoPortal.Text;
            Unidade_C.TB020_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            Unidade_C.TB020_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
            Unidade_C.TB020_StatusS = cmbUnidadeStatus.SelectedValue.ToString();

            if (txtUnidadeId.Text.Trim() == string.Empty)
            {
                //Novo
                Unidade_C.TB020_Matriz = 0;

                Unidade_C = Unidade_N.UnidadeInsert(Unidade_C);
                txtUnidadeId.Text = Unidade_C.TB020_id.ToString();
            }
            else
            {
                //Atualizar
                Unidade_C.TB020_id = Convert.ToInt64(txtUnidadeId.Text);
                UnidadeController Logo_R = Unidade_N.UnidadeAtualizar(Unidade_C);
                MessageBox.Show(MensagensDoSistema._0018, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }          
        private void chkAbodarAdesao_Click(object sender, EventArgs e)
        {
            //double preco = 0;
            //var Data = DateTime.Now;
            //var ultimoDia = DateTime.DaysInMonth(Data.Year, Data.Month);
            //var DiaAtual = Data.Day;
            //var DiasParaFinalDoMes = ultimoDia - DiaAtual;
            //preco = preco + (Convert.ToDouble(lblValorMes.Text.Replace("R$", "")) / ultimoDia) * DiasParaFinalDoMes;

            //lblValorEntrada.Text = "R$ " + preco.ToString("#,##0.00");
        }
        private void mnuContratoParcela_Click(object sender, EventArgs e)
        {
            try
            {
                TB013_Tipo = Convert.ToInt16(cmbContratoTB013Tipo.SelectedValue);
                _tb012Status = Convert.ToInt16(cmbContratoStatus.SelectedValue);

                cmbParcelaCiclo.Items.Clear();
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
                    if (lblCiclo.Text.Trim() == Empty | lblCiclo.Text.Trim() == @"0")
                    {
                        lblCiclo.Text = dtContratoInicio.Value.Month.ToString() + dtContratoInicio.Value.Year.ToString();
                    }

                    cmbParcelaCiclo.Items.Add(Convert.ToInt64(lblCiclo.Text));
                    cmbParcelaCiclo.SelectedIndex = 0;
                }


                ListarParcelas(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(lblCiclo.Text));                           
                if (cmbParcelaStatus.ComboBox != null) cmbParcelaStatus.ComboBox.SelectedValue = "-1";



                if( new ParcelaNegocios().parcelasGeradasContrato(Convert.ToInt64(lblContrato.Text))==0)
                {
                    mnuParcelaGerarParcelas.Visible = true;
                    mnuParcelaGerarParcelas.Enabled = true;
                }

                ddtParcelas.DataSource =
                     new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                         Convert.ToInt64(lblCiclo.Text), -1);
                ddtParcelas.Refresh();


                tabPrincipal.TabPages.Remove(tbContrato);
                tabPrincipal.TabPages.Add(tbpParcelas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void mnuParcelasGerarCobranca_Click(object sender, EventArgs e)
        {
            GerarParcelasDoClicro(DateTime.Now.AddDays(2),1);
        }
        private void GerarParcelasDoClicro(DateTime vencimento, int aberturacontrato)
        {
            try
            {
                if (new ContratoNegocios().EdicaoContrato(Convert.ToInt64(lblContrato.Text)) > 0)
                {
                    MessageBox.Show(MensagensDoSistema._0077, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show(@"Deseja gerar as parcelas?", @"Parcelas", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mnuParcelaGerarParcelas.Enabled = false;
                    var parcelas = new ParcelaNegocios().gerarParcelaParceiro(
                                                        Convert.ToInt64(lblContrato.Text)
                                                        , vencimento
                                                        , ParametrosInterface.objUsuarioLogado.TB011_Id
                                                        , Convert.ToInt32(cmbParcelaCiclo.Text)
                                                        , ParametrosInterface.objUsuarioLogado.TB037_Id
                                                        ,0
                                                        ,-1
                                                        , aberturacontrato
                                                        , TB013_Tipo
                                                         );
                        ddtParcelas.DataSource =
                        new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                            Convert.ToInt64(lblCiclo.Text), -1);
                        ddtParcelas.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mnuParcelaGerarParcelas.Enabled = true;
            }
        }

        private void mnuParcelasFechar_Click(object sender, EventArgs e)
        {
            TB013_Tipo                  = 0;
            ddtParcelaItens.DataSource  = null;
            ddtParcelaItens.Refresh();
            lblParcelaId.Text           = "";
            lblParcelaId.Text           = "";

            lblParcelaPlanoId.Text      = "";
            lblParcelaPlano.Text        = "";

            ddtParcelas.DataSource      = null;
            ddtParcelas.Refresh();

            Parcelas.Clear();
            tabPrincipal.TabPages.Add(tbContrato);
            tabPrincipal.TabPages.Remove(tbpParcelas);
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
            for (int i = 0; i < BoletosEmitidos.Count; i++)
            {
                doc = webBoleto.Document;
                doc.Body.InnerHtml = BoletosEmitidos[i].TB016_Boleto;
                doc.Title = BoletosEmitidos[i].TB016_id.ToString();
                webBoleto.Print();
            }
        }
        private void mnuBoletoFechar_Click(object sender, EventArgs e)
        {
            BoletosEmitidos.Clear();
            lblBoletosQuant.Text = "0";
            tabPrincipal.TabPages.Remove(tbBoletos);
            tabPrincipal.TabPages.Add(tbpParcelas);
        }
        private void btnImagemContrato_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    lblContratoPrincipalImgAlterado.Text = ofd1.FileName;
                    pctLogoContrato.Image = Image.FromFile(lblContratoPrincipalImgAlterado.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void DTContratoUnidadeContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ContatoController contato = new ContatoController();
                ContatoNegocios ContatoN = new ContatoNegocios();

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    contato.TB009_id = Convert.ToInt64(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["txtContratoUnidadeContatoiD"].Value);

                    switch (DTContratoUnidadeContatos.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Excluir":

                            if (contato.TB009_id > 0)
                            {
                                contato.TB009_Contato = Convert.ToString(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["txtContratoUnidadeContato"].Value);
                                ContatoN.contatosContratoExcluir(contato, Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);
                                DTContratoUnidadeContatos.Rows.RemoveAt(DTContratoUnidadeContatos.CurrentRow.Index);
                            }
                            else
                            {
                                DTContratoUnidadeContatos.Rows.RemoveAt(DTContratoUnidadeContatos.CurrentRow.Index);
                            }


                            break;
                        case "Salvar":
                            if (ValidarContrato())
                            {
                                //

                                if (lblContrato.Text.Trim() == string.Empty)
                                {
                                    SalvarContrato();
                                }
                                PessoaController Pessoa_C = new PessoaController();
                                contato.Pessoa = Pessoa_C;
                                contato.Pessoa.TB013_id = 0;

                                //var temp = DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["TB009_ExibirPortal"].Value;
                                contato.TB009_ExibirPortal = Convert.ToInt16(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["TB009_ExibirPortal"].Value);
                                contato.Pessoa.TB013_id= Convert.ToInt64(lblTitularTB013id.Text); 
                                contato.TB020_id = Convert.ToInt64(lblUnidadeMatrizId.Text);
                                contato.TB009_TipoS = Convert.ToString(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["cmbContratoUnidadeContatoTipo"].Value);
                                //contato.TB009_Contato = Convert.ToString(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["txtContratoUnidadeContato"].Value);



                                if (Convert.ToInt16(contato.TB009_TipoS) == 3)//Email
                                {
                                    contato.TB009_Contato = Convert.ToString(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["txtContratoUnidadeContato"].Value);
                                }
                                else
                                {
                                    String Contato = Convert.ToString(DTContratoUnidadeContatos.Rows[e.RowIndex].Cells["txtContratoUnidadeContato"].Value).ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');


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

                                        DTContratoUnidadeContatos.Rows[e.RowIndex].Cells[3].Value = contato.TB009_Contato;
                                    }
                                }

                                /**/
                                contato.TB009_CadastradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                                contato.TB009_AlteradoPor = ParametrosInterface.objUsuarioLogado.TB011_Id;
                                contato.TB009_Nota = "Cadastrado via APP";

                                if (ValidaContato(contato))
                                {
                                    if (contato.TB009_id > 0)
                                    {
                                        ContatoN.contatosContratoUpdate(contato);
                                    }
                                    else
                                    {
                                        DTContratoUnidadeContatos.Rows[e.RowIndex].Cells[0].Value = ContatoN.contatosContratoInsert(contato, Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);
                                    }
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
        private void cmbContratoTB013Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt16(cmbContratoTB013Tipo.SelectedValue) == 1)
            {
                mskContratoTB013_CPFCNPJ.Mask = "999.999.999-99";
            }
            else
            {
                mskContratoTB013_CPFCNPJ.Mask = "99.999.999/9999-99";
            }
        }
        private void cmbContratoCategoriaNivel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt64(cmbContratoCategoriaNivel1.SelectedValue) > 0)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mskTitularTB013_CPFCNPJ_Leave(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() == string.Empty)
            {
                //Verificar se o CPF já não esta cadastrado
                if (mskTitularTB013_CPFCNPJ.Text.Replace(".", "").Replace(",", "").Replace("-", "").Replace("_", "").Replace(" ", "") != string.Empty)
                {
                    Util ValidaCPF = new Util();
                    if (ValidaCPF.CPF(mskTitularTB013_CPFCNPJ.Text))
                    {
                        //Consultar base de dados
                        PessoaNegocios Pessoa_N = new PessoaNegocios();
                        PessoaController Retorno = Pessoa_N.pessoaSelectCPFCNPJ(mskTitularTB013_CPFCNPJ.Text);
                        if (Retorno.TB013_id > 0)
                        {
                            lblTitularTB013id.Text = Retorno.TB013_id.ToString();
                            //txtTitularTB013_IdProtheus.Text         = Retorno.TB013_IdProtheus;
                            txtTitularTB013_RG.Text = Retorno.TB013_RG;
                            txtTitularTB013_RGOrgaoEmissor.Text = Retorno.TB013_RGOrgaoEmissor;
                            dtpTitularTB013_DataNascimento.Value = Retorno.TB013_DataNascimento;
                            txtTitularTB013_NomeCompleto.Text = Retorno.TB013_NomeCompleto;
                            cmbTitularTB013Sexo.SelectedValue = Retorno.TB013_SexoS;
                            cmbTitularPais.SelectedValue = Retorno.Municipio.Estado.Pais.TB003_id;
                            mskTitularTB004Cep.Text = Retorno.TB004_Cep;
                            cmbTitularEstado.SelectedValue = Retorno.Municipio.Estado.TB005_Id;
                            cmbTitularMunicipio.SelectedValue = Retorno.Municipio.TB006_id;
                            txtTitularEndereco.Text = Retorno.TB013_Logradouro;
                            txtTitularNumero.Text = Retorno.TB013_Numero;
                            txtTitularBairro.Text = Retorno.TB013_Bairro;
                            txtTitularComplemento.Text = Retorno.TB013_Complemento;
                            txtTitularTB013_NomeCompleto.Text = Retorno.TB013_NomeExibicao;
                        }
                        else
                        {
                            lblTitularTB013id.Text = "";
                            //txtTitularTB013_IdProtheus.Text     = "";
                            txtTitularTB013_RG.Text = "";
                            txtTitularTB013_RGOrgaoEmissor.Text = "";
                            txtTitularTB013_NomeCompleto.Text = "";
                            mskTitularTB004Cep.Text = "";
                            txtTitularEndereco.Text = "";
                            txtTitularNumero.Text = "";
                            txtTitularBairro.Text = "";
                            txtTitularComplemento.Text = "";
                            txtTitularTB013_NomeCompleto.Text = "";


                            if (mskTitularTB013_CPFCNPJ.Text == mskContratoTB013_CPFCNPJ.Text)
                            {
                                txtTitularTB013_NomeCompleto.Text = txtContatoTB013NomeCompleto.Text;
                                txtTitularTB013_NomeCompleto.Text = txtContatoTB013NomeCompleto.Text;
                                mskTitularTB004Cep.Text = mskContratoCEP.Text;
                                cmbTitularEstado.SelectedValue = cmbContratoEstado.SelectedValue;
                                cmbTitularMunicipio.SelectedValue = cmbContatoMunicipio.SelectedValue;
                                txtTitularEndereco.Text = txtContratoTB013Logradouro.Text;
                                txtTitularNumero.Text = txtContratoTB013Numero.Text;
                                txtTitularBairro.Text = txtContratoTB013Bairro.Text;
                                txtTitularComplemento.Text = txtContratoTB013Complemento.Text;
                            }
                        }
                    }
                }
            }
        }
        private void dtpContratoTB012Inicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime Hoje = dtContratoInicio.Value;
            dtContratoInicio.Enabled = true;
            dtContratoInicio.Value = Hoje;
            dtpContratoTB012Fim.Value = Hoje.AddMonths(12);
        }
        private void cmbContratoCategoriaNivel1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt64(cmbContratoCategoriaNivel1.SelectedValue) > 0)
                {
                    PreencherNivel2(Convert.ToInt64(cmbContratoCategoriaNivel1.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pctIncluirNivel2_Click(object sender, EventArgs e)
        {

            if (ValidarContrato())
            {
                try
                {
                    SalvarContrato();

                    CategoriaNegocios Categoria_N = new CategoriaNegocios();

                    if (Categoria_N.IncluirNivel2Contrato(Convert.ToInt64(cmbNivel2.SelectedValue), Convert.ToInt64(lblContrato.Text)) > 0)
                    {
                        CarregarNivel2DoContrato(Convert.ToInt64(cmbContratoCategoriaNivel1.SelectedValue), Convert.ToInt64(lblContrato.Text));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void pctExcluirNivel2_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                if (Categoria_N.ExcluirNivel2Contrato(Convert.ToInt64(cmbNivel2.SelectedValue), Convert.ToInt64(lblContrato.Text)))
                {
                    CarregarNivel2DoContrato(Convert.ToInt64(cmbContratoCategoriaNivel1.SelectedValue), Convert.ToInt64(lblContrato.Text));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pctIncluirNivel3_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                if (Categoria_N.IncluirNivel3Contrato(Convert.ToInt64(cmbNivel3.SelectedValue), Convert.ToInt64(lblContrato.Text)) > 0)
                {
                    CarregarNivel3DoContrato(Convert.ToInt64(cmbNivel2.SelectedValue), Convert.ToInt64(lblContrato.Text)); ;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pctExcluirNivel3_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                if (Categoria_N.ExcluirNivel3Contrato(Convert.ToInt64(cmbNivel3.SelectedValue), Convert.ToInt64(lblContrato.Text)))
                {
                    CarregarNivel3DoContrato(Convert.ToInt64(cmbNivel2.SelectedValue), Convert.ToInt64(lblContrato.Text));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lsbNivel2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lsbNivel2.SelectedValue != null)
                {
                    cmbNivel2.SelectedValue = lsbNivel2.SelectedValue;
                    PreencherNivel3(Convert.ToInt64(lsbNivel2.SelectedValue));
                    CarregarNivel3DoContrato(Convert.ToInt64(lsbNivel2.SelectedValue), Convert.ToInt64(lblContrato.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lsbNivel3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lsbNivel3.SelectedValue != null)
                {
                    cmbNivel3.SelectedValue = lsbNivel3.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RecuperarContatosMatriz(long TB020_id)
        {
            try
            {
                ContatoNegocios Contato_N = new ContatoNegocios();
                List<ContatoController> Retorno = Contato_N.contatosUnidade(TB020_id);

                for (int i = 0; i < Retorno.Count; i++)
                {
                    DTContratoUnidadeContatos.Rows.Add(new object[] { Retorno[i].TB009_id });
                    DTContratoUnidadeContatos.Rows[i].Cells["cmbContratoUnidadeContatoTipo"].Value = Retorno[i].TB009_TipoS.ToString();
                    DTContratoUnidadeContatos.Rows[i].Cells["txtContratoUnidadeContato"].Value = Retorno[i].TB009_Contato;
                    DTContratoUnidadeContatos.Rows[i].Cells["TB009_ExibirPortal"].Value = Retorno[i].TB009_ExibirPortal;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ptbFiltrarAssociado_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Parcelas.Count; i++)
            {
                var temp = Parcelas[i].TB016_FormaPagamentoS;
            }
        }
        private void pcbFiltrarLista_Click(object sender, EventArgs e)
        {
            try
            {
                string tipoCampo = @"Exibição";

                if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                {
                    tipoCampo = @"Exibição";
                }
                else
                {
                    if (this._validacoes.contemNumeros(txtFiltroAssociado.Text.Trim()))
                    {
                        if (txtFiltroAssociado.Text.Trim().Length == 6)
                        {
                            tipoCampo = @"Contrato";
                        }
                        else
                        {
                            tipoCampo = @"Documento";
                        }
                    }
                }

                switch (tipoCampo)
                {
                    case @"Exibição":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                            CarregarContratos(ListarContratos(vQuery));
                            break;
                        }
                    case @"Contrato":
                        {
                            string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart();
                            CarregarContratos(ListarContratos(vQuery));
                            break;
                        }
                    case @"Documento":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                                .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                .Replace("-", "").Replace("/", "") + "'";
                            CarregarContratos(ListarContratos(vQuery));
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
        private void descontosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabPrincipal.TabPages.Remove(tbContrato);
                tabPrincipal.TabPages.Add(tbDescontoUnidade);

                label1.Text = lblUnidadeMatrizId.Text;
                UnidadeController docR = new UnidadeNegocios().UnidadeDescontoSelect(Convert.ToInt64(lblUnidadeMatrizId.Text));
                string nomeArquivo = @"C:\Temp\" + label1.Text + ".doc";
                FileInfo logoUnidade = new FileInfo(nomeArquivo);
                File.Delete(nomeArquivo);


                //var fs = new FileStream(nomeArquivo,
                //                    FileMode.Create);
                //fs.Write(docR.TB020_Desconto, 0, docR.TB020_Desconto.Length);
                //fs.Close();


                FileStream fs2 = new FileStream(nomeArquivo, FileMode.CreateNew, FileAccess.Write);
                fs2.Write(docR.TB020_Desconto, 0, docR.TB020_Desconto.Length);
                fs2.Flush();
                fs2.Close();


                object filename = @"C:\Temp\" + label1.Text + ".doc";

                object save = false;
                object oMissing = System.Reflection.Missing.Value;

                Word._Application oWord;
                Word._Document oDoc;
                oWord = new Word.Application();
                oWord.Visible = false;
                // Open word document
                // different versions of word may have more or less oMissings
                oDoc = oWord.Documents.Open(ref filename, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing);

                // Select everything
                oDoc.Select();

                // Copy everything
                oWord.Selection.Copy();

                // Clean up the RTB
                rtbDoc.Text = "";
                // Paste the entire text with format
                rtbDoc.Paste();

                // Close word
                oDoc.Close(ref save, ref oMissing, ref oMissing);
                oWord.Quit(ref save, ref oMissing, ref oMissing);
            }
            catch (Exception)
            {

               
            }

        }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentFile= @"C:\Temp\" + label1.Text.Trim() + ".RTF";
            string pdfFile = @"C:\Temp\" + label1.Text.Trim() + ".doc";
            string strExt;
            
            strExt = System.IO.Path.GetExtension(currentFile);
            strExt = strExt.ToUpper();
            if (strExt == ".RTF")
            {
                rtbDoc.SaveFile(currentFile);
            }
            else
            {
                System.IO.StreamWriter txtWriter;
                txtWriter = new System.IO.StreamWriter(currentFile);
                txtWriter.Write(rtbDoc.Text);
                txtWriter.Close();
                txtWriter = null;
                rtbDoc.SelectionStart = 0;
                rtbDoc.SelectionLength = 0;
            }

            this.Text = "Editor: " + currentFile.ToString();
            rtbDoc.Modified = false;


            Cursor = Cursors.WaitCursor;
            // Obtem o objeto da aplicação Word
            Word._Application aplicacaoWord = new Word.Application();
            // Torna o Word Visivel (opcional).
            //aplicacaoWord.Visible = true;

            // Abre o arquivo
            object arquivoOrigem = currentFile;

            object missing = Type.Missing;
            aplicacaoWord.Documents.Open(ref arquivoOrigem, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing,
                ref missing, ref missing, ref missing, ref missing);

            // Salva o arquivo de saida
            object arquivoDestino = pdfFile;
            // 16 para docx, 0 for doc.
            // if (rdbDOC.Checked)
            // {
             object formato_doc = (int)0;
            // }

            Word._Document documentoAtivo = aplicacaoWord.ActiveDocument;
            documentoAtivo.SaveAs(ref arquivoDestino, ref formato_doc,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing);
            // Sai do aplicativo sem avisar
            object false_obj = false;
            //fecha e sai
            documentoAtivo.Close(ref false_obj, ref missing, ref missing);
            aplicacaoWord.Quit(ref missing, ref missing, ref missing);
            Cursor = Cursors.Default;

            var sample = new FileInfo(@"C:\Temp\" + label1.Text.Trim() + ".doc");
            var fileBinary = new byte[sample.Length];

            using (var fileStream = sample.OpenRead())
            {
                fileStream.Read(fileBinary, 0, (int)sample.Length);
                if(new UnidadeNegocios().UnidadeAtualizarDesconto(Convert.ToInt64(label1.Text), fileBinary))
                {
                    MessageBox.Show(MensagensDoSistema._0013, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbDescontoUnidade);
            lblUnidadeId.Text = "";
            rtbDoc.Text = "";
            tabPrincipal.TabPages.Add(tbContrato);
        }
        private void pbxGerarCartao_Click(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0043, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (lblCarteirinha.Text.Trim() != string.Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0044, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    PessoaNegocios Pessoa_N = new PessoaNegocios();

                    string Cartao = Pessoa_N.GerarCartaoParceiro(Convert.ToInt64(lblContrato.Text), ParametrosInterface.objUsuarioLogado.TB011_Id);

                    if (Cartao != string.Empty)
                    {
                        lblCarteirinha.Text = Cartao;
                        MessageBox.Show(MensagensDoSistema._0046, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void pcbContratoPrincipalLiberarStatus_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioAPPNegocios Privilegio_N = new UsuarioAPPNegocios();
                if (Privilegio_N.VerificaPrivilario(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id, 9) == true)
                {
                    cmbContratoStatus.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbContratoTB012Status_SelectedValueChanged(object sender, EventArgs e)
        {



            if (lblContrato.Text.Trim() != string.Empty)
            {
                cmbContratoStatus.Enabled = false;
            }
        }
        private void tabPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lsbNivel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lsbNivel1.SelectedValue != null)
                {
                    PreencherNivel2(Convert.ToInt64(lsbNivel1.SelectedValue));
                    CarregarNivel2DoContrato(Convert.ToInt64(lsbNivel1.SelectedValue), Convert.ToInt64(lblContrato.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pctIncluirNivel1_Click(object sender, EventArgs e)
        {
            if (ValidarContrato())
            {
                try
                {
                    SalvarContrato();

                    CategoriaNegocios Categoria_N = new CategoriaNegocios();

                    if (Categoria_N.IncluirNivel1Contrato(Convert.ToInt64(cmbContratoCategoriaNivel1.SelectedValue), Convert.ToInt64(lblContrato.Text)) > 0)
                    {
                        CarregarCategoriaNivel1DoContrato(Convert.ToInt64(lblContrato.Text));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void pctExcluirNivel1_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();

                if (Categoria_N.ExcluirNivel1Contrato(Convert.ToInt64(lsbNivel1.SelectedValue), Convert.ToInt64(lblContrato.Text)))
                {
                    CarregarCategoriaNivel1DoContrato(Convert.ToInt64(lblContrato.Text));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuContratoAcessos_Click(object sender, EventArgs e)
        {
            if (lblContrato.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0091.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                return;
            }

            CarregarAcessos();
            
            tabPrincipal.TabPages.Add(tbAcessos);
            label66.Text = cmbContatoMunicipio.SelectedValue.ToString();
                tabPrincipal.TabPages.Remove(tbContrato);

        }
        private void CarregarAcessos()
        {
            try
            {
                ddgAcessos.AutoGenerateColumns = false;
               
                ddgAcessos.DataSource = new PessoaNegocios().AcessosContrato(Convert.ToInt64(lblContrato.Text));
                ddgAcessos.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuAcessosFechar_Click(object sender, EventArgs e)
        {
            Limpar();
            tabPrincipal.TabPages.Remove(tbAcessos);
            tabPrincipal.TabPages.Add(tbContrato);
        }
        private void mnuAcessosIncluir_Click(object sender, EventArgs e)
        {
            lblAcessoId.Text = "";
            mskAcessoCPF.Text = "";
            txtAcessoNomeCompleto.Text = "";
            txtAcessoEmail.Text = "";
        }
        private void mskAcessoCPF_Leave(object sender, EventArgs e)
        {
            if (mskTitularTB013_CPFCNPJ.Text== mskAcessoCPF.Text)
            {
                MessageBox.Show(MensagensDoSistema._0095, "Acesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

                if (lblAcessoId.Text.Trim() != Empty) return;

            Util validaCpf = new Util();
            if (validaCpf.CPF(mskAcessoCPF.Text))
                {
                lblAcessoEmailId.Text = "";
                lblAcessoId.Text = "";
                label72.Text = "";

                //Consultar base de dados                  
                var retorno = new PessoaNegocios().pessoaSelectCPFCNPJ(mskAcessoCPF.Text.Trim());
                    if (retorno.TB013_id > 0)
                    {
                        lblAcessoId.Text                    = retorno.TB013_id.ToString();
                        txtAcessoNomeCompleto.Text          = retorno.TB013_NomeCompleto;
                        daddtAcesssoDataNascimento.Value    =  Convert.ToDateTime(retorno.TB013_DataNascimento);
                        cmbAcessoSexo.SelectedValue         = retorno.TB013_SexoS;
                        mskAcessoCPF.Enabled = false;

                        PessoaController Acesso = new PessoaNegocios().AcessoPessoaFiltrar(retorno.TB013_id);
                        if(Acesso.TB040_id>0)
                        { label72.Text = Acesso.TB040_id.ToString(); }
                        else { label72.Text = ""; }
                   
                    var contato = new ContatoNegocios().contatoTipoEmailPessoa(retorno.TB013_id);

                    if(contato.Count>0)
                    {
                   
                        lblAcessoEmailId.Text= contato[0].TB009_id.ToString();
                        txtAcessoEmail.Text = contato[0].TB009_Contato.ToString();
                    }
                
                }
            }
        }
        private void mnuAcessosLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }
        private void Limpar()
        {
            mskAcessoCPF.Enabled = true;
            label72.Text = "";
            lblAcessoId.Text = "";
            lblAcessoEmailId.Text = "";
            mskAcessoCPF.Text = "";
            txtAcessoNomeCompleto.Text = "";
            txtAcessoEmail.Text = "";
        }
        private void ddgAcessos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mskAcessoCPF.Enabled = true;
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {      
                            try
                            {
                                PessoaController Acesso = new PessoaNegocios().AcessoPessoaFiltrar(Convert.ToInt64(ddgAcessos.Rows[e.RowIndex].Cells["AcessoId"].Value));
                                label72.Text = Acesso.TB040_id.ToString();
                                mskAcessoCPF.Text = Acesso.TB013_CPFCNPJ;
                                lblAcessoId.Text = Acesso.TB013_id.ToString();

                                txtAcessoNomeCompleto.Text = Acesso.TB013_NomeCompleto;
                                daddtAcesssoDataNascimento.Value = Acesso.TB013_DataNascimento;
                                cmbAcessoSexo.SelectedValue = Acesso.TB013_SexoS;
                                txtAcessoEmail.Text = Acesso.Contato.TB009_Contato;
                                lblAcessoEmailId.Text = Acesso.Contato.TB009_id.ToString();
                                mskAcessoCPF.Enabled = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuAcessosSalvar_Click(object sender, EventArgs e)
        {
            try
            {

             
                if (mskAcessoCPF.Text.Trim().Replace(",","").Replace(".", "").Replace("/", "").Replace("-", "") == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CPF"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mskAcessoCPF.Focus();
                    return ;
                }


                if (txtAcessoNomeCompleto.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Nome Completo"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAcessoNomeCompleto.Focus();
                    return;
                }


                PessoaController Acesso = new PessoaController();
                Acesso.Contato = new ContatoController();
                Acesso.Municipio = new MunicipioController();

                
                Acesso.TB013_CPFCNPJ                = mskAcessoCPF.Text;
                Acesso.TB013_NomeCompleto           = txtAcessoNomeCompleto.Text.TrimEnd().ToUpper();
                Acesso.TB013_NomeExibicao           = txtAcessoNomeCompleto.Text.TrimEnd().ToUpper();
                Acesso.TB013_DataNascimento         = daddtAcesssoDataNascimento.Value;
                Acesso.TB013_SexoS                  = cmbAcessoSexo.SelectedValue.ToString();             
                Acesso.TB013_CadastradoPor          = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Acesso.TB013_AlteradoPor            = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Acesso.TB012_Id                     = Convert.ToInt64(lblContrato.Text);
                Acesso.TB012_EraContezino           = 0;
                Acesso.TB013_StatusS                  = "1";
                Acesso.TB013_CodigoDependente       = 0000;
                Acesso.TB013_MaeNome                = "NÃO INFORMADO";
                Acesso.TB013_MaeDataNascimento      = Acesso.TB013_DataNascimento;
                Acesso.TB013_PaiNome                = "NÃO INFORMADO";
                Acesso.TB013_PaiDataNascimento      = Acesso.TB013_DataNascimento;
                Acesso.TB013_RG                     = "-";
                Acesso.TB013_RGOrgaoEmissor         = "-";
                Acesso.TB013_DeclaroSerMaiorCapaz   = 1;
                Acesso.TB013_TipoS                  = "1";
                Acesso.TB004_Cep                    = mskContratoCEP.Text;
                Acesso.Municipio.TB006_id           = Convert.ToInt64(label66.Text);
                Acesso.TB013_Logradouro             = txtContratoTB013Logradouro.Text;
                Acesso.TB013_Numero                 = txtContratoTB013Numero.Text;
                Acesso.TB013_Bairro                 = txtContratoTB013Bairro.Text;
                Acesso.TB013_Complemento            = txtContratoTB013Complemento.Text;
                Acesso.TB013_ListaNegra             = 0;
                Acesso.Contato.TB009_TipoS          = "3";
                Acesso.Contato.TB009_Contato        = txtAcessoEmail.Text.TrimEnd();
                Acesso.Contato.TB009_ExibirPortal   = 1;
                Acesso.Contato.TB009_Nota           = "Acesso ao contrato de do parceiro";
                Acesso.Contato.TB009_CadastradoPor  = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Acesso.Contato.TB009_AlteradoPor    = ParametrosInterface.objUsuarioLogado.TB011_Id;
                Acesso.Contato.TB020_id             = 0;

                if (lblAcessoId.Text.Trim() == string.Empty)
                {
                    Acesso.TB013_id = 0;
                   
                    lblAcessoId.Text = Convert.ToString(new PessoaNegocios().AcessoLiberar(Acesso));
                    MessageBox.Show(MensagensDoSistema._0094, "Acesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpar();
                }
                else
                {
                    Acesso.TB013_id = Convert.ToInt64(lblAcessoId.Text);
                    Acesso.Contato.Pessoa = new PessoaController();
                    Acesso.Contato.Pessoa.TB013_id = Acesso.TB013_id;
                    if (lblAcessoEmailId.Text.Trim() == string.Empty)
                    {
                        /*Se o usuario não possuir e-mail, cadastre-o*/
                        Acesso.Contato.TB009_id = new ContatoNegocios().contatosContratoInsert(Acesso.Contato, Acesso.TB012_Id, Acesso.Contato.TB009_CadastradoPor);
                        MessageBox.Show(MensagensDoSistema._0093, "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        /*Se possuir e-mail atualize*/
                    Acesso.Contato.TB009_id = Convert.ToInt64(lblAcessoEmailId.Text);
                    if (new ContatoNegocios().contatosContratoUpdate(Acesso.Contato))
                        {
                            MessageBox.Show(MensagensDoSistema._0092, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    /*Atualize dados da Pessoa*/
                    if(new PessoaNegocios().AtualizarDadosPessoaAcesso(Acesso))
                    {
                        /*Se não possuir vinculo, vincule-o*/
                        if (label72.Text.Trim() == string.Empty)
                        {
                            if(new PessoaNegocios().acessoVincular(Acesso))
                            {
                                MessageBox.Show(MensagensDoSistema._0094, "Acesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                            }
                        }
                        Limpar();
                    }




                   
                }
                CarregarAcessos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                pessoa.TB013_id = Convert.ToInt64(lblTitularTB013id.Text);
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
            lblParcelaPlanoId.Text      = parcela.TB015_id.ToString();
            lblParcelaPlano.Text        = parcela.TB015_Plano;
            lblParcelaId.Text           = parcela.TB016_id.ToString();
            ddtParcelaVencimento.Value  = parcela.TB016_Vencimento;
            lblParcelaValorTotal.Text   = Format("{0:C2}",
                txtNossoNumero.Text     = parcela.TB016_NossoNumero,
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
        private void ddtParcelaItens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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
                            break;
                    }
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
        private void pctParcelaDescontoConfirmar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Deseja alterar o valor do desconto no produto?", @"Parcelas", MessageBoxButtons.YesNo) ==    DialogResult.No)
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
                    new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
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
                        new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
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
        private void txtFiltroAssociado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    string tipoCampo= @"Exibição";

                    if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                    {
                        tipoCampo = @"Exibição";
                    }
                    else
                    {
                        if(this._validacoes.contemNumeros(txtFiltroAssociado.Text.Trim()))
                        {
                            if(txtFiltroAssociado.Text.Trim().Length==6)
                            {
                                tipoCampo = @"Contrato";
                            }
                            else
                            {
                                tipoCampo = @"Documento";
                            }
                        }
                    }

                    switch (tipoCampo)
                    {
                        case @"Exibição":
                            {
                                string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                                txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                                CarregarContratos(ListarContratos(vQuery));
                                break;
                            }
                        case @"Contrato":
                            {
                                string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                                txtFiltroAssociado.Text.TrimEnd().TrimStart();
                                CarregarContratos(ListarContratos(vQuery));
                                break;
                            }
                        case @"Documento":
                            {
                                string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                                    .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                    .Replace("-", "").Replace("/", "") + "'";
                                CarregarContratos(ListarContratos(vQuery));
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
        private void txtFiltroAssociado_Leave(object sender, EventArgs e)
        {
            try
            {
                string tipoCampo = @"Exibição";

                if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                {
                    tipoCampo = @"Exibição";
                }
                else
                {
                    if (this._validacoes.contemNumeros(txtFiltroAssociado.Text.Trim()))
                    {
                        if (txtFiltroAssociado.Text.Trim().Length == 6)
                        {
                            tipoCampo = @"Contrato";
                        }
                        else
                        {
                            tipoCampo = @"Documento";
                        }
                    }
                }

                switch (tipoCampo)
                {
                    case @"Exibição":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                            CarregarContratos(ListarContratos(vQuery));
                            break;
                        }
                    case @"Contrato":
                        {
                            string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart();
                            CarregarContratos(ListarContratos(vQuery));
                            break;
                        }
                    case @"Documento":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                                .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                .Replace("-", "").Replace("/", "") + "'";
                            CarregarContratos(ListarContratos(vQuery));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void picBoletoVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbFormaPagamento.SelectedValue) == 1)
                {
                    var boletosEmitidos = new ParcelaNegocios().BoletosParaImpressao(Convert.ToInt64(lblContrato.Text), Convert.ToInt64(cmbParcelaCiclo.Text));

                    tabPrincipal.TabPages.Add(tbBoletos);
                    tabPrincipal.TabPages.Remove(tbpParcelas);
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
        private void mnuPagamentoFechar_Click(object sender, EventArgs e)
        {
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
        private void mnuContratoDocumentos_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbpDocumentos);
            CarregarDocumentosContrato();
            tabPrincipal.TabPages.Remove(tbContrato);
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
        private void mnuDocumentosAnexarContrato_Click_1(object sender, EventArgs e)
        {
            gprAnexarTermo.Visible = true;
            label135.Text = "1";
            label134.Text = "";
        }
        private void mnuDocumentosFechar_Click_1(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbpDocumentos);
            tabPrincipal.TabPages.Add(tbContrato);
        }
        private void btnTermoLocalizar_Click_1(object sender, EventArgs e)
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
        private void btnTermoAnexar_Click_1(object sender, EventArgs e)
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
        private void btnTermoFechar_Click_1(object sender, EventArgs e)
        {
            gprAnexarTermo.Visible = false;
            label134.Text = "";
            axAcroPDF2.src = "";
            axAcroPDF2.Visible = false;
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
                    new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
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
        private void mnuParcelasReencaminharSICOOB_Click(object sender, EventArgs e)
        {
            try
            {
                for (var y = 0; y < ddtParcelas.RowCount; y++)
                {
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
        private void mnuImprimirAtual_Click(object sender, EventArgs e)
        {
            webBoleto.Print();
            MessageBox.Show(@"Documento enviado para impressora", @"Impressão", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void mnuBoletoImprimirTodos_Click_1(object sender, EventArgs e)
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
        private void mnuBoletoFechar_Click_1(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbpParcelas);
            tabPrincipal.TabPages.Remove(tbBoletos);

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
        private void mnuCarneFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbBoletos);
            tabPrincipal.TabPages.Remove(tbCarne);
        }
        private void mnuParcelaEditCancelarSelecionada_Click(object sender, EventArgs e)
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
        private void dividirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lblParcelaId.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0104, @"Erro", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return ;
            }

            if (Convert.ToInt16((int)((ParcelaController.TB016_StatusE)Enum.Parse(typeof(ParcelaController.TB016_StatusE), lblParcelaStatusS.Text))) > 1)
            {
                MessageBox.Show(MensagensDoSistema._0080, @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //if(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id==1)
            //{
                mskCPF.Text     = ParametrosInterface.objUsuarioLogado.TB011_CPF;
                txtSenha.Text   = ParametrosInterface.objUsuarioLogado.TB011_Senha.TrimEnd();
            //}

            bpbDividirParcela.Location = new Point(150, 90);
            cmbParcelasDividir.SelectedIndex = 0;           
            bpbDividirParcela.Visible = true;
            mskCPF.Focus();
        }
        private void btnConfirmarFechar_Click(object sender, EventArgs e)
        {
            bpbDividirParcela.Visible = false;
            mskCPF.Text = "";
            txtSenha.Text = "";

        }
        private void btnConfirmarDividir_Click(object sender, EventArgs e)
        {
            int adessao = 0;
            if (new UsuarioAPPNegocios().VerificaPrivilarioAcaoPontual(29, mskCPF.Text, txtSenha.Text) > 0)
            {

                ParcelaController parcelaMae= new  ParcelaNegocios().ParcelaPesquisaId(Convert.ToInt64(lblParcelaId.Text));
                
                List<ParcelaController> parcelas = new List<ParcelaController>();

                for (int i = 0; i <Convert.ToInt16(cmbParcelasDividir.Text); i++)
                {

                    ParcelaController parcelaFilha          = new ParcelaController();
                    parcelaFilha.ParcelaProduto_L           = new List<ParcelaProdutosController>();
                    parcelaFilha.Empresa                    = new EmpresaController();
                    parcelaFilha.Pessoa                     = new PessoaController();
                    parcelaFilha.Titular                    = new PessoaController();
                    parcelaFilha.Pessoa.Municipio           = new MunicipioController();
                    parcelaFilha.Pessoa.Municipio.Estado    = new EstadoController();

                    parcelaFilha.Plano                                  = parcelaMae.Plano;
                    parcelaFilha.Empresa                                = parcelaMae.Empresa;
                    parcelaFilha.Unidade                                = parcelaMae.Unidade;
                    parcelaFilha.TB033_Parcela                          = i+1;
                    parcelaFilha.TB016_TotalParcelas                    = Convert.ToInt16(cmbParcelasDividir.Text);
                    parcelaFilha.TB016_Emissao                          = DateTime.Now;
                   
                    if (i==0)
                    {
                        parcelaFilha.TB016_Vencimento                   = parcelaMae.TB016_Vencimento;
                    }
                    else
                    {
                        parcelaFilha.TB016_Vencimento                   = parcelaMae.TB016_Vencimento.AddMonths(i);
                    }
                    parcelaFilha.Empresa.TB001_id                       = parcelaMae.Empresa.TB001_id;
                    if(parcelaMae.TB002_ComissaoAdesao>0)
                    {
                        parcelaFilha.TB002_ComissaoAdesao               = parcelaMae.TB002_ComissaoAdesao/ Convert.ToInt16(cmbParcelasDividir.Text);
                    }
                    else
                    {
                        parcelaFilha.TB002_ComissaoAdesao               = 0;
                    }

                    parcelaFilha.TB016_DiaVencimento                    = parcelaMae.TB016_DiaVencimento;
                    parcelaFilha.TB016_DiaFechamento                    = parcelaMae.TB016_Vencimento.AddDays(-2).Day;
                    parcelaFilha.TB016_Beneficiario                     = parcelaMae.TB016_Beneficiario;
                    parcelaFilha.TB016_BeneficiarioCidade               = parcelaMae.TB016_BeneficiarioCidade;
                    parcelaFilha.TB016_BeneficiarioCPFCNPJ              = parcelaMae.TB016_BeneficiarioCPFCNPJ;
                    parcelaFilha.TB016_BeneficiarioEndereco             = parcelaMae.TB016_BeneficiarioEndereco;
                    parcelaFilha.TB016_BeneficiarioUF                   = parcelaMae.TB016_BeneficiarioUF;
                    parcelaFilha.TB016_CadastradoEm                     = DateTime.Now;
                    parcelaFilha.TB016_CadastradoPor                    = ParametrosInterface.objUsuarioLogado.TB011_Id; 
                    parcelaFilha.TB016_AlteradoEm                       = DateTime.Now;
                    parcelaFilha.TB016_AlteradoPor                      = ParametrosInterface.objUsuarioLogado.TB011_Id;
                    parcelaFilha.Pessoa.Municipio.TB006_Municipio       = parcelaMae.Titular.Municipio.TB006_Municipio;
                    parcelaFilha.Pessoa.Municipio.Estado.TB005_Estado   = parcelaMae.Titular.Estado.TB005_Sigla;
                    parcelaFilha.Titular.TB013_id                       = parcelaMae.Titular.TB013_id;
                    parcelaFilha.TB016_CPFCNPJ                          = parcelaMae.TB016_CPFCNPJ;
                    parcelaFilha.TB016_Pagador                          = parcelaMae.TB016_Pagador;
                    parcelaFilha.TB016_PagadorCEP                       = parcelaMae.TB016_PagadorCEP;
                    parcelaFilha.TB016_PagadorCidade                    = parcelaMae.TB016_PagadorCidade;
                    parcelaFilha.TB016_PagadorUF                        = parcelaMae.TB016_PagadorUF;
                    parcelaFilha.TB012_id                               = parcelaMae.TB012_id;
                    parcelaFilha.TB016_EnderecoPagador                  = parcelaMae.TB016_EnderecoPagador;
                    parcelaFilha.TB012_CicloContrato                    = parcelaMae.TB012_CicloContrato;
                    parcelaFilha.TB016_FormaPagamentoS                  = parcelaMae.TB016_FormaPagamentoS;
                    parcelaFilha.TB016_EmitirBoleto                     = 1;
                    parcelaFilha.TB016_StatusS                          = parcelaMae.TB016_StatusS;
                    parcelaFilha.Plano.TB015_Maiores                    = parcelaMae.Plano.TB015_Maiores;
                    parcelaFilha.Plano.TB015_Menores                    = parcelaMae.Plano.TB015_Menores;
                    parcelaFilha.Plano.TB015_Isentos                    = parcelaMae.Plano.TB015_Isentos;
                    parcelaFilha.Plano.TB015_IOF                        = parcelaMae.TB016_IOF;
                    parcelaFilha.TB016_TipoVencimento                   = parcelaMae.TB016_TipoVencimento;
                    parcelaFilha.TB016_EspecieDocumento                 = parcelaMae.TB016_EspecieDocumento;
                    parcelaFilha.TB016_BoletoDesc1                      = parcelaMae.TB016_BoletoDesc1;
                    parcelaFilha.TB016_BoletoDesc2                      = parcelaMae.TB016_BoletoDesc2;
                    parcelaFilha.TB016_BoletoDesc3                      = parcelaMae.TB016_BoletoDesc3;
                    parcelaFilha.TB016_BoletoDesc4                      = parcelaMae.TB016_BoletoDesc4;
                    parcelaFilha.TB016_BoletoDesc5                      = parcelaMae.TB016_BoletoDesc5;
                    parcelaFilha.TB016_Multa                            = parcelaMae.TB016_Multa;
                    parcelaFilha.TB016_Juros                            = parcelaMae.TB016_Juros;
                    parcelaFilha.TB016_TipoSacadoS                      = parcelaMae.TB016_TipoSacadoS;
                    parcelaFilha.TB031_TipoVencimento                   = parcelaMae.TB031_TipoVencimento;                                   
                    parcelaFilha.TB037_Id                               = parcelaMae.TB037_Id;
                    parcelaFilha.TB037_Comissao                         = parcelaMae.TB037_Comissao;
                    parcelaFilha.TB015_id                               = parcelaMae.TB015_id;
                    parcelaFilha.TB015_Plano                            = parcelaMae.TB015_Plano;
                    parcelaFilha.TB016_LoteExportacao                   = parcelaMae.TB016_LoteExportacao;
                    parcelaFilha.TB016_Entrada                          = parcelaMae.TB016_Entrada;
                    foreach (ParcelaProdutosController produtoparcelamae in parcelaMae.ParcelaProduto_L)
                    {
                        ParcelaProdutosController item = new ParcelaProdutosController();

                        if (adessao ==0 && Convert.ToInt16(produtoparcelamae.TB017_TipoS) == 1)
                        {
                            adessao = 1;
                            item.TB017_id               = produtoparcelamae.TB017_id;
                            item.TB017_IdProteus        = produtoparcelamae.TB017_IdProteus;
                            item.TB017_Item             = produtoparcelamae.TB017_Item;
                            item.TB017_Maior            = produtoparcelamae.TB017_Maior;
                            item.TB017_Menor            = produtoparcelamae.TB017_Menor;
                            item.TB017_Isento           = produtoparcelamae.TB017_Isento;
                            item.TB017_TipoS            = produtoparcelamae.TB017_TipoS;
                            item.TB017_ValorUnitario    = produtoparcelamae.TB017_ValorUnitario;
                            item.TB017_ValorDesconto    = produtoparcelamae.TB017_ValorDesconto;
                            item.TB017_ValorFinal       = produtoparcelamae.TB017_ValorFinal;
                            parcelaFilha.ParcelaProduto_L.Add(item);                           
                        }
                        else
                        {
                            if (Convert.ToInt16(produtoparcelamae.TB017_TipoS) > 1)
                            {
                                item.TB017_id               = produtoparcelamae.TB017_id;
                                item.TB017_IdProteus        = produtoparcelamae.TB017_IdProteus;
                                item.TB017_Item             = produtoparcelamae.TB017_Item;
                                item.TB017_Maior            = produtoparcelamae.TB017_Maior;
                                item.TB017_Menor            = produtoparcelamae.TB017_Menor;
                                item.TB017_Isento           = produtoparcelamae.TB017_Isento;
                                item.TB017_TipoS            = produtoparcelamae.TB017_TipoS;
                                item.TB017_ValorUnitario    = produtoparcelamae.TB017_ValorUnitario / Convert.ToInt16(cmbParcelasDividir.Text);
                                item.TB017_ValorDesconto    = produtoparcelamae.TB017_ValorDesconto / Convert.ToInt16(cmbParcelasDividir.Text);
                                item.TB017_ValorFinal       = produtoparcelamae.TB017_ValorFinal / Convert.ToInt16(cmbParcelasDividir.Text);
                                parcelaFilha.ParcelaProduto_L.Add(item);
                            }                      
                        }
                    }

                    double valor = 0;
                    foreach (var t in parcelaFilha.ParcelaProduto_L)
                    {
                        valor                                           = valor + t.TB017_ValorFinal;
                    }

                    parcelaFilha.TB016_Valor                            = valor;
                    if (parcelaMae.TB016_ValorBruto > 0)
                    {
                        parcelaFilha.TB016_ValorBruto                   = parcelaMae.TB016_ValorBruto / Convert.ToInt16(cmbParcelasDividir.Text);
                    }
                    else
                    {
                        parcelaFilha.TB016_ValorBruto                   = valor;
                    }

                    if (parcelaMae.TB016_ValorOutrosDesconto>0)
                    {
                        parcelaFilha.TB016_ValorOutrosDesconto          = parcelaMae.TB016_ValorOutrosDesconto / Convert.ToInt16(cmbParcelasDividir.Text);
                    }
                    else
                    {
                        parcelaFilha.TB016_ValorOutrosDesconto          = 0;
                    }

                    if(parcelaMae.TB016_ValorAdesao>0)
                    {
                        parcelaFilha.TB016_ValorAdesao                  = parcelaMae.TB016_ValorAdesao / Convert.ToInt16(cmbParcelasDividir.Text);
                    }
                    else
                    {
                        parcelaFilha.TB016_ValorAdesao                  = 0;
                    }
                    
                    if(parcelaMae.TB002_ComissaoAdesao>0)
                    {
                        parcelaFilha.TB002_ComissaoAdesao               = parcelaMae.TB002_ComissaoAdesao / Convert.ToInt16(cmbParcelasDividir.Text);
                    }
                    else
                    {
                        parcelaFilha.TB002_ComissaoAdesao               = 0;
                    }

                    if (parcelaMae.TB002_ComissaoMensalidade > 0)
                    {
                        parcelaFilha.TB002_ComissaoMensalidade          = parcelaMae.TB002_ComissaoMensalidade / Convert.ToInt16(cmbParcelasDividir.Text);
                    }
                    else
                    {
                        parcelaFilha.TB002_ComissaoMensalidade          = 0;
                    }
          
                    parcelas.Add(parcelaFilha);
                }
                if(new ParcelaNegocios().parceiroParcelaInsert(parcelas))
                {
                    new ParcelaNegocios().ParcelaCancela(Convert.ToInt64(lblParcelaId.Text), ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text));
                    ddtParcelas.DataSource =
                         new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                             Convert.ToInt64(lblCiclo.Text), -1);
                    ddtParcelas.Refresh();
                    bpbDividirParcela.Visible = false;
                }               
            }
            else
            {
                MessageBox.Show(MensagensDoSistema._0105, @"Acesso", MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
        }

        #region Texto
        private void mnuPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog1.Document  = printDocument1;
                PageSetupDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void PreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPreviewDialog1.Document = printDocument1;
                PrintPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void tbrFont_Click(object sender, EventArgs e)
        {
            SelectFontToolStripMenuItem_Click(this, e);
        }
        private void SelectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    fontDialog1.Font = rtbDoc.SelectionFont;
                }
                else
                {
                    fontDialog1.Font = null;
                }
                fontDialog1.ShowApply = true;
                if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionFont = fontDialog1.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void tspColor_Click(object sender, EventArgs e)
        {
            FontColorToolStripMenuItem_Click(this, new EventArgs());
        }
        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = rtbDoc.ForeColor;
                if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionColor = colorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void tbrLeft_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void tbrCenter_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void tbrRight_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void tbrBold_Click(object sender, EventArgs e)
        {
            BoldToolStripMenuItem_Click(this, e);
        }
        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void tbrItalic_Click(object sender, EventArgs e)
        {
            ItalicToolStripMenuItem_Click(this, e);
        }
        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void tbrUnderline_Click(object sender, EventArgs e)
        {
            UnderlineToolStripMenuItem_Click(this, e);
        }
        private void UnderlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void tbrFind_Click(object sender, EventArgs e)
        {
            //frmFind f = new frmFind(this);
            //f.Show();
        }
        private void mnuIndent0_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void mnuIndent5_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void mnuIndent10_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void mnuIndent15_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 15;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void mnuIndent20_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void LeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void CenterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void RightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }  
        private void InsertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "RTE - Inserir arquivo de imagem";
            openFileDialog1.DefaultExt = "rtf";
            openFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "")
            {
                return;
            }

            try
            {
                string strImagePath = openFileDialog1.FileName;
                Image img;
                img = Image.FromFile(strImagePath);
                Clipboard.SetDataObject(img);
                DataFormats.Format df;
                df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (this.rtbDoc.CanPaste(df))
                {
                    this.rtbDoc.Paste(df);
                }
            }
            catch
            {
                MessageBox.Show("Não foi possível inserir o formato da imagem selecionado.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Paste();
            }
            catch
            {
                MessageBox.Show("Não é possível copiar o conteúdo da prancheta para o documento.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Cut();
            }
            catch
            {
                MessageBox.Show("Não é possível cortar o conteúdo do documento.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Não é possível copiar o conteúdo do documento.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Não é possível selecionar todo o conteúdo do documento.", "RTE - Selecione", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FindAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmReplace f = new frmReplace(this);
            //    f.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), "Error");
            //}
        }
        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmFind f = new frmFind(this);
            //    f.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), "Error");
            //}
        }
        private void mnuRedo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanRedo)
                {
                    rtbDoc.Redo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void mnuUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanUndo)
                {
                    rtbDoc.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFile == string.Empty)
                {
                    SaveAsToolStripMenuItem_Click(this, e);
                    return;
                }

                try
                {
                    string strExt;
                    strExt = System.IO.Path.GetExtension(currentFile);
                    strExt = strExt.ToUpper();
                    if (strExt == ".RTF")
                    {
                        rtbDoc.SaveFile(currentFile);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(currentFile);
                        txtWriter.Write(rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    this.Text = "Editor: " + currentFile.ToString();
                    rtbDoc.Modified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "File Save Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog1.Title = "RTE - Save File";
                SaveFileDialog1.DefaultExt = "rtf";
                SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                SaveFileDialog1.FilterIndex = 1;

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (SaveFileDialog1.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(SaveFileDialog1.FileName);
                        txtWriter.Write(rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    currentFile = SaveFileDialog1.FileName;
                    rtbDoc.Modified = false;
                    this.Text = "Editor: " + currentFile.ToString();
                    MessageBox.Show(currentFile.ToString() + " saved.", "File Save");
                }
                else
                {
                    MessageBox.Show("Save File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save current file before opening another document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        rtbDoc.Modified = false;
                        OpenFile();
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, new EventArgs());
                        OpenFile();
                    }
                }
                else
                {
                    OpenFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        private void OpenFile()
        {
            try
            {
                openFileDialog1.Title = "RTE - Open File";
                openFileDialog1.DefaultExt = "rtf";
                openFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.FileName = string.Empty;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (openFileDialog1.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(openFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamReader txtReader;
                        txtReader = new System.IO.StreamReader(openFileDialog1.FileName);
                        rtbDoc.Text = txtReader.ReadToEnd();
                        txtReader.Close();
                        txtReader = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    currentFile = openFileDialog1.FileName;
                    rtbDoc.Modified = false;
                    this.Text = "Editor: " + currentFile.ToString();
                }
                else
                {
                    MessageBox.Show("Open File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        #endregion fim area texto

        private void parcelaAvulsaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnParcelaConfirmar.Enabled = true;
            dtParcelaAvulsaNParcelas.SelectedIndex = 0;
            gpbParcelaAvulsa.Visible = true;
        }

        private void btnParcelaFechar_Click(object sender, EventArgs e)
        {
            gpbParcelaAvulsa.Visible = false;
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


                //var parcelas = new ParcelaNegocios().GerarParcelasFamiliar(
                //    Convert.ToInt16(dtParcelaAvulsaNParcelas.Text),
                //    dtParcelaAvulsaVencimento.Value,
                //    Convert.ToInt16(cmbDiaVencimento.Text),
                //    ParametrosInterface.objUsuarioLogado.TB011_Id,
                //    Convert.ToInt64(lblContrato.Text),
                //    Convert.ToInt32(cmbParcelaCiclo.Text),
                //    -1,
                //    0,
                //    ParametrosInterface.objUsuarioLogado.TB037_Id,
                //    0

                //);

                var parcelas = new ParcelaNegocios().gerarParcelaParceiro(
                                                 Convert.ToInt64(lblContrato.Text)
                                                 , dtParcelaAvulsaVencimento.Value
                                                 , ParametrosInterface.objUsuarioLogado.TB011_Id
                                                 , Convert.ToInt32(cmbParcelaCiclo.Text)
                                                 , ParametrosInterface.objUsuarioLogado.TB037_Id
                                                 , 0
                                                 , -1
                                                 , 0
                                                 , TB013_Tipo
                                                  );

                // if (!new ParcelaNegocios().parceiroListaParcelasContrato(parcelas))
                //new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                //         Convert.ToInt64(lblCiclo.Text), -1);
                gpbParcelaAvulsa.Visible = false;
                    ddtParcelas.DataSource = new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                         Convert.ToInt64(lblCiclo.Text), -1);

                //new ParcelaNegocios().FamiliarListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                //    Convert.ToInt64(lblCiclo.Text), -1);
                ddtParcelas.Refresh();
                    gpbParcelaAvulsa.Visible = false;

            }
            catch (Exception ex)
            {
                btnParcelaConfirmar.Enabled = true;
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pctAlterarNossoNumero_Click(object sender, EventArgs e)
        {
            new ParcelaNegocios().atualizarnossonumero(Convert.ToInt64(lblParcelaId.Text), txtNossoNumero.Text, ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblContrato.Text));

            var parcelas =
                      new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
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

        private void cmbParcelaCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TB013_Tipo = Convert.ToInt16(cmbContratoTB013Tipo.SelectedValue);
                _tb012Status = Convert.ToInt16(cmbContratoStatus.SelectedValue);

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
                ListarParcelas(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(cmbParcelaCiclo.Text));

                if (new ParcelaNegocios().parcelasGeradasContrato(Convert.ToInt64(lblContrato.Text)) == 0)
                {
                    mnuParcelaGerarParcelas.Visible = true;
                    mnuParcelaGerarParcelas.Enabled = true;
                }

                ddtParcelas.DataSource =
                     new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                         Convert.ToInt64(cmbParcelaCiclo.Text), -1);
                ddtParcelas.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TB013_Tipo = Convert.ToInt16(cmbContratoTB013Tipo.SelectedValue);
                _tb012Status = Convert.ToInt16(cmbContratoStatus.SelectedValue);

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
                ListarParcelas(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(cmbParcelaCiclo.Text));

                if (new ParcelaNegocios().parcelasGeradasContrato(Convert.ToInt64(lblContrato.Text)) == 0)
                {
                    mnuParcelaGerarParcelas.Visible = true;
                    mnuParcelaGerarParcelas.Enabled = true;
                }

                ddtParcelas.DataSource =
                     new ParcelaNegocios().parceiroListaParcelasContrato(Convert.ToInt64(lblContrato.Text),
                         Convert.ToInt64(cmbParcelaCiclo.Text), -1);
                ddtParcelas.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
