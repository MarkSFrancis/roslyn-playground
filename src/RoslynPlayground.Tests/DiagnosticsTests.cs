using Microsoft.CodeAnalysis;
using NUnit.Framework;
using RoslynPlayground.Diagnostics;
using RoslynPlayground.Samples;
using RoslynPlayground.Workspace;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynPlayground.Tests
{
    public class DiagnosticsTests
    {
        [Test]
        public async Task GetDiagnostics_ForCodeThatProducesWarnings_ReturnsWarnings()
        {
            var code = SampleCode.WarningsTest;

            var workspace = PlaygroundWorkspace.FromSource(SourceCodeKind.Regular, code, 0);

            IReadOnlyCollection<Diagnostic> diagnostics = await workspace.GetDiagnosticsAsync();

            Assert.AreEqual(1, diagnostics.Count);
            Assert.AreEqual(DiagnosticSeverity.Warning, diagnostics.First().Severity);
        }
    }
}
