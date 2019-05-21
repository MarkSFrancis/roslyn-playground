using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Analysis;
using RoslynPlayground.Samples;
using RoslynPlayground.Workspace;
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

            await Autocomplete(autocompleteService);
            playground.EditingFile.Code.ChangeCode(172, "To", true);
            await Autocomplete(autocompleteService);
            playground.EditingFile.Code.ChangeCode(172, "Ex", true);
            await Autocomplete(autocompleteService);
            playground.EditingFile.Code.ChangeCode(172, "T", true);
            await Autocomplete(autocompleteService);
            playground.EditingFile.EditorPosition--;

            Console.ReadKey(true);
        }

        private static async Task Autocomplete(PlaygroundAutocomplete autocompleteService)
        {
            IEnumerable<CompletionItem> autocomplete = await autocompleteService.GetAutoComplete();

            var forDisplay = string.Join(Environment.NewLine,
                autocomplete.Select(c => string.Join(", ", c.Tags) + " " + c.DisplayText));

            if (forDisplay != string.Empty)
            {
                Console.WriteLine(forDisplay);
            }
        }
    }
}
