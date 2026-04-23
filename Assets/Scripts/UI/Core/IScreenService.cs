using Cysharp.Threading.Tasks;

namespace UI.Core
{
    public interface IScreenService
    {
        UniTask ShowAsync(string key);
        UniTask ReplaceAsync(string key);
        UniTask CloseTopAsync();
        UniTask CloseAllAsync();
    }
}