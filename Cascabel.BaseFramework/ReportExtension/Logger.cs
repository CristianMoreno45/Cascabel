using System;
using System.Collections.Generic;
using System.Text;

namespace Cascabel.BaseFramework.ReportExtension
{
    /// <summary>
    /// Managing the log
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Write a record to the log
        /// </summary>
        /// <param name="format">A string formatted as string {0}</param>
        /// <param name="args0">String array to replace in format {0}, {1}, ... { n}</param>
        public static void Log(string format, params object[] args0)
        {
            Console.WriteLine("\n" + format, args0);
        }
    }
}
