using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public int Score { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void AddPoints(int amount)
        {
            Score += amount;
            Debug.Log($"Score: {Score}");
        }
    }
}