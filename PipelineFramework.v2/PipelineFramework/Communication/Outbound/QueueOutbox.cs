using PipelineFramework.Common.Configuration;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Outbound
{
    public class QueueOutbox: Outbox
    {
        public AzureServiceBusConfiguration Configuration { get; set; }
        public string QueueName { get; set; }

        public QueueOutbox(AzureServiceBusConfiguration configuration, string queueName): base("QueueOutbox", "QO")
        {
            Configuration = configuration;
            QueueName = queueName;
        }
    }
}