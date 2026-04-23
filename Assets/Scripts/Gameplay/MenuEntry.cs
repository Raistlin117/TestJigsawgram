using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneLoading;
using UI.Core;
using VContainer.Unity;

namespace Gameplay
{
    public sealed class MenuEntry : IAsyncStartable
    {
        private IScreenService _screenService;

        public MenuEntry(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
        {
            await _screenService.ShowAsync(UIScreenKeys.MainMenu);

            if (!DDOLCleaner.Captured)
                DDOLCleaner.CaptureBaseline();
            else
                DDOLCleaner.CleanExtras();
        }
    }
}