using Microsoft.CodeAnalysis;
using RoslynPlayground.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<Diagnostic>> GetDiagnosticsAsync()
        {
            var semantics = await Workspace.EditingDocument.GetSemanticModelAsync();

            var diagnostics = semantics.GetDiagnostics();

            return diagnostics;
        }
    }
}
