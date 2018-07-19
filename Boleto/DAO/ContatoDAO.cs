using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;


namespace Boleto.DAO
{
    public class ContatoDAO
    {

        public List<ContatoController> ContatosPortalDoParceiro(long TB020_id)
        {
            List<ContatoController> Retorno_L = new List<ContatoController>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" SELECT TB009_ExibirPortal, TB009_id, TB020_id, TB009_Tipo, TB009_Contato, TB009_Nota ");
                sSQL.Append(" FROM dbo.TB009_Contato ");
                sSQL.Append(" WHERE TB020_id = ");
                sSQL.Append(TB020_id);
                sSQL.Append(" AND TB009_ExibirPortal = 1 ");
                sSQL.Append(" ORDER BY TB009_Tipo ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ContatoController obj = new ContatoController();
                    obj.TB009_id            = Convert.ToInt64(reader["TB009_id"]);
                    obj.TB009_ExibirPortal  = Convert.ToInt16(reader["TB009_ExibirPortal"]);
                    obj.TB020_id            = Convert.ToInt64(reader["TB020_id"]);
                    obj.TB009_TipoS         = reader["TB009_Tipo"].ToString();
                    obj.TB009_Nota          = reader["TB009_Nota"].ToString();


                    String Contato = reader["TB009_Contato"].ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');

                    //Celular = 1,
                    //Fixo    = 2,
                    //Email   = 3,
                    //Nextel  = 4

                    if (Convert.ToInt16(obj.TB009_TipoS) == 3)//Email
                    {
                        obj.TB009_Contato = reader["TB009_Contato"].ToString();
                    }
                    else
                    {
                        if (Convert.ToInt16(obj.TB009_TipoS) < 4)//Nextel
                        {
                            //= FormataString("##/##/####", Contato);
                            if (Contato.Length == 10)
                            {
                                obj.TB009_Contato = Convert.ToUInt64(Contato).ToString(@"(00\)0000\-0000");
                            }
                            else
                            {
                                if (Contato.Length == 11)
                                {
                                    obj.TB009_Contato = Convert.ToUInt64(Contato).ToString(@"(00\)000\-000\-000");
                                }
                            }

                            //Obj.TB009_Contato = reader["TB009_Contato"].ToString();
                        }
                    }
                   

                    Retorno_L.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno_L;
        }
    }
}