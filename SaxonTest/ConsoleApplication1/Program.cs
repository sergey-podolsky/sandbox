using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    using System.IO;
    using System.Xml;

    using Saxon.Api;

    class Program
    {
        static void Main(string[] args)
        {
            var xml = "<root attr1='42' attr2=''/>";
            var xpath = "//root/name()";
            var evaluator = new XPathEvaluator();
            var document = evaluator.CreateDocument(xml);
            var result = evaluator.Evaluate(document, xpath);
            Console.WriteLine(result.GetType());
        }

        class XPathEvaluator
        {
            private readonly Processor processor;
            private readonly XPathCompiler xpathCompiler;
            private DocumentBuilder documentBuilder;

            public XPathEvaluator()
            {
                processor = new Processor();
                xpathCompiler = processor.NewXPathCompiler();
                documentBuilder = processor.NewDocumentBuilder();
                documentBuilder.BaseUri = new Uri("c://dummy");
            }

            public XdmValue Evaluate(XdmItem xml, string xpath)
            {
                XPathExecutable xpathExecutable = xpathCompiler.Compile(xpath);
                XPathSelector xpathSelector = xpathExecutable.Load();
                xpathSelector.ContextItem = xml;
                return xpathSelector.Evaluate();
            }

            public XdmNode CreateDocument(string xml)
            {
                var stringReader = new StringReader(xml);
                return documentBuilder.Build(stringReader);
            }
        }
    }
}
