using System.Threading.Tasks;

namespace PipelineFramework.Infrastructure.Events.Spec
{
    public interface IEventReceiver
    {
        Task StartAsync();
        EventCallback Callback { get; }
    }
}