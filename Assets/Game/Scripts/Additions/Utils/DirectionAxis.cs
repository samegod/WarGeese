using System.Collections.Generic;
using UnityEngine;
using Additions.Enums;

namespace Additions.Utils
{
    public static class DirectionAxis
    {
        private static readonly Dictionary<Direction, Vector3> DirectionsDictionary = new Dictionary<Direction, Vector3>()
        {
            {Direction.None, Vector3.zero},
            {Direction.Left, Vector3.left},
            {Direction.Right, Vector3.right},
            {Direction.Up, Vector3.up},
            {Direction.Down, Vector3.down},
            {Direction.Forward, Vector3.forward},
            {Direction.Back, Vector3.back},
        };

        public static Vector3 GetAxis (Direction direction)
        {
            return DirectionsDictionary[direction];
        }
    }
}