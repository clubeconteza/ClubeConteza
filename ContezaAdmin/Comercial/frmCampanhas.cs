using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.String;


namespace ContezaAdmin.Comercial
{
    public partial class frmCampanhas : Form
    {
        public frmCampanhas()
        {
            InitializeComponent();
  
        }

        private void frmCampanhas_Load(object sender, EventArgs e)
        {

            popularEstadosTitular();
            cmbSexo.SelectedIndex               = 0;
            cmbTipoContato.SelectedIndex        = 0;
            tbcPrincipal.TabPages.Remove(tbCampanha);
            tbCanais.TabPages.Remove(tbEmail);          
            grpFiltro.Visible                   = false;
            filtrarCampanhas();
            ddtSmsAgendamento.Value             = DateTime.Now.AddDays(+2);
        }

        private void filtrarCampanhas()
        {
           
            try
            {

                if(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id==1)
                {
                    /*Usuário Administrador*/
                    lblCampanhaId.Visible = true;
                }
                StringBuilder filtro = new StringBuilder();
                ddgCampanhas.AutoGenerateColumns = false;
                ddgCampanhas.DataSource = null;
                ddgCampanhas.DataSource = new CampanhaNegocios().Campanhas(filtro.ToString());
                ddgCampanhas.Refresh();
                cmbSmsVariaveis.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuPrincipalFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void popularEstadosTitular()
        {
            cmbEstado.DataSource = null;
            cmbEstado.Items.Clear();
            try
            {
                cmbEstado.DataSource     = new EnderecoNegocios().estadoAtivos();
                cmbEstado.DisplayMember  = "TB005_Estado";
                cmbEstado.ValueMember    = "TB005_Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                carregarMunicipios(Convert.ToInt64(cmbEstado.SelectedValue));
            }
            catch (Exception)
            {
               // MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregarMunicipios(long TB005_Id)
        {
            cmbMunicipio.DataSource = null;
            cmbMunicipio.Items.Clear();
            try
            {
                cmbMunicipio.DataSource = new EnderecoNegocios().municipiosAtivosPorEstado(TB005_Id);
                cmbMunicipio.DisplayMember = "TB006_Municipio";
                cmbMunicipio.ValueMember = "TB006_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcbFiltrarPessoas_Click(object sender, EventArgs e)
        {
            chkSelecionarTodos.Checked = false;
            var sSql = new StringBuilder();

            if(chkTitulares.Checked)
            { 
                sSql.Append(" AND TB013_CodigoDependente = 1001 ");
            }

            if(cmbSexo.SelectedIndex>0)
            {
                if (cmbSexo.SelectedIndex ==1)
                {
                    sSql.Append(" AND dbo.TB013_Pessoa.TB013_Sexo = 1 ");
                }
                else
                {
                    sSql.Append(" AND dbo.TB013_Pessoa.TB013_Sexo = 2 ");
                }
            }


            if (cmbTipoContato.SelectedIndex == 0)
            {
                sSql.Append(" AND (dbo.View_Contato_Tipo3.Expr1 > 0) ");
            }
            else
            {
                if (cmbTipoContato.SelectedIndex == 1)
                {
                    sSql.Append(" AND (dbo.View_Contato_Tipo1.Expr1 > 0) ");
                }
                else
                {
                    if (cmbTipoContato.SelectedIndex == 2)
                    {
                        sSql.Append(" AND (dbo.View_Contato_Tipo2.Expr1> 0) ");
                    }
                }
            }

            dgvPessoasEncontradas.AutoGenerateColumns = false;
            dgvPessoasEncontradas.DataSource = null;
            dgvPessoasEncontradas.DataSource = new PessoaNegocios().comercialFiltroEmail(Convert.ToInt64(cmbMunicipio.SelectedValue), ddtInicio.Value, ddtFim.Value, sSql.ToString());
            dgvPessoasEncontradas.Refresh();
            lblTotalRetornados.Text = @"Registros encontratados = " + dgvPessoasEncontradas.RowCount.ToString();
        }

        private void limparCamposCampanha()
        {
            txtCampanha.Text                = "";
            txtCampanhaContrato.Text        = "";
            lblCampanhaStatus.Text          = "";
            txtSmsAssunto.Text              = "";
            txtCampanhaContrato.Text        = "";
            txtSmsConteudo.Text             = "";
            lblSmsContadorCaracteres.Text   = "140 caracteres disponiveis";
      
            grpFiltro.Visible = false;
        }


        private void mnuCampanhaFechar_Click(object sender, EventArgs e)
        {
            limparCamposCampanha();
            tbcPrincipal.TabPages.Remove(tbCampanha);

            tbcPrincipal.TabPages.Add(tbLista);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbcPrincipal.TabPages.Remove(tbLista);
            tbcPrincipal.TabPages.Add(tbCampanha);
        }

        private void mnuCampanhaAlvos_Click(object sender, EventArgs e)
        {
            grpFiltro.Location = new Point(9, 75);
            grpFiltro.Visible = true;
        }

        private void mnuFiltrosFechar_Click(object sender, EventArgs e)
        {
            grpFiltro.Visible = false;

        }

        private void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int y = 0; y < dgvPessoasEncontradas.RowCount; y++)
            {
                dgvPessoasEncontradas.Rows[y].Cells["lSelecionar"].Value = chkSelecionarTodos.Checked;
            }
        }

        private void mnuFiltrosConfirmar_Click(object sender, EventArgs e)
        {
            txtCampanha.Focus();
            try
            {
                //List<MensagemController> deleterMensagem = new List<MensagemController>();
                List<MensagemController> incluirMensagem = new List<MensagemController>();
                
                incluirMensagem.Clear();

                for (var y = 0; y < dgvPessoasEncontradas.RowCount; y++)
                {
                    if (!Convert.ToBoolean(dgvPessoasEncontradas.Rows[y].Cells["lSelecionar"].Value))
                    {
                        new MensagemNegocios().MensagemExcluir(Convert.ToInt64(lblCampanhaId.Text), Convert.ToInt64(dgvPessoasEncontradas.Rows[y].Cells["fTB013_id"].Value), Convert.ToInt64(dgvPessoasEncontradas.Rows[y].Cells["fTB012_Id"].Value), ParametrosInterface.objUsuarioLogado.TB011_Id);
                    }
                    else
                    {
                        MensagemController obj  = new MensagemController();
                        obj.TB041_id            = Convert.ToInt64(lblCampanhaId.Text);
                        obj.TB012_id            = Convert.ToInt64(dgvPessoasEncontradas.Rows[y].Cells["fTB012_Id"].Value);
                        obj.TB013_id            = Convert.ToInt64(dgvPessoasEncontradas.Rows[y].Cells["fTB013_id"].Value);
                        obj.TB009_id            = Convert.ToInt64(dgvPessoasEncontradas.Rows[y].Cells["IdCelular"].Value);
                        obj.TB009_TipoI         = 1;
                        obj.TB039_Assunto       = txtSmsAssunto.Text.TrimEnd();
                        obj.TB039_Agendamento   = ddtSmsAgendamento.Value;
                        obj.TB039_Conteudo = txtSmsConteudo.Text.TrimEnd();
                        obj.TB012_StatusI       = 0;
                        obj.TB009_id            = Convert.ToInt64(dgvPessoasEncontradas.Rows[y].Cells["IdCelular"].Value);

                        incluirMensagem.Add(obj);
                    }
                }
                /*Incluir na lista*/
                new MensagemNegocios().MensagemIncluir(incluirMensagem);
            }  
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }


        private void carregarAlvosEmail()
        {

        }
        private void carregarAlvosSMS()
        {

        }
        /****************************************************************/
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

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmFind f = new frmFind(this);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void FindAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmReplace f = new frmReplace(this);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
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

        private void InsertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Title = "RTE - Inserir arquivo de imagem";
            OpenFileDialog1.DefaultExt = "rtf";
            OpenFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif";
            OpenFileDialog1.FilterIndex = 1;
            OpenFileDialog1.ShowDialog();

            if (OpenFileDialog1.FileName == "")
            {
                return;
            }

            try
            {
                string strImagePath = OpenFileDialog1.FileName;
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

        private void SelectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    FontDialog1.Font = rtbDoc.SelectionFont;
                }
                else
                {
                    FontDialog1.Font = null;
                }
                FontDialog1.ShowApply = true;
                if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionFont = FontDialog1.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.ForeColor;
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
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

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;
                    newFontStyle = FontStyle.Regular;
                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void PageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.BackColor;
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.BackColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
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

        private void AddBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.BulletIndent = 10;
                rtbDoc.SelectionBullet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void RemoveBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionBullet = false;
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

        private void tspColor_Click(object sender, EventArgs e)
        {
            FontColorToolStripMenuItem_Click(this, new EventArgs());
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

        private void tbrItalic_Click(object sender, EventArgs e)
        {
            ItalicToolStripMenuItem_Click(this, e);
        }

        private void tbrUnderline_Click(object sender, EventArgs e)
        {
            UnderlineToolStripMenuItem_Click(this, e);
        }

        private void tbrFind_Click(object sender, EventArgs e)
        {
            frmFind f = new frmFind(this);
            f.Show();
        }

        private void frmCampanhas_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    if (rtbDoc.Modified == true)
            //    {
            //        System.Windows.Forms.DialogResult answer;
            //        answer = MessageBox.Show("Save current document before exiting?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (answer == System.Windows.Forms.DialogResult.No)
            //        {
            //            rtbDoc.Modified = false;
            //            rtbDoc.Clear();
            //            return;
            //        }
            //        else
            //        {
            //            //SaveToolStripMenuItem_Click(this, new EventArgs());
            //        }
            //    }
            //    else
            //    {
            //        rtbDoc.Clear();
            //    }
            //    currentFile = "";
            //    this.Text = "Editor: New Document";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), "Error");
            //}
        }

        private void mnuPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog1.Document = PrintDocument1;
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
                PrintPreviewDialog1.Document = PrintDocument1;
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
                PrintDialog1.Document = PrintDocument1;
                if (PrintDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PrintDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void ddgCampanhas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    var campanha = new CampanhaNegocios().campanha(Convert.ToInt64(ddgCampanhas.Rows[e.RowIndex].Cells["lTB041_id"].Value));
                    
                    if(campanha.TB041_id>0)
                    {
                        txtCampanhaContrato.Text    = campanha.TB012_id.ToString();
                        lblCampanhaId.Text          = campanha.TB041_id.ToString();
                        txtCampanha.Text            = campanha.TB041_Campanha.TrimEnd();
                        dtmCampanhaInicio.Value     = campanha.TB041_Inicio;
                        dtmCampanhaFim.Value        = campanha.TB041_Fim;
                        lblCampanhaStatus.Text      = campanha.TB041_StatusS;
                        txtSmsAssunto.Text          = campanha.TB041_SmsAssunto;
                        txtSmsConteudo.Text         = campanha.TB041_SmsConteudo;
                        ddtSmsAgendamento.Value     = campanha.TB041_SmsAgendamento;
                        mnuCampanhaAlvos.Enabled    = true;

                        tbcPrincipal.TabPages.Remove(tbLista);
                        tbcPrincipal.TabPages.Add(tbCampanha);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pctSmsVariavel_Click(object sender, EventArgs e)
        {
            
                var texto = txtSmsConteudo.Text + " $" + cmbSmsVariaveis.Text.Trim();
                txtSmsConteudo.Text = texto.ToString();
                pctSmsVariavel.Focus();



        }

        private void txtSmsConteudo_TextChanged(object sender, EventArgs e)
        {
         
            lblSmsContadorCaracteres.Text = Convert.ToString( 140 - txtSmsConteudo.TextLength) + " Caracteres disponiveis";
        }

        private void mnuSmsSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCampanhaId.Text.Trim() == Empty)
                {
                    /*Salvar Campanha*/
                }

                if (txtSmsAssunto.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Assunto"), @"Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ;
                }

                if (txtSmsConteudo.Text.Trim() == Empty)
                {
                    MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Conteudo"), @"Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtCampanhaContrato.Text.Trim() == Empty)
                {
                    txtCampanhaContrato.Text = "0";
                }

              


                CampanhaController sms = new CampanhaController();              
                sms.TB041_id                = Convert.ToInt64(lblCampanhaId.Text);
                sms.TB041_Sms               = 1;
                sms.TB041_SmsAssunto        = txtSmsAssunto.Text;
                sms.TB041_SmsConteudo       = txtSmsConteudo.Text;
                sms.TB041_SmsAgendamento    = ddtSmsAgendamento.Value;
                sms.TB041_AlteradoPor       = ParametrosInterface.objUsuarioLogado.TB011_Id;
                sms.TB012_id                = Convert.ToInt64(txtCampanhaContrato.Text);                  

                if(new CampanhaNegocios().campanhaUpdateCanalSms(sms))
                {
                    MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCampanhaSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}
