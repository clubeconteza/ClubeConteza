
using System;
using System.Collections.Generic;
using Controller;
using DAO;

namespace Negocios
{
    public class AnotacoesNegocios
    {

        public long Anotacaoinsert(AnotacoesController anotacao)
        {
            try
            {
                long retorno = new AnotacoesDao().Anotacaoinsert(anotacao);
                if (retorno <= 0) return retorno;
                var logC = new LogController
                {
                    TB012_Id = retorno,
                    TB011_Id = anotacao.Tb011Id,
                    TB000_IdTabela = 26,
                    TB000_Tabela = "Anotações",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0061, retorno)
                };

                new LogNegocios().LogInsert(logC);
                return retorno;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public List<AnotacoesController> AnotacoesDoContrato(long tb012Id,string TB026_Cod,int TB026_Negociacao)
        {
            try
            {
                return new AnotacoesDao().AnotacoesDoContrato(tb012Id, TB026_Cod, TB026_Negociacao);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public AnotacoesController AnotacaoSelect(long tb026Id)
        {
            try
            {
                return new AnotacoesDao().AnotacaoSelect(tb026Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
    }
}