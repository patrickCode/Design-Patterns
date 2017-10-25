using System;
using PipelineFramework;
using ItemBuilder.Middleware;

namespace ItemBuilder
{
    public class ItemCreationInMemoryPipeline
    {
        public static BasePipeline CreatePipeline()
        {
            var validator = new ValidatorMiddleware();
            var skuAssigner = new SkuAssignerMiddleware();
            skuAssigner.AddConfiguration("SKU.Prefix", "AAA");
            var printer = new PrintMiddleware();

            var builder = new InMemoryPipeline.PipelineBuilder();
            builder.Add(validator);
            builder.Add(skuAssigner);
            builder.Add(printer);

            return builder.Build("Item Builder Pipeline", Guid.NewGuid());
        }
    }
}