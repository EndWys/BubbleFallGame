using Assets._Project.Scripts.Gameplay.Wall;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class FloatingBallFinder
    {
        private readonly WallGrid _grid;
        private readonly List<Vector2Int> _directions = new()
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

        public FloatingBallFinder(WallGrid grid)
        {
            _grid = grid;
        }

        public List<Ball> FindFloatingBalls()
        {
            HashSet<Vector2Int> connected = new();
            Queue<Vector2Int> queue = new();


            foreach (Ball ball in _grid.GetAllBalls())
            {
                Vector2Int gridPos = _grid.WorldToGrid(ball.transform.position);
                if (gridPos.y >= _grid.GridMaxY)
                {
                    queue.Enqueue(gridPos);
                }
            }

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                if (connected.Contains(current)) continue;

                connected.Add(current);

                foreach (var dir in _directions)
                {
                    Vector2Int neighbor = current + dir;
                    if (_grid.Contains(neighbor) && !connected.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            
            List<Ball> floatingBalls = new();
            foreach (Ball ball in _grid.GetAllBalls())
            {
                Vector2Int pos = _grid.WorldToGrid(ball.transform.position);
                if (!connected.Contains(pos))
                {
                    floatingBalls.Add(ball);
                }
            }

            return floatingBalls;
        }
    }
}