using R3;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI.Screens.MainMenu
{
    public class HeaderView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;

        private HeaderViewModel _viewModel;
        private DisposableBag _disposables;

        [Inject]
        public void Construct(HeaderViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            _viewModel.SoftCurrencyAmount
                .Subscribe(text => _currencyText.text = text)
                .AddTo(ref _disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}