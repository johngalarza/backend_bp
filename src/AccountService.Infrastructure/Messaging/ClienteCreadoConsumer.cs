using System.Text;
using System.Text.Json;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Contracts.Events;

namespace AccountService.Infrastructure.Messaging;

public class ClienteCreadoConsumer : BackgroundService
{
    private const string ExchangeName = "banking.events";
    private const string QueueName = "account-service.client-created";
    private const string RoutingKey = "cliente.creado";

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabbitMqOptions _options;
    private readonly ILogger<ClienteCreadoConsumer> _logger;

    private IConnection? _connection;
    private IChannel? _channel;

    public ClienteCreadoConsumer(
        IServiceScopeFactory scopeFactory,
        RabbitMqOptions options,
        ILogger<ClienteCreadoConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _options = options;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        _connection = await factory.CreateConnectionAsync(
            stoppingToken);

        _channel = await _connection.CreateChannelAsync(
            cancellationToken: stoppingToken);

        await _channel.ExchangeDeclareAsync(
            ExchangeName,
            ExchangeType.Topic,
            durable: true,
            autoDelete: false,
            cancellationToken: stoppingToken);

        await _channel.QueueDeclareAsync(
            QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        await _channel.QueueBindAsync(
            QueueName,
            ExchangeName,
            RoutingKey,
            cancellationToken: stoppingToken);

        await _channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(
                    eventArgs.Body.ToArray());

                var evento =
                    JsonSerializer.Deserialize<ClienteCreadoEvent>(
                        json);

                if (evento is null)
                {
                    throw new InvalidOperationException(
                        "No se pudo deserializar ClienteCreadoEvent.");
                }

                await ProcesarEventoAsync(
                    evento,
                    stoppingToken);

                await _channel.BasicAckAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    cancellationToken: stoppingToken);

                _logger.LogInformation(
                    "Cliente {ClienteId} sincronizado correctamente.",
                    evento.ClienteId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error procesando ClienteCreadoEvent.");

                await _channel.BasicNackAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    requeue: true,
                    cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        _logger.LogInformation(
            "Consumer de clientes iniciado.");

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken);
    }

    private async Task ProcesarEventoAsync(
        ClienteCreadoEvent evento,
        CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var dbContext =
            scope.ServiceProvider
                .GetRequiredService<AccountDbContext>();

        var cliente =
            await dbContext.ClientesReadModel
                .FirstOrDefaultAsync(
                    c => c.ClienteId == evento.ClienteId,
                    cancellationToken);

        if (cliente is null)
        {
            cliente = new ClienteReadModel(
                evento.Id,
                evento.ClienteId,
                evento.Nombre,
                evento.Identificacion,
                evento.Estado);

            await dbContext.ClientesReadModel.AddAsync(
                cliente,
                cancellationToken);
        }
        else
        {
            cliente.Actualizar(
                evento.Nombre,
                evento.Identificacion,
                evento.Estado);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public override async Task StopAsync(
        CancellationToken cancellationToken)
    {
        if (_channel is not null)
            await _channel.CloseAsync(cancellationToken);

        if (_connection is not null)
            await _connection.CloseAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}