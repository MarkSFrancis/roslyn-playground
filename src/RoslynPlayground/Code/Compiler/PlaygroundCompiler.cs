using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RoslynPlayground.Code.Compiler
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
            var project = Workspace.ToProject();

            Compilation compilation = await project.GetCompilationAsync();

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
