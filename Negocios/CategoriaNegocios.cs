
using Controller;
using DAO;
using System;
using System.Collections.Generic;


namespace Negocios
{
    public class CategoriaNegocios
    {
        public List<CategoriaController> RetoranarLista()
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> ListarSessao()
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.ListarSessao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> ListarSessaoNivel1(long TB021_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.ListarSessaoNivel1(TB021_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long VincularSessaoNivel1(long TB021_id, long TB024_Id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();

                if(DAO.SelectSessaoNivel1(TB021_id, TB024_Id)==0)
                {
                    return DAO.VincularSessaoNivel1(TB021_id, TB024_Id);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Boolean DesvincularSessaoNivel1(long TB021_id, long TB024_Id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.DesvincularSessaoNivel1(TB021_id, TB024_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CategoriaController> RetoranarTodasAsCategorias()
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarTodasAsCategorias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoriaController RetoranarCategoriaNivel1(long TB021_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarCategoriaNivel1(TB021_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long IncluirNivel1(CategoriaController Nivel_C)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();

                Int64 Id = DAO.IncluirNivel1(Nivel_C);
           

                return Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean AtualizarNivel1(CategoriaController Nivel_C)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.AtualizarNivel1(Nivel_C);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long IncluirNivel2(CategoriaController Nivel_C)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();

                Int64 Id = DAO.IncluirNivel2(Nivel_C);


                return Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoriaController RetornoItemNivel2(long TB022_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetornoItemNivel2(TB022_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean AtualizarNivel2(CategoriaController Nivel_C)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.AtualizarNivel2(Nivel_C);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long IncluirNivel3(CategoriaController Nivel_C)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();

                Int64 Id = DAO.IncluirNivel3(Nivel_C);


                return Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoriaController RetornoItemNivel3(long TB023_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetornoItemNivel3(TB023_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean AtualizarNivel3(CategoriaController Nivel_C)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.AtualizarNivel3(Nivel_C);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> RetoranarcCategoriaNivel2(Int64 TB021_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarcCategoriaNivel2(TB021_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> RetoranarcCategoriaNivel1DoContrato(Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarcCategoriaNivel1DoContrato(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> RetoranarcCategoriaNivel2DoContrato(Int64 TB021_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarcCategoriaNivel2DoContrato(TB021_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long IncluirNivel1Contrato(Int64 TB021_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.IncluirNivel1Contrato(TB021_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ExcluirNivel1Contrato(Int64 TB021_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.ExcluirNivel1Contrato(TB021_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long IncluirNivel2Contrato(Int64 TB022_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.IncluirNivel2Contrato(TB022_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ExcluirNivel2Contrato(Int64 TB022_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.ExcluirNivel2Contrato(TB022_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> RetoranarcCategoriaNivel3(Int64 TB022_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarcCategoriaNivel3(TB022_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaController> RetoranarcCategoriaNivel3DoContrato(Int64 TB022_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.RetoranarcCategoriaNivel3DoContrato(TB022_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long IncluirNivel3Contrato(Int64 TB023_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.IncluirNivel3Contrato(TB023_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ExcluirNivel3Contrato(Int64 TB023_id, Int64 TB012_id)
        {
            try
            {
                CategoriaDao DAO = new CategoriaDao();
                return DAO.ExcluirNivel3Contrato(TB023_id, TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}