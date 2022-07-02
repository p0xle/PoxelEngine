using System;

namespace PoxelEngine.Utility
{
    public static class Log
    {
        static Log()
        {
            Console.CursorVisible = false;
            Console.Title = "Debug Console";
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static readonly ConsoleColor DefaultColor = ConsoleColor.White;

        public static ConsoleColor WriteColor = ConsoleColor.White;
        public static ConsoleColor InfoColor = ConsoleColor.Cyan;
        public static ConsoleColor WarnColor = ConsoleColor.Yellow;
        public static ConsoleColor ErrorColor = ConsoleColor.Red;

        public static void Reset()
        {
            Console.Clear();
            Console.ForegroundColor = DefaultColor;
        }

        public static void Write(string msg)
        {
            Console.ForegroundColor = WriteColor;
            Console.WriteLine(msg);
            Console.ForegroundColor = DefaultColor;
        }

        public static void Debug(string msg)
        {
            Console.ForegroundColor = WriteColor;
            Console.WriteLine($"[MSG] - [{DateTime.Now}] - {msg}");
            Console.ForegroundColor = DefaultColor;
        }

        public static void Info(string msg)
        {
            Console.ForegroundColor = InfoColor;
            Console.WriteLine($"[INFO] - [{DateTime.Now}] - {msg}");
            Console.ForegroundColor = DefaultColor;
        }

        public static void Warning(string msg)
        {
            Console.ForegroundColor = WarnColor;
            Console.WriteLine($"[WARN] - [{DateTime.Now}] - {msg}");
            Console.ForegroundColor = DefaultColor;
        }

        public static void Error(string msg)
        {
            Console.ForegroundColor = ErrorColor;
            Console.WriteLine($"[ERROR] - [{DateTime.Now}] - {msg}");
            Console.ForegroundColor = DefaultColor;
        }
    }
}
