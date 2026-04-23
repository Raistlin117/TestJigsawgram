using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Arts;
using Gameplay.Puzzle;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayEntry : IAsyncStartable
    {
        private readonly PuzzleSession _session;
        private readonly IArtService _artService;
        private readonly PuzzleGenerator _generator;

        public GameplayEntry(PuzzleSession session, IArtService artService, PuzzleGenerator generator)
        {
            _session = session;
            _artService = artService;
            _generator = generator;
        }

        public async UniTask StartAsync(CancellationToken ct)
        {
            var sprite = await _artService.GetSpriteAsync(_session.Config.Sprite);
            _generator.Generate(sprite.texture, _session.Columns, _session.Rows);
        }
    }
}