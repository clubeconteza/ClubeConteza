using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class AcessoDAO
    {
        /// <summary>
        /// Descrição:  Retorna lista de modulos liberados para o ID do perfil
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<AcessoController> AcessoPerfilModulo(AcessoController filtro)
        {
            List<AcessoController> LstModulos = new List<AcessoController>();
            try
            {

                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT ");
                sSQL.Append("dbo.TB010_Perfil.TB010_id,");
                sSQL.Append("dbo.TB010_Perfil.TB010_Perfil,");
                sSQL.Append("dbo.TB010_TB007.TB007_Id ");
                sSQL.Append(" FROM ");
                sSQL.Append("dbo.TB010_TB007");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append("dbo.TB010_Perfil ON dbo.TB010_TB007.TB010_id = dbo.TB010_Perfil.TB010_id ");
                sSQL.Append(" WHERE ");
                sSQL.Append("dbo.TB010_Perfil.TB010_id = " + filtro.TB010_id);

                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AcessoController objModulo = new AcessoController();
                    objModulo.TB007_Id= Convert.ToInt64(reader["TB007_Id"]);
                    LstModulos.Add(objModulo);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LstModulos;
        }

        /// <summary>
        /// Descrição:  Retorna lista de Privilegios ligados a um modulo
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet AcessoPerfilPrivilegioModulo(Int64 vTb010id, Int64 vTB007Id)
        {
            DataSet dsRetorno = new DataSet();
            try
            {

                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT ");
                sSQL.Append("dbo.TB010_TB008.id,");
                sSQL.Append("dbo.TB010_TB008.TB010_id,");
                sSQL.Append("dbo.TB008_Privilegio.TB007_Id,");
                sSQL.Append("dbo.TB008_Privilegio.TB008_id");
                sSQL.Append(" FROM ");
                sSQL.Append("dbo.TB010_TB008 INNER JOIN ");
                sSQL.Append("dbo.TB008_Privilegio ON dbo.TB010_TB008.TB008_id = dbo.TB008_Privilegio.TB008_id");
                sSQL.Append(" WHERE ");
                sSQL.Append("dbo.TB010_TB008.TB010_id =");
                sSQL.Append( vTb010id );
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB008_Privilegio.TB007_Id =");
                sSQL.Append(vTB007Id); 
                sSQL.Append(" ORDER BY TB008_id ");

                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sSQL.ToString(), con);
                    dsRetorno.Tables.Add("TB008_Id");
                    dsRetorno.EnforceConstraints = false;

                    dsRetorno.Tables["TB008_Id"].BeginLoadData();
                    da.Fill(dsRetorno.Tables["TB008_Id"]);
                    dsRetorno.Tables["TB008_Id"].EndLoadData();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRetorno;
        }
    }
}
