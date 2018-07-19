using Boleto.Controller;
using Boleto.DAO;
using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Boleto.Negocios
{
    public class MensagemNegocios
    {
        private readonly string numUsu = "gconteza";
        private readonly string senha = "L6z09BGX";

        public bool smsLiberados()
        {
            try
            {
                List<SmsDataSetController> sms = new MensagemDAO().smsLiberados();

                if (sms.Count>0)
                {


                    var dt = new DataTable("EnviaSMS");
                    dt.Columns.Add("SeuNum");
                    dt.Columns.Add("Celular");
                    dt.Columns.Add("Mensagem");
                    dt.Columns.Add("Agendamento");

                    foreach (var item in sms)
                    {
                        var seuNum = string.Format("A{0}B{1}C{2}", item.TB012_id, item.TB009_id, item.TB039_id);
                        var celular = string.Concat("55", item.TB009_Contato.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", ""));
                        dt.Rows.Add(seuNum, celular, item.TB039_Mensagem, item.TB039_Agendamento.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    var ds = new DataSet("InDataSet");
                    ds.Tables.Add(dt);

                    var ws = new WSReluzCap.ReluzCapWebService();
                    var retorno = ws.EnviaSMSDataSet(numUsu, senha, ds);

                 
                    foreach (var obj in sms)
                    {
                        new MensagemDAO().confirmarenviomensagem(obj.TB039_id);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }


        public bool emailLiberados()
        {
            try
            {
                List<MensagemController> email = new MensagemDAO().emailsLiberados(DateTime.Now);

                if (email.Count > 0)
                {
                    string str_from_address = ParametrosNegocios.EmailSaidaLogin;
                    string str_name = ParametrosNegocios.NomeConta;


                    foreach (var obj in email)
                    {

                        
                        //The To address (Email ID)
                        string str_to_address = obj.TB009_Contato;

                        System.Net.Mail.MailMessage email_msg = new MailMessage();

                        //Specifying From,Sender & Reply to address
                        email_msg.From = new MailAddress(str_from_address, str_name);
                        email_msg.Sender = new MailAddress(str_from_address, str_name);
                        //email_msg.ReplyTo = new MailAddress(str_from_address, str_name);

                        //The To Email id
                        email_msg.To.Add(str_to_address);

                        email_msg.Subject = obj.TB039_Assunto;//Subject of email
                        StringBuilder HTML = new StringBuilder();

                        HTML.Append(obj.TB039_Mensagem);

                        //new MensagemDAO().confirmarenviomensagem(obj.TB039_id);
                        email_msg.IsBodyHtml = true;
                        email_msg.Body = HTML.ToString();//body
                                                         //Create an object for SmtpClient class
                        SmtpClient mail_client = new SmtpClient();

                        //Providing Credentials (Username & password)
                        NetworkCredential network_cdr = new NetworkCredential();
                        network_cdr.UserName = str_from_address;
                        network_cdr.Password = ParametrosNegocios.EmailSaidaSenha;

                        mail_client.Credentials = network_cdr;

                        //Specify the SMTP Port
                        mail_client.Port = ParametrosNegocios.EmailSaidaPorta;

                        //Specify the name/IP address of Host
                        mail_client.Host = ParametrosNegocios.EmailSaidaSMTP;

                        //Uses Secure Sockets Layer(SSL) to encrypt the connection
                        mail_client.EnableSsl = true;

                        //Now Send the message
                        mail_client.Send(email_msg);

                        new MensagemDAO().confirmarenviomensagem(obj.TB039_id);
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }
    }
}