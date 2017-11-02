namespace PipelineFramework.Common.Configuration
{
    public abstract class BaseConfiguration
    {
        public string Name { get; set; }

        public BaseConfiguration() { }
        public BaseConfiguration(string name)
        {
            Name = name;
        }
    }
}