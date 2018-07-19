using Negocios;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ContezaAdmin.RPT
{
    public partial class frmRpt0024 : Form
    {
        public frmRpt0024()
        {
            InitializeComponent();
        }

        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuRelatorioFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRpt0024_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0024'. Você pode movê-la ou removê-la conforme necessário.
            this.dTRPT0024TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0024);
            //this.dTRPT0023TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0023);
            dtmFiltroDataInicio.Value = DateTime.Now.AddDays(-1);
            dtmFiltroDataFim.Value = DateTime.Now.AddDays(-1);
            cmbFiltroTipo.SelectedIndex = 0;
            dtmFiltroDataInicio.Location = new Point(240, 5);
            txtFiltro.Location = new Point(240, 5);
            cmbFiltroMegociador.Location = new Point(240, 5);
            dtmFiltroDataFim.Location = new Point(360, 5);

        }

        private void carregarNegociadores()
        {
            try
            {
                //cmbFiltroMegociador.AutoGenerateColumns = false;
                cmbFiltroMegociador.DataSource = null;


                cmbFiltroMegociador.DataSource = new ParcelaNegocios().negociadoresLista();
                cmbFiltroMegociador.DisplayMember = "TB037_Negociador";
                cmbFiltroMegociador.ValueMember = "TB037_Id";
                cmbFiltroMegociador.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var sSqFiltro = new StringBuilder();

                if (cmbFiltroTipo.SelectedIndex == 0)
                {
                    sSqFiltro.Append(" WHERE ");
                    sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                    sSqFiltro.Append(" AND ");
                    sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 2 ");
                    sSqFiltro.Append(" and ");
                    sSqFiltro.Append(" dbo.TB016_Parcela.TB016_DataPagamento ");
                    sSqFiltro.Append(" BETWEEN ");
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(dtmFiltroDataInicio.Value.ToString("MM/dd/yyyy"));
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(" AND ");
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(dtmFiltroDataFim.Value.ToString("MM/dd/yyyy"));
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(" OR ");
                    sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                    sSqFiltro.Append(" AND ");
                    sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 4 ");
                    sSqFiltro.Append(" and ");
                    sSqFiltro.Append(" dbo.TB016_Parcela.TB016_DataPagamento ");
                    sSqFiltro.Append(" BETWEEN ");
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(dtmFiltroDataInicio.Value.ToString("MM/dd/yyyy"));
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(" AND ");
                    sSqFiltro.Append("'");
                    sSqFiltro.Append(dtmFiltroDataFim.Value.ToString("MM/dd/yyyy"));
                    sSqFiltro.Append("'");
                }
                else
                {
                    if (cmbFiltroTipo.SelectedIndex == 1)
                    {
                        sSqFiltro.Append(" WHERE ");
                        sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                        sSqFiltro.Append(" AND ");
                        sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 2 ");
                        sSqFiltro.Append(" and ");
                        sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Emissao ");
                        sSqFiltro.Append(" BETWEEN ");
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(dtmFiltroDataInicio.Value.ToString("MM/dd/yyyy"));
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(" AND ");
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(dtmFiltroDataFim.Value.ToString("MM/dd/yyyy"));
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(" OR ");
                        sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                        sSqFiltro.Append(" AND ");
                        sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 4 ");
                        sSqFiltro.Append(" and ");
                        sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Emissao ");
                        sSqFiltro.Append(" BETWEEN ");
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(dtmFiltroDataInicio.Value.ToString("MM/dd/yyyy"));
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(" AND ");
                        sSqFiltro.Append("'");
                        sSqFiltro.Append(dtmFiltroDataFim.Value.ToString("MM/dd/yyyy"));
                        sSqFiltro.Append("'");

                    }
                    else
                    {
                        if (cmbFiltroTipo.SelectedIndex == 2)
                        {
                            sSqFiltro.Append(" WHERE ");
                            sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                            sSqFiltro.Append(" AND ");
                            sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 2 ");
                            sSqFiltro.Append(" and ");
                            sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Vencimento ");
                            sSqFiltro.Append(" BETWEEN ");
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(dtmFiltroDataInicio.Value.ToString("MM/dd/yyyy"));
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(" AND ");
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(dtmFiltroDataFim.Value.ToString("MM/dd/yyyy"));
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(" OR ");
                            sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                            sSqFiltro.Append(" AND ");
                            sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 4 ");
                            sSqFiltro.Append(" and ");
                            sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Vencimento ");
                            sSqFiltro.Append(" BETWEEN ");
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(dtmFiltroDataInicio.Value.ToString("MM/dd/yyyy"));
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(" AND ");
                            sSqFiltro.Append("'");
                            sSqFiltro.Append(dtmFiltroDataFim.Value.ToString("MM/dd/yyyy"));
                            sSqFiltro.Append("'");
                        }
                        else
                        {
                            if (cmbFiltroTipo.SelectedIndex == 3)
                            {
                                /*Negociador*/
                                sSqFiltro.Append(" where ");
                                sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                                sSqFiltro.Append(" AND ");
                                sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 4 ");
                                sSqFiltro.Append(" AND ");
                                sSqFiltro.Append(" dbo.TB037_NegociacaoEntidade.TB037_Id ");
                                sSqFiltro.Append(" = ");
                                sSqFiltro.Append(Convert.ToInt64(cmbFiltroMegociador.SelectedValue));
                            }
                            else
                            {
                                if (cmbFiltroTipo.SelectedIndex == 4)
                                {
                                    /*Contrato*/
                                    sSqFiltro.Append(" WHERE ");
                                    sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                                    sSqFiltro.Append(" AND ");
                                    sSqFiltro.Append(" dbo.TB016_Parcela.TB012_id =  ");
                                    sSqFiltro.Append(txtFiltro.Text.Replace(".", "").Replace(",", "").Replace("/", "").Trim());
                                    sSqFiltro.Append(" AND ");
                                    sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 2 ");
                                    sSqFiltro.Append(" OR ");
                                    sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                                    sSqFiltro.Append(" AND ");
                                    sSqFiltro.Append(" dbo.TB016_Parcela.TB012_id = ");
                                    sSqFiltro.Append(txtFiltro.Text.Replace(".", "").Replace(",", "").Replace("/", "").Trim());
                                    sSqFiltro.Append(" AND ");
                                    sSqFiltro.Append("  dbo.TB016_Parcela.TB016_Status = 4 ");
                                }
                                else
                                {
                                    if (cmbFiltroTipo.SelectedIndex == 5)
                                    {
                                        sSqFiltro.Append(" WHERE ");
                                        sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                                        sSqFiltro.Append(" AND ");
                                        sSqFiltro.Append(" dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                                        sSqFiltro.Append("'");
                                        sSqFiltro.Append(txtFiltro.Text.Replace(".", "").Replace(",", "").Replace("/", "").Replace("-", "").Trim());
                                        sSqFiltro.Append("'");
                                        sSqFiltro.Append(" AND ");
                                        sSqFiltro.Append(" dbo.TB016_Parcela.TB016_Status = 2 ");
                                        sSqFiltro.Append(" OR ");
                                        sSqFiltro.Append(" dbo.TB016_Parcela.TB015_id = 0 ");
                                        sSqFiltro.Append(" AND ");
                                        sSqFiltro.Append(" dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                                        sSqFiltro.Append("'");
                                        sSqFiltro.Append(txtFiltro.Text.Replace(".", "").Replace(",", "").Replace("/", "").Replace("-", "").Trim());
                                        sSqFiltro.Append("'");
                                        sSqFiltro.Append(" AND ");
                                        sSqFiltro.Append("  dbo.TB016_Parcela.TB016_Status = 4 ");
                                    }
                                }
                            }
                        }
                    }
                }



                dTRPT0024TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  ");

                sSql.Append(" dbo.TB016_Parcela.TB016_Status ");
                sSql.Append(" ,dbo.TB016_Parcela.TB015_id ");
                sSql.Append(" ,dbo.TB016_Parcela.TB012_id ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_id ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_Parcela ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_TotalParcelas ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_Emissao ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_Vencimento ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_ValorBruto ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_ValorOutrosDesconto ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_Valor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_id ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_Ponto ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_FormaPagamento ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_NossoNumero ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_FormaProcessamentoBaixa ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_CadastradoEm ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_CadastradoPor ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_Id ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_NomeExibicao ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_DataPagamento ");
                sSql.Append(" ,dbo.TB016_Parcela.TB037_Comissao ");
                sSql.Append(" ,dbo.TB037_NegociacaoEntidade.TB037_Negociador ");
                sSql.Append(" ,dbo.TB037_NegociacaoEntidade.TB037_Id ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB016_Parcela ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB016_Parcela.TB016_CadastradoPor = dbo.TB011_APPUsuarios.TB011_Id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB037_NegociacaoEntidade ON dbo.TB016_Parcela.TB037_Id = dbo.TB037_NegociacaoEntidade.TB037_Id ");
                sSql.Append(" LEFT OUTER JOIN ");
                sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id ");
     

                sSql.Append(sSqFiltro.ToString());



                sSql.Append(" ORDER BY  ");
                sSql.Append(" dbo.TB016_Parcela.TB037_Id ");
                sSql.Append(" ,dbo.TB016_Parcela.TB012_id ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_id ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_id ");
                sSql.Append(" ,dbo.TB016_Parcela.TB016_FormaPagamento ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_NomeExibicao ");


                this.dTRPT0024TableAdapter.Adapter.SelectCommand.CommandText = sSql.ToString();

                try
                {
                    this.dTRPT0024TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0024);
                    rpwRPT0023.RefreshReport();
                }
                catch (Exception)
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtmFiltroDataInicio.Visible = false;
            dtmFiltroDataFim.Visible = false;
            txtFiltro.Visible = false;

            cmbFiltroMegociador.Visible = false;

            if (cmbFiltroTipo.SelectedIndex < 3)
            {
                dtmFiltroDataInicio.Visible = true;
                dtmFiltroDataFim.Visible = true;
            }
            else
            {
                if (cmbFiltroTipo.SelectedIndex == 3)
                {
                    carregarNegociadores();
                    cmbFiltroMegociador.Visible = true;
                }
                else
                {
                    if (cmbFiltroTipo.SelectedIndex == 4)
                    {
                        txtFiltro.Visible = true;
                    }
                    else
                    {
                        if (cmbFiltroTipo.SelectedIndex == 5)
                        {
                            txtFiltro.Visible = true;
                        }
                    }
                }
            }
        }
    }
}
