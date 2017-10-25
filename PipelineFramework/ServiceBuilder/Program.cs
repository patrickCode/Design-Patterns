using ItemBuilder;
using Newtonsoft.Json;
using PipelineFramework;
using System;
using System.Collections.Generic;

namespace ServiceBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Item>
            {
                new Item()
                {
                    Id = 1,
                    Name = "Item_1",
                    Description = "Item_1 Description",
                    Cost = 12.32M,
                    CreatedBy = "pratikb@microsoft.com",
                    CreatedOn = DateTime.UtcNow
                },
                new Item()
                {
                    Id = 2,
                    Name = "Item_2",
                    Description = "Item_2 Description",
                    Cost = 17.89M,
                    CreatedBy = "pratikb@microsoft.com",
                    CreatedOn = DateTime.UtcNow
                }
            };

            var requestObject = JsonConvert.SerializeObject(items);
            var pipelineRequest = new PipelineRequest()
            {
                ExecutedBy = "pratikb@microsoft.com",
                RequestObject = requestObject,
                Configuration = new Dictionary<string, object>()
            };

            var pipeline = ItemCreationInMemoryPipeline.CreatePipeline();
            pipeline.Execute(pipelineRequest);

            Console.ReadLine();
        }
    }
}
