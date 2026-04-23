using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PopUps.PuzzleStart
{
    public class SliceOptionView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _selectedMark;
        [SerializeField] private int _pieces;

        public int TotalPieces => _pieces;

        public event Action<SliceOptionView> OnSelected;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnSelected?.Invoke(this));

            SetSelected(false);
        }

        public void SetSelected(bool selected)
        {
            if (_selectedMark != null)
            {
                _selectedMark.SetActive(selected);
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            OnSelected = null;
        }
    }
}