using PipelineFramework.Core;
using PipelineFramework.Common.Configuration;

namespace PipelineFramework.AzureTopicMailbox
{
    public class AzureTopicAddress: MailboxAddress
    {
        public AzureServiceBusConfiguration ServiceBusConfiguration { get; set; }
        public string TopicName { get; set; }
        public string SubscriptionName { get; set; }
        public string CorrelationFilter { get; set; }
        
        public AzureTopicAddress(AzureServiceBusConfiguration configuration, string topicName)
        {
            ServiceBusConfiguration = configuration;
            TopicName = topicName;
        }

        public AzureTopicAddress(AzureServiceBusConfiguration configuration, string topicName, string subscriptionName, string correlationFilter)
            :this(configuration, topicName)
        {   
            SubscriptionName = subscriptionName;
            CorrelationFilter = correlationFilter;
        }
    }
}