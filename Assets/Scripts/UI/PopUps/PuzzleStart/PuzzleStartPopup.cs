using System;
using Core.Audio;
using Cysharp.Threading.Tasks;
using Gameplay;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.PopUps.PuzzleStart
{
    public class PuzzleStartPopup : PopupBase
    {
        [SerializeField] private Image _preview;
        [SerializeField] private SliceOptionView[] _sliceOptions;
        [SerializeField] private Button _startBtn;
        [SerializeField] private TMP_Text _startBtnText;
        [SerializeField] private Button _closeButton;

        private PuzzleStartPresenter _presenter;
        private OverlayManager _overlayManager;
        private AudioService _audio;

        [Inject]
        public void Construct(PuzzleStartPresenter presenter, OverlayManager overlayManager, AudioService audio)
        {
            _presenter = presenter;
            _overlayManager = overlayManager;
            _audio = audio;
        }

        public override async UniTask ShowAsync(PopupArgs args)
        {
            var data = (PuzzleStartArgs)args;
            _preview.sprite = data.Preview;
            _presenter.Configure(data.Config);

            _startBtnText.text = _presenter.IsFree ? "Play" : _presenter.GetCost().ToString();
            _startBtn.interactable = _presenter.CanAfford();

            InitSliceOptions();

            _startBtn.onClick.AddListener(OnStartClicked);
            _closeButton.onClick.AddListener(OnCloseClicked);

            await base.ShowAsync(args);
        }

        public override async UniTask HideAsync()
        {
            _startBtn.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();

            if (_sliceOptions != null)
            {
                foreach (var o in _sliceOptions)
                {
                    if (o != null) o.OnSelected -= HandleSliceSelected;
                }

            }
            await base.HideAsync();
        }

        private void OnStartClicked()
        {
            _audio.Play(AudioId.Click);
            _startBtn.interactable = false;
            _closeButton.interactable = false;
            _presenter.StartAsync(destroyCancellationToken).Forget();
        }

        private void OnCloseClicked()
        {
            _audio.Play(AudioId.Click);
            _overlayManager.CloseTopAsync().Forget();
        }

        private void InitSliceOptions()
        {
            if (_sliceOptions == null || _sliceOptions.Length == 0) return;
            foreach (var o in _sliceOptions)
            {
                o.SetSelected(false);
                o.OnSelected += HandleSliceSelected;
            }

            HandleSliceSelected(_sliceOptions[0]);
        }

        private void HandleSliceSelected(SliceOptionView selected)
        {
            foreach (var o in _sliceOptions) o.SetSelected(o == selected);

            int cols = (int)Math.Round(Math.Sqrt(selected.TotalPieces));
            _presenter.SelectSlice(cols, cols);
        }
    }
}