using Controller;
using Negocios;
using System;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{

    public partial class FrmContratoAnotacoes : Form
    {
        public long Tb012Id { get; set; }
        public FrmContratoAnotacoes(long vTb012Id)
        {
            Tb012Id = vTb012Id;
            InitializeComponent();
        }

        private void FrmContratoAnotacoes_Load(object sender, EventArgs e)
        {
            Text = @"Anotações do Contrato [" + Tb012Id + @"]";
            PreencherGrid();
        }

        private void mnuAnotacoesNovo_Click(object sender, EventArgs e)
        {
            lblTb026Id.Text = "";
            lblTb011NomeExibicao.Text = "";
            lblTb026Data.Text = "";
            txtTb026Anotacao.Text = "";
            txtTb026Anotacao.ReadOnly = false;

        }

        private void mnuAnotacoesSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTb026Anotacao.Text.Trim()))
            {
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Anotação"), @"Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTb026Anotacao.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(lblTb026Id.Text.Trim())) return;
            var anotacao = new AnotacoesController
            {
                Tb012Id = Tb012Id,
                Tb011Id = ParametrosInterface.objUsuarioLogado.TB011_Id,
                Tb026Data = DateTime.Now,
                Tb026Cod = "00000",
                TB026_Negociacao = 0,
                Tb026Anotacao = txtTb026Anotacao.Text
            };

            var retorno = new AnotacoesNegocios().Anotacaoinsert(anotacao);
            if (retorno <= 0) return;
            txtTb026Anotacao.ReadOnly = true;
            Anotacao(retorno);
            PreencherGrid();
            MessageBox.Show(MensagensDoSistema._0018, @"Aviso", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void Anotacao(long tb026Id)
        {
            try
            {
                var anotacao = new AnotacoesNegocios().AnotacaoSelect(tb026Id);
                lblTb026Id.Text = anotacao.Tb026Id.ToString();
                lblTb011NomeExibicao.Text = anotacao.Tb011NomeExibicao;
                lblTb026Data.Text = anotacao.Tb026Data.ToString("dd//MM/yyyy HH:mm");
                txtTb026Anotacao.Text = anotacao.Tb026Anotacao.TrimEnd();
                txtTb026Anotacao.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreencherGrid()
        {
            try
            {
                dgAnotacoes.AutoGenerateColumns = false;
                dgAnotacoes.DataSource = null;
                dgAnotacoes.DataSource = new AnotacoesNegocios().AnotacoesDoContrato(Tb012Id,"00000",2);
                dgAnotacoes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgAnotacoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    switch (dgAnotacoes.Columns[e.ColumnIndex].HeaderText)
                    {
                        case "Anotação":

                            Anotacao(Convert.ToInt64(dgAnotacoes.Rows[e.RowIndex].Cells["Tb026Id"].Value));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
