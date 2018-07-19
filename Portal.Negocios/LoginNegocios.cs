using System;
using System.Dynamic;

namespace Portal.Negocios
{
    public class LoginNegocios
    {
        public dynamic AcessoUsuario(string acesso, string cnpj, string senha)
        {
            try
            {
                var ws = new WSLogin.Login();
                var obj = ws.AcessoUsuario(acesso, cnpj, senha);

                dynamic usuario = new ExpandoObject();
                usuario.CpfCnpjUsuario = new Func<string>(() => { return obj.CpfCnpjUsuario; });
                usuario.NomeUsuario = new Func<string>(() => { return obj.NomeUsuario; });
                usuario.CnpjPlanos = new Func<string>(() => { return string.Join(";", obj.CnpjPlanos); });

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
