using Negocios;
using System;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace ContezaAdmin.RPT
{
    public partial class Frmrpt0016 : Form
    {
        public Frmrpt0016()
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

        private void Frmrpt0016_Load(object sender, EventArgs e)
        {
            dtmDataInicio.Value = DateTime.Now.AddDays(-1);
            dtmDataFim.Value = DateTime.Now;
            PontosDeVenda();
        }

        private void PontosDeVenda()
        {
            try
            {
                cmbContratoPontosDeVenda.DataSource = null;
                cmbContratoPontosDeVenda.Items.Clear();
                cmbContratoPontosDeVenda.DataSource = new PontoDeVendaNegocios().PontosDeVendaLiberadosParaUsuario(ParametrosInterface.objUsuarioLogado).Tables[0];
                cmbContratoPontosDeVenda.DisplayMember = "TB002_Ponto";
                cmbContratoPontosDeVenda.ValueMember = "TB002_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ptbGerarRelatorio_Click(object sender, EventArgs e)
        {
            dTRPT0016TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            // dTRPT0016TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0016, dtmDataInicio.Value.ToString(CultureInfo.CurrentCulture), dtmDataFim.Value.ToString(CultureInfo.CurrentCulture), Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue));
            dTRPT0016TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0016, Convert.ToInt64(cmbContratoPontosDeVenda.SelectedValue), dtmDataInicio.Value.ToString(CultureInfo.CurrentCulture), dtmDataInicio.Value.ToString(CultureInfo.CurrentCulture));


                //, dtmDataInicio.Value.ToString(CultureInfo.CurrentCulture), dtmDataFim.Value.ToString(CultureInfo.CurrentCulture)));

            var setup = rpwRpt0016.GetPageSettings();
            setup.Margins = new Margins(1, 1, 1, 1);
            rpwRpt0016.SetPageSettings(setup);

            rpwRpt0016.RefreshReport();
        }

       
    }
}
