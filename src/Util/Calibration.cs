
using System;
using System.Collections.Generic;
namespace Naovigate.Util
{
    /// <summary>
    /// A class that loads and serves Nao-specific calibration records to other classes.
    /// </summary>
    internal class Calibration
    {
        private static readonly string DefaultPath = "../resources/calibs/default.naocalib";
        private static Calibration instance;

        /// <summary>
        /// This singleton's instance.
        /// </summary>
        public static Calibration Instance
        {
            get { return instance == null ? instance = new Calibration(DefaultPath) : instance; }
            set { instance = value; }
        }

        private string path;
        private Dictionary<string, object> records;

        /// <summary>
        /// Creates a new calibration instance using given file.
        /// </summary>
        /// <param name="path">The path to the calibration file.</param>
        public Calibration(string path)
        {
            this.path = path;
            records = new Dictionary<string, object>();
            Load();
        }

        /// <summary>
        /// The path to the loaded calibration file.
        /// </summary>
        public string Path
        {
            get { return path; }
        }

        /// <summary>
        /// Loads the records contained in the calibration file into this instance.
        /// </summary>
        protected void Load()
        {
            string[] lines;
            string[] words;
            try
            {
                lines = System.IO.File.ReadAllLines(path);
            }
            catch
            {
                Logger.Log(this, "Calibration file path does not exist: " + path);
                return;
            }

            foreach (string line in lines)
            {
                words = line.Split(' ');
                if (words.Length > 1)
                    records.Add(words[0], words[1]);
            }
            Logger.Log(this, "Calibration file was loaded succesfully.");
        }

        /// <summary>
        /// Retrieves a record.
        /// </summary>
        /// <typeparam name="T">The desired result type.</typeparam>
        /// <param name="key">The record's key.</param>
        /// <returns>The record's value.</returns>
        public T GetRecord<T>(string key)
        {
            if (records.ContainsKey(key))
                return (T)records[key];
            else
                return default(T);
        }
    }
}
