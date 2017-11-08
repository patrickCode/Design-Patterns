using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using PipelineFramework.Common.Configuration;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public class MessageBusBuilder : IMessageBusBuilder
    {
        private readonly AzureServiceBusConfiguration _configuration;
        
        public MessageBusBuilder(AzureServiceBusConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SubscriptionClient> CreateSubscriptionClient(string topicName, string subscriptionName, string correlationFilter, string correlationRuleName)
        {
            var client = new SubscriptionClient(connectionString: _configuration.ConnectionString,
                topicPath: topicName,
                subscriptionName: subscriptionName);
            await client.AddRuleAsync(correlationRuleName, new CorrelationFilter(correlationFilter));
            return client;
        }

        public async Task<TopicClient> CreateTopicAsync(string topicName)
        {
            return await Task.Run(() =>
            {
                return new TopicClient(connectionString: _configuration.ConnectionString, entityPath: topicName);
            });
        }
    }
}