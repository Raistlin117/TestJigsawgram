using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneLoading;
using VContainer.Unity;

namespace Core.Bootstrap
{
    public sealed class BootstrapEntry : IAsyncStartable
    {
        private readonly ISceneLoader _sceneLoader;

        public BootstrapEntry(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask StartAsync(CancellationToken ct)
        {
            await _sceneLoader.InitializeAsync(ct);

            await _sceneLoader.LoadAddressableSceneAsync(SceneKeys.Menu, true, ct);

            _sceneLoader.UnloadSceneAsync(SceneKeys.Bootstrap);
        }
    }
}