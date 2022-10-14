using DevFreela.Infrastructure.CrossCutting.Services.MessageBus.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.CrossCutting.Services.MessageBus.Implementations
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;
        public MessageBusService(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        public void Publish(string queue, byte[] message)
        {
            //Inicia a conexão
            using (var connection = _factory.CreateConnection())
            {
                //Criar um canal de comunicação
                using (var channel = connection.CreateModel())
                {
                    //Garantir que a fila seja criada
                    channel.QueueDeclare(queue: queue, 
                                         durable: false, 
                                         exclusive: false, 
                                         autoDelete: false, 
                                         arguments: null);

                    //Publicar a mensagem
                    channel.BasicPublish(exchange: "", 
                                         routingKey: queue, 
                                         basicProperties: null, 
                                         body: message);

                }
            }
        }
    }
}
