using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WebApplication1.Interfaces;

namespace biZTrack.Static
{
    public static class LogHandler
    {
        public static void WriteToLog(string exceptionMsg, string methodName)
        {
            try
            {
                DateTime now = DateTime.Now;

                string folderPath = @"C:\Users\LENOVO\Documents\AutoCareLogs";
                string filePath = Path.Combine(folderPath, "ExceptionLogs.txt");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string message = $"{now:MM/dd/yyyy HH:mm:ss} ~ {methodName} ~ {exceptionMsg};";

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                // Last-resort fallback: swallow log errors so signup doesn’t break
                System.Diagnostics.Debug.WriteLine("Log failed: " + ex.Message);
            }
        }

    }
}