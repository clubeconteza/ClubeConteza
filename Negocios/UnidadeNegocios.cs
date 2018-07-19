using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class UnidadeNegocios
    {
        public UnidadeController UnidadeInsert(UnidadeController Unidade)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                UnidadeController Retorno = DAO.UnidadeInsert(Unidade);

                if(Retorno.TB020_id>0)
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = Retorno.TB012_id;
                    Log_C.TB011_Id = Retorno.TB020_CadastradoPor;
                    Log_C.TB000_IdTabela = 20;
                    Log_C.TB000_Tabela = "Unidades";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0020.ToString().Replace("$Unidade", Retorno.TB012_id.ToString());
                    Log_N.LogInsert(Log_C);
                }
                

                return Retorno;
            }
            catch (Exception ex)
            {
              throw ex;
            }
        }

        //public UnidadeController UnidadeAtualizarLogo(UnidadeController Unidade_C)
        //{
        //    try
        //    {
        //        UnidadeDAO DAO = new UnidadeDAO();

        //        UnidadeController Logo= DAO.UnidadeAtualizarLogo(Unidade_C);

        //        WSPortal.Portal WS = new WSPortal.Portal();
        //        WS.GravarLogoUnidade(Unidade_C.TB020_id);

        //        return Logo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public UnidadeController UnidadeMatriz(long TB012_id)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeMatriz(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadeController> UnidadesContrato(Int64 TB012_id)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadesContrato(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadeController UnidadeSelect(long TB020_id)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeSelect(TB020_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadeController UnidadeAtualizar(UnidadeController Unidade_C)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeAtualizar(Unidade_C);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadeController UnidadeAtualizarCorporativo(UnidadeController Unidade_C)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeAtualizarCorporativo(Unidade_C);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadeController UnidadeDescontoSelect(long TB020_id)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeDescontoSelect(TB020_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UnidadeAtualizarDesconto(long TB020_id, byte[] Desconto)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeAtualizarDesconto(TB020_id,Desconto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadeController UnidadeCNPJJaCadastrado(string CNPJ, int TB012_TipoContrato)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeCNPJJaCadastrado(CNPJ,TB012_TipoContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadeController UnidadeCNPJJaCadastradoCorporativo(string CNPJ)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.UnidadeCNPJJaCadastradoCorporativo(CNPJ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Boolean CorporativoVinculaUnidadeContrato(long TB020_id, long TB012_idCorporativo)
        {
            try
            {
                UnidadeDAO DAO = new UnidadeDAO();
                return DAO.CorporativoVinculaUnidadeContrato(TB020_id, TB012_idCorporativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
