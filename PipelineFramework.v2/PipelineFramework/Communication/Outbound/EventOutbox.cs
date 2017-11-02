using PipelineFramework.Common.Configuration;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Outbound
{
    public class EventOutbox: Outbox
    {
        public AzureEventHubConfiguration Configuration { get; set; }

        public EventOutbox(AzureEventHubConfiguration configuration): base("EventOutbox", "EVTO")
        {
            Configuration = configuration;
        }
    }
}