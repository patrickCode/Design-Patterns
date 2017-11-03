namespace PipelineFramework.Common.Configuration
{
    public class AzureServiceBusConfiguration: BaseConfiguration
    {
        public string ConnectionString { get; set; }
        public AzureServiceBusConfiguration(): base("Azure Service Bus Configuration") { }
    }
}