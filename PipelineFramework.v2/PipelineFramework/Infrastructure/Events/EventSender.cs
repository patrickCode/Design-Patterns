using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using PipelineFramework.Infrastructure.Events.Spec;

namespace PipelineFramework.Infrastructure.Events
{
    public class EventSender : IEventSender
    {
        private readonly IEventHubBuilder _builder;
        private readonly string _eventHubName;
        private EventHubClient _client;
        
        public EventSender(IEventHubBuilder builder, string eventHubName)
        {
            _builder = builder;
            _eventHubName = eventHubName;
        }

        public async Task SendAsync(Event @event)
        {
            _client = await _builder.CreateEventHubAsync(_eventHubName);
            var azureEvent = @event.ToAzureEvent();
            if (string.IsNullOrEmpty(@event.PartitionKey) || string.IsNullOrWhiteSpace(@event.PartitionKey))
                await _client.SendAsync(azureEvent);
            else
                await _client.SendAsync(azureEvent, @event.PartitionKey);

            await _client.CloseAsync();
        }
    }
}