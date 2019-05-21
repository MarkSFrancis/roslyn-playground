using System;

namespace RoslynPlayground.Workspace
{
    public class EditorPositionChangedEventArgs
    {
        public EditorPositionChangedEventArgs(SourceFile source, int? newPosition)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            NewPosition = newPosition;
        }

        public int? NewPosition { get; set; }

        public SourceFile Source { get; set; }
    }
}
