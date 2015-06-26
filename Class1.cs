using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootBasics
{
    /// <summary>
    /// Maths operations
    /// </summary>
    public static class Maths
    {
        /// <summary>
        /// Finds the factorial of an int in the form of a float
        /// </summary>
        /// <param name="n">The number to operate on</param>
        /// <returns>Float n!</returns>
        public static float fac(int n)
        {
            float r = 1;

            for (int i = n; i > 1; i--)
            {
                r = r * i;
            }

            return r;
        }

        /// <summary>
        /// Finds the factorial of an int and returns an int
        /// </summary>
        /// <param name="n">The number</param>
        /// <returns>Int n!</returns>
        public static int facInt(int n)
        {
            int r = 1;

            for (int i = n; i > 1; i--)
            {
                r = r * i;
            }

            return r;
        }


    }

    /// <summary>
    /// Methods relating to the console display
    /// </summary>
    public static class Display
    {
        /// <summary>
        /// Write a string to the console at a given position
        /// </summary>
        /// <param name="x">Collumn (from the left) to write at</param>
        /// <param name="y">Row (from the top) to write at</param>
        /// <param name="message">The string to write</param>
        /// <param name="foreground">Optional foreground colour (default is gray)</param>
        /// <param name="background">Optional background colour (default is black)</param>
        public static void writeAt(int x, int y, string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.Write(message);
        }


        /// <summary>
        /// Creates a selector with various options for the user to chose from. User must press enter to select
        /// </summary>
        /// <param name="options">The options to be displayed to the user</param>
        /// <param name="x">The collumn (from the left) to display the selector from</param>
        /// <param name="y">The row (from the top) to display the selector on</param>
        /// <returns>The zero-based index of the option selected by the user.</returns>
        public static int selector(string[] options, int x, int y)
        {
            bool selected = false;
            int selectedIndex = 0;

            writeOptions(options, 0, x, y);

            while (!selected)
            {
                ConsoleKeyInfo k = Console.ReadKey();

                if (k.Key == ConsoleKey.LeftArrow && selectedIndex != 0)
                {
                    selectedIndex--;
                }
                else if (k.Key == ConsoleKey.RightArrow && selectedIndex != 4)
                {
                    selectedIndex++;
                }
                else if (k.Key == ConsoleKey.Enter)
                {
                    selected = true;
                    break;
                }

                clearLine(3);

                writeOptions(options, selectedIndex, x, y);
            }

            return selectedIndex;
        }

        static void writeOptions(string[] options, int indexSelected, int x, int y)
        {
            int i = 0;
            int currentPos = x;

            clearLine(y);

            foreach (string o in options)
            {
                if (indexSelected == i)
                {
                    writeAt(currentPos, y, "[" + o + "]");
                    currentPos += 2 + o.Length + 1;
                }
                else
                {
                    writeAt(currentPos, y, o);

                    currentPos += o.Length + 1;
                }

                i++;
            }
        }

        /// <summary>
        /// Clear a given line
        /// </summary>
        /// <param name="y">The row (from the top) to clear</param>
        public static void clearLine(int y)
        {
            writeAt(0, y, "                                                                        ");
        }

        /// <summary>
        /// Displays a distribution input prompt in the form X ~ D(
        /// </summary>
        /// <param name="varName">The name of the variable to be displayed</param>
        /// <param name="distributionType">The distribution type to be displayed to the user (i.e. B for Binomial, P for Poisson)</param>
        /// <param name="numberOfVariables">The number of variables</param>
        /// <param name="x">The collumn (from left) to write at</param>
        /// <param name="y">The row (from the top) to write at</param>
        /// <returns></returns>
        public static double[] distributionInput(string varName, string distributionType, int numberOfVariables, int x, int y)
        {
            Display.writeAt(x, y, varName + " ~ " + distributionType + "(");

            int consolePositionAtInput = Console.CursorLeft;

            double[] varReturns = new double[numberOfVariables];



            for (int i = 1; i <= numberOfVariables; i++)
            {
                string k = Console.ReadLine();

                double id = 0.0;
                while (!double.TryParse(k, out id))
                {
                    Display.writeAt(consolePositionAtInput, y, "Invalid input, please enter a number");
                    Console.ReadKey();
                    Display.writeAt(consolePositionAtInput, y, "                                     ");
                    Console.SetCursorPosition(consolePositionAtInput, y);
                    k = Console.ReadLine();
                }

                varReturns[i - 1] = double.Parse(k);


                if (i == numberOfVariables)
                {
                    Display.writeAt(consolePositionAtInput + k.Length, y, ")");
                }
                else
                {
                    Display.writeAt(consolePositionAtInput + k.Length, y, ", ");
                }

                consolePositionAtInput += k.Length + 2;

            }

            return varReturns;
        }
    }
}
