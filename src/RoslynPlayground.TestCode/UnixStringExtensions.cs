public static class UnixStringExtensions
{
    public const string WindowsNewLine = "\r\n";
    public const string UnixNewLine = "\n";

    public static string ToUnix(this string str)
    {
        return str.Replace(WindowsNewLine, UnixNewLine);
    }
}
