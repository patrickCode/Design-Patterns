using System.Collections.Generic;
using PipelineFramework.Core.Message;
using PipelineFramework.Common.Converter;
using PipelineFramework.AzureTopicMailbox.Spec;
using PipelineFramework.Infrastrucure.MessageBus.Spec;

namespace PipelineFramework.AzureTopicMailbox
{
    public class MessageConverter: IMessageConverter
    {
        private readonly IConverter _converter;
        public MessageConverter()
        {
            _converter = new JsonConverter();
        }

        public Message Convert(BaseMessage baseMessage)
        {
            if (baseMessage is MiddlewareRequest)
                return Convert(baseMessage as MiddlewareRequest);
            if (baseMessage is MiddlewareResponse)
                return Convert(baseMessage as MiddlewareResponse);
            if (baseMessage is MiddlewareException)
                return Convert(baseMessage as MiddlewareException);

            return null;
        }

        public BaseMessage Convert(Message message)
        {
            if (message.ContentType == typeof(MiddlewareRequest).FullName)
                return _converter.Deserialize<MiddlewareRequest>(message.Payload.ToString());

            if (message.ContentType == typeof(MiddlewareResponse).FullName)
                return _converter.Deserialize<MiddlewareResponse>(message.Payload.ToString());

            if (message.ContentType == typeof(MiddlewareException).FullName)
                return _converter.Deserialize<MiddlewareException>(message.Payload.ToString());

            return null;
        }


        #region ServiceBus Message Converters
        private Message Convert(MiddlewareRequest request)
        {
            var message = new Message()
            {
                Id = request.Id.ToString(),
                Payload = _converter.Serialize(request),
                Properties = new Dictionary<string, object>(request.ExecutionConfiguration),
                FilterCorrelation = request.MiddlewareId,
                IsControl = false,
                ContentType = typeof(MiddlewareRequest).FullName
            };

            return message;
        }

        private Message Convert(MiddlewareResponse response)
        {
            var message = new Message()
            {
                Id = response.Id.ToString(),
                Payload = _converter.Serialize(response),
                Properties = new Dictionary<string, object>(response.ExecutionConfiguration),
                FilterCorrelation = response.PipelineId,
                IsControl = false,
                ContentType = typeof(MiddlewareResponse).FullName
            };

            return message;
        }

        private Message Convert(MiddlewareException exception)
        {
            var message = new Message()
            {
                Id = exception.Id.ToString(),
                Payload = _converter.Serialize(exception),
                Properties = new Dictionary<string, object>(exception.ExecutionConfiguration),
                FilterCorrelation = exception.PipelineId,
                IsControl = false,
                ContentType = typeof(MiddlewareException).FullName
            };

            return message;
        }
        #endregion
    }
}