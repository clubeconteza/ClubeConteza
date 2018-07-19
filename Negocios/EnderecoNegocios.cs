using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;


namespace Negocios
{
    public class EnderecoNegocios
    {
        public DataSet PaisController()
        {
            try
            {  
                return new EnderecoDao().PaisController();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public DataSet EstadosController(PaisController filtro)
        {
            try
            {
                return new EnderecoDao().EstadosController(filtro);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public DataSet MunicipioController(EstadoController filtro)
        {
            try
            {
                return new EnderecoDao().MunicipioController(filtro);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public DataSet Cep(CEPController filtro)
        {
            try
            {
                return new EnderecoDao().Cep(filtro);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public long DddMunicipio(long tb006Id)
        {
            try
            {

                return new EnderecoDao().DddMunicipio(tb006Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public MunicipioController Municipio(long tb006Id)
        {
            try
            {
                return new EnderecoDao().Municipio(tb006Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public EstadoController Estado(long tb005Id)
        {
            try
            {
                return new EnderecoDao().Estado(tb005Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public List<MunicipioController> MunicipiosAtivos()
        {
            try
            {
                return new EnderecoDao().MunicipiosAtivos();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public List<EstadoController> estadoAtivos()
        {
            try
            {
                return new EnderecoDao().estadoAtivos();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public List<MunicipioController> municipiosAtivosPorEstado(long TB005_Id)
        {
            try
            {
                return new EnderecoDao().municipiosAtivosPorEstado(TB005_Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }


    }
}
