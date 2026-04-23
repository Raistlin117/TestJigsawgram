using R3;

namespace Core.Progress
{
    public interface ICurrencyService
    {
        ReadOnlyReactiveProperty<int> SoftCurrency { get; }

        void AddCurrency(int amount);
        void SpendCurrency(int amount);
    }
}
