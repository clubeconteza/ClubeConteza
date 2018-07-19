using Negocios;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.Administrativo
{
    public partial class frmBancoDeDados : Form
    {
        public frmBancoDeDados()
        {
            InitializeComponent();
        }

        private void frmBancoDeDados_Load(object sender, EventArgs e)
        {
          
            try
            {
                string[] Parametros = new UsuarioAPPNegocios().RetornoConexao().Split('#');

                cmbServidor.Text = Parametros[0].ToString();
                txtBanco.Text = Parametros[1].ToString();
                txtUsuario.Text = Parametros[2].ToString();
                txtSenha.Text = Parametros[3].ToString();

                txtExe.Text = Environment.CurrentDirectory.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
         
            try
            {
                new UsuarioAPPNegocios().BancoDeDados(cmbServidor.Text, txtUsuario.Text, txtSenha.Text, txtBanco.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestar_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    string StringConexao = "Data Source=" + cmbServidor.Text.TrimEnd() + ";Initial Catalog=" + txtBanco.Text.TrimEnd() + ";User ID =" + txtUsuario.Text.TrimEnd() + ";Password=" + txtSenha.Text.TrimEnd() + ";Persist Security Info=" + "True";


                    sSQL.Append("SELECT MAX(TB001_id) AS Registros FROM dbo.TB001_Empresa");

                    SqlConnection con = new SqlConnection(StringConexao);
                    SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MessageBox.Show("Teste bem executado", "Clube Conteza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbServidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServidor.Text.Contains("FGE"))
            {
                txtUsuario.Text = "sa";
                txtSenha.Text = "root";
                txtBanco.Text = "DBClubeConteza_Local";
            }
            else
            {
                if (cmbServidor.Text.Contains("srvdbclubeconteza"))
                {
                    txtUsuario.Text = "db_clubeconteza";
                    txtSenha.Text = "eBOX1T52";
                    txtBanco.Text = "DBClubeConteza";
                }
                else
                {
                    if (cmbServidor.Text.Contains("191.232.160.34"))
                    {
                        txtUsuario.Text = "sa";
                        txtSenha.Text = "eBOX1T52";
                        txtBanco.Text = "DBClubeConteza_Hom";
                    }
                }
            }
        }
    }
}
