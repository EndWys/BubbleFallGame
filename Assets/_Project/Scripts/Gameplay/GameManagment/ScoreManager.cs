using Assets._Project.Scripts.ServiceLocatorSystem;
using System;


namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class ScoreManager : IService
    {
        public int Score { get; private set; }

        public event Action<int> OnScoreChange;

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