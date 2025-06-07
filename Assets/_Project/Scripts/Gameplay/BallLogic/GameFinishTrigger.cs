using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.GameManagment;
using UnityEngine;

public class GameFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
            GameOverHandler.Instance.TriggerGameOver();
    }
}
