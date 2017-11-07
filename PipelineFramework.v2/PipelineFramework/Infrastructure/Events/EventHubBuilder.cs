using PipelineFramework.Infrastructure.Events.Spec;
using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;
using PipelineFramework.Common.Configuration;
using Microsoft.Azure.EventHubs.Processor;

namespace PipelineFramework.Infrastructure.Events
{
    public class EventHubBuilder : IEventHubBuilder
    {
        private readonly AzureEventHubConfiguration _configuration;
        public EventHubBuilder(AzureEventHubConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<EventHubClient> CreateEventHubAsync(string eventHubName)
        {
            return await Task.Run(() =>
            {
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(_configuration.ConnectionString)
                {
                    EntityPath = eventHubName
                };
                var client = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
                return client;
            });
        }

        public async Task<EventProcessorHost> CreateEventProcessorHost(string eventHubName, string consumerName)
        {
            return await Task.Run(() =>
            {
                var host = new EventProcessorHost(
                eventHubPath: eventHubName,
                consumerGroupName: consumerName,
                eventHubConnectionString: _configuration.ConnectionString,
                storageConnectionString: _configuration.StorageConnectionString,
                leaseContainerName: _configuration.LeaseContainer);
                return host;
            });
        }
    }
}