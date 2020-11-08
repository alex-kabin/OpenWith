using System;
using System.Diagnostics;
using System.Text;

namespace OpenWith.Services.impl
{
    public class DefaultProcessStarter : IProcessStarter
    {
        public Process Start(string filePath, bool asAdmin, string workDir, string argsTemplate, string[] args) {
            string arguments = string.Empty;
            if (!string.IsNullOrWhiteSpace(argsTemplate) && args != null && args.Length > 0) {
                StringBuilder sb = new StringBuilder(argsTemplate);
                for (int i = 0; i < args.Length; i++) {
                    sb.Replace($"%{i}", args[i]);
                }
                arguments = sb.ToString();
            }

            var startInfo = new ProcessStartInfo(filePath, arguments) {
                WorkingDirectory = workDir,
            };

            if (asAdmin) {
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
            }

            Debug.WriteLine($"Starting '{filePath}'");

            return Process.Start(startInfo);
        }
    }
}
