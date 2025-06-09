using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.Gameplay.Wall;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallCollisionHandler : IService
    {
        private ScoreManager _gameScore;

        private BallFactory _ballFactory;
        private WallGrid _wallGrid;
        private BallMatchDetector _matchDetector;
        private FloatingBallFinder _floatingBallFinder;

        private int _ballsCountForPop = 3;
        private int _scoreForBall = 10;

        public BallCollisionHandler(WallGrid grid)
        {
            _wallGrid = grid;

            _ballFactory = ServiceLocator.Local.Get<BallFactory>();
            _gameScore = ServiceLocator.Local.Get<ScoreManager>();

            _matchDetector = new BallMatchDetector(_wallGrid);
            _floatingBallFinder = new FloatingBallFinder(_wallGrid);
        }

        public void HandleBallCollision(Ball playerBall)
        {
            _wallGrid.AddBall(playerBall);

            List<Ball> matchedBalls = _matchDetector.FindMatchingGroup(playerBall);

            if (matchedBalls.Count >= _ballsCountForPop)
            {
                PopBalls(matchedBalls);

                List<Ball> floatingBalls = _floatingBallFinder.FindFloatingBalls();
                FallFloatingBalls(floatingBalls);
            }
        }

        private void PopBalls(List<Ball> balls)
        {
            foreach (var ball in balls)
            {
                _wallGrid.RemoveBall(ball);
                _ballFactory.DespawnBall(ball);
                _gameScore.AddPoints(_scoreForBall);
            }
        }

        private void FallFloatingBalls(List<Ball> balls)
        {
            foreach (var ball in balls)
            {
                _wallGrid.RemoveBall(ball);
                ball.EnablePhysics();
                _gameScore.AddPoints(_scoreForBall);
            }
        }
    }
}