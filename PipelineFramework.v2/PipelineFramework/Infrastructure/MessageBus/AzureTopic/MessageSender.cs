using Microsoft.Azure.ServiceBus;
using PipelineFramework.Common.Converter;
using PipelineFramework.Infrastrucure.MessageBus.Spec;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB = Microsoft.Azure.ServiceBus;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public class MessageSender : ISender
    {
        private readonly IMessageBusBuilder _builder;
        private readonly string _topicName;
        private readonly TopicClient _client;
        private readonly IConverter _converter;

        public MessageSender(IMessageBusBuilder builder, IConverter converter, string topicName)
        {
            _builder = builder;
            _topicName = topicName;
            _converter = converter;
            _client = _builder.CreateTopicAsync(_topicName).Result;
        }

        public async Task SendAsync(MessageBus.Spec.Message message)
        {
            var sbMessage = ToServiceBusMessage(message);
            await _client.SendAsync(sbMessage);
        }

        private SB.Message ToServiceBusMessage(MessageBus.Spec.Message message)
        {
            var serializedMessage = _converter.Serialize(message.Payload);
            var messageBody = Encoding.UTF8.GetBytes(serializedMessage);
            var sbMsg = new SB.Message(messageBody)
            {
                MessageId = message.Id,
                ContentType = message.IsControl ? "control" : message.ContentType
            };
            if (!string.IsNullOrEmpty(message.FilterCorrelation))
                sbMsg.CorrelationId = message.FilterCorrelation;

            if (message.Properties != null && message.Properties.Any())
            {
                foreach (var property in message.Properties)
                {
                    sbMsg.UserProperties.Add(property);
                }
            }

            return sbMsg;
        }
    }
}