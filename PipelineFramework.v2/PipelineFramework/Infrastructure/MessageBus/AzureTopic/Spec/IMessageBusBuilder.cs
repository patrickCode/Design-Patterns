using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public interface IMessageBusBuilder
    {
        Task<TopicClient> CreateTopicAsync(string topicName);
        Task<SubscriptionClient> CreateSubscriptionAsync(string topicName, string subscriptionName, string correlationFilter, string correlationRuleName);
        Task DeleteTopicAsync(string topicName);
        Task DeleteSubscriptionAsync(string topicName, string subscriptionName);
    }
}