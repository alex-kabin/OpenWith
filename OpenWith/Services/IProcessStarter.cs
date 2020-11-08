using System.Diagnostics;

namespace OpenWith.Services
{
    public interface IProcessStarter
    {
        Process Start(string filePath, bool asAdmin, string workDir, string argsTemplate, string[] args);
    }
}
