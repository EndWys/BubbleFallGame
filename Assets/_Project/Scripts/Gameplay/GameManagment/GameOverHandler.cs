using Assets._Project.Scripts.Gameplay.Player;
using Assets._Project.Scripts.Gameplay.Wall;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class GameOverHandler : MonoBehaviour
    {
        public static GameOverHandler Instance { get; private set; }

        [SerializeField] private PlayerShooter _shooter;
        [SerializeField] private WallGenerator _wallGenerator;
        [SerializeField] private WallMover _wallMover;
        
        [SerializeField] private float _restartDelay = 1f;

        private bool _isGameOver = false;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;

            _wallGenerator.Init();
            _wallMover.Init();
            _shooter.Init();
        }

        private void Start()
        {
            Restart();
        }

        public void TriggerGameOver()
        {
            if (_isGameOver)
                return;

            Debug.Log("Game Over!");

            _isGameOver = true;
            _wallMover.Stop();

            _wallGenerator.ClearWall();
            _shooter.DespawnCurrentPlayerBall();
            // TODO: UI

            StartCoroutine(RestartCoroutine());
        }

        private IEnumerator RestartCoroutine()
        {
            yield return new WaitForSeconds(_restartDelay);
            Restart();
        }

        private void Restart()
        {
            _isGameOver = false;

            ScoreManager.Instance.ResetScore();

            _wallMover.ResetMover();
            _wallGenerator.CreateNewWall();
            _shooter.RespawnPlayerBall();

        }
    }
}