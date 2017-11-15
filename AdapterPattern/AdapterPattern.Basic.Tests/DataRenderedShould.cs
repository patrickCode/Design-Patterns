using AdapterPattern.Basic.Renderer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdapterPattern.Basic.Tests
{
    [TestClass]
    public class DataRenderedShould
    {
        [TestMethod]
        public void ShouldRenderFromStubDataAdapter()
        {
            var dataAdapter = new StubDataDbAdapter();
            var renderer = new DataRenderer(dataAdapter);

            var writer = new StringWriter();
            renderer.Render(writer);

            var result = writer.ToString();
            Console.Write(result);

            Assert.IsNotNull(result);
        }
    }
}
