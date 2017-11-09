using Microsoft.Rest;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using PipelineFramework.Common.Spec;
using Microsoft.Azure.Management.ServiceBus;
using PipelineFramework.Common.Configuration;
using Microsoft.Azure.Management.ServiceBus.Models;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public class MessageBusBuilder : IMessageBusBuilder
    {
        private readonly AzureServiceBusConfiguration _configuration;
        private readonly IAuthContext _authContext;
        
        public MessageBusBuilder(AzureServiceBusConfiguration configuration, IAuthContext authContext)
        {
            _configuration = configuration;
            _authContext = authContext;
            CreateServiceBus().Wait();
        }

        public async Task<TopicClient> CreateTopicAsync(string topicName)
        {
            var serviceBusManagementClient = await CreateServiceBusManagementClientAsync();
            var parameters = new SBTopic();
            await serviceBusManagementClient.Topics.CreateOrUpdateAsync(_configuration.ResourceGroupName, _configuration.ServiceBusName, topicName, parameters);
            return new TopicClient(connectionString: _configuration.ConnectionString, entityPath: topicName);
        }

        public async Task<SubscriptionClient> CreateSubscriptionAsync(string topicName, string subscriptionName, string correlationFilter, string correlationRuleName)
        {
            var serviceBusManagementClient = await CreateServiceBusManagementClientAsync();
            var parameters = new SBSubscription();

            await serviceBusManagementClient.Subscriptions.CreateOrUpdateAsync(
                resourceGroupName: _configuration.ResourceGroupName, 
                namespaceName: _configuration.ServiceBusName, 
                topicName: topicName, 
                subscriptionName: subscriptionName, 
                parameters: parameters);

            var rule = new Rule()
            {
                CorrelationFilter = new Microsoft.Azure.Management.ServiceBus.Models.CorrelationFilter(correlationFilter)
            };

            await serviceBusManagementClient.Rules.CreateOrUpdateAsync(
                resourceGroupName: _configuration.ResourceGroupName, 
                namespaceName: _configuration.ServiceBusName, 
                topicName: topicName, 
                subscriptionName: subscriptionName, 
                ruleName: correlationRuleName, 
                parameters: rule);

            var client = new SubscriptionClient(connectionString: _configuration.ConnectionString,
                topicPath: topicName,
                subscriptionName: subscriptionName);
            return client;
        }

        public async Task DeleteSubscriptionAsync(string topicName, string subscriptionName)
        {
            var serviceBusManagementClient = await CreateServiceBusManagementClientAsync();
            await serviceBusManagementClient.Subscriptions.DeleteAsync(
                resourceGroupName: _configuration.ResourceGroupName, 
                namespaceName: _configuration.ServiceBusName, 
                topicName: topicName, 
                subscriptionName: subscriptionName);
        }

        public async Task DeleteTopicAsync(string topicName)
        {
            var serviceBusManagementClient = await CreateServiceBusManagementClientAsync();
            await serviceBusManagementClient.Topics.DeleteAsync(
                resourceGroupName: _configuration.ResourceGroupName,
                namespaceName: _configuration.ServiceBusName,
                topicName: topicName);
        }

        private async Task CreateServiceBus()
        {
            var serviceBusManagementClient = await CreateServiceBusManagementClientAsync();
            var parameter = new SBNamespace()
            {
                Location = _configuration.DataCenterLocation,
                Sku = new SBSku()
                {
                    Tier = GetSkuTier(_configuration.Sku),
                    Name = GetSkuName(_configuration.Sku)
                }
            };

            await serviceBusManagementClient.Namespaces.CreateOrUpdateAsync(_configuration.ResourceGroupName, _configuration.ServiceBusName, parameter);
        }

        private async Task<ServiceBusManagementClient> CreateServiceBusManagementClientAsync()
        {
            var token = await _authContext.GetTokenUsingSecret(_configuration.AzureManagementUrl);
            var credentials = new TokenCredentials(token);

            var serviceBusManagementClient = new ServiceBusManagementClient(credentials)
            {
                SubscriptionId = _configuration.Subscription
            };
            return serviceBusManagementClient;
        }

        private SkuTier GetSkuTier(string sku)
        {
            switch(sku)
            {
                case "Basic": return SkuTier.Basic;
                case "Standard": return SkuTier.Standard;
                case "Premier": return SkuTier.Premium;
            }
            return SkuTier.Basic;
        }

        private SkuName GetSkuName(string sku)
        {
            switch (sku)
            {
                case "Basic": return SkuName.Basic;
                case "Standard": return SkuName.Standard;
                case "Premier": return SkuName.Premium;
            }
            return SkuName.Basic;
        }
    } 
}