using Cysharp.Threading.Tasks;
using DG.Tweening;
using Extensions;
using UnityEngine;

namespace UI.Core
{
    public abstract class UIScreenBase : MonoBehaviour
    {
        private CanvasGroup _cg;

        public virtual async UniTask GetReadyAsync()
        {
            await UniTask.CompletedTask;
        }

        public virtual void Awake()
        {
            _cg = GetComponent<CanvasGroup>();
            _cg.alpha = 0;
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
        }

        public virtual async UniTask ShowAsync()
        {
            _cg.interactable = true;
            _cg.blocksRaycasts = true;
            await _cg.DOFade(1f, 0.25f).ToUniTask();
        }

        public virtual async UniTask HideAsync()
        {
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
            await _cg.DOFade(0f, 0.25f).ToUniTask();
        }
    }
}