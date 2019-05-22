using Microsoft.CodeAnalysis;
using RoslynPlayground.Code;
using System.Collections.Generic;
using System.IO;

namespace RoslynPlayground.Workspace
{
    public static class ReferenceBuilder
    {
        private static readonly string _coreLibrariesDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location);

        public static IEnumerable<MetadataReference> GetReferences(IEnumerable<string> extraReferences)
        {
            yield return GetAssemblyPathByName("System.Private.CoreLib");

            foreach (var reference in CodeImports.GetFrameworkReferences())
            {
                yield return GetAssemblyPathByName(reference);
            }

            foreach (var extraReference in extraReferences)
            {
                yield return GetAssemblyPathByName(extraReference);
            }
        }

        public static IEnumerable<MetadataReference> GetReferences(params string[] extraReferences)
        {
            return GetReferences((IEnumerable<string>)extraReferences);
        }

        private static MetadataReference GetAssemblyPathByName(string filename)
        {
            const string dllExtension = ".dll";

            if (!filename.EndsWith(dllExtension))
            {
                filename += dllExtension;
            }

            return MetadataReference.CreateFromFile(Path.Combine(_coreLibrariesDirectory, filename));
        }
    }
}
