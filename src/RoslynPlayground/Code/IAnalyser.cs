using Microsoft.CodeAnalysis.Completion;
using System.Threading.Tasks;

namespace RoslynPlayground.Code
{
    public interface IAnalyser
    {
        Task<CompletionItem> GetAutocompleteSuggestions(PlaygroundWorkspace workspace);


    }
}
