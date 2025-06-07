using Assets._Project.Scripts.Gameplay.Wall;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallMatchDetector
    {
        private readonly WallGrid _grid;

        public BallMatchDetector(WallGrid grid)
        {
            _grid = grid;
        }

        public List<Ball> FindMatchingGroup(Ball startBall)
        {
            List<Ball> result = new();
            HashSet<Vector2Int> visited = new();

            Vector2Int start = _grid.WorldToGrid(startBall.transform.position);
            BallColor color = startBall.Color;

            Queue<Vector2Int> queue = new();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                if (visited.Contains(current)) continue;
                visited.Add(current);

                Ball ball = _grid.GetBall(current);
                if (ball != null && ball.Color == color)
                {
                    result.Add(ball);

                    foreach (var neighbor in _grid.GetNeighbors(current))
                    {
                        if (!visited.Contains(neighbor) && _grid.Contains(neighbor))
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return result;
        }
    }
}