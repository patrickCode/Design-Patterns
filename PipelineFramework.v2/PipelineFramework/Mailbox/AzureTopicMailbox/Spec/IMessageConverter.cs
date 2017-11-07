using PipelineFramework.Core.Message;
using PipelineFramework.Infrastrucure.MessageBus.Spec;

namespace PipelineFramework.AzureTopicMailbox.Spec
{
    public interface IMessageConverter
    {
        Message Convert(BaseMessage baseMessage);
        BaseMessage Convert(Message message);
    }
}