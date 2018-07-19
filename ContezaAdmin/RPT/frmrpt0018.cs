using Negocios;
using System;
using System.Text;
using System.Windows.Forms;
using static System.String;

namespace ContezaAdmin.RPT
{
    public partial class frmrpt0018 : Form
    {
        public frmrpt0018()
        {
            InitializeComponent();
        }

        private void frmrpt0018_Load(object sender, EventArgs e)
        {
            mnuRelatorioTipo.SelectedIndex = 0;
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0018'. Você pode movê-la ou removê-la conforme necessário.
            this.dTRPT0018TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0018);
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0018'. Você pode movê-la ou removê-la conforme necessário.

            PontosDeVenda();
            Planos();
            dtmFiltroDataReferencia.Value = DateTime.Now;
            dtmFiltroDataReferenciaFim.Value = DateTime.Now;

            dtmFiltroDataReferencia.Visible = false;
            dtmFiltroDataReferenciaFim.Visible = false;
            cmbFiltroTipo.SelectedIndex = 0;

            this.txtFiltroContratoInicio.Location = new System.Drawing.Point(240, 5);
            this.txtFiltroContratoFim.Location = new System.Drawing.Point(350, 5);
            txtFiltroContratoInicio.Visible = true;
            txtFiltroContratoFim.Visible = true;


            cmbFiltroPlano.Location = new System.Drawing.Point(240, 5);
            //cmbFiltroPlano.Visible = true;

