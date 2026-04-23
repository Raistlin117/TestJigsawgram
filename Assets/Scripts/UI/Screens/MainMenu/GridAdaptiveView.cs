using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class GridAdaptiveView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        public void Apply(GridSettings settings)
        {
            _gridLayoutGroup.constraintCount = settings.Columns;
            _gridLayoutGroup.cellSize = new Vector2(settings.CellSize, settings.CellSize);
            _gridLayoutGroup.spacing = new Vector2(settings.Spacing, settings.Spacing);
            _gridLayoutGroup.padding.left = (int)settings.PaddingLeft;

            var rectTransform = transform.GetComponent<RectTransform>();

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}