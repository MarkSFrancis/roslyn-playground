namespace RoslynPlayground.Workspace
{
    public class EditorPositionChangedEventArgs
    {
        public EditorPositionChangedEventArgs(string filename, int? newPosition)
        {
            Filename = filename;
            NewPosition = newPosition;
        }

        public int? NewPosition { get; set; }

        public string Filename { get; set; }
    }
}
