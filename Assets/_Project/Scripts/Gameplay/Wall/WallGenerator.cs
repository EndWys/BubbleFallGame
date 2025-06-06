using Assets._Project.Scripts.Gameplay.BallLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallGenerator : MonoBehaviour
    {
        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private int _width = 10;  // X
        [SerializeField] private int _height = 15; // Z
        [SerializeField] private float _spacing = 1f;

        private List<List<Ball>> _wall = new();

        private void Start()
        {
            GenerateWall();
        }

        private void GenerateWall()
        {
            float offsetX = (_width - 1) * _spacing / 2f;

            for (int z = 0; z < _height; z++)
            {
                List<Ball> row = new();
                for (int x = 0; x < _width; x++)
                {
                    
                    float posX = x * _spacing - offsetX;
                    float posZ = -z * _spacing + 20f;

                    Vector3 position = new Vector3(posX, 0f, posZ);
                    Ball ball = _ballFactory.SpawnBall(transform.position + position);
                    ball.transform.SetParent(transform);
                    row.Add(ball);
                }

                _wall.Add(row);
            }
        }
    }
}