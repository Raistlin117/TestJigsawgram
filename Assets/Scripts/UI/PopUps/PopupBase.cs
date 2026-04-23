using Cysharp.Threading.Tasks;
using DG.Tweening;
using Extensions;
using UnityEngine;

namespace UI.Popups
{
    public abstract class PopupBase : MonoBehaviour
    {
        private CanvasGroup _cg;

        public virtual void Awake()
        {
            _cg = GetComponent<CanvasGroup>();

            _cg.alpha = 0;
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
        }

        public virtual async UniTask ShowAsync(PopupArgs args)
        {
            _cg.interactable = true;
            _cg.blocksRaycasts = true;

            await _cg.DOFade(1, .25f).ToUniTask();
        }

        public virtual async UniTask HideAsync()
        {
            _cg.interactable = false;
            _cg.blocksRaycasts = false;

            await _cg.DOFade(0, .25f).ToUniTask();
        }
    }
}