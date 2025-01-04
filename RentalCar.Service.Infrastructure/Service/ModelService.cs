using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.MessageBus;
using RentalCar.Service.Core.Service;

namespace RentalCar.Service.Infrastructure.Service;

public class ModelService : IModelService
{
    private readonly IRabbitMqService _rabbitMqService;

    public ModelService(IRabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }


    public async Task<string> GetService(string idService, string queue, CancellationToken cancellationToken)
    {
        var connection = await _rabbitMqService.CreateConnection(cancellationToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        try
        {
            var response = new ServiceRequest();
            
            await channel.QueueDeclareAsync(queue, true, false, false, null, cancellationToken: cancellationToken);

            //Garantir que seja enviado ao consumidor uma mensagem em cada processamento
            await channel.BasicQosAsync(0, 1, false, cancellationToken);

            //Definição do consumo das mensagens recebidas
            var consumer = new AsyncEventingBasicConsumer(channel);
            
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                //r request = JsonSerializer.Deserialize<ServiceRequest>(message);
                response = JsonSerializer.Deserialize<ServiceRequest>(message);
            };

            await channel.BasicConsumeAsync(queue: queue, autoAck: true, consumer: consumer, cancellationToken: cancellationToken);

            //Aguardar o processamento da mensagem
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            
            return string.Equals(response.Id, idService) ? response.Name : String.Empty;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            await _rabbitMqService.CloseConnection(connection, channel, cancellationToken);
        }
    }
    
}