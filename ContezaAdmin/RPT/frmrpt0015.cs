using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ContezaAdmin.RPT
{
    public partial class Frmrpt0015 : Form
    {
        public Frmrpt0015()
        {
            InitializeComponent();
        }

        private void mnuRelatorioFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frmrpt0015_Load(object sender, EventArgs e)
        {
            dtmContratoCanc.Value = DateTime.Now.AddDays(-1);
        }

        private void ptbGerarRelatorio_Click(object sender, EventArgs e)
        {
            var inicio = new DateTime(dtmContratoCanc.Value.Year, dtmContratoCanc.Value.Month, dtmContratoCanc.Value.Day, 00, 00, 00);
            var fim = new DateTime(dtmContratoCanc.Value.Year, dtmContratoCanc.Value.Month, dtmContratoCanc.Value.Day, 23, 59, 00);
            dTRPT0015TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            dTRPT0015TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0015, inicio.ToString("dd/MM/yyyy hh:mm" ), fim.ToString("dd/M/yyyy hh:mm"));
            var setup = rpwRpt0015.GetPageSettings();
            setup.Margins = new Margins(1, 1, 1, 1);
            rpwRpt0015.SetPageSettings(setup);

            rpwRpt0015.RefreshReport();
        }

        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
