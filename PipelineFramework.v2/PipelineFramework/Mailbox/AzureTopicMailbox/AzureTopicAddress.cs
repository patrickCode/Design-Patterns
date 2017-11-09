using PipelineFramework.Core;
using PipelineFramework.Common.Configuration;

namespace PipelineFramework.AzureTopicMailbox
{
    public class AzureTopicAddress: MailboxAddress
    {
        public AzureServiceBusConfiguration ServiceBusConfiguration { get; set; }
        public AuthenticationConfiguration AuthenticationConfiguration { get; set; }
        public string TopicName { get; set; }
        public string SubscriptionName { get; set; }
        public string CorrelationFilter { get; set; }

        public AzureTopicAddress(AzureServiceBusConfiguration sbConfiguration, AuthenticationConfiguration authConfigruation, string middlewareName, string middlewareReferenceId)
        {
            ServiceBusConfiguration = sbConfiguration;
            AuthenticationConfiguration = authConfigruation;
            TopicName = middlewareName;
            SubscriptionName = $"{middlewareName}-{middlewareReferenceId}";
            CorrelationFilter = middlewareReferenceId;
        }
    }
}