using Boleto.Controller;
using Boleto.DAO;
using System;

namespace Boleto.Negocios
{
    public class AcessoUsuarioNegocios
    {
        public string Cnpj { get; set; }

        public AcessoUsuarioController AcessoUsuarioPlano(string chaveUsuario, string cnpjParceiro, string senhaParceiro)
        {
            try
            {
                var cpfUsuario = RecuperarCpfUsuario(chaveUsuario);

                var ehParceiro = new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro);
                if (!ehParceiro)
                {
                    throw new Exception("Parceiro não encontrado.");
                }

                var usuario = new PortalUsuarioDAO().ConsultaUsuario(cpfUsuario);
                if (usuario.TB013_id <= 0)
                {
                    throw new Exception("Usuário do portal não encontrado.");
                }

                var planos = new ContratosDAO().ConsultaPlanoCorporativoUsuario(usuario.TB013_id);
                return new AcessoUsuarioController
                {
                    CpfCnpjUsuario = usuario.TB013_CPFCNPJ,
                    NomeUsuario = usuario.TB013_NomeCompleto,
                    CnpjPlanos = planos
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string RecuperarCpfUsuario(string chaveUsuario)
        {
            try
            {
                var parametros = new CriptografiaDAO().Decrypt(chaveUsuario).Split(';');
                return parametros[0].ToString().Trim();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}