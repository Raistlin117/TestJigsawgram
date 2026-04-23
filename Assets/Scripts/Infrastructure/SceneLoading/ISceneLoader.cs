using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneLoading
{
    public interface ISceneLoader
    {
        UniTask InitializeAsync(CancellationToken ct);
        UniTask LoadAddressableSceneAsync(string key, bool additive, CancellationToken ct);
        UniTask UnloadSceneAsync(string key);
    }
}