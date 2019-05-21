using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;

namespace RoslynPlayground.Workspace
{
    public class SourceFile
    {
        public SourceFile(string filename, SourceText code, int? editorPosition = null)
        {
            Filename = filename;
            Code = new PlaygroundCodeContainer(code);
            _editorPosition = editorPosition;
        }

        public SourceFile(string filename, string code, int? editorPosition = null)
        {
            Filename = filename;
            Code = new PlaygroundCodeContainer(code);
            _editorPosition = editorPosition;
        }

        private int? _editorPosition;

        public string Filename { get; }
        public PlaygroundCodeContainer Code { get; }
        public string RawContents => Code?.CurrentText?.ToString();
        public int? EditorPosition
        {
            get => _editorPosition;
            set
            {
                _editorPosition = value;
                EditorPositionChanged?.Invoke(this, new EditorPositionChangedEventArgs(Filename, _editorPosition));
            }
        }

        public static SourceFile FromFile(string fileLocation, int? editorPosition = null)
        {
            var contents = File.ReadAllText(fileLocation);

            return new SourceFile(Path.GetFileName(fileLocation), contents, editorPosition);
        }

        public TextLoader GetSourceLoader() => Code.GetTextLoader(Filename);

        public event EventHandler<EditorPositionChangedEventArgs> EditorPositionChanged;
    }
}
