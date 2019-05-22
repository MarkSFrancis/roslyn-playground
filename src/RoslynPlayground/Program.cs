using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Analysis;
using RoslynPlayground.Code;
using RoslynPlayground.ConsoleHelpers;
using RoslynPlayground.Samples;
using RoslynPlayground.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynPlayground
{
    using static Console;
    using static ConsoleHelper;

    internal class Program
    {
        private static async Task Main()
        {
            WriteLine("Creating sandbox from source:");
            WriteLine();
            WriteLine(SampleCode.WarningsTest);
            WriteLine();

            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Regular, SampleCode.WarningsTest, 170);
            var analyser = new Analyser(playground);

            await Diagnostics(analyser);

            WriteLine();

            (var line, var column) = PositionConverter.PositionToRowColumn(playground.EditingFile.EditorPosition.Value, playground.EditingFile.RawContents);

            WriteLine($"Intellisense at {playground.EditingFile.EditorPosition} (line {line}, column {column}): ");

            using (UseColor(ConsoleColor.Cyan))
            {
                await Autocomplete(analyser);
            }
            WriteLine();

            WriteLine($"Press {nameof(ConsoleKey.Enter)} to exit");
            UntilEnterPressed();
        }

        private static async Task Diagnostics(Analyser analyser)
        {
            IReadOnlyCollection<Diagnostic> diagnosticsResult = await analyser.GetDiagnosticsAsync();

            if (diagnosticsResult.Count == 0)
            {
                WriteLineInColor("No diagnostics", ConsoleColor.Green);
                return;
            }

            WriteLineInColor("Diagnostics: ", ConsoleColor.White);

            foreach (Diagnostic diagnostic in diagnosticsResult.OrderByDescending(d => d.Severity))
            {
                var start = diagnostic.Location.SourceSpan.Start;
                var length = diagnostic.Location.SourceSpan.Length;

                ConsoleColor colorOfDiagnostic;
                switch (diagnostic.Severity)
                {
                    case DiagnosticSeverity.Hidden:
                        colorOfDiagnostic = ConsoleColor.White;
                        break;
                    case DiagnosticSeverity.Info:
                        colorOfDiagnostic = ConsoleColor.Cyan;
                        break;
                    case DiagnosticSeverity.Warning:
                        colorOfDiagnostic = ConsoleColor.Yellow;
                        break;
                    case DiagnosticSeverity.Error:
                        colorOfDiagnostic = ConsoleColor.Red;
                        break;
                    default:
                        colorOfDiagnostic = ConsoleColor.White;
                        break;
                }

                WriteLineInColor($"From {start} to {start + length}: {diagnostic}", colorOfDiagnostic);
            }
        }

        private static async Task Autocomplete(Analyser analyser)
        {
            IEnumerable<CompletionItem> autocomplete = await analyser.GetAutocompleteAsync();

            var forDisplay = string.Join(Environment.NewLine,
                autocomplete.Select(c => string.Join(", ", c.Tags) + " " + c.DisplayText));

            if (forDisplay != string.Empty)
            {
                WriteLine(forDisplay);
            }
        }
    }
}
