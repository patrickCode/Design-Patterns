using System.Threading.Tasks;
using Microsoft.Azure.EventHubs.Processor;
using PipelineFramework.Infrastructure.Events.Spec;
using System;

namespace PipelineFramework.Infrastructure.Events
{
    public class EventReceiver : IEventReceiver
    {
        private EventCallback _callback;
        public EventCallback Callback => _callback;
        public event EventHandler<EventReceivedArgs> EventReceived = (sender, args) => { };

        private readonly IEventHubBuilder _builder;
        private readonly EventProcessorHost _eventProcessorHost;
        private readonly string _eventHubName;
        private readonly string _consumerName;

        private IEventProcessorFactory _eventProcessorFactory;

        public EventReceiver(IEventHubBuilder builder, string eventHubName, string consumerName)
        {   
            _builder = builder;
            _eventHubName = eventHubName;
            _consumerName = consumerName;
            _callback = new EventCallback(EventReceived);
            _eventProcessorHost = _builder.CreateEventProcessorHost(_eventHubName, _consumerName).Result;
            _eventProcessorFactory = new AzureEventProcessorFactory(Callback);
        }


        public async Task StartAsync()
        {
            await _eventProcessorHost.RegisterEventProcessorFactoryAsync(_eventProcessorFactory);
        }
    }
}