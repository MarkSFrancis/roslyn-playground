using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Code;
using RoslynPlayground.Compiler;
using RoslynPlayground.ConsoleHelpers;
using RoslynPlayground.Samples;
using RoslynPlayground.Tokens;
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
            WriteLine("Creating sandbox from C# script:");
            WriteLine();
            WriteLine(SampleScript.HelloWorld);
            WriteLine();

            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Script, SampleScript.HelloWorld, 26);

            using (var analyser = new Analyser(playground))
            {
                await Tokenize(analyser);

                WriteLine();

                await Diagnostics(analyser);

                WriteLine();

                (var line, var column) = PositionConverter.PositionToRowColumn(playground.EditingFile.EditorPosition.Value, playground.EditingFile.RawContents);

                WriteLine($"Intellisense at {playground.EditingFile.EditorPosition} (line {line}, column {column}): ");

                using (UseColor(ConsoleColor.Cyan))
                {
                    await Autocomplete(analyser);
                }
                WriteLine();

                WriteLine($"Compiling...");

                using (UseColor(ConsoleColor.Cyan))
                {
                    await analyser.CompileAndRunScript();
                }
            }

            WriteLine();

            WriteLine($"Press {nameof(ConsoleKey.Enter)} to exit");
            UntilEnterPressed();
        }

        private static async Task Tokenize(IAnalyser analyser)
        {
            var tokens = await analyser.GetTokensAsync();

            WriteLine("Tokenized source: ");

            int lineId = 0;
            foreach (var token in tokens.OrderBy(t => t.Start))
            {
                WriteCodeBlock(token.Text, token.Type, ref lineId);
            }

            WriteLine();
        }

        private static async Task Diagnostics(IAnalyser analyser)
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
                    case DiagnosticSeverity.Info:
                        colorOfDiagnostic = ConsoleColor.Cyan;
                        break;
                    case DiagnosticSeverity.Warning:
                        colorOfDiagnostic = ConsoleColor.Yellow;
                        break;
                    case DiagnosticSeverity.Error:
                        colorOfDiagnostic = ConsoleColor.Red;
                        break;
                    case DiagnosticSeverity.Hidden:
                    default:
                        colorOfDiagnostic = ConsoleColor.White;
                        break;
                }

                WriteLineInColor($"From {start} to {start + length}: {diagnostic}", colorOfDiagnostic);
            }
        }

        private static async Task Autocomplete(IAnalyser analyser)
        {
            IEnumerable<CompletionItem> autocomplete = await analyser.GetAutoCompleteAsync();

            var forDisplay = string.Join(Environment.NewLine,
                autocomplete.Select(c => string.Join(", ", c.Tags) + " " + c.DisplayText));

            if (forDisplay != string.Empty)
            {
                WriteLine(forDisplay);
            }
        }

        private static void WriteCodeBlock(string codeBlock, TokenType type, ref int currentLine)
        {
            if (currentLine == 0)
            {
                WriteLineMarker(ref currentLine);
            }

            var formattedLines = codeBlock.Split(Environment.NewLine);

            void WriteLineMarker(ref int currentLine)
            {
                WriteInColor(++currentLine + " |", ConsoleColor.DarkGray);
            }

            void WriteNode(string node)
            {
                if (type == TokenType.Keyword)
                {
                    WriteInColor(node, ConsoleColor.Blue);
                }
                else if (type == TokenType.Literal)
                {
                    WriteInColor(node, ConsoleColor.Red);
                }
                else
                {
                    Write(node);
                }
            }

            if (formattedLines.Length > 1)
            {
                // Is multi-line block
                WriteNode(formattedLines[0] + Environment.NewLine);

                for (int index = 1; index < formattedLines.Length - 1; index++)
                {
                    WriteLineMarker(ref currentLine);

                    WriteNode(formattedLines[index] + Environment.NewLine);
                }

                WriteLineMarker(ref currentLine);

                WriteNode(formattedLines[^1]);
            }
            else
            {
                WriteNode(codeBlock);
            }
        }
    }
}
