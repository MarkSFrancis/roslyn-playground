using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using RoslynPlayground.Workspace;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RoslynPlayground.Compiler
{
    public class PlaygroundCompiler
    {
        public PlaygroundCompiler(PlaygroundWorkspace workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }

        public PlaygroundWorkspace Workspace { get; set; }

        public async Task<CompilerResult> Compile()
        {
            Compilation compilation = await Workspace.ActiveProject.GetCompilationAsync();

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
