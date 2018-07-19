using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.Comercial
{
    public partial class frmsms : Form
    {
        public frmsms()
        {
            InitializeComponent();
        }

        private void frmsms_Load(object sender, EventArgs e)
        {
            ListarSMS();
        }
        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ListarSMS()
        {
            try
            {
                dgwLista.AutoGenerateColumns = false;

                dgwLista.DataSource = null;
                dgwLista.DataSource = new MensagemNegocios().smsListar(dtmReferencia.Value); 
                dgwLista.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pctFiltrar_Click(object sender, EventArgs e)
        {
            ListarSMS();
        }

        private void mnuListaEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if(!new MensagemNegocios().smsAgendados(dtmReferencia.Value))
                {

                }
                else
                {

                }
                ListarSMS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
