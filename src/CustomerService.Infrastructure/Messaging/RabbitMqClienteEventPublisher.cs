using System.Text;
using System.Text.Json;
using CustomerService.Application.Interfaces;
using RabbitMQ.Client;
using Shared.Contracts.Events;

namespace CustomerService.Infrastructure.Messaging;

public class RabbitMqClienteEventPublisher : IClienteEventPublisher
{
    private const string ExchangeName = "banking.events";
    private const string RoutingKey = "cliente.creado";

    private readonly RabbitMqOptions _options;

    public RabbitMqClienteEventPublisher(RabbitMqOptions options)
    {
        _options = options;
    }

    public async Task PublishClienteCreadoAsync(
        ClienteCreadoEvent evento)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        await using var connection =
            await factory.CreateConnectionAsync();

        await using var channel =
            await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            ExchangeName,
            ExchangeType.Topic,
            durable: true,
            autoDelete: false
        );

        var json = JsonSerializer.Serialize(evento);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        await channel.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: RoutingKey,
            mandatory: false,
            basicProperties: properties,
            body: body
        );
    }
}