using System;
using System.Threading.Tasks;
using PipelineFramework.Infrastrucure.MessageBus.AzureTopic.Spec;

namespace PipelineFramework.Infrastrucure.MessageBus.Spec
{
    public interface IReceiver
    {
        event EventHandler<MessageReceivedArgs> MessageReceived;
        Task StartAsync();
    }
}