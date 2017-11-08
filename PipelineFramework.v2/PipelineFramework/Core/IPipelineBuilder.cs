using System.Threading.Tasks;

namespace PipelineFramework.Core
{
    public interface IPipelineBuilder
    {
        Task Use(Middleware middleware);
        Task<BasePipeline> Build();
        Task<BasePipeline> BuildAndRun();
    }
}