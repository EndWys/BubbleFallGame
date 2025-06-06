using Assets._Project.Scripts.Gameplay.BallLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallGenerator : MonoBehaviour
    {
        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private int _width = 10;     // X
        [SerializeField] private int _length = 15;    // Z
        [SerializeField] private float _spacing = 1f;

        private List<List<Ball>> _wall = new();

        private void Start()
        {
            GenerateWall();
        }

        private void GenerateWall()
        {
            for (int z = 0; z < _length; z++)
            {
                List<Ball> row = new();
                for (int x = 0; x < _width; x++)
                {
                    Vector3 position = new Vector3(
                        x * _spacing - (_width / 2f * _spacing),
                        0f,
                        z * _spacing
                    );

                    Ball ball = _ballFactory.SpawnBall(transform.position + position);
                    ball.transform.SetParent(transform);
                    row.Add(ball);
                }
                _wall.Add(row);
            }
        }
    }
}