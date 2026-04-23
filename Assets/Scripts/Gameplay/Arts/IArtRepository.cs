namespace Gameplay.Arts
{
    public interface IArtRepository
    {
        ArtConfig   GetArtConfigById(string id);
        ArtConfig[] GetAllByCategory(ArtCategory category);
        ArtConfig[] GetAll();
    }
}