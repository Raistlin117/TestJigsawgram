using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;

namespace UI.Core
{
    public sealed class ScreenService : IScreenService
    {
        private readonly UIStack _stack;
        private readonly IObjectResolver _resolver;
        private readonly Stack<(GameObject go, AsyncOperationHandle handle)> _cache = new();

        [Inject]
        public ScreenService(UIStack stack, IObjectResolver resolver)
        {
            _stack = stack;
            _resolver = resolver;
        }

        public async UniTask ShowAsync(string key)
        {
            var (screenGo, h, screen) = await LoadScreen(key);

            await _stack.PushAsync(screen);
            await screen.GetReadyAsync();

            _cache.Push((screenGo, h));
        }

        public async UniTask ReplaceAsync(string key)
        {
            await CloseAllAsync();
            await ShowAsync(key);
        }

        public async UniTask CloseTopAsync()
        {
            if (_cache.Count == 0) return;

            var (go, h) = _cache.Pop();

            await _stack.CloseTopAsync();

            Addressables.ReleaseInstance(h);
        }

        public async UniTask CloseAllAsync()
        {
            while (_cache.Count > 0) await CloseTopAsync();
        }

        private async UniTask<(GameObject go, AsyncOperationHandle handle, UIScreenBase screen)> LoadScreen(string key)
        {
            var handle = Addressables.InstantiateAsync(key);
            var go = await handle.Task.AsUniTask();

            _resolver.InjectGameObject(go);

            var screen = go.GetComponent<UIScreenBase>();

            if (screen == null) throw new Exception($"UIScreenBase not found on {key}");

            return (go, handle, screen);
        }
    }
}