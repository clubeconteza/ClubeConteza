using System;
using System.Windows.Forms;
using Negocios;

namespace ContezaAdmin.RPT
{
    public partial class Frmrpt0013 : Form
    {
        public Frmrpt0013()
        {
            InitializeComponent();
        }

        private void frmrpt0013_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'clubeConteza_Relatorios.DTRPT0013'. Você pode movê-la ou removê-la conforme necessário.
            //dTRPT0013TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            //dTRPT0013TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0013);
            //reportViewer1.RefreshReport();
            try
            {
                cmbMunicipios.DataSource = null;
                cmbMunicipios.Items.Clear();
                cmbMunicipios.DataSource = new EnderecoNegocios().MunicipiosAtivos();
                cmbMunicipios.DisplayMember = "TB006_Municipio";
                cmbMunicipios.ValueMember = "TB006_id";

                cmbMunicipios.SelectedValue = ParametrosInterface.objUsuarioLogado.Municipio.TB006_id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void mnuRelatorioFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pcbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ptbGerarRelatorio_Click(object sender, EventArgs e)
        {
            dTRPT0013TableAdapter.Connection.ConnectionString = ParametrosInterface.ConectReport;
            dTRPT0013TableAdapter.Fill(clubeConteza_Relatorios.DTRPT0013,Convert.ToInt64(cmbMunicipios.SelectedValue));
            reportViewer1.RefreshReport();
        }
    }
}
