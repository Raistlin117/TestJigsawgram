using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;

namespace UI.Popups
{
    public sealed class OverlayManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;

        private readonly Stack<(PopupBase popup, AsyncOperationHandle handle)> _stack = new();

        private IObjectResolver _resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) => _resolver = resolver;

        public async UniTask ShowAsync(string address, PopupArgs args = null)
        {
            var handle = Addressables.InstantiateAsync(address, _root);
            var go = await handle;

            if (go == null) Debug.LogError(go);

            _resolver.InjectGameObject(go);

            var popup = go.GetComponent<PopupBase>();

            await popup.ShowAsync(args);

            _stack.Push((popup, handle));
        }

        public async UniTask CloseTopAsync()
        {
            if (_stack.Count == 0) return;

            var (popup, handle) = _stack.Pop();
            await popup.HideAsync();

            Addressables.ReleaseInstance(handle);
        }

        public async UniTask CloseAllAsync()
        {
            while (_stack.Count > 0)
            {
                await CloseTopAsync();
            }
        }
    }
}