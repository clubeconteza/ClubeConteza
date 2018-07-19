using Negocios;
using System;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmCorporativoNovo : Form
    {
        public UnidadesViewModelNegocios ViewModel { get; set; }

        public frmCorporativoNovo()
        {
            InitializeComponent();
            ViewModel = new UnidadesViewModelNegocios();
            tabCorporativo.TabPages.Remove(tabCorporativoEmpresa);

            cboFiltro.DataSource = ViewModel.ListarCorporativoFiltro();

            cboStatusContrato.DataSource = ViewModel.ListarContratosStatus();
            cboSexo.DataSource = ViewModel.ListarPessoasSexoTitular();

            dgdListaCorporativo.DataSource = ViewModel.ListarCorporativo(cboFiltro.SelectedItem.ToString(), txtFiltro.Text);
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro.Enabled = ViewModel.SelecionarFiltroTodos(cboFiltro.SelectedItem.ToString());
            txtFiltro.Text = ViewModel.SelecionarFiltroTodos(cboFiltro.SelectedItem.ToString(), txtFiltro.Text);

            if (ViewModel.SelecionarFiltroStatus(cboFiltro.SelectedItem.ToString()))
            {
                txtFiltro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtFiltro.AutoCompleteSource = AutoCompleteSource.CustomSource;
                var dados = new AutoCompleteStringCollection();
                dados.AddRange(ViewModel.ListarUnidadesStatusFiltro().ToArray());
                txtFiltro.AutoCompleteCustomSource = dados;
            }
            else
            {
                txtFiltro.AutoCompleteMode = AutoCompleteMode.None;
                txtFiltro.AutoCompleteSource = AutoCompleteSource.None;
                txtFiltro.AutoCompleteCustomSource = null;
            }
        }

        private void picFiltrarLista_Click(object sender, EventArgs e)
        {
            dgdListaCorporativo.DataSource = ViewModel.ListarCorporativo(cboFiltro.SelectedItem.ToString(), txtFiltro.Text);
        }

        private void dgdListaCorporativo_DoubleClick(object sender, EventArgs e)
        {
            tabCorporativo.TabPages.Add(tabCorporativoEmpresa);
            tabCorporativo.TabPages.Remove(tabCorporativoLista);
        }

        private void mnuListaNovo_Click(object sender, EventArgs e)
        {
            cboPais.DataSource = ViewModel.ListarPais();
            cboEstado.DataSource = ViewModel.ListarEstadosPorPais(0);
            cboEstado.Enabled = false;
            cboMunicipio.DataSource = ViewModel.ListarMunicipiosPorEstado(0);
            cboMunicipio.Enabled = false;

            cboPaisTitular.DataSource = ViewModel.ListarPais();
            cboEstadoTitular.DataSource = ViewModel.ListarEstadosPorPais(0);
            cboEstadoTitular.Enabled = false;
            cboMunicipioTitular.DataSource = ViewModel.ListarMunicipiosPorEstado(0);
            cboMunicipioTitular.Enabled = false;

            tabCorporativo.TabPages.Add(tabCorporativoEmpresa);
            tabCorporativo.TabPages.Remove(tabCorporativoLista);
        }

        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstado.DataSource = ViewModel.ListarEstadosPorPais(cboPais.SelectedValue != null ? (long)cboPais.SelectedValue : 0);
            cboEstado.Enabled = cboPais.SelectedIndex > 0;
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMunicipio.DataSource = ViewModel.ListarMunicipiosPorEstado(cboEstado.SelectedValue != null ? (long)cboEstado.SelectedValue : 0);
            cboMunicipio.Enabled = cboEstado.SelectedIndex > 0;
        }

        private void cboPaisTitular_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstadoTitular.DataSource = ViewModel.ListarEstadosPorPais(cboPaisTitular.SelectedValue != null ? (long)cboPaisTitular.SelectedValue : 0);
            cboEstadoTitular.Enabled = cboPaisTitular.SelectedIndex > 0;
        }

        private void cboEstadoTitular_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMunicipioTitular.DataSource = ViewModel.ListarMunicipiosPorEstado(cboEstadoTitular.SelectedValue != null ? (long)cboEstadoTitular.SelectedValue : 0);
            cboMunicipioTitular.Enabled = cboEstadoTitular.SelectedIndex > 0;
        }

        private void mnuEmpresaSalvar_Click(object sender, EventArgs e)
        {
            ViewModel.Contratos.Inicio = dteInicioContrato.Value.Date;
            ViewModel.Contratos.Fim = dteTerminoContrato.Value.Date;
            //Status;
            //Ciclo;
            ViewModel.Pessoas.CpfCnpj = txtCpf.Text;
            ViewModel.Pessoas.NomeCompleto = txtNomeCompleto.Text;
            ViewModel.Pessoas.NomeExibicao = txtNomeExibicao.Text;
            ViewModel.Pessoas.Sexo = ViewModel.BuscarSexoPessoaTitular(cboSexo.SelectedItem.ToString());
            ViewModel.Pessoas.DataNascimento = dteDataNascimento.Value.Date;
            ViewModel.Pessoas.Rg = txtRg.Text;
            ViewModel.Pessoas.RgOrgaoEmissor = txtOrgaoEmissor.Text;
            ViewModel.Pessoas.MaeNome = txtNomeMae.Text;
            ViewModel.Pessoas.MaeDataNascimento = dteDataNascimentoMae.Value.Date;
            ViewModel.Pessoas.PaiNome = txtNomePai.Text;
            ViewModel.Pessoas.PaiDataNascimento = dteDataNascimentoPai.Value.Date;
            ViewModel.Pessoas.Cep = !string.IsNullOrEmpty(txtCepTitular.Text) ? long.Parse(txtCepTitular.Text.Replace(" ", "")) : -1;
            ViewModel.Pessoas.Logradouro = txtLogradouroTitular.Text;
            ViewModel.Pessoas.Numero = txtNumeroTitular.Text;
            ViewModel.Pessoas.Complemento = txtComplementoTitular.Text;
            ViewModel.Pessoas.Bairro = txtBairroTitular.Text;
            ViewModel.Pessoas.IdMunicipio = cboMunicipioTitular.SelectedValue != null ? (long)cboMunicipioTitular.SelectedValue : 0;
            ViewModel.Unidades.Documento = txtCnpj.Text;
            ViewModel.Unidades.NomeFantasia = txtNomeFantasia.Text;
            ViewModel.Unidades.RazaoSocial = txtRazaoSocial.Text;
            ViewModel.Unidades.Cep = txtCep.Text;
            ViewModel.Unidades.Logradouro = txtLogradouro.Text;
            ViewModel.Unidades.Numero = txtNumero.Text;
            ViewModel.Unidades.Complemento = txtComplemento.Text;
            ViewModel.Unidades.Bairro = txtBairro.Text;
            ViewModel.Unidades.IdMunicipio = cboMunicipio.SelectedValue != null ? (long)cboMunicipio.SelectedValue : 0;

            var resultado = ViewModel.SalvarCorporativoEmpresa();

            MessageBox.Show(resultado["Mensagem"], resultado["Titulo"], MessageBoxButtons.OK, (MessageBoxIcon)int.Parse(resultado["Icone"]));
        }

        private void mnuEmpresaFechar_Click(object sender, EventArgs e)
        {
            //cboPais.DataSource = null;
            //cboEstado.DataSource = null;
            //cboMunicipio.DataSource = null;

            tabCorporativo.TabPages.Add(tabCorporativoLista);
            tabCorporativo.TabPages.Remove(tabCorporativoEmpresa);
        }
    }
}
