using Assets._Project.Scripts.Gameplay.BallLogic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallGenerator : MonoBehaviour
    {
        public const int MIN_WALL_Y = 0;

        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private WallGrid _wallGrid;

        [SerializeField] private int _width = 10;  // X
        [SerializeField] private int _height = 15; // Z
        [SerializeField] private float _cellSize = 1f;

        private float _hexHight = Mathf.Sqrt(3f) / 2f;

        public void Init()
        {
            _wallGrid.SetGridSize(_cellSize, _height, _width);
        }

        public void CreateNewWall()
        {
            GenerateWall();
        }

        public void ClearWall()
        {
            var balls = _wallGrid.GetAllBalls();

            foreach (var ball in balls)
            {
                _wallGrid.RemoveBall(ball);
                _ballFactory.DespawnBall(ball);
            }
        }

        private void GenerateWall()
        {
            float rowHeight = _cellSize * _hexHight;
            float offsetX = (_width - 1) * _cellSize / 2f;

            for (int z = MIN_WALL_Y; z < _height; z++)
            {
                bool isOddRow = z % 2 != 0;

                int width = isOddRow? _width - 1 : _width;

                for (int x = 0; x < width; x++)
                {
                    BallColor color = BallColorService.Instance.GetRandomColor();
                    float xOffset = isOddRow ? _cellSize / 2f : 0f;
                    float posX = x * _cellSize - offsetX + xOffset;
                    float posZ = -z * rowHeight + _height;

                    Vector3 position = new Vector3(posX, 0f, posZ);
                    Ball ball = _ballFactory.SpawnBall(transform.position + position, color);

                    _wallGrid.AddBall(ball);
                }
            }
        }
    }
}