using Microsoft.CodeAnalysis.Completion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynPlayground.Code.Analysis
{
    public class PlaygroundAutocomplete
    {
        public PlaygroundAutocomplete(PlaygroundWorkspace workspace)
        {
            Workspace = workspace;
        }

        public PlaygroundWorkspace Workspace { get; }

        public async Task<IEnumerable<CompletionItem>> GetAutoComplete()
        {
            if (Workspace.EditingFile is null)
            {
                return new List<CompletionItem>();
            }

            var proj = Workspace.ToProject();
            Microsoft.CodeAnalysis.Document editingDocument = proj.Documents.First(d => d.Name == Workspace.EditingFile.Filename);

            var completor = CompletionService.GetService(editingDocument);

            CompletionList completionList = await completor.GetCompletionsAsync(editingDocument, Workspace.EditingFile.EditorPosition.Value);

            return FilterByActiveSpan(completionList, Workspace.EditingFile.RawCode);
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
