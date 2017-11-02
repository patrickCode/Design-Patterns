using PipelineFramework.Common.Configuration;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Inbound
{
    public class TopicInbox: Inbox
    {
        public AzureServiceBusConfiguration Configuration { get; set; }
        public string TopicName { get; set; }
        public string PartitionFilter { get; set; }
        public string SubscriptionName { get; set; }

        public TopicInbox(AzureServiceBusConfiguration configuration, string topicName, string partitionFilter, string subscriptionName): base("TopicInbox", "TPI")
        {
            Configuration = configuration;
            TopicName = topicName;
            SubscriptionName = subscriptionName;
            PartitionFilter = partitionFilter;
        }
    }
}