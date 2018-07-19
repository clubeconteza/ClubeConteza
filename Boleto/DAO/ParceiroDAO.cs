using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Boleto.DAO
{
    public class ParceiroDAO
    {
        public List<UnidadeController> SP_S_Parceiro_Sessao_Cidade(long Sessao, long Cidade, int Registros, int Pagina, string Nivel1, string Nivel2, string Nivel3)
        {
            List<UnidadeController> Retorno_L = new List<UnidadeController>();

            try
            {
                Int64 TB020_id = 0; //Evitar Duplicidade de Unidades no Controle
                double Paginas = 0;

                // 0 Todos
                // 1 Nivel 1 (Especialidades)
                // 2 Nivel 2 (Especialidades + Categorias)
                // 3 Nivel 3 (Especialidades + Categorias + SubCategorias)

                var FiltrosNivel1 = string.Empty;
                var FiltrosNivel2 = string.Empty;
                var FiltrosNivel3 = string.Empty;

                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));

                if (Nivel1.Trim() != "0" & Nivel2.Trim() == "0" & Nivel3.Trim() == "0")
                {
                    string[] Parametros = Nivel1.Split(';');
                    FiltrosNivel1 = " AND dbo.TB021_TB024.TB021_id = " + Parametros[0];

                    if (Parametros.Length > 1)
                    {
                        for (int i = 1; i < Parametros.Length; i++)
                        {
                            FiltrosNivel1 = FiltrosNivel1 + " OR dbo.TB021_TB024.TB021_id = " + Parametros[i];
                        }
                    }
                }
                else if (Nivel1.Trim() != "0" & Nivel2.Trim() != "0" & Nivel3.Trim() == "0")
                {
                    string[] Parametros = Nivel2.Split(';');
                    FiltrosNivel2 = " AND dbo.TB022_TB012.TB022_id = " + Parametros[0];

                    if (Parametros.Length > 1)
                    {
                        for (int i = 1; i < Parametros.Length; i++)
                        {
                            FiltrosNivel2 = FiltrosNivel2 + " OR dbo.TB022_TB012.TB022_id = " + Parametros[i];
                        }
                    }
                }
                else if (Nivel1.Trim() != "0" & Nivel2.Trim() != "0" & Nivel3.Trim() != "0")
                {

                }

                /*Definido tipo de busca*/
                /*Definir SQL de pesquisa*/

                StringBuilder sSQL = new StringBuilder();
                StringBuilder cSQL = new StringBuilder();

                sSQL.Append("          SELECT dbo.TB020_Unidades.TB020_id, ");
                sSQL.Append("                 dbo.TB006_Municipio.TB006_id, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB012_id, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB020_RazaoSocial, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB020_NomeFantasia, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB020_CategoriaExibicao, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB020_TipoPessoa, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB020_Cep, ");
                sSQL.Append("                 dbo.TB012_Contratos.TB012_Logradouro, ");
                sSQL.Append("                 dbo.TB012_Contratos.TB012_Numero, ");
                sSQL.Append("                 dbo.TB012_Contratos.TB012_Bairro, ");
                sSQL.Append("                 dbo.TB012_Contratos.TB012_Complemento, ");
                sSQL.Append("                 dbo.TB020_Unidades.TB020_TextoPortal ");
                sSQL.Append("            FROM dbo.TB006_Municipio ");
                sSQL.Append("      INNER JOIN dbo.TB020_Unidades ON dbo.TB006_Municipio.TB006_id = dbo.TB020_Unidades.TB006_id ");
                sSQL.Append("      INNER JOIN dbo.TB021_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB021_TB012.TB012_id ");
                sSQL.Append("      INNER JOIN dbo.TB021_TB024 ON dbo.TB021_TB012.TB021_id = dbo.TB021_TB024.TB021_id ");
                if (!string.IsNullOrEmpty(FiltrosNivel2))
                {
                    sSQL.Append("  INNER JOIN dbo.TB022_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB022_TB012.TB012_id ");
                }
                sSQL.Append(" LEFT OUTER JOIN dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" LEFT OUTER JOIN dbo.TB027_PesoParceiros ON dbo.TB020_Unidades.TB020_id = dbo.TB027_PesoParceiros.TB020_id ");
                sSQL.Append("           WHERE dbo.TB020_Unidades.TB020_Status = 1 ");
                sSQL.Append("             AND dbo.TB012_Contratos.TB012_Status = 1 ");
                sSQL.Append("             AND dbo.TB006_Municipio.TB006_id = ");
                sSQL.Append(Cidade);
                sSQL.Append("             AND dbo.TB021_TB024.TB024_Id = ");
                sSQL.Append(Sessao);
                sSQL.Append(" {0} ");
                sSQL.Append(" {1} ");
                sSQL.Append(" {2} ");
                sSQL.Append("           GROUP BY dbo.TB020_Unidades.TB020_id, ");
                sSQL.Append("                    dbo.TB006_Municipio.TB006_id, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB012_id, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_RazaoSocial, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_NomeFantasia, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_CategoriaExibicao, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_TipoPessoa, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_Cep, ");
                sSQL.Append("                    dbo.TB012_Contratos.TB012_Logradouro, ");
                sSQL.Append("                    dbo.TB012_Contratos.TB012_Numero, ");
                sSQL.Append("                    dbo.TB012_Contratos.TB012_Bairro, ");
                sSQL.Append("                    dbo.TB012_Contratos.TB012_Complemento, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_TextoPortal, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P01, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P02, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P03, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P04, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P05, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P06, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P07, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P08, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P09, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P10 ");
                sSQL.Append("           ORDER BY dbo.TB027_PesoParceiros.TB027_P01, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P02, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P03, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P04, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P05, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P06, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P07, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P08, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P09, ");
                sSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P10, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_NomeFantasia, ");
                sSQL.Append("                    dbo.TB020_Unidades.TB020_CategoriaExibicao ");
                sSQL.Append("          OFFSET (( ");
                sSQL.Append(Pagina);
                sSQL.Append("- 1) * ");
                sSQL.Append(Registros);
                sSQL.Append(" ) ROWS ");
                sSQL.Append("           FETCH NEXT ");
                sSQL.Append(Registros);
                sSQL.Append(" ROWS ONLY ");

                /*Contador*/
                cSQL.Append("  SELECT COUNT(*) AS TotalRegistro");
                cSQL.Append("    FROM (SELECT dbo.TB020_Unidades.TB020_id, ");
                cSQL.Append("                 dbo.TB006_Municipio.TB006_id, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB012_id, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB020_RazaoSocial, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB020_NomeFantasia, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB020_CategoriaExibicao, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB020_TipoPessoa, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB020_Cep, ");
                cSQL.Append("                 dbo.TB012_Contratos.TB012_Logradouro, ");
                cSQL.Append("                 dbo.TB012_Contratos.TB012_Numero, ");
                cSQL.Append("                 dbo.TB012_Contratos.TB012_Bairro, ");
                cSQL.Append("                 dbo.TB012_Contratos.TB012_Complemento, ");
                cSQL.Append("                 dbo.TB020_Unidades.TB020_TextoPortal ");
                cSQL.Append("            FROM dbo.TB006_Municipio ");
                cSQL.Append("      INNER JOIN dbo.TB020_Unidades ON dbo.TB006_Municipio.TB006_id = dbo.TB020_Unidades.TB006_id ");
                cSQL.Append("      INNER JOIN dbo.TB021_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB021_TB012.TB012_id ");
                cSQL.Append("      INNER JOIN dbo.TB021_TB024 ON dbo.TB021_TB012.TB021_id = dbo.TB021_TB024.TB021_id ");
                if (!string.IsNullOrEmpty(FiltrosNivel2))
                {
                    cSQL.Append("  INNER JOIN dbo.TB022_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB022_TB012.TB012_id ");
                }
                cSQL.Append(" LEFT OUTER JOIN dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id ");
                cSQL.Append(" LEFT OUTER JOIN dbo.TB027_PesoParceiros ON dbo.TB020_Unidades.TB020_id = dbo.TB027_PesoParceiros.TB020_id ");
                cSQL.Append("           WHERE dbo.TB020_Unidades.TB020_Status = 1 ");
                cSQL.Append("             AND dbo.TB012_Contratos.TB012_Status = 1 ");
                cSQL.Append("             AND dbo.TB006_Municipio.TB006_id = ");
                cSQL.Append(Cidade);
                cSQL.Append("             AND dbo.TB021_TB024.TB024_Id = ");
                cSQL.Append(Sessao);
                cSQL.Append(" {0} ");
                cSQL.Append(" {1} ");
                cSQL.Append(" {2} ");
                cSQL.Append("           GROUP BY dbo.TB020_Unidades.TB020_id, ");
                cSQL.Append("                    dbo.TB006_Municipio.TB006_id, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB012_id, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB020_RazaoSocial, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB020_NomeFantasia, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB020_CategoriaExibicao, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB020_TipoPessoa, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB020_Cep, ");
                cSQL.Append("                    dbo.TB012_Contratos.TB012_Logradouro, ");
                cSQL.Append("                    dbo.TB012_Contratos.TB012_Numero, ");
                cSQL.Append("                    dbo.TB012_Contratos.TB012_Bairro, ");
                cSQL.Append("                    dbo.TB012_Contratos.TB012_Complemento, ");
                cSQL.Append("                    dbo.TB020_Unidades.TB020_TextoPortal, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P01, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P02, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P03, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P04, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P05, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P06, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P07, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P08, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P09, ");
                cSQL.Append("                    dbo.TB027_PesoParceiros.TB027_P10) X ");

                SqlCommand commandContador = new SqlCommand(string.Format(cSQL.ToString(), FiltrosNivel1, FiltrosNivel2, FiltrosNivel3), con);
                con.Open();

                SqlDataReader rCont = commandContador.ExecuteReader();

                while (rCont.Read())
                {
                    Paginas = Paginas + Convert.ToInt64(rCont["TotalRegistro"]);
                }

                rCont.Close();

                Paginas = Paginas / Registros;

                Paginas = Math.Ceiling(Paginas);
                con.Close();

                SqlCommand commandSelect = new SqlCommand(string.Format(sSQL.ToString(), FiltrosNivel1, FiltrosNivel2, FiltrosNivel3), con);
                con.Open();

                SqlDataReader reader = commandSelect.ExecuteReader();

                while (reader.Read())
                {
                    MunicipioController obj = new MunicipioController();

                    if (TB020_id != Convert.ToInt64(reader["TB020_id"]))
                    {
                        UnidadeController ObjParceiro = new UnidadeController();
                        ObjParceiro.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                        ObjParceiro.Paginas = Paginas;
                        ObjParceiro.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                        ObjParceiro.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                        ObjParceiro.TB020_RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                        ObjParceiro.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();
                        ObjParceiro.TB020_CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "INFORMAR" : reader["TB020_CategoriaExibicao"].ToString().TrimEnd().TrimStart();

                        ObjParceiro.TB020_TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                        ObjParceiro.TB020_Cep = reader["TB020_Cep"].ToString();
                        ObjParceiro.TB020_Logradouro = reader["TB012_Logradouro"].ToString();
                        ObjParceiro.TB020_Numero = reader["TB012_Numero"].ToString();
                        ObjParceiro.TB020_Bairro = reader["TB012_Bairro"].ToString();
                        ObjParceiro.TB020_Complemento = reader["TB012_Complemento"].ToString();
                        ObjParceiro.TB020_TextoPortal = reader["TB020_TextoPortal"].ToString();
                        ObjParceiro.wContatos = ContatosParceiro(ObjParceiro.TB020_id);
                        ObjParceiro.Area = ParceiroAreaPrincipal(ObjParceiro.TB020_id);

                        Retorno_L.Add(ObjParceiro);
                    }

                    TB020_id = Convert.ToInt64(reader["TB020_id"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Retorno_L;
        }

        public List<wContatoController> ContatosParceiro(long TB020_id)
        {
            List<wContatoController> Retorno_L = new List<wContatoController>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB020_id, ");
                sSQL.Append("        TB009_id, ");
                sSQL.Append("        TB009_Tipo, ");
                sSQL.Append("        TB009_Contato, ");
                sSQL.Append("        TB009_ExibirPortal ");
                sSQL.Append("   FROM dbo.TB009_Contato ");
                sSQL.Append("  WHERE TB020_id = ");
                sSQL.Append(TB020_id);
                sSQL.Append("    AND TB009_ExibirPortal = 1 ");
                sSQL.Append("  ORDER BY TB009_Tipo ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    wContatoController obj = new wContatoController();

                    obj.Id = Convert.ToInt64(reader["TB009_id"]);
                    obj.Tipo = Convert.ToInt16(reader["TB009_Tipo"]);

                    if (obj.Tipo == (int)Enum.Contato.Email)
                    {
                        obj.Contato = reader["TB009_Contato"].ToString().TrimEnd();
                    }
                    else
                    {
                        if (obj.Tipo < (int)Enum.Contato.Nextel)
                        {
                            String Contato = reader["TB009_Contato"].ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');

                            if (Contato.Length == 10)
                            {
                                obj.Contato = Convert.ToUInt64(Contato).ToString(@"(00\)0000\-0000");
                            }
                            else
                            {
                                if (Contato.Length == 11)
                                {
                                    obj.Contato = Convert.ToUInt64(Contato).ToString(@"(00\)000\-000\-000");
                                }
                            }
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


        public List<UnidadeController> SP_S_Parceiro_Busca_Sessao_Cidade_Todos(long Sessao, long Cidade, int Linhas, int Pagina, string Buscar)
        {
            List<UnidadeController> Retorno_L = new List<UnidadeController>();

            try
            {

                using (SqlConnection connection = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    /*Parametros da Store Procedure*/
                    command.Parameters.Add(new SqlParameter("@TB006_id", Cidade));
                    command.Parameters.Add(new SqlParameter("@TB024_Id", Sessao));
                    command.Parameters.Add(new SqlParameter("@PageNumber", Pagina));
                    command.Parameters.Add(new SqlParameter("@RowspPage", Linhas));
                    command.Parameters.Add(new SqlParameter("@TB020_NomeFantasia", Buscar));

                    command.CommandText = "SP_S_Parceiro_Busca_Sessao_Cidade_Todos";



                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Int64 TB020_id = 0;
                    while (reader.Read())
                    {

                        if (TB020_id != Convert.ToInt64(reader["TB020_id"]))
                        {

                            UnidadeController ObjParceiro = new UnidadeController();
                            ObjParceiro.TB020_id = Convert.ToInt64(reader["TB020_id"]);

                            ObjParceiro.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                            ObjParceiro.TB012_id = Convert.ToInt64(reader["TB012_id"]);

                            ObjParceiro.TB020_RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                            ObjParceiro.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();
                            ObjParceiro.TB020_TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                            ObjParceiro.TB020_Cep = reader["TB020_Cep"].ToString();
                            ObjParceiro.TB020_Logradouro = reader["TB012_Logradouro"].ToString();
                            ObjParceiro.TB020_Numero = reader["TB012_Numero"].ToString();
                            ObjParceiro.TB020_Bairro = reader["TB012_Bairro"].ToString();
                            ObjParceiro.TB020_Complemento = reader["TB012_Complemento"].ToString();
                            ObjParceiro.TB020_TextoPortal = reader["TB020_TextoPortal"].ToString();

                            ObjParceiro.wContatos = ContatosParceiro(ObjParceiro.TB020_id);

                            ObjParceiro.Area = ParceiroAreaPrincipal(ObjParceiro.TB020_id);


                            Retorno_L.Add(ObjParceiro);
                        }

                        TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    }

                    reader.Close();
                    connection.Close();
                }

                double PaginasTeste = Retorno_L.Count;

                for (int i = 0; i < PaginasTeste; i++)
                {
                    Retorno_L[i].Paginas = PaginasTeste;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno_L;
        }

        public wCategoriaController ParceiroAreaPrincipal(long TB020_id)
        {
            wCategoriaController Retorno = new wCategoriaController();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TOP(1) PERCENT dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB012_id, dbo.TB021_CategoriaNivel1.TB021_Descricao FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB021_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB021_TB012.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB012.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(TB020_id);


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    wCategoriaController obj = new wCategoriaController();

                    Retorno.Area = reader["TB021_Descricao"].ToString().TrimEnd().Trim();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public UnidadeController UnidadeRecuperarLogo(long TB012_id)
        {
            UnidadeController Unidade_C = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB020_id, TB020_logo FROM dbo.TB020_Unidades WHERE TB020_id = ");
                sSQL.Append(TB012_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Unidade_C.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    Unidade_C.TB020_logo = (byte[])reader["TB020_logo"];
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Unidade_C;
        }

        public UnidadeController RetornaDetalheParceiroPessoaFisica(long TB020_id)
        {
            UnidadeController Retorno = new UnidadeController();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Matriz, dbo.TB020_Unidades.TB020_TipoPessoa, dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Cep, dbo.TB020_Unidades.TB020_Logradouro, dbo.TB020_Unidades.TB020_Numero, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Bairro, dbo.TB020_Unidades.TB020_Complemento, dbo.TB020_Unidades.TB020_TextoPortal, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Municipio, ");
                sSQL.Append(" dbo.TB006_Municipio.TB006_Codigo, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Sigla, dbo.TB005_Estado.TB005_Estado, dbo.TB005_Estado.TB005_Codigo, dbo.TB003_Pais.TB003_id, ");
                sSQL.Append(" dbo.TB003_Pais.TB003_Pais, dbo.TB003_Pais.TB003_DDI, TB012_Contratos_1.TB012_Logradouro, TB012_Contratos_1.TB012_Numero, TB012_Contratos_1.TB012_Bairro, ");
                sSQL.Append(" TB012_Contratos_1.TB012_Complemento, TB012_Contratos_1.TB004_Cep     , dbo.TB020_Unidades.TB020_NomeExibicaoDetalhes                 , dbo.TB020_Unidades.TB020_NomeFantasia, dbo.TB020_Unidades.TB020_CategoriaExibicao,  dbo.TB020_Unidades.TB020_RazaoSocial ");
                sSQL.Append(" FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB006_Municipio ON dbo.TB020_Unidades.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSQL.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSQL.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos AS TB012_Contratos_1 ON dbo.TB020_Unidades.TB012_id = TB012_Contratos_1.TB012_id ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(TB020_id);
                sSQL.Append(" ORDER BY dbo.TB020_Unidades.TB020_Matriz ");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    /*Dados da Matriz do Contrato*/
                    Retorno.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    Retorno.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString().TrimEnd().TrimStart();

                    Retorno.TB020_RazaoSocial = reader["TB020_NomeExibicaoDetalhes"] is DBNull ? reader["TB020_RazaoSocial"].ToString().TrimEnd() : reader["TB020_NomeExibicaoDetalhes"].ToString().TrimEnd();

                    Retorno.TB020_CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "INFORMAR" : reader["TB020_CategoriaExibicao"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                    Retorno.TB020_Matriz = Convert.ToInt16(reader["TB020_Matriz"]);
                    Retorno.TB020_Cep = reader["TB004_Cep"].ToString();
                    Retorno.TB020_Logradouro = reader["TB012_Logradouro"].ToString();

                    Retorno.TB020_Numero = reader["TB012_Numero"].ToString();
                    Retorno.TB020_Bairro = reader["TB012_Bairro"].ToString();
                    Retorno.TB020_Complemento = reader["TB012_Complemento"].ToString();
                    Retorno.TB020_TextoPortal = reader["TB020_TextoPortal"].ToString();

                    MunicipioController objMunicipio = new MunicipioController();
                    objMunicipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    objMunicipio.TB006_Municipio = reader["TB006_Municipio"].ToString();
                    objMunicipio.TB006_Codigo = reader["TB006_Codigo"].ToString();

                    Retorno.Municipio = objMunicipio;

                    EstadoController objEstado = new EstadoController();

                    objEstado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    objEstado.TB005_Sigla = reader["TB005_Sigla"].ToString();
                    objEstado.TB005_Estado = reader["TB005_Estado"].ToString();
                    objEstado.TB005_Codigo = reader["TB005_Codigo"].ToString();

                    Retorno.Estado = objEstado;

                    PaisController objPais = new PaisController();
                    objPais.TB003_id = Convert.ToInt64(reader["TB003_id"]);
                    objPais.TB003_Pais = reader["TB003_Pais"].ToString();
                    objPais.TB003_DDI = reader["TB003_DDI"].ToString();

                    Retorno.Pais = objPais;


                    Retorno.Areas = ParceiroAreas(TB020_id);
                }

                con.Close();

                /*Pesquisar Contatos*/
                ContatoDAO DAOContatos = new ContatoDAO();
                Retorno.wContatos = ContatosParceiro(TB020_id); ;// DAOContatos.ContatosPortalDoParceiro(TB020_id);

                /*Pesquisar Niveis 2 e 3*/
                CategoriaDAO DAONiveis = new CategoriaDAO();

                /*Pesquisar Locais de Atendimento*/
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Retorno;
        }

        public UnidadeController RetornaDetalheParceiroPessoaJuridica(long TB020_id)
        {
            UnidadeController Retorno = new UnidadeController();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB012_Contratos.TB012_id, dbo.TB020_Unidades.TB020_id, dbo.TB012_Contratos.TB012_Status, dbo.TB020_Unidades.TB020_Status,  dbo.TB020_Unidades.TB020_NomeExibicaoDetalhes,                 dbo.TB020_Unidades.TB020_NomeFantasia,  ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_TipoPessoa, dbo.TB006_Municipio.TB006_Municipio, dbo.TB006_Municipio.TB006_Codigo, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Sigla,  ");
                sSQL.Append(" dbo.TB005_Estado.TB005_Estado, dbo.TB003_Pais.TB003_id, dbo.TB003_Pais.TB003_Pais, dbo.TB003_Pais.TB003_DDI, dbo.TB020_Unidades.TB020_Cep, dbo.TB020_Unidades.TB020_Logradouro,  ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Numero, dbo.TB020_Unidades.TB020_Bairro, dbo.TB020_Unidades.TB020_Complemento, dbo.TB020_Unidades.TB020_TextoPortal, dbo.TB020_Unidades.TB020_CategoriaExibicao,  dbo.TB020_Unidades.TB020_RazaoSocial ");
                sSQL.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB006_Municipio ON dbo.TB020_Unidades.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSQL.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSQL.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_id =  ");
                sSQL.Append(TB020_id);
                sSQL.Append(" AND dbo.TB012_Contratos.TB012_Status = 1 AND dbo.TB020_Unidades.TB020_Status = 1 ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB020_id                        = Convert.ToInt64(reader["TB020_id"]);
                    Retorno.TB020_Cep                       = reader["TB020_Cep"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_Logradouro                = reader["TB020_Logradouro"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_Numero                    = reader["TB020_Numero"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_Bairro                    = reader["TB020_Bairro"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_Complemento               = reader["TB020_Complemento"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_TextoPortal               = reader["TB020_TextoPortal"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_StatusS                   = reader["TB020_Status"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_NomeFantasia              = reader["TB020_NomeFantasia"].ToString().TrimEnd().TrimStart();
            

                    Retorno.TB020_RazaoSocial               = reader["TB020_NomeExibicaoDetalhes"] is DBNull ? reader["TB020_RazaoSocial"].ToString().TrimEnd() : reader["TB020_NomeExibicaoDetalhes"].ToString().TrimEnd();

                    Retorno.TB020_CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "INFORMAR" : reader["TB020_CategoriaExibicao"].ToString().TrimEnd().TrimStart();
                    Retorno.TB020_TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);

                    Retorno.Contrato = new ContratosController();
                    Retorno.Contrato.TB012_Id = Convert.ToInt64(reader["TB012_id"]);
                    Retorno.Contrato.TB012_StatusS = reader["TB012_Status"].ToString().TrimEnd().TrimStart();


                    Retorno.Municipio = new MunicipioController();
                    Retorno.Municipio.TB006_Municipio = reader["TB006_Municipio"].ToString().TrimEnd().TrimStart();
                    Retorno.Municipio.TB006_Codigo = reader["TB006_Codigo"].ToString().TrimEnd().TrimStart();

                    Retorno.Estado = new EstadoController();
                    Retorno.Estado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    Retorno.Estado.TB005_Sigla = reader["TB005_Sigla"].ToString().TrimEnd().TrimStart();
                    Retorno.Estado.TB005_Estado = reader["TB005_Estado"].ToString().TrimEnd().TrimStart();

                    Retorno.Pais = new PaisController();
                    Retorno.Pais.TB003_id = Convert.ToInt64(reader["TB003_id"]);
                    Retorno.Pais.TB003_Pais = reader["TB003_Pais"].ToString().TrimEnd().TrimStart();
                    Retorno.Pais.TB003_DDI = reader["TB003_DDI"].ToString().TrimEnd().TrimStart();
                }

                con.Close();

                /*Pesquisar Contatos*/
                ContatoDAO DAOContatos = new ContatoDAO();
                Retorno.wContatos = ContatosParceiro(TB020_id);

                Retorno.Areas = ParceiroAreas(TB020_id);



                /*Midicos Ativos Vinculados*/

                List<ContratosController> ContratosVinculados = VinculoContratosSaude_S(Retorno.Contrato.TB002_id);


                List<UnidadeController> Medicos = new List<UnidadeController>();



                for (int i = 0; i < ContratosVinculados.Count; i++)
                {
                    UnidadeController Medico = new UnidadeController();
                    Medico = RetornaDetalheParceiroPessoaFisica(ContratosVinculados[i].Unidade.TB020_id);

                    Medicos.Add(Medico);
                }

                if (Medicos.Count > 0)
                {
                    Retorno.UnidadesVinculadas = Medicos;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Retorno;
        }


        public List<ContratosController> VinculoContratosSaude_S(long TB012_Pai)
        {
            List<ContratosController> Retorno_L = new List<ContratosController>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT dbo.TB028_VinculoContratosSaude.TB028_Id, dbo.TB028_VinculoContratosSaude.TB012_Pai, dbo.TB028_VinculoContratosSaude.TB012_Filho, dbo.TB012_Contratos.TB012_Status, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Status ");
                sSQL.Append(" FROM dbo.TB028_VinculoContratosSaude INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB028_VinculoContratosSaude.TB012_Filho = dbo.TB012_Contratos.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id ");
                sSQL.Append(" WHERE dbo.TB028_VinculoContratosSaude.TB012_Pai =  ");
                sSQL.Append(TB012_Pai);
                sSQL.Append(" AND dbo.TB012_Contratos.TB012_Status = 1 ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ContratosController Retorno = new ContratosController();
                    Retorno.TB012_Pai = Convert.ToInt64(reader["TB012_Pai"]);
                    Retorno.TB012_Filho = Convert.ToInt64(reader["TB012_Filho"]);
                    Retorno.Unidade = new UnidadeController();
                    Retorno.Unidade.TB020_id = Convert.ToInt64(reader["TB020_id"]);

                    Retorno_L.Add(Retorno);
                }

                con.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno_L;
        }


        public List<wCategoriaController> ParceiroAreas(long TB020_id)
        {
            List<wCategoriaController> Retorno_L = new List<wCategoriaController>();

            try
            {
                List<CategoriaController> Nivel1 = ParceiroAreasNivel1(TB020_id);
                int i;

                for (i = 0; i < Nivel1.Count; i++)
                {



                    wCategoriaController wRet = new wCategoriaController();
                    StringBuilder sNivel1 = new StringBuilder();


                    wRet.Area = Nivel1[i].TB021_Descricao.TrimEnd();
                    Retorno_L.Add(wRet);

                    List<CategoriaController> Nivel2 = ParceiroAreasNivel2(TB020_id, Nivel1[i].TB021_id);

                    //}
                    //else
                    //{
                    int e;
                    for (e = 0; e < Nivel2.Count; e++)
                    {
                        wCategoriaController wRet2 = new wCategoriaController();
                        wRet2.Area = Nivel2[e].TB022_Descricao.TrimEnd();
                        Retorno_L.Add(wRet2);

                        List<CategoriaController> Nivel3 = ParceiroAreasNivel3(TB020_id, Nivel2[e].TB022_id);
                        int x;
                        for (x = 0; x < Nivel3.Count; x++)
                        {
                            wCategoriaController wRet3 = new wCategoriaController();
                            wRet3.Area = Nivel3[x].TB023_Descricao.TrimEnd();
                            Retorno_L.Add(wRet3);
                        }

                    }
                    //}
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno_L;
        }

        public List<CategoriaController> ParceiroAreasNivel1(long TB020_id)
        {
            List<CategoriaController> Retorno_L = new List<CategoriaController>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append(" FROM dbo.TB021_TB012 INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB012.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades ON dbo.TB021_TB012.TB012_id = dbo.TB020_Unidades.TB012_id ");
                sSQL.Append(" GROUP BY dbo.TB020_Unidades.TB020_id, dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append(" HAVING dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(TB020_id);


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();

                    obj.TB021_id = Convert.ToInt64(reader["TB021_id"]);

                    obj.TB021_Descricao = reader["TB021_Descricao"].ToString().TrimEnd().Trim();
                    //

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

        public List<CategoriaController> ParceiroAreasNivel2(long TB020_id, long TB021_id)
        {
            List<CategoriaController> Retorno_L = new List<CategoriaController>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB022_CategoriaNivel2.TB021_id, dbo.TB020_Unidades.TB020_id, dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB022_CategoriaNivel2.TB022_Descricao ");
                sSQL.Append(" FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB022_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB022_TB012.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB022_CategoriaNivel2 ON dbo.TB022_TB012.TB022_id = dbo.TB022_CategoriaNivel2.TB022_id ");
                sSQL.Append(" GROUP BY dbo.TB020_Unidades.TB020_id, dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB022_CategoriaNivel2.TB022_Descricao, dbo.TB022_CategoriaNivel2.TB021_id ");
                sSQL.Append(" HAVING dbo.TB020_Unidades.TB020_id =");
                sSQL.Append(TB020_id);
                sSQL.Append(" AND dbo.TB022_CategoriaNivel2.TB021_id = ");
                sSQL.Append(TB021_id);
                sSQL.Append(" ORDER BY dbo.TB022_CategoriaNivel2.TB022_Descricao");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();

                    obj.TB022_id = Convert.ToInt64(reader["TB022_id"]);
                    //obj.TB022_Descricao = reader["TB022_Descricao"].ToString().TrimEnd().Trim();
                    //System.Globalization.CultureInfo cultureinfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    obj.TB022_Descricao = reader["TB022_Descricao"].ToString().TrimEnd();
                    //cultureinfo.TextInfo.ToTitleCase();

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

        public List<CategoriaController> ParceiroAreasNivel3(long TB020_id, long TB022_id)
        {
            List<CategoriaController> Retorno_L = new List<CategoriaController>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB023_CategoriaNivel3.TB022_id, dbo.TB023_CategoriaNivel3.TB023_id, dbo.TB023_CategoriaNivel3.TB023_Descricao ");
                sSQL.Append(" FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB023_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB023_TB012.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB023_CategoriaNivel3 ON dbo.TB023_TB012.TB023_id = dbo.TB023_CategoriaNivel3.TB023_id ");
                sSQL.Append(" GROUP BY dbo.TB020_Unidades.TB020_id, dbo.TB023_CategoriaNivel3.TB023_id, dbo.TB023_CategoriaNivel3.TB023_Descricao, dbo.TB023_CategoriaNivel3.TB022_id ");
                sSQL.Append(" HAVING dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(TB020_id);
                sSQL.Append(" AND dbo.TB023_CategoriaNivel3.TB022_id =  ");
                sSQL.Append(TB022_id);
                sSQL.Append(" ORDER BY dbo.TB023_CategoriaNivel3.TB023_Descricao ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();

                    obj.TB023_id = Convert.ToInt64(reader["TB023_id"]);
                    obj.TB023_Descricao = reader["TB023_Descricao"].ToString().TrimEnd();

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

        public bool ValidarParceiro(string Cnpj, string Senha)
        {
            try
            {
                var valida = false;
                using (var con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    con.Open();

                    var sql = new StringBuilder();
                    sql.Append("SELECT COUNT(*) QTD ");
                    sql.Append(" FROM TB034_Parceiro ");
                    sql.Append(" WHERE TB034_CNPJ = '" + (Cnpj != null ? Cnpj.Trim() : string.Empty) + "'");
                    sql.Append(" AND TB034_Senha = '" + new CriptografiaDAO().Encrypt(Senha) + "'");

                    using (var comando = new SqlCommand(sql.ToString(), con))
                    {
                        using (var leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                if (Convert.ToInt32(leitor["QTD"]) > 0)
                                {
                                    valida = true;
                                }
                            }
                        }
                    }
                }
                return valida;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public PessoaController ParceiroPrivilegio(string Cnpj, string Senha)
        {
            PessoaController retorno = new PessoaController();

            try
            {

                using (var con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {


                    var sql = new StringBuilder();
                    sql.Append("SELECT * ");
                    sql.Append(" FROM TB034_Parceiro ");
                    sql.Append(" WHERE TB034_CNPJ = '" + (Cnpj != null ? Cnpj.Trim() : string.Empty) + "'");
                    sql.Append(" AND TB034_Senha = '" + new CriptografiaDAO().Encrypt(Senha) + "'");

                    SqlCommand command = new SqlCommand(sql.ToString(), con);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.TB034_Id = Convert.ToInt64(reader["TB034_Id"]);
                        retorno.TB034_ImpContezinos = Convert.ToInt16(reader["TB034_ImpContezinos"]);
                        retorno.TB034_ImpParceiros = Convert.ToInt16(reader["TB034_ImpParceiros"]);

                        // retorno.TB020_logo = (byte[])reader["TB020_logo"];
                    }
                    con.Close();
                }
                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UnidadeController> ParceirosAtivos(string cnpjParceiro, string senhaParceiro)
        {

            if (!new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro))
            {
                throw new Exception("Parceiro não encontrado.");
            }

            PessoaController ParceiroPrivilegio = new ParceiroDAO().ParceiroPrivilegio(cnpjParceiro, senhaParceiro);

            if (ParceiroPrivilegio.TB034_ImpParceiros < 1)
            {
                throw new Exception("Parceiro sem acesso a funcionalidade");
            }

            List<UnidadeController> Retorno = new List<UnidadeController>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();


                //sSQL.Append(" SELECT  ");
                //sSQL.Append(" dbo.TB020_Unidades.TB020_Status ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_Status ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_Status ");
                //sSQL.Append(" , dbo.TB020_Unidades.TB020_id ");
                //sSQL.Append(" , dbo.TB020_Unidades.TB020_RazaoSocial ");
                //sSQL.Append(" , dbo.TB020_Unidades.TB020_NomeFantasia ");
                //sSQL.Append(" , dbo.TB020_Unidades.TB020_Documento ");
                //sSQL.Append(" , dbo.TB020_Unidades.TB020_TipoPessoa ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_id ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_id ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_Cartao ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                //sSQL.Append(" FROM  ");
                //sSQL.Append(" dbo.TB020_Unidades  ");
                //sSQL.Append(" INNER JOIN ");
                //sSQL.Append(" dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                //sSQL.Append(" INNER JOIN ");
                //sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                //sSQL.Append(" WHERE ");
                //sSQL.Append(" (dbo.TB020_Unidades.TB020_Status = 1) ");
                //sSQL.Append(" AND ");
                //sSQL.Append(" (dbo.TB012_Contratos.TB012_TipoContrato = 2) ");
                //sSQL.Append(" ORDER BY  ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_id ");
                //sSQL.Append(" , dbo.TB020_Unidades.TB020_id ");

                sSQL.Append(" SELECT  ");

                sSQL.Append(" dbo.TB020_Unidades.TB020_Status ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Status ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSQL.Append(" , dbo.TB020_Unidades.TB020_id ");
                sSQL.Append(" , dbo.TB020_Unidades.TB020_RazaoSocial ");
                sSQL.Append(" , dbo.TB020_Unidades.TB020_NomeFantasia ");
                sSQL.Append(" , dbo.TB020_Unidades.TB020_Documento ");
                sSQL.Append(" , dbo.TB020_Unidades.TB020_TipoPessoa ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_id ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Cartao ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSQL.Append(" , dbo.TB024_Sessao.TB024_Id ");
                sSQL.Append(" , dbo.TB024_Sessao.TB024_Sessao ");
                sSQL.Append("  FROM            ");
                sSQL.Append("  dbo.TB021_CategoriaNivel1  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB021_TB012 ON dbo.TB021_CategoriaNivel1.TB021_id = dbo.TB021_TB012.TB021_id  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB021_TB024 ON dbo.TB021_CategoriaNivel1.TB021_id = dbo.TB021_TB024.TB021_id  ");
                sSQL.Append("  INNER JOIN ");
                sSQL.Append(" dbo.TB024_Sessao ON dbo.TB021_TB024.TB024_Id = dbo.TB024_Sessao.TB024_Id  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos  ");
                sSQL.Append("  ON  ");
                sSQL.Append("  dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSQL.Append("   INNER JOIN ");
                sSQL.Append("   dbo.TB013_Pessoa  ");
                sSQL.Append("  ON  ");
                sSQL.Append("  dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id ON dbo.TB021_TB012.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Status = 1 ");
                sSQL.Append("  AND ");
                sSQL.Append("  dbo.TB012_Contratos.TB012_TipoContrato = 2 ");
                sSQL.Append("  ORDER BY  ");
                sSQL.Append("  dbo.TB012_Contratos.TB012_id ");
                sSQL.Append("  , dbo.TB020_Unidades.TB020_id ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UnidadeController obj = new UnidadeController();

                    obj.Pessoa = new PessoaController();
                    obj.Categoria = new CategoriaController();

                    obj.TB020_id                    = Convert.ToInt64(reader["TB020_id"]);
                    obj.TB020_TipoPessoa            = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                    obj.TB020_NomeFantasia          = reader["TB020_NomeFantasia"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.TB020_RazaoSocial           = reader["TB020_RazaoSocial"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.TB012_id                    = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB020_Documento             = reader["TB020_Documento"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();

                    obj.TB020_StatusS               = reader["TB020_Status"].ToString();

                    obj.Pessoa.TB013_id             = Convert.ToInt64(reader["TB013_id"]);
                    obj.Pessoa.TB013_NomeCompleto   = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.Pessoa.TB013_Cartao         = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.Pessoa.TB013_CPFCNPJ        = reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.Categoria.TB024_Id = Convert.ToInt64(reader["TB024_Id"]);

                    //Enum.GetName(typeof(UnidadeController.TB020_StatusE), Convert.ToInt16(reader["TB020_Status"]));

                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
    }
}