using System;
using System.Threading;

namespace Naovigate.Util
{
    /// <summary>
    /// A class that helps the developers maintain a readable log of the program's execution.
    /// </summary>
    public static class Logger
    {
        private static readonly string Format = "{0} [{1}] {2} :: {3} :: {4}";
        private static readonly string LogFilePath = "log.txt";
        private static readonly string DefaultInvokerName = "Token";

        private static int id = 0;

        static Logger()
        {
            Enabled = true;
            LogToFile = true;
        }

        /// <summary>
        /// Clears the contents of the log file and resets the internal output ID.
        /// </summary>
        public static void Clear()
        {
            lock (LogFilePath)
                System.IO.File.WriteAllText(LogFilePath, "");
            id = 0;
        }

        /// <summary>
        /// Logs given invoker & message.
        /// </summary>
        /// <param name="invoker">The name under which the message should be logged.</param>
        /// <param name="messageObject">An object to log.</param>
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
            string tname = Thread.CurrentThread.Name;
            string formatted = String.Format(Format, id++, time, tname == null ? "Thread" : tname, invoker, message);
            Console.WriteLine(formatted);
            if (LogToFile)
                lock (LogFilePath)
                    System.IO.File.AppendAllText(LogFilePath, formatted + "\n");
        }

        /// <summary>
        /// Logs given message under given type.
        /// </summary>
        /// <param name="t">The type under which the message should be logged.</param>
        /// <param name="messageObject">An object to log.</param>
        public static void Log(Type t, Object messageObject)
        {
            Log(t.Name, messageObject);
        }

        /// <summary>
        /// Logs given message under given object type.
        /// </summary>
        /// <param name="o">The object under which the message should be logged.</param>
        /// <param name="messageObject">An object to log.</param>
        public static void Log(Object o, Object messageObject)
        {
            Log(o.GetType(), messageObject);
        }

        /// <summary>
        /// Logs given message under a token invoker.
        /// </summary>
        /// <param name="messageObject">An object to log.</param>
        public static void Log(Object messageObject)
        {
            Log(DefaultInvokerName, messageObject);
        }

        public static void Except(Exception e)
        {
            string msg = "Exception " + e.GetType().Name;
            Log(e);
            if (NaoState.Instance.Connected)
                Proxies.GetProxy<Aldebaran.Proxies.TextToSpeechProxy>().say(msg);
        }

        /// <summary>
        /// Logs a ping.
        /// </summary>
        public static void Log()
        {
            Log(typeof(Logger), "Ping!");
        }

        /// <summary>
        /// The logger will only output logs when enabled.
        /// </summary>
        public static bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// The logger will output logs into a file when enabled.
        /// </summary>
        public static bool LogToFile
        {
            get;
            set;
        }
    }
}
