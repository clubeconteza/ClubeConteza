
namespace Controller
{
    public class CategoriaController
    {

        public long     TB021_id        { get; set; }
        public string   TB021_Descricao { get; set; }

        public long     TB022_id        { get; set; }
        public string   TB022_Descricao { get; set; }
        //public int      TB022_Check     { get; set; }

        public long     TB023_id        { get; set; }
        public string   TB023_Descricao { get; set; }

        public long     TB024_Id        { get; set; }
        public string   TB024_Sessao    { get; set; }

        public string   TB024_StatusS   { get; set; }

        public enum TB024_StatusE
        {
            Ativo = 1,
            Inativo = 2
        }


    }
}