using Newtonsoft.Json;

namespace Boleto.Controller.ServicesClient
{
    [JsonObject]
    public class EnviaSmsController
    {
        [JsonProperty("sendSmsRequest")]
        public MensagemSmsController MensagemSms { get; set; }
    }
}