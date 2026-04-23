using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.SceneLoading
{
    public static class DDOLCleaner
    {
        public static bool Captured { get; private set; }

        static readonly HashSet<int> _whitelist = new(64);
        static readonly List<GameObject> _rootsBuf = new(128);

        public static void CaptureBaseline()
        {
            _whitelist.Clear();
            GetDDOLRoots(_rootsBuf);
            for (int i = 0; i < _rootsBuf.Count; i++)
                _whitelist.Add(_rootsBuf[i].GetInstanceID());
            _rootsBuf.Clear();
            Captured = true;
            Debug.Log($"[DDOL] Baseline captured: {_whitelist.Count} roots");
        }

        public static void CleanExtras(bool unloadUnusedAssets = false)
        {
            if (!Captured) CaptureBaseline();

            GetDDOLRoots(_rootsBuf);
            int removed = 0;
            for (int i = 0; i < _rootsBuf.Count; i++)
            {
                var root = _rootsBuf[i];
                if (!_whitelist.Contains(root.GetInstanceID()))
                {
                    Object.Destroy(root);
                    removed++;
                }
            }

            _rootsBuf.Clear();

            if (unloadUnusedAssets)
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }

            if (removed > 0) Debug.Log($"[DDOL] Removed {removed} extra DDOL roots.");
        }

        static void GetDDOLRoots(List<GameObject> outList)
        {
            outList.Clear();
            var probe = new GameObject("__ddol_probe__");
            Object.DontDestroyOnLoad(probe);
            probe.scene.GetRootGameObjects(outList);
            outList.Remove(probe);
            Object.Destroy(probe);
        }
    }
}