using Microsoft.Azure.EventHubs.Processor;
using PipelineFramework.Infrastructure.Events.Spec;

namespace PipelineFramework.Infrastructure.Events
{
    public class AzureEventProcessorFactory : IEventProcessorFactory
    {
        public EventCallback Callback;
        public AzureEventProcessorFactory(EventCallback callback)
        {
            Callback = callback;
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            var processor = new AzureEventProcessor(Callback);
            return processor;
        }
    }
}