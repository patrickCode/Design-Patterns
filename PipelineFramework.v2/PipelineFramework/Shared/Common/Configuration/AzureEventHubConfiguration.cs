namespace PipelineFramework.Common.Configuration
{
    public class AzureEventHubConfiguration: BaseConfiguration
    {
        public AzureEventHubConfiguration(): base("Azure Event Hub Configuration") { }

        public string ConnectionString { get; set; }
        public string StorageConnectionString { get; set; }
        public string LeaseContainer { get; set; }
    }
}