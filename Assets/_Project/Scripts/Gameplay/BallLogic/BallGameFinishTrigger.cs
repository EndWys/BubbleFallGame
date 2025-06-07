using Assets._Project.Scripts.Gameplay.GameManagment;
using UnityEngine;

public class BallGameFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameOverHandler.Instance.TriggerGameOver();
    }
}
