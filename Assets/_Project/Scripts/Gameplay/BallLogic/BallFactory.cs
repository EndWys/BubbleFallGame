using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallFactory : MonoBehaviour
    {
        [SerializeField] private Ball _ballPrefab;

        public Ball SpawnBall(Vector3 position, bool usePhysics = false)
        {
            BallColor color = GetRandomColor();
            Ball ball = Instantiate(_ballPrefab, position, Quaternion.identity);
            ball.Initialize(color, usePhysics);
            return ball;
        }

        private BallColor GetRandomColor()
        {
            return (BallColor)Random.Range(0, Enum.GetValues(typeof(BallColor)).Length);
        }
    }
}