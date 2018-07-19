using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using System.Windows.Forms;

namespace ContezaAdmin.Financeiro
{
    public partial class frmrenovacao : Form
    {
        public frmrenovacao()
        {
            InitializeComponent();
        }

        private void frmrenovacao_Load(object sender, EventArgs e)
        {
            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);

            tabPrincipal.TabPages.Remove(tbExportar);
            


            cmbPesquisaTop.SelectedIndex = 0;

            PopularPesquisaTipoContrato();
            cmbPesquisaTipoContrato.SelectedIndex = 0;
            cmbPesquisaCiclo.SelectedIndex = 0;
        }

        private void PopularPesquisaTipoContrato()
        {
            cmbPesquisaTipoContrato.DataSource = null;
            cmbPesquisaTipoContrato.Items.Clear();

            List<KeyValuePair<string, string>> lstTipoContrato = new List<KeyValuePair<string, string>>();
            Array TipoS = Enum.GetValues(typeof(ContratosController.TB012_TipoContratoE));
            foreach (ContratosController.TB012_TipoContratoE Tipo in TipoS)
            {
                lstTipoContrato.Add(new KeyValuePair<string, string>(Tipo.ToString(), ((int)Tipo).ToString()));
            }

            cmbPesquisaTipoContrato.DataSource = lstTipoContrato;
            cmbPesquisaTipoContrato.DisplayMember = "Key";
            cmbPesquisaTipoContrato.ValueMember = "Value";

        }

        private void pctBoletoLocalizar_Click(object sender, EventArgs e)
        {

            PopularListaParaRenovacao();
        }

