
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

        public static Calibration Instance
        {
            get { return instance == null ? instance = new Calibration(DefaultPath) : instance; }
            set { instance = value; }
        }

        public static bool Initialized
        {
            get { return instance != null; }
        }

        private string path;
        private Dictionary<string, object> records;

        public Calibration(string path)
        {
            this.path = path;
            records = new Dictionary<string, object>();
            Load();
        }

        public string Path
        {
            get { return path; }
        }

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
                words = line.Split(new string[]{" "}, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 1)
                    records.Add(words[0], words[1]);
            }
            Logger.Log(this, "Calibration file was loaded succesfully.");
        }

        public T GetRecord<T>(string key)
        {
            if (records.ContainsKey(key))
                return (T)records[key];
            else
            {
                Logger.Log(this, "No such entry: " + key);
                return default(T);
            }
        }
    }
}
