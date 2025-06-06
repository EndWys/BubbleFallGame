using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.Gameplay.Wall;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallCollisionHandler : MonoBehaviour
    {
        public static BallCollisionHandler Instance { get; private set; }

        [SerializeField] private WallGrid _wallGrid;

        private BallMatchDetector _matchDetector;
        private FloatingBallFinder _floatingBallFinder;


        private void Awake()
        {
            _matchDetector = new BallMatchDetector(_wallGrid);
            _floatingBallFinder = new FloatingBallFinder(_wallGrid);

            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        public void HandleBallCollision(Ball playerBall)
        {
            _wallGrid.AddBall(playerBall);

            List<Ball> matchedBalls = _matchDetector.FindMatchingGroup(playerBall);

            if (matchedBalls.Count >= 3)
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
                Destroy(ball.gameObject);
                ScoreManager.Instance.AddPoints(10);
            }
        }

        private void FallFloatingBalls(List<Ball> balls)
        {
            foreach (var ball in balls)
            {
                _wallGrid.RemoveBall(ball);
                ball.EnablePhysics();
                ScoreManager.Instance.AddPoints(10);
            }
        }
    }
}