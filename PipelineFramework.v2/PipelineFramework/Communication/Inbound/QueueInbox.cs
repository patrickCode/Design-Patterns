using PipelineFramework.Common.Configuration;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Inbound
{
    public class QueueInbox: Inbox
    {
        public AzureServiceBusConfiguration Configuration { get; set; }
        public string QueueName { get; set; }

        public QueueInbox(AzureServiceBusConfiguration configuration, string queueName): base("QueueInbox", "QUI")
        {
            Configuration = configuration;
            QueueName = queueName;
        }
    }
}