using Microsoft.CodeAnalysis;
using RoslynPlayground.Workspace;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace RoslynPlayground.Diagnostics
{
    public static class DiagnosticService
    {
        public static async Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync(this PlaygroundWorkspace workspace)
        {
            SemanticModel semantics = await workspace.EditingDocument.GetSemanticModelAsync();

            ImmutableArray<Diagnostic> diagnostics = semantics.GetDiagnostics();

            return diagnostics;
        }
    }
}
