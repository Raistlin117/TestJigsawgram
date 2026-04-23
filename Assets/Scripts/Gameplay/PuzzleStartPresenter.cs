using System.Threading;
using Core.Progress;
using Cysharp.Threading.Tasks;
using Gameplay.Arts;
using Gameplay.Puzzle;
using Infrastructure.SceneLoading;
using UI.Popups;

namespace Gameplay
{
    public class PuzzleStartPresenter
    {
        private readonly ICurrencyService _currencyService;
        private readonly PuzzleSession _session;
        private readonly OverlayManager _overlayManager;
        private readonly ISceneLoader _sceneLoader;

        private ArtConfig _config;

        private int _selectedColumns = 6;
        private int _selectedRows = 6;

        public PuzzleStartPresenter(ICurrencyService currencyService, PuzzleSession session,
            OverlayManager overlayManager, ISceneLoader sceneLoader)
        {
            _currencyService = currencyService;
            _session = session;
            _overlayManager = overlayManager;
            _sceneLoader = sceneLoader;
        }

        public void Configure(ArtConfig config)
        {
            _config = config;
        }

        public void SelectSlice(int columns, int rows)
        {
            _selectedColumns = columns;
            _selectedRows = rows;
        }

        public bool IsFree => _config?.Cost == 0;

        public int GetCost()
        {
            return _config?.Cost ?? 0;
        }

        public bool CanAfford()
        {
            return IsFree || _currencyService.SoftCurrency.CurrentValue >= GetCost();
        }

        public async UniTask StartAsync(CancellationToken ct = default)
        {
            if (!IsFree) _currencyService.SpendCurrency(GetCost());

            _session.Configure(_config, _selectedColumns, _selectedRows);

            await _overlayManager.CloseAllAsync();
            await _sceneLoader.LoadAddressableSceneAsync(SceneKeys.Gameplay, false, ct);
        }
    }
}