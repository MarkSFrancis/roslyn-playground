using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoslynPlayground.Workspace
{
    public class PlaygroundCodeContainer : SourceTextContainer
    {
        private SourceText _source;

        public PlaygroundCodeContainer(string text) : this(SourceText.From(text))
        {
        }

        public PlaygroundCodeContainer(SourceText text)
        {
            _source = text;
        }

        public override SourceText CurrentText => _source;

        public void ChangeCode(int start, string newCode, bool replaceOld = false)
        {
            OverwriteCode(start, replaceOld ? newCode.Length : 0, newCode);
        }

        public void OverwriteCode(int start, int length, string newCode)
        {
            var changes = new List<(TextSpan span, string newText)>(1)
            {
                (new TextSpan(start, length), newCode)
            };

            var allChanges = changes.Select(c => new TextChange(c.span, c.newText)).ToArray();
            SourceText newSource = _source.WithChanges(allChanges);

            var textChangesEvent = new TextChangeEventArgs(_source, newSource, changes.Select(c => new TextChangeRange(c.span, c.newText.Length)).ToArray());
            TextChanged?.Invoke(this, textChangesEvent);

            _source = newSource;
        }

        public void ReplaceCode(string newCode)
        {
            OverwriteCode(0, CurrentText.Length, newCode);
        }

        public override event EventHandler<TextChangeEventArgs> TextChanged;

        public TextLoader GetTextLoader(string filename)
        {
            return new SourceTextLoader(this, filename);
        }

        private sealed class SourceTextLoader : TextLoader
        {
            private readonly SourceTextContainer _textContainer;
            private readonly string _filePath;

            public SourceTextLoader(SourceTextContainer textContainer, string filePath)
            {
                _textContainer = textContainer;
                _filePath = filePath;
            }

            public override Task<TextAndVersion> LoadTextAndVersionAsync(Microsoft.CodeAnalysis.Workspace workspace, DocumentId documentId, CancellationToken cancellationToken)
            {
                return Task.FromResult(TextAndVersion.Create(_textContainer.CurrentText, VersionStamp.Create(), _filePath));
            }
        }
    }
}
