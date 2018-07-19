using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class ProdutoDAO
    {
        /// <summary>
        /// Descrição:  Pesquisar produtos ligados a um plano
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<ProdutoController> ProdutoPlano(Int64 TB015_id)
        {
            List<ProdutoController> RetornoList = new List<ProdutoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                
                sSQL.Append("SELECT ");
                sSQL.Append("dbo.TB015_TB014.TB015_id, ");              
                sSQL.Append("dbo.TB014_Produtos.TB014_id,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_IdProtheus,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_ValorMultiplo,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Produto,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_ValorUnitario,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Descricao,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_ValidoContezinos,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_ValidoParceiros,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_ValidoComporativo, ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Maiores,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Menores,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Isentos,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Tipo,  ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Status ");
                sSQL.Append("FROM ");
                sSQL.Append("dbo.TB015_TB014 ");
                sSQL.Append("INNER JOIN ");
                sSQL.Append("dbo.TB014_Produtos  ");
                sSQL.Append("ON  ");
                sSQL.Append("dbo.TB015_TB014.TB014_id = dbo.TB014_Produtos.TB014_id ");
                sSQL.Append("WHERE ");
                sSQL.Append("dbo.TB015_TB014.TB015_id = ");
                sSQL.Append(TB015_id);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB014_Produtos.TB014_Status = 1 ");//Somente Produtos com status de Ativos

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProdutoController obj = new ProdutoController();

                    obj.TB014_id                = Convert.ToInt64(reader["TB014_id"]);
                    obj.TB014_IdProtheus        = reader["TB014_IdProtheus"].ToString();
                    obj.TB014_Produto           = reader["TB014_Produto"].ToString();
                    obj.TB014_ValorUnitario     = Convert.ToDouble(reader["TB014_ValorUnitario"]);
                    obj.TB014_Descricao         = reader["TB014_Descricao"].ToString();
                    obj.TB014_ValidoContezinos  = Convert.ToInt16(reader["TB014_ValidoContezinos"]);
                    obj.TB014_ValidoParceiros   = Convert.ToInt16(reader["TB014_ValidoParceiros"]);
                    obj.TB014_ValidoComporativo = Convert.ToInt16(reader["TB014_ValidoComporativo"]);
                    obj.TB014_Maiores           = Convert.ToInt16(reader["TB014_Maiores"]);
                    obj.TB014_Menores           = Convert.ToInt16(reader["TB014_Menores"]);
                    obj.TB014_Isentos           = Convert.ToInt16(reader["TB014_Isentos"]);
                    obj.TB014_StatusS           = reader["TB014_Status"].ToString();
                    obj.TB014_ValorMultiplo     = Convert.ToInt16(reader["TB014_ValorMultiplo"]);
                    obj.TB014_TipoS             = reader["TB014_Tipo"].ToString();

                    RetornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetornoList;
        }
        public ProdutoController ProdutoID(Int64 TB014_id)
        {
            ProdutoController Retorno = new ProdutoController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT ");
                sSQL.Append(" * ");
                sSQL.Append(" FROM ");
                sSQL.Append(" TB014_Produtos ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB014_id = ");
                sSQL.Append( TB014_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Retorno.TB014_id                    = Convert.ToInt64(reader["TB014_id"]);
                    Retorno.TB014_IdProtheus            = reader["TB014_IdProtheus"].ToString();
                    Retorno.TB014_Produto               = reader["TB014_Produto"].ToString();
                    Retorno.TB014_ValorUnitario         = Convert.ToDouble(reader["TB014_ValorUnitario"]);
                    Retorno.TB014_Descricao             = reader["TB014_Descricao"].ToString();
                    Retorno.TB014_ValidoContezinos      = Convert.ToInt16(reader["TB014_ValidoContezinos"]);
                    Retorno.TB014_ValidoParceiros       = Convert.ToInt16(reader["TB014_ValidoParceiros"]);
                    Retorno.TB014_ValidoComporativo     = Convert.ToInt16(reader["TB014_ValidoComporativo"]);
                    Retorno.TB014_Maiores               = Convert.ToInt16(reader["TB014_Maiores"]);
                    Retorno.TB014_Menores               = Convert.ToInt16(reader["TB014_Menores"]);
                    Retorno.TB014_Isentos               = Convert.ToInt16(reader["TB014_Isentos"]);
                    Retorno.TB014_StatusS               = reader["TB014_Status"].ToString();
                    Retorno.TB014_TipoS                 = reader["TB014_Tipo"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaProdutosController> ProdutosParcelaID(long TB016_id)
        {
            List<ParcelaProdutosController> Retorno = new List<ParcelaProdutosController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append("SELECT * ");
                sSQL.Append("FROM dbo.TB017_ParcelaProduto ");
                sSQL.Append("WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaProdutosController obj = new ParcelaProdutosController();
                    obj.TB017_id            = Convert.ToInt64(reader["TB017_id"]);
                    obj.TB017_Item          = reader["TB017_Item"].ToString();
                    obj.TB017_IdProteus     = reader["TB017_IdProteus"] is DBNull ? reader["TB017_id"].ToString() : reader["TB017_IdProteus"].ToString().TrimEnd();
                    obj.TB017_ValorUnitario = Convert.ToDouble(reader["TB017_ValorUnitario"]);
                    obj.TB017_ValorDesconto = Convert.ToDouble(reader["TB017_ValorDesconto"]);
                    obj.TB017_ValorFinal    = Convert.ToDouble(reader["TB017_ValorFinal"]);
                    obj.TB017_TipoS         = reader["TB017_Tipo"].ToString();

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


        /// <summary>
        /// Descrição:  Listar produtos liberados para contezinos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       17/01/2018
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<ProdutoController> produtosContezinos()
        {
            List<ProdutoController> RetornoList = new List<ProdutoController>();
            try
            {
                SqlConnection con   = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL  = new StringBuilder();

                sSQL.Append(" SELECT  ");
                sSQL.Append(" TB014_id ");
                sSQL.Append(" ,TB014_Produto ");
                sSQL.Append(" ,TB014_ValidoContezinos ");
                sSQL.Append(" FROM ");
                sSQL.Append(" dbo.TB014_Produtos ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB014_Status = 1 ");
                sSQL.Append(" AND ");
                sSQL.Append(" TB014_ValidoContezinos = 1 ");
                sSQL.Append(" ORDER BY  ");
                sSQL.Append(" TB014_Produto ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                ProdutoController obj1 = new ProdutoController();
                obj1.TB014_id = 0;
                obj1.TB014_Produto ="<SELECIONE>";
                RetornoList.Add(obj1);

                while (reader.Read())
                {
                    ProdutoController obj = new ProdutoController();
                    obj.TB014_id        = Convert.ToInt64(reader["TB014_id"]);     
                    obj.TB014_Produto   = reader["TB014_Produto"].ToString();
                    RetornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetornoList;
        }
    }
}
