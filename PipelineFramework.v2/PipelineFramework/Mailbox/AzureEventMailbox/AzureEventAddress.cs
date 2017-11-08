using PipelineFramework.Core;
using PipelineFramework.Common.Configuration;

namespace PipelineFramework.AzureEventMailbox
{
    public class AzureEventAddress : MailboxAddress
    {
        public readonly AzureEventHubConfiguration Configuration;
        public string EventHubName { get; set; }
        public string ConsumerGroupName { get; set; }

        public AzureEventAddress(AzureEventHubConfiguration configuration, string eventHubName, string consumerGroupName)
        {
            Configuration = configuration;
            EventHubName = eventHubName;
            ConsumerGroupName = consumerGroupName;
        }
    }
}