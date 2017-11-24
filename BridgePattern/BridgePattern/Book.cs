using System;

namespace BridgePattern
{
    public class Book: Document
    {
        public Book(IFormatterBridge formatter) : base(formatter)
        {
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public override void Print()
        {
            Console.WriteLine(Formatter.Format("Title: {0}", Title));
            Console.WriteLine(Formatter.Format("Author: {0}", Author));
            Console.WriteLine(Formatter.Format("Text: {0}", Text));
        }
    }
}