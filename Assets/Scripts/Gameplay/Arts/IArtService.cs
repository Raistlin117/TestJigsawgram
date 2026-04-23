using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Arts
{
    public interface IArtService
    {
        UniTask<Sprite> GetSpriteAsync(AssetReferenceSprite reference);
        Sprite GetCachedSprite(string assetGuid);
        void ReleaseAll();
    }
}
