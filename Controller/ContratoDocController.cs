using System;


namespace Controller
{
    public class ContratoDocController
    {
        public long     TB029_Id                { get; set; }
        public long     TB012_id                { get; set; }
        public Int16     TB012_VSContrato        { get; set; }
        public byte[]   TB029_DocImpressao      { get; set; }
        public byte[]   TB029_DocScanner        { get; set; }
        public DateTime TB029_DocImpressaoEm    { get; set; }
        public long     TB029_DocImpressaoPor   { get; set; }
        public DateTime TB029_DocScannerEm      { get; set; }
        public long     TB029_DocScannerPor     { get; set; }
        public string   TB029_TipoS             { get; set; }
        public enum TB029_TipoE
        {
            Contrato        = 1,
            Alteração       = 2,
            Comprovante     = 3,
            Cancelamento    = 4,
            Reativação      = 5
        }
    }
}
