using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private int _score;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        public void AddPoints(int amount)
        {
            _score += amount;
            Debug.Log($"Score: {_score}");
            // TODO: Update UI
        }
    }
}