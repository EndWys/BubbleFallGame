using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallFactory : IService
    {
        private BallPool _ballPool;

        private BallColorService _colorService;

        public BallFactory(BallPool pool)
        {
            _ballPool = pool;
            _ballPool.CreatePool();

            _colorService = ServiceLocator.Local.Get<BallColorService>();
        }

        public Ball SpawnBall(Vector3 position)
        {
            BallColor color = _colorService.GetRandomColor();
            Material colorMaterial = _colorService.GetMaterialForColor(color);

            Ball ball = _ballPool.GetObject();
            ball.transform.position = position;
            ball.Init(color, colorMaterial);
            return ball;
        }

        public void DespawnBall(Ball ball)
        {
            _ballPool.ReleaseObject(ball);
        }
    }
}