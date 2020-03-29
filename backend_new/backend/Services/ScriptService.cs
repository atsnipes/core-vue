using System;
using System.Diagnostics;

namespace backend.Services
{
    public class ScriptService : IScriptService
    {
        private static string _scriptDir = @".\scripts\iicDriver.sh";

        public bool callScript
        {
            get
            {
                bool success = false;
                using (var process = new Process())
                {
                    process.StartInfo.FileName = _scriptDir;
                    process.StartInfo.Arguments = $"hi there dude";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    Console.WriteLine("starting");
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    success = process.WaitForExit(1000 * 10);     // (optional) wait up to 10 seconds
                    Console.WriteLine($"exited w/ {success}");
                }

                return success;
            }
        }
    }
}