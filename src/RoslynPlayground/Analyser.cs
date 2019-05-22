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

            _compiler = new CompilerService(workspace);
            _autocomplete = new AutocompleteService(workspace);
            _diagnostics = new DiagnosticService(workspace);
        }

        public PlaygroundWorkspace Workspace { get; }

        private readonly CompilerService _compiler;
        private readonly AutocompleteService _autocomplete;
        private readonly DiagnosticService _diagnostics;

        public Task<CompilerResult> CompileAsync()
        {
            return _compiler.Compile();
        }

        public Task<IEnumerable<CompletionItem>> GetAutocompleteAsync()
        {
            return _autocomplete.GetAutoComplete();
        }

        public Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync()
        {
            return _diagnostics.GetDiagnosticsAsync();
        }
    }
}
