using System;
using Newtonsoft.Json;
namespace WebApi
{

    public class Producao
    {
        public int Id { get; set; }
        public int Id_Gerador { get; set; }
        public DateTime Datahora_Captura { get; set; }
        public string? Valor { get; set; }
    }

}