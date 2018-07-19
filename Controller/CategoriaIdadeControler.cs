using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class CategoriaIdadeControler
    {
        public DateTime     DataReferencia  { get; set; }
        public DateTime     DataNascimento  { get; set; }
        public int          idade           { get; set; }
        public int          Maior           { get; set; }
        public int          Menor           { get; set; }
        public int          Isento          { get; set; }

    }
}
