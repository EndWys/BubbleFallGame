using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public int Score { get; private set; }

        public event Action<int> OnScoreChange;

        private void Awake()
        {
            Instance = this;
        }

        public void ResetScore()
        {
            Score = 0;
            OnScoreChange?.Invoke(Score);
        }

        public void AddPoints(int amount)
        {
            Score += amount;
            OnScoreChange?.Invoke(Score);
        }
    }
}