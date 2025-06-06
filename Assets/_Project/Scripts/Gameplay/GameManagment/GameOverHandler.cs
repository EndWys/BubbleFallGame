using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class GameOverHandler : MonoBehaviour
    {
        public static GameOverHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        public void TriggerGameOver()
        {
            Debug.Log("Game Over!");
            // TODO: UI, переходы, рестарт
        }
    }
}