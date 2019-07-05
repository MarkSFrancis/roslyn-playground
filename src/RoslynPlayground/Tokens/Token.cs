namespace RoslynPlayground.Tokens
{
    public class Token
    {
        public string Text { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public TokenType Type { get; set; }
    }
}
