using PipelineFramework.AzureEventMailbox.Spec;
using System;
using System.Collections.Generic;
using System.Text;
using PipelineFramework.Core.Message;
using PipelineFramework.Infrastructure.Events.Spec;
using PipelineFramework.Common.Converter;

namespace PipelineFramework.AzureEventMailbox
{
    public class EventConverter : IEventConverter
    {
        private readonly IConverter _converter;
        public EventConverter()
        {
            _converter = new JsonConverter();
        }

        public Event Convert(BaseMessage baseMessage)
        {
            if (baseMessage is MiddlewareRequest)
                return Convert(baseMessage as MiddlewareRequest);
            if (baseMessage is MiddlewareResponse)
                return Convert(baseMessage as MiddlewareResponse);
            if (baseMessage is MiddlewareException)
                return Convert(baseMessage as MiddlewareException);

            return null;
        }

        public BaseMessage Convert(Event @event)
        {
            var contentType = @event.ContentType;

            if (contentType == typeof(MiddlewareRequest).FullName)
                return _converter.Deserialize<MiddlewareRequest>(@event.Payload);

            if (contentType == typeof(MiddlewareResponse).FullName)
                return _converter.Deserialize<MiddlewareResponse>(@event.Payload);

            if (contentType == typeof(MiddlewareException).FullName)
                return _converter.Deserialize<MiddlewareException>(@event.Payload);

            return null;
        }

        private Event Convert(MiddlewareRequest request)
        {
            var @event = new Event()
            {
                Payload = _converter.Serialize(request),
                CreatedOn = request.CreatedOn,
                Id = request.Id.ToString(),
                PartitionKey = request.MiddlewareId,
                ContentType = typeof(MiddlewareRequest).FullName,
                Properties = new Dictionary<string, object>(request.ExecutionConfiguration)
            };
            return @event;
        }

        private Event Convert(MiddlewareResponse response)
        {
            var @event = new Event()
            {
                Payload = _converter.Serialize(response),
                CreatedOn = response.CreatedOn,
                Id = response.Id.ToString(),
                PartitionKey = response.PipelineId,
                ContentType = typeof(MiddlewareResponse).FullName,
                Properties = new Dictionary<string, object>(response.ExecutionConfiguration)
            };
            return @event;
        }

        private Event Convert(MiddlewareException exception)
        {
            var @event = new Event()
            {
                Payload = _converter.Serialize(exception),
                CreatedOn = exception.CreatedOn,
                Id = exception.Id.ToString(),
                PartitionKey = exception.PipelineId,
                ContentType = typeof(MiddlewareException).FullName,
                Properties = new Dictionary<string, object>(exception.ExecutionConfiguration)
            };
            return @event;
        }
    }
}