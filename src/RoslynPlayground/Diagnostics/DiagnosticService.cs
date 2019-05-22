using Microsoft.CodeAnalysis;
using RoslynPlayground.Workspace;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace RoslynPlayground.Diagnostics
{
    public class DiagnosticService
    {
        public DiagnosticService(PlaygroundWorkspace workspace)
        {
            Workspace = workspace;
        }

        public PlaygroundWorkspace Workspace { get; }

        public async Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync()
        {
            SemanticModel semantics = await Workspace.EditingDocument.GetSemanticModelAsync();

            ImmutableArray<Diagnostic> diagnostics = semantics.GetDiagnostics();

            return diagnostics;
        }
    }
}
