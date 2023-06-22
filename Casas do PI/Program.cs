using System;

namespace Casas_do_PI
{
    internal class Program
    {
        static void Main()
        {
            int digits;
            string qSentense, answer, pi;
            bool repeatQ;

            Console.Title = "Casas do PI";
            RefreshConsole();

            do
            {
                qSentense = "\n    Qual é a quantidade de números a gerar: ";
                Console.Write(qSentense);

                answer = Console.ReadLine();
                if (!int.TryParse(answer, out digits))
                {
                    RepeatInput(qSentense.Length, "Formato inválido", null);
                    repeatQ = true;
                }
                else if (digits <= 0)
                {
                    RepeatInput(qSentense.Length, "Deve ser gerado pelo menos 1 dígito", null);
                    repeatQ = true;
                }
                else
                    repeatQ = false;
            }
            while (repeatQ);

            RefreshConsole();
            Console.Write("\n    Aguarde enquanto as casas de PI são calculadas...");

            pi = BigMath.GetPi(digits, 3000).ToString()[1..];

            RefreshConsole();

            while (true)
            {
                int pos = 1;

                do
                {
                    qSentense = "\n    Insira a posição a obter ('e' - Sair, 's' - Mostrar PI): ";
                    Console.Write(qSentense);

                    answer = Console.ReadLine();
                    if (answer == "e")
                        repeatQ = false;
                    else if (answer == "s")
                    {
                        RepeatInput(qSentense.Length, "\n3," + pi, ConsoleColor.Cyan);
                        repeatQ = true;
                    }
                    else if (!int.TryParse(answer, out pos))
                    {
                        RepeatInput(qSentense.Length, "Formato inválido", null);
                        repeatQ = true;
                    }
                    else if (pos < 1 || pos > digits)
                    {
                        RepeatInput(qSentense.Length, "A posição sai fora dos limites (1 - " + digits + ")", null);
                        repeatQ = true;
                    }
                    else
                        repeatQ = false;
                }
                while (repeatQ);

                RefreshConsole();

                if (answer == "e")
                    break;

                Console.SetCursorPosition(qSentense.Length - 1, Console.CursorTop + 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Valor: " + pi[pos - 1]);
                Console.ResetColor();
                Console.SetCursorPosition(0, Console.CursorTop - 2);
            }
        }

        private static void RefreshConsole()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ╔═════════════╗");
            Console.Write(" ║ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Casas do PI");
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