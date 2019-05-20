public static class UnixStringExtensions
{
    public static string ToUnix(this string str)
    {
        return str.Replace("\r\n", "\n");
    }
}
