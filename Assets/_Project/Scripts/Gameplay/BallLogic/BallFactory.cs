using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallFactory : MonoBehaviour
    {
        [SerializeField] private Ball _ballPrefab;

        public Ball SpawnBall(Vector3 position, bool usePhysics = false)
        {
            BallColor color = BallColorService.Instance.GetRandomColor();
            Ball ball = Instantiate(_ballPrefab, position, Quaternion.identity);
            ball.Initialize(color, usePhysics);
            return ball;
        }
    }
}