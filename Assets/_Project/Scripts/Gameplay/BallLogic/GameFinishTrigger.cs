using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            try
            {
                ServiceLocator.Local.Get<GameStateHandler>().TriggerGameOver().Forget();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
