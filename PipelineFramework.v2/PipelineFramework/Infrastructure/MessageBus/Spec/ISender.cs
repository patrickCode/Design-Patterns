using System.Threading.Tasks;

namespace PipelineFramework.Infrastrucure.MessageBus.Spec
{
    public interface ISender
    {
        Task SendAsync(Message message);
    }
}