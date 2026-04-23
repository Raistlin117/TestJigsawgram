namespace UI.Screens.MainMenu
{
    public class MenuGridCalculator : IGridCalculator
    {
        private const float AspectRatioThreshold = 1.35f;

        public GridSettings Calculate(float screenWidth, float screenHeight)
        {
            float aspect = screenHeight / screenWidth;
            int columns = aspect < AspectRatioThreshold ? 3 : 2;

            float spacing = 32.4f;
            float cellSize = columns == 2 ? 491.4f : 317.2f;

            return new GridSettings
            {
                CellSize = cellSize,
                Columns = columns,
                Spacing = spacing,
                PaddingLeft = spacing,
            };
        }
    }
}