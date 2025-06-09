using Assets._Project.Scripts.Gameplay.Player;
using Assets._Project.Scripts.Gameplay.Wall;
using Assets._Project.Scripts.UI;
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

        [SerializeField] private GameOverUIPanel _gameOverUI;
        [SerializeField] private GameUIPanel _gameUI;
        [SerializeField] private ReloadUIPanel _reloadUI;
        
        [SerializeField] private float _restartDelay = 1f;

        private bool _isGameOver = false;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;


            _gameOverUI.Init();
            _gameUI.Init();
            _reloadUI.Init();


            _wallGenerator.Init();
            _wallMover.Init();
            _shooter.Init();
        }

        private void Start()
        {
            Restart();
        }

        public async void TriggerGameOver()
        {
            if (_isGameOver)
                return;

            Debug.Log("Game Over!");

            _isGameOver = true;
            _wallMover.Stop();

            _wallGenerator.ClearWall();
            _shooter.DespawnCurrentPlayerBall();
            // TODO: UI
            await _gameUI.Hide();
            await _gameOverUI.Show();

            Restart();
        }

        private async void Restart()
        {
            await _reloadUI.Show();
            await _gameOverUI.Hide();
            await _gameUI.Show();

            _isGameOver = false;

            ScoreManager.Instance.ResetScore();

            _wallMover.ResetMover();
            _wallGenerator.CreateNewWall();
            _shooter.RespawnPlayerBall();

            await _reloadUI.Hide();
        }
    }
}