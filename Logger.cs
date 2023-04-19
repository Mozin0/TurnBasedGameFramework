using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary
{
    public class Logger
    {
        private static TraceSource _traceSource;
        private static int _id;

        static Logger()
        {
            _traceSource = new TraceSource("MyTraceSource");
            _traceSource.Switch = new SourceSwitch("MySwitch", "All");

            _traceSource.Listeners.Add(new ConsoleTraceListener());
            
            _traceSource.Close();
        }

        /// <summary>
        /// Writes an informational message to the console.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _traceSource.TraceEvent(TraceEventType.Information, _id, message);
            _id++;
            _traceSource.Flush();
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a warning message to the console.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            _traceSource.TraceEvent(TraceEventType.Warning, _id, message);
            _id++;
            _traceSource.Flush();
            Console.ResetColor();
        }

        /// <summary>
        /// Writes an error message to the console.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            _traceSource.TraceEvent(TraceEventType.Error, _id, message);
            _id++;
            _traceSource.Flush();
            Console.ResetColor();
        }
    }
}