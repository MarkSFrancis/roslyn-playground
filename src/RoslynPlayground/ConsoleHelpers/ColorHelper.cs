using System;

namespace RoslynPlayground.ConsoleHelpers
{
    public class ColorHelper : IDisposable
    {
        private readonly ConsoleColor _initialColor;

        public ColorHelper(ConsoleColor color)
        {
            _initialColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ForegroundColor = _initialColor;
        }
    }
}
