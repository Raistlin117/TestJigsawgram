using UnityEngine;

namespace UI.Screens.MainMenu
{
    public struct MenuPuzzleViewData
    {
        public string ArtId;
        public Sprite ArtSprite;
        public bool Locked;
        public bool Started;
        public int PuzzleSliceCount;
        public float ProgressBar;
    }
}