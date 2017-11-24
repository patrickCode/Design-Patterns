using BridgePattern;
using System;
using System.Collections.Generic;

namespace BridgePatter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Document> documents = new List<Document>();
            IFormatterBridge formatter = new StandardFormatter();

            var book = new Book(formatter)
            {
                Author = "Dan Brown",
                Title = "The Da Vinci Code",
                Text = "Blah blah blah ..."
            };
            documents.Add(book);

            var paper = new TermPaper(formatter)
            {
                Class = "Software Engineering",
                Student = "Jeremy Hall, Clara Knight",
                References = "SWE101",
                Text = "Blah blah blah ..."
            };
            documents.Add(paper);

            var faq = new FAQ(formatter)
            {
                Title = "Design Patterns"
            };
            faq.Questions.Add("Who owns this?", "No one");
            faq.Questions.Add("Who created this?", "No one");
            documents.Add(faq);

            foreach (var document in documents)
            {
                document.Print();
            }

            System.Console.ReadLine();
        }
    }
}
