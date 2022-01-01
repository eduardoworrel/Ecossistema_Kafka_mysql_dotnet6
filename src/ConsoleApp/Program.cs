using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;
using Infra;



string bootstrapServers = "localhost:9092";
string nomeTopic = "topic1";
string groupId = "group1";

Console.Write($"BootstrapServers = {bootstrapServers}");
Console.Write($"Topic = {nomeTopic}");
Console.Write($"Group Id = {groupId}");

var config = new ConsumerConfig
{
    BootstrapServers = bootstrapServers,
    GroupId = groupId,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    using (var consumer = new ConsumerBuilder<Ignore, string>(config)
    .Build())
    {
        consumer.Subscribe(nomeTopic);
        try
        {
            while (true)
            {
                var cr = consumer.Consume(cts.Token);
                Save(Newtonsoft.Json.JsonConvert.DeserializeObject<Producao>(cr.Message.Value));
                Console.Write("Registro salvo com sucesso!")
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
            Console.Write("Cancelada a execução do Consumer...");
        }
    }
}
catch (Exception ex)
{
    Console.Write($"Exceção: {ex.GetType().FullName} | " +
                $"Mensagem: {ex.Message}");
}
        

static void Save(Producao producao)
{
    using (var db = new MyDbContext())
    {
        db.Producoes.Add(producao);
        db.SaveChanges();
    }
}
 