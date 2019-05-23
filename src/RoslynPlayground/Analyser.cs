using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Analysis;
using RoslynPlayground.Compiler;
using RoslynPlayground.Diagnostics;
using RoslynPlayground.Workspace;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynPlayground
{
    public class Analyser : IAnalyser
    {
        public Analyser(PlaygroundWorkspace workspace)
        {
            Workspace = workspace;

            _autoComplete = new AutoCompleteService(workspace);
        }

        public PlaygroundWorkspace Workspace { get; }

        private readonly AutoCompleteService _autoComplete;

        public Task<CompilerResult> CompileAsync()
        {
            return Workspace.CompileAsync();
        }

        public Task<IEnumerable<CompletionItem>> GetAutoCompleteAsync()
        {
            return _autoComplete.GetAutoCompleteAsync();
        }

        public Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync()
        {
            return Workspace.GetDiagnosticsAsync();
        }

        public void Dispose()
        {
            _autoComplete?.Dispose();
        }
    }
}
