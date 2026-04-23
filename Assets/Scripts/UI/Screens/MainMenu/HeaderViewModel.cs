using System;
using Core.Progress;
using R3;

namespace UI.Screens.MainMenu
{
    public class HeaderViewModel : IDisposable
    {
        private DisposableBag _disposables;

        public ReadOnlyReactiveProperty<string> SoftCurrencyAmount { get; }

        public HeaderViewModel(ICurrencyService currencyService)
        {
            SoftCurrencyAmount = currencyService.SoftCurrency
                .Select(x => x.ToString("N0"))
                .ToReadOnlyReactiveProperty()
                .AddTo(ref _disposables);
        }

        public void Dispose() => _disposables.Dispose();
    }
}