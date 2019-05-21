using Microsoft.CodeAnalysis.Completion;
using RoslynPlayground.Workspace;
using System.Threading.Tasks;

namespace RoslynPlayground
{
    public interface IAnalyser
    {
        Task<CompletionItem> GetAutocompleteSuggestions(PlaygroundWorkspace workspace);


    }
}
