using Core.Audio;
using Cysharp.Threading.Tasks;
using UI.PopUps.PuzzleStart;
using UI.Core;
using UI.Popups;
using UnityEngine;
using VContainer;

namespace UI.Screens.MainMenu
{
    public sealed class MainMenuScreen : UIScreenBase
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GridAdaptiveView _gridAdaptiveView;
        [SerializeField] private MenuPuzzleView _puzzleViewPrefab;

        private MainMenuViewModel _viewModel;
        private OverlayManager _overlayManager;
        private AudioService _audio;

        [Inject]
        public void Construct(MainMenuViewModel viewModel, OverlayManager overlayManager, AudioService audio)
        {
            _viewModel = viewModel;
            _overlayManager = overlayManager;
            _audio = audio;
        }

        public override async UniTask GetReadyAsync()
        {
            RefreshLayout();

            await LoadPuzzlesAsync();

            _audio.PlayMusic(AudioId.Background);
        }

        private void RefreshLayout()
        {
            var settings = _viewModel.GetGridSettings(Screen.width, Screen.height);

            _gridAdaptiveView.Apply(settings);
        }

        private async UniTask LoadPuzzlesAsync()
        {
            var items = await _viewModel.LoadItemsAsync();

            foreach (var data in items)
            {
                var view = Instantiate(_puzzleViewPrefab, _contentRoot);

                view.Apply(data);
                view.OnSelected += HandlePuzzleSelected;
            }
        }

        private void HandlePuzzleSelected(string artId)
        {
            _audio.Play(AudioId.Click);

            var args = new PuzzleStartArgs
            {
                Config = _viewModel.GetConfig(artId),
                Preview = _viewModel.GetCachedSprite(artId)
            };

            _overlayManager.ShowAsync(UIScreenKeys.PuzzleStartPopup, args).Forget();
        }
    }
}