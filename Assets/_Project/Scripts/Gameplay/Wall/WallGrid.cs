using Assets._Project.Scripts.Gameplay.BallLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallGrid : MonoBehaviour
    {
        private const string GRID_BALL_LAYERNAME = "GridBall";
        private const string FALLING_BALL_LAYERNAME = "FallingBall";

        private Dictionary<Vector2Int, Ball> _grid = new();
        private float _cellSize = 1f;

        public int GridMaxY { get; private set; } = 0;

        public void AddBall(Ball ball)
        {
            Vector2Int coords = WorldToGrid(ball.transform.position);
            if (!_grid.ContainsKey(coords))
            {
                _grid.Add(coords, ball);

                ball.DisablePhysics();
                ball.transform.SetParent(transform);

                ball.gameObject.layer = LayerMask.NameToLayer(GRID_BALL_LAYERNAME);

                if(coords.y > GridMaxY)
                    GridMaxY = coords.y;
            }
        }

        public void RemoveBall(Ball ball)
        {
            Vector2Int coords = WorldToGrid(ball.transform.position);
            if (_grid.ContainsKey(coords))
            {
                _grid.Remove(coords);
                ball.transform.SetParent(null);

                ball.gameObject.layer = LayerMask.NameToLayer(FALLING_BALL_LAYERNAME);
            }
        }

        public Ball GetBallAt(Vector2Int coords)
        {
            _grid.TryGetValue(coords, out Ball ball);
            return ball;
        }

        public IEnumerable<Ball> GetAllBalls() => _grid.Values;

        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            Vector3 gridRelatedPos = transform.InverseTransformPoint(worldPos);

            int x = Mathf.RoundToInt(gridRelatedPos.x / _cellSize);
            int z = Mathf.RoundToInt(gridRelatedPos.z / _cellSize);
            return new Vector2Int(x, z);
        }

        public Vector3 GridToWorld(Vector2Int gridPos)
        {
            return new Vector3(gridPos.x * _cellSize, 0f, gridPos.y * _cellSize);
        }

        public bool Contains(Vector2Int coords) => _grid.ContainsKey(coords);
    }
}