using Controller;
using Negocios;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmCorporativoExportar : Form
    {
        public frmCorporativoExportar()
        {
            InitializeComponent();
        }

        private void frmCorporativoExportar_Load(object sender, EventArgs e)
        {
            var vQuery = "";
            carregarListaCorporativo(listarCorporativo(vQuery));
        }

        private void pcbFiltrarLista_Click(object sender, EventArgs e)
        {
            try
            {
                string tipoCampo;

                if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                {
                    tipoCampo = @"Nome";
                }
                else
                {
                    tipoCampo = txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "")
                                    .Trim().Length > 10 ? @"CPF" : @"Contrato";
                }

                switch (tipoCampo)
                {
                    case @"Nome":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                            carregarListaCorporativo(listarCorporativo(vQuery));
                            break;
                        }
                    case @"Contrato":
                        {
                            string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart();
                            carregarListaCorporativo(listarCorporativo(vQuery));
                            break;
                        }
                    case @"CPF":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                                .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                .Replace("-", "").Replace("/", "") + "'";
                            carregarListaCorporativo(listarCorporativo(vQuery));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<ContratosController> listarCorporativo(string query)
        {
            var retorno = new List<ContratosController>();

            try
            {
              retorno = new ContratoNegocios().corporativoListaParaExportacao(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return retorno;
        }
        private void carregarListaCorporativo(List<ContratosController> corporativos)
        {
            dgwContratos.AutoGenerateColumns = false;

            dgwContratos.DataSource = null;
            dgwContratos.DataSource = corporativos;
            dgwContratos.Refresh();

            //lblTotalRegistrosRetornados.Text = listarContratos.Count.ToString();

            //Int64 contrato = 0;
            //int contratos = 0;
            //for (var i = listarContratos.Count - 1; i >= 0; i--)
            //{
            //    if (Convert.ToInt64(listarContratos[i].TB012_Id) == contrato) continue;
            //    contratos++;
            //    contrato = Convert.ToInt64(listarContratos[i].TB012_Id);
            //}

            //lblTotalContratosRetornados.Text = contratos.ToString();
        }

        private void mnuListaFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFiltroAssociado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    string tipoCampo;

                    if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                    {
                        tipoCampo = @"Nome";
                    }
                    else
                    {
                        tipoCampo = txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "")
                                        .Trim().Length > 10 ? @"CPF" : @"Contrato";
                    }

                    switch (tipoCampo)
                    {
                        case @"Nome":
                            {
                                string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                                txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                                carregarListaCorporativo(listarCorporativo(vQuery));
                                break;
                            }
                        case @"Contrato":
                            {
                                string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                                txtFiltroAssociado.Text.TrimEnd().TrimStart();
                                carregarListaCorporativo(listarCorporativo(vQuery));
                                break;
                            }
                        case @"CPF":
                            {
                                string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                                    .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                    .Replace("-", "").Replace("/", "") + "'";
                                carregarListaCorporativo(listarCorporativo(vQuery));
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

        private void txtFiltroAssociado_Leave(object sender, EventArgs e)
        {
            try
            {
                string tipoCampo;

                if (Regex.IsMatch(txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim(), @"^[ a-zA-Z á]*$"))
                {
                    tipoCampo = @"Nome";
                }
                else
                {
                    tipoCampo = txtFiltroAssociado.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "")
                                    .Trim().Length > 10 ? @"CPF" : @"Contrato";
                }

                switch (tipoCampo)
                {
                    case @"Nome":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_NomeFantasia LIKE '" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart() + "%'";
                            carregarListaCorporativo(listarCorporativo(vQuery));
                            break;
                        }
                    case @"Contrato":
                        {
                            string vQuery = " AND dbo.TB012_Contratos.TB012_id =" +
                                            txtFiltroAssociado.Text.TrimEnd().TrimStart();
                            carregarListaCorporativo(listarCorporativo(vQuery));
                            break;
                        }
                    case @"CPF":
                        {
                            string vQuery = " AND dbo.TB020_Unidades.TB020_Documento = '" + txtFiltroAssociado.Text
                                                .TrimEnd().TrimStart().Replace(".", "").Replace(",", "")
                                                .Replace("-", "").Replace("/", "") + "'";
                            carregarListaCorporativo(listarCorporativo(vQuery));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuListaExportar_Click(object sender, EventArgs e)
        {
            try
            {
                //retorno = new ContratoNegocios().corporativoListaParaExportacao(query);
                new ContratoNegocios().corporativoArquivoExportar();
                MessageBox.Show("Fim", @"Fim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
