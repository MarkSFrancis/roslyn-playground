using Microsoft.CodeAnalysis;
using NUnit.Framework;
using RoslynPlayground.Compiler;
using RoslynPlayground.Samples;
using RoslynPlayground.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RoslynPlayground.Tests
{
    public class Tests
    {
        [Test]
        public async Task CompilingCode_ThatDoesCompile_CompilesLoadableCode()
        {
            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Regular, SampleCode.CompilerTest);

            var compiler = new CompilerService(playground);

            CompilerResult result = await compiler.Compile();

            Assert.IsTrue(result.Success);

            CollectionAssert.IsEmpty(result.Diagnostics);

            if (result.Success)
            {
                Assembly assembly = AppDomain.CurrentDomain.Load(result.Assembly);

                Type[] sourceTypes = assembly.GetTypes();
                Console.WriteLine(string.Join(", ", sourceTypes.Select(s => s.FullName)));

                Type sourceType = sourceTypes.Single();
                MethodInfo main = sourceType.GetMethod("Sample");
                var invokeResult = main.Invoke(null, new object[] { new List<string> { "asdf1", "asdf" } });

                Assert.IsAssignableFrom<string>(invokeResult);
                Assert.AreEqual("asdf1, asdf, From Source", invokeResult);
            }
        }
    }
}