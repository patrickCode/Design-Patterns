using System.Threading.Tasks;

namespace PipelineFramework.Infrastrucure.MessageBus.Spec
{
    public interface IReceiver
    {
        Task StartAsync();
    }
}