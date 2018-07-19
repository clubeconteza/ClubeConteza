using Controller;
using Controller.ServicesClient;
using Negocios;
using Negocios.ServicesClient.Vouchers;
using Negocios.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using static System.String;

namespace ContezaAdmin.Comercial
{
    public partial class frmMensalidadePremiada : Form
    {
        Util _validacoes = new Util();
        public frmMensalidadePremiada()
        {
            InitializeComponent();
        }
        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void mnuPremioSorteio_Click(object sender, EventArgs e)
        {




            var servico = new ServicoListaCuponsVouchersValidados(1, 2018);
            servico.Enviador = new RequisicaoVouchers();
            servico.Envia();
            var lista = Utility.GetObjectByJson<List<CuponsVouchersController>>(servico.Retorno);

            var strQuery = "";

            strQuery += " UPDATE ";
            strQuery += " TB012_Contratos ";
            strQuery += " SET ";
            strQuery += " TB012_TotalVoucher = 0,  ";
            strQuery += " TB012_TotalCupons = 0  ";

            for (int i = 0; i < lista.Count; i++)
            {
                strQuery += " UPDATE ";
                strQuery += " TB012_Contratos ";
                strQuery += " SET ";
                strQuery += string.Format(" TB012_TotalVoucher = '{0}' ,", lista[i].QuantidadeUsada.Vouchers);
                strQuery += string.Format(" TB012_TotalCupons  = '{0}' ", lista[i].QuantidadeUsada.Cupons);
                strQuery += " WHERE ";
                strQuery += string.Format(" TB012_id           = '{0}' ", lista[i].Contrato);
                strQuery += " ";
            }

            if (!new mensalidadePremiadaNegocios().atualizarconsumo(strQuery))
            {
                MessageBox.Show("Atualização consumo", @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if(Convert.ToInt16(cmbPremioStatus.SelectedValue)==3)
            {
                MessageBox.Show(MensagensDoSistema._0111, @"Premio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Convert.ToInt16(cmbPremioStatus.SelectedValue) == 4)
            {
                MessageBox.Show(MensagensDoSistema._0112, @"Premio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtPremio1.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "1º Bilhete"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!_validacoes.contemNumeros(txtPremio1.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0109, "1º Bilhete", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                return;
            }

            if (txtPremio2.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "2º Bilhete"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_validacoes.contemNumeros(txtPremio2.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0109, "2º Bilhete", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                return;
            }

            if (txtPremio3.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "3º Bilhete"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_validacoes.contemNumeros(txtPremio3.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0109, "3º Bilhete", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                return;
            }

            if (txtPremio4.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "4º Bilhete"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_validacoes.contemNumeros(txtPremio4.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0109, "4º Bilhete", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                return;
            }

            if (txtPremio5.Text.Trim() == Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "5º Bilhete"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_validacoes.contemNumeros(txtPremio5.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0109, "5º Bilhete", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                return;
            }

            if (txtPremio1.Text.Trim().Length !=5)
            {
                 MessageBox.Show(MensagensDoSistema._0110, @"1º Bilhete", MessageBoxButtons.OK,
                                                   MessageBoxIcon.Error);
                return;
            }
            if (txtPremio2.Text.Trim().Length != 5)
            {
                MessageBox.Show(MensagensDoSistema._0110, @"2º Bilhete", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
                return;
            }

            if (txtPremio3.Text.Trim().Length != 5)
            {
                MessageBox.Show(MensagensDoSistema._0110, @"3º Bilhete", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
                return;
            }

            if (txtPremio4.Text.Trim().Length != 5)
            {
                MessageBox.Show(MensagensDoSistema._0110, @"4º Bilhete", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
                return;
            }

            if (txtPremio5.Text.Trim().Length != 5)
            {
                MessageBox.Show(MensagensDoSistema._0110, @"5º Bilhete", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
                return;
            }

            txtNumeroDaSorte.Text = txtPremio1.Text.Substring(4,1) + txtPremio2.Text.Substring(4, 1)+ txtPremio3.Text.Substring(4, 1)+ txtPremio4.Text.Substring(4, 1) + txtPremio5.Text.Substring(4, 1);

            mensalidadePremiadaController sorteio = new mensalidadePremiadaNegocios().sorteio(Convert.ToInt64(txtNumeroDaSorte.Text), Convert.ToInt64(txtPremioPontMin.Text), Convert.ToInt64(txtPremioPontMax.Text));

            textBox1.Text                   = sorteio.TB012_id.ToString();
            textBox2.Text                   = sorteio.titular.TB013_NomeCompleto;
            textBox3.Text                   = sorteio.titular.Celular;
            textBox4.Text                   = sorteio.titular.fixo;
            textBox5.Text                   = sorteio.titular.email;
            textBox6.Text                   = sorteio.titular.TB013_Logradouro + " N.º " + sorteio.titular.TB013_Numero + ", CEP: " + sorteio.titular.TB004_Cep + ", Bairro: " + sorteio.titular.TB013_Bairro + " , UF: " + sorteio.titular.Estado.TB005_Sigla + ", Municipio: " + sorteio.titular.Municipio.TB006_Municipio;
            cmbPremioStatus.SelectedValue   = @"2";
        }
        private void frmMensalidadePremiada_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0027'. Você pode movê-la ou removê-la conforme necessário.
            this.dTRPT0027TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0027);
            carregarPremiacaoDoAno();
            tabPrincipal.TabPages.Remove(tbPremio);
            tabPrincipal.TabPages.Remove(tbComprovante);
            
            status();
        }
        private void status()
        {
            cmbPremioStatus.DataSource = null;
            cmbPremioStatus.Items.Clear();

            var Status = new List<KeyValuePair<string, string>>();
            var status = Enum.GetValues(typeof(mensalidadePremiadaController.TB042_StatusE));
            foreach (mensalidadePremiadaController.TB042_StatusE statu in status)
            {
                Status.Add(new KeyValuePair<string, string>(statu.ToString(), ((int)statu).ToString()));
            }

            cmbPremioStatus.DataSource = Status;
            cmbPremioStatus.DisplayMember = "Key";
            cmbPremioStatus.ValueMember = "Value";
        }
        private void ddtAnoSorteio_Leave(object sender, EventArgs e)
        {

            if(ParametrosInterface.objUsuarioLogado.Perfil.TB010_id==1)
            {
                txtPremioId.Visible = true;

            }
            tabPrincipal.TabPages.Remove(tbPremio);
            carregarPremiacaoDoAno();
        }
        private void carregarPremiacaoDoAno()
        {
            try
            {
                dgwLista.AutoGenerateColumns = false;

                dgwLista.DataSource = null;
                dgwLista.DataSource = new mensalidadePremiadaNegocios().listarpremios(ddtAnoSorteio.Value);
                dgwLista.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuPremioFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tpLista);
            tabPrincipal.TabPages.Remove(tbPremio);
            Limpar();

        }
        private void Limpar()
        {
            mnuPremioSalvar.Visible         = true;
            txtPremioId.Text                = @"";
            txtPremioVlrUni.Text            = @"R$ 0,00";
            comboBox1.Text                  = @"1";
            txtPremioVlrTotal.Text          = @"R$ 0,00";
            txtPremioPontMin.Text           = @"1";
            txtPremioPontMax.Text           = @"1";
            cmbPremioStatus.SelectedValue   = @"1";
            txtPremioDescricao.Text         = @"";       
            textBox1.Text                   = @"";
            txtNumeroDaSorte.Text           = @"";
            textBox2.Text                   = @"";
            textBox3.Text                   = @"";
            textBox4.Text                   = @"";
            textBox5.Text                   = @"";
            textBox6.Text                   = @"";
            //txtPremioConcurso.Text        = @"";
            //txtPremio1.Text               = @"";
            //txtPremio2.Text               = @"";
            //txtPremio3.Text               = @"";
            //txtPremio4.Text               = @"";
            //txtPremio5.Text               = @"";

            carregarPremiacaoDoAno();
        }
        private void dgwLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgwLista.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Id":
                            tabPrincipal.TabPages.Add(tbPremio);
                            tabPrincipal.TabPages.Remove(tpLista);
                            recuperarpremio(Convert.ToInt64(dgwLista.Rows[e.RowIndex].Cells["TB042_id"].Value));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                tabPrincipal.TabPages.Add(tpLista);
                tabPrincipal.TabPages.Remove(tbPremio);
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recuperarpremio(long id)
        {

            mnuPremioSorteio.Visible = false;
            mensalidadePremiadaController retorno   = new mensalidadePremiadaNegocios().item(id);

            if (retorno.TB042_id > 0)
            {             
                txtPremioId.Text                    = retorno.TB042_id.ToString();
                dtmPremioDataSorteio.Value          = retorno.TB042_DataSorteio;
                txtPremioVlrUni.Text                = Format("{0:C2}", Convert.ToDouble(retorno.TB042_VlrUni.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                comboBox1.SelectedItem              = retorno.TB042_Quantidade.ToString();
                txtPremioVlrTotal.Text              = Format("{0:C2}", Convert.ToDouble(retorno.TB042_VlrTotal.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                txtPremioPontMin.Text               = retorno.TB042_PontosMinimo.ToString();
                txtPremioPontMax.Text               = retorno.TB042_PontosMaximo.ToString();
                txtPremioDescricao.Text             = retorno.TB042_Descricao;
                cmbPremioStatus.SelectedValue       = retorno.TB042_StatusS;

                if(Convert.ToInt16(cmbPremioStatus.SelectedValue) ==1)
                {
                    mnuPremioSorteio.Visible = true;
                }
                if (Convert.ToInt16(cmbPremioStatus.SelectedValue) == 2)
                {
                    recuperarcontemplado(retorno.TB012_id);
                }
                if (Convert.ToInt16(cmbPremioStatus.SelectedValue) == 3)
                {
                    mnuPremioSalvar.Visible = false;
                    recuperarcontemplado(retorno.TB012_id);
                }
                if (Convert.ToInt16(cmbPremioStatus.SelectedValue) == 4)
                {
                    mnuPremioSalvar.Visible = false;
                }
            }
        }

        private void recuperarcontemplado(long contrato)
        {
            try
            {
                PessoaController contemplado = new mensalidadePremiadaNegocios().recuperacontemplado(contrato);
                if(contemplado.TB013_id>0)
                {
                    textBox1.Text = contemplado.TB012_Id.ToString();
                    textBox2.Text = contemplado.TB013_NomeCompleto;
                    textBox3.Text = contemplado.Celular;
                    textBox4.Text = contemplado.fixo;
                    textBox5.Text = contemplado.email;
                    textBox6.Text = contemplado.TB013_Logradouro + " N.º " + contemplado.TB013_Numero + ", CEP: " + contemplado.TB004_Cep + ", Bairro: " + contemplado.TB013_Bairro + " , UF: " + contemplado.Estado.TB005_Sigla + ", Municipio: " + contemplado.Municipio.TB006_Municipio;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mnuPremioSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbPremioStatus.SelectedValue) == 1)
                {
                    /*Salvar Premio*/
                    if (txtPremioId.Text.Trim() == Empty)
                    {
                       /*Cadastrar*/
                    }
                    else
                    {
                        /*Atualizar*/
                    }
                }
                else
                {
                    if (Convert.ToInt16(cmbPremioStatus.SelectedValue) == 2)
                    {
                        /*Salvar comtemplado*/
                        if (txtPremioConcurso.Text.Trim() == Empty)
                        {
                            MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Concurso"), @"Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (txtNumeroDaSorte.Text.Trim() == Empty)
                        {
                            MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Numero da Sorte"), @"Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (textBox1.Text.Trim() == Empty)
                        {
                            MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Contrato"), @"Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!_validacoes.contemNumeros(txtPremioConcurso.Text.Trim()))
                        {
                            MessageBox.Show(string.Format(MensagensDoSistema._0109, "Concurso"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        if (txtPremioConcurso.Text.Trim().Length != 5)
                        {
                            MessageBox.Show(MensagensDoSistema._0110, @"Concurso", MessageBoxButtons.OK,
                                                              MessageBoxIcon.Error);
                            return;
                        }


                        mensalidadePremiadaController contemplado = new mensalidadePremiadaController();

                        contemplado.TB042_AlteradoEm        = DateTime.Now;
                        contemplado.TB042_AlteradoPor       = ParametrosInterface.objUsuarioLogado.TB011_Id;
                        contemplado.TB042_Concurso          = txtPremioConcurso.Text;
                        contemplado.TB042_Bilhete1          = txtPremio1.Text;
                        contemplado.TB042_Bilhete2          = txtPremio2.Text;
                        contemplado.TB042_Bilhete3          = txtPremio3.Text;
                        contemplado.TB042_Bilhete4          = txtPremio4.Text;
                        contemplado.TB042_Bilhete5          = txtPremio5.Text;
                        contemplado.TB042_NumeroDaSorte     = txtNumeroDaSorte.Text;
                        contemplado.TB012_id                = Convert.ToInt64(textBox1.Text);
                        contemplado.TB042_id                = Convert.ToInt64(txtPremioId.Text);


                        if (new mensalidadePremiadaNegocios().contemplar(contemplado))
                        {
                            long id = contemplado.TB042_id;
                            Limpar();
                            recuperarpremio(id);

                            MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt16(cmbPremioStatus.SelectedValue) > 2)
                        {
                            /*Não permite edição*/
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void liberarSorteioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (new mensalidadePremiadaNegocios().prepararsorteio())
                {
                    MessageBox.Show("Sorteios Liberados", @"Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbPremio);
            tabPrincipal.TabPages.Remove(tbComprovante);
        }

        private void comprovanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbComprovante);
            tabPrincipal.TabPages.Remove(tbPremio);

            var sSql = new StringBuilder();



            sSql.Append(" SELECT dbo.TB042_SorteioMensalidadePremiada.TB042_id, dbo.TB042_SorteioMensalidadePremiada.TB042_Status, dbo.TB042_SorteioMensalidadePremiada.TB042_DataSorteio,  ");
            sSql.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_Descricao, dbo.TB042_SorteioMensalidadePremiada.TB042_VlrUni, dbo.TB042_SorteioMensalidadePremiada.TB042_Quantidade,  ");
            sSql.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_VlrTotal, dbo.TB042_SorteioMensalidadePremiada.TB042_Concurso, dbo.TB042_SorteioMensalidadePremiada.TB042_Bilhete1,  ");
            sSql.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_Bilhete2, dbo.TB042_SorteioMensalidadePremiada.TB042_Bilhete3, dbo.TB042_SorteioMensalidadePremiada.TB042_Bilhete4,  ");
            sSql.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_Bilhete5, dbo.TB042_SorteioMensalidadePremiada.TB042_NumeroDaSorte, dbo.TB012_Contratos.TB012_id, dbo.TB013_Pessoa.TB013_NomeCompleto,  ");
            sSql.Append(" dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_RG, dbo.TB013_Pessoa.TB013_RGOrgaoEmissor, dbo.TB013_Pessoa.TB013_Logradouro, dbo.TB013_Pessoa.TB013_Numero,  ");
            sSql.Append(" dbo.TB013_Pessoa.TB004_Cep, dbo.TB013_Pessoa.TB013_Bairro, dbo.TB013_Pessoa.TB013_Complemento, dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Sigla,  ");
            sSql.Append(" dbo.View_Contato_Tipo1.Expr2 AS Celular, dbo.View_Contato_Tipo2.Expr2 AS Fixo, dbo.View_Contato_Tipo3.Expr2 AS Email ");
            sSql.Append(" FROM dbo.TB042_SorteioMensalidadePremiada INNER JOIN ");
            sSql.Append(" dbo.TB012_Contratos ON dbo.TB042_SorteioMensalidadePremiada.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
            sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
            sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
            sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id LEFT OUTER JOIN ");
            sSql.Append(" dbo.View_Contato_Tipo3 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id LEFT OUTER JOIN ");
            sSql.Append(" dbo.View_Contato_Tipo2 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo2.TB013_id LEFT OUTER JOIN ");
            sSql.Append(" dbo.View_Contato_Tipo1 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo1.TB013_id ");
            sSql.Append(" WHERE ");
            sSql.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_id =  ");
            sSql.Append(txtPremioId.Text);
            sSql.Append("AND ");
            sSql.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_Status = 3 ");

            this.dTRPT0027TableAdapter.Adapter.SelectCommand.CommandText = sSql.ToString();

            try
            {
             

                this.dTRPT0027TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0027);
                rpwRPT0027.RefreshReport();

              
            }
            catch (Exception)
            {

            }
        }
    }
}
