namespace Core.GridSystem
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public static class DirectionExtensions
    {
        public static float ToDegrees(this Direction direction)
        {
            return (float)direction * 90f;
        }

        public static float ToClockwiseDegrees(this Direction direction)
        {
            return -direction.ToDegrees();
        }
    }
}