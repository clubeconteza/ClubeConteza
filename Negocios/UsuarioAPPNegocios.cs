using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Negocios
{
    public class UsuarioAPPNegocios
    {
        public UsuarioAPPController LoginUsuarioAPPDAO(UsuarioAPPController filtro)
        {
            try
            {
                UsuarioAppdao           ADOUsuario         = new UsuarioAppdao();
                UsuarioAPPController    Usuario            = ADOUsuario.LoginUsuarioAppdao(filtro);

                //Caso seja encontrado um usuário, busca a lista de modulos que o perfil do usuário tenha acesso
                if(Usuario.TB011_Id>0)
                {
                    AcessoDAO ADOModulo = new AcessoDAO();
                    AcessoController filtroModulo = new AcessoController();
                    filtroModulo.TB010_id = Usuario.Perfil.TB010_id;
                    List<AcessoController> LstModulos = ADOModulo.AcessoPerfilModulo(filtroModulo);

                    Usuario.Modulos = LstModulos;

                    //Caso possua acesso a pelo menos um Módulo
                    if (LstModulos.Count > 0)
                    {
                        List<AcessoController> LstPrivilegioPrivilegios = new List<AcessoController>();
                        for (int i = 0; i < LstModulos.Count; i++)
                        {
                            DataSet LstPrivilegioModuo = ADOModulo.AcessoPerfilPrivilegioModulo(Usuario.Perfil.TB010_id, LstModulos[i].TB007_Id);

                            foreach (DataRow theRow in LstPrivilegioModuo.Tables["TB008_Id"].Rows)
                            {
                                
                                AcessoController objPrivilegio = new AcessoController();
                                objPrivilegio.TB008_id = Convert.ToInt64(theRow["TB008_Id"]);
                                LstPrivilegioPrivilegios.Add(objPrivilegio);
                            }
                        }
                        Usuario.Privilegios= LstPrivilegioPrivilegios;
                    }
                }

                return Usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BancoDeDados(string Servidor, string Usuario, string Senha, string Banco)
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();
                return DAO.BancoDeDados(Servidor, Usuario, Senha, Banco);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Conecxao()
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();
                return DAO.Conecxao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RetornoConexao()
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();
                return DAO.RetornoConexao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string VS()
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();
                return DAO.Vs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VerificaPrivilario(long TB010_id, long TB008_id)
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();
                return DAO.VerificaPrivilario(TB010_id, TB008_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int64 VerificaPrivilarioAcaoPontual(long TB008_id, string TB011_CPF, string TB011_Senha)
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();
                return DAO.VerificaPrivilarioAcaoPontual( TB008_id, TB011_CPF, TB011_Senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AlterarMinhaSenha(long TB011_Id, String TB011_Senha, string NomeExibicao)
        {
            try
            {
                UsuarioAppdao DAO = new UsuarioAppdao();

                bool Retorno = DAO.AlterarMinhaSenha(TB011_Id, TB011_Senha);

                if(Retorno==true)
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id          = 0;
                    Log_C.TB011_Id          = TB011_Id;
                    Log_C.TB011_Ref         = TB011_Id;
                    Log_C.TB000_IdTabela    = 11;
                    Log_C.TB000_Tabela      = "APPUsuarios";
                    Log_C.TB000_Data        = DateTime.Now;
                    Log_C.TB000_Descricao   = string.Format(MensagensLog.L0036.ToString(), TB011_Id.ToString(), NomeExibicao);
                    Log_N.LogInsert(Log_C);
                }

                return Retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
