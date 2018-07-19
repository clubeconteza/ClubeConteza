using Newtonsoft.Json;
using System;

namespace Boleto.Controller.ServicesClient
{
    [JsonObject]
    public class MensagemVariosSmsController
    {
        [JsonProperty("from")]
        public string Remetente { get; set; }

        [JsonRequired]
        [JsonProperty("to")]
        public long NumeroCelular { get; set; }

        [JsonProperty("schedule")]
        public DateTime? DataMensagemEnvia { get; set; }

        [JsonRequired]
        [JsonProperty("msg")]
        public string Mensagem { get; set; }

        [JsonProperty("id")]
        public long? IdMensagem { get; set; }

        [JsonProperty("flashSms")]
        public bool FlashSms { get { return false; } }
    }
}