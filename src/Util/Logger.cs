using System;
using System.Text;

namespace Naovigate.Util
{
    /// <summary>
    /// A class that helps the developers maintain a readable log of the program's execution.
    /// </summary>
    internal static class Logger
    {
        private static readonly string Format = "{0} {1} :: {2} :: {3}";
        private static readonly string DefaultInvokerName = "Token";
        private static readonly string logFilePath = "log.txt";

        private static int id = 0;
        private static bool enabled = true;
        private static bool logToFile = true;

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
        /// Clears the contents of the log file.
        /// </summary>
        public static void Clear()
        {
            lock (logFilePath)
                System.IO.File.WriteAllText(logFilePath, "");
        }

        /// <summary>
        /// Logs given invoker & amp; message.
        /// </summary>
        /// <param name="invoker">The name under which the message should be logged.</param>
        /// <param name="messageObject">A string to log.</param>
        public static void Log(string invoker, Object messageObject)
        {
            if (!Enabled)
                return;
            
            string time = DateTime.Now.ToLongTimeString();
            string message = messageObject.ToString();
            if (message.Contains("\n"))
            {
                message = message.Insert(0, "\n");
                message = message.Replace("\n", "\n\t");
            }
            string formatted = String.Format(Format, id++, time, invoker, message);
            Console.WriteLine(formatted);
            if (logToFile)
                lock (logFilePath)
                    System.IO.File.AppendAllText(logFilePath, formatted + "\n");
        }

        /// <summary>
        /// Logs given message under given type.
        /// </summary>
        /// <param name="t">The type under which the message should be logged.</param>
        /// <param name="messageObject">A string to log.</param>
        public static void Log(Type t, Object messageObject)
        {
            Log(t.Name, messageObject);
        }

        /// <summary>
        /// Logs given message under given object type.
        /// </summary>
        /// <param name="o">The object under which the message should be logged.</param>
        /// <param name="messageObject">A string to log.</param>
        public static void Log(Object o, Object messageObject)
        {
            Log(o.GetType(), messageObject);
        }

        /// <summary>
        /// Logs given message under a token invoker.
        /// </summary>
        /// <param name="messageObject">A string to log.</param>
        public static void Log(Object messageObject)
        {
            Log(DefaultInvokerName, messageObject);
        }

        /// <summary>
        /// The logger will only output logs when enabled.
        /// </summary>
        public static bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// The logger will output logs in to a file when enabled.
        /// </summary>
        public static bool LogToFile
        {
            get { return logToFile; }
            set { logToFile = value; }
        }
    }
}
