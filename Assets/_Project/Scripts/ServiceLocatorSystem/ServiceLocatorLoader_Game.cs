using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.Gameplay.Player;
using Assets._Project.Scripts.Gameplay.Wall;
using Assets._Project.Scripts.UI;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Game : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private ReloadUIPanel _reloadUIPanel;
        [SerializeField] private GameUIPanel _gameUIPanel;
        [SerializeField] private GameOverUIPanel _gameOverUIPanel;

        [Header("Gameplay")]
        [SerializeField] private PlayerShooter _playerShooter;
        [SerializeField] private WallGenerator _wallGenerator;
        [SerializeField] private WallMover _wallMover;
        [SerializeField] private BallColorService _colorService;
        [SerializeField] private BallPool _ballPool;

        [Header("Level")]
        [SerializeField] private Transform _ballsWallRoot;

        private ServiceLocator _local;

        public void Load()
        {
            _local = ServiceLocator.CreateLocalSceneServiceLocator();

            RegisterAllServices();
        }

        private void RegisterAllServices()
        {
            WallGrid grid = new WallGrid(_ballsWallRoot);

            _local.Register(new ScoreManager());

            _local.Register(_colorService);

            _local.Register(new BallFactory(_ballPool));
            _local.Register(new BallCollisionHandler(grid));

            //UI
            _reloadUIPanel.Init();
            _local.Register<IReloadUI>(_reloadUIPanel);
            _gameUIPanel.Init();
            _local.Register<IGameUI>(_gameUIPanel);
            _gameOverUIPanel.Init();
            _local.Register<IGameOverdUI>(_gameOverUIPanel);
            
            //Level
            _playerShooter.Init();
            _wallGenerator.Init(grid);
            _wallMover.Init();

            _local.Register(new GameStateHandler(_playerShooter, _wallGenerator, _wallMover));

        }

        private void OnDestroy()
        {
            UnregisterAllServices();
        }

        private void UnregisterAllServices()
        {
            if (_local == null)
                return;

            _local.Unregister<ScoreManager>();

            _local.Unregister<BallFactory>();
            _local.Unregister<BallCollisionHandler>();

            _local.Unregister<IReloadUI>();
            _local.Unregister<IGameUI>();
            _local.Unregister<IGameOverdUI>();

            _local.Unregister<GameStateHandler>();
        }
    }
}