using Controller;
using DAO;
using System;
using System.Collections.Generic;


namespace Negocios
{
    public class ProdutoNegocios
    {
        public List<ProdutoController> ProdutoPlano(Int64 TB015_id)
        {
            try
            {
                ProdutoDAO DAO = new ProdutoDAO();

                return DAO.ProdutoPlano(TB015_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProdutoController ProdutoID(Int64 TB014_id)
        {
            try
            {
                ProdutoDAO DAO = new ProdutoDAO();

                return DAO.ProdutoID(TB014_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ParcelaProdutosController> ProdutosParcelaID(long TB016_id)
        {
            try
            {
                ProdutoDAO DAO = new ProdutoDAO();

                return DAO.ProdutosParcelaID(TB016_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProdutoController> produtosContezinos()
        {
            try
            {
                ProdutoDAO DAO = new ProdutoDAO();

                return DAO.produtosContezinos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
