using Newtonsoft.Json;

namespace Boleto.Controller.ServicesClient
{
    [JsonObject]
    public class EnviaSmsMultiController
    {
        [JsonProperty("sendSmsMultiRequest")]
        public EnviaSmsListaController EnviaSmsLista { get; set; }
    }
}