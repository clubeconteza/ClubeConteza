using Controller;
using DAO;
using Negocios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using static System.String;

namespace ContezaAdmin.Administrativo
{
    public partial class FrmLojas : Form
    {
        private long _id;
        public FrmLojas()
        {
            InitializeComponent();
            _id = 0;
        }

        private void FrmLojas_Load(object sender, EventArgs e)
        {
            tbPrincipal.TabPages.Remove(tbpPontoVenda);
            CarregarPontos();
            StatusDeContrato();
            AdesaoForma();
            MensalidadeForma();

        }

        private void CarregarPontos()
        {
            dgwLista.AutoGenerateColumns = false;

            dgwLista.DataSource = null;
            dgwLista.DataSource = new PontoDeVendaDao().PontoDeVendaLista();
            dgwLista.Refresh();
        }

        private void StatusDeContrato()
        {
            cmbPontoVendaStatus.DataSource = null;
            cmbPontoVendaStatus.Items.Clear();

            var pStatus = new List<KeyValuePair<string, string>>();
            var status = Enum.GetValues(typeof(PontoDeVendaController.Tb002StatusE));
            foreach (PontoDeVendaController.Tb002StatusE statu in status)
            {
                pStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbPontoVendaStatus.DataSource = pStatus;
            cmbPontoVendaStatus.DisplayMember = "Key";
            cmbPontoVendaStatus.ValueMember = "Value";
        }

        private void AdesaoForma()
        {
            cmbFamiliarComissaoAdesaoForma.DataSource = null;
            cmbFamiliarComissaoAdesaoForma.Items.Clear();
            cmbFamiliarComissaoMensalidadeForma.DataSource = null;
            cmbFamiliarComissaoMensalidadeForma.Items.Clear();

            cmbParceiroComissaoAdesaoForma.DataSource = null;
            cmbParceiroComissaoAdesaoForma.Items.Clear();
            cmbParceiroComissaoMensalidadeForma.DataSource = null;
            cmbParceiroComissaoMensalidadeForma.Items.Clear();

            cmbCorporativoComissaoAdesaoForma.DataSource = null;
            cmbCorporativoComissaoAdesaoForma.Items.Clear();
            cmbCorporativoComissaoMensalidadeForma.DataSource = null;
            cmbCorporativoComissaoMensalidadeForma.Items.Clear();



            var pStatus = new List<KeyValuePair<string, string>>();
            var status = Enum.GetValues(typeof(PontoDeVendaController.Tb002FamiliarAdesaoFormaE));
            foreach (PontoDeVendaController.Tb002FamiliarAdesaoFormaE statu in status)
            {
                pStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbFamiliarComissaoAdesaoForma.DataSource = pStatus;
            cmbFamiliarComissaoAdesaoForma.DisplayMember = "Key";
            cmbFamiliarComissaoAdesaoForma.ValueMember = "Value";
            cmbFamiliarComissaoMensalidadeForma.DataSource = pStatus;
            cmbFamiliarComissaoMensalidadeForma.DisplayMember = "Key";
            cmbFamiliarComissaoMensalidadeForma.ValueMember = "Value";


            cmbParceiroComissaoAdesaoForma.DataSource = pStatus;
            cmbParceiroComissaoAdesaoForma.DisplayMember = "Key";
            cmbParceiroComissaoAdesaoForma.ValueMember = "Value";
            cmbParceiroComissaoMensalidadeForma.DataSource = pStatus;
            cmbParceiroComissaoMensalidadeForma.DisplayMember = "Key";
            cmbParceiroComissaoMensalidadeForma.ValueMember = "Value";

            cmbCorporativoComissaoAdesaoForma.DataSource = pStatus;
            cmbCorporativoComissaoAdesaoForma.DisplayMember = "Key";
            cmbCorporativoComissaoAdesaoForma.ValueMember = "Value";
            cmbCorporativoComissaoMensalidadeForma.DataSource = pStatus;
            cmbCorporativoComissaoMensalidadeForma.DisplayMember = "Key";
            cmbCorporativoComissaoMensalidadeForma.ValueMember = "Value";
        }

        private void MensalidadeForma()
        {
            cmbFamiliarComissaoMensalidadeForma.DataSource = null;
            cmbFamiliarComissaoMensalidadeForma.Items.Clear();

            var pStatus = new List<KeyValuePair<string, string>>();
            var status = Enum.GetValues(typeof(PontoDeVendaController.Tb002FamiliarMensalidadeFormaE));
            foreach (PontoDeVendaController.Tb002FamiliarMensalidadeFormaE statu in status)
            {
                pStatus.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbFamiliarComissaoMensalidadeForma.DataSource = pStatus;
            cmbFamiliarComissaoMensalidadeForma.DisplayMember = "Key";
            cmbFamiliarComissaoMensalidadeForma.ValueMember = "Value";
        }

        private void CarregarEmpresa()
        {
            try
            {
                //var pontoDeVendaN = new PontoDeVendaNegocios();
                cmbEmpresa.DataSource = null;
                cmbEmpresa.Items.Clear();
                cmbEmpresa.DataSource = new EmpresaNegocios().Empresas();
                //pontoDeVendaN
                //    .PontosDeVendaLiberadosParaUsuario(ParametrosInterface.objUsuarioLogado).Tables[0];
                cmbEmpresa.DisplayMember = "TB001_NomeFantasia";
                cmbEmpresa.ValueMember = "TB001_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgwLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                switch (dgwLista.Columns[e.ColumnIndex].HeaderText)
                {
                    case "Ponto":
                        try
                        {
                            CarregarEmpresa();
                            var ponto = new PontoDeVendaNegocios().PontoDeVenda(Convert.ToInt64(dgwLista.Rows[e.RowIndex].Cells["TB002_id"].Value));

                            if (ponto.TB002_id > 0)
                            {

                                tbPrincipal.TabPages.Add(tbpPontoVenda);
                                _id = ponto.TB002_id;
                                cmbEmpresa.SelectedValue = ponto.Empresa.TB001_id;
                                txtPontoVenda.Text = ponto.TB002_Ponto;
                                cmbFamiliarComissaoAdesaoForma.SelectedValue = ponto.Tb002FamiliarAdesaoFormaS;
                                txtFamiliarComissaoAdesaoValor.Text = double.Parse(ponto.Tb002FamiliarAdesaoValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                                txtFamiliarComissaoAdesaoAliquota.Text = ponto.Tb002FamiliarAdesaoAliquota.ToString(CultureInfo.CurrentCulture);
                                cmbFamiliarComissaoMensalidadeForma.SelectedValue = ponto.Tb002FamiliarMensalidadeFormaS;
                                txtFamiliarComissaoMensalidadeValor.Text = double.Parse(ponto.Tb002FamiliarMensalidadeValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                                txtFamiliarComissaoMensalidadeAliquota.Text = ponto.Tb002FamiliarMensalidadeAliquota.ToString(CultureInfo.CurrentCulture);
                                cmbPontoVendaStatus.SelectedValue = ponto.Tb002StatusS;
                                cmbParceiroComissaoAdesaoForma.SelectedValue = ponto.Tb002ParceiroAdesaoFormaS;
                                txtParceiroComissaoAdesaoValor.Text = double.Parse(ponto.Tb002ParceiroAdesaoValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                                txtParceiroComissaoAdesaoAliquota.Text = ponto.Tb002ParceiroAdesaoAliquota.ToString(CultureInfo.CurrentCulture);
                                cmbParceiroComissaoMensalidadeForma.SelectedValue = ponto.Tb002ParceiroMensalidadeFormaS;
                                txtParceiroComissaoMensalidadeValor.Text = double.Parse(ponto.Tb002ParceiroMensalidadeValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                                txtParceiroComissaoMensalidadeAliquota.Text = ponto.Tb002ParceiroMensalidadeAliquota.ToString(CultureInfo.CurrentCulture);


                                cmbCorporativoComissaoAdesaoForma.SelectedValue = ponto.Tb002CorporativoAdesaoFormaS;
                                txtCorporativoComissaoAdesaoValor.Text = double.Parse(ponto.Tb002CorporativoAdesaoValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                                txtCorporativoComissaoAdesaoAliquota.Text = ponto.Tb002CorporativoAdesaoAliquota.ToString(CultureInfo.CurrentCulture);
                                cmbCorporativoComissaoMensalidadeForma.SelectedValue = ponto.Tb002CorporativoMensalidadeFormaS;
                                txtCorporativoComissaoMensalidadeValor.Text = double.Parse(ponto.Tb002CorporativoMensalidadeValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")).ToString("C2");
                                txtCorporativoComissaoMensalidadeAliquota.Text = ponto.Tb002CorporativoMensalidadeAliquota.ToString(CultureInfo.CurrentCulture);


                                tbPrincipal.TabPages.Remove(tbpLista);
                                CarregarUsuarios();
                                txtPontoVenda.Focus();
                            }
                        }
                        catch (Exception ex)
                        {
                            tbPrincipal.TabPages.Remove(tbpPontoVenda);
                            MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarUsuarios()
        {
            dgwUsuarios.AutoGenerateColumns = false;

            dgwUsuarios.DataSource = null;
            dgwUsuarios.DataSource = new PontoDeVendaDao().PontoDeVendaUsuariosComAcesso(_id);
            dgwUsuarios.Refresh();
        }

        private void LimparCampos()
        {
            _id = 0;
            txtPontoVenda.Text = "";
            txtFamiliarComissaoAdesaoValor.Text = @"R$ 0,00";
            txtFamiliarComissaoAdesaoAliquota.Text = @"0";
            txtFamiliarComissaoMensalidadeValor.Text = @"R$ 0,00";
            txtFamiliarComissaoMensalidadeAliquota.Text = @"0";
            cmbEmpresa.SelectedIndex = 0;
            cmbFamiliarComissaoAdesaoForma.SelectedIndex = 0;
            cmbFamiliarComissaoMensalidadeForma.SelectedIndex = 0;
            cmbPontoVendaStatus.SelectedIndex = 0;
            dgwUsuarios.AutoGenerateColumns = false;
            dgwUsuarios.DataSource = null;
            //dgwUsuarios.DataSource = new PontoDeVendaDao().AnotacoesDoContrato();
            dgwUsuarios.Refresh();
        }

        private void mnuPontoVendaFechar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            tbPrincipal.TabPages.Add(tbpLista);
            CarregarPontos();
            tbPrincipal.TabPages.Remove(tbpPontoVenda);

        }
        private void mnuListaNovo_Click(object sender, EventArgs e)
        {
            if (new UsuarioAPPNegocios().VS() != Application.ProductVersion)
            {
                MessageBox.Show(Format(MensagensDoSistema._0051, Application.ProductVersion,
                        ParametrosInterface.objUsuarioLogado.VS), @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            CarregarEmpresa();

            tbPrincipal.TabPages.Add(tbpPontoVenda);
            txtPontoVenda.Focus();

            tbPrincipal.TabPages.Remove(tbpLista);

        }

        private void mnuPontoVendaSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                var ponto = new PontoDeVendaController();
                ponto.Empresa = new EmpresaController();

                ponto.TB002_Ponto = txtPontoVenda.Text;
                ponto.Tb002FamiliarAdesaoFormaS = cmbFamiliarComissaoAdesaoForma.SelectedValue.ToString();
                ponto.Tb002FamiliarAdesaoValor = Convert.ToDouble(txtFamiliarComissaoAdesaoValor.Text.Replace("R$", ""));
                ponto.Tb002FamiliarAdesaoAliquota = Convert.ToDouble(txtFamiliarComissaoAdesaoAliquota.Text.Replace("%", ""));

                ponto.Tb002FamiliarMensalidadeFormaS = cmbFamiliarComissaoMensalidadeForma.SelectedValue.ToString();
                ponto.Tb002FamiliarMensalidadeValor = Convert.ToDouble(txtFamiliarComissaoMensalidadeValor.Text.Replace("R$", ""));
                ponto.Tb002FamiliarMensalidadeAliquota = Convert.ToDouble(txtFamiliarComissaoMensalidadeAliquota.Text.Replace("%", ""));

                ponto.Tb002StatusS = cmbPontoVendaStatus.SelectedValue.ToString();
                ponto.Empresa.TB001_id = Convert.ToInt64(cmbEmpresa.SelectedValue);
                ponto.Tb002CadastradoPorI = ParametrosInterface.objUsuarioLogado.TB011_Id;
                ponto.Tb002AlteradoPorI = ParametrosInterface.objUsuarioLogado.TB011_Id;

                ponto.Tb002ParceiroAdesaoFormaS = cmbParceiroComissaoAdesaoForma.SelectedValue.ToString();
                ponto.Tb002ParceiroAdesaoValor = Convert.ToDouble(txtParceiroComissaoAdesaoValor.Text.Replace("R$", ""));
                ponto.Tb002ParceiroAdesaoAliquota = Convert.ToDouble(txtParceiroComissaoAdesaoAliquota.Text.Replace("%", ""));

                ponto.Tb002ParceiroMensalidadeFormaS = cmbParceiroComissaoMensalidadeForma.SelectedValue.ToString();
                ponto.Tb002ParceiroMensalidadeValor = Convert.ToDouble(txtParceiroComissaoMensalidadeValor.Text.Replace("R$", ""));
                ponto.Tb002ParceiroMensalidadeAliquota = Convert.ToDouble(txtParceiroComissaoMensalidadeAliquota.Text.Replace("%", ""));

                ponto.Tb002CorporativoAdesaoFormaS = cmbCorporativoComissaoAdesaoForma.SelectedValue.ToString();
                ponto.Tb002CorporativoAdesaoValor = Convert.ToDouble(txtCorporativoComissaoAdesaoValor.Text.Replace("R$", ""));
                ponto.Tb002CorporativoAdesaoAliquota = Convert.ToDouble(txtCorporativoComissaoAdesaoAliquota.Text.Replace("%", ""));

                ponto.Tb002CorporativoMensalidadeFormaS = cmbCorporativoComissaoMensalidadeForma.SelectedValue.ToString();
                ponto.Tb002CorporativoMensalidadeValor = Convert.ToDouble(txtCorporativoComissaoMensalidadeValor.Text.Replace("R$", ""));
                ponto.Tb002CorporativoMensalidadeAliquota = Convert.ToDouble(txtCorporativoComissaoMensalidadeAliquota.Text.Replace("%", ""));


                if (_id > 0)
                {
                    /*Atualizar*/
                    ponto.TB002_id = _id;
                    if (new PontoDeVendaNegocios().PontoDeVendaAtualizar(ponto))
                    {
                        MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MessageBox.Show(@"Operação não pode ser completada", @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //    /*Cadastar*/

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgwLista_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                dgwLista.Cursor = Cursors.Hand;
            }
            else
            {
                dgwLista.Cursor = Cursors.Arrow;
            }
        }

        private void txtComissaoAdesaoValor_Leave(object sender, EventArgs e)
        {
            txtFamiliarComissaoAdesaoValor.Text = double.Parse(txtFamiliarComissaoAdesaoValor.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");
        }

        private void txtComissaoMensalidadeValor_Leave(object sender, EventArgs e)
        {
            txtFamiliarComissaoMensalidadeValor.Text = double.Parse(txtFamiliarComissaoMensalidadeValor.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");
        }

        private void dgwUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1 || e.ColumnIndex <= -1) return;
                switch (dgwUsuarios.Columns[e.ColumnIndex].HeaderText)
                {
                    case "Excluir":
                        try
                        {
                            if (MessageBox.Show(@"Deseja excluir o acesso deste usuário ao ponto de venda?", @"Acesso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (new PontoDeVendaNegocios().PontoDeVendaDeletarUsuariosComAcesso(
                                    _id,
                                    Convert.ToInt64(dgwUsuarios.Rows[e.RowIndex].Cells["TB011_Id"].Value),
                                    ParametrosInterface.objUsuarioLogado.TB011_Id))
                                {
                                    CarregarUsuarios();
                                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            tbPrincipal.TabPages.Remove(tbpPontoVenda);
                            MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtParceiroComissaoAdesaoValor_Leave(object sender, EventArgs e)
        {
            txtParceiroComissaoAdesaoValor.Text = double.Parse(txtParceiroComissaoAdesaoValor.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");
        }

        private void txtParceiroComissaoMensalidadeValor_Leave(object sender, EventArgs e)
        {
            txtParceiroComissaoMensalidadeValor.Text = double.Parse(txtParceiroComissaoMensalidadeValor.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");
        }

        private void txtCorporativoComissaoAdesaoValor_Leave(object sender, EventArgs e)
        {
            txtCorporativoComissaoAdesaoValor.Text = double.Parse(txtCorporativoComissaoAdesaoValor.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");
        }

        private void txtCorporativoComissaoMensalidadeValor_Leave(object sender, EventArgs e)
        {
            txtCorporativoComissaoMensalidadeValor.Text = double.Parse(txtCorporativoComissaoMensalidadeValor.Text.ToString(CultureInfo.InvariantCulture).Replace("R$", "")).ToString("C2");

        }
    }
}
