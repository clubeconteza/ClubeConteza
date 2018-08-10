using Controller;
using DAO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public class ContratosModelDAO
    {
        public ContratosModelController Contratos { get; set; }

        private UnidadeTrabalho unidadeTrabalho;

        public ContratosModelDAO(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho as UnidadeTrabalho;
            Contratos = new ContratosModelController();
        }

        public bool SalvarCorporativo()
        {
            var sql = new StringBuilder();
            sql.Append(" INSERT INTO TB012_Contratos ");
            sql.Append(" (TB012_Inicio, TB012_Fim, TB012_NumeroDaSorte) ");
            sql.Append(" VALUES ");
            sql.Append(" (@Inicio, @Fim, @NumeroDaSorte) ");

            unidadeTrabalho.Conexao.Open();
            unidadeTrabalho.Transacao = unidadeTrabalho.Conexao.BeginTransaction();

            try
            {
                var comando = new SqlCommand(sql.ToString(), (SqlConnection)unidadeTrabalho.Conexao);
                comando.CommandTimeout = 300;
                comando.Parameters.AddWithValue("@Inicio", Contratos.Inicio);
                comando.Parameters.AddWithValue("@Fim", Contratos.Fim);
                comando.Parameters.AddWithValue("@NumeroDaSorte", Contratos.NumeroDaSorte);
                comando.CommandType = CommandType.Text;
                comando.Transaction = (SqlTransaction)unidadeTrabalho.Transacao;

                comando.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                unidadeTrabalho.Transacao.Rollback();
                return false;
            }
            finally
            {
            }
        }
    }
}
