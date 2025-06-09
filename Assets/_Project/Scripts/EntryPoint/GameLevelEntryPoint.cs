using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.EntryPoint
{
    public class GameLevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private ServiceLocatorLoader_Game _serviceLocatorLoader;

        [SerializeField] private FallingBallsDespawnTrigger _despawnTrigger;
        void Start()
        {
            _serviceLocatorLoader.Load();

            _despawnTrigger.Init();

            ServiceLocator.Local.Get<GameStateHandler>().StartGame();
        }
    }
}