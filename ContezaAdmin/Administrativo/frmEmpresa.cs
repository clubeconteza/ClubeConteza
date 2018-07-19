using Controller;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using System.Data;
using System.IO;
using System.Data.SqlTypes;

namespace ContezaAdmin.Administrativo
{
    public partial class frmEmpresa : Form
    {
       EmpresaController Matriz = new EmpresaController();
        private const string ConnStr = "Data Source=NOTFGE\\SQLNOTFGE;Initial Catalog=ClubeConteza_Local;Persist Security Info=True;User ID=sa;Password=sa";
        public frmEmpresa()
        {
            InitializeComponent();
        }

      
        private void btnIncluirLogo_Click(object sender, EventArgs e)
        {

           



            //if (DialogResult.OK == opfLogoMatriz.ShowDialog())
            //{
            //    // Retrieve the Album to add photo(s) to
            //   // EmpresaController Matriz = (Album)treeAlbums.SelectedNode.Tag;

            //    // We allow multiple selections so loop through each one
            //    foreach (string file in opfLogoMatriz.FileNames)
            //    {
            //        // Create a new stream to load this photo into
            //        System.IO.FileStream stream = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //        // Create a buffer to hold the stream bytes
            //        byte[] buffer = new byte[stream.Length];
            //        // Read the bytes from this stream
            //        stream.Read(buffer, 0, (int)stream.Length);
            //        // Now we can close the stream
            //        stream.Close();

            //        Matriz.TB001_Logo = buffer;
            //    }
            //}
        }

        public static void InsertPhoto(int photoId, string desc, string filename)
        {
            const string InsertTSql = @" INSERT INTO TB001_Empresa(PhotoId, Description) VALUES(@PhotoId, @Description); SELECT Photo.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() FROM PhotoAlbum  WHERE PhotoId = @PhotoId";

            string serverPath;
            byte[] serverTxn;

            using (TransactionScope ts = new TransactionScope())
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(InsertTSql, conn))
                    {
                        cmd.Parameters.Add("@PhotoId", SqlDbType.Int).Value = photoId;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = desc;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            serverPath = rdr.GetSqlString(0).Value;
                            serverTxn = rdr.GetSqlBinary(1).Value;
                            rdr.Close();
                        }
                    }
                    SavePhotoFile(filename, serverPath, serverTxn);
                }
                ts.Complete();
            }
        }

        private static void SavePhotoFile(string clientPath, string serverPath, byte[] serverTxn)
        {
            const int BlockSize = 1024 * 512;

            using (FileStream source =
              new FileStream(clientPath, FileMode.Open, FileAccess.Read))
            {
                using (SqlFileStream dest =     new SqlFileStream(serverPath, serverTxn, FileAccess.Write))
                {
                    byte[] buffer = new byte[BlockSize];
                    int bytesRead;
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        dest.Write(buffer, 0, bytesRead);
                        dest.Flush();
                    }
                    dest.Close();
                }
                source.Close();
            }
        }
    }
}
