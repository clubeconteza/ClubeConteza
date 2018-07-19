using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO.Infrastructure
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        public IDbConnection Conexao { get; set; }

        public IDbTransaction Transacao { get; set; }

        public UnidadeTrabalho(IDbConnection conexao)
        {
            Conexao = conexao;
        }

        public void Commit()
        {

        }
    }
}
