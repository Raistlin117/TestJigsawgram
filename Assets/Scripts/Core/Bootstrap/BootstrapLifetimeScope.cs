using Core.Audio;
using Gameplay.Puzzle;
using Infrastructure.SceneLoading;
using Plugins.SignalBus;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Bootstrap
{
    public sealed class BootstrapLifetimeScope : LifetimeScope
    {
        [SerializeField] private AudioServiceHost _audioServiceHost;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_audioServiceHost);
            builder.RegisterInstance(SignalHub.Global).AsSelf();

            builder.Register<AudioService>(Lifetime.Singleton);
            builder.Register<PuzzleSession>(Lifetime.Singleton);
            builder.Register<AddressableSceneLoader>(Lifetime.Singleton).As<ISceneLoader>();

            builder.RegisterEntryPoint<BootstrapEntry>();
        }

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}
