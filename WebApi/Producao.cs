using System;
using Newtonsoft.Json;
namespace WebApi
{

    public class Producao
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonRequired]
        [JsonProperty("Id_Gerador")]
        public int Id_Gerador { get; set; }

        [JsonRequired]
        [JsonProperty("Datahora_Captura")]
        public DateTime Datahora_Captura { get; set; }

        [JsonRequired]
        [JsonProperty("Valor")]
        public string? Valor { get; set; }
    }

}