using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace RoslynPlayground.Compiler
{
    public class CompilerResult
    {
        protected CompilerResult(ImmutableArray<Diagnostic> diagnostics, byte[] result)
        {
            Diagnostics = diagnostics;
            Assembly = result;
        }

        public bool Success => Assembly != null;

        public ImmutableArray<Diagnostic> Diagnostics { get; set; }

        public byte[] Assembly { get; set; }

        public static CompilerResult FromFail(ImmutableArray<Diagnostic> diagnostics)
        {
            return new CompilerResult(diagnostics, null);
        }

        public static CompilerResult FromSuccess(ImmutableArray<Diagnostic> diagnostics, byte[] result)
        {
            return new CompilerResult(diagnostics, result);
        }
    }
}
