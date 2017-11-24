using System;

namespace BridgePattern
{
    public class TermPaper : Document
    {
        public TermPaper(IFormatterBridge formatter) : base(formatter)
        {
        }

        public string Class { get; set; }
        public string Student { get; set; }
        public string Text { get; set; }
        public string References { get; set; }

        public override void Print()
        {
            Console.WriteLine(Formatter.Format("Class: {0}", Class));
            Console.WriteLine(Formatter.Format("Student: {0}", Student));
            Console.WriteLine(Formatter.Format("Text: {0}", Text));
            Console.WriteLine(Formatter.Format("References: {0}", References));
        }
    }
}