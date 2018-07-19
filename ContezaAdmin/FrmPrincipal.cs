using ContezaAdmin.Administrativo;
using ContezaAdmin.Atendimento;
using ContezaAdmin.Comercial;
using ContezaAdmin.Financeiro;
using ContezaAdmin.Inadimpentes;
using ContezaAdmin.RPT;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ContezaAdmin
{


    public partial class FrmPrincipal : Form
    {
        public frmRprContezinosContratosDiariosPorCanal Rpt0004 = new frmRprContezinosContratosDiariosPorCanal();
        public frmParceiros Parceiros = new frmParceiros();
        public frmEmpresa Empresa = new frmEmpresa();
        public frmGrupos GrupoDeAcesso = new frmGrupos();
        public frmUsuarios Usuarios = new frmUsuarios();
        public FrmLojas Lojas = new FrmLojas();
        public frmProduto Produtos = new frmProduto();
        public frmPlanos Planos = new frmPlanos();
        public frmBanner Banner = new frmBanner();
        public frmPromocao Promocoes = new frmPromocao();
        public frmBoletos Boletos = new frmBoletos();



        public FrmPrincipal()
        {
            InitializeComponent();
            RecuperarModulos();
        }

        protected void RecuperarModulos()
        {

            this.Text = "Gestor de Contratos Clube Conteza [" + ParametrosInterface.objUsuarioLogado.TB011_NomeExibicao.TrimEnd() + "]"; 
            try
            {
                #region Modulo Atendimento

                if ((from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 1 select i).Count() < 1)
                {
                    rbtAtendimento.Visible = false;
                }
                else
                {
                    //Manter Contezinos
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 1 select i).Count() < 1)
                    {
                        btnContezinos.Enabled = false;
                        btnContezinos.Visible = false;
                    }
                    //Relatorio Contezinos
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 2 select i).Count() < 1)
                    {
                        btnRelatorioContezinos.Enabled = false;
                        btnRelatorioContezinos.Visible = false;
                    }

                    //    //Manter Parceiros
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 3 select i).Count() < 1)
                    {
                        rbpParceiros.Enabled = false;
                        //rbpParceiros.Visible = false;
                    }

                    //Relatorio Contrato Contezino Diario Por Canal
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 5 select i).Count() < 1)
                    {
                        rbRptContezinoContratoDiarioPorCanal.Visible = false;
                    }
                    //Exportar dados do Corporativo
                    //
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 25 select i).Count() < 1)
                    {
                        btnCorporativoExportar.Visible = false;
                    }
                }
                #endregion

                #region Modulo Financeiro
               // btnBoletos.Visible = false;
                if ((from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 2 select i).Count() < 1)
                {
                    rbtFinanceiro.Visible = false;
                }
                else
                {
                    //Importar Arquivo Banco
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 6 select i).Count() < 1)
                    {
                        bbtFinanceiroImportarPagamento.Enabled = false;
                        bbtFinanceiroImportarPagamento.Visible = false;
                    }
                

                    //RPT0023
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id ==26 select i).Count() < 1)
                    {
                        fRpt0023.Enabled = false;
                        fRpt0023.Visible = false;
                    }
                }
                #endregion

                #region Modulo Comercial

                rbpPlanos.Enabled = false;
                rbpPlanos.Visible = false;
                
                btnBanner.Enabled = false;
                btnBanner.Visible = false;

                btnPromocoes.Enabled = false;
                btnPromocoes.Visible = false;

                rbtnMensagens.Enabled = false;
                rbtnMensagens.Visible = false;
                

                if ((from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 3 select i).Count() < 1)
                {
                    /**/
                    rbtComercial.Visible = false;
                }
                else
                {
                    //Importar Arquivo Banco
                    if ((from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 28 select i).Count() < 1)
                    {
                        btnCampanhas.Enabled = false;
                        btnCampanhas.Visible = false;
                    }

                }
                #endregion

                #region Modulo Administrativo
                // rbpEmpresa.Visible = false;
                // rbpEmpresa.Enabled = false;

                btnEmpresa.Visible = false;
                btnEmpresa.Enabled = false;
                btnUsuarios.Visible = false;
                btnUsuarios.Enabled = false;
                btnGrupos.Visible = false;
                btnGrupos.Enabled = false;

                rbtImportarContezino.Visible = false;

                rbtImportarContezino.Enabled = false;
                //btnLojas.Visible = false;
                //ribbonPanel1.Visible = false;
                //ribbonPanel1.Enabled = false;

                if (!(from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 4 select i).Any())
                {
                    /**/
                    rbtAdministrativo.Visible = false;
                }
                else
                {
                    //Manter Pontos de Venda
                    if (!(from i in ParametrosInterface.objUsuarioLogado.Privilegios where i.TB008_id == 24 select i).Any())
                    {
                        btnLojas.Enabled = false;
                        btnLojas.Visible = false;
                    }
                }
                #endregion

                #region Modulo Help Desck
                if ((from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 5 select i).Count() < 1)
                {

                    rbtHelpDesck.Visible = false;
                }
                else
                {

                }
                #endregion

                #region Inadimplencia
                //rbtInadimplencia
                if ((from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 6 select i).Count() < 1)
                {

                    rbtInadimplencia.Visible = false;
                }
                else
                {

                }
                #endregion

                #region Corporativo
                //rbtInadimplencia
                if ((from i in ParametrosInterface.objUsuarioLogado.Modulos where i.TB007_Id == 7 select i).Count() < 1)
                {

                    rbtCorporativo.Visible = false;
                }
                else
                {

                }
                #endregion

                if (ParametrosInterface.objUsuarioLogado.TB037_Id >1)
                {
                    rbtAtendimento.Visible = false;
                    rbtFinanceiro.Visible = false;
                    rbtComercial.Visible = false;
                    rbtAdministrativo.Visible = false;
                    rbtHelpDesck.Visible = false;
                    rbtCorporativo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnContezinos_Click(object sender, EventArgs e)
        {
            //frmContezinos Contezinos = new frmContezinos();
            //Contezinos.MdiParent = this;
            //Contezinos.Show();

            var familiar = new FrmFamiliar { MdiParent = this };
            familiar.Show();
        }

        private void btnParceiros_Click(object sender, EventArgs e)
        {
            if (Parceiros != null && (Parceiros != null || Parceiros.IsDisposed))
            {
                Parceiros.MdiParent = this;
                Parceiros.Show();
                Parceiros.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void btnEmpresa_Click(object sender, EventArgs e)
        {
            // new ComissaoNegocios().ComissaoProcessamento();
            //if (Empresa != null && (Empresa != null || Empresa.IsDisposed))
            //{
            //    Empresa.MdiParent = this;
            //    Empresa.Show();
            //}
        }

        private void btnGrupos_Click(object sender, EventArgs e)
        {
            if (GrupoDeAcesso != null && (GrupoDeAcesso != null || GrupoDeAcesso.IsDisposed))
            {
                GrupoDeAcesso.MdiParent = this;
                GrupoDeAcesso.Show();
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (Usuarios != null && (Usuarios != null || Usuarios.IsDisposed))
            {
                Usuarios.MdiParent = this;
                Usuarios.Show();
            }
        }

        private void btnLojas_Click(object sender, EventArgs e)
        {
            if (Lojas != null && (Lojas != null || Lojas.IsDisposed))
            {
                Lojas.MdiParent = this;
                Lojas.Show();
            }
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            if (Produtos != null && (Produtos != null || Produtos.IsDisposed))
            {
                Produtos.MdiParent = this;
                Produtos.Show();
            }
        }

        private void btnPlanos_Click(object sender, EventArgs e)
        {
            if (Planos != null && (Planos != null || Planos.IsDisposed))
            {
                Planos.MdiParent = this;
                Planos.Show();
            }
        }

        private void btnBanner_Click(object sender, EventArgs e)
        {
            if (Banner != null && (Banner != null || Banner.IsDisposed))
            {
                Banner.MdiParent = this;
                Banner.Show();
            }
        }

        private void btnPromocoes_Click(object sender, EventArgs e)
        {
            if (Promocoes != null && (Promocoes != null || Promocoes.IsDisposed))
            {
                Promocoes.MdiParent = this;
                Promocoes.Show();
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            tslVersao.Text = string.Format("Gestor Contratos Clube Conteza (VS: {0}), Banco {1}", Application.ProductVersion, ParametrosInterface.objUsuarioLogado.Banco.TrimEnd());
        }

        private void btnBoletos_Click(object sender, EventArgs e)
        {
            //if (Boletos != null && (Boletos != null || Boletos.IsDisposed))
            //{
            //    Boletos.MdiParent = this;
            //    Boletos.Show();
            //}
        }

        private void rbtImportarContezino_Click(object sender, EventArgs e)
        {
            //if (ImportarFamiliar != null && (ImportarFamiliar != null || ImportarFamiliar.IsDisposed))
            //{
            //    ImportarFamiliar.MdiParent = this;
            //    ImportarFamiliar.Show();
            //}
        }

        private void rbcAtendimentoRelatorios_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            switch (rbcAtendimentoRelatorios.SelectedItem.Value)
            {
                case "5":

                    if (Rpt0004 != null && (Rpt0004 != null || Rpt0004.IsDisposed))
                    {
                        Rpt0004.MdiParent = this;
                        Rpt0004.StartPosition = FormStartPosition.CenterScreen;
                        Rpt0004.Show();
                    }
                    break;
                case "13":
                    var rpt0013 = new Frmrpt0013 { MdiParent = this };
                    rpt0013.Show();
                    break;
                case "15":
                    var rpt0015 = new Frmrpt0015 { MdiParent = this };
                    rpt0015.Show();
                    break;

                case "16":
                    var rpt0016 = new Frmrpt0016 { MdiParent = this };
                    rpt0016.Show();
                    break;


            }
        }

        private void bbtFinanceiroImportarPagamento_Click(object sender, EventArgs e)
        {
            var finImpArqPagBanc = new frmImpArquivoPagamentoBanco
            {
                MdiParent = this,
                StartPosition = FormStartPosition.CenterScreen
            };
            finImpArqPagBanc.Show();
        }



        private void btnCartaoContezinos_Click(object sender, EventArgs e)
        {
            var cartoes = new frmCartoes
            {
                MdiParent = this,
                StartPosition = FormStartPosition.CenterScreen
            };
            cartoes.Show();
        }

        private void btnCorporativo_Click(object sender, EventArgs e)
        {
            var corporativo = new frmCorporativo { MdiParent = this };
            corporativo.Show();
            corporativo.StartPosition = FormStartPosition.CenterScreen;

        }

        private void FrmPrincipal_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_MaximumSizeChanged(object sender, EventArgs e)
        {

        }

     

        private void brmTrocaSenha_Click(object sender, EventArgs e)
        {

            var trocaSenha = new frmTrocaDeSenha { MdiParent = this };
            trocaSenha.Show();
        }

        private void btnManuNiveis_Click(object sender, EventArgs e)
        {
            var manutencaoNiveis = new frmCorporativoManuNiveis { MdiParent = this };
            manutencaoNiveis.Show();
            //ManutencaoNiveis.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ribFinanceiroRenovacao_Click(object sender, EventArgs e)
        {
            var renovacao = new frmrenovacao { MdiParent = this };
            renovacao.Show();

        }

        private void ribFinanceiroParcelaManutencao_Click(object sender, EventArgs e)
        {
            var parcelamanutencao = new FrmManutencaoParcela
            {
                MdiParent = this,
                Size = new System.Drawing.Size(1236, 548)
            };
            parcelamanutencao.Show();
        }

        private void rpFinanceiroRpt0016_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            switch (rpFinanceiroRpt0016.SelectedItem.Value)
            {
                case "16":
                    var rpt0016 = new Frmrpt0016 { MdiParent = this };
                    rpt0016.Show();
                    break;

                case "17":
                    var rpt0017 = new frmRPT0017 { MdiParent = this };
                    rpt0017.Show();
                    break;

                case "18":
                    var rpt0018 = new frmrpt0018 { MdiParent = this };
                    rpt0018.Show();
                    break;
                case "23":
                    var rpt0023 = new frmRpt0023 { MdiParent = this };
                    rpt0023.Show();
                    break;
                case "24":
                    var rpt0024 = new frmRpt0024 { MdiParent = this };
                    rpt0024.Show();
                    break;
                case "25":
                    var rpt0025 = new frmRpt0025 { MdiParent = this };
                    rpt0025.Show();
                    break;

                case "26":
                    var rpt0026 = new frmRpt0026 { MdiParent = this };
                    rpt0026.Show();
                    break;



            }
        }

        private void rbtnNegociacao_Click(object sender, EventArgs e)
        {
            var negociacao = new frmNegociacao { MdiParent = this };
            negociacao.Show();
        }

        private void rbpInadimpenciaNegociacao_Click(object sender, EventArgs e)
        {
           
        }

        private void rbtAjuda_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(ParametrosInterface.Intranet + "/#list");

            Process.Start(sInfo);
            //Help.ShowHelp(this, "file://" + System.Environment.CurrentDirectory + "\\Gestor.chm", HelpNavigator.Topic, "Corporativo.htm");
        }

        private void rbtnMensagens_Click(object sender, EventArgs e)
        {
            var sms = new frmsms { MdiParent = this };
            sms.Show();
        }

        private void btnCorporativoExportar_Click(object sender, EventArgs e)
        {
            var corporativoexportar = new frmCorporativoExportar { MdiParent = this };
            corporativoexportar.Show();
            corporativoexportar.StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void rbnCorporativoAtendimento_Click(object sender, EventArgs e)
        {
            var corporativo = new frmCorporativoNovo { MdiParent = this };
            corporativo.StartPosition = FormStartPosition.CenterScreen;
            corporativo.Show();
        }

        private void btnCampanhas_Click(object sender, EventArgs e)
        {

            
            var campanhas = new frmCampanhas { MdiParent = this };
            campanhas.StartPosition = FormStartPosition.CenterScreen;
            campanhas.Show();
        }

        private void btnMensalidadePremiada_Click(object sender, EventArgs e)
        {
            var mensalidadepremiada = new frmMensalidadePremiada { MdiParent = this };
            mensalidadepremiada.StartPosition = FormStartPosition.CenterScreen;
            mensalidadepremiada.Show();
        }
    }
}
