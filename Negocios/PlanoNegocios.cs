using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Negocios
{
    public class PlanoNegocios
    {
        public DataSet PlanoVendaContezino(PlanoController filtro, short contezinos, short parceiro, int TB013_Tipo)
        {
            try
            {

                return new PlanoDao().PlanoVendaContezino(filtro, contezinos, parceiro, TB013_Tipo);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public DataSet PlanoNegociacao()
        {
            try
            {

                return new PlanoDao().PlanoNegociacao();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        //public DataSet PlanoParceiros(long tb002Id, int tb015LiberadoCpf, int tb015LiberadoCnpj)
        //{
        //    try
        //    {

        //        return new PlanoDao().PlanoParceiros(tb002Id, tb015LiberadoCpf, tb015LiberadoCnpj);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}

        //public DataSet PlanoContrato(long TB015_id)
        //{
        //    try
        //    {
        //        PlanoDao DAO = new PlanoDao();
        //        return DAO.PlanoContrato(TB015_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public PlanoController PlanoVendaSelectID(long TB015_id, int Maiores, int Menores, int isentos)
        //{
        //    try
        //    {

        //        PlanoDao DAO = new PlanoDao();
        //        PlanoController Plano = DAO.PlanoVendaSelectId(TB015_id, Maiores, Menores, isentos);

        //        //ProdutoDAO DAOProsuto = new ProdutoDAO();
        //        //List<ProdutoController> PlanoProdutos = DAOProsuto.ProdutoPlano(TB015_id);
        //        //Plano.PlanoProduto_L = PlanoProdutos;

        //        return Plano;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<PlanoController> ListarPlanosPorTipo(int TipoContrato)
        //{
        //    try
        //    {
        //        PlanoDao DAO = new PlanoDao();
        //        return DAO.ListarPlanosPorTipo(TipoContrato);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<PlanoController> PlanosCorporativo()
        {
            try
            {
                PlanoDao DAO = new PlanoDao();
                return DAO.PlanosCorporativo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Double PlanoCorporativoValor(long TB015_id)
        {
            try
            {
                PlanoDao DAO = new PlanoDao();
                return DAO.PlanoCorporativoValor(TB015_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet RecuperarPlano(long TB015_id)
        {
            try
            {
                PlanoDao DAO = new PlanoDao();
                return DAO.RecuperarPlano(TB015_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlanoController> ListarPlanos()
        {
            try
            {
                return new PlanoDao().ListarPlanos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlanoController PlanoVendaSelectId(long tb015Id, int maiores, int menores, int isentos)
        {
            try
            {
                return new PlanoDao().PlanoVendaSelectId(tb015Id, maiores, menores, isentos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
