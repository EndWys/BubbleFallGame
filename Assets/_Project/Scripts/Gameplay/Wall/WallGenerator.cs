using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallGenerator : MonoBehaviour
    {
        [SerializeField] private int _width = 10;  // X
        [SerializeField] private int _height = 15; // Z
        [SerializeField] private float _cellSize = 1f;

        private BallFactory _ballFactory;
        private WallGrid _wallGrid;

        private int _lastRowIndex = 0;
        private float zPositionOnLastRowGeneration;

        private float _hexHeight = Mathf.Sqrt(3f) / 2f;

        public void Init(WallGrid grid)
        {
            _ballFactory = ServiceLocator.Local.Get<BallFactory>();
            _wallGrid = grid;


            _wallGrid.SetGridSize(_cellSize, _height, _width);
        }

        public void CreateNewWall()
        {
            zPositionOnLastRowGeneration = transform.position.z;

            Generate�BaseWall();
        }

        public void ClearWall()
        {
            var balls = _wallGrid.GetAllBalls();

            foreach (var ball in balls)
            {
                _wallGrid.RemoveBall(ball);
                _ballFactory.DespawnBall(ball);
            }

            _wallGrid.ResetGrid();
        }

        private void Generate�BaseWall()
        {
            for (int z = 0; z < _height; z++)
            {
                GenerateRow(z);
                _lastRowIndex = z;
            }
        }

        private void GenerateRow(int rowIndex)
        {
            bool isOddRow = rowIndex % 2 != 0;
            int width = isOddRow ? _width - 1 : _width;

            float offsetX = (_width - 1) * _cellSize / 2f;
            float rowOffsetX = isOddRow ? _cellSize / 2f : 0f;
            float posZ = rowIndex * _cellSize * _hexHeight;

            for (int x = 0; x < width; x++)
            {
                float posX = x * _cellSize - offsetX + rowOffsetX;
                Vector3 position = new Vector3(posX, 0f, posZ);

                Ball ball = _ballFactory.SpawnBall(transform.position + position);
                _wallGrid.AddBall(ball);
            }
        }

        public void GenerateRowsIfNeeded(float zWallPosition)
        {
            if (zPositionOnLastRowGeneration - _cellSize > zWallPosition)
            {
                _lastRowIndex++;

                GenerateRow(_lastRowIndex);

                zPositionOnLastRowGeneration = zWallPosition;
            }
        }
    }
}