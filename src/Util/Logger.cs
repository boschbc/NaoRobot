using System;
using System.IO;
using System.Threading;

namespace Naovigate.Util
{
    /// <summary>
    /// A class that helps the developers maintain a readable log of the program's execution.
    /// </summary>
    public static class Logger
    {
        private static readonly string Format = "{0} [{1}] {2} :: {3} :: {4}";
        private static readonly string LogFilePath = InitLogFilePath();
        private static readonly string DefaultInvokerName = "Token";

        private static int id = 0;

        private static string InitLogFilePath()
        {
            int suffix = 0;
            while (File.Exists("log" + suffix + ".txt"))
                suffix++;
            return "log" + suffix + ".txt";
        }

        static Logger()
        {
            Enabled = true;
            LogToFile = true;
            CanSay = true;
        }

        /// <summary>
        /// Clears the contents of the log file and resets the internal output ID.
        /// </summary>
        public static void Clear()
        {
            lock (LogFilePath)
                File.WriteAllText(LogFilePath, "");
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
                    File.AppendAllText(LogFilePath, formatted + "\n");
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

        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="e">An exception to log.</param>
        public static void Except(Exception e)
        {
            Say("Exception " + e.GetType().Name+" "+e.Message);
        }

        /// <summary>
        /// speak out a message. the message is also logged.
        /// </summary>
        /// <param name="message"></param>
        public static void Say(string message)
        {
            Log(message);
            if (CanSay && NaoState.Instance.Connected)
                Proxies.GetProxy<Aldebaran.Proxies.TextToSpeechProxy>().say(message);
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

        /// <summary>
        /// The Nao is allowed to speak commands from Logger.Say()
        /// </summary>
        public static bool CanSay
        {
            get;
            set;
        }
    }

    public static class LogExtensions
    {
        /// <summary>
        /// Returns (at max) the first 5 charachters of a floating point number as a string.
        /// return "small" if the number contains an E.
        /// </summary>
        /// <param name="f">A floating point number.</param>
        /// <returns>A string of length 5 (at max).</returns>
        public static string Readable(this float f)
        {
            string res = f.ToString();
            if (res.Contains("E")) return "small";
            if (res.Length < 5) return res;
            else return res.Substring(0, 5);
        }

        public static object Log(this object o, object type)
        {
            Logger.Log(type, o);
            return o;
        }

        public static void Say(this object msg)
        {
            Logger.Say(msg.ToString());
        }
    }
}
