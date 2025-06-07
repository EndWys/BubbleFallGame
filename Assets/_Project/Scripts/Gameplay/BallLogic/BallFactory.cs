using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallFactory : MonoBehaviour
    {
        [SerializeField] private Ball _ballPrefab;

        public Ball SpawnBall(Vector3 position, BallColor color)
        {
            Ball ball = Instantiate(_ballPrefab, position, Quaternion.identity);
            ball.Initialize(color);
            return ball;
        }

        public void DespawnBall(Ball ball)
        {
            Destroy(ball.gameObject);
        }
    }
}