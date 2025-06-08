using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallFactory : MonoBehaviour
    {
        [SerializeField] private BallPool _ballPool;

        private void Awake()
        {
            _ballPool.CreatePool();
        }

        public Ball SpawnBall(Vector3 position, BallColor color)
        {
            Ball ball = _ballPool.GetObject();
            ball.transform.position = position;
            ball.Init(color);
            return ball;
        }

        public void DespawnBall(Ball ball)
        {
            _ballPool.ReleaseObject(ball);
        }
    }
}