namespace RoslynPlayground.Samples
{
    public static class SampleCode
    {
        public static string AutocompleteTest =>
@"
using System.Collections.Generic;

namespace RoslynPlayground
{
    class Source
    {
        public static string Sample(List<string> texts)
        {
            texts.Ad;
            return string.Join("", "", texts);
        }
    }
}
".ToUnix();

        public static string CompilerTest =>
@"
using System.Collections.Generic;

namespace RoslynPlayground
{
    class Source
    {
        public static string Sample(List<string> texts)
        {
            texts.Add(""From Source"");
            return string.Join("", "", texts);
        }
    }
}
".ToUnix();
    }
}
