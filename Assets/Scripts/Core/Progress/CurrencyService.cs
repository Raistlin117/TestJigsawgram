using R3;
using UnityEngine;

namespace Core.Progress
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ReactiveProperty<int> _softCurrency;
        public ReadOnlyReactiveProperty<int> SoftCurrency => _softCurrency;

        public CurrencyService()
        {
            _softCurrency = new ReactiveProperty<int>(Random.Range(2000, 99001));
        }

        public void AddCurrency(int amount)
        {
            _softCurrency.Value += amount;
        }

        public void SpendCurrency(int amount)
        {
            _softCurrency.Value = Mathf.Max(0, _softCurrency.Value - amount);
        }
    }
}