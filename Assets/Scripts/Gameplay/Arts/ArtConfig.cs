using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Arts
{
    [CreateAssetMenu(fileName = "ArtConfig", menuName = "Configs/Art Config")]
    public class ArtConfig : ScriptableObject
    {
        public string Id;
        public AssetReferenceSprite Sprite;
        public int Cost;
        public ArtCategory Category;
    }
}