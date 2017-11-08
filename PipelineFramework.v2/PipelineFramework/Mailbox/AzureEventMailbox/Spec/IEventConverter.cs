using PipelineFramework.Core.Message;
using PipelineFramework.Infrastructure.Events.Spec;

namespace PipelineFramework.AzureEventMailbox.Spec
{
    public interface IEventConverter
    {
        Event Convert(BaseMessage baseMessage);
        BaseMessage Convert(Event @event);
    }
}