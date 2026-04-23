using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Core
{
    public sealed class UIStack : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;

        private readonly Stack<UIScreenBase> _stack = new();

        public async UniTask PushAsync(UIScreenBase screen)
        {
            screen.transform.SetParent(_root, false);
            _stack.Push(screen);

            await screen.ShowAsync();
        }

        public async UniTask CloseTopAsync()
        {
            if (_stack.Count == 0) return;

            var top = _stack.Pop();
            await top.HideAsync();

            Destroy(top.gameObject);
        }

        public async UniTask ReplaceAsync(UIScreenBase prefab)
        {
            while (_stack.Count > 0) await CloseTopAsync();

            await PushAsync(prefab);
        }
    }
}