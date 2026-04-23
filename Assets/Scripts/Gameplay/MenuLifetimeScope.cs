using Core.Progress;
using Gameplay.Arts;
using UI.Core;
using UI.Popups;
using UI.Screens.MainMenu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class MenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private ArtArchive _artArchive;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntry>();

            builder.RegisterInstance(_artArchive).As<IArtRepository>();

            builder.Register<ScreenService>(Lifetime.Scoped).As<IScreenService>();
            builder.Register<ArtService>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ProgressService>(Lifetime.Scoped).As<IProgressService>();
            builder.Register<CurrencyService>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<MenuGridCalculator>(Lifetime.Scoped).As<IGridCalculator>();
            builder.Register<HeaderViewModel>(Lifetime.Scoped);
            builder.Register<MainMenuViewModel>(Lifetime.Scoped);
            builder.Register<PuzzleStartPresenter>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<OverlayManager>();
            builder.RegisterComponentInHierarchy<HeaderView>();
            builder.RegisterComponentInHierarchy<UIStack>();
        }
    }
}
