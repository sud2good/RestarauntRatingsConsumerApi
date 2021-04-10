using Microsoft.Azure.ServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using RestarauntRatingsConsumerApi;

//using Microsoft.ServiceBus.Messaging;

namespace RestarauntRatingsConsumerApi
{
    public interface IServiceBusConsumer
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
    public class ServiceBusConsumer : IServiceBusConsumer
    {
        private QueueClient _queueClient;
        private BlobContainerClient _container;
     
        private string _connectionStringServiceBus;
        private string _connectionStringBlobStorage;
        private string _queueName;
        private string _containerName;
        private SerializationService _serializationService;

        public ServiceBusConsumer(IConfigureServiceBus configure, SerializationService serializationService)
        {
   
            _connectionStringServiceBus = configure.ConnectionStringServiceBus;
            _queueName = configure.QueueName;
           
            _serializationService = serializationService;
            _connectionStringBlobStorage = configure.ConnectionStringBlobStorage;
            _containerName = configure.ContainerName;

            _queueClient = new QueueClient(_connectionStringServiceBus, _queueName);
            _container = new BlobContainerClient(_connectionStringBlobStorage, _containerName);

        }
        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
      
        }
        private async Task ProcessMessagesAsync(Microsoft.Azure.ServiceBus.Message message, CancellationToken token)
        {
         
            var msg = Encoding.UTF8.GetString(message.Body);

            string filePath = Path.GetTempFileName();

            try
            {
                BlobClient blob = _container.GetBlobClient(msg);

                BlobDownloadInfo download = blob.Download();
                using (FileStream file = System.IO.File.Create(filePath))
                {
                    download.Content.CopyTo(file);
                }


                var ratings = _serializationService.Deserialize(filePath);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                await _queueClient.CompleteAsync(message.SystemProperties.LockToken);

            }
        }

      
        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
           
            return Task.CompletedTask;
        }
    }
}
