using System;

namespace Core.Progress
{
    public class ProgressService : IProgressService
    {
        private static int StableHash(string id)
        {
            return Math.Abs(id.GetHashCode());
        }

        public bool IsStarted(string id)
        {
            return StableHash(id) % 3 == 0;
        }

        public bool IsLocked(string id)
        {
            return !IsStarted(id) && StableHash(id) % 2 == 0;
        }

        public float GetCompletionPercent(string id)
        {
            return IsStarted(id) ? (StableHash(id) % 100) / 100f : 0f;
        }
    }
}