using Newtonsoft.Json;
using System.Collections.Generic;

namespace Boleto.Controller.ServicesClient
{
    [JsonObject]
    public class EnviaSmsListaController
    {
        [JsonProperty("aggregateId")]
        public long? CodigoAgregador { get { return 28723; } }

        [JsonProperty("sendSmsRequestList")]
        public List<MensagemVariosSmsController> MensagemVariosSms { get; set; }
    }
}