using Newtonsoft.Json;

namespace PipelineFramework.Common.Converter
{
    public class JsonConverter : IConverter
    {
        public T Deserialize<T>(string payload)
        {
            return JsonConvert.DeserializeObject<T>(payload);
        }

        public string Serialize(object value)
        {
            if (value.GetType() == typeof(string))
                return value.ToString();

            return JsonConvert.SerializeObject(value);
        }
    }
}