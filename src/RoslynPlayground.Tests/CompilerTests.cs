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

            CompilerResult result = await playground.CompileAsync();

            Assert.IsTrue(result.Success);

            CollectionAssert.IsEmpty(result.Diagnostics);

            if (result.Success)
            {
                Assembly assembly = AppDomain.CurrentDomain.Load(result.Assembly);

                Type[] sourceTypes = assembly.GetTypes();

                Type sourceType = sourceTypes.Single();
                MethodInfo main = sourceType.GetMethod("Sample");
                var invokeResult = main.Invoke(null, new object[] { new List<string> { "asdf1", "asdf" } });

                Assert.IsAssignableFrom<string>(invokeResult);
                Assert.AreEqual("asdf1, asdf, From Source", invokeResult);
            }
        }

        [Test]
        public async Task CompilingScript_ThatDoesCompile_CompilesLoadableCode()
        {
            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Script, SampleScript.HelloWorld);

            CompilerResult result = await playground.CompileAsync();

            Assert.IsTrue(result.Success);

            CollectionAssert.IsEmpty(result.Diagnostics);

            if (result.Success)
            {
                Assembly assembly = AppDomain.CurrentDomain.Load(result.Assembly);

                var scriptType = assembly.GetType(Runner.ScriptTypeName);
                MethodInfo main = scriptType.GetMethod(Runner.ScriptEntryPointName, Runner.ScriptEntryPointFlags);
                var invokeResult = main.Invoke(null, new object[0]);

                Assert.AreEqual(null, invokeResult);
            }
        }
    }
}