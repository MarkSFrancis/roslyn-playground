using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Code;
using RoslynPlayground.Code.Analysis;
using RoslynPlayground.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynPlayground
{
    internal class Program
    {
        private static async Task Main()
        {
            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Regular, SampleCode.AutocompleteTest, 174);

            var autocompleteService = new PlaygroundAutocomplete(playground);

            IEnumerable<CompletionItem> autocomplete = await autocompleteService.GetAutoComplete();

            var forDisplay = string.Join(Environment.NewLine, 
                autocomplete.Select(c => string.Join(", ", c.Tags) + " " + c.DisplayText));

            if (forDisplay != string.Empty)
            {
                Console.WriteLine(forDisplay);
            }

            Console.ReadKey(true);
        }
    }
}
