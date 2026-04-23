using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Infrastructure.SceneLoading
{
    public sealed class AddressableSceneLoader : ISceneLoader
    {
        private readonly Dictionary<string, SceneInstance> _loadedScenes = new();
        private LifetimeScope _lifetimeScope;

        public AddressableSceneLoader(LifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public UniTask InitializeAsync(CancellationToken ct)
        {
            return Addressables.InitializeAsync().ToUniTask(cancellationToken: ct);
        }

        public async UniTask LoadAddressableSceneAsync(string key, bool additive, CancellationToken ct)
        {
            var mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;

            using (LifetimeScope.EnqueueParent(_lifetimeScope))
            {
                var handle = Addressables.LoadSceneAsync(key, mode, activateOnLoad: true);
                var sceneInstance = await handle.WithCancellation(ct);
                if (additive) _loadedScenes[key] = sceneInstance;
            }
        }

        public async UniTask UnloadSceneAsync(string key)
        {
            if (_loadedScenes.TryGetValue(key, out var instance))
            {
                await Addressables.UnloadSceneAsync(instance);
                _loadedScenes.Remove(key);
            }
            else if (SceneManager.GetSceneByName(key).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(key);
            }
        }
    }
}