using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Compiler;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynPlayground
{
    public interface IAnalyser
    {
        Task<IEnumerable<CompletionItem>> GetAutocompleteAsync();

        Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync();

        Task<CompilerResult> CompileAsync();
    }
}
