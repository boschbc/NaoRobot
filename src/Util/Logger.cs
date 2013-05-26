using System;
using System.Text;

namespace Naovigate.Util
{
    /// <summary>
    /// A class that helps the developers maintain a readable log of the program's execution.
    /// </summary>
    internal static class Logger
    {
        private static readonly string Format = "{0} :: {1} :: {2}";
        private static readonly string DefaultInvokerName = "Token";

        /// <summary>
        /// Multiplies given string in an integer and returns the result.
        /// Credit goes to DrJokepu of stackoverflow.com
        /// </summary>
        /// <param name="source">A string to multiply.</param>
        /// <param name="multiplier">An integer.</param>
        /// <returns>The source string repeated as many times as the given multiplier.</returns>
        private static string Multiply(this string source, int multiplier)
        {
            StringBuilder sb = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Logs given invoker & message.
        /// </summary>
        /// <param name="invoker">The name under which the message should be logged.</param>
        /// <param name="message">A string to log.</param>
        public static void Log(string invoker, string message)
        {
            string time = DateTime.Now.ToShortTimeString();
            if (message.Contains("\n"))
            {
                message = message.Insert(0, "\n");
                message = message.Replace("\n", "\n\t");
            }
            Console.WriteLine(String.Format(Format, time, invoker, message));
        }

        /// <summary>
        /// Logs given message under given type.
        /// </summary>
        /// <param name="t">The type under which the message should be logged.</param>
        /// <param name="message">A string to log.</param>
        public static void Log(Type t, string message)
        {
            Log(t.Name, message);
        }

        /// <summary>
        /// Logs given message under given object type.
        /// </summary>
        /// <param name="o">The object under which the message should be logged.</param>
        /// <param name="message">A string to log.</param>
        public static void Log(Object o, string message)
        {
            Log(o.GetType(), message);
        }

        /// <summary>
        /// Logs given message under a token invoker.
        /// </summary>
        /// <param name="message">A string to log.</param>
        public static void Log(string message)
        {
            Log(DefaultInvokerName, message);
        }
    }
}
