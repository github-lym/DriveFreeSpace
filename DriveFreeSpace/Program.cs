using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DriveFreeSpace
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            float gb = 0.000f;

            string logName;
            string logFullPath;
            StreamWriter log;

            string pathNow = Directory.GetCurrentDirectory();
            string now = DateTime.Now.ToString("yyyyMMdd");
            logName = now + ".log";   //以當時為名的log

            if (!System.IO.Directory.Exists("DriveFreeSpace_log"))
                System.IO.Directory.CreateDirectory("DriveFreeSpace_log");  //沒有資料夾就產生

            logFullPath = Path.Combine("DriveFreeSpace_log", logName);
            if (!File.Exists(logFullPath))
                log = new StreamWriter(logFullPath);
            else
                log = File.AppendText(logFullPath);

            log.WriteLine("log產生時間{0}", DateTime.Now.ToString());
            log.Flush();
            log.Close();
            log.Dispose();

            foreach (DriveInfo d in allDrives)
            {
                if (!File.Exists(logFullPath))
                    log = new StreamWriter(logFullPath);
                else
                    log = File.AppendText(logFullPath);

                log.WriteLine("Drive {0}", d.Name);
                log.WriteLine("  Drive type: {0}", d.DriveType);

                if (d.IsReady == true)
                {
                    log.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    log.WriteLine("  File system: {0}", d.DriveFormat);

                    gb = (float)(d.TotalSize / 1073741824d);
                    log.WriteLine(
                        "  Total size of drive:            {0, 15} GBs",
                        gb);
                    gb = (float)((d.TotalSize - d.AvailableFreeSpace) / 1073741824d);

                    log.WriteLine(
                        "  Used space:                     {0, 15} GBs",
                        gb);

                    gb = (float)(d.TotalFreeSpace / 1073741824d);
                    log.WriteLine(
                        "  Total available space:          {0, 15} GBs",
                        gb);
                    log.WriteLine("");

                    #region example

                    /*
                    gb = (float)(d.TotalSize / 1073741824d);
                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} GBs",
                        gb);
                    gb = (float)((d.TotalSize - d.AvailableFreeSpace) / 1073741824d);

                    Console.WriteLine(
                        "  Used space:                     {0, 15} GBs",
                        gb);

                    gb = (float)(d.TotalFreeSpace / 1073741824d);
                    Console.WriteLine(
                        "  Total available space:          {0, 15} GBs",
                        gb);
                     */

                    #endregion example
                }
                log.Flush();
                log.Close();
                log.Dispose();
            }
            //Console.ReadKey();
        }
    }
}