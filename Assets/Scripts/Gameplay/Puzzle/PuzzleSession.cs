using Gameplay.Arts;

namespace Gameplay.Puzzle
{
    public class PuzzleSession
    {
        public ArtConfig Config { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public bool IsConfigured => Config != null;

        public void Configure(ArtConfig config, int columns, int rows)
        {
            Config = config;
            Columns = columns;
            Rows = rows;
        }

        public void Reset()
        {
            Config = null;
            Columns = 0;
            Rows = 0;
        }
    }
}