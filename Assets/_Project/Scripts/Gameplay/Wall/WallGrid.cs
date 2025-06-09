using Assets._Project.Scripts.Gameplay.BallLogic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallGrid
    {
        private static readonly Vector2Int[] EvenRowDirections =
        {
            new(1, 0), new(0, 1), new(-1, 1),
            new(-1, 0), new(-1, -1), new(0, -1)
        };

        private static readonly Vector2Int[] OddRowDirections =
        {
            new(1, 0), new(1, 1), new(0, 1),
            new(-1, 0), new(0, -1), new(1, -1)
        };

        private Transform _gridRoot;

        private float _cellSize;
        private float _height;
        private float _width;

        private readonly Dictionary<Vector2Int, Ball> _grid = new();

        public int MaxY { get; private set; } = 0;

        public WallGrid(Transform gridRoot)
        {
            _gridRoot = gridRoot;
        }

        public void SetGridSize(float cellSize, float height, float width)
        {
            _cellSize = cellSize;
            _height = height;
            _width = width;
        }

        public void ResetGrid()
        {
            MaxY = 0;
        }

        public void AddBall(Ball ball)
        {
            Vector2Int gridPos = WorldToGrid(ball.transform.position);
            if (!_grid.ContainsKey(gridPos))
            {
                _grid[gridPos] = ball;

                ball.Attach(_gridRoot, GridToWorld(gridPos));

                if(gridPos.y > MaxY)
                    MaxY = gridPos.y;
            }
        }

        public void RemoveBall(Ball ball)
        {
            Vector2Int gridPos = WorldToGrid(ball.transform.position);
            if (_grid.ContainsKey(gridPos))
            {
                _grid.Remove(gridPos);
            }
        }

        public bool Contains(Vector2Int gridPos)
        {
            return _grid.ContainsKey(gridPos);
        }

        public Ball GetBall(Vector2Int gridPos)
        {
            return _grid.TryGetValue(gridPos, out var ball) ? ball : null;
        }

        public List<Ball> GetAllBalls()
        {
            return _grid.Values.ToList();
        }

        public Vector2Int[] GetNeighbors(Vector2Int pos)
        {
            var directions = (pos.y % 2 == 0) ? EvenRowDirections : OddRowDirections;

            List<Vector2Int> neighbors = new();
            foreach (var dir in directions)
            {
                Vector2Int neighbor = pos + dir;
                if (_grid.ContainsKey(neighbor))
                    neighbors.Add(neighbor);
            }

            return neighbors.ToArray();
        }

        public Vector2Int WorldToGrid(Vector3 position)
        {
            Vector3 gridRlatedPosition = _gridRoot.InverseTransformPoint(position);

            float rowHeight = _cellSize * Mathf.Sqrt(3f) / 2f;

            int z = Mathf.RoundToInt(gridRlatedPosition.z / rowHeight);
            float xOffset = (z % 2 != 0) ? _cellSize / 2f : 0f;
            float xLocal = gridRlatedPosition.x + (_width - 1) * _cellSize / 2f - xOffset;
            int x = Mathf.RoundToInt(xLocal / _cellSize);

            return new Vector2Int(x, z);
        }

        public Vector3 GridToWorld(Vector2Int gridPos)
        {
            float rowHeight = _cellSize * Mathf.Sqrt(3f) / 2f;
            float xOffset = (gridPos.y % 2 != 0) ? _cellSize / 2f : 0f;

            float x = gridPos.x * _cellSize - (_width - 1) * _cellSize / 2f + xOffset;
            float z = gridPos.y * rowHeight;

            return _gridRoot.TransformPoint(new Vector3(x, 0f, z));
        }
    }
}