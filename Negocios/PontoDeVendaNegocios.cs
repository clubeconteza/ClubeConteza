using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;


namespace Negocios
{
    public class PontoDeVendaNegocios
    {
        public DataSet PontosDeVendaLiberadosParaUsuario(UsuarioAPPController filtro)
        {
            try
            {

                return new PontoDeVendaDao().PontosDeVendaLiberadosParaUsuario(filtro);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public PontoDeVendaController PontoDeVendaEmpresa(long tb002Id)
        {
            try
            {

                return new PontoDeVendaDao().PontoDeVendaEmpresa(tb002Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        //public List<PontoDeVendaController> PontoDeVendaLista()
        //{
        //    try
        //    {
        //        return new PontoDeVendaDao().PontoDeVendaLista();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}

        public PontoDeVendaController PontoDeVenda(long tb002Id)
        {
            try
            {
                return new PontoDeVendaDao().PontoDeVenda(tb002Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public bool PontoDeVendaAtualizar(PontoDeVendaController ponto)
        {
            try
            {
                if (!new PontoDeVendaDao().PontoDeVendaAtualizar(ponto)) return false;
                var logC = new LogController
                {
                    TB012_Id = 0,
                    TB011_Id = ponto.Tb002AlteradoPorI,
                    TB000_IdTabela = 26,
                    TB000_Tabela = "Anotações",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0068, ponto.TB002_id)
                };
                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        //public List<UsuarioAPPController> PontoDeVendaUsuariosComAcesso(long tb002Id)
        //{
        //    try
        //    {
        //        return new PontoDeVendaDao().PontoDeVendaUsuariosComAcesso(tb002Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}

        public bool PontoDeVendaDeletarUsuariosComAcesso(long tb002Id, long tb011Id, long resp)
        {
            try
            {
                if (!new PontoDeVendaDao().PontoDeVendaDeletarUsuariosComAcesso(tb002Id, tb011Id)) return true;
                var logC = new LogController
                {
                    TB012_Id = 0,
                    TB011_Id = resp,
                    TB000_IdTabela = 11,
                    TB000_Tabela = "TB011_TB002",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0069, tb011Id, tb002Id)
                };
                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
    }
}
