using Gameplay.Arts;
using UI.Popups;
using UnityEngine;

namespace UI.PopUps.PuzzleStart
{
    public class PuzzleStartArgs : PopupArgs
    {
        public ArtConfig Config  { get; set; }
        public Sprite    Preview { get; set; }
    }
}
