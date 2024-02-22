using PI_Decimals.Properties;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace PI_Decimals
{
    internal class Program
    {
        static void Main()
        {
            int digits;
            string qSentense, answer, pi;
            bool repeatQ;

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            Console.Title = Resources.AppName;
            RefreshConsole();

            do
            {
                qSentense = $"\n    {Resources.GenerateQuestion}: ";
                Console.Write(qSentense);

                answer = Console.ReadLine().Trim();
                if (!int.TryParse(answer, out digits))
                {
                    RepeatInput(qSentense.Length, Resources.FormatError, null);
                    repeatQ = true;
                }
                else if (digits <= 0)
                {
                    RepeatInput(qSentense.Length, Resources.NegativeError, null);
                    repeatQ = true;
                }
                else
                    repeatQ = false;
            }
            while (repeatQ);

            RefreshConsole();
            Console.Write($"\n    {Resources.LoadingMsg}...");

            pi = BigMath.GetPi(digits, 3000).ToString()[1..];

            RefreshConsole();

            while (true)
            {
                int[] matches;

                do
                {
                    qSentense = $"\n    {Resources.PositionQuestion}: ";
                    Console.Write(qSentense);

                    answer = Console.ReadLine().ToLower().Trim();
                    matches = Regex.Matches(answer, @"\d+").Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToArray();

                    if (answer == "e")
                        repeatQ = false;
                    else if (answer == "h")
                    {
                        RepeatInput(qSentense.Length, $"\n        1-9 - {Resources.HelpRangeMsg}\n        e   - {Resources.HelpExitMsg}\n        s   - {Resources.HelpShowMsg}", ConsoleColor.DarkGray);
                        repeatQ = true;
                    }
                    else if (answer == "s")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n    3," + pi);
                        Console.ResetColor();
                        Console.Write($"\n\n    {Resources.EnterMsg}...");
                        Console.ReadKey();

                        RefreshConsole();
                        repeatQ = true;
                    }
                    else if (matches.Length == 0 || matches.Length == 2 && matches[0] > matches[1])
                    {
                        RepeatInput(qSentense.Length, Resources.FormatError, null);
                        repeatQ = true;
                    }
                    else if (matches.Length == 1 && (matches[0] < 1 || matches[0] > digits) || matches.Length == 2 && (matches[0] < 1 || matches[1] > digits))
                    {
                        RepeatInput(qSentense.Length, $"{Resources.PositionOverflow} [1-" + digits + "]", null);
                        repeatQ = true;
                    }
                    else
                        repeatQ = false;
                }
                while (repeatQ);

                if (answer == "e")
                    break;

                RefreshConsole();

                Console.ForegroundColor = ConsoleColor.Cyan;
                if (matches.Length == 1)
                {
                    Console.SetCursorPosition(qSentense.Length - 1, Console.CursorTop + 2);
                    Console.Write($"{Resources.ValueMsg}: " + pi[matches[0] - 1]);
                    Console.ResetColor();
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                }
                else
                {
                    Console.Write("\n    " + pi[(matches[0] - 1)..(matches[1])]);
                    Console.ResetColor();
                    Console.Write($"\n\n    {Resources.EnterMsg}...");
                    Console.ReadKey();

                    RefreshConsole();
                }
            }
        }

        private static void RefreshConsole()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ╔═════════════╗");
            Console.Write(" ║ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Resources.AppName);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ║");
            Console.WriteLine(" ╚═════════════╝");
            Console.ResetColor();
        }

        private static void RepeatInput(int length, string reason, ConsoleColor? fColor)
        {
            RefreshConsole();

            int topPos = Console.CursorTop;
            Console.SetCursorPosition(length - 1, Console.CursorTop + 2);
            Console.ForegroundColor = fColor == null ? ConsoleColor.Red : (ConsoleColor)fColor;
            Console.Write(reason);
            Console.ResetColor();
            Console.SetCursorPosition(0, topPos);
        }
    }
}