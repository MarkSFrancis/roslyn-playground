using System.IO;

namespace RoslynPlayground.Compiler
{
    public static class CompilerSettings
    {
        public static readonly string OutputDirectory = Path.GetDirectoryName(typeof(CompilerSettings).Assembly.Location);
    }
}
