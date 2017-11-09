using System.Threading.Tasks;

namespace PipelineFramework.Common.Spec
{
    public interface IAuthContext
    {
        Task<string> GetTokenUsingSecret(string resourceId);
    }
}