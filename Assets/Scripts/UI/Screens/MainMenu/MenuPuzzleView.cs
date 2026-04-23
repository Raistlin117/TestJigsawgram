using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class MenuPuzzleView : MonoBehaviour
    {
        [SerializeField] private Image _art;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _progress;
        [SerializeField] private TextMeshProUGUI _sliceAmountText;
        [SerializeField] private Image _progressBar;
        [SerializeField] private Button _selectButton;

        private string _artId;
        public event Action<string> OnSelected;

        private void Awake()
        {
            _selectButton.onClick.AddListener(HandleClick);
        }

        public void Apply(MenuPuzzleViewData data)
        {
            _artId = data.ArtId;
            _art.sprite = data.ArtSprite;

            if (data.Locked)
            {
                _lock.SetActive(true);
                _progress.SetActive(false);
            }
            else if (data.Started)
            {
                _lock.SetActive(false);
                _progress.SetActive(true);
                _sliceAmountText.text = data.PuzzleSliceCount.ToString();
                _progressBar.fillAmount = data.ProgressBar;
            }
            else
            {
                _lock.SetActive(false);
                _progress.SetActive(false);
            }
        }

        private void HandleClick()
        {
            OnSelected?.Invoke(_artId);
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveAllListeners();
            OnSelected = null;
        }
    }
}