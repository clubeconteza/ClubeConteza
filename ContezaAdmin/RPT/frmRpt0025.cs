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
    public partial class frmRpt0025 : Form
    {
        public frmRpt0025()
        {
            InitializeComponent();
        }

        private void mnuRelatorioFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRpt0025_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0025'. Você pode movê-la ou removê-la conforme necessário.
            this.dTRPT0025TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0025);
            carregar();

        }

        private void ptbFiltrar_Click(object sender, EventArgs e)
        {
            carregar();

        }

        private void carregar()
        {
            var sSql = new StringBuilder();
            sSql.Append(" SELECT  ");

            sSql.Append(" dbo.TB012_Contratos.TB012_id ");
            sSql.Append(" , dbo.TB012_Contratos.TB012_TipoContrato ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_id ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Parcela ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_TotalParcelas ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Emissao ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Vencimento ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Valor ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Status ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_Pagador ");
            sSql.Append(" FROM ");
            sSql.Append(" dbo.TB012_Contratos INNER JOIN ");
            sSql.Append(" dbo.TB016_Parcela ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id ");
            sSql.Append(" WHERE ");
            sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 2 ");
            sSql.Append(" AND ");
            sSql.Append(" dbo.TB016_Parcela.TB016_Status = 4 ");
            sSql.Append(" ORDER BY  ");
            sSql.Append(" dbo.TB012_Contratos.TB012_id ");
            sSql.Append(" , dbo.TB016_Parcela.TB016_id ");


            this.dTRPT0025TableAdapter.Adapter.SelectCommand.CommandText = sSql.ToString();

            try
            {
                this.dTRPT0025TableAdapter.Fill(this.clubeConteza_Relatorios.DTRPT0025);
                rpwRPT0025.RefreshReport();
            }
            catch (Exception)
            {

            }
        }
    }
}
