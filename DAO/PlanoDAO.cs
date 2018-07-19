using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class PlanoDao
    {
        /// <summary>
        /// Descrição:  Retorna plano de acordo com categoria de idade, local de cadastro, data validade, e status = Somente para Contezinos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       01/30/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet PlanoVendaContezino(PlanoController filtro, short contezinos, short parceiro, int TB013_Tipo)
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();
                sSql.Append("SELECT  ");
                sSql.Append("dbo.TB015_Planos.TB015_id,  ");
                sSql.Append("SUM(dbo.TB014_Produtos.TB014_ValorUnitario) AS ValorPlano,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Plano,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Ciclo,  ");
                sSql.Append("dbo.TB015_Planos.TB015_IOF,  ");
                sSql.Append("dbo.TB015_Planos.TB015_LiberadoCPF,  ");
                sSql.Append("dbo.TB015_Planos.TB015_LiberadoCNPJ,  ");
                sSql.Append("dbo.TB015_Planos.TB015_TipoVencimento,  ");
                sSql.Append("dbo.TB015_Planos.TB015_EspecieDocumento, ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc1,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc2,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc3,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc4,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc5, ");
                sSql.Append("dbo.TB015_Planos.TB015_Juros, ");
                sSql.Append("dbo.TB015_Planos.TB015_Multa ");
                sSql.Append("FROM ");
                sSql.Append("dbo.TB015_Planos ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB015_TB002 ON dbo.TB015_Planos.TB015_id = dbo.TB015_TB002.TB015_id  ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB015_TB014 ON dbo.TB015_Planos.TB015_id = dbo.TB015_TB014.TB015_id  ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB014_Produtos ON dbo.TB015_TB014.TB014_id = dbo.TB014_Produtos.TB014_id ");
                sSql.Append("GROUP BY dbo.TB015_Planos.TB015_id,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Plano,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Maiores, ");
                sSql.Append("dbo.TB015_Planos.TB015_Menores,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Isentos,  ");
                sSql.Append("dbo.TB015_TB002.TB002_id,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Fim, ");
                sSql.Append("dbo.TB015_Planos.TB015_Status, ");
                sSql.Append("dbo.TB015_Planos.TB015_Contezinos, ");
                sSql.Append("dbo.TB015_Planos.TB015_Parceiros, ");
                sSql.Append("dbo.TB015_Planos.TB015_Inicio,  ");
                sSql.Append("dbo.TB015_Planos.TB015_Ciclo,  ");
                sSql.Append("dbo.TB015_Planos.TB015_IOF,  ");
                sSql.Append("dbo.TB015_Planos.TB015_LiberadoCPF,  ");
                sSql.Append("dbo.TB015_Planos.TB015_LiberadoCNPJ,  ");
                sSql.Append("dbo.TB015_Planos.TB015_TipoVencimento, ");
                sSql.Append("dbo.TB015_Planos.TB015_EspecieDocumento, ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc1,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc2,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc3,  ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc4, ");
                sSql.Append("dbo.TB015_Planos.TB015_BoletoDesc5, ");
                sSql.Append("dbo.TB015_Planos.TB015_Juros, ");
                sSql.Append("dbo.TB015_Planos.TB015_Multa ");
                sSql.Append("HAVING ");
                sSql.Append("dbo.TB015_Planos.TB015_Maiores = ");
                sSql.Append(filtro.TB015_Maiores);
                sSql.Append("AND ");
                sSql.Append("dbo.TB015_Planos.TB015_Menores =  ");

                if (filtro.TB015_Menores > 0)
                {
                    sSql.Append(1);
                }
                else
                {
                    sSql.Append(0);
                }


                sSql.Append("AND ");
                sSql.Append("dbo.TB015_Planos.TB015_Isentos = ");

                if (filtro.TB015_Isentos > 0)
                {
                    sSql.Append(1);
                }
                else
                {
                    sSql.Append(0);
                }
                sSql.Append("AND ");
                sSql.Append("dbo.TB015_TB002.TB002_id =  ");
                sSql.Append(filtro.PontoDeVenda.TB002_id);
               
                sSql.Append("AND ");
                sSql.Append("dbo.TB015_Planos.TB015_Status = 1 "); //Somente Planos Ativos


                if (contezinos == 1)
                {
                    sSql.Append("AND ");
                    sSql.Append("dbo.TB015_Planos.TB015_Contezinos = 1 ");
                }
                if (parceiro == 1)
                {
                    sSql.Append("AND ");
                    sSql.Append("dbo.TB015_Planos.TB015_Parceiros = 1 ");
                }
    
                if(TB013_Tipo==1)
                {
                    sSql.Append(" AND dbo.TB015_Planos.TB015_LiberadoCPF =  1 ");
                }
                else
                {
                    sSql.Append(" AND dbo.TB015_Planos.TB015_LiberadoCNPJ =  1 ");
                }
  
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sSql.ToString(), con);

                da.Fill(dsRetorno);
                da.Dispose();
                con.Dispose();

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }


        /// <summary>
        /// Descrição:  Retorna plano de acordo com categoria de idade, local de cadastro, data validade, e status = Somente para Contezinos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       01/30/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet PlanoNegociacao()
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();

                sSql.Append("SELECT dbo.TB015_Planos.TB015_id, SUM(dbo.TB014_Produtos.TB014_ValorUnitario) AS ValorPlano, dbo.TB015_Planos.TB015_Plano, dbo.TB015_Planos.TB015_Ciclo, dbo.TB015_Planos.TB015_IOF,  ");
                sSql.Append("dbo.TB015_Planos.TB015_TipoVencimento, dbo.TB015_Planos.TB015_EspecieDocumento, dbo.TB015_Planos.TB015_BoletoDesc1, dbo.TB015_Planos.TB015_BoletoDesc2, dbo.TB015_Planos.TB015_BoletoDesc3,  ");
                sSql.Append(" dbo.TB015_Planos.TB015_BoletoDesc4, dbo.TB015_Planos.TB015_BoletoDesc5, dbo.TB015_Planos.TB015_Juros, dbo.TB015_Planos.TB015_Multa ");
                sSql.Append("FROM            dbo.TB015_Planos INNER JOIN ");
                sSql.Append("dbo.TB015_TB014 ON dbo.TB015_Planos.TB015_id = dbo.TB015_TB014.TB015_id INNER JOIN ");
                sSql.Append(" dbo.TB014_Produtos ON dbo.TB015_TB014.TB014_id = dbo.TB014_Produtos.TB014_id ");
                sSql.Append("GROUP BY dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB015_Planos.TB015_Maiores, dbo.TB015_Planos.TB015_Menores, dbo.TB015_Planos.TB015_Isentos, dbo.TB015_Planos.TB015_Fim,  ");
                sSql.Append(" dbo.TB015_Planos.TB015_Status, dbo.TB015_Planos.TB015_Contezinos, dbo.TB015_Planos.TB015_Parceiros, dbo.TB015_Planos.TB015_Inicio, dbo.TB015_Planos.TB015_Ciclo, dbo.TB015_Planos.TB015_IOF,  ");
                sSql.Append(" dbo.TB015_Planos.TB015_TipoVencimento, dbo.TB015_Planos.TB015_EspecieDocumento, dbo.TB015_Planos.TB015_BoletoDesc1, dbo.TB015_Planos.TB015_BoletoDesc2, dbo.TB015_Planos.TB015_BoletoDesc3,  ");
                sSql.Append(" dbo.TB015_Planos.TB015_BoletoDesc4, dbo.TB015_Planos.TB015_BoletoDesc5, dbo.TB015_Planos.TB015_Juros, dbo.TB015_Planos.TB015_Multa ");
                sSql.Append("HAVING(dbo.TB015_Planos.TB015_id = 0) ");


                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sSql.ToString(), con);

                da.Fill(dsRetorno);
                da.Dispose();
                con.Dispose();

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }

        /// <summary>
        /// Descrição:  Retorna plano de acordo com categoria de idade, local de cadastro, data validade, e status = Somente para Contezinos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       01/30/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        //public DataSet PlanoParceiros(long tb002Id, int tb015LiberadoCpf, int tb015LiberadoCnpj)
        //{
        //    DataSet dsRetorno = new DataSet();
        //    try
        //    {
        //        var sSql = new StringBuilder();

        //        sSql.Append(" SELECT dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB015_Planos.TB015_ValorAdesao, SUM(dbo.TB014_Produtos.TB014_ValorUnitario) AS Mensalidade, dbo.TB002_PontosDeVenda.TB002_id,  ");
        //        sSql.Append(" dbo.TB002_PontosDeVenda.TB002_Ponto, dbo.TB015_Planos.TB015_LiberadoCPF, dbo.TB015_Planos.TB015_LiberadoCNPJ ");
        //        sSql.Append(" FROM  dbo.TB015_Planos INNER JOIN ");
        //        sSql.Append(" dbo.TB015_TB014 ON dbo.TB015_Planos.TB015_id = dbo.TB015_TB014.TB015_id INNER JOIN ");
        //        sSql.Append(" dbo.TB014_Produtos ON dbo.TB015_TB014.TB014_id = dbo.TB014_Produtos.TB014_id INNER JOIN ");
        //        sSql.Append(" dbo.TB015_TB002 ON dbo.TB015_Planos.TB015_id = dbo.TB015_TB002.TB015_id INNER JOIN ");
        //        sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB015_TB002.TB002_id = dbo.TB002_PontosDeVenda.TB002_id ");
        //        sSql.Append(" WHERE ");
        //        sSql.Append(" dbo.TB015_Planos.TB015_Parceiros = 1 ");
        //        sSql.Append(" AND ");
        //        sSql.Append(" dbo.TB015_Planos.TB015_Status = 1 ");
        //        sSql.Append(" GROUP BY dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB015_Planos.TB015_ValorAdesao, dbo.TB002_PontosDeVenda.TB002_id, dbo.TB002_PontosDeVenda.TB002_Ponto,  ");
        //        sSql.Append(" dbo.TB015_Planos.TB015_LiberadoCPF, dbo.TB015_Planos.TB015_LiberadoCNPJ ");
        //        sSql.Append(" HAVING ");
        //        sSql.Append(" dbo.TB002_PontosDeVenda.TB002_id = ");
        //        sSql.Append(tb002Id);
        //        sSql.Append(" AND ");
        //        sSql.Append(" dbo.TB015_Planos.TB015_LiberadoCPF = ");
        //        sSql.Append(tb015LiberadoCpf);
        //        sSql.Append(" AND ");
        //        sSql.Append(" dbo.TB015_Planos.TB015_LiberadoCNPJ =  ");
        //        sSql.Append(tb015LiberadoCnpj);
        //        SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(sSql.ToString(), con);

        //        da.Fill(dsRetorno);
        //        da.Dispose();
        //        con.Dispose();

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //    return dsRetorno;
        //}

        /// <summary>
        /// Descrição:  Retorna plano de acordo com categoria de idade, local de cadastro, data validade, e status = Somente para Contezinos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       01/30/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        //public DataSet PlanoContrato(long tb015Id)
        //{
        //    var dsRetorno = new DataSet();
        //    try
        //    {
        //        var sSql = new StringBuilder();

        //        sSql.Append(" SELECT ");
        //        sSql.Append(" TB015_id, ");
        //        sSql.Append(" TB015_Plano ");
        //        sSql.Append(" FROM ");
        //        sSql.Append(" dbo.TB015_Planos ");
        //        sSql.Append(" GROUP BY ");
        //        sSql.Append(" TB015_id, ");
        //        sSql.Append(" TB015_Plano ");
        //        sSql.Append(" HAVING ");
        //        sSql.Append(" TB015_id = ");
        //        sSql.Append(tb015Id);

        //        SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(sSql.ToString(), con);

        //        da.Fill(dsRetorno);
        //        da.Dispose();
        //        con.Dispose();

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //    return dsRetorno;
        //}

        public PlanoController PlanoVendaSelectId(long tb015Id, int maiores, int menores, int isentos)
        {
            PlanoController retorno = new PlanoController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT ");
                sSql.Append(" * ");
                sSql.Append(" FROM ");
                sSql.Append(" TB015_Planos ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB015_id = ");
                sSql.Append(tb015Id);

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB015_id                    = Convert.ToInt64(reader["TB015_id"]);
                    retorno.TB015_Plano                 = Convert.ToString(reader["TB015_Plano"]);
                    retorno.TB015_ValorAdesao           = Convert.ToDouble(reader["TB015_ValorAdesao"]);
                    retorno.TB015_PermiteAbonarAdesao   = Convert.ToInt16(reader["TB015_PermiteAbonarAdesao"]);
                    retorno.TB015_Contezinos            = Convert.ToInt16(reader["TB015_Contezinos"]);
                    retorno.TB015_Parceiros             = Convert.ToInt16(reader["TB015_Parceiros"]);
                    retorno.TB015_Corporativo           = Convert.ToInt16(reader["TB015_Corporativo"]);
                    retorno.TB015_Inicio                = Convert.ToDateTime(reader["TB015_Inicio"]);
                    retorno.TB015_Fim                   = Convert.ToDateTime(reader["TB015_Fim"]);
                    retorno.TB015_Ciclo                 = Convert.ToInt16(reader["TB015_Ciclo"]);
                    retorno.TB015_Descricao             = Convert.ToString(reader["TB015_Descricao"]);
                    retorno.TB015_Maiores               = Convert.ToInt16(reader["TB015_Maiores"]);
                    retorno.TB015_Menores               = Convert.ToInt16(reader["TB015_Menores"]);
                    retorno.TB015_Isentos               = Convert.ToInt16(reader["TB015_Isentos"]);
                    retorno.TB015_StatusS               = Convert.ToString(reader["TB015_Status"]);
                    retorno.TB015_EspecieDocumento      = Convert.ToString(reader["TB015_EspecieDocumento"]);
                    retorno.TB015_BoletoDesc1           = Convert.ToString(reader["TB015_BoletoDesc1"]);
                    retorno.TB015_BoletoDesc2           = Convert.ToString(reader["TB015_BoletoDesc2"]);
                    retorno.TB015_BoletoDesc3           = Convert.ToString(reader["TB015_BoletoDesc3"]);
                    retorno.TB015_BoletoDesc4           = Convert.ToString(reader["TB015_BoletoDesc4"]);
                    retorno.TB015_BoletoDesc5           = Convert.ToString(reader["TB015_BoletoDesc5"]);
                    retorno.TB015_TipoVencimento        = Convert.ToInt16(reader["TB015_TipoVencimento"]);
                    retorno.TB015_TipoVencimento        = Convert.ToInt16(reader["TB015_TipoVencimento"]);
                    retorno.TB015_IOF                   = Convert.ToDouble(reader["TB015_IOF"]);

                }
                con.Close();


                StringBuilder sProdutosDoPlano = new StringBuilder();


                sProdutosDoPlano.Append(" SELECT SUM(dbo.TB014_Produtos.TB014_ValorUnitario)AS ValorTotalProdutos,  ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_ValorMultiplo,  ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_Maiores, ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_Menores, ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_Isentos ");
                sProdutosDoPlano.Append(" FROM          ");
                sProdutosDoPlano.Append(" dbo.TB015_TB014 INNER JOIN ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos ON dbo.TB015_TB014.TB014_id = dbo.TB014_Produtos.TB014_id ");
                sProdutosDoPlano.Append(" GROUP BY dbo.TB015_TB014.TB015_id,  ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_ValorMultiplo,  ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_Maiores,  ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_Menores,  ");
                sProdutosDoPlano.Append(" dbo.TB014_Produtos.TB014_Isentos ");
                sProdutosDoPlano.Append(" HAVING dbo.TB015_TB014.TB015_id =  ");
                sProdutosDoPlano.Append(tb015Id);

                command = new SqlCommand(sProdutosDoPlano.ToString(), con);

                con.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int multiplo = Convert.ToInt16(reader["TB014_ValorMultiplo"]);
                    if (multiplo > 0)
                    {
                        if (Convert.ToInt16(reader["TB014_Maiores"]) > 0)
                        {
                            retorno.ValorTotalProdutos = Convert.ToDouble(reader["ValorTotalProdutos"]) * maiores;
                        }
                        if (Convert.ToInt16(reader["TB014_Menores"]) > 0)
                        {
                            retorno.ValorTotalProdutos = Convert.ToDouble(reader["ValorTotalProdutos"]) * menores;
                        }

                    }
                    else
                    {
                        retorno.ValorTotalProdutos = Convert.ToDouble(reader["ValorTotalProdutos"]);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }

        //public List<PlanoController> ListarPlanosPorTipo(int tipoContrato)
        //{
        //    var retorno = new List<PlanoController>();
        //    try
        //    {
        //        var con = new SqlConnection(ParametrosDAO.StringConexao);
        //        var sSql = new StringBuilder();

        //        sSql.Append(" SELECT TB015_id, TB015_Plano, TB015_Contezinos, TB015_Parceiros, TB015_Corporativo FROM dbo.TB015_Planos ");

        //        switch (tipoContrato)
        //        {
        //            case 1:
        //                sSql.Append(" WHERE TB015_Contezinos = 1");
        //                break;
        //            case 2:
        //                sSql.Append(" WHERE TB015_Parceiros = 1");
        //                break;
        //            case 3:
        //                sSql.Append(" WHERE TB015_Corporativo = 1");
        //                break;
        //            case 4:
        //                sSql.Append(" WHERE TB015_Contezinos = 1");
        //                break;
        //            case 5:
        //                sSql.Append(" WHERE TB015_Contezinos = 1");
        //                break;
        //        }

        //        sSql.Append(" ORDER BY TB015_Plano");



        //        SqlCommand command = new SqlCommand(sSql.ToString(), con);

        //        con.Open();
        //        var reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var obj = new PlanoController
        //            {
        //                TB015_id = Convert.ToInt64(reader["TB015_id"]),
        //                TB015_Plano = reader["TB015_Plano"].ToString().TrimEnd()
        //            };

        //            retorno.Add(obj);
        //        }

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //    return retorno;
        //}


        public List<PlanoController> ListarPlanos()
        {
            var retorno = new List<PlanoController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT TB015_id, TB015_Plano, TB015_Contezinos, TB015_Parceiros, TB015_Corporativo FROM dbo.TB015_Planos ORDER BY TB015_Contezinos DESC, TB015_Parceiros DESC, TB015_Corporativo DESC, TB015_Plano ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PlanoController
                    {
                        TB015_id = Convert.ToInt64(reader["TB015_id"]),
                        TB015_Plano = reader["TB015_Plano"].ToString().TrimEnd()
                    };

                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }

        public List<PlanoController> PlanosCorporativo()
        {
            var retorno = new List<PlanoController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();


                sSql.Append(" SELECT TB015_id, TB015_Plano, TB015_Corporativo, TB015_Status FROM dbo.TB015_Planos WHERE TB015_Corporativo = 1  AND(TB015_Status = 1)");

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                var obj1 = new PlanoController();
                obj1.TB015_id = 0;
                obj1.TB015_Plano = "<SELECIONE>";
                retorno.Add(obj1);

                while (reader.Read())
                {
                    var obj = new PlanoController();
                    obj.TB015_id = Convert.ToInt64(reader["TB015_id"]);
                    obj.TB015_Plano = reader["TB015_Plano"].ToString().TrimEnd();
                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }

        public double PlanoCorporativoValor(long tb015Id)
        {
            double retorno = 0;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB014_Produtos.TB014_ValorUnitario AS ValorTotalProdutos ");
                sSql.Append(" FROM dbo.TB015_Planos INNER JOIN ");
                sSql.Append(" dbo.TB015_TB014 ON dbo.TB015_Planos.TB015_id = dbo.TB015_TB014.id INNER JOIN ");
                sSql.Append(" dbo.TB014_Produtos ON dbo.TB015_TB014.TB014_id = dbo.TB014_Produtos.TB014_id ");
                sSql.Append(" WHERE dbo.TB015_Planos.TB015_id = ");
                sSql.Append(tb015Id);

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = Convert.ToDouble(reader["ValorTotalProdutos"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }

        public DataSet RecuperarPlano(long tb015Id)
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" * ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB015_Planos ");
                sSql.Append(" where ");
                sSql.Append(" TB015_id = ");
                sSql.Append(tb015Id);

                var con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                var da = new SqlDataAdapter(sSql.ToString(), con);

                da.Fill(dsRetorno);
                da.Dispose();
                con.Dispose();

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }
    }
}
