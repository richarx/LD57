using UnityEngine;

public enum Direction4
{
    Up,
    Left,
    Down,
    Right,
}

public static class Direction4Extension
{
    public static Vector2 ToVector2(this Direction4 direction) => direction switch
    {
        Direction4.Up => Vector2.up,
        Direction4.Left => Vector2.left,
        Direction4.Down => Vector2.down,
        Direction4.Right => Vector2.right,

        _ => throw new System.NotImplementedException(),
    };

    public static float GetValueInDirection(this Direction4 direction, Vector2 input) => direction switch
    {
        Direction4.Up => -input.y,
        Direction4.Left => input.x,
        Direction4.Down => input.y,
        Direction4.Right => -input.x,

        _ => throw new System.NotImplementedException(),
    };
}
