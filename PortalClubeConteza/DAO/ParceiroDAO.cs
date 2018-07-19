using PortalClubeConteza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PortalClubeConteza.DAO
{
    public class ParceiroDAO
    {
        public List<Unidade> ParceiroSessaoCidade(long sessao, long cidade, int registros, int pagina, string nivel1, string nivel2, string nivel3)
        {
            List<Unidade> Retorno_L = new List<Unidade>();

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

                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));

                if (nivel1.Trim() != "0" & nivel2.Trim() == "0" & nivel3.Trim() == "0")
                {
                    string[] Parametros = nivel1.Split(';');
                    FiltrosNivel1 = " AND dbo.TB021_TB024.TB021_id = " + Parametros[0];

                    if (Parametros.Length > 1)
                    {
                        for (int i = 1; i < Parametros.Length; i++)
                        {
                            FiltrosNivel1 = FiltrosNivel1 + " OR dbo.TB021_TB024.TB021_id = " + Parametros[i];
                        }
                    }
                }
                else if (nivel1.Trim() != "0" & nivel2.Trim() != "0" & nivel3.Trim() == "0")
                {
                    string[] Parametros = nivel2.Split(';');
                    FiltrosNivel2 = " AND dbo.TB022_TB012.TB022_id = " + Parametros[0];

                    if (Parametros.Length > 1)
                    {
                        for (int i = 1; i < Parametros.Length; i++)
                        {
                            FiltrosNivel2 = FiltrosNivel2 + " OR dbo.TB022_TB012.TB022_id = " + Parametros[i];
                        }
                    }
                }
                else if (nivel1.Trim() != "0" & nivel2.Trim() != "0" & nivel3.Trim() != "0")
                {
                    //string[] Parametros = Nivel3.Split(';');
                    //FiltrosNivel3 = "";

                    //if (Parametros.Length > 1)
                    //{
                    //    for (int i = 1; i < Parametros.Length; i++)
                    //    {
                    //        FiltrosNivel3 = FiltrosNivel3 + "" + Parametros[i];
                    //    }
                    //}
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
                sSQL.Append(cidade);
                sSQL.Append("             AND dbo.TB021_TB024.TB024_Id = ");
                sSQL.Append(sessao);
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
                sSQL.Append(pagina);
                sSQL.Append("- 1) * ");
                sSQL.Append(registros);
                sSQL.Append(" ) ROWS ");
                sSQL.Append("           FETCH NEXT ");
                sSQL.Append(registros);
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
                cSQL.Append(cidade);
                cSQL.Append("             AND dbo.TB021_TB024.TB024_Id = ");
                cSQL.Append(sessao);
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

                Paginas = Paginas / registros;

                Paginas = Math.Ceiling(Paginas);
                con.Close();

                SqlCommand commandSelect = new SqlCommand(string.Format(sSQL.ToString(), FiltrosNivel1, FiltrosNivel2, FiltrosNivel3), con);
                con.Open();

                SqlDataReader reader = commandSelect.ExecuteReader();

                while (reader.Read())
                {
                    //MunicipioController obj = new MunicipioController();

                    if (TB020_id != Convert.ToInt64(reader["TB020_id"]))
                    {
                        Unidade ObjParceiro = new Unidade();
                        ObjParceiro.Id_T020 = Convert.ToInt64(reader["TB020_id"]);
                        ObjParceiro.Paginas = Paginas;
                        ObjParceiro.Id_T006 = Convert.ToInt64(reader["TB006_id"]);
                        ObjParceiro.Id_T012 = Convert.ToInt64(reader["TB012_id"]);
                        ObjParceiro.RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                        ObjParceiro.NomeFantasia = reader["TB020_NomeFantasia"].ToString();
                        ObjParceiro.CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "INFORMAR" : reader["TB020_CategoriaExibicao"].ToString().TrimEnd().TrimStart();

                        ObjParceiro.TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                        ObjParceiro.Cep = reader["TB020_Cep"].ToString();
                        ObjParceiro.Logradouro = reader["TB012_Logradouro"].ToString();
                        ObjParceiro.Numero = reader["TB012_Numero"].ToString();
                        ObjParceiro.Bairro = reader["TB012_Bairro"].ToString();
                        ObjParceiro.Complemento = reader["TB012_Complemento"].ToString();
                        ObjParceiro.TextoPortal = reader["TB020_TextoPortal"].ToString();
                        ObjParceiro.Contatos = ContatosParceiro(ObjParceiro.Id_T020);
                        ObjParceiro.Area = ParceiroAreaPrincipal(ObjParceiro.Id_T020);

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

        public List<Contato> ContatosParceiro(long Id_T020)
        {
            List<Contato> Retorno_L = new List<Contato>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB020_id, ");
                sSQL.Append("        TB009_id, ");
                sSQL.Append("        TB009_Tipo, ");
                sSQL.Append("        TB009_Contato, ");
                sSQL.Append("        TB009_ExibirPortal ");
                sSQL.Append("   FROM dbo.TB009_Contato ");
                sSQL.Append("  WHERE TB020_id = ");
                sSQL.Append(Id_T020);
                sSQL.Append("    AND TB009_ExibirPortal = 1 ");
                sSQL.Append("  ORDER BY TB009_Tipo ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Contato obj = new Contato();

                    obj.Id = Convert.ToInt64(reader["TB009_id"]);
                    obj.Tipo = Convert.ToInt16(reader["TB009_Tipo"]);

                    if (obj.Tipo == (int)Enum.Contato.Email)
                    {
                        obj.Descricao = reader["TB009_Contato"].ToString().TrimEnd();
                    }
                    else
                    {
                        if (obj.Tipo < (int)Enum.Contato.Nextel)
                        {
                            String Contato = reader["TB009_Contato"].ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim().TrimStart('0');

                            if (Contato.Length == 10)
                            {
                                obj.Descricao = Convert.ToUInt64(Contato).ToString(@"(00\)0000\-0000");
                            }
                            else
                            {
                                if (Contato.Length == 11)
                                {
                                    obj.Descricao = Convert.ToUInt64(Contato).ToString(@"(00\)000\-000\-000");
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

        public Categoria ParceiroAreaPrincipal(long Id_T020)
        {
            Categoria Retorno = new Categoria();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TOP(1) PERCENT dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB012_id, dbo.TB021_CategoriaNivel1.TB021_Descricao FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB021_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB021_TB012.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB012.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(Id_T020);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Categoria obj = new Categoria();

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

        public List<Unidade> ParceiroBuscaSessaoCidadeTodos(long sessao, long cidade, int linhas, int pagina, string buscar)
        {
            List<Unidade> Retorno_L = new List<Unidade>();

            try
            {
                using (SqlConnection connection = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString)))
                {
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
                    sSQL.Append("      INNER JOIN dbo.TB020_Unidades  ON dbo.TB006_Municipio.TB006_id = dbo.TB020_Unidades.TB006_id ");
                    sSQL.Append("      INNER JOIN dbo.TB021_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB021_TB012.TB012_id ");
                    sSQL.Append("      INNER JOIN dbo.TB021_TB024 ON dbo.TB021_TB012.TB021_id = dbo.TB021_TB024.TB021_id ");
                    sSQL.Append(" LEFT OUTER JOIN dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id ");
                    sSQL.Append(" LEFT OUTER JOIN dbo.TB027_PesoParceiros ON dbo.TB020_Unidades.TB020_id = dbo.TB027_PesoParceiros.TB020_id ");
                    sSQL.Append("           WHERE dbo.TB020_Unidades.TB020_Status = 1 ");
                    sSQL.Append("             AND dbo.TB012_Contratos.TB012_Status = 1 ");
                    sSQL.Append("             AND dbo.TB021_TB024.TB024_Id = ");
                    sSQL.Append(sessao);
                    sSQL.Append("             AND dbo.TB006_Municipio.TB006_id = ");
                    sSQL.Append(cidade);
                    sSQL.Append("             AND UPPER(dbo.TB020_Unidades.TB020_NomeFantasia) LIKE ");
                    sSQL.Append("'%" + buscar.Trim().ToUpper() + "%'");
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
                    sSQL.Append(pagina);
                    sSQL.Append(" - 1) * ");
                    sSQL.Append(linhas);
                    sSQL.Append(" ) ROWS ");
                    sSQL.Append("           FETCH NEXT ");
                    sSQL.Append(linhas);
                    sSQL.Append(" ROWS ONLY ");

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
                    cSQL.Append("      INNER JOIN dbo.TB020_Unidades  ON dbo.TB006_Municipio.TB006_id = dbo.TB020_Unidades.TB006_id ");
                    cSQL.Append("      INNER JOIN dbo.TB021_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB021_TB012.TB012_id ");
                    cSQL.Append("      INNER JOIN dbo.TB021_TB024 ON dbo.TB021_TB012.TB021_id = dbo.TB021_TB024.TB021_id ");
                    cSQL.Append(" LEFT OUTER JOIN dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id ");
                    cSQL.Append(" LEFT OUTER JOIN dbo.TB027_PesoParceiros ON dbo.TB020_Unidades.TB020_id = dbo.TB027_PesoParceiros.TB020_id ");
                    cSQL.Append("           WHERE dbo.TB020_Unidades.TB020_Status = 1 ");
                    cSQL.Append("             AND dbo.TB012_Contratos.TB012_Status = 1 ");
                    cSQL.Append("             AND dbo.TB021_TB024.TB024_Id = ");
                    cSQL.Append(sessao);
                    cSQL.Append("             AND dbo.TB006_Municipio.TB006_id = ");
                    cSQL.Append(cidade);
                    cSQL.Append("             AND UPPER(dbo.TB020_Unidades.TB020_NomeFantasia) LIKE ");
                    cSQL.Append("'%" + buscar.Trim().ToUpper() + "%'");
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

                    connection.Open();
                    SqlCommand commandContador = new SqlCommand(cSQL.ToString(), connection);

                    double paginas = 0;
                    SqlDataReader rCont = commandContador.ExecuteReader();
                    while (rCont.Read())
                    {
                        paginas = paginas + Convert.ToInt64(rCont["TotalRegistro"]);
                    }
                    rCont.Close();

                    paginas = paginas / linhas;

                    paginas = Math.Ceiling(paginas);
                    connection.Close();

                    connection.Open();
                    SqlCommand command = new SqlCommand(sSQL.ToString(), connection);

                    Int64 TB020_id = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (TB020_id != Convert.ToInt64(reader["TB020_id"]))
                        {
                            Unidade ObjParceiro = new Unidade();
                            ObjParceiro.Id_T020 = Convert.ToInt64(reader["TB020_id"]);
                            ObjParceiro.Paginas = paginas;
                            ObjParceiro.Id_T006 = Convert.ToInt64(reader["TB006_id"]);
                            ObjParceiro.Id_T012 = Convert.ToInt64(reader["TB012_id"]);

                            ObjParceiro.RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                            ObjParceiro.NomeFantasia = reader["TB020_NomeFantasia"].ToString();
                            ObjParceiro.CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "INFORMAR" : reader["TB020_CategoriaExibicao"].ToString().TrimEnd().TrimStart();

                            ObjParceiro.TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                            ObjParceiro.Cep = reader["TB020_Cep"].ToString();
                            ObjParceiro.Logradouro = reader["TB012_Logradouro"].ToString();
                            ObjParceiro.Numero = reader["TB012_Numero"].ToString();
                            ObjParceiro.Bairro = reader["TB012_Bairro"].ToString();
                            ObjParceiro.Complemento = reader["TB012_Complemento"].ToString();
                            ObjParceiro.TextoPortal = reader["TB020_TextoPortal"].ToString();
                            ObjParceiro.Contatos = ContatosParceiro(ObjParceiro.Id_T020);
                            ObjParceiro.Area = ParceiroAreaPrincipal(ObjParceiro.Id_T020);

                            Retorno_L.Add(ObjParceiro);
                        }

                        TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno_L;
        }

        public UnidadeDetalhe RetornaDetalheParceiroPessoaFisica(long Id_T020)
        {
            UnidadeDetalhe Retorno = new UnidadeDetalhe();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
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
                sSQL.Append(Id_T020);
                sSQL.Append(" ORDER BY dbo.TB020_Unidades.TB020_Matriz ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    /*Dados da Matriz do Contrato*/
                    Retorno.Id_T020 = Convert.ToInt64(reader["TB020_id"]);
                    Retorno.NomeFantasia = reader["TB020_NomeFantasia"].ToString().TrimEnd().TrimStart();
                    Retorno.RazaoSocial = reader["TB020_NomeExibicaoDetalhes"] is DBNull ? reader["TB020_RazaoSocial"].ToString().TrimEnd() : reader["TB020_NomeExibicaoDetalhes"].ToString().TrimEnd();
                    Retorno.CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "INFORMAR" : reader["TB020_CategoriaExibicao"].ToString().TrimEnd().TrimStart();
                    Retorno.TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                    Retorno.Matriz = Convert.ToInt16(reader["TB020_Matriz"]);
                    Retorno.Cep = reader["TB004_Cep"].ToString();
                    Retorno.Logradouro = reader["TB012_Logradouro"].ToString();
                    Retorno.Numero = reader["TB012_Numero"].ToString();
                    Retorno.Bairro = reader["TB012_Bairro"].ToString();
                    Retorno.Complemento = reader["TB012_Complemento"].ToString();
                    Retorno.TextoPortal = reader["TB020_TextoPortal"].ToString();

                    Municipio objMunicipio = new Municipio();
                    objMunicipio.Id = Convert.ToInt64(reader["TB006_id"]);
                    objMunicipio.Descricao = reader["TB006_Municipio"].ToString();
                    objMunicipio.Codigo = reader["TB006_Codigo"].ToString();
                    Retorno.Municipio = objMunicipio;

                    Estado objEstado = new Estado();
                    objEstado.Id = Convert.ToInt64(reader["TB005_Id"]);
                    objEstado.Sigla = reader["TB005_Sigla"].ToString();
                    objEstado.Descricao = reader["TB005_Estado"].ToString();
                    objEstado.Codigo = reader["TB005_Codigo"].ToString();
                    Retorno.Estado = objEstado;

                    Pais objPais = new Pais();
                    objPais.Id = Convert.ToInt64(reader["TB003_id"]);
                    objPais.Descricao = reader["TB003_Pais"].ToString();
                    objPais.DDI = reader["TB003_DDI"].ToString();
                    Retorno.Pais = objPais;

                    Retorno.Areas = ParceiroAreas(Id_T020);
                    Retorno.Contatos = ContatosParceiro(Id_T020);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Retorno;
        }

        public List<Categoria> ParceiroAreas(long Id_T020)
        {
            List<Categoria> Retorno_L = new List<Categoria>();

            try
            {
                List<CategoriaNivelUm> Nivel1 = ParceiroAreasNivel1(Id_T020);
                int i;

                for (i = 0; i < Nivel1.Count; i++)
                {
                    Categoria wRet = new Categoria();
                    StringBuilder sNivel1 = new StringBuilder();

                    wRet.Area = Nivel1[i].Descricao.TrimEnd();
                    Retorno_L.Add(wRet);

                    List<CategoriaNivelDois> Nivel2 = ParceiroAreasNivel2(Id_T020, Nivel1[i].Id);

                    int e;
                    for (e = 0; e < Nivel2.Count; e++)
                    {
                        Categoria wRet2 = new Categoria();
                        wRet2.Area = Nivel2[e].Descricao.TrimEnd();
                        Retorno_L.Add(wRet2);

                        List<CategoriaNivelTres> Nivel3 = ParceiroAreasNivel3(Id_T020, Nivel2[e].Id_T022);
                        int x;
                        for (x = 0; x < Nivel3.Count; x++)
                        {
                            Categoria wRet3 = new Categoria();
                            wRet3.Area = Nivel3[x].Descricao.TrimEnd();
                            Retorno_L.Add(wRet3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno_L;
        }

        public List<CategoriaNivelUm> ParceiroAreasNivel1(long Id_T020)
        {
            List<CategoriaNivelUm> Retorno_L = new List<CategoriaNivelUm>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append(" FROM dbo.TB021_TB012 INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB012.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades ON dbo.TB021_TB012.TB012_id = dbo.TB020_Unidades.TB012_id ");
                sSQL.Append(" GROUP BY dbo.TB020_Unidades.TB020_id, dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append(" HAVING dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(Id_T020);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaNivelUm obj = new CategoriaNivelUm();
                    obj.Id = Convert.ToInt64(reader["TB021_id"]);
                    obj.Descricao = reader["TB021_Descricao"].ToString().TrimEnd().Trim();
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

        public List<CategoriaNivelDois> ParceiroAreasNivel2(long Id_T020, long Id_T021)
        {
            List<CategoriaNivelDois> Retorno_L = new List<CategoriaNivelDois>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB022_CategoriaNivel2.TB021_id, dbo.TB020_Unidades.TB020_id, dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB022_CategoriaNivel2.TB022_Descricao ");
                sSQL.Append(" FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB022_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB022_TB012.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB022_CategoriaNivel2 ON dbo.TB022_TB012.TB022_id = dbo.TB022_CategoriaNivel2.TB022_id ");
                sSQL.Append(" GROUP BY dbo.TB020_Unidades.TB020_id, dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB022_CategoriaNivel2.TB022_Descricao, dbo.TB022_CategoriaNivel2.TB021_id ");
                sSQL.Append(" HAVING dbo.TB020_Unidades.TB020_id =");
                sSQL.Append(Id_T020);
                sSQL.Append(" AND dbo.TB022_CategoriaNivel2.TB021_id = ");
                sSQL.Append(Id_T021);
                sSQL.Append(" ORDER BY dbo.TB022_CategoriaNivel2.TB022_Descricao");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaNivelDois obj = new CategoriaNivelDois();
                    obj.Id_T022 = Convert.ToInt64(reader["TB022_id"]);
                    obj.Descricao = reader["TB022_Descricao"].ToString().TrimEnd();
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

        public List<CategoriaNivelTres> ParceiroAreasNivel3(long Id_T020, long Id_T022)
        {
            List<CategoriaNivelTres> Retorno_L = new List<CategoriaNivelTres>();

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB023_CategoriaNivel3.TB022_id, dbo.TB023_CategoriaNivel3.TB023_id, dbo.TB023_CategoriaNivel3.TB023_Descricao ");
                sSQL.Append(" FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB023_TB012 ON dbo.TB020_Unidades.TB012_id = dbo.TB023_TB012.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB023_CategoriaNivel3 ON dbo.TB023_TB012.TB023_id = dbo.TB023_CategoriaNivel3.TB023_id ");
                sSQL.Append(" GROUP BY dbo.TB020_Unidades.TB020_id, dbo.TB023_CategoriaNivel3.TB023_id, dbo.TB023_CategoriaNivel3.TB023_Descricao, dbo.TB023_CategoriaNivel3.TB022_id ");
                sSQL.Append(" HAVING dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(Id_T020);
                sSQL.Append(" AND dbo.TB023_CategoriaNivel3.TB022_id =  ");
                sSQL.Append(Id_T022);
                sSQL.Append(" ORDER BY dbo.TB023_CategoriaNivel3.TB023_Descricao ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaNivelTres obj = new CategoriaNivelTres();
                    obj.Id_T023 = Convert.ToInt64(reader["TB023_id"]);
                    obj.Descricao = reader["TB023_Descricao"].ToString().TrimEnd();
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