        private void PopularListaParaRenovacao()
        {

            new ParcelaNegocios().SetarParcelaVencida(ParametrosInterface.objUsuarioLogado.TB011_Id);
            lblTotalDeContratos.Text = "0";
            try
            {
                ContratoNegocios Contrato_N = new ContratoNegocios();
                dgwLista.AutoGenerateColumns = false;
                List<ContratosController> Retorno = Contrato_N.ListarParcelasParaRenovacao(Convert.ToInt32(cmbPesquisaTipoContrato.SelectedValue), Convert.ToInt32(cmbPesquisaCiclo.Text),Convert.ToInt32(cmbPesquisaTop.Text));
                dgwLista.DataSource = Retorno;
                dgwLista.Refresh();

                lblTotalDeContratos.Text = Retorno.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mnuListaRenovar.Enabled = false;
            if (Convert.ToInt16(cmbPesquisaTipoContrato.SelectedValue)==1)
            {
                RenovarPlanoFamiliar();
            }
        }

        private void RenovarPlanoFamiliar()
        {
            try
            {

                txtMensagens.Focus();
                txtMensagens.Text = "Iniciando processo de renovação.";
                for (int y = 0; y < dgwLista.RowCount; y++)
                {
                    if(Convert.ToBoolean(dgwLista.Rows[y].Cells["Renovar"].Value)==true)
                    {
                        List<ParcelaController> Parcelas = new List<ParcelaController>();

                        /*Dados da Ultima Parcela*/
                        ParcelaNegocios Parcela_N = new ParcelaNegocios();
                        ParcelaController UltimaParcela = new ParcelaController();
                         UltimaParcela = Parcela_N.UltimaParcelaRenovacao(Convert.ToInt64(dgwLista.Rows[y].Cells["TB012_id"].Value.ToString()));

                        
                        /****************************************************/
                        try
                        {
                            for (int i = 0; i < 12; i++)
                            {

                                ParcelaController Parcela = new ParcelaController();
                                PlanoController objPlano = new PlanoController();

                                Parcela.Plano = objPlano;

                                if(cnkExportarArquivo.Checked==true)
                                {
                                    Parcela.TB016_LoteExportacao = 0;
                                }
                                else
                                {
                                    Parcela.TB016_LoteExportacao = -1;
                                }
                              


                                List<ParcelaProdutosController> ParcelaItens_L = new List<ParcelaProdutosController>();

                                //Recuperar Empresa Pelo Local de Cadastro
                                PontoDeVendaNegocios PontoDeVenda_N = new PontoDeVendaNegocios();

                                EmpresaNegocios Empresa_N = new EmpresaNegocios();


                         
                                PontoDeVendaController Ponto= new PontoDeVendaNegocios().PontoDeVendaEmpresa(Convert.ToInt64(dgwLista.Rows[y].Cells["TB002_id"].Value.ToString()));

                   

                                Parcela.Empresa = new EmpresaNegocios().Empresa(Ponto.Empresa.TB001_id); 
                    

                             
                                PessoaNegocios Titular_N = new PessoaNegocios();
                             
                                Parcela.Pessoa = Titular_N.pessoaSelectId(Convert.ToInt64(dgwLista.Rows[y].Cells["TB013_id"].Value.ToString()));

                                Parcela.Pessoa.Municipio.TB006_Municipio = Parcela.Pessoa.Municipio.TB006_Municipio;
                                Parcela.Pessoa.Municipio.Estado.TB005_Estado = Parcela.Pessoa.Municipio.Estado.TB005_Estado;
                                Parcela.Pessoa.TB013_CPFCNPJ = dgwLista.Rows[y].Cells["TB013_CPFCNPJ"].Value.ToString().Replace(".", "").Replace(",", "").Replace("/", "").Replace("-", "").Trim();

                                Parcela.TB012_id = Convert.ToInt64(dgwLista.Rows[y].Cells["TB012_id"].Value.ToString());
                                Parcela.TB016_Parcela = i + 1;
                                Parcela.TB016_Emissao = DateTime.Now;



                                
                                Int32 Ano = Convert.ToInt32(cmbPesquisaCiclo.Text.Substring(cmbPesquisaCiclo.Text.Length - 4, 4));
                                Int16 Mes = Convert.ToInt16(cmbPesquisaCiclo.Text.Replace(Ano.ToString(),""));
                                Ano++;
                                 string Ciclo = Mes.ToString() + Ano.ToString();
                                Parcela.TB012_CicloContrato = Convert.ToInt32( Ciclo);

                                Parcela.TB016_FormaPagamentoS = "1";
                                Parcela.TB016_EmitirBoleto = 1;


                                if(i == 0)
                                {
                                    Parcela.TB016_Vencimento = Convert.ToDateTime(UltimaParcela.TB016_Vencimento.Day + "/" + UltimaParcela.TB016_Vencimento.Month + "/" + UltimaParcela.TB016_Vencimento.Year).AddMonths(1);
                                }
                                else
                                {
                                    Parcela.TB016_Vencimento = Convert.ToDateTime(Parcelas[i-1].TB016_Vencimento.Day + "/" + Parcelas[i-1].TB016_Vencimento.Month + "/" + Parcelas[i-1].TB016_Vencimento.Year).AddMonths(1);
                                }

                                Parcela.TB016_StatusS = "1";

                                //Filtrar Distribuição de major, menor e isento                        
                                PessoaNegocios Participantes_N = new PessoaNegocios();
                                List<CategoriaIdadeControler> Participantes_L = Participantes_N.MembrosAtivosDoConrato(Convert.ToInt64(dgwLista.Rows[y].Cells["TB012_id"].Value.ToString()), Parcela.TB016_Vencimento);

                                CategoriaIdadeNegocios CategoriaIdade_N = new CategoriaIdadeNegocios();
                                CategoriaIdadeControler DadosFiltroIdade = CategoriaIdade_N.DistribuicaoIsencaoIdade(Participantes_L);
                                //Localiar Plano's conforme itens de filtro
                                PlanoController filtro = new PlanoController();
                                PontoDeVendaController ObjPontoDeVenda = new PontoDeVendaController();
                                filtro.PontoDeVenda = ObjPontoDeVenda;

                                filtro.TB015_Maiores = DadosFiltroIdade.Maior;
                                filtro.TB015_Menores = DadosFiltroIdade.Menor;
                                filtro.TB015_Isentos = DadosFiltroIdade.Isento;
                                filtro.PontoDeVenda.TB002_id = Convert.ToInt64(Convert.ToInt64(dgwLista.Rows[y].Cells["TB002_id"].Value.ToString()));

                                PlanoNegocios Plano_N = new PlanoNegocios();
                                DataSet Plano = new DataSet();

                                Plano = Plano_N.PlanoVendaContezino(filtro, 1, 0,1);

                                Parcela.Plano.TB015_Maiores = filtro.TB015_Maiores;
                                Parcela.Plano.TB015_Menores = filtro.TB015_Menores;
                                Parcela.Plano.TB015_Isentos = filtro.TB015_Isentos;

                                Parcela.Plano.TB015_IOF = Convert.ToDouble(Plano.Tables[0].Rows[0]["TB015_IOF"].ToString());
                                Parcela.Plano.TB015_TipoVencimento = Convert.ToInt16(Plano.Tables[0].Rows[0]["TB015_TipoVencimento"].ToString());
                                Parcela.Plano.TB015_EspecieDocumento = Plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                                Parcela.Plano.TB015_BoletoDesc1 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc1"].ToString();
                                Parcela.Plano.TB015_BoletoDesc2 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc2"].ToString();
                                Parcela.Plano.TB015_BoletoDesc3 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc3"].ToString();
                                Parcela.Plano.TB015_BoletoDesc4 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc4"].ToString();
                                Parcela.Plano.TB015_BoletoDesc5 = Plano.Tables[0].Rows[0]["TB015_BoletoDesc5"].ToString();
                                Parcela.TB016_Juros= Convert.ToDouble(Plano.Tables[0].Rows[0]["TB015_Juros"].ToString());
                                Parcela.TB016_Multa = Convert.ToDouble(Plano.Tables[0].Rows[0]["TB015_Multa"].ToString());
                                Parcela.TB016_DiaVencimento = Parcela.TB016_Vencimento.Day;



                                //Incluir Planos Possiveis para cada Parcela
                                Parcela.TB016_Valor         = Convert.ToDouble(Plano.Tables[0].Rows[0]["ValorPlano"].ToString());
                                Parcela.TB015_id            = Convert.ToInt64(Plano.Tables[0].Rows[0]["TB015_id"].ToString());
                                Parcela.TB015_Plano         = Plano.Tables[0].Rows[0]["TB015_Plano"].ToString();
                                Parcela.TB016_Entrada       = 0;
                                Parcela.TB016_ValorAdesao   = 0;

                                ProdutoNegocios PlanoProduto_N = new ProdutoNegocios();
                                List<ProdutoController> PlanoProduto_L = PlanoProduto_N.ProdutoPlano(Parcela.TB015_id);
                                
                                //*******************Inserir Parcela
                                foreach (ProdutoController Produto in PlanoProduto_L)
                                {
                                    ParcelaProdutosController ParcelaItem = new ParcelaProdutosController();
                                    ParcelaItem.TB017_id = Produto.TB014_id;
                                    ParcelaItem.TB017_IdProteus = Produto.TB014_IdProtheus;
                                    ParcelaItem.TB017_Item = Produto.TB014_Produto;
                                    ParcelaItem.TB017_Maior = Produto.TB014_Maiores;
                                    ParcelaItem.TB017_Menor = Produto.TB014_Menores;
                                    ParcelaItem.TB017_Isento = Produto.TB014_Isentos;

                                    if (Produto.TB014_Menores > 0)
                                    {
                                        Produto.TB014_ValorUnitario = Produto.TB014_ValorUnitario * filtro.TB015_Menores;
                                        ParcelaItem.TB017_Item = ParcelaItem.TB017_Item + "( * " + filtro.TB015_Menores + ")";
                                    }

                                    if (Produto.TB014_Isentos > 0)
                                    {
                                        ParcelaItem.TB017_Item = ParcelaItem.TB017_Item + "( * " + filtro.TB015_Isentos + ")";
                                        Produto.TB014_ValorUnitario = Produto.TB014_ValorUnitario * filtro.TB015_Isentos;
                                    }

                                    ParcelaItem.TB017_ValorUnitario = Produto.TB014_ValorUnitario;
                                    if (Produto.TB014_ValorMultiplo == 1)
                                    {
                                        ParcelaItem.TB017_ValorUnitario = (ParcelaItem.TB017_ValorUnitario * ParcelaItem.TB017_Maior) + (ParcelaItem.TB017_ValorUnitario * ParcelaItem.TB017_Menor);
                                        Produto.TB014_ValorUnitario = (Produto.TB014_ValorUnitario * ParcelaItem.TB017_Maior) + (Produto.TB014_ValorUnitario * ParcelaItem.TB017_Menor);
                                    }
                                    
                                    ParcelaItem.TB017_ValorDesconto = 0;
                                      
                                    ParcelaItem.TB017_ValorFinal = Produto.TB014_ValorUnitario - ParcelaItem.TB017_ValorDesconto;
                                    string[] vValor = ParcelaItem.TB017_ValorFinal.ToString().Split(',');

                                    string v1 = vValor[0];
                                    string v2 = "0";

                                    if (ParcelaItem.TB017_ValorFinal > 0.01)
                                    {
                                        if (vValor[1].Length > 1)
                                        {
                                            v2 = vValor[1].Substring(0, 2);
                                        }
                                        else
                                        {
                                            v2 = vValor[1];
                                        }
                                    }
                                    ParcelaItem.TB017_ValorFinal = Convert.ToDouble(v1 + "," + v2);

                                    ParcelaItens_L.Add(ParcelaItem);
                                }

                                double Valor = 0;
                                for (int p = 0; p < ParcelaItens_L.Count; p++)
                                {
                                    Valor = Valor + ParcelaItens_L[p].TB017_ValorFinal;
                                }
                                Parcela.TB016_Valor = Valor;

                                /*Inserir Produto da Parcela*/
                                Parcela.ParcelaProduto_L = ParcelaItens_L;
                                
                                Parcelas.Add(Parcela);
                            }

                            Parcela_N.GerarCobrancasParcela(Parcelas, ParametrosInterface.objUsuarioLogado.TB011_Id);
                            /*Alterar Ciclo do contrato*/
                            ContratoNegocios Contrato_N = new ContratoNegocios();
                            Contrato_N.CicloContratoAtualizar(Convert.ToInt64(dgwLista.Rows[y].Cells["TB012_id"].Value.ToString()), Parcelas[0].TB012_CicloContrato, ParametrosInterface.objUsuarioLogado.TB011_Id, UltimaParcela.TB016_Vencimento.AddYears(1));

                      
                                /*Gerar Boletos*/
                                List<ParcelaController>    BoletosEmitidos = Parcela_N.ParcelasParaEmissaoBoleto(Convert.ToInt64(dgwLista.Rows[y].Cells["TB012_id"].Value.ToString()), ParametrosInterface.objUsuarioLogado.TB011_Id,0);
                      
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        /*****************************************************/
                    }
                }

                /*Atualizar List*/
                PopularListaParaRenovacao();
                lblExportarTotalDeContratos.Text = "Boletos emitidos, pronto para exportar aquivo";
                mnuListaRenovar.Enabled = true;

                MessageBox.Show("Operação Concluida", "Renovação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkSelecionarTodos_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < dgwLista.RowCount; y++)
            {
                if(chkSelecionarTodos.Checked==true)
                {
                    dgwLista.Rows[y].Cells["Renovar"].Value = true;
                }
                else
                {
                    dgwLista.Rows[y].Cells["Renovar"].Value = false;
                }
            }
        }

        private void mnuListaExportar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Add(tbExportar);

            lblExportarLocalArquivo.Text = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            cmbExportarTipoContrato.DataSource = null;
            cmbExportarTipoContrato.Items.Clear();

            List<KeyValuePair<string, string>> lstTipoContrato = new List<KeyValuePair<string, string>>();
            Array TipoS = Enum.GetValues(typeof(ContratosController.TB012_TipoContratoE));
            foreach (ContratosController.TB012_TipoContratoE Tipo in TipoS)
            {
                lstTipoContrato.Add(new KeyValuePair<string, string>(Tipo.ToString(), ((int)Tipo).ToString()));
            }

            cmbExportarTipoContrato.DataSource = lstTipoContrato;
            cmbExportarTipoContrato.DisplayMember = "Key";
            cmbExportarTipoContrato.ValueMember = "Value";
            cmbExportarTop.SelectedIndex = 0;
            cmbExportarTipoContrato.SelectedIndex = 0;
            cmbExportarCiclo.SelectedIndex = 0;

            tabPrincipal.TabPages.Remove(tpLista);
            
        }

        private void pcbExportarFiltrar_Click(object sender, EventArgs e)
        {
            PopularListaParaExportar();
        }

        private void PopularListaParaExportar()
        {
            lblTotalDeContratos.Text = "0";
            try
            {
                ContratoNegocios Contrato_N = new ContratoNegocios();
                dgwExportarLista.AutoGenerateColumns = false;
                List<ContratosController> Retorno = Contrato_N.ListarParcelasParaExportacao(Convert.ToInt32(cmbExportarTipoContrato.SelectedValue), Convert.ToInt32(cmbExportarCiclo.Text), Convert.ToInt32(cmbExportarTop.Text));
                dgwExportarLista.DataSource = Retorno;
                dgwExportarLista.Refresh();

                label6.Text = Retorno.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgwExportarLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 1)
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    int value;
                    if (e.Value != null && int.TryParse(e.Value.ToString(), out value))
                    {
                        if (Convert.ToInt16(dgwExportarLista.Rows[e.RowIndex].Cells["ExportTB012_NParcelas"].Value.ToString()) < 12)
                        {
                            //ddtParcelas.Rows[e.RowIndex].ReadOnly = false;
                            dgwExportarLista.Rows[e.RowIndex].Cells["exportExportar"].ReadOnly = true;
                            dgwExportarLista.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkExportarSelecionarTodos_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < dgwExportarLista.RowCount; y++)
            {
                if (chkExportarSelecionarTodos.Checked == true && Convert.ToInt16(dgwExportarLista.Rows[y].Cells["ExportTB012_NParcelas"].Value.ToString()) == 12)
                {
                    dgwExportarLista.Rows[y].Cells["exportExportar"].Value = true;
                }
                else
                {
                    dgwExportarLista.Rows[y].Cells["exportExportar"].Value = false;
                }
            }
        }

        private void mnuExportarFechar_Click(object sender, EventArgs e)
        {
            tabPrincipal.TabPages.Remove(tbExportar);
            tabPrincipal.TabPages.Add(tpLista);
        }

        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuExportarGerarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                lblExportarTotalDeContratos.Focus();
                lblExportarTotalDeContratos.Text = @"Iniciando Exportação.";
                List<ParcelaController> Export = new List<ParcelaController>();

                for (int y = 0; y < dgwExportarLista.RowCount; y++)
                {
                    if (Convert.ToBoolean(dgwExportarLista.Rows[y].Cells["exportExportar"].Value) == true)
                    {
                        ParcelaController Contrato = new ParcelaController();
                        Contrato.TB012_id = Convert.ToInt64(dgwExportarLista.Rows[y].Cells["ExportTB012_id"].Value.ToString());
                        Export.Add(Contrato);

                    }
                }

                ParcelaNegocios Parcela_N = new ParcelaNegocios();
                string Arquivo = Parcela_N.ParcelasExportarBoletos(Export, Convert.ToInt32(cmbExportarCiclo.Text), lblExportarLocalArquivo.Text);

                MessageBox.Show(Arquivo, @"Arquivo Exportado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgwExportarLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgwExportarLista.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":
                            try
                            {
                                ParcelaNegocios Parcela_N = new ParcelaNegocios();

                                lblContrato.Text = dgwExportarLista.Rows[e.RowIndex].Cells["ExportTB012_id"].Value.ToString();

                             
                                dgwParcelas.AutoGenerateColumns = false;
                                dgwParcelas.DataSource = Parcela_N.ParcelasListarAtivasPorContrato(Convert.ToInt64(lblContrato.Text),Convert.ToInt32(cmbExportarCiclo.Text) );
                                dgwParcelas.Refresh();

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

        private void dgwParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgwParcelas.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Parcela":
                            try
                            {
                                lblExpParcela.Text = dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_id"].Value.ToString();
                                lblExpNossoNumero.Text = dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_NossoNumero"].Value.ToString();

                                if(Convert.ToInt64(lblExpNossoNumero.Text) < 1)
                                {
                                    btnSolicitarBoleto.Enabled = true;
                                }
                                else
                                {
                                    btnSolicitarBoleto.Enabled = false;
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

        private void btnSolicitarBoleto_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblContrato.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Selecione uma parcela", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ;
                }



                btnSolicitarBoleto.Enabled = false;
            ParcelaNegocios Parcela_N = new ParcelaNegocios();
            ParcelaController Parcela = Parcela_N.ParcelaId(Convert.ToInt64(lblExpParcela.Text));

            if(Parcela.TB016_id>0)
            {
                if(Parcela_N.EmitirBoletoAvulsto(Parcela,ParametrosInterface.objUsuarioLogado.TB011_Id, Convert.ToInt64(lblExpParcela.Text)))
                {

                    dgwParcelas.AutoGenerateColumns = false;
                    dgwParcelas.DataSource = Parcela_N.ParcelasListarAtivasPorContrato(Convert.ToInt64(lblContrato.Text), Convert.ToInt32(cmbExportarCiclo.Text));
                    dgwParcelas.Refresh();

                    
                    lblExpParcela.Text = "";
                    lblExpNossoNumero.Text = "";
                }

            }

            btnSolicitarBoleto.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgwLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgwLista.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Contrato":
                            try
                            {
                                ParcelaNegocios Parcela_N = new ParcelaNegocios();

                                lblContratoAltVencimento.Text = dgwLista.Rows[e.RowIndex].Cells["TB012_id"].Value.ToString();
                                lblParcelaAltVencimento.Text = dgwLista.Rows[e.RowIndex].Cells["TB006_id"].Value.ToString();
                                dtmContratoAltVencimento.Value= Convert.ToDateTime( dgwLista.Rows[e.RowIndex].Cells["TB016_Vencimento"].Value.ToString());
                                lblLinha.Text = e.RowIndex.ToString();
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

        private void btnAlterarDataUltimoVencimento_Click(object sender, EventArgs e)
        {

            try
            {
                if (lblContratoAltVencimento.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Selecione uma parcela", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ;
                }

                ParcelaNegocios Parcela_N = new ParcelaNegocios();

                if(Parcela_N.AlterarVencimentoParcela(Convert.ToInt64(lblParcelaAltVencimento.Text), dtmContratoAltVencimento.Value,ParametrosInterface.objUsuarioLogado.TB011_Id,Convert.ToInt64(lblContratoAltVencimento.Text)))
                {
                    MessageBox.Show("Data alterada com sucesso", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwLista.Rows[Convert.ToInt32(lblLinha.Text)].Cells["TB016_Vencimento"].Value = dtmContratoAltVencimento.Value;
                }
                else
                {
                    MessageBox.Show("ERRO", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                lblContratoAltVencimento.Text = "";
                lblParcelaAltVencimento.Text = "";
                lblLinha.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void dgwParcelas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var nossoNumeroColuna = dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_NossoNumero"].Value != null ? dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_NossoNumero"].Value.ToString() : string.Empty;
            long.TryParse(nossoNumeroColuna, out long nossoNumero);

            if (nossoNumero <= 0)
            {
                MessageBox.Show("Digite um número válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var parcelaColuna = dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_id"].Value != null ? dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_id"].Value.ToString() : string.Empty;
            long.TryParse(parcelaColuna, out long idParcela);

            if (idParcela <= 0)
            {
                MessageBox.Show("Não foi possível encontrar a parcela.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nossoNumeroParcela = new ParcelaNegocios().BuscarNossoNumeroPorIdParcela(idParcela);

            if (nossoNumero != nossoNumeroParcela)
            {
                if (MessageBox.Show("Deseja alterar o nosso número?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_NossoNumero"].Value = nossoNumeroParcela;
                    return;
                }

                if (new ParcelaNegocios().AlterarNossoNumeroPorParcela(idParcela, nossoNumero))
                {
                    dgwParcelas.Rows[e.RowIndex].Cells["DetTB016_NossoNumero"].Value = nossoNumero;
                    MessageBox.Show("Nosso Número alterado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro ao alterar o nosso número, tente novamente...", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
