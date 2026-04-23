namespace Core.Progress
{
    public interface IProgressService
    {
        bool IsStarted(string id);
        bool IsLocked(string id);
        float GetCompletionPercent(string id);
    }
}