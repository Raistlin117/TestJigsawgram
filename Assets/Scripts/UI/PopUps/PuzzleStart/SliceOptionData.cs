using System;

namespace UI.PopUps.PuzzleStart
{
    public struct SliceOptionData
    {
        public int TotalPieces;
        public int Columns => (int)Math.Round(Math.Sqrt(TotalPieces));
        public int Rows => Columns;
    }
}