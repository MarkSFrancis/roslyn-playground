using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Workspace;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynPlayground.Analysis
{
    public class PlaygroundAutocomplete
    {
        public PlaygroundAutocomplete(PlaygroundWorkspace workspace)
        {
            Workspace = workspace;

            if (Workspace.EditingDocument != null)
            {
                _completionService = CompletionService.GetService(Workspace.EditingDocument);
            }
        }

        public PlaygroundWorkspace Workspace { get; }

        private readonly CompletionService _completionService;

        public async Task<IEnumerable<CompletionItem>> GetAutoComplete()
        {
            if (Workspace.EditingDocument is null)
            {
                return new List<CompletionItem>();
            }

            CompletionList completionList = await _completionService.GetCompletionsAsync(Workspace.EditingDocument, Workspace.EditingFile.EditorPosition.Value);

            return FilterByActiveSpan(completionList, Workspace.EditingFile.RawContents);
        }

        private IEnumerable<CompletionItem> FilterByActiveSpan(CompletionList suggested, string originalSource)
        {
            if (originalSource is null || suggested.Span.IsEmpty)
            {
                return suggested.Items;
            }

            var editingPrefix = originalSource.Substring(suggested.Span.Start, suggested.Span.Length);

            return suggested.Items.Where(c => c.DisplayText.StartsWith(editingPrefix));
        }
    }
}
