using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "MyCompany", durable: true, exclusive: false, autoDelete: false,
    arguments: new Dictionary<string, object?> { { "x-queue-type", "quorum" } });

const string message = "Hello World! My Componay";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "MyCompany", body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();