namespace OpenRA
{
    public static class Util
    {
        public static float TickFacing(float facing, float desiredFacing, float step)
        {
            var leftTurn = (facing - desiredFacing);
            var rightTurn = (desiredFacing - facing);
            if (leftTurn < step || rightTurn < step)
                return desiredFacing;
            return rightTurn < leftTurn ? facing + step : facing - step;
        }
    }
}