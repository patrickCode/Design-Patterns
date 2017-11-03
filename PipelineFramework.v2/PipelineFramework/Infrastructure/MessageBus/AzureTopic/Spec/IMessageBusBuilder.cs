using System;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public interface IMessageBusBuilder
    {
        Task<TopicClient> CreateTopicAsync(string topicName);
        Task<SubscriptionClient> CreateSubscriptionClient(string topicName, string subscriptionName, string correlationFilter, string correlationRuleName);
    }
}