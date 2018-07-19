using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Text;
using System.IO;
using Negocios;
using Controller;
using ContezaAdmin.Administrativo;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;

namespace ContezaAdmin
{
    public partial class Login : Form
    {
        string _registro = @"SOFTWARE\Microsoft\Windows\ClubeConteza";
        public Login()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidaForm())
            {
                var filtro = new UsuarioAPPController();

                try
                {
                    filtro.TB011_CPF    = mskCPF.Text.Replace(".", "").Replace("-", "").Replace(",", "");
                    filtro.TB011_Senha  = txtSenha.Text.TrimEnd().TrimStart();

                    ParametrosInterface.objUsuarioLogado = new UsuarioAPPNegocios().LoginUsuarioAPPDAO(filtro);
                    ParametrosInterface.PastaLogoUnidade = "/Images/Unidades";
                    ParametrosInterface.Intranet = "http://intranet.clubeconteza.com.br";
                    ParametrosInterface.objUsuarioLogado.TB011_Senha= txtSenha.Text.TrimEnd().TrimStart();

                    if (Directory.Exists(@"c:\Temp"))
                    {
                        var dir = new DirectoryInfo(@"c:\Temp");

                        foreach (var fi in dir.GetFiles())
                        {
                            fi.Delete();
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(@"c:\Temp");
                    }

                    if (ParametrosInterface.objUsuarioLogado.TB011_Id == 0)
                    {
                        MessageBox.Show(MensagensDoSistema._0002, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Convert.ToString((int)((UsuarioAPPController.TB011_StatusE)Enum.Parse(typeof(UsuarioAPPController.TB011_StatusE), ParametrosInterface.objUsuarioLogado.TB011_StatusS))) == "0")
                        {
                            MessageBox.Show(MensagensDoSistema._0003, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            /*Confirma Vs do Sistema*/
                            if(ParametrosInterface.objUsuarioLogado.VS!= Application.ProductVersion)
                            {
                                MessageBox.Show(string.Format(MensagensDoSistema._0050, Application.ProductVersion, ParametrosInterface.objUsuarioLogado.VS), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                            }

                           ParametrosInterface.ConectReport = new UsuarioAPPNegocios().Conecxao();
                           var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                           config.ConnectionStrings.ConnectionStrings["ContezaAdmin.Properties.Settings.ClubeConteza_ConnectionString"].ConnectionString = ParametrosInterface.ConectReport;
                           System.Configuration.ConfigurationManager.RefreshSection("ClubeConteza_ConnectionString");
                 
                           Hide();
                           var principal = new FrmPrincipal();
                           principal.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        bool ValidaForm()
        {
            if (mskCPF.Text.Trim() != string.Empty)
            {
                if (txtSenha.Text.Trim() != string.Empty) return true;
                MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "Senha"), @"Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtSenha.Focus();
                return false;
            }

            MessageBox.Show(MensagensDoSistema._0001.Replace("$Campo", "CPF"), @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            mskCPF.Focus();
            return false;
        }

        public bool FonteExiste(string aMinhaFonte)
        {
            var fonts = new InstalledFontCollection();
            return fonts.Families.Any(f => f.Name.Equals(aMinhaFonte, StringComparison.OrdinalIgnoreCase));
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuBancoDeDados_Click(object sender, EventArgs e)
        {
            frmBancoDeDados banco = new frmBancoDeDados();
            banco.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if(Environment.UserName=="fabia" & Environment.MachineName=="FGE")
            {
                mskCPF.Text = @"02544181982";
                //mskCPF.Text = "03506122932";
                txtSenha.Text = @"temp";
            }
            //Verificar se a chave existe
            RegistryKey registroClubeConteza = Registry.CurrentUser.OpenSubKey(_registro);
            try
             {
                if (registroClubeConteza==null)
                {
                    //existe!      
                    RegistryKey criar = Registry.CurrentUser.CreateSubKey(_registro);
                    if (criar != null)
                    {
                        criar.SetValue("Servidor", "srvdbclubeconteza.database.windows.net");
                        criar.SetValue("Usuario", "db_clubeconteza");
                        criar.SetValue("Senha", "9/Z7yR0xZgiWSZwkIRGq+Q==");
                        criar.SetValue("Banco", "DBClubeConteza");
                        criar.Close();
                    }
                }

                mskCPF.Focus();
            }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message, @"Erro ao executar operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DateTime GetNetworkTime()
        {
            //Servidor nacional para melhor latência
            const string ntpServer = "a.ntp.br";

            // Tamanho da mensagem NTP - 16 bytes (RFC 2030)
            var ntpData = new byte[48];

            //Indicador de Leap (ver RFC), Versão e Modo
            ntpData[0] = 0x1B; //LI = 0 (sem warnings), VN = 3 (IPv4 apenas), Mode = 3 (modo cliente)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //123 é a porta padrão do NTP
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            //NTP usa UDP
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.Connect(ipEndPoint);

            //Caso NTP esteja bloqueado, ao menos nao trava o app
            socket.ReceiveTimeout = 3000;

            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();

            //Offset para chegar no campo "Transmit Timestamp" (que é
            //o do momento da saída do servidor, em formato 64-bit timestamp
            const byte serverReplyTime = 40;

            //Pegando os segundos
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //e a fração de segundos
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //Passando de big-endian pra little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //Tempo em **UTC**
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
    }
}
