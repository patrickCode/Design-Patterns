namespace BridgePattern
{
    public abstract class Document
    {
        protected IFormatterBridge Formatter;
        public Document(IFormatterBridge formatter)
        {
            Formatter = formatter;
        }

        public abstract void Print();
    }
}