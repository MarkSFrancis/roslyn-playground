using System;
using System.Linq;

namespace RoslynPlayground.ConsoleHelpers
{
    public static class ConsoleHelper
    {
        public static ConsoleKey UntilPressed(params ConsoleKey[] keysToWaitFor)
        {
            if (keysToWaitFor is null)
            {
                throw new ArgumentNullException(nameof(keysToWaitFor));
            }
            else if (keysToWaitFor.Length == 0)
            {
                throw new ArgumentException(nameof(keysToWaitFor) + " cannot be empty");
            }

            ConsoleKey keypress;
            do
            {
                keypress = Console.ReadKey(true).Key;
            } while (!keysToWaitFor.Contains(keypress));

            return keypress;
        }

        public static void UntilEnterPressed() => UntilPressed(ConsoleKey.Enter);

        public static void WriteInColor(string message, ConsoleColor color)
        {
            using (new ColorHelper(color))
            {
                Console.Write(message);
            }
        }

        public static void WriteLineInColor(string message, ConsoleColor color)
        {
            using (new ColorHelper(color))
            {
                Console.WriteLine(message);
            }
        }

        public static void WriteLineInfo(string message) => WriteLineInColor(message, ConsoleColor.Cyan);

        public static void WriteLineError(string message) => WriteLineInColor(message, ConsoleColor.Red);

        public static ColorHelper UseColor(ConsoleColor color) => new ColorHelper(color);
    }
}
