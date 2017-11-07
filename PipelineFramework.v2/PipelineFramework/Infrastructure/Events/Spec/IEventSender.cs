using System.Threading.Tasks;

namespace PipelineFramework.Infrastructure.Events.Spec
{
    public interface IEventSender
    {
        Task SendAsync(Event @event);
    }
}