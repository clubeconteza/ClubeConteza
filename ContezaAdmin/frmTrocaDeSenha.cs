using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin
{
    public partial class frmTrocaDeSenha : Form
    {
        public frmTrocaDeSenha()
        {
            InitializeComponent();
        }

        private void frmTrocaDeSenha_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mskNovaSenha.Text.TrimStart().TrimEnd() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Senha "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskNovaSenha.Focus();
                return;
            }

            if (mskRepetirNovaSenha.Text.TrimStart().TrimEnd() == string.Empty)
            {
                MessageBox.Show(MensagensDoSistema._0001.ToString().Replace("$Campo", "Confirmação da Nova Senha "), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mskRepetirNovaSenha.Focus();
                return;
            }

            if (mskNovaSenha.Text.TrimStart().TrimEnd() != mskRepetirNovaSenha.Text.TrimStart().TrimEnd())
            {
                MessageBox.Show(MensagensDoSistema._0059, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UsuarioAPPNegocios Usuario_N = new UsuarioAPPNegocios();
                if(Usuario_N.AlterarMinhaSenha(ParametrosInterface.objUsuarioLogado.TB011_Id, mskNovaSenha.Text.TrimStart().TrimEnd(),ParametrosInterface.objUsuarioLogado.TB011_NomeExibicao))
                {
                    MessageBox.Show(MensagensDoSistema._0060, "Informativo Alteração de Senha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                }

            }
        }
    }
}
