using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Server
{
    class Program
    {
        private static bool isRunning = false;
        public static Thread mainThread;
        public static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            Server.Start(2, 26950);
        }

        public static void Stop()
        {
            isRunning = false;
            Server.Stop();
            var prc = new ProcManager();
            prc.KillByPort(26950);
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        public class PRC
        {
            public int PID { get; set; }
            public int Port { get; set; }
            public string Protocol { get; set; }
        }
        public class ProcManager
        {
            public void KillByPort(int port)
            {
                var processes = GetAllProcesses();
                if (processes.Any(p => p.Port == port))
                    try
                    {
                        Process.GetProcessById(processes.First(p => p.Port == port).PID).Kill();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                else
                {
                    Console.WriteLine("No process to kill!");
                }
            }

            public List<PRC> GetAllProcesses()
            {
                var pStartInfo = new ProcessStartInfo();
                pStartInfo.FileName = "netstat.exe";
                pStartInfo.Arguments = "-a -n -o";
                pStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                pStartInfo.UseShellExecute = false;
                pStartInfo.RedirectStandardInput = true;
                pStartInfo.RedirectStandardOutput = true;
                pStartInfo.RedirectStandardError = true;

                var process = new Process()
                {
                    StartInfo = pStartInfo
                };
                process.Start();

                var soStream = process.StandardOutput;

                var output = soStream.ReadToEnd();
                if (process.ExitCode != 0)
                    throw new Exception("somethign broke");

                var result = new List<PRC>();

                var lines = Regex.Split(output, "\r\n");
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("Proto"))
                        continue;

                    var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var len = parts.Length;
                    if (len > 2)
                        result.Add(new PRC
                        {
                            Protocol = parts[0],
                            Port = int.Parse(parts[1].Split(':').Last()),
                            PID = int.Parse(parts[len - 1])
                        });


                }
                return result;
            }
        }
    }
}
