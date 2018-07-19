using PortalClubeConteza.Entities;
using System;
using System.Linq;

namespace PortalClubeConteza.DAO
{
    public class PortalUsuarioDAO
    {
        private EntidadesContext contexto;

        public PortalUsuarioDAO(EntidadesContext contexto)
        {
            this.contexto = contexto;
        }

        public PortalUsuario BuscaUsuarioPorLogin(string cpfCnpj, string senha)
        {
            try
            {
                var cript = new CriptografiaDAO();
                var senhaPrivada = cript.Encrypt(senha.Trim());
                var busca = from u in contexto.PortalUsuarios
                            join p in contexto.Pessoas on u.IdPessoa equals p.Id
                            where p.CpfCnpj == cpfCnpj.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim()
                               && u.Senha == senhaPrivada
                            select u;

                var usuario = busca.FirstOrDefault();
                return usuario ?? new PortalUsuario();
            }
            catch
            {
                return new PortalUsuario();
            }
        }

        public PortalUsuario BuscaUsuarioPorChaveCpfCnpj(string chave)
        {
            try
            {
                var cript = new CriptografiaDAO();
                var valida = cript.ValidarChave(chave);
                if (valida != "Erro")
                {
                    var busca = from u in contexto.PortalUsuarios
                                join p in contexto.Pessoas on u.IdPessoa equals p.Id
                                where p.CpfCnpj == valida
                                select u;

                    var usuario = busca.FirstOrDefault();
                    return usuario ?? new PortalUsuario();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new PortalUsuario();
        }

        public PortalUsuario BuscaUsuarioPorIdPessoa(long idPessoa)
        {
            try
            {
                var busca = from u in contexto.PortalUsuarios
                            where u.IdPessoa == idPessoa
                            select u;

                var usuario = busca.FirstOrDefault();
                return usuario ?? new PortalUsuario();
            }
            catch
            {
                return new PortalUsuario();
            }
        }

        public bool AlterarChaveTemporaria(long idPessoa, string cpfCnpf)
        {
            try
            {
                var dataAcesso = DateTime.Now;
                var cript = new CriptografiaDAO();

                var usuario = contexto.PortalUsuarios.FirstOrDefault(u => u.IdPessoa == idPessoa);
                usuario.ChaveTemporaria = cript.EncryptInterna(cpfCnpf.Trim() + ";" + dataAcesso.ToString("dd/MM/yyyy hh:mm"));
                usuario.UltimoAcesso = dataAcesso;
                contexto.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarSenha(long idPessoa, string novaSenha)
        {
            var resultado = false;

            try
            {
                var cript = new CriptografiaDAO();

                var usuario = contexto.PortalUsuarios.FirstOrDefault(u => u.IdPessoa == idPessoa);
                usuario.Senha = cript.Encrypt(novaSenha.TrimEnd());
                usuario.AlteradoEm = DateTime.Now;
                usuario.AlteradoPor = 1;
                contexto.SaveChanges();

                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        public bool IncluirUsuario(Pessoa pessoa, string novaSenha)
        {
            var resultado = false;

            try
            {
                var cript = new CriptografiaDAO();

                var usuario = new PortalUsuario()
                {
                    IdPessoa = pessoa.Id,
                    Senha = cript.Encrypt(novaSenha.TrimEnd()),
                    Status = 1,
                    CadastradoEm = DateTime.Now,
                    CadastradoPor = 1
                };

                contexto.PortalUsuarios.Add(usuario);
                contexto.SaveChanges();

                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}