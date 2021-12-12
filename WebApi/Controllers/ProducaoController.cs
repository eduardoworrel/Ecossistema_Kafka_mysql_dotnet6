using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducaoController : ControllerBase
{
    public ProducaoController() { }

    [HttpPost]
    public async Task SaveRecord([FromBody] Producao value)
    {

        string bootstrapServers = "localhost:9092";
        string nomeTopic = "topic1";

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };
        
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            var result = await producer.ProduceAsync(
                nomeTopic,
                new() { Value = Newtonsoft.Json.JsonConvert.SerializeObject(value) });
        }
    }

}

