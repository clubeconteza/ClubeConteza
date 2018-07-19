using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.RPT
{
    public partial class frmRpt0026 : Form
    {
        public frmRpt0026()
        {
            InitializeComponent();
        }

        private void ptbFiltrar_Click(object sender, EventArgs e)
        {
            carregar();
        }

        private void frmRpt0026_Load(object sender, EventArgs e)
        {
            dtmInicio.Value = DateTime.Now.AddDays(-1);
            dtmFim.Value = DateTime.Now.AddDays(-1);
            this.dTRPT0026TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0026);
        }

        private void carregar()
        {
            try
            {
                var sSql = new StringBuilder();


            sSql.Append(" SELECT  ");
            sSql.Append(" dbo.TB016_Parcela.TB012_id ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_id ");
            sSql.Append(" , dbo.TB037_NegociacaoEntidade.TB037_Id ");
            sSql.Append(" , dbo.TB037_NegociacaoEntidade.TB037_Negociador ");
            sSql.Append(" , dbo.TB037_NegociacaoEntidade.TB037_TipoComissao ");
            sSql.Append(" , dbo.TB037_NegociacaoEntidade.TB037_Aliquota ");
            sSql.Append(" , dbo.TB037_NegociacaoEntidade.TB037_Valor ");
            sSql.Append(" , dbo.TB015_Planos.TB015_id ");
            sSql.Append(" , dbo.TB015_Planos.TB015_Plano ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Valor ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_ValorAdesao ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Status ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Emissao ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Vencimento ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_DataPagamento ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_DataPagamento AS Inicio ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_DataPagamento AS Fim ");
            sSql.Append(" FROM  ");
            sSql.Append(" dbo.TB016_Parcela  ");
            sSql.Append(" INNER JOIN  ");
            sSql.Append(" dbo.TB015_Planos  ");
            sSql.Append(" ON  ");
            sSql.Append(" dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id  ");
            sSql.Append(" INNER JOIN ");
            sSql.Append(" dbo.TB037_NegociacaoEntidade  ");
            sSql.Append(" ON  ");
            sSql.Append(" dbo.TB016_Parcela.TB037_Id = dbo.TB037_NegociacaoEntidade.TB037_Id ");
            sSql.Append(" WHERE ");
            sSql.Append(" dbo.TB015_Planos.TB015_id = 0 ");
            sSql.Append(" AND ");
            sSql.Append(" dbo.TB016_Parcela.TB016_Status = 5 ");
            sSql.Append(" AND ");
            sSql.Append(" dbo.TB016_Parcela.TB016_DataPagamento >= ");
            sSql.Append("'");
            sSql.Append(dtmInicio.Value.ToString("MM/dd/yyyy"));
            sSql.Append("'");
            sSql.Append(" AND ");
            sSql.Append(" dbo.TB016_Parcela.TB016_DataPagamento <= ");
            sSql.Append("'");
            sSql.Append(dtmFim.Value.ToString("MM/dd/yyyy"));
            sSql.Append("'");
            sSql.Append(" ORDER BY  ");
            sSql.Append(" dbo.TB037_NegociacaoEntidade.TB037_Id ");
            sSql.Append(" , dbo.TB016_Parcela.TB012_id ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_id ");


            this.dTRPT0026TableAdapter.Adapter.SelectCommand.CommandText = sSql.ToString();

           
                this.dTRPT0026TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0026);
                rpwRPT0026.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
