using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmCartoes : Form
    {
        public frmCartoes()
        {
            InitializeComponent();
        }

        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCartoes_Load(object sender, EventArgs e)
        {
            UsuarioAPPNegocios Usuario_N = new UsuarioAPPNegocios();
            if (Usuario_N.VS() != Application.ProductVersion)
            {
                MessageBox.Show(string.Format(MensagensDoSistema._0051, Application.ProductVersion, ParametrosInterface.objUsuarioLogado.VS).ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tabPrincipal.TabPages.Remove(tbCartoesContrato);

            string vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 5";

            CarregarCartoes(vQuery);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            string vQuery = " ";
            if (cmbFiltroAssociado.SelectedItem.ToString() == "Disponivel para Entrega")
            {
                vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus =5";
                LSelecionar.Visible = false;
                pcbSelecionar.Visible = false;
            }
            else
            {
                if (cmbFiltroAssociado.SelectedItem.ToString() == "Enviada para Impressão")
                {
                    vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 2";
                    LSelecionar.Visible = true;
                    pcbSelecionar.Visible = true;
                }
                else
                {
                    if (cmbFiltroAssociado.SelectedItem.ToString() == "Status Gerada")
                    {
                        vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 1";
                        LSelecionar.Visible = true;
                        pcbSelecionar.Visible = true;
                    }
                    else
                    {
                        if (cmbFiltroAssociado.SelectedItem.ToString().TrimEnd() == "Entregue")
                        {
                            vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 4";
                            LSelecionar.Visible = true;
                            pcbSelecionar.Visible = true;
                        }
                        else
                        {
                            if (cmbFiltroAssociado.SelectedItem.ToString() == "Contrato")
                            {
                                vQuery = " AND dbo.TB013_Pessoa.TB012_id = " + Convert.ToInt64(txtFiltroAssociado.Text);
                                LSelecionar.Visible = true;
                                pcbSelecionar.Visible = true;
                            }
                            else
                            {
                                if (cmbFiltroAssociado.SelectedItem.ToString() == "Nome")
                                {
                                    vQuery = " AND dbo.TB013_Pessoa.TB013_NomeCompleto  LIKE'" + txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                                    LSelecionar.Visible = true;
                                    pcbSelecionar.Visible = true;
                                }
                                else
                                {
                                    if (cmbFiltroAssociado.SelectedItem.ToString() == "Status Gerada Manualmente")
                                    {
                                        vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 6";
                                        LSelecionar.Visible = true;
                                        pcbSelecionar.Visible = true;
                                    }
                                    else
                                    {
                                        if (cmbFiltroAssociado.SelectedItem.ToString() == "Gerados via Baxa Manual")
                                        {
                                            vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 7";
                                            LSelecionar.Visible = true;
                                            pcbSelecionar.Visible = true;
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CarregarCartoes(vQuery);
        }

        private void CarregarCartoes(string Query)
        {
            try
            {
                ddgCartoes.AutoGenerateColumns = false;
                PessoaNegocios Pessoa_N = new PessoaNegocios();
                ddgCartoes.DataSource = Pessoa_N.Cartoes(Query);
                ddgCartoes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcbSelecionar_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < ddgCartoes.RowCount; i++)
            {

                if (Convert.ToBoolean(ddgCartoes.Rows[i].Cells["LSelecionar"].Value) ==false)
                {
                    ddgCartoes.Rows[i].Cells["LSelecionar"].Value = true;
                }
                else
                {
                    ddgCartoes.Rows[i].Cells["LSelecionar"].Value = false;
                }
            }
        }

        private void ddgCartoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgCartoes.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":
                            try
                            {
                                tabPrincipal.TabPages.Add(tbCartoesContrato);
                                CartoesContrato(Convert.ToInt64(ddgCartoes.Rows[e.RowIndex].Cells["LTB012_Id"].Value));
                                tabPrincipal.TabPages.Remove(tbLista);
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

        private void CartoesContrato(Int64 TB012_id)
        {
            try
            {
                PessoaNegocios Pessoa_N = new PessoaNegocios();

                ddgCartoesContrato.AutoGenerateColumns = false;
                List<PessoaController> CartoesContrato = Pessoa_N.CartoesContrato(TB012_id);

                ddgCartoesContrato.DataSource = CartoesContrato;
                lblContrato.Text = CartoesContrato[0].Contrato.TB012_Id.ToString();
                lblStatusContrato.Text = CartoesContrato[0].Contrato.TB012_StatusS;

                ddgCartoesContrato.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCartoesFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbLista);
           
            tabPrincipal.TabPages.Remove(tbCartoesContrato);
        }

        private void ddgCartoesContrato_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgCartoes.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":
                            try
                            {
                                PessoaNegocios Pessoa_N = new PessoaNegocios();
                                PessoaController CartaoPessoa = Pessoa_N.CartaoPessoa(Convert.ToInt64(ddgCartoesContrato.Rows[e.RowIndex].Cells["cTB013_id"].Value));

                                lblPessoaNomeId.Text = CartaoPessoa.TB013_id.ToString();
                                lblPessoaNome.Text = CartaoPessoa.TB013_NomeCompleto;
                                lblPessoaStatus.Text = CartaoPessoa.TB013_StatusS;
                                lblPessoaCartao.Text = CartaoPessoa.TB013_Cartao;
                                lblPessoaCartaoStatus.Text = CartaoPessoa.TB013_CarteirinhaStatusS;
                                txtPessoaCartaoEntreguePara.Text = CartaoPessoa.TB013_CartaoEntreguePara;
                                lblPessoaCartaoEntregueEm.Text = CartaoPessoa.TB013_CartaoEntregueEm.ToShortDateString();


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

        private void mnuListaConfirmarRecebimento_Click(object sender, EventArgs e)
        {

            txtFiltroAssociado.Focus();

            try
            {
                PessoaNegocios Pessoa_N = new PessoaNegocios();
                int i;
                for (i = 0; i < ddgCartoes.RowCount; i++)
                {
                    if (Convert.ToBoolean(ddgCartoes.Rows[i].Cells["LSelecionar"].Value) == true)
                    {
                        Int64 TB013_id = Convert.ToInt64(ddgCartoes.Rows[i].Cells["LTB013_id"].Value);
                        Int64 TB012_id = Convert.ToInt64(ddgCartoes.Rows[i].Cells["LTB012_Id"].Value);
                        //
                        Pessoa_N.CartaoConfirmarRecebimento(TB013_id, ParametrosInterface.objUsuarioLogado.TB011_Id, TB012_id, ddgCartoes.Rows[i].Cells["LTB013_Cartao"].Value.ToString());                            
                    }
                }

                cmbFiltroAssociado.SelectedItem = "Disponivel para Entrega";
                string vQuery = " AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 5";

                CarregarCartoes(vQuery);

                LSelecionar.Visible = false;
                pcbSelecionar.Visible = false;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblPessoaCartaoStatus.Text.TrimEnd().Contains("Entregue"))
                {
                    MessageBox.Show(MensagensDoSistema._0042.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                { 
                    PessoaNegocios Pessoa_N = new PessoaNegocios();
                    if(Pessoa_N.CartaoConfirmarEntrega(Convert.ToInt64(lblPessoaNomeId.Text), Convert.ToInt64(lblContrato.Text), txtPessoaCartaoEntreguePara.Text.TrimEnd(),ParametrosInterface.objUsuarioLogado.TB011_Id, lblPessoaCartao.Text))
                    {

                        lblPessoaNomeId.Text = "";
                        lblPessoaNome.Text = "";
                        lblPessoaCartao.Text = "";
                        txtPessoaCartaoEntreguePara.Text = "";
                        lblPessoaStatus.Text = "";
                        lblPessoaCartaoStatus.Text = "";
                        lblPessoaCartaoEntregueEm.Text = "";


                        CartoesContrato(Convert.ToInt64(Convert.ToInt64(lblContrato.Text)));
                        MessageBox.Show(MensagensDoSistema._0018, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuListaEnviarParaImpressao_Click(object sender, EventArgs e)
        {
            if (cmbFiltroAssociado.SelectedItem.ToString() == "Status Gerada")
            {
                try
                {
                    PessoaNegocios Pessoa_N = new PessoaNegocios(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(MensagensDoSistema._0053, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
