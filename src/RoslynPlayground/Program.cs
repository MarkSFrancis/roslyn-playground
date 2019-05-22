using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Analysis;
using RoslynPlayground.Diagnostics;
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
            Console.WriteLine("Creating sandbox from source:");
            Console.WriteLine();
            Console.WriteLine(SampleCode.AutocompleteTest);

            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Regular, SampleCode.AutocompleteTest, 174);

            var diagnostics = new DiagnosticService(playground);
            var diagnosticsResult = await diagnostics.GetDiagnosticsAsync();

            var originalConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            foreach(var diagnostic in diagnosticsResult)
            {
                var start = diagnostic.Location.SourceSpan.Start;
                var length = diagnostic.Location.SourceSpan.Length;

                Console.WriteLine($"From {start} to {start + length}: {diagnostic}");
            }
            Console.ForegroundColor = originalConsoleColor;

            Console.WriteLine();
            Console.WriteLine("Intellisense at " + playground.EditingFile.EditorPosition + ": ");

            var autocompleteService = new AutocompleteService(playground);
            Console.ForegroundColor = ConsoleColor.Cyan;
            await Autocomplete(autocompleteService);
            Console.ForegroundColor = originalConsoleColor;

            Console.WriteLine($"Press {nameof(ConsoleKey.Enter)} to exit");
            UntilEnter();
        }

        private static async Task Autocomplete(AutocompleteService autocompleteService)
        {
            IEnumerable<CompletionItem> autocomplete = await autocompleteService.GetAutoComplete();

            var forDisplay = string.Join(Environment.NewLine,
                autocomplete.Select(c => string.Join(", ", c.Tags) + " " + c.DisplayText));

            if (forDisplay != string.Empty)
            {
                Console.WriteLine(forDisplay);
            }
        }

        private static void UntilEnter()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Enter);
        }
    }
}
