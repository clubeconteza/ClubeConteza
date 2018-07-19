using System;
using System.Windows.Forms;

namespace ExecutarWebService
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void cbxCartaoParaImpressao_MouseHover(object sender, EventArgs e)
        {
            lblDescricao.Text = "Envia todos os cartões gerados para impressão (impressaocartoes@clubeconteza.com.br) - Plano Familiar";
        }

        private void cbxCartaoParaImpressao_MouseLeave(object sender, EventArgs e)
        {
            lblDescricao.Text = "";
        }

        private void cbxCartaoParaImpressaoCorporativo_MouseHover(object sender, EventArgs e)
        {
            lblDescricao.Text = "Envia todos os cartões gerados para impressão (impressaocartoescorporativo@clubeconteza.com.br) - Plano Corporativo";

        }

        private void cbxCartaoParaImpressaoCorporativo_MouseLeave(object sender, EventArgs e)
        {
            lblDescricao.Text = "";
        }


        private void btnExecutar_Click(object sender, EventArgs e)
        {
            Executar();
        }

        private void Executar()
        {
            WSPortal.Portal WS = new WSPortal.Portal();
            WsSMS.SMS wSMS = new WsSMS.SMS();

            if (cbxCartaoParaImpressaoFamiliar.Checked == true)
            {
                Int16 Temp = WS.CartaoParaImpressaoFamiliar();
            }


            if (cbxEnviarSMS.Checked == true)
            {
                //Int16 Temp = wSMS.EnvioVariosSms();
            }



            trmFinalizar.Start();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            Executar();
        }

        private void trmFinalizar_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
    }
}
