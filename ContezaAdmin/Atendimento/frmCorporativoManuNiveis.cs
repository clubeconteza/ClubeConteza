using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmCorporativoManuNiveis : Form
    {
        public frmCorporativoManuNiveis()
        {
            InitializeComponent();
        }

        private void frmCorporativoManuNiveis_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            tabPrincipal.TabPages.Remove(tbCadastro);

            CarregarContratos();
        }

        private void CarregarContratos()
        {
            try
            {
                ddgLista.AutoGenerateColumns = false;


                CategoriaNegocios Categoria_N = new CategoriaNegocios();
                ddgLista.DataSource = Categoria_N.RetoranarLista();
                ddgLista.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ddgLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (ddgLista.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":
                            try
                            {
                                // 

                                CategoriaNegocios Categoria_N = new CategoriaNegocios();
                              
                                CategoriaController Categoria_C = Categoria_N.RetoranarCategoriaNivel1(Convert.ToInt64(ddgLista.Rows[e.RowIndex].Cells["TB021_id"].Value));

                                if(Categoria_C.TB021_id>0)
                                {

                                    CarregarSessao();
                                    lblNivel1Id.Text = Categoria_C.TB021_id.ToString();
                                    txtNivel1Desc.Text = Categoria_C.TB021_Descricao.ToString();
                               
                                    lsbNivel2.DataSource = Categoria_N.RetoranarcCategoriaNivel2(Categoria_C.TB021_id);
                                    lsbNivel2.DisplayMember = "TB022_Descricao";
                                    lsbNivel2.ValueMember = "TB022_id";
                                    GrpNivel2.Enabled = true;
                                    tabPrincipal.TabPages.Add(tbCadastro);
                                    tabPrincipal.TabPages.Remove(tbLista);

                                    CarregarSessaoVinculadaNivel1();

                                    txtNivel1Desc.Focus();
                                }
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

        private void lsbNivel2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                CategoriaNegocios Categoria_N = new CategoriaNegocios();


                CategoriaController Retorno = Categoria_N.RetornoItemNivel2(Convert.ToInt64(lsbNivel2.SelectedValue));
                lblNivel2Id.Text = Retorno.TB022_id.ToString();
                txtNivel2Desc.Text = Retorno.TB022_Descricao;




                lsbNivel3.DataSource = Categoria_N.RetoranarcCategoriaNivel3(Convert.ToInt64(lsbNivel2.SelectedValue));
                lsbNivel3.DisplayMember = "TB023_Descricao";
                lsbNivel3.ValueMember = "TB023_id";

                if(lsbNivel2.Items.Count>0)
                {
                    GrpNivel3.Enabled = true;
                }
                else
                {
                    GrpNivel3.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCadastroSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNivel1Desc.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Descrição do nivel 1 "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNivel1Desc.Focus();
                    return;
                }

                CategoriaNegocios   Registro_N = new CategoriaNegocios();
                CategoriaController Registro_C = new CategoriaController();

                Registro_C.TB021_Descricao = txtNivel1Desc.Text.TrimEnd();

                if (lblNivel1Id.Text.Trim()== string.Empty)
                {
                    //Novo
                    Int64 Id = Registro_N.IncluirNivel1(Registro_C);
                    if(Id>0)
                    {
                        lblNivel1Id.Text = Id.ToString();
                        MessageBox.Show(MensagensDoSistema._0017, "Incluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GrpNivel2.Enabled = true;
                    }
                }
                else
                {
                    //Atualização
                    Registro_C.TB021_id = Convert.ToInt64(lblNivel1Id.Text);


                    if(Registro_N.AtualizarNivel1(Registro_C))
                    {
                        MessageBox.Show(MensagensDoSistema._0018, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCadastroFechar_Click(object sender, EventArgs e)
        {
            
            txtNivel1Desc.Text  = "";
            lblNivel1Id.Text    = "";

            GrpNivel2.Enabled   = false;
            txtNivel2Desc.Text       = "";
            lblNivel2Id.Text         = "";

            GrpNivel3.Enabled   = false;
            txtNivel3Desc.Text       = "";
            lblNivel3d.Text         = "";

            tabPrincipal.TabPages.Add(tbLista);
            tabPrincipal.TabPages.Remove(tbCadastro);

            CarregarContratos();
        }

        private void mnuNivel2Limpar_Click(object sender, EventArgs e)
        {
            txtNivel2Desc.Text = "";
            lblNivel2Id.Text = "";
        }

        private void mnuNivel2Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNivel2Desc.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Descrição do nivel 2 "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNivel2Desc.Focus();
                    return;
                }

                CategoriaNegocios   Registro_N = new CategoriaNegocios();
                CategoriaController Registro_C = new CategoriaController();

                Registro_C.TB021_id         = Convert.ToInt64(lblNivel1Id.Text);
                Registro_C.TB022_Descricao  = txtNivel2Desc.Text.TrimEnd();        
                    
                if (lblNivel2Id.Text.Trim() == string.Empty)
                {
                    //Novo
                    Int64 Id = Registro_N.IncluirNivel2(Registro_C);
                    if (Id > 0)
                    {
                        lblNivel2Id.Text = Id.ToString();
                        MessageBox.Show(MensagensDoSistema._0017, "Incluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Atualização
                    Registro_C.TB022_id = Convert.ToInt64(lblNivel2Id.Text);
                    if (Registro_N.AtualizarNivel2(Registro_C))
                    {
                        MessageBox.Show(MensagensDoSistema._0018, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                lsbNivel2.DataSource    = Registro_N.RetoranarcCategoriaNivel2(Registro_C.TB021_id);
                lsbNivel2.DisplayMember = "TB022_Descricao";
                lsbNivel2.ValueMember   = "TB022_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuNivel3Limpar_Click(object sender, EventArgs e)
        {
            txtNivel3Desc.Text = "";
            lblNivel3d.Text = "";
        }

        private void mnuNivel3Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNivel3Desc.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace("_", "") == string.Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Descrição do nivel 3 "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNivel3Desc.Focus();
                    return;
                }

                CategoriaNegocios   Registro_N = new CategoriaNegocios();
                CategoriaController Registro_C = new CategoriaController();

                Registro_C.TB022_id = Convert.ToInt64(lblNivel2Id.Text);
                Registro_C.TB023_Descricao = txtNivel3Desc.Text.TrimEnd();

                if (lblNivel3d.Text.Trim() == string.Empty)
                {
                    //Novo
                    Int64 Id = Registro_N.IncluirNivel3(Registro_C);
                    if (Id > 0)
                    {
                        lblNivel3d.Text = Id.ToString();
                        MessageBox.Show(MensagensDoSistema._0017, "Incluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Atualização
                    Registro_C.TB023_id = Convert.ToInt64(lblNivel3d.Text);

                    if (Registro_N.AtualizarNivel3(Registro_C))
                    {
                        MessageBox.Show(MensagensDoSistema._0018, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            
                lsbNivel3.DataSource = Registro_N.RetoranarcCategoriaNivel3(Registro_C.TB022_id);
                lsbNivel3.DisplayMember = "TB023_Descricao";
                lsbNivel3.ValueMember = "TB023_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lsbNivel3_MouseClick(object sender, MouseEventArgs e)
        {
            CategoriaNegocios Registro_N = new CategoriaNegocios();

            CategoriaController Retorno = Registro_N.RetornoItemNivel3(Convert.ToInt64(lsbNivel3.SelectedValue));
            lblNivel3d.Text = Retorno.TB023_id.ToString();
            txtNivel3Desc.Text = Retorno.TB023_Descricao;
        }

        private void mnuListaNovo_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbCadastro);
            tabPrincipal.TabPages.Remove(tbLista);


            CarregarSessao();

            txtNivel1Desc.Focus();
        }

        private void fecharToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CarregarSessao()
        {
            try
            {

                CategoriaNegocios Sessao_N = new CategoriaNegocios();
                cmbSessao.DataSource    = Sessao_N.ListarSessao();
                cmbSessao.DisplayMember = "TB024_Sessao";
                cmbSessao.ValueMember   = "TB024_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarSessaoVinculadaNivel1()
        {
            try
            {

                CategoriaNegocios Sessao_N = new CategoriaNegocios();
                lsbSessoes.DataSource = Sessao_N.ListarSessaoNivel1(Convert.ToInt64(lblNivel1Id.Text));
                lsbSessoes.DisplayMember = "TB024_Sessao";
                lsbSessoes.ValueMember = "TB024_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSessaoVincular_Click(object sender, EventArgs e)
        {
            try
            {

                CategoriaNegocios Sessao_N = new CategoriaNegocios();
                if( Sessao_N.VincularSessaoNivel1(Convert.ToInt64(lblNivel1Id.Text),Convert.ToInt64(cmbSessao.SelectedValue)) >0)
                {
                    MessageBox.Show(MensagensDoSistema._0017, "Incluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                
                CarregarSessaoVinculadaNivel1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSessaoDesvincular_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format(MensagensDoSistema._0061, "\n" +  txtNivel1Desc.Text.ToString().TrimEnd() + "\n", "\n" + lsbSessoes.Text.ToString().TrimEnd()) , "Sessão", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CategoriaNegocios Sessao_N = new CategoriaNegocios();
                if (Sessao_N.DesvincularSessaoNivel1(Convert.ToInt64(lblNivel1Id.Text), Convert.ToInt64(lsbSessoes.SelectedValue)))
                {
                    MessageBox.Show(MensagensDoSistema._0018, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                CarregarSessaoVinculadaNivel1();
            }
        }
    }
}
