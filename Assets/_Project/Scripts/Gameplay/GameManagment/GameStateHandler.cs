using Assets._Project.Scripts.Gameplay.Player;
using Assets._Project.Scripts.Gameplay.Wall;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Assets._Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using System;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class GameStateHandler : IService
    {
        private PlayerShooter _shooter;
        private WallGenerator _wallGenerator;
        private WallMover _wallMover;

        private IGameOverdUI _gameOverUI;
        private IGameUI _gameUI;
        private IReloadUI _reloadUI;

        private bool _isGameOver = false;

        public GameStateHandler(PlayerShooter shooter, WallGenerator wallGenerator, WallMover wallMover)
        {
            _gameOverUI = ServiceLocator.Local.Get<IGameOverdUI>();
            _gameUI = ServiceLocator.Local.Get<IGameUI>();
            _reloadUI = ServiceLocator.Local.Get<IReloadUI>();

            _shooter = shooter;
            _wallGenerator = wallGenerator;
            _wallMover = wallMover;
        }

        public void StartGame()
        {
            try
            {
                Restart().Forget();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async UniTask TriggerGameOver()
        {
            if (_isGameOver)
                return;

            _isGameOver = true;
            _wallMover.Stop();

            _wallGenerator.ClearWall();
            _shooter.DespawnCurrentPlayerBall();
            
            await _gameUI.Hide();
            await _gameOverUI.Show();

            await Restart();
        }

        private async UniTask Restart()
        {
            await _reloadUI.Show();
            await _gameOverUI.Hide();
            await _gameUI.Show();

            _isGameOver = false;

            ServiceLocator.Local.Get<ScoreManager>().ResetScore();

            _wallMover.ResetMover();
            _wallGenerator.CreateNewWall();
            _shooter.RespawnPlayerBall();

            await _reloadUI.Hide();
        }
    }
}