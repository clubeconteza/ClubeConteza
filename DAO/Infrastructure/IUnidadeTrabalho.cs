using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO.Infrastructure
{
    public interface IUnidadeTrabalho
    {
        IDbConnection Conexao { get; set; }
        IDbTransaction Transacao { get; set; }
        void Commit();
    }
}
