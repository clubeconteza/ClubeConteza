using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class ContatoNegocios
    {
        public long contatosContratoInsert(ContatoController contato, Int64 TB012_id, Int64 TB011_Id)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();
                long Retorno = DAO.ContatosContratoInsert(contato);

                if(Retorno>0)
                {
                    LogNegocios     Log_N = new LogNegocios();
                    LogController   Log_C = new LogController();
                    Log_C.TB012_Id  = TB012_id;
                    Log_C.TB011_Id  = TB011_Id;
                    Log_C.TB000_IdTabela = 9;
                    Log_C.TB000_Tabela = "Contatos";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0005.ToString().Replace("$Contato", contato.TB009_Contato.Trim()).Replace("$ID", Retorno.ToString());
                    Log_N.LogInsert(Log_C);
                }



                return Retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean contatosContratoUpdate(ContatoController contato)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();
                return DAO.ContatosContratoUpdate(contato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContatoController> contatosDaPessoa(Int64 TB013_id)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();
                return DAO.ContatosDaPessoa(TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool contatosContratoExcluir(ContatoController contato, Int64 TB012_id, Int64 TB011_id)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();

                LogNegocios Log_N = new LogNegocios();
                LogController Log_C = new LogController();

                Log_C.TB012_Id = TB012_id;
                Log_C.TB011_Id = TB011_id;
                Log_C.TB000_IdTabela = 9;
                Log_C.TB000_Tabela = "Contratos";
                Log_C.TB000_Data = DateTime.Now;
                Log_C.TB000_Descricao = MensagensLog.L0006.ToString().Replace("$Contato", contato.TB009_Contato.Trim()).Replace("$ID", contato.TB009_id .ToString());
                Log_N.LogInsert(Log_C);


                return DAO.ContatosContratoExcluir(contato);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<ContatoController> contatosTitularContrato(long TB012_id)
        //{
        //    try
        //    {
        //        ContatoDAO DAO = new ContatoDAO();
        //        return DAO.contatosTitularContrato(TB012_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<ContatoController> contatosUnidade(long TB020_id)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();
                return DAO.ContatosUnidade(TB020_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContatoController> ContatosTitularContratoEmail(long tb012Id)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();
                return DAO.ContatosTitularContratoEmail(tb012Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContatoController> contatoTipoEmailPessoa(long tb013Id)
        {
            try
            {
                ContatoDao DAO = new ContatoDao();
                return DAO.contatoTipoEmailPessoa(tb013Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    

    }
}
