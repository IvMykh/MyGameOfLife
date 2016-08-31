using GameOfLife.UI.Properties;

namespace GameOfLife.UI.ViewModel
{
    static class AppConsts
    {
        static AppConsts()
        {
            SpaceGridSideLength = int.Parse(Resources.SpaceGridSideLength);

            MinCellSquareLength = int.Parse(Resources.MinCellSquareLength);
            MaxCellSquareLength = int.Parse(Resources.MaxCellSquareLength);
            ZoomStep            = int.Parse(Resources.ZoomStep);
        }

        public static int SpaceGridSideLength { get; private set; }

        public static int MinCellSquareLength { get; private set; }
        public static int MaxCellSquareLength { get; private set; }
        public static int ZoomStep            { get; private set; }

    }
}
