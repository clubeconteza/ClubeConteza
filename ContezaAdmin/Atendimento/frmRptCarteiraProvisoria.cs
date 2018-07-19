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
    public partial class frmRptCarteiraProvisoria : Form
    {
        public Int64 TB012_id { get; set; }
 
        public frmRptCarteiraProvisoria(Int64 vTB012_id)
        {
            InitializeComponent();
            TB012_id = vTB012_id;
        }

        private void frmRptCarteiraProvisoria_Load(object sender, EventArgs e)
        {
            carteiraProvisoriaPorContratoTableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            carteiraProvisoriaPorContratoTableAdapter.Fill(clubeConteza_Relatorios.CarteiraProvisoriaPorContrato, TB012_id);
            this.reportViewer1.RefreshReport();
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