            //carregar();
            try
            {
                cmbFiltroPlano.SelectedValue = 0;

                dTRPT0018TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0018);
            }
            catch (Exception)
            {
                // MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PontosDeVenda()
        {
            try
            {
                var pontoDeVendaN = new PontoDeVendaNegocios();
                cmbFiltroPontoDeVenda.DataSource = null;
                cmbFiltroPontoDeVenda.Items.Clear();
                cmbFiltroPontoDeVenda.DataSource = pontoDeVendaN
                    .PontosDeVendaLiberadosParaUsuario(ParametrosInterface.objUsuarioLogado).Tables[0];
                cmbFiltroPontoDeVenda.DisplayMember = "TB002_Ponto";
                cmbFiltroPontoDeVenda.ValueMember = "TB002_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Planos()
        {
            try
            {
                cmbFiltroPlano.DataSource = null;
                cmbFiltroPlano.Items.Clear();
                cmbFiltroPlano.DataSource = new PlanoNegocios().ListarPlanos();
                cmbFiltroPlano.DisplayMember = "TB015_Plano";
                cmbFiltroPlano.ValueMember = "TB015_id";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuRelatorioFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtmFiltroDataReferencia.Visible = false;
            dtmFiltroDataReferenciaFim.Visible = false;
            txtFiltroContratoInicio.Visible = false;
            txtFiltroContratoFim.Visible = false;
            cmbFiltroPontoDeVenda.Visible = false;
            txtFiltroCPFCNPJ.Visible = false;
            cmbFiltroPlano.Visible = false;

            if (cmbFiltroTipo.Text.TrimEnd() == @"Vencimento")
            {
                this.dtmFiltroDataReferencia.Location = new System.Drawing.Point(240, 5);
                this.dtmFiltroDataReferenciaFim.Location = new System.Drawing.Point(350, 5);
                dtmFiltroDataReferencia.Visible = true;
                dtmFiltroDataReferenciaFim.Visible = true;
            }

            if (cmbFiltroTipo.Text.TrimEnd() == @"Pagamento")
            {
                this.dtmFiltroDataReferencia.Location = new System.Drawing.Point(240, 5);
                this.dtmFiltroDataReferenciaFim.Location = new System.Drawing.Point(350, 5);
                dtmFiltroDataReferencia.Visible = true;
                dtmFiltroDataReferenciaFim.Visible = true;
            }


            if (cmbFiltroTipo.Text.TrimEnd() == @"Emissão")
            {
                this.dtmFiltroDataReferencia.Location = new System.Drawing.Point(240, 5);
                this.dtmFiltroDataReferenciaFim.Location = new System.Drawing.Point(350, 5);
                dtmFiltroDataReferencia.Visible = true;
                dtmFiltroDataReferenciaFim.Visible = true;
            }

            if (cmbFiltroTipo.Text.TrimEnd() == @"Contrato")
            {
                this.txtFiltroContratoInicio.Location = new System.Drawing.Point(240, 5);
                this.txtFiltroContratoFim.Location = new System.Drawing.Point(350, 5);
                txtFiltroContratoInicio.Visible = true;
                txtFiltroContratoFim.Visible = true;
            }

            if (cmbFiltroTipo.Text.TrimEnd() == @"Ponto de Venda")
            {
                cmbFiltroPontoDeVenda.Location = new System.Drawing.Point(240, 5);
                cmbFiltroPontoDeVenda.Visible = true;
            }

            if (cmbFiltroTipo.Text.TrimEnd() == @"CPF")
            {
                txtFiltroCPFCNPJ.Location = new System.Drawing.Point(240, 5);
                txtFiltroCPFCNPJ.Visible = true;
            }

            if (cmbFiltroTipo.Text.TrimEnd() == @"Plano")
            {
                cmbFiltroPlano.Location = new System.Drawing.Point(240, 5);
                cmbFiltroPlano.Visible = true;
            }
        }

        private void ptbFiltrar_Click(object sender, EventArgs e)
        {
            carregar();
        }

        private void carregar()
        {
            try
            {
                dTRPT0018TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;

                //inadimplenciaAnual();

                var sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_Beneficiario, dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador,  ");
                sSql.Append(" dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB002_PontosDeVenda.TB002_id, dbo.TB002_PontosDeVenda.TB002_Ponto, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_Valor, ");
                sSql.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_Abatimento, dbo.TB016_Parcela.TB016_IOF, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_FormaPagamento, ");
                sSql.Append(" dbo.TB016_Parcela.TB016_NossoNumero, dbo.TB016_Parcela.TB016_id, dbo.TB016_Parcela.TB016_CPFCNPJ ");
                sSql.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSql.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
                sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id ");
                sSql.Append(" WHERE dbo.TB016_Parcela.TB016_Status = 5  ");

                if (cmbFiltroTipo.Text.TrimEnd() == @"Vencimento")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_Vencimento >= ");
                    sSql.Append("'");
                    sSql.Append(dtmFiltroDataReferencia.Value.ToString("MM/dd/yyyy"));
                    sSql.Append("'");
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_Vencimento <= ");
                    sSql.Append("'");
                    sSql.Append(dtmFiltroDataReferenciaFim.Value.ToString("MM/dd/yyyy"));
                    sSql.Append("'");
                }


                if (cmbFiltroTipo.Text.TrimEnd() == @"Emissão")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_Emissao >= ");
                    sSql.Append("'");
                    sSql.Append(dtmFiltroDataReferencia.Value.ToString("MM/dd/yyyy"));
                    sSql.Append("'");
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_Emissao <= ");
                    sSql.Append("'");
                    sSql.Append(dtmFiltroDataReferenciaFim.Value.ToString("MM/dd/yyyy"));
                    sSql.Append("'");
                }


                if (cmbFiltroTipo.Text.TrimEnd() == @"Pagamento")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_DataPagamento >= ");
                    sSql.Append("'");
                    sSql.Append(dtmFiltroDataReferencia.Value.ToString("MM/dd/yyyy"));
                    sSql.Append("'");
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_DataPagamento <= ");
                    sSql.Append("'");
                    sSql.Append(dtmFiltroDataReferenciaFim.Value.ToString("MM/dd/yyyy"));
                    sSql.Append("'");
                }

                if (cmbFiltroTipo.Text.TrimEnd() == @"Contrato")
                {
                    if (txtFiltroContratoFim.Text.Trim() == Empty)
                    {
                        txtFiltroContratoFim.Text = txtFiltroContratoInicio.Text;
                    }

                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB012_id  >= ");
                    sSql.Append(txtFiltroContratoInicio.Text.TrimEnd());
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB012_id  <= ");
                    sSql.Append(txtFiltroContratoFim.Text.TrimEnd());
                }

                if (cmbFiltroTipo.Text.TrimEnd() == @"Ponto de Venda")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB002_PontosDeVenda.TB002_id= ");
                    sSql.Append(cmbFiltroPontoDeVenda.SelectedValue);
                }

                if (cmbFiltroTipo.Text.TrimEnd() == @"CPF")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_CPFCNPJ= ");
                    sSql.Append("'");
                    sSql.Append(txtFiltroCPFCNPJ.Text.Replace("-", "").Replace(".", "").Replace(",", "").Replace("/", ""));
                    sSql.Append("'");
                }

                if (cmbFiltroTipo.Text.TrimEnd() == @"Plano")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB015_Planos.TB015_id = ");
                    sSql.Append(cmbFiltroPlano.SelectedValue);
                }


                if(mnuRelatorioTipo.Text== "Boleto")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_FormaPagamento = 1 ");
                }

                if (mnuRelatorioTipo.Text == "Dinheiro")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_FormaPagamento = 2 ");
                }

                if (mnuRelatorioTipo.Text == "Credito")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_FormaPagamento = 3 ");
                    sSql.Append(" or ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_FormaPagamento = 4 ");
                    sSql.Append(" or ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_FormaPagamento = 5 ");
                }

                if (mnuRelatorioTipo.Text == "Debito")
                {
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB016_Parcela.TB016_FormaPagamento = 6 ");
                }

                sSql.Append(" ORDER BY dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_id ");

                this.dTRPT0018TableAdapter.Adapter.SelectCommand.CommandText = sSql.ToString();

                try
                {
                    dTRPT0018TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0018);
                }
                catch (Exception)
                {

                }

                rpwRPT0018.RefreshReport();
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
