using System;

namespace Controller
{
    public class AcessoController
    {
        /*Modulo*/
        public Int64    TB007_Id            { get; set; }
        public string   TB007_Modulo        { get; set; }
        public int      TB007_Descktop      { get; set; }
        public int      TB007_Portal        { get; set; }
        /*Privilégio*/
        public Int64    TB008_id            { get; set; }
        public string   TB008_Privilegio    { get; set; }
        /*Perfil*/
        public Int64    TB010_id            { get; set; }
        public string   TB010_Perfil        { get; set; }
    }
}
