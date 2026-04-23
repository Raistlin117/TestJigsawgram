using System.Linq;
using UnityEngine;

namespace Gameplay.Arts
{
    [CreateAssetMenu(fileName = "ArtArchive", menuName = "Configs/Art Archive", order = 0)]
    public class ArtArchive : ScriptableObject, IArtRepository
    {
        [SerializeField] private ArtConfig[] _artConfigs;

        public ArtConfig GetArtConfigById(string id)
        {
            return _artConfigs.SingleOrDefault(x => x.Id == id);
        }

        public ArtConfig[] GetAllByCategory(ArtCategory category)
        {
            return _artConfigs.Where(x => x.Category == category).ToArray();
        }

        public ArtConfig[] GetAll()
        {
            return _artConfigs;
        }
    }
}