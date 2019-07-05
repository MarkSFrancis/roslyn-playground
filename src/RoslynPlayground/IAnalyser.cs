using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Compiler;
using RoslynPlayground.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynPlayground
{
    public interface IAnalyser : IDisposable
    {
        Task<IEnumerable<Token>> GetTokensAsync();

        Task<IEnumerable<CompletionItem>> GetAutoCompleteAsync();

        Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync();

        Task<CompilerResult> CompileAsync();
    }
}
