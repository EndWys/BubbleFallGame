using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using DG.Tweening;
using UnityEngine;

namespace Assets._Project.Scripts.EntryPoint
{
    public class GameLevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private ServiceLocatorLoader_Game _serviceLocatorLoader;

        [SerializeField] private FallingBallsDespawnTrigger _despawnTrigger;
        void Start()
        {
            DOTween.SetTweensCapacity(1500, 1000);


            _serviceLocatorLoader.Load();

            _despawnTrigger.Init();

            ServiceLocator.Local.Get<GameStateHandler>().StartGame();
        }
    }
}