using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using RoslynPlayground.Workspace;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RoslynPlayground.Compiler
{
    public static class AnalyserCompilerExtensions
    {
        public static async Task<CompilerResult> CompileAsync(this PlaygroundWorkspace workspace)
        {
            Compilation compilation = await workspace.ActiveProject.GetCompilationAsync();

            var outputCodeStream = new MemoryStream();
            EmitResult result = compilation.Emit(outputCodeStream);
            var compiled = outputCodeStream.ToArray();

            if (!result.Success || compiled.Length == 0)
            {
                return CompilerResult.FromFail(result.Diagnostics);
            }
            else
            {
                return CompilerResult.FromSuccess(result.Diagnostics, compiled);
            }
        }
    }
}
