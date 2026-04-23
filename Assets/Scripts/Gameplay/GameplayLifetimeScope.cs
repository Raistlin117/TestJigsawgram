using Gameplay.Arts;
using Gameplay.Puzzle;
using UI.Screens.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayEntry>();

            builder.Register<ArtService>(Lifetime.Scoped).As<IArtService>();

            builder.RegisterComponentInHierarchy<PuzzleGenerator>();
            builder.RegisterComponentInHierarchy<GameplayHUD>();
        }
    }
}