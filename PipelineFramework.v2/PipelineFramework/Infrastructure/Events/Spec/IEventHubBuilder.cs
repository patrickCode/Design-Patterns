using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PipelineFramework.Infrastructure.Events.Spec
{
    public interface IEventHubBuilder
    {
        Task<EventHubClient> CreateEventHubAsync(string eventHubName);
        Task<EventProcessorHost> CreateEventProcessorHost(string eventHubName, string consumerName);
    }
}
