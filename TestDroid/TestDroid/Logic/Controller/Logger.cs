using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestDroid.Logic.Controller
{
    class Logger
    {
        private static Logger instance;
        private ArrayAdapter logAdapter;

        private Logger(ArrayAdapter adapter)
        {
            logAdapter = adapter;
        }

        /// <summary>
        /// Get singleton insance of Logger
        /// </summary>
        /// <returns>Logger</returns>
        public static Logger GetInstance(ArrayAdapter adapter = null)
        {
            if(instance == null)
            {
                instance = new Logger(adapter);
            }
            return instance;
        }

        /// <summary>
        /// Log an event to display on the screen. Includes timestamp and loglevel
        /// </summary>
        /// <param name="data">The even to log</param>
        /// <param name="level">
        /// The level of the log:
        /// 0: Info
        /// 1: Debug
        /// 2: Warning
        /// 3: Error
        /// 4: Fatal
        /// </param>
        public void LogEvent(string logEvent, int level = 0)
        {
            string logLevel = "";

            switch (level)
            {
                case 0: logLevel = "[Info]";
                    break;
                case 1: logLevel = "[Debug]";
                    break;
                case 2: logLevel = "[Warning]";
                    break;
                case 3: logLevel = "[Error]";
                    break;
                case 4: logLevel = "[FATAL]";
                    break;
                default:
                    break;
            }
            logLevel += ": ";

            string timestamp = "[" + DateTime.Now.ToShortTimeString() + "] ";
            
            string logMessage = timestamp + logLevel + logEvent;

            try
            {
                logAdapter.Add(logMessage);
            }
            catch (Exception)
            {
                
            }
        }
    }
}