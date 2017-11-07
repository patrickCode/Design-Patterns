namespace PipelineFramework.Common.Converter
{
    public interface IConverter
    {
        string Serialize(object data);
        T Deserialize<T>(string payload);
    }
}