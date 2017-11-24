namespace BridgePattern
{
    public class StandardFormatter : IFormatterBridge
    {
        public string Format(string format, string value)
        {
            return string.Format("{0} {1}", format, value.ToUpper());
        }
    }
}