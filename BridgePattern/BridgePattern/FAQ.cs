using System;
using System.Collections.Generic;

namespace BridgePattern
{
    public class FAQ : Document
    {
        public string Title { get; set; }
        public Dictionary<string, string> Questions { get; set; }

        public FAQ(IFormatterBridge formatter) : base(formatter)
        {
            Questions = new Dictionary<string, string>();
        }

        public override void Print()
        {
            Console.WriteLine(Formatter.Format("Title: {0}", Title));
            foreach(var question in Questions)
            {
                Console.WriteLine(Formatter.Format("    Question: {0}", question.Key));
                Console.WriteLine(Formatter.Format("    Answer: {0}", question.Value));
            }
        }
    }
}