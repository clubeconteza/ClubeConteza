using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmRprContezinosContratosDiariosPorCanal : Form
    {
        public frmRprContezinosContratosDiariosPorCanal()
        {
            InitializeComponent();
        }

        private void frmRprContezinosContratosDiariosPorCanal_Load(object sender, EventArgs e)
        {
            sP_S_ContratosDiariosPorCanalTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            dtmDataInicio.Value = DateTime.Now.AddDays(-1);
            dtmDataFim.Value = dtmDataInicio.Value;
        }

        private void ptbFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                dtmDataInicio.Focus();
                //sP_S_ContratosDiariosPorCanalTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
                DateTime Fim  = new DateTime(dtmDataFim.Value.Year, dtmDataFim.Value.Month , dtmDataFim.Value.Day,23,59,59);
                sP_S_ContratosDiariosPorCanalTableAdapter.Fill(dBClubeContezaDataSet.SP_S_ContratosDiariosPorCanal, Convert.ToDateTime(dtmDataInicio.Value.ToString("dd/MM/yyyy")), Convert.ToDateTime(Fim.ToString("dd/MM/yyyy hh:mm:ss")));
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
