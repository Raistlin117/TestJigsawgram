using System;
using System.Collections.Generic;
using Core.Progress;
using Cysharp.Threading.Tasks;
using Gameplay.Arts;
using UnityEngine;

namespace UI.Screens.MainMenu
{
    public class MainMenuViewModel : IDisposable
    {
        private readonly IArtRepository _artRepository;
        private readonly IArtService _artService;
        private readonly IProgressService _progressService;
        private readonly IGridCalculator _gridCalculator;
        private readonly Dictionary<string, Sprite> _spriteCache = new();

        public MainMenuViewModel(IArtRepository artRepository, IArtService artService, IProgressService progressService,
            IGridCalculator gridCalculator)
        {
            _artRepository = artRepository;
            _artService = artService;
            _progressService = progressService;
            _gridCalculator = gridCalculator;
        }

        public GridSettings GetGridSettings(float screenWidth, float screenHeight)
        {
            return _gridCalculator.Calculate(screenWidth, screenHeight);
        }

        public async UniTask<IReadOnlyList<MenuPuzzleViewData>> LoadItemsAsync()
        {
            var allArts = _artRepository.GetAll();
            var sprites = await UniTask.WhenAll(allArts.Select(a => _artService.GetSpriteAsync(a.Sprite)));
            var result = new List<MenuPuzzleViewData>(allArts.Length);

            for (int i = 0; i < allArts.Length; i++)
            {
                var config = allArts[i];

                _spriteCache[config.Id] = sprites[i];

                result.Add(new MenuPuzzleViewData
                {
                    ArtId = config.Id,
                    ArtSprite = sprites[i],
                    Started = _progressService.IsStarted(config.Id),
                    Locked = _progressService.IsLocked(config.Id),
                    ProgressBar = _progressService.GetCompletionPercent(config.Id),
                    PuzzleSliceCount = config.Cost > 0 ? config.Cost : 400,
                });
            }

            return result;
        }

        public Sprite GetCachedSprite(string artId)
        {
            return _spriteCache.TryGetValue(artId, out var s) ? s : null;
        }

        public ArtConfig GetConfig(string artId)
        {
            return _artRepository.GetArtConfigById(artId);
        }

        public void Dispose() => _spriteCache.Clear();
    }
}