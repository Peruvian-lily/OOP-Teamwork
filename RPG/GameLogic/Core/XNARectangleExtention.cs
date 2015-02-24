namespace RPG.GameLogic.Core
{
    using Microsoft.Xna.Framework;

    public static class XNARectangleExtention
    {
        public static bool IsPointInRect(this Rectangle rectangle, Vector2 point)
        {
            bool isPointXIn = point.X >= rectangle.X && point.X <= rectangle.X + rectangle.Width;
            bool isPointYIn = point.Y <= rectangle.Y && point.Y >= rectangle.Y - rectangle.Height;
            return isPointXIn && isPointYIn;
        }
    }
}
