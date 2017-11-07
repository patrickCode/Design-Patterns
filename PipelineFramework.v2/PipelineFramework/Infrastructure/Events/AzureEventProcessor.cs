using Microsoft.Azure.EventHubs.Processor;
using PipelineFramework.Infrastructure.Events.Spec;
using System;
using System.Collections.Generic;
using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;

namespace PipelineFramework.Infrastructure.Events
{
    public class AzureEventProcessor: IEventProcessor
    {   
        public EventCallback Callback;

        public AzureEventProcessor(EventCallback callback)
        {
            Callback = callback;
        }

        public Task OpenAsync(PartitionContext context)
        {   
            return Task.CompletedTask;
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            if (reason == CloseReason.Shutdown)
                await context.CheckpointAsync();
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var @event in messages)
            {
                var args = new EventReceivedArgs(@event, context.PartitionId);
                Callback.Receive(args);
            }
            await context.CheckpointAsync();
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            return Task.CompletedTask;
        }
    }
}