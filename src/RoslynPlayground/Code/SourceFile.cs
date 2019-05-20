using Microsoft.CodeAnalysis.Text;
using System.IO;

namespace RoslynPlayground.Code
{
    public class SourceFile
    {
        public SourceFile(string filename, SourceText code, int? editorPosition = null)
        {
            Filename = filename;
            RawCode = code.ToString();
            Code = code;
            EditorPosition = editorPosition;
        }

        public SourceFile(string filename, string code, int? editorPosition = null)
        {
            Filename = filename;
            RawCode = code;
            Code = SourceText.From(code);
            EditorPosition = editorPosition;
        }

        public string Filename { get; }
        public SourceText Code { get; }
        public string RawCode { get; set; }
        public int? EditorPosition { get; }

        public static SourceFile FromFile(string fileLocation, int? editorPosition = null)
        {
            var contents = File.ReadAllText(fileLocation);

            return new SourceFile(contents, Path.GetFileName(fileLocation), editorPosition);
        }
    }
}
