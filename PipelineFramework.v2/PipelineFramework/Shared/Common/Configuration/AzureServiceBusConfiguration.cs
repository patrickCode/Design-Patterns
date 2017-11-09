namespace PipelineFramework.Common.Configuration
{
    public class AzureServiceBusConfiguration: BaseConfiguration
    {
        public string Subscription { get; set; }
        public string ResourceGroupName { get; set; }
        public string DataCenterLocation { get; set; }
        public string ConnectionString { get; set; }
        public string AzureManagementUrl { get; set; }

        public string ServiceBusName { get; set; }
        public string Sku { get; set; }
        public AzureServiceBusConfiguration(): base("Azure Service Bus Configuration") { }
    }
}