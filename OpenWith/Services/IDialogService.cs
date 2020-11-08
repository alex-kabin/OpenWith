namespace OpenWith.Services
{
    public interface IDialogService
    {
        void Info(string text, string caption = "Information");
        void Warn(string text, string caption = "Warning");
        void Error(string text, string caption = "Error");
    }
}
