using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class CEPController
    {
        public Int64  TB004_id           { get; set; }
        public Int64  TB004_Cep         { get; set; }
        public string TB004_Logradouro  { get; set; }
        public string TB004_Bairro      { get; set; }

        public EstadoController     Estado      { get; set; }

        public MunicipioController  Municipio   { get; set; }

    }
}
